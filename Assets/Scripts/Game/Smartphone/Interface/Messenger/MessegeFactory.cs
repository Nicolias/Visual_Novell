using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

public class MessegeFactory : MonoBehaviour
{
    [SerializeField] private Transform _messegeContainer;
    [SerializeField] private MessegeView _playerMessegeTemplate, _characterMessegeTemplate;

    [SerializeField] private float _characterChatingTime;

    [SerializeField] private Transform _containerForAnswerButtons;
    [SerializeField] private ChoiceButton _answerMassegaButtonTemplate;

    private int _poolCount = 10;
    private Queue<MessegeView> _playerMessagesPool;
    private Queue<MessegeView> _characterMessagesPool;
    private List<MessegeView> _sentMessages = new List<MessegeView>();

    private void Awake()
    {
        _playerMessagesPool = new Queue<MessegeView>(CreateMessegesView(_playerMessegeTemplate, _poolCount));
        _characterMessagesPool = new Queue<MessegeView>(CreateMessegesView(_characterMessegeTemplate, _poolCount));
    }

    public void HidePreviouslyCreatedMessages()
    {
        foreach (var sentMessage in _sentMessages)
        {
            sentMessage.Hide();

            if (sentMessage.Sender == MessegeSenderType.Player)
                _playerMessagesPool.Enqueue(sentMessage);
            else
                _characterMessagesPool.Enqueue(sentMessage);
        }
    }

    public void Show(IEnumerable<Messege> messages)
    {
        foreach (var message in messages)
            ShowMessegeView(message.SenderType, message);
    }

    public IEnumerator Show(Messege message, Action messageCreated)
    {
        if (message.SenderType == MessegeSenderType.Player)
            yield return ShowPlayerMessage(message);
        else
            yield return ShowCharacterMessage(message);

        messageCreated?.Invoke();
    }

    private IEnumerator ShowPlayerMessage(Messege messege)
    {
        bool isMessageSent = false;
        ChoiceButton answerButton = Instantiate(_answerMassegaButtonTemplate, _containerForAnswerButtons);
        answerButton.Initialized(new ChoiseElement(GetTextForAnswerButton(messege.Text), () => isMessageSent = true));

        yield return new WaitUntil(() => isMessageSent == true);

        ShowMessegeView(MessegeSenderType.Player, messege);
        Destroy(answerButton.gameObject);
    }

    private IEnumerator ShowCharacterMessage(Messege messege)
    {
        if (_characterChatingTime <= 0)
            throw new InvalidProgramException("Нулевое время ожидания");

        yield return new WaitForSeconds(_characterChatingTime);

        ShowMessegeView(MessegeSenderType.Character, messege);
    }

    private void ShowMessegeView(MessegeSenderType senderType, Messege message)
    {
        MessegeView messegeView = senderType == MessegeSenderType.Player ?
                _playerMessagesPool.Dequeue() : _characterMessagesPool.Dequeue();

        _sentMessages.Add(messegeView);
        messegeView.Show(message);
    }

    private List<MessegeView> CreateMessegesView(MessegeView messageViewTemplate, int messagesCount)
    {
        List<MessegeView> messegesView = new List<MessegeView>();

        for (int i = 0; i < messagesCount; i++)
        {
            MessegeView newMessageView = Instantiate(messageViewTemplate, _messegeContainer);
            newMessageView.Hide();
            messegesView.Add(newMessageView);
        }

        return messegesView;
    }

    private string GetTextForAnswerButton(string messageText)
    {
        int resultLength = 10;
        StringBuilder stringBuilder = new StringBuilder();

        for (int i = 0; i < resultLength; i++)
            stringBuilder.Append(messageText[i]);

        stringBuilder.Append("...");

        return stringBuilder.ToString();
    }
}