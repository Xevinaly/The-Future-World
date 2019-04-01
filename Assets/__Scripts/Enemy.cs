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

    private bool alarm;
    private int pointPatroled;
    private Vector3 target;
    private NavMeshAgent agent;
    private float alarmTime;
	// Use this for initialization
	void Start () {
        alarm = false;
        pointPatroled = 0;
        agent = GetComponent<NavMeshAgent>();
        alarmTime = 0.0f;
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

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Vector3 distance = other.transform.position - transform.position;
            RaycastHit[] hits = Physics.RaycastAll(transform.position, distance, distance.magnitude);
            if (hits.Length == 0) print("No Objects");
            string tempTag = "";
            float minDistance = Mathf.Infinity;
            foreach(RaycastHit hit in hits)
            {
                float temp = Vector3.Magnitude(hit.transform.position - transform.position);
                if(temp < minDistance)
                {
                    minDistance = temp;
                    tempTag = hit.transform.gameObject.tag;
                }
            }
            if (other.gameObject.CompareTag(tempTag))
            {
                alarm = true;
                alarmTime = 0.0f;
                target = other.transform.position;
            }
            else
            {
                if(alarmTime >= timeAlarmed)
                {
                    alarm = false;
                    alarmTime = 0.0f;
                }
                alarmTime += Time.deltaTime;
            }
        }
    }

    //public void Move(Vector3 speed)
    //{
    //    transform.position += speed;
    //}
}
