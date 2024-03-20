using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPlayerCollision : MonoBehaviour
{

    public GameWorldBlock block;

    private void OnTriggerEnter2D(Collider2D other)
    {

        Debug.Log("Entered");
        if (!block.isClicked && other.gameObject.CompareTag("Player"))
        {

            block.playerInTrigger = true;
            block.GrayOutAllChildImages();
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {

        Debug.Log("Exited");
        if (!block.isClicked && other.gameObject.CompareTag("Player"))
        {
            block.playerInTrigger = false;
            block.ColorizeAllChildImages();
        }
    }
}
