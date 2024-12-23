using DG.Tweening;
using UnityEngine;

public class LeaguesHandler : MonoBehaviour
{
   
    [SerializeField] private Transform gridParent = null;
    public static int currentLeague = 0;
    private void Start()
    {
        DisplayLeageus();
    }

    void DisplayLeageus()
    {
        if (ProfileManager.instance.currentPlayer.leagueCheck == "")
        {
            CurrentLeagueCheck();
        }
        else
        {
            if ((System.DateTime.Now - System.DateTime.Parse(ProfileManager.instance.currentPlayer.leagueCheck)).TotalSeconds > 0)
            {
                 Debug.Log("GiveReward");
                GiveReward();
                CurrentLeagueCheck();
                ProfileManager.instance.currentPlayer.weeklyWins = 0;
                ProfileManager.instance.currentPlayer.leagueCheck = System.DateTime.Now.AddDays(7).ToString();
            }
            else
            {
                currentLeague = ProfileManager.instance.currentPlayer.currentLeague;
            }
        }
        for (int n = 0; n <GameManager.instance. leaguesFile.leagues.Count; n++)
        {
            if (n >= gridParent.childCount)
                Instantiate(gridParent.GetChild(0).gameObject, gridParent);
            gridParent.GetChild(n).GetComponent<LeagueBox>().PassData(GameManager.instance.leaguesFile.leagues[n]);
        }
    }
    void CurrentLeagueCheck()
    {
        ProfileManager.instance.currentPlayer.leagueCheck = System.DateTime.Now.AddDays(7).ToString();
        for (int n = 0; n < GameManager.instance.leaguesFile.leagues.Count - 1; n++)
        {
            if (ProfileManager.instance.currentPlayer.trophies < GameManager.instance.leaguesFile.leagues[n + 1].trophiesReq)
            {
                currentLeague = n;
                ProfileManager.instance.currentPlayer.currentLeague = n;
                break;
            }
            else
            {
                currentLeague = n + 1;
                ProfileManager.instance.currentPlayer.currentLeague = n;
            }
        }
        ProfileManager.instance.SaveUserData();
    }
    public void Show()
    {
      //  transform.DOScale(1, 0.5f);
    }
    public void Hide()
    {
        transform.DOScale(0, 0.5f);
    }
    void GiveReward()
    {
        if (RankingListHandler.instance.leagueRanks.Count > 0)
        {
            if (ProfileManager.instance.currentPlayer.id == RankingListHandler.instance.leagueRanks[RankingListHandler.instance.leagueRanks.Count - 1].id)
            {
                Debug.Log("First Position");
                ProfileManager.instance.GetReward(GameManager.instance.leaguesFile.leagues[currentLeague].firstPosReward.reward, GameManager.instance.leaguesFile.leagues[currentLeague].firstPosReward.quantity);
            }
            else if (ProfileManager.instance.currentPlayer.id == RankingListHandler.instance.leagueRanks[RankingListHandler.instance.leagueRanks.Count - 2].id)
            {
                Debug.Log("Second Position");
                ProfileManager.instance.GetReward(GameManager.instance.leaguesFile.leagues[currentLeague].secondPosReward.reward, GameManager.instance.leaguesFile.leagues[currentLeague].secondPosReward.quantity);
            }
            else if (ProfileManager.instance.currentPlayer.id == RankingListHandler.instance.leagueRanks[RankingListHandler.instance.leagueRanks.Count - 3].id)
            {
                Debug.Log("Third Position");
                ProfileManager.instance.GetReward(GameManager.instance.leaguesFile.leagues[currentLeague].thirdPosReward.reward, GameManager.instance.leaguesFile.leagues[currentLeague].thirdPosReward.quantity);
            }
        }
        else
        {
            Invoke(nameof(GiveReward), 2);
        }
    }
}
