using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class IglooBehavior : MonoBehaviour
{
    public Transform checkpointPosition;
    public PlayerBehavior playerBehavior;
    private bool checkPointReached = false;
    private Animator animator;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (!checkPointReached)
        {
            Debug.Log("Igloo Reached");
            if (other.gameObject.CompareTag("Player"))
            {
                playerBehavior.SetSpawnPosition(checkpointPosition);
            }
            animator = GetComponent<Animator>();

            animator.SetBool("flagReached", true);

            checkPointReached = true;
        }
    }
}
