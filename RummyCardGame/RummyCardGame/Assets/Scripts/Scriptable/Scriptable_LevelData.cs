using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Level Data", menuName = "Level Data")]
public class Scriptable_LevelData : ScriptableObject
{
    public List<LevelData> levelsData = new List<LevelData>();
}
[System.Serializable]
public class LevelData
{
    public string name = "";
    public int xpReq = 0; 
    public RewardItems Rewards;
}