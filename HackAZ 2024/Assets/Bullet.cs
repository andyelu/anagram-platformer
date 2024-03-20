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
            //if (collision.gameObject.CompareTag("Player"))
            //{
            
            //SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            //}

        Destroy(gameObject);
    }
}
