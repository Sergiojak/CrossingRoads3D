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

        if (coinAmount >= 4)
        {
            canvasWinScreen.SetActive(true);
            Time.timeScale = 0f;  //para pausar el juego, si quisiese reproducirlo de nuevo tendría que poner el Time.timeScale de nuevo a 1.

        }
    }


    public void OnDestroy()
    {
       //Elimina suscripción (para que deje de funcionar si se desactiva el GameObject)
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
            Time.timeScale = 0f;
        }
        if (other.gameObject.CompareTag("Coin"))
        {
            coinAmount++;
        }
    }
}
