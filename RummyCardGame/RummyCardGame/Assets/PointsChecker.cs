using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Photon.Pun;
using UnityEngine;

public class PointsChecker : MonoBehaviour
{
    public Gameplay_Manager Gameplay_Manager;

    public List<InGamePlayer> inGamePlayers = new List<InGamePlayer>();

    public PhotonView Pv;

    public List<ResultDataForScore> resultDataForScores = new List<ResultDataForScore>();

    public Action CalculateResult;
    public bool recalculate;

    public Transform WinnerParent;
    public WinnerPrefab WinnerPrefab;

    public Transform LooserParent;
    public LooserPrefab LooserPrefab;

    public Resultscreen ResultScreen;

    public bool looser;

    private void Start()
    {
        Pv = GetComponent<PhotonView>();
        InvokeRepeating(nameof(CountScore), 2, 2f);
        CalculateResult += CheckForResults;
    }
    // Will continously update the total players
    private void CountScore()
    {
        Debug.LogError("Checking Score");
        inGamePlayers = Gameplay_Manager.totalPlayers;
    }

    int matchedCards = 0;
    [SerializeField]
    InGamePlayer Player;
    [SerializeField]
    List<CardView> MyCards;


    /// <summary>
    /// when knock out button is pressed this means someone has won
    /// </summary>
    [ContextMenu("OnKnockout Button")]
    public void OnKnockOutButton()
    {
        Pv.RPC("SendListAndCardsEvents", RpcTarget.All);
    }

    /// <summary>
    /// when I have pressed knockout btn so that everyone knows
    /// Also I am sending my data to all
    /// </summary>
    [PunRPC]
    private void SendListAndCardsEvents()
    {
        ResultDataForScore result = new ResultDataForScore();
        // finding my card and my player list to see where card are matched or not
        result.Player = inGamePlayers.Where(x => x.ID == ProfileManager.instance.currentPlayer.id).FirstOrDefault();
        // Sending all my cards in hand

        //result.MyCards = GamePlayPlayer.instance.gridParent.GetComponentsInChildren<CardView>().ToList();

        MyCards = GamePlayPlayer.instance.gridParent.GetComponentsInChildren<CardView>().ToList();

        foreach (var item in MyCards)
        {
            result.CardsName.Add(item.gameObject.name);
        }
        result.PlayerName = ProfileManager.instance.currentPlayer.name;
        Debug.LogError(result.PlayerName);
        result.Avatarindex = ProfileManager.instance.currentPlayer.selectedFrameIndex;
        //Combining both in json and sending it
        string seq = JsonUtility.ToJson(result);
        Debug.LogError(seq);
        // Sending my things to all so that all know that I have pressed knowckout also to let them send me the same thing
        Pv.RPC("SendDataCardsAndInGameList", RpcTarget.All, seq);

    }

    [PunRPC]
    public void SendDataCardsAndInGameList(string DataForSq)
    {
        Debug.LogError(DataForSq);

        ResultDataForScore seqAns = new ResultDataForScore();
        seqAns = JsonUtility.FromJson<ResultDataForScore>(DataForSq);
        Debug.LogError(seqAns);
        resultDataForScores.Add(seqAns);

        if (recalculate == false)
        {
            Invoke(nameof(CheckForResults), 3f);
            recalculate = true;
        }
    }


    InGamePlayer Winner;
    public List<string> WinnerCards = new List<string>();
    public string WinnerPoints;
    public string WinnerName;
    public int winnerAvatarindex;
    public List<ResultDataForScore> loosers = new List<ResultDataForScore>();
    public List<string> looserPoint = new List<string>();


    private void CheckForResults()
    {
        Debug.LogError("Method running");
        foreach (var item in resultDataForScores)
        {
            Debug.LogError(item.Player.ID);
            bool winnerBool = item.Player.cards.All(x => x.matched == true);
            if (winnerBool)
            {
                Winner = item.Player;
                WinnerCards = item.CardsName;
                WinnerName = item.PlayerName;
                winnerAvatarindex = item.Avatarindex;
            }
            else
            {
                loosers.Add(item);
            }
        }


        foreach (var item in WinnerCards)
        {
            // checking for same cards
            //if(WinnerCards.All(x=>x.ToLower().Contains("spade"))
            //    || WinnerCards.All(x => x.ToLower().Contains("club"))
            //    || WinnerCards.All(x => x.ToLower().Contains("heart"))
            //    || WinnerCards.All(x => x.ToLower().Contains("diamond"))

            //    &&
            if (
                WinnerCards.Any(x => x.ToLower().Contains("1"))
                )
            {
                Debug.LogError("this is minus Hand");
                WinnerPoints = "15";

            }
            // Go for next Conditions

            else if (WinnerCards.All(x => x.ToLower().Contains("spade"))
                || WinnerCards.All(x => x.ToLower().Contains("club"))
                || WinnerCards.All(x => x.ToLower().Contains("heart"))
                || WinnerCards.All(x => x.ToLower().Contains("diamond"))
                )
            {
                Debug.LogError("this is Golden Hand Hand");
                Debug.LogError("User gets 20 points");
                WinnerPoints = "20";

            }
            else
            {
                Debug.LogError("user gets 10 points");
                WinnerPoints = "10";

            }

        }

        int looserscore = 0;
        int cardPoints = 0;
        string loosername;
        foreach (var item in loosers)
        {
            List<string> _loosepoint = item.CardsName;
            looserscore = 0;
            cardPoints = 0;
            loosername = null;
            foreach (var point in _loosepoint)
            {
                string[] pointsAndName = point.Split(' ');
                cardPoints = int.Parse(pointsAndName[1]);
                if (cardPoints >= 10)
                {
                    cardPoints = 10;
                }

                looserscore = looserscore + cardPoints;
                Debug.LogError("Looser" + item.Player.ID + " get the score of" + looserscore);
            }
            looserPoint.Add(item.Player.ID + "-" + looserscore.ToString() + "-" + item.PlayerName + "-" + item.Avatarindex);
        }
        foreach (var item in looserPoint)
        {
            if (item.Contains(ProfileManager.instance.currentPlayer.id))
            {
                Debug.LogError(looser+"looser");
                looser = true;
            }
            else
            {
                looser = false;
            }
        }
        

        DisplayResult();
    }

    private void DisplayResult()
    {
        foreach (var item in looserPoint)
        {
            LooserPrefab _looserPrefab = Instantiate(LooserPrefab, LooserParent);
            // id-score-name
            string[] data = item.Split('-');
            Debug.LogError(data[3] + "Avatar number");
            _looserPrefab.SetData(data[2], data[1], int.Parse(data[3]));
        }
        if (WinnerCards.Count > 0)
        {
            WinnerPrefab winnerPrefab = Instantiate(WinnerPrefab, WinnerParent);
            winnerPrefab.SetData(WinnerName, WinnerPoints, winnerAvatarindex);
        }

        // if looser contains my id looser ribbon true else winner ribbon true
        if (Winner != null)
        {
            if (Winner.ID == ProfileManager.instance.currentPlayer.id)
            {
                ResultScreen.SetImage(1);
            }
            else
            {
                ResultScreen.SetImage(0);
            }
        }
        //else if (PhotonNetwork.CurrentRoom.PlayerCount < 2)
        {
            DisplayDummyData();
        }
        ResultScreen.gameObject.SetActive(true);
    }

    private void DisplayDummyData()
    {
        DisplayWinner();
        DisplayLooser();
    }
    string[] dummynames = new string[] { "Peter", "John" };
    string[] dummyScores = new string[] { "-30", "-45", "-31", "-40", "-32", "-5" };
    int[] dummyImages = new int[] { 0, 1, 2, 3, 4 };

    private void DisplayLooser()
    {
        if (Winner == null)
        {
            for (int i = 0; i < 3-looserPoint.Count; i++)
            {
                LooserPrefab looserPrefab = Instantiate(LooserPrefab, LooserParent);
                looserPrefab.SetData(dummynames[i],
                    dummyScores[UnityEngine.Random.Range(0, dummyScores.Length - 1)],
                    dummyImages[0]);
            }
        }
        else
        {
            for (int i = 0; i < 3; i++)
            {
                LooserPrefab looserPrefab = Instantiate(LooserPrefab, LooserParent);
                looserPrefab.SetData(dummynames[UnityEngine.Random.Range(0, dummynames.Length - 1)],
                    dummyScores[UnityEngine.Random.Range(0, dummyScores.Length - 1)],
                    dummyImages[UnityEngine.Random.Range(0, dummyImages.Length - 1)]);
            }
        }
        DisplayResultScreen();
    }

    private void DisplayWinner()
    {
        if (Winner == null)
        {
            WinnerPrefab winner = Instantiate(WinnerPrefab, WinnerParent);
            winner.SetData("David",
                "30",
                1);
        }
        DisplayResultScreen();
    }

    public void DisplayResultScreen()
    {
        ResultScreen.gameObject.SetActive(true);
    }
}



[Serializable]
public class ResultDataForScore
{
    [SerializeField]
    public List<string> CardsName = new List<string>();
    public InGamePlayer Player;
    public string PlayerName;
    public int Avatarindex;
}
