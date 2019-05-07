using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitanActivator : MonoBehaviour
{

	public TitanController titan;
	public FollowCam cam;
	public PlayerControllerCylinder player;

	private void OnTriggerEnter(Collider other)
	{
		if (other.CompareTag("Player"))
		{
			titan.isActivated = true;
			cam.rotateAndZoomOut();
			player.rotate90();
			enabled = false;
		}
	}
}
