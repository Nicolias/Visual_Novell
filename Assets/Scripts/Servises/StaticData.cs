using UnityEngine;

public class StaticData : MonoBehaviour
{
    public string Nickname { get; private set; }
    [field: SerializeField] public string SpecWordForNickName { get; private set; }

    public void SetNickname(string nickname)
    {
        Nickname = nickname;
    }

    private void OnEnable()
    {
        Nickname = "Везунчик";
    }
}
