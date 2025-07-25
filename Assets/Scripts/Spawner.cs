using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public List<Item> ItemTable = new List<Item>(); 
    public float Radius = 1;
    private float timer;
    public float period = 1;

    void Spawn() 
    {
        Vector3 RandomPosition = Random.insideUnitCircle * Radius;
        foreach (Item item in ItemTable) 
        {
            if(Random.Range(0f, 100f) <= item.DropChance) 
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
        Gizmos.DrawWireSphere(this.transform.position, Radius);
    }

    void Update()
    {
        timer += Time.deltaTime;
        if (timer > period)
        {
            timer = 0;
            Spawn();
        }
    }
}
