using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MessegeView : MonoBehaviour
{
    [SerializeField] private Image _avatar;
    [SerializeField] private TMP_Text _name, _messege;
    
    public void Initialize(Messege messege)
    {
        _avatar.sprite = messege.Avatar;
        _name.text = messege.Name;
        _messege.text = messege.MessegeText;
    }
}
