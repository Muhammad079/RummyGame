using UnityEngine;
using UnityEngine.UI;

public class DQ_WinsOn50K : DQ_QuestBar{
    [SerializeField] private int winsCount = 0;
    private void OnEnable()
    {
        WinStatus(WinningCondition, this.gameObject.name);
    }
    public bool WinningCondition()
    {
        if (ProfileManager.instance.currentPlayer.winsOn50KTable >= winsCount)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}
