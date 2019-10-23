using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemId : MonoBehaviour
{
    public int itemId;
    public bool isAvailable = true;

    public void Init(int id)
    {
        itemId = id;
    }
}
