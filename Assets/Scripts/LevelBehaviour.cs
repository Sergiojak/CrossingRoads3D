using UnityEngine;
using UnityEngine.EventSystems;

public class LevelBehaviour : MonoBehaviour
{

    public GameObject ground;

    float jumpDistance = 2f;
    public float timeAnim = 0.25f;

    public void Awake()
    {
        ground = this.gameObject;
    }

    public void Start()
    {
        SwipeController.instance.OnSwype += MoveTarget;
    }

    public void OnDisable()
    {
        SwipeController.instance.OnSwype -= MoveTarget;
    }

    void MoveTarget(Vector3 direction)
    {
        RaycastHit raycastHit = PlayerBehaviour.raycastDirection;

        if (PlayerBehaviour.instance.canJump == true)
        {
            if (Physics.Raycast(PlayerBehaviour.instance.transform.position + new Vector3(0, 1f, 0f), direction, out raycastHit, 2f))
            {
                if (raycastHit.collider.tag != "Ground" && raycastHit.collider.tag != "Log")
                {
                    if (direction.z != 0)
                    {
                        direction.z = 0;
                    }
                    /* if (direction.x != 0)
                     {
                         direction.x = 0;
                     }*/
                }
            }

            if (direction.z < 0 && PlayerBehaviour.instance.stepsBack < 3)
            {
                LeanTween.move(ground, ground.transform.position + new Vector3(0, 0, -direction.normalized.z) * jumpDistance, timeAnim / 2).setEase(LeanTweenType.easeOutQuad); //vertical abajo
            }
            if (direction.z > 0)
            {
                LeanTween.move(ground, ground.transform.position + new Vector3(0, 0, -direction.normalized.z) * jumpDistance, timeAnim / 2).setEase(LeanTweenType.easeOutQuad); //vertical arriba
            }

            //Movimiento horizontal del mundo, anulado para darle el movimiento horizontal al jugador
           /* if (new Vector3(direction.x, 0, 0) != Vector3.zero)
            {
                LeanTween.move(ground, ground.transform.position + new Vector3(-direction.x, 0, 0) * jumpDistance, timeAnim / 2).setEase(LeanTweenType.easeOutQuad); //horizontal
            }*/
        }
    }
}