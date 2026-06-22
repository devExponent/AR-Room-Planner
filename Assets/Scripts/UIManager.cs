using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    private GraphicRaycaster raycaster;
    private PointerEventData pointerEventData;
    private EventSystem eventSystem;

    public RectTransform selectionPoint;

    private static UIManager instance;

    public static UIManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindFirstObjectByType<UIManager>();
            }

            return instance;
        }
    }

    void Start()
    {
        raycaster = FindFirstObjectByType<GraphicRaycaster>();

        eventSystem = FindFirstObjectByType<EventSystem>();

        pointerEventData = new PointerEventData(eventSystem);
    }

    public bool OnEntered(GameObject button)
    {
        pointerEventData.position = selectionPoint.position;

        List<RaycastResult> results = new List<RaycastResult>();

        raycaster.Raycast(pointerEventData, results);

        foreach (RaycastResult result in results)
        {
            if (result.gameObject == button)
            {
                return true;
            }
        }

        return false;
    }
}