using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Unity.VisualScripting.Antlr3.Runtime.Misc;

public class PlayerBehaviour : MonoBehaviour
{
    SwipeController swipeController;

    public SwipeController eventoEnter;

    [SerializeField]
    GameObject player;
    Rigidbody rb;

    public float jumpDistance = 1.5f;
    public float timeAnim = 0.25f;
    public bool isJumping;

    [SerializeField]
    TextMeshProUGUI coinAmountText;
    int coinAmount = 0;

    [SerializeField]
    GameObject canvasLoseScreen;


    [SerializeField]
    TextMeshProUGUI pasosText;
    public int steps;

    private void Start()
    {
        player = this.gameObject;
        swipeController = SwipeController.instance;

        SwipeController.instance.OnSwype += MoveTarget;

        rb = GetComponent<Rigidbody>();
    }
    private void Update()
    {
        pasosText.text = "Steps: " + steps;
        coinAmountText.text = "Coins: " + coinAmount;
    }
    public void OnDestroy()
    {
       //Elimina suscripción (para que deje de funcionar si se desactiva el GameObject)
       SwipeController.instance.OnSwype -= MoveTarget;
    }
    void MoveTarget(Vector3 direction)
    {
        //para que se mueva y de un saltito
        if(isJumping == false)
        {
         LeanTween.moveLocal(player, player.transform.position + direction.normalized * jumpDistance + Vector3.up / 2, timeAnim / 2).setOnComplete(() =>
         {
             //para que baje del saltito
            LeanTween.moveLocal(player, player.transform.position + direction.normalized / 2 - Vector3.up / 2, timeAnim / 2);

         });
            isJumping = true;
            if(direction.normalized.z == 1)
            {
                steps++;
            }
            if (direction.normalized.z == -1)
            {
                steps--;
            }      
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Coche")|| other.gameObject.CompareTag("Water"))
        {
            rb.isKinematic = true;
            Destroy(player);
            canvasLoseScreen.SetActive(true);
            Time.timeScale = 0f;
        }
        if (other.gameObject.CompareTag("Coin"))
        {
            coinAmount++;
        }
        if (other.gameObject.tag == "Log")
        {
            transform.parent = other.transform;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Log")
        {
            transform.parent = null;
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground") || collision.gameObject.CompareTag("Log"))
        {
            isJumping = false;
        }
    }
}