using UnityEngine;
using System.Collections.Generic;

public class MothSpawner : MonoBehaviour
{
    public GameObject mothCommonPrefab;
    public GameObject mothGreenPrefab;
    public GameObject mothBluePrefab;
    public Transform[] spawnPoints;

    private InventoryManager inventory;
    private HashSet<Transform> occupiedPoints = new HashSet<Transform>(); // ³��������� �������� �����

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
                    Transform freePoint = GetFreeSpawnPoint();

                    if (freePoint != null)
                    {
                        // ��������� ����
                        GameObject mothObj = Instantiate(prefabToSpawn, freePoint.position, Quaternion.identity);

                        // ����'���� ����� �� ���
                        Moth mothScript = mothObj.GetComponent<Moth>();
                        mothScript.SetSpawnPoint(freePoint, this);

                        occupiedPoints.Add(freePoint); // ��������� ����� �� �������

                        // �������� ������� � ����
                        slot.quantity--;
                        if (slot.quantity == 0)
                            inventory.DeleteItem(i);

                        return; // ������� ����� � ��������
                    }
                    else
                    {
                        Debug.Log("No free spawn points available.");
                        return;
                    }
                }
            }
        }

        Debug.Log("No suitable egg found in inventory.");
    }

    private Transform GetFreeSpawnPoint()
    {
        // �������� ��������� ����� �����
        List<Transform> freePoints = new List<Transform>();
        foreach (var point in spawnPoints)
        {
            if (!occupiedPoints.Contains(point))
                freePoints.Add(point);
        }

        if (freePoints.Count > 0)
            return freePoints[Random.Range(0, freePoints.Count)];

        return null; // ���� ������ ����� ����
    }

    // ����� ��� ��������� ����� (����������� � Moth)
    public void FreeSpawnPoint(Transform point)
    {
        if (occupiedPoints.Contains(point))
            occupiedPoints.Remove(point);
    }

#if UNITY_EDITOR
    // ³��������� ����� � Scene View (����� � ����, ������ � ������)
    private void OnDrawGizmos()
    {
        if (spawnPoints == null) return;

        foreach (var point in spawnPoints)
        {
            Gizmos.color = occupiedPoints != null && occupiedPoints.Contains(point) ? Color.red : Color.green;
            Gizmos.DrawSphere(point.position, 0.2f);
        }
    }
#endif
}
