using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitanActivator : MonoBehaviour
{

	public TitanController titan;

	private void OnTriggerEnter(Collider other)
	{
		if (other.CompareTag("Player"))
		{
			titan.isActivated = true;
		}
	}
}
