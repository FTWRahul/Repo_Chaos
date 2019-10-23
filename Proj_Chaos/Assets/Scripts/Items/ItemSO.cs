using UnityEngine;

public enum TypeSize
{
        SMALL,
        MEDIUM,
        LARGE
}

[CreateAssetMenu(menuName = "Items", fileName = "newItem")]
public class ItemSO : ScriptableObject
{
    public int itemId;
    public string itemName;
    public GameObject itemPrefab;
    public TypeSize typeSize;

}
