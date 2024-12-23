using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="Medals4_Data",menuName = "Medals4_Data")]
public class Scriptable_4Medals : ScriptableObject
{
    public List<Medals4_Credentials> Medals = new List<Medals4_Credentials>();
}
[System.Serializable]
public class Medals4_Credentials
{
    public string Medal_Name;
    public Sprite Medal_Image;
    //public bool isLocked;
}