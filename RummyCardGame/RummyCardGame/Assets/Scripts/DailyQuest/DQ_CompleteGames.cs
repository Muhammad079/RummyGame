using UnityEngine;

public class DQ_CompleteGames : DQ_QuestBar
{
    [SerializeField] private float gamesCount = 0;
    private void OnEnable()
    {
        WinStatus(WinningCondition, this.gameObject.name);
    }
    private bool WinningCondition()
    {
        if (ProfileManager.instance.currentPlayer.totalWins+ ProfileManager.instance.currentPlayer.totalLoss > gamesCount)
            return true;
        else
            return false;
    }
}

