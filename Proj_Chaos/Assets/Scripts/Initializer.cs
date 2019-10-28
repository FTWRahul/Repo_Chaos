using System.Collections;
using Cinemachine;
using DG.Tweening;
using UnityEngine;

public class Initializer : MonoBehaviour
{
    [SerializeField] private NPCSpawner npcSpawner;
    [SerializeField] public GameObject playerPrefab;
    [SerializeField] private Transform spawnLocation;
    [SerializeField] private MapFactory mapFactory;

    private void Start()
    {
        Init();
    }

    void Init()
    {
        ItemsDatabase.Instance.Init();
        mapFactory.SpawnShelves();
        
        Instantiate(playerPrefab, spawnLocation.position, Quaternion.identity);
        UIManager.Instance.InitQuestMenu();
        npcSpawner.Init();
    }
}
