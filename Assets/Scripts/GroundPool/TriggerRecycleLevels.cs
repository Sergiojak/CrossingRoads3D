using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerRecycleLevels : MonoBehaviour
{
    public LevelSpawner levelSpawner;
    public SpawnerIntermedio SpawnerIntermedio;


    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Level"))
        {
            // Si el objeto que entra en el otro collider es un "prop", añadirlo a la lista de prefabs a spawnear
            levelSpawner.levelsUsed.Add(other.gameObject);
            other.gameObject.SetActive(false);
            other.gameObject.transform.parent = null;
        }
        if (other.CompareTag("MidLevel"))
        {
            SpawnerIntermedio.inactiveObjects.Add(other.gameObject);
            other.gameObject.SetActive(false);
            other.gameObject.transform.parent = null;
        }
    }
}