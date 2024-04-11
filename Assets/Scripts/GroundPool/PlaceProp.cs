using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaceProp : MonoBehaviour
{
    public RandomPrefabSpawner randomPrefabSpawner;
    public Collider trigger; // El trigger donde se detectarán las salidas del objeto con el tag "prop"

    private void Start()
    {
        randomPrefabSpawner.SpawnRandomPrefab();  //llamamos la funcion mediante script al iniciar
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Prop"))
        {
            // Si el objeto que sale del trigger es un "prop" y es el mismo trigger seleccionado, generar un nuevo objeto
            randomPrefabSpawner.SpawnRandomPrefab();
        }
    }

}
