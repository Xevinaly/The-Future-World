using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.UI;

public class Collectible : MonoBehaviour {

    [Header("Set in Inspector")] 
    public Image InventoryImage;
    public Image InventoryBackgroundImage;
    public string inventoryName;
    [Header("Set dynamically")]
    public CollectibleData collectibleData;

    public void Start()
    {
        collectibleData = new CollectibleData(inventoryName, InventoryImage, InventoryBackgroundImage);
    }
}
