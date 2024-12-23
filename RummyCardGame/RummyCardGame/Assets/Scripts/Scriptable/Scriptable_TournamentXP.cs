using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Tournament XP Data", fileName = "TournamentXpData")]
public class Scriptable_TournamentXP : ScriptableObject
{
    public List<TournamentMedalAndReq> tournamentMedals = new List<TournamentMedalAndReq>();
}
[System.Serializable]
public class TournamentMedalAndReq
{
    public int xpReq = 0;
    public Sprite tournamentMedal = null;
}