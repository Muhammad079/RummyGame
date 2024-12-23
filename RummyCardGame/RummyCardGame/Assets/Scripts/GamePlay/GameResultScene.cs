using Photon.Pun;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameResultScene : SceneLoader
{
    public GameObject Loading_Panel;
    public GameObject[] Timers;

    [SerializeField] private GameObject winnerBadge = null, loserBadge = null;
    [SerializeField] private Text xpText = null, levelText = null;
    [SerializeField] private Image fillingBar = null;
    [SerializeField] private Transform prizeGrid = null;
    [SerializeField] private GameObject prizeObject = null;
    [SerializeField] private Sprite goldenCard = null, coins = null, trophies = null;
    [SerializeField] private Sprite[] boxReward_Sprite = null;
    private int xpDataIndex;
    int winCoins = 0;

    void Start()
    {

    }
    public void GameWin(int coinsWin)
    {
        this.gameObject.SetActive(true);
        winCoins = coinsWin;
        if (GameManager.instance.selectedTournament != TournamentType.empty)
            TournamentWin();
        else
            SimplePlayWin(coinsWin);
    }
    public void GameLoss()
    {

        for (int i = 0; i < Timers.Length; i++)
        {
            if (i == 0)
            {
                Timers[i].SetActive(false);
            }
            else
            {
                if (Timers[i].transform.childCount > 0)
                {
                    if (Timers[i].transform.GetChild(0).childCount > 0)
                    {
                        Timers[i].transform.GetChild(0).GetChild(0).GetChild(1).gameObject.SetActive(false);

                    }
                }
            }
        }


        this.gameObject.SetActive(true);
        GameManager.instance.replayed = false;


        if (GameManager.instance.selectedTournament == TournamentType.twoMEvents)
        {
            ProfileManager.instance.currentPlayer.TwoM_Loses++;
        }
        if (ProfileManager.instance.currentPlayer.TwoHK_Selected)
        {
            ProfileManager.instance.currentPlayer.TwoHK_LostTable_ID.Add(ProfileManager.instance.currentPlayer.TwoHK_Selected_TableID);
            ProfileManager.instance.currentPlayer.TwoHK_Selected_TableID = 0;
            //ProfileManager.instance.SaveUserData();
            ProfileManager.instance.currentPlayer.TwoHK_Loses++;
            //ProfileManager.instance.currentPlayer.TwoHK_Lost = true;
        }
        if (GameManager.instance.selectedTournament == TournamentType.normalEvents)
        {
            ProfileManager.instance.currentPlayer.normalTournamentLoses++;
        }
        ProfileManager.instance.SaveUserData();
        QuitReplayPanel();
    }
    public override void Update()
    {
        base.Update();
    }
    void QuitReplayPanel()
    {
        int n = 0;

        if (GameManager.instance.selectedTournament == TournamentType.twoMEvents)
        {
            GameManager.instance.selectedTournament = TournamentType.empty;
        }
        else if (PhotonNetwork.CurrentRoom.Name.Contains("roomvip") || PhotonNetwork.CurrentRoom.Name.Contains("tutorial"))
        {
            if (!Gameplay_Manager.instance.VIP_GameStarted)
            {
                StartCoroutine(OnClick());
                //Loading_Panel.SetActive(true);
                //PhotonNetwork.LeaveRoom();
                //SceneManager.LoadScene("Home");
            }
        }
        else
        {
            while (GameManager.instance.selectedBid != GameManager.instance.selectedXPData.data[n].coinsBid)
            {
                n++;
            }
        }
        PhotonNetwork.LeaveRoom();


        xpDataIndex = n;
        ShowLossReward(GameManager.instance.selectedXPData.data[n]);
        ProfileManager.instance.currentPlayer.lossToday++;
        ProfileManager.instance.currentPlayer.totalLoss++;
        ProfileManager.instance.currentPlayer.consectiveWins = 0;
        
        //ProfileManager.instance.currentPlayer.coins -= GameManager.instance.selectedBid;
        ProfileManager.instance.currentPlayer.xp += GameManager.instance.selectedXPData.data[xpDataIndex].xpOnLoss;
        ProfileManager.instance.currentPlayer.trophies -= GameManager.instance.selectedXPData.data[xpDataIndex].trophiesOnLoss;
        if (ProfileManager.instance.currentPlayer.trophies < 2)
            ProfileManager.instance.currentPlayer.trophies = 2;
        ProfileManager.instance.SaveUserData();
        //     UnityEngine.SceneManagement.SceneManager.LoadScene("Home");
    }
    public bool Replayable()
    {
        if (!GameManager.instance.replayed)
        {
            GameManager.instance.replayed = true;
            return true;
        }
        else
        {
            return false;
        }
    }
    void TournamentWin()
    {
        if (GameManager.instance.selectedTournament == TournamentType.normalEvents)
        {
            ProfileManager.instance.currentPlayer.normalTournamentWins++;
        }
        else if (GameManager.instance.selectedTournament == TournamentType.twoMEvents)
        {
            ProfileManager.instance.currentPlayer.TwoM_Wins++;
        }
        ProfileManager.instance.currentPlayer.weeklyWins++;
        ProfileManager.instance.currentPlayer.winsToday++;
        ProfileManager.instance.currentPlayer.totalWins++;
        ProfileManager.instance.currentPlayer.consectiveWins++;
        ProfileManager.instance.currentPlayer.tournamentXp += 10;
        ProfileManager.instance.SaveUserData();
        ShowWinRewards(GameManager.instance.selectedXPData.data[0]);
    }
    void SimplePlayWin(int coinsWin)
    {
        if (!ProfileManager.instance.currentPlayer.TwoHK_Selected)
        {
            Debug.LogError("Win called");
            int n = 0;
            while (GameManager.instance.selectedBid != GameManager.instance.selectedXPData.data[n].coinsBid)
            {
                n++;
            }
            ShowWinRewards(GameManager.instance.selectedXPData.data[n]);
            GameManager.instance.TodaysWin++;
            ProfileManager.instance.currentPlayer.winsToday++;
            ProfileManager.instance.currentPlayer.weeklyWins++;
            ProfileManager.instance.currentPlayer.totalWins++;
            ProfileManager.instance.currentPlayer.coins += coinsWin;
            ProfileManager.instance.currentPlayer.xp += GameManager.instance.selectedXPData.data[n].xpOnWin;
            ProfileManager.instance.currentPlayer.goldenCards += GameManager.instance.selectedXPData.data[n].goldenCards;
            ProfileManager.instance.currentPlayer.trophies += GameManager.instance.selectedXPData.data[n].trophiesOnWin;
            ProfileManager.instance.currentPlayer.trophies = Mathf.Clamp(ProfileManager.instance.currentPlayer.trophies, 0, GameManager.instance.selectedXPData.data[n].maxTrophiesWin);
            //if (ProfileManager.instance.currentPlayer.boxRewardInQue.Count < 3)
            //    ProfileManager.instance.currentPlayer.boxRewardInQue.Add(GameManager.instance.XpData.data[n].winReward);
            ProfileManager.instance.currentPlayer.consectiveWins++;
            ProfileManager.instance.SaveUserData();
            GameManager.instance.replayed = false;
            //      UnityEngine.SceneManagement.SceneManager.LoadScene("Home");
        }
        else
        {
            Debug.LogError("Win called");
            int n = 0;
            while (GameManager.instance.selectedBid != GameManager.instance.selectedXPData.data[n].coinsBid)
            {
                n++;
            }
            ShowWinRewards(GameManager.instance.selectedXPData.data[n]);
            GameManager.instance.TodaysWin++;
            ProfileManager.instance.currentPlayer.winsToday++;
            ProfileManager.instance.currentPlayer.weeklyWins++;
            ProfileManager.instance.currentPlayer.totalWins++;
            ProfileManager.instance.currentPlayer.coins += coinsWin;
            ProfileManager.instance.currentPlayer.xp += GameManager.instance.selectedXPData.data[n].xpOnWin;
            ProfileManager.instance.currentPlayer.goldenCards += GameManager.instance.selectedXPData.data[n].goldenCards;
            ProfileManager.instance.currentPlayer.trophies += GameManager.instance.selectedXPData.data[n].trophiesOnWin;
            ProfileManager.instance.currentPlayer.trophies = Mathf.Clamp(ProfileManager.instance.currentPlayer.trophies, 0, GameManager.instance.selectedXPData.data[n].maxTrophiesWin);
            //if (ProfileManager.instance.currentPlayer.boxRewardInQue.Count < 3)
            //    ProfileManager.instance.currentPlayer.boxRewardInQue.Add(GameManager.instance.XpData.data[n].winReward);
            ProfileManager.instance.currentPlayer.consectiveWins++;



            ProfileManager.instance.currentPlayer.TwoHK_Wins++;
            ProfileManager.instance.SaveUserData();
            GameManager.instance.replayed = false;
            //SceneManager.LoadScene("Home");
        }

    }
    private void ShowLossReward(XPData data)
    {
        ProfileManager.instance.currentPlayer.consectiveWins = 0;
        Instantiate(prizeObject, prizeGrid).GetComponent<PrizeObject>().DisplayReward(trophies, data.trophiesOnLoss);
        winnerBadge.SetActive(false);
        loserBadge.SetActive(true);
        xpText.text = "+" + data.xpOnLoss.ToString();
        levelText.text = /*"+" + */ProfileManager.instance.currentPlayer.level.ToString();
        //fillingBar.fillAmount = (float)ProfileManager.instance.currentPlayer.xp / (float)GameManager.instance.LevelUpData.levelsData[ProfileManager.instance.currentPlayer.level].xpReq;
        StartCoroutine(Filling_Bar());
    }
    private void ShowWinRewards(XPData data)
    {
        var Coin_Reward = Instantiate(prizeObject, prizeGrid);
        Coin_Reward.GetComponent<PrizeObject>().DisplayReward(coins, winCoins);
        Result_Panel_animation_Controller.Instantiated_Rewards[0] = Coin_Reward;
        Result_Panel_animation_Controller.coins_Won = winCoins;

        var Trophy_Rewardi = Instantiate(prizeObject, prizeGrid);
        Trophy_Rewardi.GetComponent<PrizeObject>().DisplayReward(trophies, data.trophiesOnWin);
        Result_Panel_animation_Controller.Instantiated_Rewards[1] = Trophy_Rewardi;


        int n = ProfileManager.instance.currentPlayer.consectiveWins;
        //n = Mathf.Clamp(n, 0, 2);
        //if (n < data.positionRewards.Count)
        if (n % 2 == 0)
        {

            if (ProfileManager.instance.currentPlayer.boxRewardInQue.Count < ProfileManager.instance.currentPlayer.Box_Slots_unlocked)
            {
                var box = data.positionRewards[0].boxReward;
                //BoxRewards box = new BoxRewards();
                int range = Random.Range(0, 2);
                if (range == 0)
                {
                    box.boxType = BoxType.bronze;
                    var Box = Instantiate(prizeObject, prizeGrid);
                    box.boxTitle = "Bronze Box";
                    box.unlockTimer = 7200;
                    BoxItems boxItems = new BoxItems();
                    boxItems.reward = RewardType.coins;
                    boxItems.quantity = 1000;

                    box.boxItems.Add(boxItems);
                    Box.GetComponent<PrizeObject>().DisplayReward(boxReward_Sprite[0], data.positionRewards[0].quantity);
                    Result_Panel_animation_Controller.Instantiated_Rewards[2] = Box;
                }
                else if (range == 1)
                {
                    box.boxType = BoxType.silver;
                    var Box = Instantiate(prizeObject, prizeGrid);
                    box.boxTitle = "Silver Box";
                    box.unlockTimer = 9200;
                    BoxItems boxItems = new BoxItems();
                    boxItems.reward = RewardType.coins;
                    boxItems.quantity = 2000;

                    box.boxItems.Add(boxItems);
                    Box.GetComponent<PrizeObject>().DisplayReward(boxReward_Sprite[1], data.positionRewards[0].quantity);
                    Result_Panel_animation_Controller.Instantiated_Rewards[2] = Box;
                }
                else if (range == 2)
                {
                    box.boxType = BoxType.golden;
                    var Box = Instantiate(prizeObject, prizeGrid);
                    box.boxTitle = "Golden Box";
                    box.unlockTimer = 11200;
                    BoxItems boxItems = new BoxItems();
                    boxItems.reward = RewardType.coins;
                    boxItems.quantity = 3500;

                    box.boxItems.Add(boxItems);
                    Box.GetComponent<PrizeObject>().DisplayReward(boxReward_Sprite[2], data.positionRewards[0].quantity);
                    Result_Panel_animation_Controller.Instantiated_Rewards[2] = Box;
                }
                //ProfileManager.instance.currentPlayer.boxRewardInQue.Add(data.positionRewards[n].boxReward);
                ProfileManager.instance.currentPlayer.boxRewardInQue.Add(box);
            }
            //Instantiate(prizeObject, prizeGrid).GetComponent<PrizeObject>().DisplayReward(boxReward, data.positionRewards[n].quantity);
        }
        if (data.goldenCards > 0)
        {
            var Golden_Cards = Instantiate(prizeObject, prizeGrid);
            Golden_Cards.GetComponent<PrizeObject>().DisplayReward(goldenCard, data.goldenCards);
            Result_Panel_animation_Controller.Instantiated_Rewards[3] = Golden_Cards;
        }
        winnerBadge.SetActive(true);
        loserBadge.SetActive(false);
        xpText.text = "+" + data.xpOnWin.ToString();
        levelText.text = /*"+" + */ProfileManager.instance.currentPlayer.level.ToString();

        StartCoroutine(Filling_Bar());


    }
    IEnumerator Filling_Bar()
    {
        Debug.Log("Filling bar started");
        yield return new WaitForSeconds(2.6f);
        Debug.Log("Filling bar Now");
        //fillingBar.fillAmount = (float)ProfileManager.instance.currentPlayer.xp / (float)GameManager.instance.LevelUpData.levelsData[ProfileManager.instance.currentPlayer.level].xpReq;
        float Point = (float)ProfileManager.instance.currentPlayer.xp / (float)GameManager.instance.LevelUpData.levelsData[ProfileManager.instance.currentPlayer.level].xpReq;


        //Debug.Log("Xp Earned: " + Point);
        //while (Point > 0)
        //{
        //    Point -= Time.deltaTime;
        //    fillingBar.fillAmount += Point;
        //    yield return new WaitForSeconds(0.05f);
        //}

        float result = 0;
        while (result < Point)
        {
            result += Time.deltaTime;
            fillingBar.fillAmount = result;
            yield return new WaitForSeconds(0.05f);
        }

        //float result = 1-Point, initial=0;
        //while (initial < result)
        //{
        //    initial += Time.deltaTime;
        //    fillingBar.fillAmount = initial;
        //    yield return new WaitForSeconds(0.05f);
        //}
    }
}
