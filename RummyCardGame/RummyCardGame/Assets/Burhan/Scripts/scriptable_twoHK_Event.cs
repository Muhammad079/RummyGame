using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="Two_HK_Event_data",menuName = "Two_HK_Event_data")]
public class scriptable_twoHK_Event : ScriptableObject
{
    public List<two_HK_Values> two_HK_Tables = new List<two_HK_Values>();
}
[System.Serializable]
public class two_HK_Values
{
    public int Entry_fee;
    public int Total_Prize;
    public int Crowns_Req_to_Unlock;
    public int Crowns_Req_for_Prize;
    public int Gems_Recovery;
    public int Crowns_Win;
    public int Crowns_Loss;
}
