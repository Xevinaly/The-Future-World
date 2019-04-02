using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.UI;

public class AudioLog : MonoBehaviour
{
	[Header("Set in Inspector")] 
	public AudioClip clip;
	public Image InventoryImage;
	public Image InventoryBackgroundImage;
	public string inventoryName;
	[Header("Set dynamically")]
	public AudioLogData logData;

	public void Start()
	{
		logData = new AudioLogData(clip, inventoryName, InventoryImage, InventoryBackgroundImage);
	}
	
	
	//not implemented
	//public string Description;
	//
	//TODO: add description field and other stuff for UI	
}
