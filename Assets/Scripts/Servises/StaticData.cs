using UnityEngine;
using System.Collections.Generic;

public class StaticData : MonoBehaviour
{
    private Dictionary<int, int> _needPointsToRichLevel = new(); 

    [field: SerializeField] public string SpecWordForNickName { get; private set; }
    public string Nickname { get; private set; }

    private void Awake()
    {
        Nickname = "Везунчик";

        _needPointsToRichLevel.Add(2, 100);
        _needPointsToRichLevel.Add(3, 200);
    }

    public int HowManyPointesNeedForReach(int level)
    {
        return _needPointsToRichLevel[level];
    }

    public void SetNickname(string nickname)
    {
        Nickname = nickname;
    }
}
