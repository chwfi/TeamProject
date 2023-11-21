using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class LoadingScreen : MonoBehaviour
{
    private bool isComplete = false;

    private void Awake()
    {
        AssetLoader.Instance.OnLoadComplete += HandleLoadComplete;
    }

    private void OnDisable()
    {
        AssetLoader.Instance.OnLoadComplete -= HandleLoadComplete;
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
