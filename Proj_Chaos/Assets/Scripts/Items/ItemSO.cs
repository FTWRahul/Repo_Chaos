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
    public Texture2D boxArt;
    public Texture2D ShelfArt1;
    public Texture2D ShelfArt2;
    public Texture2D ShelfArt3;
    public Texture2D ShelfArt4;
}
