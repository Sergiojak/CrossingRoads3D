using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialSpawnPool : MonoBehaviour
{
    public GameObject[] tutorial;
    public GameObject tutorialSpawnpoint;

    void Start()
    {
        int randomIndex = Random.Range(0, tutorial.Length);
        tutorial[randomIndex].transform.position = tutorialSpawnpoint.transform.position;
    }
}
