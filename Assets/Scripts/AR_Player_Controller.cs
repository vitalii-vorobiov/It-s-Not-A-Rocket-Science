using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using Photon.Pun;
using UnityEngine;

public class AR_Player_Controller : Singleton<AR_Player_Controller>
{
    private PhotonView      PV;
    private GameObject      followedGameObject;
    private WrenchComponent wrenchComponent;
    private DragComponent   dragComponent;

    // Start is called before the first frame update
    private void Awake()
    {
        PV                 = GetComponent<PhotonView>();
        wrenchComponent    = GetComponent<WrenchComponent>();
        dragComponent      = GetComponent<DragComponent>();
        followedGameObject = GameObject.FindWithTag("MainCamera");
        if (PV.IsMine)
        {
            gameObject.layer = 8;
            transform.GetChild(0).gameObject.layer = 8;
        }
    }

    private void Start() {
        if (!PV.IsMine) return;
        wrenchComponent.enabled = false;
        dragComponent.enabled   = true;
    }

    public void ToggleTool() {
        if (!PV.IsMine) return;
        dragComponent.enabled   = !dragComponent.enabled;
        wrenchComponent.enabled = !wrenchComponent.enabled;
    }

    private void Update()
    {
        if (!PV.IsMine)
            return;

        transform.position = followedGameObject.transform.position;
        transform.rotation = followedGameObject.transform.rotation;
    }
}