using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeOrigin : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        transform.SetParent(GameObject.FindWithTag("CustomOrigin").transform);
    }

}
