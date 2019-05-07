using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class TitanController : MonoBehaviour
{

	public RailSystem railSystem;
	public bool isActivated = false;
	public float shootWarning = 4f;
	public float shootTimer = 8f;
	public float shootEndTimer = 9f;
	public float speed = 0.1f;
	private float timer = 0f;
	private bool isAtNode = true;
	public RailNode currentTarget;
	public GameObject POI;
	public float EnemyDamage;
	public bool isChargingShot = false;
	public ParticleSystem gunStartup;
	public LineRenderer gunLine;
	public GameObject gun;
	public RaycastHit hit;
	public GameObject explosion;
	
	void Start ()
	{
		transform.position = railSystem.startingNode.transform.position;
		currentTarget = railSystem.startingNode;
	}

	void Activate()
	{
		isActivated = true;
	}

	private void FixedUpdate()
	{
		if (isActivated)
		{
			transform.LookAt(POI.transform.position);
			AttemptShot();
			Move();
		}
	}

	private void AttemptShot()
	{
		timer += Time.deltaTime;
		if (timer >= shootWarning && !isChargingShot)
		{
			if (Physics.Raycast(transform.position, POI.transform.position - transform.position, out hit))
			{
				if (hit.collider.gameObject.CompareTag("Player"))
				{
					StartShot();
				}
			}
		} else if (timer >= shootEndTimer)
		{
			isChargingShot = false;
			gunLine.enabled = false;
			timer = 0;
		}else if (timer >= shootTimer && isChargingShot)
		{
			Shoot(POI.transform.position);
		} 
	}

	private void Move()
	{
		Vector3 approachNode = Vector3.MoveTowards(transform.position, currentTarget.transform.position, speed);
		if (approachNode == currentTarget.transform.position)
		{
			SwitchTargets();
		}
		transform.position = approachNode;
	}

	private void SwitchTargets()
	{
		if (currentTarget.isExplosionTrigger)
		{
			Destroy(this.gameObject);
			return;
		}

		if (currentTarget.isBroken)
		{
			Instantiate(explosion, this.transform);
		}
		currentTarget = currentTarget.nextNode;
	}
	
	private void Shoot(Vector3 target)
	{
		target = target + new Vector3(0, 6, 0);
		gunStartup.Stop();
		gunLine.enabled = true;
		Vector3 gunStart = gun.transform.position - new Vector3(0, 0, 1);
		gunLine.SetPosition(0, gunStart);

		if(Physics.Raycast(transform.position, POI.transform.position - transform.position, out hit))
		{
			PlayerControllerCylinder player = hit.collider.GetComponent<PlayerControllerCylinder>();
			if (player != null)
			{
				player.PlayerHealth -= EnemyDamage;
			}
			else
			{
				gunLine.SetPosition(1, hit.transform.position);
				return;
			}
		}
		gunLine.SetPosition(1, target);
	}

	void StartShot()
	{
		isChargingShot = true;
		gunStartup.Play();
	}
}
