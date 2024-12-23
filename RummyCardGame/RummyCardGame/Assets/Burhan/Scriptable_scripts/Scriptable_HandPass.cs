using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenuAttribute(menuName = "HandPass_Data", fileName = "HandPass_Data")]
public class Scriptable_HandPass : ScriptableObject
{
    public List<HandPassReward> handpass_Rewards;


    //public int id=0;
}
[System.Serializable]
public class HandPassReward
{
    public List<RewardItems> Rewards;
    public  Sprite  Premium_sprites;
    public  string  Premium_texts;
    public string Premium_texts_Arabic;
    //public  int  id;
}
