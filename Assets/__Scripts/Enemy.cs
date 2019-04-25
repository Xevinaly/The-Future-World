using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour {
    [Header("Set in Inspector")]
    public Vector3[] patrolRoute;
    public float moveSpeed = 3.0f;
    public float approximationRadius = 0.5f;
    public float timeAlarmed = 5.0f;
    public float shootTime = 2.0f;
    public float shootChargeTime = 1.0f;
    public int EnemyHealth = 10;
    public int EnemyDamage = 1;
    public int timeStunned = 0;

    private bool alarm;
    private int pointPatroled;
    private Vector3 target;
    private NavMeshAgent agent;
    private float alarmTime;
    private float timeToShoot;
    private float chargeTime;
	// Use this for initialization
	void Start () {
        alarm = false;
        pointPatroled = 0;
        agent = GetComponent<NavMeshAgent>();
        timeReset();
        if(agent == null)
        {
            agent = gameObject.AddComponent<NavMeshAgent>();
        }
        agent.speed = moveSpeed;
	}
	
	// Update is called once per frame
	void Update () {
        if (alarm)
        {
            agent.SetDestination(target);
        }
        else if (timeStunned <= 0)
        {
            // print("Time stunned " + timeStunned);
            this.gameObject.transform.GetChild(1).GetComponent<Animator>().enabled = true;
            if(patrolRoute.Length != 0)
            {
                if (Mathf.Abs((transform.position - patrolRoute[pointPatroled]).magnitude) < approximationRadius)
                {
                    pointPatroled++;
                    pointPatroled = pointPatroled % patrolRoute.Length;
                }
                agent.SetDestination(patrolRoute[pointPatroled]);
            }
        }
        else{
            print("stunned");
            timeStunned--;
            this.gameObject.transform.GetChild(1).GetComponent<Animator>().enabled = false;
        }
        if(EnemyHealth <= 0)
        {
            print("enemy destroyed from health");
            Destroy(gameObject);
        }
	}

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Vector3 distance = other.transform.position - transform.position;
            string tempTag = "";
            RaycastHit hit;
            if(Physics.Raycast(transform.position, distance,out hit, distance.magnitude))
            {
                tempTag = hit.collider.gameObject.tag;
            }
            if (other.gameObject.CompareTag(tempTag))
            {
                alarm = true;
                alarmTime = 0.0f;
                target = other.transform.position;
                timeToShoot += Time.deltaTime;
                if (timeToShoot >= shootTime)
                {
                    agent.isStopped = true;
                    chargeTime += Time.deltaTime;
                    if(chargeTime >= shootChargeTime)
                    {
                        shoot(other.transform.position);
                        timeReset();
                        agent.isStopped = false;
                    }
                }
            }
            else
            {
                if(alarmTime >= timeAlarmed)
                {
                    alarm = false;
                    timeReset();
                }
                alarmTime += Time.deltaTime;
            }
        }
    }

    public void timeReset()
    {
        alarmTime = 0.0f;
        timeToShoot = 0.0f;
        chargeTime = 0.0f;
    }

    public void shoot(Vector3 target)
    {
        RaycastHit hit;
        if(Physics.Raycast(transform.position, target - transform.position, out hit))
        {
            if (hit.collider.gameObject.CompareTag("Player"))
            {
                hit.collider.gameObject.GetComponent<PlayerControllerCylinder>().PlayerHealth -= EnemyDamage;
            }
        }
    }
}
