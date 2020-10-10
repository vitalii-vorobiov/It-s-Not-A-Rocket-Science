using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
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
                if (hittedGameObject != hit.transform.gameObject && hittedGameObject != null)
                {
                    hittedGameObject.GetComponent<PhotonView>().RPC("Desselect", RpcTarget.AllBuffered);;
                }
                
                hittedGameObject = hit.transform.gameObject;
                hittedGameObject.GetComponent<PhotonView>().RPC("Select", RpcTarget.AllBuffered);
            }
            else
            {
                if (hittedGameObject != null)
                {
                    hittedGameObject.GetComponent<PhotonView>().RPC("Deselect", RpcTarget.AllBuffered);
                    hittedGameObject = null;
                }
            }
            
            if (hittedGameObject)
            {
                if (isValidTouch)
                {
                    hittedGameObject.GetComponent<DraggableObject>().RequestOwnerChange();
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
