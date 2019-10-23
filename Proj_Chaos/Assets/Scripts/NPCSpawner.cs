using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class NPCSpawner : MonoBehaviour
{
    public GameObject npcPrefab;
    public float delay;
    public List<Transform> spawnPosition;

    [SerializeField]
    private int maxNPCAmount;

    public List<GameObject> currentNPCList;


    private void Start()
    {
        SpawnNPCS(20);
        Invoke("SpawnNPCS", 2f);
        StartCoroutine(ConstantSpawn());
    }

    public IEnumerator ConstantSpawn()
    {
        while (true)
        {
            delay = Random.Range(0, 10);
            int rand = Random.Range(0, spawnPosition.Count);
            yield return new WaitForSeconds(delay);
            if (currentNPCList.Count < maxNPCAmount)
            {
                GameObject tempGo = Instantiate(npcPrefab, spawnPosition[rand].position, Quaternion.identity);
                currentNPCList.Add(tempGo);
            }
        }
    }

    public void SpawnNPCS(int amount)
    {
        if (amount == null)
        {
            amount = 20;
        }
        for (int i = 0; i < amount; i++)
        {
            int rand = Random.Range(0, spawnPosition.Count);
            GameObject tempGo = Instantiate(npcPrefab, spawnPosition[rand].position, Quaternion.identity);
            currentNPCList.Add(tempGo);
        }
    }
}
