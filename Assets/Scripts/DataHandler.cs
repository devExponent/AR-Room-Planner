using System.Collections.Generic;
using UnityEngine;

public class DataHandler : MonoBehaviour
{
    [SerializeField] private GameObject furniture;
    [SerializeField] private ButtonManager buttonPrefab;
    [SerializeField] private GameObject buttonContainer;
    [SerializeField] private List<Item> items;

    public bool hasSelection = false;

    private int id = 0;

    private static DataHandler instance;

    public static DataHandler Instance
    {
        get
        {
            if (instance == null)
                instance = FindFirstObjectByType<DataHandler>();

            return instance;
        }
    }

    void Start()
    {
        items = new List<Item>();
        LoadItems();
        CreateButtons();
    }

    void LoadItems()
    {
        var items_obj = Resources.LoadAll("Items", typeof(Item));

        foreach (var item in items_obj)
        {
            items.Add(item as Item);
        }
    }

    void CreateButtons()
    {
        foreach (Item i in items)
        {
            ButtonManager b =
                Instantiate(buttonPrefab, buttonContainer.transform);

            b.ItemId = id;
            b.ButtonTexture = i.itemImage;

            id++;
        }
    }

    public void SetFurniture(int id)
    {
        furniture = items[id].itemPrefab;
        hasSelection = true;

        // 🔥 RESET SPAWN STATE HERE
        FindFirstObjectByType<PlaceOnPlane>().ResetSpawnState();

        Debug.Log("Selected: " + furniture.name);
    }

    public GameObject GetFurniture()
    {
        return furniture;
    }
}