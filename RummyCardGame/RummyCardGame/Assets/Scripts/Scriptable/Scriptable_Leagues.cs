using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName ="Leagues Data",menuName ="Legues Asset File")]
public class Scriptable_Leagues :ScriptableObject
{
    public List<Leagues> leagues;
}
[System.Serializable]
public class Leagues
{
    public string name;
    public RewardItems firstPosReward, secondPosReward, thirdPosReward;
    public int trophiesReq;
    public Sprite leagueImage;
}
