﻿using UnityEngine;
using UnityEngine.UI;

public class DQ_LuckySpins :DQ_QuestBar{
    [SerializeField] private int spinsCount = 0;
    private void OnEnable()
    {
        WinStatus(WinningCondition, this.gameObject.name);
    }
    public bool WinningCondition()
    {
        if (ProfileManager.instance.currentPlayer.luckySpinsToday >= spinsCount)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}
