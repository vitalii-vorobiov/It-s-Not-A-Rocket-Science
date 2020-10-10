using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;

public class ChangeParentManager : MonoBehaviour
{
    [PunRPC]
    void SetBuldingCoreAsParent(){
		Debug.Log ("RPC Test");
		gameObject.transform.SetParent(GameObject.FindWithTag("BindingCore").transform);
	}
}
