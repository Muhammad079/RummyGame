using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "Gifts Data File", menuName ="Gifts Data File")]
public class Scriptable_Gifts :ScriptableObject
{
    public List<Gifts> gifts;
}
[System.Serializable]
public class Gifts
{
    public GiftItems gift;
    public Sprite giftImage=null;
    public int price=0;
    public bool forVIP=false;
    public int charmXp = 0;
}
[System.Serializable]
public class DbGifts
{
    public int gift=0;
    public int count = 0;
}
public enum GiftItems
{
    candy,ring,car,boat,helicopter,island,treasureBox,star
}