using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using Photon.Pun;
using UnityEngine;

public class DetailsSpawner : MonoBehaviour
{
    [SerializeField] private string prefabName;
    private GameObject currentInstantiatedGO;

    void Start()
    {
        if (PhotonNetwork.IsMasterClient)
        {
            currentInstantiatedGO = PhotonNetwork.Instantiate(Path.Combine("PhotonPrefabs", prefabName),
                transform.position, Quaternion.identity);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject == currentInstantiatedGO)
        {
            currentInstantiatedGO.GetComponent<Rigidbody>().isKinematic = false;

            if (PhotonNetwork.IsMasterClient)
            {
                currentInstantiatedGO = PhotonNetwork.Instantiate(Path.Combine("PhotonPrefabs", prefabName),
                    transform.position, Quaternion.identity);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
    }
}