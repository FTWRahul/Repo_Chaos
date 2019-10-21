using UnityEngine;

public enum ItemSize
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
    public ItemSize itemSize;

}
