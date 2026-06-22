using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class DragObject : MonoBehaviour
{
    [Header("AR References")]
    public ARRaycastManager raycastManager;

    private List<ARRaycastHit> hits = new List<ARRaycastHit>();

    private bool wasTouching = false;

    void Update()
    {
        if (SelectionManager.Instance.selectedObject == null) return;

        var touchscreen = Touchscreen.current;
        if (touchscreen == null) return;

        var touch = touchscreen.primaryTouch;
        bool isTouching = touch.press.isPressed;

        // Only drag after the first frame (to avoid snapping on tap)
        if (isTouching && wasTouching)
        {
            Vector2 screenPos = touch.position.ReadValue();

            if (IsTouchingUI(screenPos)) return;

            MoveSelectedObject(screenPos);
        }

        wasTouching = isTouching;
    }

    void MoveSelectedObject(Vector2 screenPos)
    {
        if (raycastManager.Raycast(screenPos, hits, TrackableType.PlaneWithinPolygon))
        {
            Pose hitPose = hits[0].pose;
            SelectionManager.Instance.selectedObject.transform.position = hitPose.position;
        }
    }

    bool IsTouchingUI(Vector2 screenPos)
    {
        PointerEventData eventData = new PointerEventData(EventSystem.current);
        eventData.position = screenPos;
        List<RaycastResult> results = new List<RaycastResult>();
        EventSystem.current.RaycastAll(eventData, results);
        return results.Count > 0;
    }
}