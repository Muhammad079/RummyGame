using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DQ_FacebookLogin :DQ_QuestBar
{
    private void OnEnable()
    {
        WinStatus(WinningCondition, this.gameObject.name);
    }
    public bool WinningCondition()
    {
        if (ProfileManager.instance.currentPlayer.facebookLogin)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}
