using Coffee.UIEffects;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainMenuStats : MonoBehaviour
{
    public static MainMenuStats instance;
    private void Awake()
    {
        instance = this;
    }
    public WarningDialogue warningDialogue = null;
    public List<GiftSendingMessege> pendingGiftMsg = new List<GiftSendingMessege>();
    [Header("Gameobject References")]
    public LevelUpScreen levelUpScreen = null;
    public LuckyBoxScreen luckyBoxScreen = null;
    public Transform playerStatArea = null;
    [SerializeField] private GameObject rateUsPanel = null;
    [SerializeField] private Text roomMessege = null;
    [SerializeField] private GiftSendingDisplay giftSendingDisplay = null;


    [Header("Shine effect objects")]
    [SerializeField] private List<UIShiny> shinyObjects = new List<UIShiny>();

    public void PlayerLevelUp()
    {
        Debug.Log("Levelling up");
        levelUpScreen.LevelUp();
    }
    private void Start()
    {
        ProfileManager.instance.currentPlayer.TwoHK_Selected = false;
        if (ProfileManager.instance.currentPlayer.isVip)
        {
            ProfileManager.instance.currentPlayer.VIP_Level = 1;
            for (int i = 0; i < GameManager.instance.VIP_Information.VIPs.Count; i++)
            {
                if (ProfileManager.instance.currentPlayer.vipXp >= GameManager.instance.VIP_Information.VIPs[i].VIP_Points)
                {
                    ProfileManager.instance.currentPlayer.VIP_Level++;
                }
            }
        }
        

        if (BoxRewardScreen.LevelUp_Show)
        {
            BoxRewardScreen.LevelUp_Show = false;
            levelUpScreen.LevelUp();
        }

        int Ran_Value = UnityEngine.Random.Range(0, 20);
        Debug.Log("Random Value= " + Ran_Value);
        if (PlayerPrefs.GetInt("RateUS_Check") == 0)
        {
            if (Ran_Value <= 5)
            {
                //    rateUsPanel.SetActive(true);
            }
        }
        StartShineEffect();
    }
    public void PauseShineEffect()
    {
        foreach (var a in shinyObjects)
        {
            a.enabled = false;
        }
    }
    public void StartShineEffect()
    {
        foreach (var a in shinyObjects)
        {
            a.enabled = true;
        }
    }
    public void ShowMessege(string messege)
    {
        if (instance)
            roomMessege.text = messege;
    }
    bool giftInfoShowing = false;
    public void DisplayGiftMessege(GiftSendingMessege giftMsg)
    {
        if (!giftInfoShowing)
        {
            giftInfoShowing = true;
            Debug.Log("Called");
            var a = Instantiate(giftSendingDisplay, transform);
            a.gameObject.SetActive(true);
            a.PassData(giftMsg);
            Invoke(nameof(ResetGiftInfoValue), 3);
        }
    }
    void ResetGiftInfoValue() => giftInfoShowing = false;
}