using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPlayerCollision : MonoBehaviour
{

    public GameWorldBlock block;

    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("Entered");

        // Check if the other object has a BoxCollider2D component
        BoxCollider2D boxCollider = other.GetComponent<BoxCollider2D>();
        if (boxCollider != null && !block.isClicked && other.gameObject.CompareTag("Player"))
        {
            block.playerInTrigger = true;
            block.GrayOutAllChildImages();
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        Debug.Log("Exited");

        // Check if the other object has a BoxCollider2D component
        BoxCollider2D boxCollider = other.GetComponent<BoxCollider2D>();
        if (boxCollider != null && !block.isClicked && other.gameObject.CompareTag("Player"))
        {
            block.playerInTrigger = false;
            block.ColorizeAllChildImages();
        }
    }
}
