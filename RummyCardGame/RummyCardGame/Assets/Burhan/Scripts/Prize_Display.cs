using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Prize_Display : MonoBehaviour
{
    public Button Collect_BTN;
    public Text Vip_Count, Gems_Count, Coins_Count, SBox_Count, BBox_Count;
    private Queue<RewardItems> pendingRewards = new Queue<RewardItems>();
    // Start is called before the first frame update
    void Start()
    {
        Collect_BTN.onClick.AddListener(CollectReward);
        DefaultValue();
    }
    void DefaultValue()
    {
        Vip_Count.text = 0.ToString();
        Gems_Count.text = 0.ToString();
        Coins_Count.text = 0.ToString();
        SBox_Count.text = 0.ToString();
        BBox_Count.text = 0.ToString();
        Vip_Count.transform.parent.parent.gameObject.SetActive(false);
        Gems_Count.transform.parent.parent.gameObject.SetActive(false);
        Coins_Count.transform.parent.parent.gameObject.SetActive(false);
        SBox_Count.transform.parent.parent.gameObject.SetActive(false);
        BBox_Count.transform.parent.parent.gameObject.SetActive(false);
        Collect_BTN.gameObject.SetActive(false);
    }
    internal void UpdateValues(RewardItems reward)
    {
        int quantity = 0;
        if (reward.reward == RewardType.coins)
        {
            Coins_Count.transform.parent.parent.gameObject.SetActive(true);
            quantity = int.Parse(Coins_Count.text);
            quantity += reward.quantity;
            Coins_Count.text = quantity.ToString();
        }
        else if (reward.reward == RewardType.vip)
        {
            Vip_Count.transform.parent.parent.gameObject.SetActive(true);
            quantity = int.Parse(Vip_Count.text);
            quantity += reward.quantity;
            Vip_Count.text = quantity.ToString();
        }
        else if (reward.reward == RewardType.gems)
        {
            Gems_Count.transform.parent.parent.gameObject.SetActive(true);
            quantity = int.Parse(Gems_Count.text);
            quantity += reward.quantity;
            Gems_Count.text = quantity.ToString();
        }
        else if (reward.reward == RewardType.box)
        {
            if (reward.boxReward.boxType == BoxType.silver)
            {
                SBox_Count.transform.parent.parent.gameObject.SetActive(true);
                quantity = int.Parse(SBox_Count.text);
                quantity += reward.quantity;
                SBox_Count.text = quantity + " Silver".ToString();
            }
            else
            {
                BBox_Count.transform.parent.parent.gameObject.SetActive(true);
                quantity = int.Parse(BBox_Count.text);
                quantity += reward.quantity;
                BBox_Count.text = quantity + " Bronze".ToString();
            }
        }
        Collect_BTN.gameObject.SetActive(true);
        pendingRewards.Enqueue(reward);

    }
    public void CollectReward()
    {
        while (pendingRewards.Count > 0)
        {
            var a = pendingRewards.Dequeue();
            ProfileManager.instance.GetReward(a.reward, a.quantity, a.boxReward);
        }
        DefaultValue();
    }
}
