using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomPrefabSpawner : MonoBehaviour
{
    public List<GameObject> prefabsToSpawn;
    public Transform spawnPoint;
    public List<GameObject> activeInstances = new List<GameObject>();


    public void SpawnRandomPrefab()
    {
        // Verificar si hay al menos un prefab en la lista
        if (prefabsToSpawn.Count == 0)
        {
            Debug.LogWarning("No hay prefabs en la lista para spawnear.");
            return;
        }

        // Generar un índice aleatorio dentro del rango de la lista de prefabs
        int selectLevelRandom = Random.Range(0, prefabsToSpawn.Count);

        // Obtener el prefab aleatorio
        GameObject randomLevel = prefabsToSpawn[selectLevelRandom];

        // Verificar si ya hay una instancia activa del prefab seleccionado
        if (activeInstances.Contains(randomLevel))
        {
            // Si ya hay una instancia activa, crear una nueva instancia //me aseguro de que no se quite el que está en uso para que no se recicle, solo se recicle el que llegue al final
            GameObject newLevelInstance = Instantiate(randomLevel, spawnPoint.position, Quaternion.identity);
            activeInstances.Add(newLevelInstance);
        }
        else
        {
            // Si no hay una instancia activa, simplemente activarla
            randomLevel.SetActive(true);
            randomLevel.transform.position = spawnPoint.position;
            activeInstances.Add(randomLevel);
        }
    }
}
