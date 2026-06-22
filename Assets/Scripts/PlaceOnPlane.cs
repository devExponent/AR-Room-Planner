using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;
using UnityEngine.InputSystem;
using UnityEngine.EventSystems;

public class PlaceOnPlane : MonoBehaviour
{
    [Header("AR References")]
    public ARRaycastManager raycastManager;
    public Camera arCam;

    [Header("Crosshair")]
    public GameObject crosshair;

    private List<ARRaycastHit> hits = new List<ARRaycastHit>();

    private Pose crosshairPose;
    private bool hasPlacementPose;

    private bool hasSpawnedForSelection = false;

    void Update()
    {
        UpdateCrosshair();
    }

    void UpdateCrosshair()
    {
        Vector3 screenCenter = new Vector3(Screen.width / 2f, Screen.height / 2f);
        Ray ray = arCam.ScreenPointToRay(screenCenter);

        if (raycastManager.Raycast(ray, hits, TrackableType.PlaneWithinPolygon))
        {
            hasPlacementPose = true;

            crosshairPose = hits[0].pose;

            crosshair.SetActive(true);
            crosshair.transform.position = crosshairPose.position;
            crosshair.transform.rotation = Quaternion.Euler(90f, 0f, 0f);
        }
        else
        {
            hasPlacementPose = false;
            crosshair.SetActive(false);
        }
    }

    public void AddObject(InputAction.CallbackContext context)
    {
        if (!context.performed)
            return;

        // 🔥 NEW: block placement if an object is already selected
        if (SelectionManager.Instance.selectedObject != null)
            return;

        Vector2 touchPos = context.ReadValue<Vector2>();

        // UI check
        PointerEventData eventData = new PointerEventData(EventSystem.current);
        eventData.position = touchPos;

        List<RaycastResult> results = new List<RaycastResult>();
        EventSystem.current.RaycastAll(eventData, results);

        if (results.Count > 0)
            return;

        // ❌ already spawned once → block
        if (hasSpawnedForSelection)
            return;

        if (!DataHandler.Instance.hasSelection ||
            DataHandler.Instance.GetFurniture() == null)
            return;

        if (!hasPlacementPose)
            return;

        Instantiate(
            DataHandler.Instance.GetFurniture(),
            crosshairPose.position,
            crosshairPose.rotation
        );

        hasSpawnedForSelection = true;

        Debug.Log("Spawned once");
    }

    public void ResetSpawnState()
    {
        hasSpawnedForSelection = false;
    }
}