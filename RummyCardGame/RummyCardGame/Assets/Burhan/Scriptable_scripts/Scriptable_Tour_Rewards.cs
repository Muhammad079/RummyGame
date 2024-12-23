using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenuAttribute(menuName = "Scriptable_Tour_Rewards", fileName = "Scriptable_Tour_Rewards")]
public class Scriptable_Tour_Rewards : ScriptableObject
{
    public List<Tournament_Rewards> tournament_Rewards;
}

[System.Serializable]
public class Tournament_Rewards
{
    public List<RewardItems> Rewards;
    public Sprite Sprites;
    public int id;
    public string rewardTitle;
}
