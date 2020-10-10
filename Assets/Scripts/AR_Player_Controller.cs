﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using Photon.Pun;
using UnityEngine;

public class AR_Player_Controller : MonoBehaviour
{

    private PhotonView PV;
    private GameObject followedGameObject;

    // Start is called before the first frame update
    private void Awake()
    {
        PV = GetComponent<PhotonView>();
        followedGameObject = GameObject.FindWithTag("MainCamera");
    }

    private void Update()
    {
        if (!PV.IsMine)
            return;

        transform.position = followedGameObject.transform.position;
        transform.rotation = followedGameObject.transform.rotation;
    }
}