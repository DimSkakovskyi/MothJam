using UnityEngine;

public class MothSpawner : MonoBehaviour
{
    public GameObject mothCommonPrefab;
    public GameObject mothGreenPrefab;
    public GameObject mothBluePrefab;
    public Transform[] spawnPoints;

    private InventoryManager inventory;

    private void Start()
    {
        inventory = FindObjectOfType<InventoryManager>();
    }

    private void OnMouseDown()
    {
        if (inventory == null || spawnPoints.Length == 0)
            return;

        for (int i = 0; i < inventory.itemSlot.Length; i++)
        {
            var slot = inventory.itemSlot[i];
            if (slot.quantity > 0)
            {
                GameObject prefabToSpawn = null;

                // ѕерев≥рка типу €йц€
                if (slot.itemName == "Egg Common")
                    prefabToSpawn = mothCommonPrefab;
                else if (slot.itemName == "Egg Green")
                    prefabToSpawn = mothGreenPrefab;
                else if (slot.itemName == "Egg Blue")
                    prefabToSpawn = mothBluePrefab;

                // якщо знайшли в≥дпов≥дний префаб Ч спавнимо
                if (prefabToSpawn != null)
                {
                    // ¬ипадкова точка спавну
                    int index = Random.Range(0, spawnPoints.Length);
                    Instantiate(prefabToSpawn, spawnPoints[index].position, Quaternion.identity);

                    // «меншити к≥льк≥сть у слот≥
                    slot.quantity--;

                    // якщо слот порожн≥й Ч очистити його (опц≥йно)
                    if (slot.quantity == 0)
                        inventory.DeleteItem(i); // або вручну: slot.itemName = "", тощо

                    return; // «робили спавн Ч виходимо
                }
            }
        }

        Debug.Log("No suitable egg found in inventory.");
    }

}
