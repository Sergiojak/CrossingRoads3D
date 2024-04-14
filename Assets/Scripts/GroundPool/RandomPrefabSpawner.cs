using UnityEngine;
using System.Collections.Generic;

public class RandomPrefabSpawner : MonoBehaviour
{
    public List<GameObject> objectList = new List<GameObject>(); // Lista de objetos a spawnear
    public List<GameObject> inactiveObjects = new List<GameObject>(); // Lista de objetos inactivos
    private GameObject activeObject; // Objeto activo actual
    public GameObject spawnPoint; // Punto de spawn

    void Start()
    {
        // Inicializar lista de objetos inactivos
        foreach (GameObject obj in objectList)
        {
            obj.SetActive(false);
            inactiveObjects.Add(obj);
        }

        // Spawnear el primer objeto
        SpawnRandomObject();
    }

    void OnTriggerExit(Collider other)
    {
        // Cuando el objeto activo sale del trigger, spawnear otro objeto
        if (other.gameObject == activeObject)
        {
            SpawnRandomObject();
        }
    }

    void SpawnRandomObject()
    {
        // Si hay objetos inactivos disponibles
        if (inactiveObjects.Count > 0)
        {
            // Obtener un índice aleatorio
            int randomIndex = Random.Range(0, inactiveObjects.Count);

            // Activar un objeto inactivo aleatorio
            activeObject = inactiveObjects[randomIndex];
            activeObject.SetActive(true);

            // Establecer la posición del objeto spawn
            activeObject.transform.position = spawnPoint.transform.position;

            // Remover el objeto activado de la lista de objetos inactivos
            inactiveObjects.RemoveAt(randomIndex);
        }
    }
}
/*
public List<GameObject> prefabsToSpawn;
public Transform spawnPoint;
public List<GameObject> activeInstances = new List<GameObject>();

RandomPrefabSpawner
public void SpawnRandomPrefab()
{
    // Verificar si hay al menos un prefab en la lista
    if (prefabsToSpawn.Count == 0)
    {
        Debug.LogWarning("No hay prefabs en la lista para spawnear.");
        return;
    }

    // Generar un índice aleatorio dentro del rango de la lista de prefabs
    int randomIndex = Random.Range(0, prefabsToSpawn.Count);

    // Obtener el prefab aleatorio
    GameObject randomPrefab = prefabsToSpawn[randomIndex];

    // Verificar si ya hay una instancia activa del prefab seleccionado
    if (activeInstances.Contains(randomPrefab))
    {
        // Si ya hay una instancia activa, crear una nueva instancia
        GameObject newPrefabInstance = Instantiate(randomPrefab, spawnPoint.position, Quaternion.identity);
        activeInstances.Add(newPrefabInstance);
    }
    else
    {
        // Si no hay una instancia activa, simplemente activarla
        randomPrefab.SetActive(true);
        randomPrefab.transform.position = spawnPoint.position;
        activeInstances.Add(randomPrefab);
    }
}
*/