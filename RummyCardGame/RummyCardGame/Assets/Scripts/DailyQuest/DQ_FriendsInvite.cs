using UnityEngine;

public class DQ_FriendsInvite : DQ_QuestBar
{
    [SerializeField] private int invitationCount = 0;
    private void OnEnable()
    {
        WinStatus(WinningCondition, this.gameObject.name);
    }
    public bool WinningCondition()
    {
        if (ProfileManager.instance.currentPlayer.consectiveLogin >= invitationCount)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}
