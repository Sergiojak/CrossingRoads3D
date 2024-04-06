using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SwipeController : MonoBehaviour
{
    //Este sería el event Creator
    SwipeController swipeController;

    [SerializeField]
    GameObject player;

    Vector3 clickInicial;
    Vector3 alSoltarClick;

    public float offset = 75f;

    //singletone
    public static SwipeController instance;

    //declarar delegado y evento para movimiento
    public delegate void Swype(Vector3 direction);
    public event Swype OnSwype;

    [SerializeField]
    GameObject tryAgainCanvasScreen;

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
       // Debug.Log(Input.mousePosition); //Nos devuelve la posición del ratón

        //Vamos a guardar la posición inicial al clickar y la final al soltar el click, para calcular el vector y determinar si el movimiento ha sido arrastrando a la izq, drcha, arriba o abajo

        if (Input.GetMouseButtonDown(0))
        {
            clickInicial = Input.mousePosition;
        }
        //al soltar click del ratón calcula la distancia por la que hemos movido el ratón
        if (Input.GetMouseButtonUp(0))
        {
            alSoltarClick = Input.mousePosition;
            //Debug.Log("Posición inicial" + clickInicial + " posición final " + alSoltarClick); //Devuelve la posición inicial y la final del click
            Vector3 diferencia = alSoltarClick - clickInicial;
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

                if (OnSwype != null)
                {
                    OnSwype(diferencia);
                }
            }
        }
    }
    //Movimos el MoveTarget al MovimientoJugador (el suscriber)
}
