using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DraggableItem : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    public Canvas worldSpaceCanvas;
    public Camera mainCamera;
    public GameObject yourElementPrefab;

    public void OnBeginDrag(PointerEventData eventData)
    {
        Debug.Log("Begin Drag");
    }

    public void OnDrag(PointerEventData eventData)
    {
        Debug.Log("Dragging");
        transform.position = Input.mousePosition;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        Debug.Log("End Drag");
        Vector3 screenPoint = new Vector3(eventData.position.x, eventData.position.y, mainCamera.nearClipPlane);
        Vector3 worldPoint = mainCamera.ScreenToWorldPoint(screenPoint);
        worldPoint.z = 0;
        Debug.Log(worldPoint);

        GameObject worldElement = Instantiate(yourElementPrefab, worldPoint, Quaternion.identity, worldSpaceCanvas.transform);

    }
}
