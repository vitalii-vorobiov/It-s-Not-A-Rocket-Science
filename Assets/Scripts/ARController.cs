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

    // private IEnumerator DragAndDropObjects() {
    //     Vector2 centerPosition = new Vector2(Screen.width / 2, Screen.height / 2);
    //     
    //     while (true) {
    //         Ray        ray = Camera.main.ScreenPointToRay(centerPosition);
    //         RaycastHit hitObject;
    //
    //         if (Physics.Raycast(ray, out hitObject, Mathf.Infinity)) {
    //             
    //             if (hitObject.transform.CompareTag("Draggable")) {
    //                 draggableObject = hitObject.transform.gameObject.GetComponent<DraggableObject>();
    //                 draggableObject.Select();
    //
    //                 if (TryGetTouchPosition(out Vector2 touchPosition)) {
    //                     bool isOverUi = touchPosition.IsPointOverUiObject();
    //                     if (!isOverUi) {
    //                         Vector3 resultingPosition = camera.transform.position + camera.transform.forward * 1f;
    //                         // hitObject.transform.position = Vector3.Lerp(hitObject.transform.position, resultingPosition, 1f * Time.deltaTime);
    //                         hitObject.transform.position = resultingPosition;
    //                     }                
    //                 }
    //             }
    //         } else {
    //             if (draggableObject != null) {
    //                 draggableObject.Deselect();
    //                 draggableObject = null;
    //             }
    //         }
    //         
    //         yield return null;
    //     }
    // }
    
    // private IEnumerator DragAndDropObjects() {
        // Vector2 centerPosition = new Vector2(Screen.width / 2, Screen.height / 2);
        //
        // while (true) {
        //     Ray        ray = Camera.main.ScreenPointToRay(centerPosition);
        //     RaycastHit hitObject;
        //
        //     if (Physics.Raycast(ray, out hitObject, Mathf.Infinity)) {
        //         
        //         if (TryGetTouchPosition(out Vector2 touchPosition)) {
        //             bool isOverUi = touchPosition.IsPointOverUiObject();
        //             if (!isOverUi) {
        //                 Vector3 resultingPosition = camera.transform.position + camera.transform.forward * 1f;
        //                 // hitObject.transform.position = Vector3.Lerp(hitObject.transform.position, resultingPosition, 1f * Time.deltaTime);
        //                 if (hitObject.transform.CompareTag("Draggable")) {
        //                     draggableObject = hitObject.transform.gameObject.GetComponent<DraggableObject>();
        //                     draggableObject.Select();
        //                     hitObject.transform.position = resultingPosition;
        //                 }
        //             }                
        //         }
        //     } else {
        //         if (draggableObject != null) {
        //             draggableObject.Deselect();
        //             draggableObject = null;
        //         }
        //     }
        //     
        //     yield return null;
        // }
    // }
}
