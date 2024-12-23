using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "XP Data", menuName = "XP Data")]
public class Scriptable_XPData : ScriptableObject
{
    public List<XPData> data = new List<XPData>();
}
[System.Serializable]
public class XPData
{
    public int coinsBid = 0;
    public int xpOnWin = 0;
    public int xpOnLoss = 0;
    public int trophiesOnWin = 0;
    public int trophiesOnLoss = 0;
    public int maxTrophiesWin = 0;
    public int goldenCards = 3;
    public List<RewardItems> positionRewards;
}


