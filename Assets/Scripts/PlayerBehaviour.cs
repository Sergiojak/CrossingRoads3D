using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
public class PlayerBehaviour : MonoBehaviour
{
    public CoinBehaviour coinBehaviour;
    public LevelBehaviour levelBehaviour;

    public static PlayerBehaviour instance;
    public static RaycastHit raycastDirection;

    [SerializeField]
    public GameObject player;
    [SerializeField]
    SkinnedMeshRenderer playerMesh;
    Rigidbody rb;
    [SerializeField]
    BoxCollider playerCollider;

    public float jumpDistance = 1.5f;
    public float timeAnim = 0.25f;
    public bool canJump = true;

    public int steps = 0;
    public int stepsBack = 0;
  
    [SerializeField]
    CanvasGroup canvasGroupLoseScreen;
    public bool needLoseCanvas = false;

    [SerializeField]
    TextMeshProUGUI coinEndgameText;


    public void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(this);
        }
        player = this.gameObject;
    }

    private void Start()
    {
        player = this.gameObject;

        SwipeController.instance.OnSwype += MoveTarget;

        rb = GetComponent<Rigidbody>();

    }

    public void OnDisable()
    {
        SwipeController.instance.OnSwype -= MoveTarget;
    }

    public void OnDestroy()
    {
       SwipeController.instance.OnSwype -= MoveTarget;
    }
    void MoveTarget(Vector3 directionOfSwype)
    {
        if (canJump)
        {
            Vector3 playerDirection = directionOfSwype.normalized;
            RaycastHit hitRay;

            if (Physics.Raycast(transform.position + new Vector3(0, 1f, 0), playerDirection, out hitRay, 2f))
            {
                Debug.Log("Hit Something, Restricting Movement");

                if (playerDirection.x != 0)
                {
                    playerDirection.x = 0;
                }
            }
            //Rotación del jugador
            if (playerDirection != Vector3.zero)
            {
                if (playerDirection.x > 0)
                {
                    transform.eulerAngles = new Vector3(0, 90f, 0);
                }
                else if (playerDirection.x < 0)
                {
                    transform.eulerAngles = new Vector3(0, -90f, 0);
                }
                else if (playerDirection.z > 0)
                {
                    transform.eulerAngles = new Vector3(0, 0, 0);
                }
                else if (playerDirection.z < 0)
                {
                    transform.eulerAngles = new Vector3(0, 180, 0);
                }
                //Movimiento HORIZONTAL del jugador y animación de saltito
                LeanTween.move(player, player.transform.position + new Vector3(directionOfSwype.x, 0, 0) + Vector3.up / 2, timeAnim / 2).setEase(LeanTweenType.easeOutQuad).setOnComplete(() =>
                {
                    //baja del saltito
                    LeanTween.move(player, player.transform.position + new Vector3(directionOfSwype.x, 0, 0) - Vector3.up / 2, timeAnim / 2).setEase(LeanTweenType.easeOutQuad);
                });

                //sumar y restar steps
                if (directionOfSwype.normalized.z < 0 && stepsBack < 3)  //abajo y que se sumen los stepsback
                {
                    stepsBack++;
                }

                if (directionOfSwype.normalized.z > 0 && stepsBack == 0 && levelBehaviour.stopAddingSteps == false) //arriba y que sume si los steps back son cero 
                {
                    steps++;
                }
                if (directionOfSwype.normalized.z > 0 && stepsBack > 0)
                { 
                    stepsBack--;
                }
                canJump = false;
            }
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Coche")|| other.gameObject.CompareTag("Water"))
        {
            //para que no se mueva de ninguna manera, no se vea y no se active el isJumping de nuevo
            rb.isKinematic = true;
            playerMesh.enabled = false;
            playerCollider.enabled = false;
            SwipeController.instance.enabled = false;

            //muestra el canvas
            needLoseCanvas = true;
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
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground") || collision.gameObject.CompareTag("Log"))
        {
            canJump = true;
        }
    }
    public void ShowLoseScreenUI()
    {
        LeanTween.alphaCanvas(canvasGroupLoseScreen, 1f, 1);
        coinEndgameText.text = "Coins: " + coinBehaviour.coinAmount;
    }
}