using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenuAttribute(menuName = "Free_HandPass_Data", fileName = "Free_HandPass_Data")]
public class Scriptable_Handpass_free : ScriptableObject
{
    public List<FreeHandPassReward> Free_rewards;
}
[System.Serializable]
public class FreeHandPassReward
{
    public List<RewardItems> Rewards;
    public Sprite Free_sprites;
    //public Text Premium_texts;
    public string Free_texts;
    public string Free_texts_Arabic;
}
