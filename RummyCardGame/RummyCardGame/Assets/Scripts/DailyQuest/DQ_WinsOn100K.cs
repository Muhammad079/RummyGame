using UnityEngine;
using UnityEngine.UI;

public class DQ_WinsOn100K : DQ_QuestBar{
    [SerializeField] private int winsCount = 0;
    private void OnEnable()
    {
        WinStatus(WinningCondition, this.gameObject.name);
    }
    public bool WinningCondition()
    {
        if (ProfileManager.instance.currentPlayer.winsOn100KTable >= winsCount)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}
