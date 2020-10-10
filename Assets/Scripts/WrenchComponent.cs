using System.Collections;
using System.Collections.Generic;
using System.IO;
using Photon.Pun;
using UnityEngine;

public class WrenchComponent : MonoBehaviour
{
    public RaycastHit[] hitList;

    private ComponentCollisionRegistry _collisionRegistry;



    // Update is called once per frame
    void Update()
    {
        Debug.DrawRay(transform.position, transform.forward * 10, Color.white);
        hitList = Physics.RaycastAll(transform.position, transform.forward, 10);
        if (hitList.Length > 1)
        {
            // string object_list = "";
            // for (int i = 0; i < hitList.Length; i++)
            // {
            //     object_list += hitList[i].transform.name + " -> ";
            // }

            System.Array.Sort(hitList, (x, y) => x.distance.CompareTo(y.distance));
            if (hitList[0].transform.CompareTag("Draggable") && hitList[1].transform.CompareTag("Draggable"))
            {
                _collisionRegistry = hitList[0].transform.GetComponent<ComponentCollisionRegistry>();
                if (_collisionRegistry.Contains(hitList[0].transform.gameObject) &&
                    _collisionRegistry.Contains(hitList[1].transform.gameObject))
                {
                    if (Input.GetKeyDown(KeyCode.E))
                    {
                        bool bolt_instatiate = false;
                        if (hitList[0].transform.parent && hitList[0].transform.parent.CompareTag("BindingCore"))
                        {
                            
                            hitList[1].transform.GetComponent<PhotonView>().RPC("SetBuldingCoreAsParent", RpcTarget.AllBuffered);
                            bolt_instatiate = true;
                        }
                        else if (hitList[1].transform.parent && hitList[1].transform.parent.CompareTag("BindingCore"))
                        {
                            hitList[0].transform.GetComponent<PhotonView>().RPC("SetBuldingCoreAsParent", RpcTarget.AllBuffered);
                            bolt_instatiate = true;
                        }

                        if (bolt_instatiate)
                        {
                            PhotonNetwork.Instantiate(Path.Combine("PhotonPrefabs", "Bolt"), hitList[0].point,
                                Quaternion.LookRotation(hitList[0].normal) *
                                Quaternion.AngleAxis(-90, new Vector3(1, 0, 0))).GetComponent<PhotonView>().RPC("SetBuldingCoreAsParent", RpcTarget.AllBuffered);
                        }
                    }

                    Debug.DrawRay(transform.position, transform.forward * 10, Color.green);
                }
                else
                {
                    Debug.DrawRay(transform.position, transform.forward * 10, Color.red);
                }
            }
        }
    }
}