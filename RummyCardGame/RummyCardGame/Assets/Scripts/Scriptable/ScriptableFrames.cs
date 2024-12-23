using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "Frames", menuName = "Frames")]
public class ScriptableFrames : ScriptableObject
{
    public List<Frames> frames;
}
[System.Serializable]
public class Frames
{
    public Sprite frameImage;
    public bool fromEvent = false;
    public bool collectAble=false;
    public bool onlyForVip = false;
    public int priceInGems = 0;
    public int priceInCoins = 0;
    public int levelReq = 0;
    public int unlockDaysDuration = 0;
    public bool lockStatus = false;
    public bool equipStatus = false;
}
[System.Serializable]
public class DbFrames
{
    public string name;
    public bool lockStatus = true;
    public bool equipStatus = false;
    public string unlockDuration;
}
