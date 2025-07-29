using UnityEngine;
using System.Collections.Generic;

public class MothSpawner : MonoBehaviour
{
    public GameObject mothCommonPrefab;
    public GameObject mothGreenPrefab;
    public GameObject mothBluePrefab;
    public Transform[] spawnPoints;

    private InventoryManager inventory;
    private HashSet<Transform> occupiedPoints = new HashSet<Transform>(); // ¬≥дстеженн€ зайн€тих точок

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
                    Transform freePoint = GetFreeSpawnPoint();

                    if (freePoint != null)
                    {
                        // —творюЇмо моль
                        GameObject mothObj = Instantiate(prefabToSpawn, freePoint.position, Quaternion.identity);

                        // ѕрив'€зка точки до мол≥
                        Moth mothScript = mothObj.GetComponent<Moth>();
                        mothScript.SetSpawnPoint(freePoint, this);

                        occupiedPoints.Add(freePoint); // ѕозначаЇмо точку €к зайн€ту

                        // «меншити к≥льк≥сть у слот≥
                        slot.quantity--;
                        if (slot.quantity == 0)
                            inventory.DeleteItem(i);

                        return; // «робили спавн Ч виходимо
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
        // ¬ибираЇмо випадкову в≥льну точку
        List<Transform> freePoints = new List<Transform>();
        foreach (var point in spawnPoints)
        {
            if (!occupiedPoints.Contains(point))
                freePoints.Add(point);
        }

        if (freePoints.Count > 0)
            return freePoints[Random.Range(0, freePoints.Count)];

        return null; // якщо в≥льних точок немаЇ
    }

    // ћетод дл€ зв≥льненн€ точки (викликаЇтьс€ з Moth)
    public void FreeSpawnPoint(Transform point)
    {
        if (occupiedPoints.Contains(point))
            occupiedPoints.Remove(point);
    }

#if UNITY_EDITOR
    // ¬≥зуал≥зац≥€ точок у Scene View (зелен≥ Ч в≥льн≥, червон≥ Ч зайн€т≥)
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
