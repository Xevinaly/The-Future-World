using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class SecurityEnemy : MonoBehaviour {
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
    public bool canShoot = false;

    private bool alarm;
    private int pointPatroled;
    private Vector3 target;
    private NavMeshAgent agent;
    private float alarmTime;
    private float timeToShoot;
    private float chargeTime;
    private EnemyShooting shooting;
    private Animator animator;
	// Use this for initialization
	void Start () {
        animator = GetComponent<Animator>();
        alarm = false;
        pointPatroled = 0;
        agent = GetComponent<NavMeshAgent>();
        timeReset();
        if (canShoot)
        {
            shooting = transform.Find("EnemyShootPoint").GetComponent<EnemyShooting>();
        }
        if(agent == null)
        {
            agent = gameObject.AddComponent<NavMeshAgent>();
        }
        agent.speed = moveSpeed;
	}
	
	// Update is called once per frame
	void Update () {
        if (timeStunned <= 0){
            animator.enabled = true;
            animator.SetInteger("Speed",1);
            this.gameObject.GetComponent<NavMeshAgent>().speed = moveSpeed;
            if (alarm)
            {
                agent.SetDestination(target);
            }
            else 
            {
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
        }
        else {
            timeStunned--;
            animator.SetInteger("Speed",0);
            animator.enabled = false;
            this.gameObject.GetComponent<NavMeshAgent>().speed = 0;
        }

        if (alarm)
        {
            transform.LookAt(target);
        }

        if(EnemyHealth <= 0)
        {
            Destroy(gameObject);
        }
	}

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Player") && timeStunned <= 0)
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
                transform.LookAt(target);
                timeToShoot += Time.deltaTime;
                if (timeToShoot >= shootTime)
                {
                    agent.isStopped = true;
                    chargeTime += Time.deltaTime;
                    if(chargeTime >= shootChargeTime)
                    {
                        if(shooting != null)
                        {
                            shooting.Shoot(other.transform.position + new Vector3(0, 0.5f, 0));
                        }
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

    
}
