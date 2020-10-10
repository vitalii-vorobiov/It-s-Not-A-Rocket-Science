using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using Photon.Pun;
using UnityEngine;

public class AR_PlayerManager : MonoBehaviour
{
    private PhotonView PW;

    private void Awake()
    {
        PW = GetComponent<PhotonView>();
    }

    // Start is called before the first frame update
    void Start()
    {
        if (PW.IsMine)
        {
            CreateController();
        }
    }

    void CreateController()
    {
        PhotonNetwork.Instantiate(Path.Combine("PhotonPrefabs", "AR_PlayerController"), Vector3.zero,
            Quaternion.identity);
        
        
    }
}
