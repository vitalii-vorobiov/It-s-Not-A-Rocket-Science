using System;
using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;

public class ARButton : MonoBehaviour
{
    public string PunFunction;
    public PhotonView PV_reference;

    private void Start()
    {
        PV_reference = transform.parent.GetComponent<PhotonView>();
    }
}
