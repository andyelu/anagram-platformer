using UnityEngine;

public class PrefabFollower : MonoBehaviour
{
    private Camera mainCamera;
    private bool isPlaced = false;

    private void Start()
    {
        mainCamera = Camera.main; // Ensure your main camera has the "MainCamera" tag
    }

    private void Update()
    {
        if (!isPlaced)
        {
            Vector3 mousePos = Input.mousePosition;
            mousePos.z = mainCamera.nearClipPlane; // Adjust this based on your camera setup
            Vector3 worldPosition = mainCamera.ScreenToWorldPoint(mousePos);
            worldPosition.z = 0; // Ensure this matches your game's requirements
            transform.position = worldPosition;

            if (Input.GetMouseButtonDown(0)) // Left mouse button
            {
                isPlaced = true;
            }
        }
    }
}
