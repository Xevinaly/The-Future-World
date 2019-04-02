using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CollectibleData{
	
	public string inventoryName;
	public Image inventoryImage;
	public Image inventoryBackgroundImage;

	public CollectibleData(string s, Image inv, Image bck)
	{
		inventoryImage = inv;
		inventoryBackgroundImage = bck;
		inventoryName = s;
	}
}
