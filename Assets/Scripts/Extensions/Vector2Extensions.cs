using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public static class Vector2Extensions {
    public static bool IsPointOverUiObject(this Vector2 pos) {
        if (EventSystem.current.IsPointerOverGameObject()) {
            return false;
        }
        
        PointerEventData eventPosition = new PointerEventData(EventSystem.current);
        eventPosition.position = new Vector2(pos.x, pos.y);
        
        List<RaycastResult> results = new List<RaycastResult>();
        EventSystem.current.RaycastAll(eventPosition, results);

        foreach (var result in results) {
            if (result.gameObject.transform.CompareTag("Transparent")) {
                continue;
            } else {
                return true;
            }
        }

        return false;
    }
}
