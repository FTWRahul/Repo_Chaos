using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ShelfType
{
    Shelf1,
    Shelf2,
    Shelf3,
    Table,
}

public class Shelf : MonoBehaviour
{
    public List<Transform> smallItemPosition;
    public List<Transform> mediumItemPosition;
    public List<Transform> largeItemPosition;

    public MeshRenderer meshRenderer;
    public Material defaultMat;
    
    public ShelfType shelfType;

    private void Awake()
    {
        meshRenderer = GetComponentInChildren<MeshRenderer>();
    }

    public void GenerateItems(Item item)
    {
        SetTexture(item);
        if (item.typeSize == TypeSize.SMALL)
        {
            PlaceItem(smallItemPosition, item);
        }
        else if (item.typeSize == TypeSize.MEDIUM)
        {
            PlaceItem(mediumItemPosition, item);
        }
        else if (item.typeSize == TypeSize.LARGE)
        {
            PlaceItem(largeItemPosition, item);
        }
    }

    public void PlaceItem(List<Transform> positions, Item item)
    {
        for (int i = 0; i < positions.Count; i++)
        {
            SpawnedItem tempGo = Instantiate(item.itemPrefab, positions[i].position, transform.rotation).GetComponent<SpawnedItem>();
            tempGo.transform.parent = this.transform;
            tempGo.Init(item.itemId);
            
            if(ItemsDatabase.Instance.itemsOnScene.ContainsKey(item.itemId))
            {
                ItemsDatabase.Instance.itemsOnScene[item.itemId]++;
            }
            else
            {
                ItemsDatabase.Instance.itemsOnScene.Add(item.itemId, 1);
            }
        }
    }

    public void SetTexture(Item item)
    {
        if (shelfType == ShelfType.Shelf1)
        {
            meshRenderer.material.mainTexture = item.ShelfArt1;
        }

        if (shelfType == ShelfType.Shelf2)
        {
            meshRenderer.material.mainTexture = item.ShelfArt2;


        }

        if (shelfType == ShelfType.Shelf3)
        {
            meshRenderer.material.mainTexture = item.ShelfArt3;


        }

        if (shelfType == ShelfType.Table)
        {
            meshRenderer.material.mainTexture = item.ShelfArt4;

        }
    }
}

