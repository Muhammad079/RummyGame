using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="VIP_Information",menuName = "VIP_Information")]
public class scriptable_VIP_information : ScriptableObject
{
    public List<Values> VIPs = new List<Values>();
}

[System.Serializable]
public class Values
{
    public int VIP_Points;
    public int Daily_Bonus;
    public int Friend_List;
    public int Gift_Exchange_PER;
    public int Golden_Spin;
    public int Coins_Purchase_PER;
}
