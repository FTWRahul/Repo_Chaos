using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

public class MapFactory : MonoBehaviour
{
    public Func<bool> finishSpawn;
    public List<Transform> spawnPositions;
    public List<GameObject> sectionPrefab;
    public static List<Section> spawnedSections = new List<Section>();
    
    public void SpawnShelves()
    {
        for (int i = 0; i < spawnPositions.Count; i++)
        {
            int randRot = Random.Range(0, 3);
            Vector3 rotation = Vector3.zero;
            rotation.y = randRot * 90;
            int rand = Random.Range(0, sectionPrefab.Count);
            Section tempGo = Instantiate(sectionPrefab[rand], spawnPositions[i].position, Quaternion.Euler(rotation), transform).GetComponent<Section>();
            if (tempGo != null)
            {
                tempGo.Inti();
                spawnedSections.Add(tempGo);
            }
        }
        Debug.Log(spawnedSections.Count);
    }

    [ContextMenu("Check Items")]
    public void CheckItemsInScene()
    {
        for (int i = 0; i < ItemsDatabase.Instance.itemsOnScene.Count; i++)
        {
            Debug.Log(ItemsDatabase.Instance.itemsOnScene[i]);
        }
    }
}
