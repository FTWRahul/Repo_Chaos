using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class ItemSpawner : MonoBehaviour
{
    public GameObject itemPrefab;

    private void Start()
    {
        SpawnItems();
    }

    [ContextMenu("Spawn")]
    void SpawnItems()
    {
        foreach (KeyValuePair<int, Item> item in ItemsDatabase.Instance.database)
        {
            int loop = Random.Range(1, 5);

            for (int j = 0; j < loop; j++)
            {
                Vector3 pos = new Vector3(Random.Range(-25,25), 0, Random.Range(-25,25));
                SpawnedItem go = Instantiate(itemPrefab, pos, Quaternion.identity).GetComponent<SpawnedItem>();
                go.Init(item.Value.itemId);
            }
        }
    }
}
