using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DQ_CupReward :DQ_QuestBar
{   [SerializeField] private int cupsCount = 0;
    private void OnEnable()
    {
        WinStatus(WinningCondition,this.gameObject.name);
    }
    public bool WinningCondition()
    {
        if (ProfileManager.instance.currentPlayer.trophies >= cupsCount)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}
