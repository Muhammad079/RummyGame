public class DQ_IsVip : DQ_QuestBar
{
    private void OnEnable()
    {
        WinStatus(WinningCondition, this.gameObject.name);
    }
    public bool WinningCondition()
    {
        if (ProfileManager.instance.currentPlayer.isVip)
        {
            return true;
        }
        else
        {
            return false;
        }

    }
}
