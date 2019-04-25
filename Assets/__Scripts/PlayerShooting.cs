using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShooting : MonoBehaviour {

    public int playerDamage = 10;
    public float shootInterval = 0.15f;
    public float range = 100f;
    public GameObject bulletHolePrefab;

    float timer;
    Ray shootRay;
    RaycastHit shootHit;
    int shootableMask;
    ParticleSystem gunParticles;
    LineRenderer gunLine;
    Light gunLight;
    float effectTime = 0.2f;
    PlayerControllerCylinder playerScript;
    

    private void Awake()
    {
        shootableMask = LayerMask.GetMask("Shootable");
        gunParticles = GetComponent<ParticleSystem>();
        gunParticles.Stop();
        gunLight = GetComponent<Light>();
        gunLine = GetComponent<LineRenderer>();
        playerScript = GameObject.Find("PlayerCharacter").GetComponent<PlayerControllerCylinder>();
    }

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        timer += Time.deltaTime;
        if(Input.GetButton("Fire1") && timer >= shootInterval && playerScript.equipped)
        {
            Shoot();
        }

        if(timer > shootInterval * effectTime)
        {
            DisableEffects();
        }
	}

    public void DisableEffects()
    {
        gunLine.enabled = false;
        gunLight.enabled = false;
        gunParticles.Stop();
    }

    private void Shoot()
    {
        timer = 0f;
        gunLight.enabled = true;
        gunParticles.Stop();
        gunParticles.Play();
        gunLine.enabled = true;
        gunLine.SetPosition(0, transform.position);
        shootRay.origin = transform.position;
        shootRay.direction = transform.TransformDirection(Vector3.forward);

        if(Physics.Raycast(shootRay, out shootHit, range, shootableMask))
        {
            Enemy enemy = shootHit.collider.GetComponent<Enemy>();
            if(enemy != null)
            {
                print("enemy damage in playershooting");
                enemy.EnemyHealth -= playerDamage;
            }
            //This does stuff tho
            else if (shootHit.collider.CompareTag("Wall"))
            {
                print("hit a wall");
                Instantiate(bulletHolePrefab, shootHit.point, Quaternion.FromToRotation(Vector3.up, shootHit.normal));
            }
            gunLine.SetPosition(1, shootHit.point);
        }
        else
        {
            gunLine.SetPosition(1, shootRay.origin + shootRay.direction * range);
        }
    }
}
