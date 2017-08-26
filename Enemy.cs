using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour {

    private NavMeshAgent agent;
    private Animator anim;
    public GameObject[] waypoints;
    private GameObject target;
    private EnemyHealth enemyHealth;
    public enum State {
        PATROL,
        CHASE,
        DEAD
    };

    public float patrolSpeed=0.5f;
    public float chaseSpeed=1f;
    private int waypointIndex = 0;

    private State state;
	// Use this for initialization
	void Start () {
        agent = GetComponent<NavMeshAgent>();
        anim = GetComponent<Animator>();
        state = Enemy.State.PATROL;
        enemyHealth = GetComponent<EnemyHealth>();
	}
	
	// Update is called once per frame
	void FixedUpdate () {
        if(enemyHealth.dead)
        {
            state = Enemy.State.DEAD;
        }
		switch(state)
        {
            case State.PATROL:
                Patrol();
                break;
            case State.CHASE:
                Chase();
                break;

        }
	}

    void Patrol()
    {
        //print(waypointIndex);
        if (Vector3.Distance(this.transform.position, waypoints[waypointIndex].transform.position) <= 2)
        {
            waypointIndex = (waypointIndex + 1) % (waypoints.Length);
        }

        else if (Vector3.Distance(this.transform.position,waypoints[waypointIndex].transform.position)>=2)
        {
            agent.SetDestination(waypoints[waypointIndex].transform.position);
            agent.speed = patrolSpeed;
        }
        

    }

    void Chase()
    {
        float distance = Vector3.Distance(transform.position, target.transform.position);
        if ( distance >= 5f && distance<=10f)
        {
            agent.speed = chaseSpeed;
            agent.SetDestination(target.transform.position);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag=="Player")
        {
            state = Enemy.State.CHASE;
            target = other.gameObject;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            state = Enemy.State.PATROL;
        }
    }
}
