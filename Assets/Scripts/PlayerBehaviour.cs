using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
public class PlayerBehaviour : MonoBehaviour
{
    public CoinBehaviour coinBehaviour;
    public TodoMovimiento todoMovimiento;
    public StepsUI stepsUI;

    public static PlayerBehaviour instance;
    public static RaycastHit raycastDirection;

    public bool playerIsDead = false;

    float jumpDistance = 2f;

    [SerializeField]
    public GameObject player;
    [SerializeField]
    SkinnedMeshRenderer player3DModel;
    Rigidbody rb;
    [SerializeField]
    BoxCollider playerBoxCol;

    public float timeAnimation = 0.25f;
    public bool canJump = true;

    public int steps = 0;
    public int stepsBack = 0;
  
    [SerializeField]
    CanvasGroup canvasGroupLoseScreen;
    public bool needLoseCanvas = false;

    //activar botones y desactivar UI de fondo
    [SerializeField]
    GameObject buttonRestart;
    [SerializeField]
    GameObject buttonExit;

    [SerializeField]
    GameObject inGameUI;

    [SerializeField]
    TextMeshProUGUI coinEndgameText;

    [SerializeField]
    AudioSource takingCoinAudio;

    [SerializeField]
    AudioSource deathSound;

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

        SwipeController.instance.OnSwipe += MoveTarget;

        rb = GetComponent<Rigidbody>();

    }

    public void OnDisable()
    {
        SwipeController.instance.OnSwipe -= MoveTarget;
    }

    public void OnDestroy()
    {
       SwipeController.instance.OnSwipe -= MoveTarget;
    }
    void MoveTarget(Vector3 directionOfSwype)
    {
        if (canJump == true)
        {
            Vector3 playerDirection = directionOfSwype.normalized;
            RaycastHit hitRay;

            if (Physics.Raycast(transform.position + new Vector3(0, 1f, 0), playerDirection, out hitRay, 2f))
            {
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
                LeanTween.move(player, player.transform.position + new Vector3(directionOfSwype.normalized.x, 0, 0) + Vector3.up / 2 * jumpDistance, timeAnimation / 2).setEase(LeanTweenType.easeInSine).setOnComplete(() =>
                {
                    //baja del saltito
                    LeanTween.move(player, player.transform.position + new Vector3(directionOfSwype.normalized.x, 0, 0) - Vector3.up / 2 * jumpDistance , timeAnimation / 2 ).setEase(LeanTweenType.easeInSine);
                });


                //sumar y restar steps
                if (directionOfSwype.normalized.z < 0 && stepsBack < 3)  //abajo y que se sumen los stepsback
                {
                    stepsBack++;
                }

                if (directionOfSwype.normalized.z > 0 && stepsBack == 0 && todoMovimiento.stopUsingStepCounter == false) //arriba y que sume si los steps back son cero 
                {
                    steps++;
                }
                if (directionOfSwype.normalized.z > 0 && stepsBack > 0)
                { 
                    stepsBack--;
                }
                canJump = false; //para que no se pueda spammear el salto se desactiva la bool del salto
            }
        }
    }
    private void Update()
    {
        if(needLoseCanvas == true)
        {
            ShowLoseScreenUI();
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Coche")|| other.gameObject.CompareTag("Water"))
        {
            //para que no se mueva de ninguna manera, no se vea y no se active el isJumping de nuevo
            rb.isKinematic = true;
            player3DModel.enabled = false;
            playerBoxCol.enabled = false;
            SwipeController.instance.enabled = false;

            //muestra el canvas y activa los botones
            needLoseCanvas = true;
            buttonRestart.SetActive(true);
            buttonExit.SetActive(true);
            inGameUI.SetActive(false);

            //desactivar movimiento del swype...
            playerIsDead = true;
            deathSound.Play();
        }
        if (other.gameObject.tag == "Coin")
        {
            coinBehaviour.coinAmount += 1;
            other.gameObject.SetActive(false);
            coinBehaviour.ShowCoinUI();
            takingCoinAudio.Play();
        }
    }
 
    private void OnCollisionEnter(Collision collision)
    {

        //Puede saltar al tocar el suelo o el tronco
        if (collision.gameObject.CompareTag("Ground") || collision.gameObject.CompareTag("Log"))
        {
            canJump = true;
        }
    }
    public void ShowLoseScreenUI()
    {
        LeanTween.alphaCanvas(canvasGroupLoseScreen, 1f, 1); //FadeIn Animation
        coinEndgameText.text = "Coins: " + coinBehaviour.coinAmount; //actualizar texto
        if (stepsUI.activateMedal == true) //que se active la medalla y el texto del nuevo record si llegas a la medalla
        {
            stepsUI.medalSprite.SetActive(true);
            stepsUI.newRecordEndgameText.text = "New Record!: " + stepsUI.stepsRecord.ToString();
        }
        else
        {
            stepsUI.newRecordEndgameText.text = "";
        }
    }
}