using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestRayCast : MonoBehaviour {
    private DraggableObject draggableObject;
    void Update()
    {
        Ray        ray = Camera.main.ScreenPointToRay(gameObject.transform.position);
        RaycastHit hitObject;
        Debug.DrawRay(transform.position, ray.direction, Color.red);
        
        if (Physics.Raycast(ray, out hitObject, 100f)) {
            if (hitObject.transform.CompareTag("Draggable")) {
                Debug.Log("SELECT");
                draggableObject = hitObject.transform.gameObject.GetComponent<DraggableObject>();
                draggableObject.Select();
            }
        } else {
            if (draggableObject != null) {
                Debug.Log("DESELECT");
                draggableObject.Deselect();
                draggableObject = null;                
            }
        }
    }
}
