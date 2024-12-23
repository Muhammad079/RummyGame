using UnityEngine;
using UnityEngine.UI;

public class DQ_GiftSend :DQ_QuestBar
{
    [SerializeField] private int giftsCount = 0;
    private void OnEnable()
    {
        WinStatus(WinningCondition, this.gameObject.name);
    }
    public bool WinningCondition()
    {
        if (ProfileManager.instance.currentPlayer.sendGifts >= giftsCount)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}
