using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pauser : MonoBehaviour {
    private bool isPaused = false;
    public PlayerInventory inventory;

	// Use this for initialization
	void Start () {
		
	}
    void Update()
    {
        if (Input.GetKeyUp(KeyCode.Escape))
        {
            if (isPaused)
            {
                Time.timeScale = 1;
                isPaused = false;
                inventory.HideInventory();
            }
            else
            {
                Time.timeScale = 0;
                isPaused = true;
                inventory.showInventory();
                Camera.main.GetComponent<AudioSource>().Stop();
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
}
