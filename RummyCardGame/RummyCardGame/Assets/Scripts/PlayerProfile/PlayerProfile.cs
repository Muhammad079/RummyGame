using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class PlayerProfile
{
    #region General Data
    [Header("General Data")]
    public string id = "";
    public string name = "";
    public Sprite profilePicture = null;
    public Sprite FbPicture;
    public string gender = "male";
    public int xp = 0;
    public int coins = 40000;
    public int level = 0;
    public int gems = 50;
    public int trophies = 2;
    public int goldenCards = 0;
    public int totalLoss = 0;
    public int totalWins = 0;
    public int gamesOn50KTable = 0;
    public int winsOn100KTable = 0;
    public int winsOn50KTable = 0;
    public int gamesOn100KTable = 0;
    public int currentLeague = 0;
    public string leagueCheck = "";
    public int consectiveWins = 0;
    public string country="China";

    public string ProfilePictureString_FB;

    #endregion


    public List<string> FBfriends_list = new List<string>(); 


    #region TwoM Event Data
    [Header("TwoM_Event_Data")]

    public int TwoM_Wins = 0;
    public int TwoM_Loses = 0;
    public bool TwoM_FeeCheck = false;
    public bool TwoM_Claim_Reward = false;


    #endregion

    #region TwoHK Event Data
    [Header("TwoHK_Event_Data")]
    public bool TwoHK_Selected = false;
    public int TwoHK_Table_Selected = 0;
    public int TwoHK_Selected_TableID = 0;
    public int TwoHK_Crowns = 0;
    public int TwoHK_Wins = 0;
    public int TwoHK_Loses = 0;
    //public bool TwoHK_Lost = false;
    //public bool TwoHK_FeeCheck = false;
    public TwoHK_Gems_Change GetTwoHK_Gems_Change = new TwoHK_Gems_Change();
    public List<int> TwoHK_FeeDeducted_ID = new List<int>();
    public List<int> TwoHK_Claim_Reward_ID = new List<int>();
    
    public List<int> TwoHK_LostTable_ID = new List<int>();

    #endregion



    [Header("Trophy_Rewards_Data")]
    public List<int> Reward_ID_container = new List<int>();

    [Header("Audio_Manager_Data")]
    public int Music_Volume = 10;
    public bool isMusicActive = false; // Range(0,10) //true = not active
    public int Sound_Effects_Volume = 10;
    public bool isSoundActive = false; // Range(0,10) //true = not active

    [Header("First Time Check")]
    public bool First_Time_Login= true;


    #region Daily Based Data
    [Header("Daily based data")]
    public int winsToday = 0;
    public int lossToday = 0;
    public int completedDailyQuests = 0;
    public int lastDailyRewardCollected = -1;
    public int displayingDailyQuests = 0;
    public int boxesOpenedToday = 0;
    public int luckySpinsToday = 0;
    #endregion



    #region Lucky Spin
    [Header("Lucky Spin")]
    public int spinCounts = 1;
    public int goldenSpinCounts = 0;
    #endregion



    #region Hand Pass
    [Header("Hand Pass")]
    public int claimedFreePassReward = 0;
    public int claimedPremiumPassReward = 0;
    public bool Hand_Pass_purchase_Check = false;
    public int HandPass_Duration_Days;
    public int HandPass_StartDate=0;
    public bool HandPass_First_Login = false;
    #endregion



    #region Friend List and Requests
    [Header("Friend list and Requests")]
    public List<string> friends = new List<string>();
    public List<string> friendReq = new List<string>();
    public List<string> pendingReq = new List<string>();
    #endregion



    #region Lucky Box Reward
    [Header("Lucky box reward")]
    public int Box_Slots_unlocked = 0;
    public List<BoxRewards> boxRewardInQue = new List<BoxRewards>();
    #endregion



    #region Gifts
    [Header("Gifts Data")]
    public int sendGifts = 0;
    public int charmXp = 0, charmXp_Weekly=0, charmXp_Monthly=0;
    public List<DbGifts> Sent_Gifts = new List<DbGifts>();
    public List<DbGifts> recievedGifts =new List<DbGifts>();
    #endregion



    #region Tournaments
    [Header("Tournaments")]
    public int tournamentLevel = 0;
    public int normalTournamentWins = 0;
    //public int twoMTournamentWins = 0;
    //public int twoHundredKTournamentWins = 0;
    public int normalTournamentLoses = 0;
    //public int twoMTournamentLoses = 0;
    //public int twoHundredKTournamentLoses = 0;
    public int tournamentXp = 0;
    public bool tournamentFeeCheck = false;



    #region Selected_Tournament_Handling   
    public bool first_try_M = true, first_try_K = true, first_try_R = true;
    public int Two_M_Count = 0, Two_K_Count = 0, R_Count = 0;
    public int Gems_deduction_K = 5, Gems_deduction_R = 12;
    public int /*Level_Display_Counter_M=0, Level_Display_Counter_K = 0,*/ Level_Display_Counter_R = 0;
    public int Regular_Tour_Lvlup_Coins = 0;



    #endregion



    #endregion


    #region Cards Collection
    [Header("Cards Collection")]
    public List<DbCardCollection> cardsCollection;
    #endregion

    #region Frames and Avatars
    [Header("Cards Collection")]
    public List<DbFrames> frames;
    public List<DbAvatar> avatars;
    public int selectedAvatarIndex = 0;
    public int selectedFrameIndex = 0;
    #endregion


    public List<Invited_Tables_Data> Tables_VIP_invited = new List<Invited_Tables_Data>();
    public List<Invited_Tables_Data> Tables_VIPFriends_invited = new List<Invited_Tables_Data>();

    public int weeklyWins = 0;
    public int badges = 0;
    public bool isOnline = false;
    public int consectiveLogin = 0;
    public string lastLogin = "";
    public bool facebookLogin = false;
    public int invitedFriends = 0;
    public bool isVip = false;
    public int totalPurchases = 0;
    public int VIP_7days_Start_Day = 0, VIP_30days_Start_Day = 0;
    public int VIP_7days_count = 0, VIP_30days_count;
    public bool Vip30_Rename = false;
    public int vipXp = 0;
    public int VIP_Level = 1;



    #region Events_Start_Dates
    [Header("Events_Start_Dates")]
    public int TwoM_Start_day = 200;
    public int TwoM_Duration = 30;

    public int TwoHK_Start_day = 200;
    public int TwoHK_Duration = 30;

    #endregion

}