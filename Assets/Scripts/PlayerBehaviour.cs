using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerBehaviour : MonoBehaviour
{
    SwipeController swipeController;

    public SwipeController eventoEnter;

    [SerializeField]
    GameObject player;
    Rigidbody rb;

    public float timeAnim = 1f;

    [SerializeField]
    TextMeshProUGUI coinAmountText;
    int coinAmount = 0;

    [SerializeField]
    GameObject canvasWinScreen;

    [SerializeField]
    GameObject canvasLoseScreen;

    private void Start()
    {
        player = this.gameObject;
        swipeController = SwipeController.instance;

        SwipeController.instance.OnSwype += MoveTarget;

        rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        coinAmountText.text = "Coins: " + coinAmount;

        if (coinAmount >= 2)
        {
            canvasWinScreen.SetActive(true);
        }
    }


    public void OnDestroy()
    {
       //Elimina suscripción (para que deje de hacer si se desactiva el GameObject)
       SwipeController.instance.OnSwype -= MoveTarget;
    }

    void MoveTarget(Vector3 direction)
    {
        Debug.Log(direction);
        //player.transform.position += direction; //para que se mueva

        //para que se mueva y de un saltito
        LeanTween.moveLocal(player, player.transform.position + direction / 2 + Vector3.up / 2, timeAnim / 2).setOnComplete(() =>
        {
            LeanTween.moveLocal(player, player.transform.position + direction / 2 - Vector3.up / 2, timeAnim / 2);

        });
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Coche"))
        {
            rb.isKinematic = true;
            Destroy(player);

        
            canvasLoseScreen.SetActive(true);
         
        }

        if (other.gameObject.CompareTag("Coin"))
        {
            coinAmount++;
        }

    }
    void EndLevel()
    {

    }
}
