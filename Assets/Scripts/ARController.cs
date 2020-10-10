using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

[RequireComponent(typeof(ARRaycastManager))]
[RequireComponent(typeof(ARPlaneManager))]
public class ARController : Singleton<ARController> {
    [SerializeField] private Camera             camera;
    [SerializeField] private GameObject         prefab;
    
    private        ARRaycastManager   raycastManager;
    private        ARPlaneManager     planeManager;
    private        GameObject         spawnedPrefab;
    private        Coroutine          sceneSetupCoroutine;
    private        Coroutine          mainGameCoroutine;
    private static List<ARRaycastHit> s_hits = new List<ARRaycastHit>();
    private        DraggableObject    draggableObject;

    public Text  debugText;

    public bool isWeld;
    public bool isBuild;
    public bool isDrill;

    private void Awake() {
        raycastManager = GetComponent<ARRaycastManager>();
        planeManager   = GetComponent<ARPlaneManager>();
    }

    // Scene Setup Code
    
    private void Start() {
        sceneSetupCoroutine = StartCoroutine(PlaceObject());
    }

    private void SetAllPlanesActive(bool value) {
        foreach (var plane in planeManager.trackables) {
            plane.gameObject.SetActive(value);
        }
    }
    
    private bool TryGetTouchPosition(out Vector2 touchPosition) {
        if (Input.touchCount > 0) {
            touchPosition = Input.GetTouch(0).position;
            return true;
        }

        touchPosition = default;
        return false;
    }
    
    public void EnableSceneSetup() {
        if (sceneSetupCoroutine is null) {
            planeManager.enabled = true;
            SetAllPlanesActive(true);
            Destroy(spawnedPrefab);
            spawnedPrefab        = null;
            sceneSetupCoroutine = StartCoroutine(PlaceObject());
        }
    }

    public void DisableSceneSetup() {
        if (sceneSetupCoroutine != null) {
            planeManager.enabled = false;
            SetAllPlanesActive(false);
            StopCoroutine(sceneSetupCoroutine);
        }

        sceneSetupCoroutine = null;
    }
    
    private IEnumerator PlaceObject() {
        while (true) {
            if (TryGetTouchPosition(out Vector2 touchPosition)) {
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
            yield return null;
        }
    }

    // Game Code 
    public void StartGame() {
        if (mainGameCoroutine == null) {
            // mainGameCoroutine = StartCoroutine(DragAndDropObjects());            
        }
    }

    public void FinishGame() {
        if (mainGameCoroutine != null) {
            StopCoroutine(mainGameCoroutine);
            mainGameCoroutine = null;
        }
    }
}
