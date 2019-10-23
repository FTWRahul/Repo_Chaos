using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;


public class NPCSpawner : MonoBehaviour
{
    public GameObject npcPrefab;
    public int spawnAmount;
    public float spawnInterval;
    public int maxNPCS;
    public float radius;

    public List<GameObject> spawnedNPCS = new List<GameObject>();

    private void Start()
    {
        StartCoroutine(SpawnNpcs());
    }

    private IEnumerator SpawnNpcs()
    {
        while (true)
        {
            if (spawnedNPCS.Count < maxNPCS)
            {
                for (int i = 0; i < spawnAmount; i++)
                {
                    Vector2 randomPos = Random.insideUnitCircle * radius;
                    Vector3 spawnPos = transform.position + new Vector3(randomPos.x, 0, randomPos.y);
                    spawnedNPCS.Add(Instantiate(npcPrefab, spawnPos, Quaternion.identity));
                    yield return new WaitForEndOfFrame();
                }
            }
            yield return new WaitForSeconds(spawnInterval);
        }
    }
}
