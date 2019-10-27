using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class NPCSpawner : MonoBehaviour
{
    public static int spawnedAmount;
    
    [SerializeField] private GameObject npcPrefab;
    [SerializeField] private Transform[] spawnPositions;
    [SerializeField] private int spawnAmount;
    [SerializeField] private float spawnInterval;
    [SerializeField] private int maxNpc;
    [SerializeField] private float radius;

    public void Init()
    {
        StartCoroutine(SpawnNpcs());
    }

    private IEnumerator SpawnNpcs()
    {
        while (true)
        {
            if (spawnedAmount < maxNpc)
            {
                Vector2 randomPos = Random.insideUnitCircle * radius;
                Vector3 spawnPos = spawnPositions[Random.Range(0, spawnPositions.Length)].position +
                                   new Vector3(randomPos.x, 0, randomPos.y);
                
                GameObject go = Instantiate(npcPrefab, spawnPos, Quaternion.identity);
                spawnedAmount++;
                go.name = "Npc" + spawnedAmount;
                /*for (int i = 0; i < spawnAmount; i++)
                {
                    Vector2 randomPos = Random.insideUnitCircle * radius;
                    Vector3 spawnPos = transform.position + new Vector3(randomPos.x, 0, randomPos.y);
                    Instantiate(npcPrefab, spawnPos, Quaternion.identity);
                    spawnedAmount++;
                    
                    yield return new WaitForEndOfFrame();
                }*/
            }
            yield return new WaitForEndOfFrame();
        }
    }
}
