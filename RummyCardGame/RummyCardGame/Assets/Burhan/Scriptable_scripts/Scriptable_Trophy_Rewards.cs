using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="Trophy_Rewards_Data",menuName = "Trophy_Rewards_Data")]
public class Scriptable_Trophy_Rewards : ScriptableObject
{
    public List<PortionLevel> Portion_Level = new List<PortionLevel>();
}
[System.Serializable]
public class PortionLevel
{
    public List<TrophyRewards> Portion_Rewards = new List<TrophyRewards>();
}




[System.Serializable]
public class TrophyRewards
{
    public Sprite Reward_Images;
    public int Trophies_Required;
    public RewardItems rewards;
}
