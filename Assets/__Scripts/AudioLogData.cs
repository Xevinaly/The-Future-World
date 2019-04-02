using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AudioLogData {

	public AudioClip audioClip;
	public string inventoryName;
	public Image inventoryImage;
	public Image inventoryBackgroundImage;

	public AudioLogData(AudioClip c, string s, Image inv, Image bck)
	{
		audioClip = c;
		inventoryImage = inv;
		inventoryName = s;
		inventoryBackgroundImage = bck;
	}
}
