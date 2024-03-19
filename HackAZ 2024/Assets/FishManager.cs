using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class FishManager : MonoBehaviour
{
    public int nextLevel = 0;
    void OnTriggerEnter2D(Collider2D other)
    {
       if (other.gameObject.CompareTag("Player"))
       {
           Tracker.Reset();
           SceneManager.LoadScene("Level " + nextLevel);
       }
    }

    }

    
