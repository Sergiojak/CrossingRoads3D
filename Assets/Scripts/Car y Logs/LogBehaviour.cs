using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LogBehaviour : MonoBehaviour
{

    public Transform[] waypoints;
    public float logSpeed = 5f;


    void FixedUpdate()
    {
        if (transform.position != waypoints[0].position)
        {
            transform.position = Vector3.MoveTowards(transform.position, waypoints[0].position, logSpeed * Time.deltaTime);
            // Vector3 waypointLocation = (waypoints[0].position - transform.position).normalized;
        }
        else
        {
            transform.position = waypoints[1].position;
        }
    }
}
