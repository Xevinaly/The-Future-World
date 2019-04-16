using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletHole : MonoBehaviour {
    [Header("Set in Inspector")]
    public float timeExists = 3.0f;
    public float fadeTime = 0.5f;

    private float timer;
    private Material mat;
    private Color color;
	// Use this for initialization
	void Start () {
        timer = 0f;
        mat = transform.GetChild(0).GetComponent<Renderer>().material;
        color = mat.color;
	}
	
	// Update is called once per frame
	void Update () {
        timer += Time.deltaTime;
        if(timer >= timeExists)
        {
            color.a -= Time.deltaTime * (color.a / fadeTime);
            mat.color = color;
        }
        if(timer >= timeExists + fadeTime)
        {
            Destroy(gameObject);
        }
	}
}
