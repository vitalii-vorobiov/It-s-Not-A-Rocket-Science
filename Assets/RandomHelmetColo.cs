using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomHelmetColo : MonoBehaviour
{
    void Start()
    {
        Color[] list = new[] { Color.blue, Color.red, Color.yellow, Color.white, Color.green };
        gameObject.GetComponent<Renderer>().material.color = list[Random.Range(0,4)];
    }

}
