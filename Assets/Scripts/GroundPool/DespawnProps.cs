using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DespawnProps : MonoBehaviour
{

    public RandomPrefabSpawner m_RandomPrefabSpawner;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Level"))
        {
            // Si el objeto que entra en el otro collider es un "prop", añadirlo a la lista de prefabs a spawnear
            m_RandomPrefabSpawner.inactiveObjects.Add(other.gameObject);
            other.gameObject.SetActive(false);
        }
    }

}