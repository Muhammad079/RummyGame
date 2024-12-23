using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(menuName ="Lucky Spin Data",fileName ="Lucky Spin")]
public class Scriptable_LuckySpin : ScriptableObject
{
    public List<SpinReward> spinRewards;
  
}
[System.Serializable]
public class SpinReward
{
    public List<LuckySpinReward> reward;
    public int availabilityCounter = 0;
}
[System.Serializable]
public class LuckySpinReward
{
    public RewardItems reward;
}

