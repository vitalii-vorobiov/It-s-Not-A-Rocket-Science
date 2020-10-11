using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using Photon.Pun;
using UnityEngine;

public class AR_Player_Controller  : MonoBehaviour
{
    private PhotonView      PV;
    private GameObject      followedGameObject;
    private WrenchComponent wrenchComponent;
    private DragComponent   dragComponent;

    public string current_active;

    // Start is called before the first frame update
    private void Awake()
    {
        PV                 = GetComponent<PhotonView>();
        wrenchComponent    = GetComponent<WrenchComponent>();
        dragComponent      = GetComponent<DragComponent>();
        followedGameObject = GameObject.FindWithTag("MainCamera");
        if (PV.IsMine) {
            gameObject.tag                         = "Player";
            // gameObject.layer                       = 8;
            transform.GetChild(0).gameObject.layer = 8;
            transform.GetChild(1).gameObject.layer = 8;
        }
    }

    private void Start() {
        if (!PV.IsMine) return;
        
        GameObject.FindWithTag("UIController").GetComponent<UIController>().InitializePlayer();
        wrenchComponent.enabled = false;
        dragComponent.enabled   = true;
        current_active          = "dr";
    }

    public void ToggleTool() {
        if (!PV.IsMine) return;
        dragComponent.enabled   = !dragComponent.enabled;
        wrenchComponent.enabled = !wrenchComponent.enabled;
        if (dragComponent.enabled) {
            current_active = "dr";
        } else {
            current_active = "wld";
        }
    }

    private void Update()
    {
        if (!PV.IsMine)
            return;

        transform.position = followedGameObject.transform.position;
        transform.rotation = followedGameObject.transform.rotation;
    }
}