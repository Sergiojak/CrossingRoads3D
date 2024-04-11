using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
public class PlayerBehaviour : MonoBehaviour
{
    public SwipeController swipeController;
    public CoinBehaviour coinBehaviour;


    public static RaycastHit raycastDirection;

    [SerializeField]
    GameObject player;
    Rigidbody rb;

    public float jumpDistance = 1.5f;
    public float timeAnim = 0.25f;
    public bool isJumping;
    
    [SerializeField]
    GameObject canvasLoseScreen;

    [SerializeField]
    TextMeshProUGUI stepsText;
    int steps;
    private int stepsRecord = 0;


    public void Awake()
    {
        player = this.gameObject;
    }

    private void Start()
    {
        player = this.gameObject;
        swipeController = SwipeController.instance;

        SwipeController.instance.OnSwype += MoveTarget;

        rb = GetComponent<Rigidbody>();

        steps = PlayerPrefs.GetInt("Score", 0);
        stepsRecord = PlayerPrefs.GetInt("Record", 0);

        UpdateStepText();
    }
    private void Update()
    {
        //stepsText.text = "Steps: " + steps;
  
        /*if (textOnScreen == true)
        {
            textShowing += Time.deltaTime;
            if(textShowing >= 2f) 
            {
                textShowing = 0f;
                LeanTween.value(gameObject, UpdateAplhaText, 1f, 0f, 2f);
                textOnScreen = false;
            }
        }*/

        PlayerPrefs.SetInt("Steps", steps);
        PlayerPrefs.Save();

        if (steps > stepsRecord)
        {
            stepsRecord = steps;
            PlayerPrefs.SetInt("Record", stepsRecord);
            PlayerPrefs.Save();
        }

        /*PlayerPrefs.SetInt("Coins", coinAmount);
        PlayerPrefs.Save();*/

            UpdateStepText();
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
            RaycastHit hitinfo;
            Vector3 moveDirection = direction.normalized;

            if (Physics.Raycast(transform.position + new Vector3(0, 1f, 0), moveDirection, out hitinfo, 1f))
            {
                Debug.Log("Hit Something, Restricting Movement");
                Debug.DrawRay(transform.position, transform.TransformDirection(0, 0, 1) * hitinfo.distance, Color.red);
                raycastDirection = hitinfo;

                if (moveDirection.x != 0)
                {
                    moveDirection.x = 0;
                }
            }
            else
            {
                Debug.DrawRay(transform.position, transform.TransformDirection(0, 0, 1) * 20f, Color.green);
            }

            if (moveDirection != Vector3.zero)
            {
                if (moveDirection.x > 0)
                {
                    transform.eulerAngles = new Vector3(0, 90f, 0);
                }
                else if (moveDirection.x < 0)
                {
                    transform.eulerAngles = new Vector3(0, -90f, 0);
                }
                else if (moveDirection.z > 0)
                {
                    transform.eulerAngles = new Vector3(0, 0, 0);
                }
                else if (moveDirection.z < 0)
                {
                    transform.eulerAngles = new Vector3(0, 180f, 0);
                }

                LeanTween.move(player, player.transform.position + Vector3.up / 2, timeAnim / 2).setOnComplete(() =>
                {
                    //para que baje del saltito
                    LeanTween.move(player, player.transform.position - Vector3.up / 2, timeAnim / 2);
                });
                if (direction.normalized.z == 1)
                {
                    steps++;
                }
                if (direction.normalized.z == -1)
                {
                    steps--;
                }
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
        if (other.gameObject.tag == "Coin")
        {
            coinBehaviour.coinAmount += 1;
            other.gameObject.SetActive(false);
            coinBehaviour.ShowCoinUI();
        }

        if (other.gameObject.tag == "Log")
        {
            transform.parent = other.transform;
            LeanTween.cancel(gameObject);
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Log")
        {
            transform.parent = null;
        }
    }
    private void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground") || collision.gameObject.CompareTag("Log"))
        {
            isJumping = false;
        }
    }
    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground") || collision.gameObject.CompareTag("Log"))
        {
            isJumping = true;
        }
    }
    private void UpdateStepText()
    {
        stepsText.text = "Score: " + steps.ToString() + "\nRecord: " + stepsRecord.ToString();
    }
    /*private void UpdateAplhaText(float alpha)
    {
        coinAmountText.alpha = alpha;
    }*/
}