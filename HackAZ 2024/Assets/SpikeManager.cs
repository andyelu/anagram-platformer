using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;



public class SpikeManager : MonoBehaviour
{
    public PlayerBehavior playerBehavior;
    void OnTriggerEnter2D(Collider2D other)
    {
       if (other.gameObject.CompareTag("Player"))
       {
            playerBehavior.TeleportBackToSpawnPoint();
        }
    }
}
