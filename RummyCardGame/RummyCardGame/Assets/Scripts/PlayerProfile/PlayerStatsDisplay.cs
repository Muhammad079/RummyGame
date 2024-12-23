using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerStatsDisplay : MonoBehaviour
{
    PlayerProfile player = null;
    [SerializeField] private Image profileImage = null,profileFrame=null, VIP_badge = null;
    [SerializeField] private Text playerCoins = null;
    [SerializeField] private Text playerLevel = null;
    [SerializeField] private Text playerXp = null;
    [SerializeField] private Text playerGems = null;
    [SerializeField] private Image levelFillImage = null;
    bool profileApplied = false;
    //public static bool facebook_login_check;
    void Start()
    {
        if(ProfileManager.instance.currentPlayer.isVip)
        {
            VIP_badge.gameObject.SetActive(true);
            VIP_badge.sprite = GameManager.instance.VIP_Levels[ProfileManager.instance.currentPlayer.VIP_Level - 1];
        }
        else
        {
            VIP_badge.gameObject.SetActive(false);
        }
        

        MainMenuStats.instance.playerStatArea = this.transform;
        player = ProfileManager.instance.currentPlayer;
      //  DatabaseFunctions.profileUpdated += DisplayPlayerData;
        
        if(ProfileManager.instance.currentPlayer.facebookLogin)
        {
         //   StartCoroutine(ProfileManager.instance.GetFBPicture(ProfileManager.instance.currentPlayer.id));
        }
        
        DisplayPlayerData(player);
        StatsDisplay();
    }
    void DisplayPlayerData(PlayerProfile profile)
    {
        profileImage.sprite = ProfileManager.instance.avatarDataFile.avatars[ProfileManager.instance.currentPlayer.selectedFrameIndex].avatarImage;
        ProfileManager.instance.currentPlayer.profilePicture = profileImage.sprite;
        //if (ProfileManager.instance.currentPlayer.facebookLogin == false)
        //{
        //    ProfileManager.instance.currentPlayer.profilePicture = ProfileManager.instance.avatarDataFile.avatars[ProfileManager.instance.currentPlayer.selectedAvatarIndex].avatarImage;
        //}

        profileFrame.sprite = ProfileManager.instance.framesDataFile.frames[ProfileManager.instance.currentPlayer.selectedFrameIndex].frameImage;
        player = profile;
        levelFillImage.fillAmount = FillImageValue();
        playerCoins.text = profile.coins.ToString();
        //playerLevel.text = GameManager.instance.LevelUpData.levelsData[player.level].name;
        playerLevel.text = /*"LEVEL "+*/ProfileManager.instance.currentPlayer.level.ToString();
        playerGems.text = profile.gems.ToString();
    }
    float FillImageValue()
    {
        int n = player.level;
        //while (player.xp > GameManager.instance.LevelUpData.levelsData[player.level + 1].xpReq)
        while (player.xp >= GameManager.instance.LevelUpData.levelsData[player.level].xpReq)
        {
            Debug.Log("Level up");
            player.level++;
        }
        if (n < player.level)
        {
            Debug.Log("Level up");
            MainMenuStats.instance.PlayerLevelUp();
        }
       // float xpDiff = (float)GameManager.instance.LevelUpData.levelsData[player.level + 1].xpReq - GameManager.instance.LevelUpData.levelsData[player.level].xpReq;
       //float c_Xp = player.xp - GameManager.instance.LevelUpData.levelsData[player.level].xpReq;
       // float fillValue = c_Xp / xpDiff;
        float fillValue = (float)player.xp / (float)GameManager.instance.LevelUpData.levelsData[player.level].xpReq;
        return fillValue;
    }
    private void Update()
    {
        //playerCoins.text = ProfileManager.instance.currentPlayer.coins.ToString();
        //playerLevel.text = ProfileManager.instance.currentPlayer.level.ToString();
        //playerGems.text = ProfileManager.instance.currentPlayer.gems.ToString();

        playerCoins.GetComponent<Kozykin.MultiLanguageItem>().strItems[0] = ProfileManager.instance.currentPlayer.coins.ToString();
        playerCoins.GetComponent<Kozykin.MultiLanguageItem>().strItems[1] = ProfileManager.instance.currentPlayer.coins.ToString();
        playerCoins.GetComponent<Kozykin.MultiLanguageItem>().text = ProfileManager.instance.currentPlayer.coins.ToString();

        playerGems.GetComponent<Kozykin.MultiLanguageItem>().strItems[0] = ProfileManager.instance.currentPlayer.gems.ToString();
        playerGems.GetComponent<Kozykin.MultiLanguageItem>().strItems[1] = ProfileManager.instance.currentPlayer.gems.ToString();
        playerGems.GetComponent<Kozykin.MultiLanguageItem>().text = ProfileManager.instance.currentPlayer.gems.ToString();

        playerLevel.GetComponent<Kozykin.MultiLanguageItem>().strItems[0] = ProfileManager.instance.currentPlayer.level.ToString();
        playerLevel.GetComponent<Kozykin.MultiLanguageItem>().strItems[1] = ProfileManager.instance.currentPlayer.level.ToString();
        playerLevel.GetComponent<Kozykin.MultiLanguageItem>().text = ProfileManager.instance.currentPlayer.level.ToString();
    }
    private void LateUpdate()
    {
        //   StatsDisplay();
        
    }
    void StatsDisplay()
    {
        if (ProfileManager.instance.currentPlayer != null)
        {
            DisplayPlayerData(ProfileManager.instance.currentPlayer);
            profileImage.sprite = player.profilePicture;
            if (!profileApplied)
            {

                if (ProfileManager.instance.currentPlayer?.profilePicture)
                {
                    profileImage.sprite = player.profilePicture;
                    profileApplied = true;
                }
            }
        }
    }
}
