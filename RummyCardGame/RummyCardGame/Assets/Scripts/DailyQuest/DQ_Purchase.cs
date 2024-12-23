using UnityEngine;

public class DQ_Purchase : DQ_QuestBar
{
    [SerializeField] private float purchaseAmount = 0;
    private void OnEnable()
    {
        WinStatus(WinningCondition, this.gameObject.name);
    }
    private bool WinningCondition()
    {
        if (ProfileManager.instance.currentPlayer.totalPurchases > purchaseAmount)
            return true;
        else
            return false;
    }
}

