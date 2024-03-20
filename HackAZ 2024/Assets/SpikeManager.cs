using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SpikeManager : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D other)
    {
       if (other.gameObject.CompareTag("Player"))
       {
           Tracker.Reset();
           SceneManager.LoadScene(SceneManager.GetActiveScene().name);
       }
    }
}
