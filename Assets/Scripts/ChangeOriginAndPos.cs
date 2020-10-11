using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.WSA;

public class ChangeOriginAndPos: MonoBehaviour {
    public Vector3 position;
    void Start()
    {
        transform.SetParent(GameObject.FindWithTag("CustomOrigin").transform);
        transform.localPosition = position;
    }

}