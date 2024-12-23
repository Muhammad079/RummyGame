using UnityEngine;

[System.Serializable]
public class RewardItems
{
    public RewardType reward;
    public int quantity;
    [Header("add data if reward type is box else leave blank")]
    public BoxRewards boxReward;
}
