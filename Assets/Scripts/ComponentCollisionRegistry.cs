using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ComponentCollisionRegistry : MonoBehaviour
{
    private HashSet<GameObject> collision_set = new HashSet<GameObject>();

    private void OnTriggerEnter(Collider other)
    {
        collision_set.Add(other.gameObject);
    }

    private void OnTriggerExit(Collider other)
    {
        collision_set.Remove(other.gameObject);
    }

    public bool Contains(GameObject other)
    {
        return collision_set.Contains(other) || gameObject == other;
    }
}
