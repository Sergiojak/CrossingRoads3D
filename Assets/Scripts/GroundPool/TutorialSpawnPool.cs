using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialSpawnPool : MonoBehaviour
{
    public GameObject[] initialTerrain;
    public GameObject spawn;

    void Start()
    {
        int randomIndex = Random.Range(0, initialTerrain.Length);
        initialTerrain[randomIndex].transform.position = spawn.transform.position;
    }
}
