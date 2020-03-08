using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//cant use navmesh without this
using UnityEngine.AI;

public class RunAway : MonoBehaviour
{
    //Choose whether enemy delays at a target spot
    [SerializeField] bool patrolWait;
    [SerializeField] float timeWaiting = 3f;

    [SerializeField] float switchProbability = 0.2f;
    //distance to chase player
    [SerializeField] float chaseDistance = 10f;

    //List of all our targets involved
    [SerializeField] List<WayPoint> targets;
    public GameObject Player;

    //How often should the enemy shoot a bullet?
    private float shotDelay;
    [SerializeField] private float shotStart;

    public GameObject bullet;
    private Transform player;

    NavMeshAgent theNav;
    int current;
    bool moving;
    bool waiting;
    bool onward;
    float waittime;

    public void Start()
    {
        theNav = this.GetComponent<NavMeshAgent>();

        shotDelay = shotStart;

        //check that nav mesh is attached
        if (theNav == null)
        {
            Debug.LogError("nav mesh component not attached to " + gameObject.name);
        }
        else
        {
            if (targets != null && targets.Count >= 2)
            {
                current = 0;
                SetDestination();
            }
            else
            {
                Debug.Log("Not enough target waypoints.");
            }
        }
    }

    public void Update()
    {
        chasePlayer();

        if (moving && theNav.remainingDistance <= 1.0f)
        {
            moving = false;

            if (waiting)
            {
                waiting = true;
                waittime = 0f;
            }
            else
            {
                ChangeTarget();
                SetDestination();
            }
        }

        if (waiting)
        {
            waittime += Time.deltaTime;
            if (waittime >= timeWaiting)
            {
                waiting = false;

                ChangeTarget();
                SetDestination();
            }
        }
    }

    private void SetDestination()
    {
        if (targets != null)
        {
            Vector3 targetVector = targets[current].transform.position;
            theNav.SetDestination(targetVector);
            moving = true;
        }
    }

    private void ChangeTarget()
    {
        if (UnityEngine.Random.Range(0f, 1f) <= switchProbability)
        {
            onward = !onward;
        }

        if (onward)
        {
            current = (current + 1) % targets.Count;
        }
        else
        {
            if (--current < 0)
            {
                current = targets.Count - 1;
            }
        }
    }

    private void chasePlayer()
    {
        //defining distance in terms of enemies and player
        float distance = Vector3.Distance(transform.position, Player.transform.position);

        // Run away from them if player gets too close
        if (distance < chaseDistance)
        {
            Vector3 changeDirection = transform.position - Player.transform.position;

            Vector3 newPosition = transform.position + changeDirection;

            theNav.SetDestination(newPosition);

            //Bullet();
        }
    }

    /*private void Bullet()
    {
        if (shotDelay <= 0)
        {
            Instantiate(bullet, transform.position, Quaternion.identity);
            shotDelay = shotStart;
        }
        else
        {
            shotDelay -= Time.deltaTime;
        }
    }*/
}

