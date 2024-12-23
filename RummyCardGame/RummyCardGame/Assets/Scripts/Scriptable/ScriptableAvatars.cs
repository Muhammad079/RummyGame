using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "Avatars", menuName = "Avatars")]
public class ScriptableAvatars : ScriptableObject
{
    public List<Avatar> avatars;
}
[System.Serializable]
public class Avatar
{
    public Sprite avatarImage;
    public bool lockStatus = false;
    public bool equipStatus = false;
    public int priceInCoins;
    public int priceInGems;
    public int unlockDaysDuration;
    public bool onlyForVip;
    public bool fromEvent;
    public int levelReq = 0;
}
[System.Serializable]
public class DbAvatar
{
    public string name;
    public bool lockStatus = true;
    public bool equipStatus = false;
    public string unlockDuration = "";
    public int levelReq = 0;
}

