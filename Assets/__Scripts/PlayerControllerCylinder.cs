using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Scripting.APIUpdating;
using System;
using UnityEngine.AI;

public class PlayerControllerCylinder : MonoBehaviour
{
    Ray cameraRay;
    RaycastHit cameraRayHit;
    public bool equipped = false;
    private int equippedWait = 0;
    [Header("Set in Inspector")]
    public float speed = 0.5f;
    public float PlayerHealth = 100.0f;
    public int playerDamagePistol = 10;
    public int playerDamageCloseRange = 5;
    public int playerDamageGauntlet = 10;
    public float mouseDeadzone = 0.2f;
    Animator anim;
    public PlayerInventory inventory;
    NavMeshAgent nav;
    public int currWeaponInt;
    public Vector3 directionFixHorizonal;
    public Vector3 directionFixVertical;
 

    float angle = 0;
    private float camdiff;
    


    void Awake()
    {
        anim = GetComponent<Animator>();
        camdiff = Camera.main.transform.position.y - transform.position.y;
        nav = GetComponent<NavMeshAgent>();
    }
    void Start(){
        GetComponent<PlayerController>().SetArsenal("Empty");

    }

    private void Update()
    {
    }
    // Update is called once per frame
    void FixedUpdate()
    {
        Rotate();
        Move();
        equippedWait++;
        if (Input.GetKeyDown("space") && equippedWait > 10)
        {
            if (!equipped){
                equippedWait = 0;
                equipWeapon(currWeaponInt);
            }
            else{
                equippedWait = 0;
                unequipWeapon();
            }
        }
        else if (Input.GetKeyDown(KeyCode.Q) && equippedWait > 10){
            equippedWait = 0;
            if (equipped){
                currWeaponInt++;
                if (currWeaponInt > 2){
                    currWeaponInt = 0;
                }
                equipWeapon(currWeaponInt);
            }
        }
        else if (Input.GetKeyDown(KeyCode.E) && currWeaponInt == 2){
            GetComponent<Actions>().Attack();
        }
    }

    void Move()
    {
        float hAxis = Input.GetAxis("Horizontal");
        float vAxis = Input.GetAxis("Vertical");
        Vector3 horizontal = directionFixHorizonal.normalized * hAxis;
        Vector3 vertical = directionFixVertical.normalized * vAxis;
        Vector3 pos = horizontal + vertical;
        pos = pos.normalized * speed;
        if (hAxis != 0 || vAxis != 0){
            anim.SetFloat("Speed", speed);
        }
        else{
            anim.SetFloat("Speed", 0);
        }
        transform.position += pos;
        nav.updatePosition = true;

    }

    void Rotate()
    {
     
        float mouseX = Input.mousePosition.x;

        float mouseY = Input.mousePosition.y;


        Vector3 worldpos = Camera.main.ScreenToWorldPoint(new Vector3(mouseX, mouseY, camdiff));
        Vector3 lookDirection = new Vector3(worldpos.x, transform.position.y, worldpos.z);
        if ((lookDirection - transform.position).magnitude > mouseDeadzone)
        {
            transform.LookAt(lookDirection);
        }
    }
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("AudioLog"))
        {
            inventory.addAudioLog(other.gameObject.GetComponent<AudioLog>());
            AudioClip logAudio = other.gameObject.GetComponent<AudioSource>().clip;
            Camera.main.GetComponent<AudioSource>().clip = logAudio;
            Camera.main.GetComponent<AudioSource>().Play();
            Destroy(other.gameObject);
        }
        else if (other.gameObject.CompareTag("Collectible"))
        {
            inventory.addCollectible(other.gameObject.GetComponent<Collectible>());
            Destroy(other.gameObject);
        }

    }
    // private void OnTriggerStay(Collider other){
    //     if (other.gameObject.CompareTag("Enemy")){
    //         if (currWeaponInt == 2 && Input.GetKeyDown(KeyCode.E)){
    //             other.gameObject.GetComponent<Enemy>().EnemyHealth -= playerDamageCloseRange;
    //             print("punched");
    //         }
    //     }
    // }

    void equipPistol(){
        GetComponent<PlayerController>().SetArsenal("Gun");
        GetComponent<Actions>().Aiming();
    }
    void equipStunGun(){
        GetComponent<PlayerController>().SetArsenal("StunGun");
        GetComponent<Actions>().Aiming(); 
    }
    void equipWristDart(){
        GetComponent<PlayerController>().SetArsenal("WristDart");
        GetComponent<Actions>().Aiming(); 
    }

    void equipWeapon(int index){
        equipped = true;
        if (index == 0){
            equipPistol();
        }
        else if (index == 1){
            equipStunGun();
        }
        else if (index == 2){
            equipWristDart();
        }
    }
    void unequipWeapon(){
        equipped = false;
        GetComponent<Actions>().Stay();
        GetComponent<PlayerController>().SetArsenal("Empty");
    }
}
