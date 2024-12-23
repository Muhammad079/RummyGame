using TMPro;
using UnityEngine;
using UnityEngine.UI;
public class CardUnlockingSets : MonoBehaviour
{
    [SerializeField] private CardSet set = null;
    [SerializeField] private TextMeshProUGUI cardName = null;
    [SerializeField] private GameObject lockObject = null;
    CardUnlockScreen screen;
    void Start()
    {
        GetComponent<Button>().onClick.AddListener(OnClick);
    }

    public void PassData(CardSet cardSet, CardUnlockScreen cardUnlockScreen)
    {
        set = cardSet;
        screen = cardUnlockScreen;
        if (cardSet.spade.cardCount >= 10 && cardSet.club.cardCount >= 10 && cardSet.heart.cardCount >= 10 && cardSet.diamond.cardCount >= 10)
        {
            cardName.gameObject.SetActive(true);
            GetComponent<Image>().enabled = true;
            lockObject.SetActive(false);
            GetComponent<Image>().sprite = cardUnlockScreen.cards.collectionImage;
            cardName.text = set.name;
        }
        else
        {
            lockObject.SetActive(true);
            cardName.gameObject.SetActive(false);
            GetComponent<Image>().enabled = false;
            lockObject.GetComponent<Image>().sprite = cardUnlockScreen.cards.lockImage;
            lockObject.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = set.name;
        }
        cardName.text = set.name;
    }
    void OnClick()
    {
        screen.cardName.text = set.name;
        screen.spade.PassData(set.spade);
        screen.club.PassData(set.club );
        screen.diamond.PassData(set.heart );
        screen.heart.PassData(set.diamond );
    }

}
