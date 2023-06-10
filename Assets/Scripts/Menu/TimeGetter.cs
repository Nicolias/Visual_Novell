using HarioGames.WorldTimeAPIManager;
using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Networking;

public class TimeGetter : MonoBehaviour
{
    private void Start()
    {
        var a = WorldTimeAPIManager.Instance.GetCurrentDateTime();
        print(a);
    }   
}