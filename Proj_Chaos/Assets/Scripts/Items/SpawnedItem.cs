using UnityEngine;


public class SpawnedItem : MonoBehaviour
{
    public int itemId;
    public bool isAvailable = true;
    public MeshRenderer meshRenderer;
    
    public Material defaultMat;
    public Material highlightMat;

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
                var material = meshRenderer.material;
                material.mainTexture = ItemsDatabase.Instance.database[i].boxArt;
                defaultMat = material;
                highlightMat = new Material(highlightMat) {mainTexture = ItemsDatabase.Instance.database[i].boxArt};
            }
        }
    }

    public void Vanish()
    {
        LeanTween.move(gameObject, transform.position + Vector3.up * 10, 0.5f);
        Destroy(gameObject, 1f);
        /*transform.DOLocalMove( transform.position + UnityEngine.Vector3.up * 2, 1f).SetEase(Ease.OutSine);*/
        //transform.GetComponent<MeshRenderer>().material.DOFade(0f, .5f).SetEase(Ease.Linear);
    }

    public void Highlight()
    {
        meshRenderer.material = highlightMat;
    }
    
    public void Dehighlight()
    {
        meshRenderer.material = defaultMat;
    }
    
}
