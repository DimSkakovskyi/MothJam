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

                // �������� ���� ����
                if (slot.itemName == "Egg Common")
                    prefabToSpawn = mothCommonPrefab;
                else if (slot.itemName == "Egg Green")
                    prefabToSpawn = mothGreenPrefab;
                else if (slot.itemName == "Egg Blue")
                    prefabToSpawn = mothBluePrefab;

                // ���� ������� ��������� ������ � ��������
                if (prefabToSpawn != null)
                {
                    // ��������� ����� ������
                    int index = Random.Range(0, spawnPoints.Length);
                    Instantiate(prefabToSpawn, spawnPoints[index].position, Quaternion.identity);

                    // �������� ������� � ����
                    slot.quantity--;

                    // ���� ���� ������� � �������� ���� (�������)
                    if (slot.quantity == 0)
                        inventory.DeleteItem(i); // ��� ������: slot.itemName = "", ����

                    return; // ������� ����� � ��������
                }
            }
        }

        Debug.Log("No suitable egg found in inventory.");
    }

}
