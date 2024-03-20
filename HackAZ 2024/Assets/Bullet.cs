using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; 

public class Bullet : MonoBehaviour
{
    public float life = 3;

    void Awake()
    {
        Destroy(gameObject, life);
    }

    void OnCollisionEnter2D(Collision2D collision)
        {
            if (collision.gameObject.CompareTag("Player"))
        {
            Tracker.Reset();
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
        // else
        // {
        //     Destroy(collision.gameObject);
        // }
        Destroy(gameObject);
    }
}
