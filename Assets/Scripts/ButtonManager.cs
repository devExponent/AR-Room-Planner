using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class ButtonManager : MonoBehaviour
{
    [SerializeField] private RawImage buttonImage;

    private Button btn;

    private int itemId;

    public int ItemId
    {
        set => itemId = value;
    }

    public Sprite ButtonTexture
    {
        set
        {
            buttonImage.texture = value.texture;
        }
    }

    void Start()
    {
        btn = GetComponent<Button>();
        btn.onClick.AddListener(SelectObject);
    }

    void Update()
    {
        if (UIManager.Instance.OnEntered(gameObject))
        {
            transform.DOScale(Vector3.one * 1.2f, 0.2f);
        }
        else
        {
            transform.DOScale(Vector3.one, 0.2f);
        }
    }

    void SelectObject()
    {
        DataHandler.Instance.SetFurniture(itemId);
    }
}