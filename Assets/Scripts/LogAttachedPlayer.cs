using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LogAttachedPlayer : MonoBehaviour
{
    [SerializeField]
    GameObject player;

    void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Player"))
        {
            player.transform.parent = this.transform;
        }
    }
    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            player.transform.parent = null;
        }
    }
}
