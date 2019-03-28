using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Transparent : MonoBehaviour {

    [Header("Set in Inspector")]
    public float fadeTime = 0.3f;
    public float tgtTransparency = 0.3f;
    public float maxTransparency = 1.0f;

    private float decreaseSpeed;
    private bool needTransparent;
    private Color originColor;
    // Use this for initialization
	void Start () {
        originColor = GetComponent<Renderer>().material.color;
        originColor.a = maxTransparency;
        GetComponent<Renderer>().material.color = originColor;
        decreaseSpeed = (maxTransparency - tgtTransparency) / (fadeTime / Time.deltaTime);
	}
	
	// Update is called once per frame
	void Update () {
        if (needTransparent)
        {
            if(originColor.a > tgtTransparency)
            {
                originColor.a -= decreaseSpeed;
            }
        }
        else
        {
            if(originColor.a < maxTransparency)
            {
                originColor.a += decreaseSpeed;
            }
        }
        GetComponent<Renderer>().material.color = originColor;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            needTransparent = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            needTransparent = false;
        }
    }
}
