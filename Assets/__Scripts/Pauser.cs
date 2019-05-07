using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Pauser : MonoBehaviour {
    private bool isPaused = false;
    public PlayerInventory inventory;
    public Button resume;

	// Use this for initialization
	void Start () {
        resume.onClick.AddListener(Resume);
	}

    void Update()
    {
        if (Input.GetKeyUp(KeyCode.Escape))
        {
            if (isPaused)
            {
                Resume();
            }
            else
            {
                Time.timeScale = 0;
                isPaused = true;
                inventory.showInventory();
                Camera.main.GetComponent<AudioSource>().Stop();
                GameObject.Find("Canvas").transform.Find("PauseMenu").gameObject.SetActive(true);
            }
        }

        if (Input.GetKeyDown("1") && isPaused)
        {
            if (inventory.logs.Count > 0)
            {
                Camera.main.GetComponent<AudioSource>().clip = inventory.logs[0].audioClip;
                Camera.main.GetComponent<AudioSource>().Play();
            }
        }
    }

    private void Resume()
    {
        Time.timeScale = 1;
        isPaused = false;
        inventory.HideInventory();
        GameObject.Find("Canvas").transform.Find("PauseMenu").gameObject.SetActive(false);
    }
}
