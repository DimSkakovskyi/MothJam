using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemsToEat : MonoBehaviour
{
    [SerializeField] private int calories;
    [SerializeField] private string typeOfFood;
    // Start is called before the first frame update

    [SerializeField]
    private string itemName;

    [SerializeField]
    private int quantity;

    [SerializeField]
    private Sprite sprite;

    [TextArea]
    [SerializeField]
    private string itemDescription;

    private InventoryManager inventoryManager;

    // Start is called before the first frame update
    void Start()
    {
        inventoryManager = GameObject.Find("InventoryCanvas").GetComponent<InventoryManager>();
    }

    private void OnMouseDown()
    {
        int leftOverItems = inventoryManager.AddItem(itemName, quantity, sprite, itemDescription);

        if (leftOverItems <= 0)
        {
            Destroy(gameObject);
        }
        else
        {
            quantity = leftOverItems;
        }
    }
}
