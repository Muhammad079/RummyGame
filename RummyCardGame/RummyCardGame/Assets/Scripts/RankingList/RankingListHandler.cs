using Firebase.Database;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RankingListHandler : MonoBehaviour
{
    public static RankingListHandler instance;
    private void Awake()
    {
        instance = this;
    }
    public List<PlayerProfile> richUsers = new List<PlayerProfile>();
    public List<PlayerProfile> tournamentsRank = new List<PlayerProfile>();
    public List<PlayerProfile> leagueRanks = new List<PlayerProfile>();
    public List<PlayerProfile> giftRanks = new List<PlayerProfile>();
    public List<PlayerProfile> giftRanks_Weekly = new List<PlayerProfile>();
    public List<PlayerProfile> giftRanks_Monthly = new List<PlayerProfile>();
    public List<RankDisplayBox> o_leagueRank = new List<RankDisplayBox>();
    private DatabaseReference reference = null;
     bool loadLeagueRank = false;
    private TaskScheduler taskScheduler;

    void OnEnable()
    {
        taskScheduler = TaskScheduler.FromCurrentSynchronizationContext();
         reference = FirebaseDatabase.DefaultInstance.RootReference;
        Invoke(nameof(Init), 2);
    }
    void Init()
    {
        // GettingRichUsers();
        //   GettingLeagueRanking();

        GettingGiftsRank_Country();
        GettingGiftsRank_Weekly();
        GettingGiftsRank_Monthly();

        GettingTournamentsRank();
        SideRanks();
        //   DisplayLeagueRank();
    }
    void GettingRichUsers()
    {
        FirebaseDatabase.DefaultInstance.GetReference("Players").OrderByChild("trophies").LimitToLast(10).GetValueAsync().ContinueWith(task =>
        {
            if (task.IsCompleted)
            {
                DataSnapshot snapshot = task.Result;
                foreach (var a in snapshot.Children)
                {
                    // richUsers.Add( (PlayerProfile) JSON.Parse(a.GetRawJsonValue()));
                    richUsers.Add(JsonUtility.FromJson<PlayerProfile>(a.GetRawJsonValue()));
                }
            }
        }, taskScheduler);
        

    }
    void GettingTournamentsRank()
    {
        FirebaseDatabase.DefaultInstance.GetReference("Players").OrderByChild("tournamentXp").LimitToLast(10).GetValueAsync().ContinueWith(task =>
        {
            if (task.IsCompleted)
            {
                DataSnapshot snapshot = task.Result;
                foreach (var a in snapshot.Children)
                {
                    tournamentsRank.Add(JsonUtility.FromJson<PlayerProfile>(a.GetRawJsonValue()));
                }
            }
        }, taskScheduler); 

    }
    void GettingGiftsRank_Country()
    {
        FirebaseDatabase.DefaultInstance.GetReference("Players").OrderByChild("charmXp").LimitToLast(10).GetValueAsync().ContinueWith(task =>
        {
            if (task.IsCompleted)
            {
                DataSnapshot snapshot = task.Result;
                foreach (var a in snapshot.Children)
                {
                    PlayerProfile str = JsonUtility.FromJson<PlayerProfile>(a.GetRawJsonValue());
                    if (str.country == ProfileManager.instance.currentPlayer.country)
                        giftRanks.Add(JsonUtility.FromJson<PlayerProfile>(a.GetRawJsonValue()));

                }
                loadLeagueRank = true;
            }
        }, taskScheduler);
      

    }
    void GettingGiftsRank_Weekly()
    {
        FirebaseDatabase.DefaultInstance.GetReference("Players").OrderByChild("charmXp_Weekly").LimitToLast(10).GetValueAsync().ContinueWith(task =>
        {
            if (task.IsCompleted)
            {
                DataSnapshot snapshot = task.Result;

                foreach (var a in snapshot.Children)
                {
                    giftRanks_Weekly.Add(JsonUtility.FromJson<PlayerProfile>(a.GetRawJsonValue()));
                }
            }
        }, taskScheduler);
        
    }
    void GettingGiftsRank_Monthly()
    {
        FirebaseDatabase.DefaultInstance.GetReference("Players").OrderByChild("charmXp_Monthly").LimitToLast(10).GetValueAsync().ContinueWith(task =>
        {
            if (task.IsCompleted)
            {
                DataSnapshot snapshot = task.Result;
                foreach (var a in snapshot.Children)
                {
                    giftRanks_Monthly.Add(JsonUtility.FromJson<PlayerProfile>(a.GetRawJsonValue()));
                }
            }
        }, taskScheduler);
        

    }
   
    void GettingLeagueRanking()
    {
        FirebaseDatabase.DefaultInstance.GetReference("Players").OrderByChild("currentLeague").EqualTo(ProfileManager.instance.currentPlayer.currentLeague).GetValueAsync().ContinueWith(task =>
        {
            if (task.IsCompleted)
            {
                DataSnapshot snapshot = task.Result;
                foreach (var a in snapshot.Children)
                {
                    var pp = JsonUtility.FromJson<PlayerProfile>(a.GetRawJsonValue());
                    leagueRanks.Add(pp);
                }
                loadLeagueRank = true;
            }
        }, taskScheduler);
    }
    void DisplayLeagueRank()
    {
        if (loadLeagueRank)
        {
            for (int p = 0; p <= leagueRanks.Count - 2; p++)
            {
                for (int i = 0; i <= leagueRanks.Count - 2; i++)
                {
                    if (leagueRanks[i].weeklyWins < leagueRanks[i + 1].weeklyWins)
                    {
                        PlayerProfile t = leagueRanks[i + 1];
                        leagueRanks[i + 1] = leagueRanks[i];
                        leagueRanks[i] = t;
                        t = null;
                    }
                }
            }
            loadLeagueRank = false;
            if (leagueRanks.Count > 10)
            {
                leagueRanks.RemoveRange(3, leagueRanks.Count - 4);
            }

            if(SceneManager.GetActiveScene().name=="Home")
            {
                Debug.Log("Displaying leagues rank");
                for (int n = 0; n < o_leagueRank.Count; n++)
                {
                    Debug.Log("J");
                    if (n < leagueRanks.Count)
                        o_leagueRank[n].PassData(leagueRanks[n]);
                }
            }

            
        }
        else
        {
            Invoke(nameof(DisplayLeagueRank), 2);
        }
    }
    void SideRanks()
    {
        if (loadLeagueRank)
        {
            if (SceneManager.GetActiveScene().name == "Home")
            {

                Debug.Log("richinng ausidn");
                loadLeagueRank = false;
                for (int n = 0; n < o_leagueRank.Count; n++)
                {
                    Debug.Log("J");
                    if (n < giftRanks.Count)
                        o_leagueRank[n].PassData(giftRanks[giftRanks.Count - 1 - n]);
                }
            }
        }
        else
        {
            Invoke(nameof(SideRanks), 2);
        }
    }
}
