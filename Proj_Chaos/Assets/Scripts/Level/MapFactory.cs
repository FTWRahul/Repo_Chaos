using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapFactory : MonoBehaviour
{
    public List<Transform> spawnPositions;
    public List<GameObject> sectionPrefab;
    public static List<Section> spawnnedSections = new List<Section>();

    [ContextMenu("GenerateMap")]
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
                spawnnedSections.Add(tempGo);
            }
        }
    }
}
