using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerInventory : MonoBehaviour
{
	public List<AudioLogData> logs = new List<AudioLogData>();

	public List<CollectibleData> collectibles = new List<CollectibleData>();

	public bool isVisible = false;
	/*public List<Collectible> collectibles = new List<Collectible>();
	public List<Image> collectibleImages = new List<Image>();

	public List<AudioLog> audioLogs = new List<AudioLog>();
	public List<Image> audioLogImages = new List<Image>();
	public List<AudioClip> audioLogClips = new List<AudioClip>();


	public void addCollectible(Collectible c)
	{
		collectibles.Add(c);
		collectibleImages.Add(c.inventoryImage);
		//collectibleImages[collectibleImages.Capacity].enabled = true;
		
	}

	public void addAudioLog(AudioLog l)
	{
		audioLogs.Add(l);
		audioLogImages.Add(l.inventoryImage);
		//audioLogImages[audioLogImages.Capacity].enabled = true;
		audioLogClips.Add(l.audioClip);
	}

	public void RemoveCollectible(Collectible c)
	{
		collectibles.Remove(c);
		collectibleImages.Remove(c.inventoryImage);
	}
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}*/
	//public List<AudioLogData> logs = new List<AudioLogData>();

	public void addAudioLog(AudioLog l)
	{
		logs.Add(l.logData);
		//l.logData.inventoryImage.enabled = true;
	}

	public void addCollectible(Collectible c)
	{
		collectibles.Add(c.collectibleData);
		//c.collectibleData.inventoryImage.enabled = true;
	}

	public void showInventory()
	{
		isVisible = true;
		foreach(CollectibleData cd in collectibles)
		{
			cd.inventoryImage.enabled = true;
			cd.inventoryBackgroundImage.enabled = true;
		}

		foreach (AudioLogData ald in logs)
		{
			ald.inventoryImage.enabled = true;
			ald.inventoryBackgroundImage.enabled = true;
		}
	}
	
	public void HideInventory()
	{
		isVisible = false;
		foreach(CollectibleData cd in collectibles)
		{
			cd.inventoryImage.enabled = false;
			cd.inventoryBackgroundImage.enabled = false;
		}

		foreach (AudioLogData ald in logs)
		{
			ald.inventoryImage.enabled = false;
			ald.inventoryBackgroundImage.enabled = false;
		}
	}

}
