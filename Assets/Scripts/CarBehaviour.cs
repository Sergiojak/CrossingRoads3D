using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarBehaviour : MonoBehaviour
{
    public Transform[] waypoints;


    public float carSpeed = 5f;

    void Start()
    {
    }

    void Update()
    {
        if (transform.position != waypoints[0].position)
        {
            transform.position = Vector3.MoveTowards(transform.position, waypoints[0].position, carSpeed * Time.deltaTime);
           // Vector3 waypointLocation = (waypoints[0].position - transform.position).normalized;
        }
        else
        {
            transform.position = waypoints[1].position;
        }
    }
}
