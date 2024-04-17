using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarBehaviour : MonoBehaviour
{
    public Transform[] waypoints;
    public float carSpeed = 5f;

    void Update()
    {
        if (transform.position != waypoints[0].position)
        {
            transform.position = Vector3.MoveTowards(transform.position, waypoints[0].position, carSpeed * Time.deltaTime);
        }
        else
        {
            transform.position = waypoints[1].position;
        }
    }
}
