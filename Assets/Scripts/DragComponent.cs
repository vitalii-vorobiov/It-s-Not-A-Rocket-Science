using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragComponent : MonoBehaviour
{
    private GameObject currentlyDraggedGameObject;
    private GameObject hittedGameObject;

    // Update is called once per frame
    void Update()
    {
        RaycastHit hit;
        bool isValidTouch = Input.touchCount > 0 && !Input.GetTouch(0).position.IsPointOverUiObject();


        if (currentlyDraggedGameObject is null)
        {
            if (Physics.Raycast(transform.position, transform.forward, out hit) &&
                hit.transform.CompareTag("Draggable"))
            {
                hittedGameObject = hit.transform.gameObject;
                hittedGameObject.GetComponent<DraggableObject>().Select();
            }
            else
            {
                if (hittedGameObject != null)
                {
                    hittedGameObject.GetComponent<DraggableObject>().Deselect();
                    hittedGameObject = null;
                }
            }
            
            if (hittedGameObject)
            {
                if (isValidTouch)
                {
                    currentlyDraggedGameObject = hittedGameObject;
                }
            }
        }

        if (currentlyDraggedGameObject != null)
        {
            if (isValidTouch)
            {
                currentlyDraggedGameObject.transform.position = transform.position + transform.forward* 1f;
            }
            else
            {
                currentlyDraggedGameObject = null;
            }
        }



        
    }
}
