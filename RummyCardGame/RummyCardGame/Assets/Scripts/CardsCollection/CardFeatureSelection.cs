using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CardFeatureSelection : MonoBehaviour
{
    [SerializeField] private CardsCollection collection = null;
    [SerializeField] private GameObject lockObject;
    void Start()
    {
        GetComponent<Button>().onClick.AddListener(OnClick);
    }

    public void PassData(CardsCollection cards)
    {
        GetComponent<Image>().sprite = cards.collectionImage;
        collection =cards;
        LockStatus();
    }
    void LockStatus()
    {
        if (collection.cards[collection.cards.Count - 1].club.cardCount >= 10 &&
            collection.cards[collection.cards.Count - 1].heart.cardCount >= 10 &&
            collection.cards[collection.cards.Count - 1].spade.cardCount >= 10 &&
            collection.cards[collection.cards.Count - 1].diamond.cardCount >= 10)
        {
            lockObject.SetActive(false);
        }
        else
            lockObject.SetActive(true);
    }
    void OnClick()
    {
        CardsCollectionHandler.instance.cardFeatureScreen.SetActive(false);
        CardsCollectionHandler.instance.cardUnlockScreen.GetComponent<CardUnlockScreen>().PassData(collection );
       CardsCollectionHandler.instance.cardUnlockScreen.SetActive(true);
    }
}
