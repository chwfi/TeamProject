using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;

[InitializeOnLoad] //플레이 버튼 눌르면 초기화되는 스크립트다
public class SaveSceneEditor
{
    static SaveSceneEditor()
    {
        EditorApplication.playModeStateChanged += LoadStartScene;
    }

    private static void LoadStartScene(PlayModeStateChange state)
    {
        if (state == PlayModeStateChange.ExitingEditMode)
        {
            //저장안했으면 저장해
            EditorSceneManager.SaveCurrentModifiedScenesIfUserWantsTo();
        }

       /* if (state == PlayModeStateChange.EnteredPlayMode)
        {
            //실제 씬을 첫번째 씬으로 돌린다.
            if (EditorSceneManager.GetActiveScene().buildIndex != 0)
            {
                EditorSceneManager.LoadScene(0);
            }
        }*/
    }
}
