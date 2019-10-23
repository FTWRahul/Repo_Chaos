using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Vector3 = System.Numerics.Vector3;

public class ItemsDatabase : Singleton<ItemsDatabase>
{
    public Dictionary<int, Item> database = new Dictionary<int, Item>();
    public Dictionary<int, int> objectsInScene = new Dictionary<int, int>();

    public GameObject playerPrefab;
    public Transform spawnLocation;
    public GameObject NPCSpawnner;
    public GameObject gate1, gate2;

    protected override void Awake()
    {
        base.Awake();
        Init();
    }
    
    private void Init()
    { 
        Object[] items = Resources.LoadAll("Items", typeof(ItemSO));
        
        // ReSharper disable once PossibleInvalidCastExceptionInForeachLoop
        foreach (ItemSO item in items)
        {
            Item newItem = new Item(item);
            database.Add(newItem.itemId, newItem);
        }

        Instantiate(playerPrefab, spawnLocation.position, Quaternion.identity);
        NPCSpawnner.SetActive(true);
        StartCoroutine(LiftGates());
    }

    public IEnumerator LiftGates()
    {
        while (gate1.transform.position.y < 15)
        {
            float t = 0;
            t += Time.deltaTime;
            gate1.transform.position += UnityEngine.Vector3.up * Time.deltaTime;
            gate2.transform.position += UnityEngine.Vector3.up * Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }
    }
}
