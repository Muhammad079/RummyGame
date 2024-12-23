using System;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ItemBuyPanel : MonoBehaviour
{
    [SerializeField] private Image displayImage = null;
    [SerializeField] private Button buyButton = null;
    [SerializeField] private GameObject coins = null, gems = null;
    [SerializeField] private TextMeshProUGUI amountText = null;
    [SerializeField] private Text descriptionText = null;
    private ProfileItemsBuy item = null;

     void Start()
    {
        buyButton.onClick.AddListener(OnClick);
    }

    internal void PassData(ProfileItemsBuy profileItemsBuy, Sprite itemImage, int priceInGems, int priceInCoins, bool onlyForVip, int duration,bool fromEvent)
    {
        displayImage.sprite = itemImage;
        item = profileItemsBuy;
        if (!fromEvent)
        {
            if (priceInCoins == 0)
            {
                coins.SetActive(false);
            }
            else
            {
                coins.SetActive(true);
                amountText.text = priceInCoins.ToString();
            }
            if (priceInGems == 0)
            {
                gems.SetActive(false);
            }
            else
            {
                gems.SetActive(true);
                amountText.text = priceInGems.ToString();
            }
        }
        else
        {
            gems.SetActive(false);
            coins.SetActive(false);
            amountText.text = "Unlock From Event";
        }
        descriptionText.text = "Unlock only for " + duration + " days.";
        if (ProfileManager.instance.currentPlayer.coins >= priceInCoins && ProfileManager.instance.currentPlayer.gems >= priceInGems && ProfileManager.instance.currentPlayer.isVip == onlyForVip)
        {
            buyButton.interactable = true;
        }else
            buyButton.interactable = false;
    }
    void OnClick()
    {
        Debug.Log("CLick buy");
        item.PurchaseSuccess();
    }

    
}
