using DG.Tweening;
using Photon.Pun;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;
using UnityEngine.UI;






public class GameManager : MonoBehaviour
{
    public List<string> friends_list = new List<string>();
    public GameObject Not_Enough_C_Panel;
    public GameObject No_Internet_Panel;

    public Scriptable_4Medals scriptable_4Medals;
    public scriptable_VIP_information VIP_Information;
    public Sprite[] VIP_Levels;
    public  DateTime currentTime;
    public static GameManager instance;
    private void Awake()
    {
#if UNITY_EDITOR
        Debug.unityLogger.logEnabled = true;
#else
             Debug.unityLogger.logEnabled=false;
#endif

        

        if (instance == null)
            instance = this;
        else
            Destroy(this.gameObject);
    }
    [SerializeField] private Scriptable_LevelData levelUpData;
    public Scriptable_XPData xpData;
    public Scriptable_XPData xpData_2Players;
    public Scriptable_LevelData LevelUpData { get => levelUpData; private set => levelUpData = value; }
    public Scriptable_XPData selectedXPData = null;
    public int TodaysWin { get => PlayerPrefs.GetInt(ProfileManager.instance.currentPlayer.name + "Today'sWin"); set => PlayerPrefs.SetInt(ProfileManager.instance.currentPlayer.name + "Today'sWin", value); }
    public int TodaysLoss { get => PlayerPrefs.GetInt(ProfileManager.instance.currentPlayer.name + "Today'sLoss"); set => PlayerPrefs.SetInt(ProfileManager.instance.currentPlayer.name + "Today'sLoss", value); }
    public int selectedBid = 0;
    public TournamentType selectedTournament = TournamentType.empty;
    public bool hasWon = false;
    internal Table selectedTable;
    public int totalBid = 0;
    internal int replayGemCost = 0;
    int xpDataIndex = 0;
    public bool replayed = false;
    public bool internet_Check;
    public Card selectedCollectionCard;
    public Scriptable_TournamentXP tournamentMedalData;
    public User_Profile_Handler userProfileHandler = null; public PlayerProfile User_Profile;
    public Scriptable_Leagues leaguesFile = null;
    public Scriptable_Gifts giftsDataFile = null;
    public Language selectedLanguage;
    internal string sceneToLoad;
    internal BoxRewards boxReward;
    public Scriptable_CardCollectionData cardsCollection;
    [Header("Reward Images Icon")]
    public Sprite r_GoldenCard = null;
    public Sprite r_SilverCard = null, r_WoodenCard = null, r_Coins = null, r_Gems = null, r_Xp = null, r_Badges = null;
    bool reset;
    public List<Sprite> countriesFlags = new List<Sprite>();
    public Dictionary<Sprite, string> flagData = new Dictionary<Sprite, string>();
    public bool reConnecting;
    IEnumerator checkInternetConnection(Action<bool> action)
    {
        UnityWebRequest www = new UnityWebRequest("http://google.com");
        yield return www.SendWebRequest();
        //  Debug.Log("Coroutine Body started");
        float n = 5;
        yield return new WaitForSeconds(n);
        if (www.isNetworkError)
        {
            action(false);
            //      Debug.Log("Coroutine Body FALSE_PART started");
        }
        else
        {
            action(true);
            //      Debug.Log("Coroutine Body TRUE_PART started");
            //      Debug.Log("WWW contains: " + www.error);
        }
        internet_Check = true;
    }

    void Update()
    {
        if (!SceneManager.GetActiveScene().name.Contains("Login"))
        {
            if (ProfileManager.instance.currentPlayer.trophies <= 1)
            {
                ProfileManager.instance.currentPlayer.trophies = 2;
            }
        }

        if (SceneManager.GetActiveScene().name.Contains("Login"))
        {
            reset = false;
        }
        if (SceneManager.GetActiveScene().name.Contains("Home") && !reset)
        {
            reset = true;
            

            Invoke(nameof(DailyBasisGameData), 5);
        }
    }


    void Start()
    {
        

        internet_Check = true;
        Debug.Log("Daily reset starting");
        //DailyBasisGameData();

        //Invoke(nameof(DailyBasisGameData), 2);
        StartCoroutine(nameof(GetInternetTime));
        DontDestroyOnLoad(this.gameObject);
        InvokeRepeating(nameof(internet_Checking), 1,1);

    }

    private void internet_Checking()
    {
        if(internet_Check)
        {
            reConnecting = false;
            if (!SceneManager.GetActiveScene().name.Contains("Login"))
            {
                if (Application.internetReachability == NetworkReachability.NotReachable)
                {
                    internet_Check = false;
                   
                    if (SceneManager.GetActiveScene().name.Contains("GamePlay"))
                    {
                        No_Internet_Panel.SetActive(true);
                        No_Internet_Panel.transform.DOScale(new Vector3(3, 3, 3), 0.5f).SetEase(Ease.OutBounce);
                    }
                    else
                    {
                        No_Internet_Panel.SetActive(true);
                        No_Internet_Panel.transform.DOScale(new Vector3(1, 1, 1), 0.5f).SetEase(Ease.OutBounce);
                    }
                }
            }
        }
    }

    void DailyBasisGameData()
    {
        if (ProfileManager.instance.currentPlayer.isVip)
        {
            ProfileManager.instance.currentPlayer.VIP_Level = 1;
            for (int i = 0; i < VIP_Information.VIPs.Count; i++)
            {
                if (ProfileManager.instance.currentPlayer.vipXp >= VIP_Information.VIPs[i].VIP_Points)
                {
                    ProfileManager.instance.currentPlayer.VIP_Level++;
                }
            }
        }
        


        Debug.Log("Daily reset started");
        Debug.Log("Todays data when started: " + PlayerPrefs.GetInt("Today'sData"));
        Debug.Log("Todays key available?: " + PlayerPrefs.HasKey("Today'sData"));
        Debug.Log("Todays refreshed data: " + DateTime.UtcNow.Day);
        //if (PlayerPrefs.GetInt("Today'sData") != DateTime.UtcNow.Day)
        //{
        //    Debug.Log("Todays data: " + PlayerPrefs.GetInt("Today'sData"));
        //    ProfileManager.instance.currentPlayer.lossToday *= 0;
        //    ProfileManager.instance.currentPlayer.winsToday *= 0;
        //    ProfileManager.instance.currentPlayer.boxesOpenedToday *= 0;
        //    ProfileManager.instance.currentPlayer.luckySpinsToday *= 0;
        //    Debug.Log("Todays Loss: " + ProfileManager.instance.currentPlayer.lossToday);
        //    Debug.Log("Todays Win: " + ProfileManager.instance.currentPlayer.winsToday);
        //    Debug.Log("Todays Box: " + ProfileManager.instance.currentPlayer.boxesOpenedToday);
        //    Debug.Log("Todays Spin: " + ProfileManager.instance.currentPlayer.luckySpinsToday);
        //    ProfileManager.instance.SaveUserData();

        //}
        if (PlayerPrefs.HasKey("Today'sData"))
        {
            if (PlayerPrefs.GetInt("Today'sData") != DateTime.UtcNow.Day)
            {
                Debug.Log("Todays data: " + PlayerPrefs.GetInt("Today'sData"));
                ProfileManager.instance.currentPlayer.lossToday = 0;
                ProfileManager.instance.currentPlayer.winsToday = 0;
                ProfileManager.instance.currentPlayer.boxesOpenedToday = 0;
                ProfileManager.instance.currentPlayer.luckySpinsToday = 0;
                Debug.Log("Todays Loss: " + ProfileManager.instance.currentPlayer.lossToday);
                Debug.Log("Todays Win: " + ProfileManager.instance.currentPlayer.winsToday);
                Debug.Log("Todays Box: " + ProfileManager.instance.currentPlayer.boxesOpenedToday);
                Debug.Log("Todays Spin: " + ProfileManager.instance.currentPlayer.luckySpinsToday);
                ProfileManager.instance.SaveUserData();

            }
        }
        else
        {

            PlayerPrefs.SetInt("Today'sData", DateTime.UtcNow.Day);
            Debug.Log("Todays data2: " + PlayerPrefs.GetInt("Today'sData"));
            ProfileManager.instance.currentPlayer.lossToday = 0;
            ProfileManager.instance.currentPlayer.winsToday = 0;
            ProfileManager.instance.currentPlayer.boxesOpenedToday = 0;
            ProfileManager.instance.currentPlayer.luckySpinsToday = 0;
            Debug.Log("Todays Loss2: " + ProfileManager.instance.currentPlayer.lossToday);
            Debug.Log("Todays Win2: " + ProfileManager.instance.currentPlayer.winsToday);
            Debug.Log("Todays Box2: " + ProfileManager.instance.currentPlayer.boxesOpenedToday);
            Debug.Log("Todays Spin2: " + ProfileManager.instance.currentPlayer.luckySpinsToday);
            ProfileManager.instance.SaveUserData();
        }

        TodaysLoss = ProfileManager.instance.currentPlayer.lossToday;
        TodaysWin = ProfileManager.instance.currentPlayer.winsToday;

        //if (PlayerPrefs.HasKey("Today'sData"))
        //    if (PlayerPrefs.GetInt("Today'sData") == DateTime.UtcNow.Day)
        //        LoadDataFromPrefs();
        //    else
        //        SaveDataToPrefs();
        //else
        //    SaveDataToPrefs();
    }
    public void TableSelection(int bid)
    {
        selectedBid = bid;
        totalBid = bid * selectedTable.totalPlayers;
    }
    public Sprite FlagData(string countryName)
    {
        Sprite s = null;
        foreach (var a in countriesFlags)
        {
            a.name = a.name.Replace("flag-of-", "");
            if (countryName == a.name)
            {
                s = a;
            }
        }
        return s;
    }
    public Sprite FlagData(int index)
    {

        return countriesFlags[index];
    }
    public IEnumerator GetInternetTime()
    {
        Debug.LogError("Getting Time");
       UnityWebRequest myHttpWebRequest = UnityWebRequest.Get("http://www.microsoft.com");
        yield return myHttpWebRequest.SendWebRequest();

        string netTime = myHttpWebRequest.GetResponseHeader("date");
        currentTime = System.DateTime.Parse(netTime);
        Debug.LogError(netTime + " was response");
    }
}
[Serializable]
public enum TournamentType
{
    twoMEvents, twoHundredKEvents, normalEvents,
    empty
}