using System;
using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;

public class AR_Player_Controller : MonoBehaviour
{
    [SerializeField] private GameObject camera;

    private PhotonView PV;
    // Start is called before the first frame update
    private void Awake()
    {
        PV = GetComponent<PhotonView>();
    }
    
    void Start()
    {
        if (!PV.IsMine)
        {
            Destroy(camera);
        }
    }

}
