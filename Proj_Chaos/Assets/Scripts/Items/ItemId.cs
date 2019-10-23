using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemId : MonoBehaviour
{
    public int itemId;
    public bool isAvailable = true;
    public MeshRenderer meshRenderer;
    public Material defaultMat;

    private void Awake()
    {
        meshRenderer = GetComponent<MeshRenderer>();
    }

    public void Init(int id)
    {
        itemId = id;
        for (int i = 0; i < ItemsDatabase.Instance.database.Count; i++)
        {
            if (itemId == ItemsDatabase.Instance.database[i].itemId)
            {
                //meshRenderer.material = new Material(meshRenderer.material);
                //meshRenderer.material == ItemsDatabase.Instance.database[i].boxArt;
                meshRenderer.material.mainTexture = ItemsDatabase.Instance.database[i].boxArt;
            }
        }
    }
}
