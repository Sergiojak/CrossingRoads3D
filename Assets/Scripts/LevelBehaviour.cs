using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelBehaviour : MonoBehaviour
{
    public SwipeController swipeController;
    public GroundPool groundPool;
    public PlayerBehaviour playerBehaviour;

    public GameObject ground;
    public bool isJumping = false;
    public int steps;

    private bool isRecycled = false;

    public int stepsCounter = 0;
    public float jumpDistance = 1.5f;
    public float timeAnim = 0.25f;

    public bool isAtInfiniteLevel = false;

    public void Awake()
    {
        ground = this.gameObject;
    }

    public void OnEnable()
    {
        swipeController.OnSwype += MoveTarget;
    }

    public void OnDisable()
    {
        swipeController.OnSwype -= MoveTarget;
    }

    void MoveTarget(Vector3 direction)
    {
        RaycastHit raycastHit = PlayerBehaviour.raycastDirection;

        if (playerBehaviour != null && isJumping == false)
        {
            if (Physics.Raycast(playerBehaviour.transform.position + new Vector3(0, 2f, 0), direction, out raycastHit, 1f))
            {
                if (raycastHit.collider.tag != "ProceduralTerrain")
                {
                    if (direction.z != 0)
                    {
                        direction.z = 0;
                    }
                }
            }
            if (direction != Vector3.zero)
            {
                if(isAtInfiniteLevel == false)
                {
                    LeanTween.move(ground, ground.transform.position + -direction.normalized * jumpDistance, timeAnim / 2).setOnComplete(() =>
                    {
                        //para que baje del saltito
                        LeanTween.move(ground, ground.transform.position + -direction.normalized / 2, timeAnim / 2);
                    });
                }

               /* if (direction.normalized.z == 1)
                {

                    steps++;
                }

                if (direction.normalized.z == -1)
                {
                   
                }*/
            }
        }
    }
    public void Update()
    {
        if (stepsCounter == 2 && isRecycled == true)
        {
            stepsCounter = 0;
            isRecycled = false;
        }
    }

    public void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            isJumping = false;
        }
    }
    public void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            isJumping = true;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            //desactivado
        }
    }
}
