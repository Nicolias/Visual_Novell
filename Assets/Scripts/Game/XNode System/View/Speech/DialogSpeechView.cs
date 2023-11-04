using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DialogSpeechView : SpeechView
{
    [SerializeField] private TMP_Text _speakerName;
    [SerializeField] private Image _speakerAvatar;

    public void Show(IDialogSpeechModel speechModel)
    {
        base.Show(speechModel);

        _speakerName.text = speechModel.SpeakerName;

        ShowAvatar(speechModel);
    }

    public void ShowSmooth(IDialogSpeechModel speechModel)
    {
        base.ShowSmooth(speechModel);

        _speakerName.text = speechModel.SpeakerName;

        ShowAvatar(speechModel);
    }

    private void ShowAvatar(IDialogSpeechModel model)
    {
        if (model.Avatar != null)
        {
            _speakerAvatar.sprite = model.Avatar;
            _speakerAvatar.color = new(1, 1, 1, 1);
        }
        else
        {
            _speakerAvatar.color = new(1, 1, 1, 0);
        }
    }
}