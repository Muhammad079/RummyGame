//using GameAnalyticsSDK;
using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
public class ProfileManager : MonoBehaviour
{
    public bool Test_Data;
    public static ProfileManager instance;
    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(this.gameObject);
    }
    public ScriptableAvatars avatarDataFile = null;
    public ScriptableFrames framesDataFile = null;
    public PlayerProfile currentPlayer;
    private void Start()
    {
   //     GameAnalytics.SetEnabledManualSessionHandling(true);
        DontDestroyOnLoad(this.gameObject);
    }
    public void SetCurrentPlayer(PlayerProfile player)
    {

        currentPlayer = player;
        currentPlayer.isOnline = true;
        PlayerPrefs.SetString("UserId", currentPlayer.id);
        Debug.Log("calling for image");
        System.DateTime login = System.DateTime.Now;
        if (player.lastLogin != "")
            login = System.DateTime.Parse(player.lastLogin);
        if (login != System.DateTime.Now)
        {
            if (login.AddDays(1) == System.DateTime.Now)
            {
                player.consectiveLogin++;
                player.lastLogin = System.DateTime.Now.ToString();
            }
            else
                player.consectiveLogin = 0;
        }
        if(Test_Data)
        {
            update_Test_CurrentProfile();
        }
    }
    public void update_Test_CurrentProfile()
    {
        Debug.Log("SETTING TEST DATA");
        string Name_Edit="";
        for (int i = 0; i < currentPlayer.id.Length; i++)
        {
            if(char.IsDigit(currentPlayer.id[i]))
            {
                Name_Edit += currentPlayer.id[i];
            }
        }
        currentPlayer.name = "Test_" + Name_Edit;
        currentPlayer.level = 15;
        currentPlayer.coins = 1000000;
        currentPlayer.gems = 50000;
        currentPlayer.Vip30_Rename = true;
        currentPlayer.isVip = true;
        currentPlayer.VIP_30days_Start_Day = 250;
        currentPlayer.VIP_30days_count = 10;
        currentPlayer.trophies = 2100;
        SaveUserData();
    }
    //public IEnumerator GetFBPicture(string facebookId)
    //{
    //    var www = new WWW("http://graph.facebook.com/" + facebookId + "/picture?width=720&height=720&type=square&redirect=true");
    //    yield return www;

    //    if (www.isDone)
    //    {
    //        Debug.Log("waiting" + www.bytesDownloaded);
    //        Texture2D tempPic = new Texture2D(720, 720);
    //        tempPic = www.texture;
    //        currentPlayer.profilePicture = Sprite.Create(tempPic, new Rect(0, 0, tempPic.width, tempPic.height), new Vector2());
    //    }
    //    //FB.API("https" + "://graph.facebook.com/" + facebookId.ToString() + "/picture?type=medium", HttpMethod.GET, delegate (IGraphResult result)
    //    //{
    //    //    Debug.Log("Loaded Image");
    //    //    currentPlayer.profilePicture = Sprite.Create(result.Texture, new Rect(0, 0, 720, 720), new Vector2(0.5f, 0.5f));
    //    //});
    //}
    public void LogOut()
    {
        currentPlayer = null;
        PlayerPrefs.SetInt("isLoggedIn", 0);
        PlayerPrefs.SetString("UserId", "");
        SceneManager.LoadScene("Login");
    }
    public void GetReward(RewardType reward, int quantity, BoxRewards boxReward = null)
    {
        if (reward == RewardType.goldenCards)
        {
            currentPlayer.goldenCards += quantity;

        }
        else if (reward == RewardType.coins)
        {
            currentPlayer.coins += quantity;
        }
        else if (reward == RewardType.gems)
        {
            currentPlayer.gems += quantity;
        }
        else if (reward == RewardType.xp)
        {
            currentPlayer.xp += quantity;
        }
        else if (reward == RewardType.badges)
        {
            currentPlayer.badges += quantity;
        }
        else if (reward == RewardType.x2)
        {

        }
        else if (reward == RewardType.vip)
        {
            currentPlayer.isVip = true;
            if (quantity == 7)
            {
                currentPlayer.VIP_7days_count += quantity;
                ProfileManager.instance.currentPlayer.vipXp += 400;
                if (ProfileManager.instance.currentPlayer.VIP_7days_Start_Day == 0)
                {
                    ProfileManager.instance.currentPlayer.VIP_7days_Start_Day = DateTime.Today.ToUniversalTime().DayOfYear;
                }
            }
            else if (quantity == 30)
            {
                currentPlayer.VIP_30days_count += quantity;
                ProfileManager.instance.currentPlayer.vipXp += 999;
                currentPlayer.Vip30_Rename = true;
                if (ProfileManager.instance.currentPlayer.VIP_30days_Start_Day == 0)
                {
                    ProfileManager.instance.currentPlayer.VIP_30days_Start_Day = DateTime.Today.ToUniversalTime().DayOfYear;
                }
            }
        }
        else if (reward == RewardType.cardCollection)
        {
            GameManager.instance.selectedCollectionCard.cardCount += quantity;
        }
        else if (reward == RewardType.box)
        {
            if (currentPlayer.boxRewardInQue.Count < currentPlayer.Box_Slots_unlocked)
            {
                currentPlayer.boxRewardInQue.Add(boxReward);
                MainMenuStats.instance.luckyBoxScreen.AddNewBox(boxReward);
            }
        }
        SaveUserData();
    }
    public void SaveUserData()
    {
        Debug.Log("From GameManager");
        DatabaseFunctions.SaveDataInDB(currentPlayer);
    }
    private void OnDisable()
    {
        currentPlayer.isOnline = false;
    }
}
