using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventCreator : MonoBehaviour
{
    //SwipeController swipeController; //aqu� llamar�amos al otro script 
    //Se necesita el event generator, se usa delegate y nombre de la funci�n que queramos, y luego event y nombre de la funcion
    public delegate void PresionaEnter();
    public event PresionaEnter OnPresionarEnter;

    /*private void Start()
    {
        swipeController = SwipeController.instance;

    }*/

    void Update()
    {
        if(Input.GetKeyUp(KeyCode.Return))
        {
            if(OnPresionarEnter != null)
            {
                OnPresionarEnter();
            }
        }
    }
}