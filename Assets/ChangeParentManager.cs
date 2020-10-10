using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;

public class ChangeParentManager : MonoBehaviour
{
    [PunRPC]
    void SetBuldingCoreAsParent(){
		transform.SetParent(GameObject.FindWithTag("BindingCore").transform);
	}
}
