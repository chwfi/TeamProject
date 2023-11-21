using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class LoadingScreen : MonoBehaviour
{
    public bool _isComplete = false;

    private void OnEnable()
    {
        AssetLoader.OnLoadComplete += HandleLoadCOmplete;
    }

    private void OnDisable()
    {
        AssetLoader.OnLoadComplete -= HandleLoadCOmplete;
    }

    private void HandleLoadCOmplete()
    {
        _isComplete = true;
    }

    private void Update()
    {
        if( _isComplete )
        {
            SceneManager.LoadScene(SceneList.TestMap);
        }
    }
}
