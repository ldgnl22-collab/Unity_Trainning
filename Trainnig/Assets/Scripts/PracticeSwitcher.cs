using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PracticeSwitcher : MonoBehaviour
{
    private const string SCENE_A = "StaticPracticeA";
    private const string SCENE_B = "StaticPracticeB";

    private void Update()
    {
        ReadSceneKey();
    }

    private void ReadSceneKey()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            MoveToOtherScene();
        }
    }

    private void MoveToOtherScene()
    {
        switch (SceneManager.GetActiveScene().name)
        {
            case SCENE_A:
                SceneManager.LoadScene(SCENE_B);
                break;
            case SCENE_B:
                SceneManager.LoadScene(SCENE_A);
                break;
        }
    }
}
