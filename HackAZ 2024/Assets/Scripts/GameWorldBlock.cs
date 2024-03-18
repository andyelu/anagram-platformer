using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameWorldBlock : MonoBehaviour
{

    [SerializeField]
    private bool isClicked = false;

    void Update()
    {
        if (!isClicked)
        {
            Debug.Log("Moving");
            // Calculate the distance from the camera to the object. For example:
            float distanceFromCamera = Mathf.Abs(Camera.main.transform.position.z) + 10; // Adjust '10' as needed to ensure visibility

            // Convert the mouse position to a world position using the calculated distance
            Vector3 mousePosition = new Vector3(Input.mousePosition.x, Input.mousePosition.y, distanceFromCamera);
            Vector3 worldPosition = Camera.main.ScreenToWorldPoint(mousePosition);

            // Update the object's position
            transform.position = new Vector3(worldPosition.x, worldPosition.y, transform.position.z); // Keep original Z or set as needed
        }

    }
}
