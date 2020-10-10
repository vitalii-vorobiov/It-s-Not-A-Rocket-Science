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

        if (Input.GetKey(KeyCode.RightArrow))
        {
            transform.Rotate(Vector3.up * Time.deltaTime * mouseSpeed, Space.World);
        }

        if (Input.GetKey(KeyCode.LeftArrow))
        {
            transform.Rotate(-Vector3.up * Time.deltaTime * mouseSpeed, Space.World);
        }

        if (Input.GetKey(KeyCode.UpArrow))
        {
            transform.Rotate(transform.right * Time.deltaTime * mouseSpeed, Space.World);
        }

        if (Input.GetKey(KeyCode.DownArrow))
        {
            //Rotate the sprite about the Y axis in the negative direction
            transform.Rotate(-transform.right * Time.deltaTime * mouseSpeed, Space.World);
        }
    }
}