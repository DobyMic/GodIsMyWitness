using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class EnemyAI : MonoBehaviour
{

    public Transform target;
    public float speed =200f;
    public float nextWayPointDistance;

    Path path;
    int currentWayPoint = 0;
    bool reachedEndOfPath = false;

    public Transform enemyGFX;

    Seeker seeker;
    Rigidbody2D rb;

    // update the path every 0.5 seconds
    void Start()
    {

        seeker = GetComponent<Seeker> ();
        rb = GetComponent<Rigidbody2D> ();
        InvokeRepeating ("UpdatePath" , 0f , 0.5f);
   
    }

    // when a waypoint is reached on the path, start the next waypoint on path
    void UpdatePath ( )
    {

        if(seeker.IsDone())
        {
            seeker . StartPath (rb . position , target . position , OnPathComplete);
        }

    }

    // when the path is finished reset the current waypoint to 0
    void OnPathComplete(Path p)
    {
        if(!p.error)
        {
            path = p;
            currentWayPoint = 0;
            
        }
    }

   
    void FixedUpdate()
    {

        if ( path == null )
        {
            return;
        }
        
        // when the last waypoint is reached stop moving
        if(currentWayPoint >= path.vectorPath.Count)
        {
            reachedEndOfPath = true;
            return;
        }
        else
        {
            reachedEndOfPath = false;
        }

        // move towards the current waypoint by applying force to the gameObjects rigidbody in the direction of the waypoint
        Vector2 direction = ((Vector2)path.vectorPath[currentWayPoint] - rb.position).normalized;
        Vector2 force = direction * speed * Time.deltaTime;

        rb . AddForce (force);

        float distance = Vector2.Distance(rb.position, path.vectorPath[currentWayPoint]);

        // once you are close enough to the waypoint, go to the next  
        if(distance < nextWayPointDistance)
        {
            currentWayPoint++;
        }


    

    }
}
