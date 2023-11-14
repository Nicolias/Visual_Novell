using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoadMenuScene : MonoBehaviour
{
    [SerializeField] private Button _nextSceneButton;

    private void Start()
    {
        StartCoroutine(LoadScene());
    }

    private IEnumerator LoadScene()
    {
        yield return null;

        AsyncOperation async = SceneManager.LoadSceneAsync(1);
        async.allowSceneActivation = false;

        while (async.isDone == false)
        {
            if (async.progress >= 0.9f)
                SceneLoaded(async);

            yield return null;
        }
    }

    private void SceneLoaded(AsyncOperation async)
    {
        _nextSceneButton.gameObject.SetActive(true);

        _nextSceneButton.onClick.AddListener(() => 
        {
            _nextSceneButton.onClick.RemoveAllListeners();
            async.allowSceneActivation = true;
        });        
    }
}
