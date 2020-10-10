using System;
using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;

[RequireComponent(typeof(Renderer))]
public class DraggableObject : MonoBehaviour {
    private Color    color;
    private Renderer renderer;
    private bool     isSelected;
    private PhotonView PV;
    
    private void Awake() {
        renderer = gameObject.GetComponent<Renderer>();
        PV = gameObject.GetComponent<PhotonView>();
    }

   [PunRPC]
    public void Select() {
        if (!isSelected) {
            color      = renderer.material.color;
            isSelected = true;
        }
        renderer.material.SetColor("_Color", Color.magenta);
    }

    [PunRPC]
    public void Deselect() {
        isSelected = false;
        renderer.material.SetColor("_Color", color);
    }

    public void RequestOwnerChange()
    {
        PV.RequestOwnership();
    }
}
