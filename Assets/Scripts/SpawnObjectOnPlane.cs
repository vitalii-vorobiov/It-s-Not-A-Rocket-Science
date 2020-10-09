using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

[RequireComponent(typeof(ARRaycastManager))]
public class SpawnObjectOnPlane : MonoBehaviour {
    private                  ARRaycastManager raycastManager;
    private                  GameObject       spawnedPrefab;
    [SerializeField] private GameObject       prefab;
    
    static List<ARRaycastHit> s_hits = new List<ARRaycastHit>();

    private void Awake() {
        raycastManager = GetComponent<ARRaycastManager>();
    }

    bool TryGetTouchPosition(out Vector2 touchPosition) {
        if (Input.touchCount > 0) {
            touchPosition = Input.GetTouch(0).position;
            return true;
        }

        touchPosition = default;
        return false;
    }

    private void Update() {
        if (!TryGetTouchPosition(out Vector2 touchPosition)) return;

        bool isOverUi = touchPosition.IsPointOverUiObject();

        if (!isOverUi && raycastManager.Raycast(touchPosition, s_hits, TrackableType.PlaneWithinPolygon)) {
            var hitPose = s_hits[0].pose;
            if (spawnedPrefab is null) {
                spawnedPrefab = Instantiate(prefab, hitPose.position, hitPose.rotation);
            } else {
                var t = spawnedPrefab.transform;
                t.position = hitPose.position;
                t.rotation = hitPose.rotation;
            }            
        }
    }
}
