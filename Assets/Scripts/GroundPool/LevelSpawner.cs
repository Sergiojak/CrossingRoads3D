using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class LevelSpawner : MonoBehaviour
{
    public List<GameObject> levels;
    public List<GameObject> levelsUsed = new List<GameObject>();
    public GameObject triggerLevelsActivator;

    [SerializeField] GameObject levelsSpawnPosition;
    [SerializeField] GameObject emptyMovement;

    private void Start()
    {
        foreach (GameObject prefab in levels)
        {
            prefab.SetActive(false);
            levelsUsed.Add(prefab);
        }

        SpawnRandomPrefab();
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject == triggerLevelsActivator)
        {
            SpawnRandomPrefab();
        }
    }

    public void SpawnRandomPrefab()
    {
        if (levelsUsed.Count > 0)
        {
            int randomIndex = Random.Range(0, levelsUsed.Count);

            triggerLevelsActivator = levelsUsed[randomIndex];
            triggerLevelsActivator.SetActive(true);

            GameObject coin = triggerLevelsActivator.transform.GetChild(0).gameObject;
            coin.SetActive(true);

            triggerLevelsActivator.transform.position = levelsSpawnPosition.transform.position;

            levelsUsed.RemoveAt(randomIndex);

            triggerLevelsActivator.transform.parent = emptyMovement.transform;
        }
    }
}