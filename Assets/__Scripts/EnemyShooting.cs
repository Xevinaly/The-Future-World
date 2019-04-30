using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShooting : MonoBehaviour {

    public int enemyDamage = 1;
    public float range = 100f;
    public GameObject bulletHolePrefab;

    private float timer = 0f;
    private bool shootenabled = false;
    Ray shootRay;
    RaycastHit shootHit;
    int shootableMask;
    ParticleSystem gunParticles;
    LineRenderer gunLine;
    Light gunLight;
    float effectTime = 0.2f;

    private void Awake()
    {
        shootableMask = LayerMask.GetMask("Shootable");
        gunParticles = GetComponent<ParticleSystem>();
        gunParticles.Stop();
        gunLight = GetComponent<Light>();
        gunLine = GetComponent<LineRenderer>();
    }

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (shootenabled)
        {
            timer += Time.deltaTime;
        }
        if(timer >= effectTime)
        {
            DisableEffects();
            shootenabled = false;
            timer = 0f;
        }
    }

    public void DisableEffects()
    {
        gunLine.enabled = false;
        gunLight.enabled = false;
        gunParticles.Stop();
    }

    public void Shoot(Vector3 target)
    {
        shootenabled = true;
        gunLight.enabled = true;
        gunParticles.Stop();
        gunParticles.Play();
        gunLine.enabled = true;
        gunLine.SetPosition(0, transform.position);
        shootRay.origin = transform.position;
        Vector3 temp = target - transform.position + new Vector3(0, transform.position.y, 0);
        shootRay.direction = temp;

        if (Physics.Raycast(shootRay, out shootHit, range, shootableMask))
        {
            PlayerControllerCylinder player = shootHit.collider.GetComponent<PlayerControllerCylinder>();
            if (player != null)
            {
                player.PlayerHealth -= enemyDamage;
            }
            else if (shootHit.collider.CompareTag("Wall"))
            {
                Instantiate(bulletHolePrefab, shootHit.point, Quaternion.FromToRotation(Vector3.up, shootHit.normal));
            }
            gunLine.SetPosition(1, target);
        }
        else
        {
            gunLine.SetPosition(1, shootRay.origin + shootRay.direction * range);
        }
    }
}
