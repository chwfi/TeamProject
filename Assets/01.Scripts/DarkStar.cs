using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DarkStar : MonoBehaviour
{
    [SerializeField]
    private GameObject testTimeline;

    public void Ending()
    {
        testTimeline.gameObject.SetActive(true);
        Invoke("Sa", 11f);
    }

    private void Sa()
    {
        SceneManager.LoadScene(SceneList.EndingScene);
    }
}
