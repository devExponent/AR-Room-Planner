using UnityEngine;
using UnityEngine.UI;

public class DeleteObject : MonoBehaviour
{
    [Header("UI Reference")]
    public GameObject deleteButton;

    void Update()
    {
        if (SelectionManager.Instance.selectedObject != null)
        {
            deleteButton.SetActive(true);
            Debug.Log("Something is selected, showing button");
        }
        else
        {
            deleteButton.SetActive(false);
            Debug.Log("Nothing selected, hiding button");
        }
    }

    public void OnDeletePressed()
    {
        if (SelectionManager.Instance.selectedObject == null) return;

        Destroy(SelectionManager.Instance.selectedObject);
        SelectionManager.Instance.selectedObject = null;

        Debug.Log("Object deleted");
    }
}