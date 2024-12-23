using TMPro;
using UnityEngine;
using UnityEngine.UI;
public class CardUnlockScreen : MonoBehaviour
{
    public CardsCollection cards = null;
    [SerializeField] private Transform upperGrid = null;
    public CardUnlockDisplay spade = null, club = null, diamond = null, heart = null;
    public Image cardImage = null;
    public Text collectionName = null;
    public Image crown = null;
    public TextMeshProUGUI cardName = null;
    void Start()
    {

    }

    public void PassData(CardsCollection collection)
    {
        cardImage.sprite = collection.collectionImage;
        collectionName.text = collection.name;

        collectionName.GetComponent<Kozykin.MultiLanguageItem>().strItems[0] = collection.name;
        collectionName.GetComponent<Kozykin.MultiLanguageItem>().strItems[1] = collection.name_Arabic;
        collectionName.GetComponent<Kozykin.MultiLanguageItem>().text = collection.name_Arabic;

        cards = collection;
        DisplayData();
    }
    void DisplayData()
    {
        for (int n = 0; n < upperGrid.childCount; n++)
        {
            upperGrid.GetChild(n).GetComponent<CardUnlockingSets>().PassData(cards.cards[n], this);
        }
    }
}
