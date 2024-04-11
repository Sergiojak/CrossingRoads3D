using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class PropsBehaviour : MonoBehaviour
{
    public RandomPrefabSpawner randomPrefabSpawner;
    public PlayerBehaviour playerBehaviour;
    public SwipeController swipeController;

    [SerializeField] GameObject player;
    [SerializeField] GameObject prop;

    public float duration = 0.25f;


    public void Awake()
    {
        prop = this.gameObject;
    }

    public void OnEnable()
    {
        swipeController.OnSwype += MoveTarget;
    }

    public void OnDisable()
    {
        swipeController.OnSwype -= MoveTarget;
    }

    public void MoveTarget(Vector3 pb_Direction)
    {
        RaycastHit pb_HitInfo = PlayerBehaviour.raycastDirection;

        if (playerBehaviour.isJumping == false)
        {
            if (Physics.Raycast(player.transform.position + new Vector3(0, 1f, 0), pb_Direction, out pb_HitInfo, 1f))
            {
                Debug.Log("Hit Something, Restricting Movement");
                if (pb_HitInfo.collider.tag != "ProceduralTerrain")
                {
                    if (pb_Direction.z != 0)
                    {
                        pb_Direction.z = 0;
                    }
                }

                Debug.DrawRay(transform.position + new Vector3(0, 1f, 0), transform.forward * pb_HitInfo.distance, Color.red);
            }

            if (pb_Direction != Vector3.zero)
            {
                print("Se mueve");
                LeanTween.move(prop, prop.transform.position + new Vector3(0, 0, -pb_Direction.normalized.z), duration).setEase(LeanTweenType.easeOutQuad);
            }
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("SpawnProps"))
        {
            print("Sale");
            randomPrefabSpawner.SpawnRandomPrefab();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Recycle"))
        {
            this.gameObject.SetActive(false);
            randomPrefabSpawner.prefabsToSpawn.Add(this.gameObject);
        }
    }
}
