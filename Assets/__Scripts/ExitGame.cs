using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ExitGame : MonoBehaviour {


    private Button exit;
	// Use this for initialization
	void Start () {
        exit = GetComponent<Button>();
        exit.onClick.AddListener(Quit);
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void Quit()
    {
        UnityEditor.EditorApplication.isPlaying = false;
        Application.Quit();
    }
}
