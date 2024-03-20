using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPlayerCollision : MonoBehaviour
{

    public GameWorldBlock block;

    private void OnTriggerEnter2D(Collider2D other)
    {

        Debug.Log("Entered");
        if (other.gameObject.CompareTag("Player"))
        {

            block.playerInTrigger = true;
            block.GrayOutAllChildImages(); // Optionally gray out images if the player is inside the trigger
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {

        Debug.Log("Exited");
        if (other.gameObject.CompareTag("Player"))
        {
            block.playerInTrigger = false;
            // Optionally reset the colorization if you want it to revert when the player leaves
        }
    }
}
