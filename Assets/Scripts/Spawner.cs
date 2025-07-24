using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public GameObject[] TestItem;
    public float Radius = 1;

    void Spawn() 
    {
        Vector3 RandomPosition = Random.insideUnitCircle * Radius;
        Instantiate(TestItem[Random.Range(0, TestItem.Length)], RandomPosition, Quaternion.identity);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(this.transform.position, Radius);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Spawn();
        }
    }
}
