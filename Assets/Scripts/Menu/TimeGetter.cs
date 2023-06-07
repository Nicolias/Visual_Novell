using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Networking;

public class TimeGetter : MonoBehaviour
{
    private void Start()
    {
        //StartCoroutine(GetServerTime());
    }

    public IEnumerator GetServerTime()
    {
        string url = "https://mvatzzbkru.temp.swtest.ru/servertime.php";

        UnityWebRequest webRequest = UnityWebRequest.Get(url);

        webRequest.timeout = 3;

        yield return webRequest.SendWebRequest();

        string timeasString = webRequest.downloadHandler.text;

        if (DateTime.TryParse(timeasString, out DateTime serverTime))
        {
            print(serverTime.ToString());
        }
    }
}