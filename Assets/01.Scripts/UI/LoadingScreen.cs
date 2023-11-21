using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class LoadingScreen : MonoBehaviour
{
    public bool isComplete = false;

    private void OnEnable()
    {
        AssetLoader.OnLoadComplete += HandleLoadComplete;
    }

    private void OnDisable()
    {
        AssetLoader.OnLoadComplete -= HandleLoadComplete;
    }

    private void HandleLoadComplete()
    {
        isComplete = true;
    }

    private void Update()
    {
        if( isComplete )
        {
            SceneManager.LoadScene(SceneList.TestMap);
            Destroy(gameObject);
        }
    }
}
