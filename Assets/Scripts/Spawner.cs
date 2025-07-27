using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public List<ItemToSpawn> ItemTable = new List<ItemToSpawn>();
    public float Radius = 1;
    public float Width = 3; // теперь публичная переменная
    private float timer;
    public float period = 1;

    void Spawn()
    {
        //Vector3 RandomPosition = Random.insideUnitCircle * Radius;

        float capsuleBodyWidth = Width - 2 * Radius;

        float x = Random.Range(-capsuleBodyWidth / 2f, capsuleBodyWidth / 2f);
        float angle = Random.Range(0f, Mathf.PI * 2);
        float y = Mathf.Sin(angle) * Random.Range(0f, Radius);

        Vector3 randomLocalPos = new Vector3(x, y, 0);
        Vector3 RandomPosition = transform.position + randomLocalPos;

        foreach (ItemToSpawn item in ItemTable)
        {
            if (Random.Range(0f, 100f) <= item.DropChance)
            {
                Instantiate(item.ItemPrefab, RandomPosition, Quaternion.identity);
                break;
            }
        }
        //Instantiate(Item[Random.Range(0, Item.Length)], RandomPosition, Quaternion.identity);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;

        float radius = Radius;

        Vector3 left = transform.position - Vector3.right * (Width / 2 - radius);
        Vector3 right = transform.position + Vector3.right * (Width / 2 - radius);

        Gizmos.DrawWireCube(transform.position, new Vector3(Width - radius * 2, radius * 2, 0.01f));
        Gizmos.DrawWireSphere(left, radius);
        Gizmos.DrawWireSphere(right, radius);
    }

    void Update()
    {
        //timer += Time.deltaTime;
        //if (timer > period)
        //{
        //    timer = 0;
        //    Spawn();
        //}
    }

    private void OnMouseDown()
    {
        Spawn();
    }
}
