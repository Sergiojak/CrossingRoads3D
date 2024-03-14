using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CoinBehaviour : MonoBehaviour
{
    [SerializeField]
    GameObject coin;

 
    // Update is called once per frame
    void Update()
    {
        transform.Rotate(0f, 0f, 1f);
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Player"))
        {
            coin.SetActive(false);
        }
    }
}
