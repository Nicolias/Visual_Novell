using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DialogSpeechView : SpeechView
{
    [SerializeField] private TMP_Text _speakerName;
    [SerializeField] private Image _speakerAvatar;

    public override void Show(ISpeechModel speechModel)
    {
        var model = (DialogSpeechModel)speechModel;

        _speakerName.text = model.SpeakerName;

        ShowAvatar(model);
    }

    public override void ShowSmooth(ISpeechModel speechModel)
    {
        var model = (DialogSpeechModel)speechModel;

        _speakerName.text = model.SpeakerName;

        ShowAvatar(model);
    }

    private void ShowAvatar(DialogSpeechModel model)
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