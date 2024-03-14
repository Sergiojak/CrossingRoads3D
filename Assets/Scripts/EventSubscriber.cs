using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventSubscriber : MonoBehaviour
{
    //Con el EventSuscriber y el EventCreator podemos seguir haciendo la acción aunque se desactive el GameObject
    //a no ser que queramos lo contrario para lo que usaremos el OnDisable para desuscribirnos del evento

    //llama para usar al creator
    public EventCreator eventoMovimiento;
    void Start()
    {
        //Esto es suscribirse al evento, si no me suscribo no hace nada
        eventoMovimiento.OnPresionarEnter += HaPresionadoEnter;
    }

    public void OnDisable()
    {
        //Elimina suscripción (para que deje de hacer si se desactiva el GameObject)
        eventoMovimiento.OnPresionarEnter -= HaPresionadoEnter;
    }

    private void HaPresionadoEnter()
    {
        Debug.Log("Ha presionado Enter");
    }


}
