using UnityEngine;

public class DQ_LuckyBox : DQ_QuestBar
{
    [SerializeField] private int boxOpened = 0;
    private void OnEnable()
    {
        WinStatus(WinningCondition, this.gameObject.name);
    }
    private bool WinningCondition()
    {
        if (ProfileManager.instance.currentPlayer.boxesOpenedToday > boxOpened)
            return true;
        else
            return false;
    }
}

