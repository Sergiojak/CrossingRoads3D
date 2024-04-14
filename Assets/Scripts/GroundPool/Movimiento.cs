using UnityEngine;
using UnityEngine.UIElements;
using static SwipeController;

public class Movimiento : MonoBehaviour
{
    Vector3 pb_ClickInicial;
    Vector3 pb_AlSoltarClick;

    [SerializeField] GameObject pb_Player;
    [SerializeField] GameObject pb_Prop;

    float jumpDistance = 2f;
    public float pb_Offset = 75f;
    public float pb_Duration = 0.25f;

    public delegate void SeMueve(Vector3 diferencia);
    public event SeMueve OnSeMueve;

    public void Awake()
    {
        pb_Prop = this.gameObject;
    }
    private void Update()
    {
        RaycastHit pb_Hitinfo = PlayerBehaviour.raycastDirection;
        if (Input.GetMouseButtonDown(0) && PlayerBehaviour.instance.playerIsDEAD == false)
        {
            pb_ClickInicial = Input.mousePosition;
        }

        if (Input.GetMouseButtonUp(0) && PlayerBehaviour.instance.playerIsDEAD == false)
        {
            pb_AlSoltarClick = Input.mousePosition;
            Vector3 pb_Diferencia = pb_AlSoltarClick - pb_ClickInicial;

            if (Mathf.Abs(pb_Diferencia.magnitude) > pb_Offset)
            {
                pb_Diferencia = pb_Diferencia.normalized;
                pb_Diferencia.z = pb_Diferencia.y;

                if (Mathf.Abs(pb_Diferencia.x) > Mathf.Abs(pb_Diferencia.z))
                {
                    pb_Diferencia.z = 0.0f;
                }
                else
                {
                    pb_Diferencia.x = 0.0f;
                }

                pb_Diferencia.y = 0.0f;

                if (OnSeMueve != null)
                {
                    OnSeMueve(pb_Diferencia);
                }

                //Pararlo si el eprsonaje choca
                if (Physics.Raycast(PlayerBehaviour.instance.transform.position + new Vector3(0, 1f, 0), pb_Diferencia, out pb_Hitinfo, 1f))
                {
                    if (pb_Hitinfo.collider.tag != "Level")
                    {
                        if (pb_Diferencia.z != 0)
                        {
                            pb_Diferencia.z = 0;
                        }
                    }
                }

                //Movimiento hacia adelante
                if (pb_Diferencia.normalized.z >= 0)
                {
                    LeanTween.move(pb_Prop, pb_Prop.transform.position + new Vector3(0, 0, -pb_Diferencia.z) * jumpDistance, pb_Duration / 2).setEase(LeanTweenType.easeOutQuad); //vertical abajo
                }
                //Movimiento hacia atrás
                if (pb_Diferencia.normalized.z < 0 && PlayerBehaviour.instance.stepsBack < 3)
                {
                    LeanTween.move(pb_Prop, pb_Prop.transform.position + new Vector3(0, 0, -pb_Diferencia.z) * jumpDistance, pb_Duration / 2).setEase(LeanTweenType.easeOutQuad); //vertical abajo
                }
            }
            else if (Mathf.Abs(pb_Diferencia.magnitude) < pb_Offset)
            {
                Vector3 pb_Clicka = pb_Prop.transform.forward;

                //Pararlo si el personaje choca
                if (Physics.Raycast(PlayerBehaviour.instance.transform.position + new Vector3(0, 1f, 0), pb_Clicka, out pb_Hitinfo, 1f))
                {
                    if (pb_Hitinfo.collider.tag != "Level")
                    {
                        if (pb_Clicka.z != 0)
                        {
                            pb_Clicka.z = 0;
                        }
                    }
                }

                //Movimiento hacia adelante
                if (pb_Clicka.normalized.z >= 0)
                {
                    LeanTween.move(pb_Prop, pb_Prop.transform.position + new Vector3(0, 0, -pb_Diferencia.z) * jumpDistance, pb_Duration / 2).setEase(LeanTweenType.easeOutQuad); //vertical abajo
                }
            }
        }
    }
}