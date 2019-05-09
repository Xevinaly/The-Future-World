using System;
using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
public class SceneController : MonoBehaviour
{

    public void loadMission1()
    {
        SceneManager.LoadScene("Mission1");
    }
    public void loadMission2()
    {
        SceneManager.LoadScene("Mission2");
    }

    public void loadMission1Cutscene()
    {
        SceneManager.LoadScene("Mission1Cutscene");
    }

    public void loadGameOver()
    {
        SceneManager.LoadScene("GameOver");
    }

    public void loadScene(String sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

}
