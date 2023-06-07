using UnityEngine;

public class MessegeFactory : MonoBehaviour
{
    [SerializeField] private Transform _messegeContainer;
    [SerializeField] private MessegeView _playerMessegeTemplate, _characterMessegeTemplate;

    public void CreateMessege(Messege messege)
    {
        MessegeView newMessege;

        if (messege.SenderType == MessegeSenderType.Player)
           newMessege = Instantiate(_playerMessegeTemplate, _messegeContainer);
        else
            newMessege = Instantiate(_characterMessegeTemplate, _messegeContainer);

        newMessege.Initialize(messege);
    }
}