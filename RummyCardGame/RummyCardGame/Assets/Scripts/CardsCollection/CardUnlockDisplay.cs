using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CardUnlockDisplay : MonoBehaviour
{
    [SerializeField] private Image cardImage = null;
    [SerializeField] private Image fillingImage = null;
    [SerializeField] private Text countText = null;
    [SerializeField] private Button rewardButton = null;
    Card card = null;
    [SerializeField] private int rewardingCoins = 0;
    private void Start()
    {
        rewardButton.onClick.AddListener(OnClick);
    }
    public void PassData(Card passingCard)
    {
        card = passingCard;
        Debug.Log("Show Data");
        card.cardCount = Mathf.Clamp(card.cardCount, 0, 10);
        cardImage.sprite = card.cardImage;
        countText.text = card.cardCount + "/10";
        fillingImage.fillAmount = FillingAmount(card.cardCount);
        RewardStatus();
    }
    void RewardStatus( )
    {
        if (card.cardCount==10)
        {
            if (card.rewardStatus == -1)
            {
                card.rewardStatus = 0;
                rewardButton.interactable = true;
            }
            else if (card.rewardStatus > 0)
            {
                rewardButton.interactable = false;
            }
            else if(card.rewardStatus == 0)
            {
                rewardButton.interactable = true;
            }
        }
        else
        {
            rewardButton.interactable = false;
        }
    }
    float FillingAmount(int count)
    {
        return (float)count / 10;
    }
 void OnClick()
    {
        ProfileManager.instance.GetReward(RewardType.coins, rewardingCoins, null);
        card.rewardStatus = 1;
        rewardButton.interactable = false;
        CardsCollectionHandler.instance.PushData();
    }
}
