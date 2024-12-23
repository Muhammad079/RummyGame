using UnityEngine;

public class DQ_7DayLogin : DQ_QuestBar
{
    [SerializeField] private int loginCount = 0;
    private void OnEnable()
    {
        WinStatus(WinningCondition, this.gameObject.name);
    }
    public bool WinningCondition()
    {
        if (ProfileManager.instance.currentPlayer.consectiveLogin >= loginCount)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}
