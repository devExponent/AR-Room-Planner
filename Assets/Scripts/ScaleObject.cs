using UnityEngine;
using UnityEngine.InputSystem;

public class ScaleObject : MonoBehaviour
{
    [Header("Scale Settings")]
    public float scaleSpeed = 0.01f;
    public float minScale = 0.1f;
    public float maxScale = 3f;

    private float previousDistance;
    private bool wasScaling = false;

    void Update()
    {
        // Only run if something is selected
        if (SelectionManager.Instance.selectedObject == null) return;

        var touchscreen = Touchscreen.current;
        if (touchscreen == null) return;

        var touch0 = touchscreen.touches[0];
        var touch1 = touchscreen.touches[1];

        bool twoFingers = touch0.press.isPressed && touch1.press.isPressed;

        if (twoFingers)
        {
            Vector2 pos0 = touch0.position.ReadValue();
            Vector2 pos1 = touch1.position.ReadValue();

            // Get the current distance between the two fingers
            float currentDistance = Vector2.Distance(pos0, pos1);

            if (wasScaling)
            {
                // How much did the distance change since last frame?
                float difference = currentDistance - previousDistance;

                // Get the current scale
                Vector3 currentScale = SelectionManager.Instance
                    .selectedObject.transform.localScale;

                // Apply new scale
                Vector3 newScale = currentScale + Vector3.one * difference * scaleSpeed;

                // Clamp so it doesn't get too big or too small
                newScale.x = Mathf.Clamp(newScale.x, minScale, maxScale);
                newScale.y = Mathf.Clamp(newScale.y, minScale, maxScale);
                newScale.z = Mathf.Clamp(newScale.z, minScale, maxScale);

                SelectionManager.Instance.selectedObject.transform.localScale = newScale;
            }

            previousDistance = currentDistance;
            wasScaling = true;
        }
        else
        {
            wasScaling = false;
        }
    }
}