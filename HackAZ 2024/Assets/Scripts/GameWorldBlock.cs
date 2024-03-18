using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
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
            // Calculate the distance from the camera to the prefab. 
            // This value should be positive and within the camera's clipping range.
            float distanceFromCamera = Mathf.Abs(Camera.main.transform.position.z - transform.position.z);

            // Use the camera to convert the screen position of the mouse to a world position,
            // using the calculated distance for the z value.
            Vector3 worldPosition = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, distanceFromCamera));

            transform.position = worldPosition;

            if (Input.GetMouseButtonDown(0))
            {
                isClicked = true;
            }
        }

    }
}
