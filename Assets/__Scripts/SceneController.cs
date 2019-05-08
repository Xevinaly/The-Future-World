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

}
