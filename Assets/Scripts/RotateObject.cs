using UnityEngine;
using UnityEngine.InputSystem;

public class RotateObject : MonoBehaviour
{
    [Header("Rotation Settings")]
    public float rotationSpeed = 1f;

    private float previousAngle;
    private bool wasRotating = false;

    void Update()
    {
        // Only run if something is selected
        if (SelectionManager.Instance.selectedObject == null) return;

        var touchscreen = Touchscreen.current;
        if (touchscreen == null) return;

        // We need exactly two fingers on screen
        var touch0 = touchscreen.touches[0];
        var touch1 = touchscreen.touches[1];

        bool twoFingers = touch0.press.isPressed && touch1.press.isPressed;

        if (twoFingers)
        {
            Vector2 pos0 = touch0.position.ReadValue();
            Vector2 pos1 = touch1.position.ReadValue();

            // Calculate the angle between the two fingers
            float currentAngle = GetAngle(pos0, pos1);

            if (wasRotating)
            {
                // How much did the angle change since last frame?
                float angleDelta = Mathf.DeltaAngle(previousAngle, currentAngle);

                // Apply rotation to the selected object around the Y axis
                SelectionManager.Instance.selectedObject.transform.Rotate(
                    0f, -angleDelta * rotationSpeed, 0f, Space.World
                );
            }

            previousAngle = currentAngle;
            wasRotating = true;
        }
        else
        {
            wasRotating = false;
        }
    }

    // Returns the angle in degrees between two screen positions
    float GetAngle(Vector2 a, Vector2 b)
    {
        Vector2 direction = b - a;
        return Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
    }
}