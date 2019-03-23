using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC : MonoBehaviour
{

	[Header("Set in Inspector")] 
	public Material defaultMat;
	public Material highlightedMat;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	
	private void OnTriggerEnter(Collider other)
	{
		if (other.gameObject.CompareTag("Player"))
		{
			gameObject.GetComponent<Renderer>().material = highlightedMat;
		}
	}

	private void OnTriggerExit(Collider other)
	{
		if (other.gameObject.CompareTag("Player"))
		{
			gameObject.GetComponent<Renderer>().material = defaultMat;
		}
	}
}
