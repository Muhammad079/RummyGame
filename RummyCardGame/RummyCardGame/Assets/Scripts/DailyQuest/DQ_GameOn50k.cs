using UnityEngine;
using UnityEngine.UI;

public class DQ_GameOn50k : DQ_QuestBar{
    [SerializeField] private int gamesCount = 0;
    private void OnEnable()
    {
        WinStatus(WinningCondition, this.gameObject.name);
    }
    public bool WinningCondition()
    {
        if (ProfileManager.instance.currentPlayer.gamesOn50KTable >= gamesCount)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}
