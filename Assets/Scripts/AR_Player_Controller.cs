using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using Photon.Pun;
using UnityEngine;

public class AR_Player_Controller : MonoBehaviour
{
    [SerializeField] private GameObject camera;

    private PhotonView PV;

    public float mouseSpeed;

    public float moveSpeed;

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

    private void FixedUpdate()
    {
        if (!PV.IsMine)
            return;
        if (Input.GetKey(KeyCode.W))
        {
            transform.position += transform.forward * moveSpeed;
        }

        if (Input.GetKey(KeyCode.S))
        {
            transform.position += -transform.forward * moveSpeed;
        }

        if (Input.GetKey(KeyCode.A))
        {
            transform.position += -transform.right * moveSpeed;
        }

        if (Input.GetKey(KeyCode.D))
        {
            transform.position += transform.right * moveSpeed;
        }

        if (Input.GetKey(KeyCode.E))
        {
            Debug.Log("Spawn");
            PhotonNetwork.Instantiate(Path.Combine("PhotonPrefabs", "SmallBolt"),
                transform.position + transform.forward + new Vector3(0, 0, 0.3f),
                Quaternion.LookRotation(transform.forward) * Quaternion.AngleAxis(-90, new Vector3(1, 0, 0)));
        }

        if (Input.GetKey(KeyCode.RightArrow))
        {
            Debug.Log("RIG");
            //Rotate the sprite about the Y axis in the positive direction
            transform.Rotate(new Vector3(0, 1, 0) * Time.deltaTime * mouseSpeed, Space.World);
        }

        if (Input.GetKey(KeyCode.LeftArrow))
        {
            //Rotate the sprite about the Y axis in the negative direction
            transform.Rotate(new Vector3(0, -1, 0) * Time.deltaTime * mouseSpeed, Space.World);
        }

        if (Input.GetKey(KeyCode.UpArrow))
        {
            Debug.Log("RIG");
            //Rotate the sprite about the Y axis in the positive direction
            transform.Rotate(new Vector3(-1, 0, 0) * Time.deltaTime * mouseSpeed, Space.World);
        }

        if (Input.GetKey(KeyCode.DownArrow))
        {
            //Rotate the sprite about the Y axis in the negative direction
            transform.Rotate(new Vector3(1, 0,0 ) * Time.deltaTime * mouseSpeed, Space.World);
        }
    }
}