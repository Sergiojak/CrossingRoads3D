using UnityEngine;


public class SwipeController : MonoBehaviour
{
    Vector3 alClickar;
    Vector3 alSoltarClick;
    Vector3 tap;

    public float offset = 75f;

    //singletone
    public static SwipeController instance;

    //declarar delegado y evento para movimiento
    public delegate void Swype(Vector3 direction);
    public event Swype OnSwipe;

    //singletone
    private void Awake()
    {
        if (SwipeController.instance == null)
        {
            SwipeController.instance = this;
        }
        else
        {
            Destroy(this);
        }
    }

    void Update()
    {
       // Debug.Log(Input.mousePosition); //Nos devuelve la posici�n del rat�n

        //Vamos a guardar la posici�n inicial al clickar y la final al soltar el click, para calcular el vector y determinar si el movimiento ha sido arrastrando a la izq, drcha, arriba o abajo

        if (Input.GetMouseButtonDown(0) && PlayerBehaviour.instance.playerIsDead == false)
        {
            alClickar = Input.mousePosition;
            tap = Vector3.forward;

        }
        //al soltar click del rat�n calcula la distancia por la que hemos movido el rat�n
        if (Input.GetMouseButtonUp(0) && PlayerBehaviour.instance.playerIsDead == false)
        {
            alSoltarClick = Input.mousePosition;
            //Debug.Log("Posici�n inicial" + clickInicial + " posici�n final " + alSoltarClick); //Devuelve la posici�n inicial y la final del click
            Vector3 diferencia = alSoltarClick - alClickar;
            //Debug.Log(diferencia); //devuelve la trayectoria 


            if (Mathf.Abs(diferencia.magnitude) > offset)
            {
                diferencia = diferencia.normalized;
                diferencia.z = diferencia.y;

                if(Mathf.Abs(diferencia.x) > Mathf.Abs(diferencia.z))
                {
                    diferencia.z = 0.0f;
                }
                else
                {
                    diferencia.x = 0.0f;

                }

                diferencia.y = 0.0f;

                if (OnSwipe != null)
                {
                    OnSwipe(diferencia);
                }
            }
            else
            {
                Vector3 click = tap;

                if (OnSwipe != null)
                {
                    OnSwipe(click);
                }
            }
        }
    }
    //Se movi� MoveTarget al MovimientoJugador (el suscriber)
}
