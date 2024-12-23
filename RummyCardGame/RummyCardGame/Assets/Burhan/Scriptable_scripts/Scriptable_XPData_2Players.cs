using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "XPData_2Players", menuName = "XPData_2Players")]
public class Scriptable_XPData_2Players : ScriptableObject
{
    public List<XPData_2Players> data = new List<XPData_2Players>();
}
[System.Serializable]
public class XPData_2Players
{
    public int coinsBid = 0;
    public int xpOnWin = 0;
    public int xpOnLoss = 0;
    public int trophiesOnWin = 0;
    public int trophiesOnLoss = 0;
    public int maxTrophiesWin = 0;
    public List<RewardItems> winReward;
}


