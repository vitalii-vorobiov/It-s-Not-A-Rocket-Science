using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;

public class ARGUIComponent : MonoBehaviour
{
    void Update()
    {
        bool isValidTouch = Input.touchCount > 0 && !Input.GetTouch(0).position.IsPointOverUiObject();
        if (isValidTouch)
        {
            RaycastHit hit;
            if (Physics.Raycast(transform.position, transform.forward, out hit) &&
                hit.transform.CompareTag("ARButton"))
            {
                ARButton component = hit.transform.GetComponent<ARButton>();
                component.PV_reference.RPC(component.PunFunction, RpcTarget.AllBuffered);
            }
        }
    }
}