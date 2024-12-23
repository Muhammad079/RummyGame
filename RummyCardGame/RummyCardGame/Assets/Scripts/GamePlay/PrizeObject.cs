using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PrizeObject : MonoBehaviour
{
   
    internal void DisplayReward(Sprite rewardImage, int quantity)
    {
        GetComponent<Image>().sprite = rewardImage;
        transform.GetChild(0).GetComponent<Text>().text = quantity.ToString();
    }
}
