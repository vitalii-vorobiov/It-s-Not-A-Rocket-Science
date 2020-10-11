using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Photon.Pun;
using UnityEngine;

public class TerminalController : MonoBehaviour
{
    public bool KEY1;
    public bool KEY2;
    public bool MAIN;
    
    private bool key1;
    private bool key2;
    public Animation anim;


    public GameObject prefabAnimation;
    
    private Animator _animator;
    void Start()
    {
        _animator = GetComponent<Animator>();
    }

    public void FixedUpdate()
    {
        if (KEY1)
        {
            KEY1 = false;
            Key1();
        }
        
        if (KEY2)
        {
            KEY2 = false;
            Key2();
        }
        
        if (MAIN)
        {
            KEY2 = false;
            MainButton();
        }
    }

    [PunRPC]
    public void Key1()
    {
        Debug.Log("Key");
        _animator.SetTrigger("Key1Pressed");
    }
    
    [PunRPC]
    public void Key2()
    {
        Debug.Log("Key2");

        _animator.SetTrigger("Key2Pressed");
    }

    [PunRPC]
    public void MainButton()
    {
        Debug.Log("Main");
        _animator.SetTrigger("PushButton");
        Debug.Log("AAAAAAAAAAAAAAAA");
        GameObject objPos = GameObject.FindGameObjectWithTag("CapsulePod");
        GameObject core   = GameObject.FindGameObjectWithTag("BindingCore");
        Instantiate(prefabAnimation, objPos.transform.position, Quaternion.identity);
        objPos.GetComponent<Animation>().enabled = true;
        core.GetComponent<Animation>().enabled = true;
    }
}
