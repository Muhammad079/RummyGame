using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName ="Trophies based Reward",fileName ="TrophiesReward")]
public class Scriptable_TrophiesBasedReward : ScriptableObject
{
    [Header("List of Trophy Rewards")]
    public List<TrophyReward> trophyReward;
}
[System.Serializable]
public class TrophyReward
{
    public string name;
    public int reqTrophies;
    public List<RewardItems> rewardType;
}
