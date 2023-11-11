using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;

[InitializeOnLoad] //�÷��� ��ư ������ �ʱ�ȭ�Ǵ� ��ũ��Ʈ��
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
            //����������� ������
            EditorSceneManager.SaveCurrentModifiedScenesIfUserWantsTo();
        }

       /* if (state == PlayModeStateChange.EnteredPlayMode)
        {
            //���� ���� ù��° ������ ������.
            if (EditorSceneManager.GetActiveScene().buildIndex != 0)
            {
                EditorSceneManager.LoadScene(0);
            }
        }*/
    }
}
