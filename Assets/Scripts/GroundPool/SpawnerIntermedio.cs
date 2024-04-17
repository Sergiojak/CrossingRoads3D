using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerIntermedio : MonoBehaviour
{
    public List<GameObject> objectsList;
    public List<GameObject> inactiveObjects = new List<GameObject>();
    public GameObject triggerSpawnMidLevels;

    [SerializeField] GameObject spawnPoint;
    [SerializeField] GameObject propParent;

    private int propsActivated = 0;

    [SerializeField] GameObject middleSpawnProp;

    private void Start()
    {
        foreach (GameObject prefab in objectsList)
        {
            prefab.SetActive(false);
            inactiveObjects.Add(prefab);
        }
        SpawnRandomPrefab();
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject == triggerSpawnMidLevels)
        {
            SpawnRandomPrefab();
        }
    }

    public void SpawnRandomPrefab()
    {
        if (propsActivated < 7 && propsActivated > 5)
        {
            this.enabled = false;
            middleSpawnProp.SetActive(true);
        }
        else
        {
            if (inactiveObjects.Count > 0)
            {
                int randomIndex = Random.Range(0, inactiveObjects.Count);

                triggerSpawnMidLevels = inactiveObjects[randomIndex];
                triggerSpawnMidLevels.SetActive(true);

                triggerSpawnMidLevels.transform.position = spawnPoint.transform.position;

                inactiveObjects.RemoveAt(randomIndex);

                triggerSpawnMidLevels.transform.parent = propParent.transform;

                propsActivated++;
            }
        }
    }
}