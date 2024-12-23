using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RewardScript : MonoBehaviour
{
    [SerializeField] public BoxItems rewardToCollect = new BoxItems();
    [SerializeField] private Text quantityText = null;

    public void GetReward(BoxItems reward,Sprite rewardImage)
    {
        GetComponent<Image>().sprite = rewardImage;
        quantityText.text = reward.quantity.ToString();
        quantityText.GetComponent<Kozykin.MultiLanguageItem>().strItems[0] = reward.quantity.ToString();
        quantityText.GetComponent<Kozykin.MultiLanguageItem>().strItems[1] = reward.quantity.ToString();
        quantityText.GetComponent<Kozykin.MultiLanguageItem>().text = reward.quantity.ToString();

        rewardToCollect = reward;
    }
}
