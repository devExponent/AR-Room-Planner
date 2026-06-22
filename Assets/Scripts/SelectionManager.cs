using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class SelectionManager : MonoBehaviour
{
    public static SelectionManager Instance;

    [Header("AR References")]
    public Camera arCam;

    public GameObject selectedObject;

    private bool wasTouching = false;

    void Awake()
    {
        Instance = this;
    }

    void Update()
    {
        var touchscreen = Touchscreen.current;
        if (touchscreen == null) return;

        var touch = touchscreen.primaryTouch;

        bool isTouching = touch.press.isPressed;

        // Detect the exact frame the finger first touches
        if (isTouching && !wasTouching)
        {
            Vector2 screenPos = touch.position.ReadValue();
            HandleTap(screenPos);
        }

        wasTouching = isTouching;
    }

    void HandleTap(Vector2 screenPos)
    {
        if (IsTappingUI(screenPos)) return;

        Ray ray = arCam.ScreenPointToRay(screenPos);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit))
        {
            GameObject tapped = hit.transform.gameObject;

            if (tapped == selectedObject)
                Deselect();
            else
                Select(tapped);
        }
        else
        {
            Deselect();
        }
    }

    public void Select(GameObject obj)
    {
        if (selectedObject != null)
            Deselect();

        selectedObject = obj;
        selectedObject.transform.localScale *= 1.05f;

        Debug.Log("Selected: " + obj.name);
    }

    public void Deselect()
    {
        if (selectedObject == null) return;

        selectedObject.transform.localScale /= 1.05f;
        selectedObject = null;

        Debug.Log("Deselected");
    }

    bool IsTappingUI(Vector2 screenPos)
    {
        PointerEventData eventData = new PointerEventData(EventSystem.current);
        eventData.position = screenPos;
        List<RaycastResult> results = new List<RaycastResult>();
        EventSystem.current.RaycastAll(eventData, results);
        return results.Count > 0;
    }
}