using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    [SerializeField] private int _scensCount;
    private List<AsyncOperation> _sceneLoaders = new();

    private void Start()
    {
        for (int i = 0; i < _scensCount; i++)
        {
            AsyncOperation async = SceneManager.LoadSceneAsync(i);
            async.allowSceneActivation = false;
            _sceneLoaders.Add(async);
        }
    }

    public void LoadScene(int sceneIndex)
    {
        _sceneLoaders[sceneIndex].allowSceneActivation = true;
    }
}
