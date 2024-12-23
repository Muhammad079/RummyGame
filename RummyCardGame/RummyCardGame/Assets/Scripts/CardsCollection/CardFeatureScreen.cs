using UnityEngine;

public class CardFeatureScreen : MonoBehaviour
{
    [SerializeField] private Transform gridParent = null;
    [SerializeField] private GameObject collectionObject = null;
    void Start()
    {
        InitDisplay();
    }
    void InitDisplay()
    {
        var col = GameManager.instance.cardsCollection.cardsCollection;
        for (int n = 0; n < col.Count; n++)
        {
            GameObject a = new GameObject();
            if (n < gridParent.childCount)
                a = gridParent.GetChild(n).gameObject;
            else
                a = Instantiate(collectionObject, gridParent);
            a.GetComponent<CardFeatureSelection>().PassData(col[n]);
        }
    }
}
