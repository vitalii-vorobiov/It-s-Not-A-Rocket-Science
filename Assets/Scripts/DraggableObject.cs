using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Renderer))]
public class DraggableObject : MonoBehaviour {
    private Color    color;
    private Renderer renderer;
    private bool     isSelected;
    
    private void Awake() {
        renderer = gameObject.GetComponent<Renderer>();
    }

    public void Select() {
        if (!isSelected) {
            color      = renderer.material.color;
            isSelected = true;
        }
        renderer.material.SetColor("_Color", Color.white);
    }

    public void Deselect() {
        isSelected = false;
        renderer.material.SetColor("_Color", color);
    }
}
