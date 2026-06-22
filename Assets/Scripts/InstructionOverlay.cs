using UnityEngine;
using UnityEngine.UI;

public class InstructionOverlay : MonoBehaviour
{
    [Header("UI References")]
    public GameObject overlayPanel;
    public Button startButton;

    [Header("AR Reference")]
    public GameObject arSession;

    void Start()
    {
        // Disable AR until user presses Start
        arSession.SetActive(false);

        // Show overlay
        overlayPanel.SetActive(true);

        // Listen for button
        startButton.onClick.AddListener(OnStartPressed);
    }

    void OnStartPressed()
    {
        // Hide overlay
        overlayPanel.SetActive(false);

        // Start the AR session
        arSession.SetActive(true);
    }
}