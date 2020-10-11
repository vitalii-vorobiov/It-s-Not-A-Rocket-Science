using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeOrigin : MonoBehaviour
{
    void Start()
    {
        transform.SetParent(GameObject.FindWithTag("CustomOrigin").transform);
    }

}
