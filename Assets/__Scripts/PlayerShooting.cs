using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShooting : MonoBehaviour {

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
        else if(Input.GetKeyDown(KeyCode.E) && timer >= shootInterval && playerScript.equipped && playerScript.currWeaponInt == 2){
            Punch();
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

        if(Physics.Raycast(shootRay, out shootHit, range))
        {
            Enemy enemy = shootHit.collider.GetComponent<Enemy>();
            SecurityEnemy securityEnemy = shootHit.collider.GetComponent<SecurityEnemy>();
            if(enemy != null)
            {

                if (playerScript.currWeaponInt == 0){
                    enemy.EnemyHealth -= playerScript.playerDamagePistol;
                    GameObject.Find("PlayerCharacter").GetComponent<Actions>().Attack();
                }
                else if (playerScript.currWeaponInt == 1){
                    enemy.timeStunned = 500;
                    GameObject.Find("PlayerCharacter").GetComponent<Actions>().Attack();
                }
                else if (playerScript.currWeaponInt == 2){
                    enemy.EnemyHealth -= playerScript.playerDamageGauntlet;
                }
            }
            else if (securityEnemy != null){
                if (playerScript.currWeaponInt == 0){
                    securityEnemy.EnemyHealth -= playerScript.playerDamagePistol;
                    GameObject.Find("PlayerCharacter").GetComponent<Actions>().Attack();
                }
                else if (playerScript.currWeaponInt == 1){
                    securityEnemy.timeStunned = 500;
                    GameObject.Find("PlayerCharacter").GetComponent<Actions>().Attack();
                }
                else if (playerScript.currWeaponInt == 2){
                    securityEnemy.EnemyHealth -= playerScript.playerDamageGauntlet;
                }
            }
            //This does stuff tho
            else if (shootHit.collider.CompareTag("Wall"))
            {
                Instantiate(bulletHolePrefab, shootHit.point, Quaternion.FromToRotation(Vector3.up, shootHit.normal));
            }
            gunLine.SetPosition(1, shootHit.point);
        }
        else
        {
            gunLine.SetPosition(1, shootRay.origin + shootRay.direction * range);
        }
    }
    private void Punch()
    {
        timer = 0f;
        gunLight.enabled = false;
        // gunParticles.Stop();
        // gunParticles.Play();
        // gunLine.enabled = true;
        // gunLine.SetPosition(0, transform.position);
        shootRay.origin = transform.position;
        shootRay.direction = transform.TransformDirection(Vector3.forward);

        if(Physics.Raycast(shootRay, out shootHit, 4f, shootableMask))
        {
            Enemy enemy = shootHit.collider.GetComponent<Enemy>();
            if(enemy != null)
            {
                enemy.EnemyHealth -= playerScript.playerDamageCloseRange;
                
            }
          
            gunLine.SetPosition(1, shootHit.point);
        }
        else
        {
            gunLine.SetPosition(1, shootRay.origin + shootRay.direction * range);
        }
    }
}
