using DG.Tweening;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BoxRewardScreen : SceneLoader
{
    [SerializeField] private List<BoxItems> rewards = new List<BoxItems>();

    [SerializeField] private Transform rewardGrid = null;
    [SerializeField] private GameObject objectReward = null;
    private Sprite passingSprite = null;
    [SerializeField] private Button collectionButton = null;
    public BoxRewards selectedBox = null;
    [SerializeField] private List<RewardTransporter> rewardCarrier = new List<RewardTransporter>();
    public PlayerProfile updatedProfile = new PlayerProfile();
    [SerializeField] private Transform circle = null;
    [SerializeField] private Transform rotatingObjects = null;
    [SerializeField] private Transform target = null;
    [SerializeField] private Button doubleUpButton = null;
    [SerializeField]
    private GameObject Stats;

    public static bool LevelUp_Show;

    private void OnEnable()
    {
        LevelUp_Show = false;
        Stats.transform.GetChild(0).GetChild(1).GetComponent<Text>().text = ProfileManager.instance.currentPlayer.coins.ToString();
        Stats.transform.GetChild(0).GetChild(1).GetComponent<Kozykin.MultiLanguageItem>().strItems[0] = ProfileManager.instance.currentPlayer.coins.ToString();
        Stats.transform.GetChild(0).GetChild(1).GetComponent<Kozykin.MultiLanguageItem>().strItems[1] = ProfileManager.instance.currentPlayer.coins.ToString();
        Stats.transform.GetChild(0).GetChild(1).GetComponent<Kozykin.MultiLanguageItem>().text = ProfileManager.instance.currentPlayer.coins.ToString();

        Stats.transform.GetChild(1).GetChild(1).GetComponent<Text>().text = ProfileManager.instance.currentPlayer.gems.ToString();
        Stats.transform.GetChild(1).GetChild(1).GetComponent<Kozykin.MultiLanguageItem>().strItems[0] = ProfileManager.instance.currentPlayer.gems.ToString();
        Stats.transform.GetChild(1).GetChild(1).GetComponent<Kozykin.MultiLanguageItem>().strItems[1] = ProfileManager.instance.currentPlayer.gems.ToString();
        Stats.transform.GetChild(1).GetChild(1).GetComponent<Kozykin.MultiLanguageItem>().text = ProfileManager.instance.currentPlayer.gems.ToString();

        Stats.transform.GetChild(2).GetChild(1).GetChild(0).GetComponent<Text>().text = ProfileManager.instance.currentPlayer.level.ToString();
        Stats.transform.GetChild(2).GetChild(1).GetChild(0).GetComponent<Kozykin.MultiLanguageItem>().strItems[0] = ProfileManager.instance.currentPlayer.level.ToString();
        Stats.transform.GetChild(2).GetChild(1).GetChild(0).GetComponent<Kozykin.MultiLanguageItem>().strItems[1] = ProfileManager.instance.currentPlayer.level.ToString();
        Stats.transform.GetChild(2).GetChild(1).GetChild(0).GetComponent<Kozykin.MultiLanguageItem>().text = ProfileManager.instance.currentPlayer.level.ToString();

        Stats.transform.GetChild(2).GetChild(0).GetChild(0).GetComponent<Image>().fillAmount = FillImageValue();
        //Stats.transform.GetChild(2).GetChild(0).GetChild(0).GetComponent<Image>().fillAmount = ProfileManager.instance.currentPlayer.xp / GameManager.instance.LevelUpData.levelsData[ProfileManager.instance.currentPlayer.level + 1].xpReq;

        Stats.transform.DOLocalMoveY(130,0.5f);
    }
    float FillImageValue()
    {
        int n = ProfileManager.instance.currentPlayer.level;
        while (ProfileManager.instance.currentPlayer.xp > GameManager.instance.LevelUpData.levelsData[ProfileManager.instance.currentPlayer.level + 1].xpReq)
        {
            Debug.Log("Level up");
            ProfileManager.instance.currentPlayer.level++;
        }
        if (n < ProfileManager.instance.currentPlayer.level)
        {
            Debug.Log("Level up");
            if(MainMenuStats.instance!=null)
            {
                MainMenuStats.instance.PlayerLevelUp();
            }
            else
            {
                Stats.transform.GetChild(2).GetChild(1).GetChild(0).GetComponent<Text>().text = ProfileManager.instance.currentPlayer.level.ToString();
                Stats.transform.GetChild(2).GetChild(1).GetChild(0).GetComponent<Kozykin.MultiLanguageItem>().strItems[0] = ProfileManager.instance.currentPlayer.level.ToString();
                Stats.transform.GetChild(2).GetChild(1).GetChild(0).GetComponent<Kozykin.MultiLanguageItem>().strItems[1] = ProfileManager.instance.currentPlayer.level.ToString();
                Stats.transform.GetChild(2).GetChild(1).GetChild(0).GetComponent<Kozykin.MultiLanguageItem>().text = ProfileManager.instance.currentPlayer.level.ToString();

                //StartCoroutine(GameManager.instance.Fix());
                LevelUp_Show = true;
            }
            
        }
        float xpDiff = (float)GameManager.instance.LevelUpData.levelsData[ProfileManager.instance.currentPlayer.level + 1].xpReq - GameManager.instance.LevelUpData.levelsData[ProfileManager.instance.currentPlayer.level].xpReq;
        float c_Xp = ProfileManager.instance.currentPlayer.xp - GameManager.instance.LevelUpData.levelsData[ProfileManager.instance.currentPlayer.level].xpReq;
        float fillValue = c_Xp / xpDiff;
        return fillValue;
    }
    public void PassReward(BoxRewards box)
    {
        this.gameObject.SetActive(true);
        selectedBox = box;
        ResetValues();
        Invoke(nameof(OpenBox), 2);
    }
    void ResetValues()
    {
        updatedProfile = new PlayerProfile();
        updatedProfile.goldenCards = 0;
        updatedProfile.coins = 0;
        updatedProfile.gems = 0;
        updatedProfile.xp = 0;
        updatedProfile.badges = 0;
        updatedProfile.VIP_7days_count = 0;
        for (int n = 0; n < rewardGrid.childCount; n++)
        {
            rewardGrid.GetChild(n).gameObject.SetActive(false);
        }
        collectionButton.gameObject.SetActive(false);
        doubleUpButton.gameObject.SetActive(false);
    }
    private void Start()
    {
        PassReward(GameManager.instance.boxReward);
        collectionButton.onClick.AddListener(CollectReward);
        doubleUpButton.onClick.AddListener(RewardVideo);
    }
    void OpenBox()
    {
        for (int n = 0; n < selectedBox.boxItems.Count; n++)
        {
            TrackingRewards(selectedBox.boxItems[n]);
            rewards.Add(selectedBox.boxItems[n]);
            if (n >= circle.childCount)
                Instantiate(circle.GetChild(0).gameObject, circle);
            circle.GetChild(n).GetComponent<Image>().sprite = passingSprite;
            if (n >= rotatingObjects.childCount)
                Instantiate(rotatingObjects.GetChild(0).gameObject, rotatingObjects);
            rotatingObjects.GetChild(n).GetComponent<Image>().sprite = passingSprite;
            var a = Instantiate(objectReward, rewardGrid);
            a.GetComponent<RewardScript>().GetReward(selectedBox.boxItems[n], passingSprite);
            rewardCarrier.Add(a.GetComponent<RewardTransporter>());
        }
        ProfileManager.instance.currentPlayer.boxesOpenedToday++;
        collectionButton.gameObject.SetActive(true);
        //doubleUpButton.gameObject.SetActive(true);
    }
    void TrackingRewards(BoxItems reward)
    {
        if (reward.reward == RewardType.cardCollection)
        {

            passingSprite = GameManager.instance.r_GoldenCard;
            updatedProfile.goldenCards = reward.quantity;
        }
        else if (reward.reward == RewardType.coins)
        {

            passingSprite = GameManager.instance.r_Coins;
            updatedProfile.coins = reward.quantity;
        }
        else if (reward.reward == RewardType.gems)
        {

            passingSprite = GameManager.instance.r_Gems;
            updatedProfile.gems = reward.quantity;
        }
        else if (reward.reward == RewardType.xp)
        {

            passingSprite = GameManager.instance.r_Xp;
            updatedProfile.xp = reward.quantity;
        }
        else if (reward.reward == RewardType.badges)
        {

            passingSprite = GameManager.instance.r_Badges;
            updatedProfile.badges = reward.quantity;
        }
        else if(reward.reward == RewardType.vip)
        {
            passingSprite = GameManager.instance.VIP_Levels[0];
            updatedProfile.VIP_7days_count = reward.quantity;
            updatedProfile.isVip = true;
            if (updatedProfile.VIP_7days_Start_Day == 0)
            {
                updatedProfile.VIP_7days_Start_Day = System.DateTime.Today.ToUniversalTime().DayOfYear;
            }
                


        }
    }
    void CollectReward()
    {
        foreach (var a in rewardCarrier)
        {
            if(a.GetComponent<RewardScript>().rewardToCollect.reward == RewardType.xp)//coins, gems, xp
            {
                a.MoveNow(Stats.transform.GetChild(2).GetChild(1).transform);
            }
            else if (a.GetComponent<RewardScript>().rewardToCollect.reward == RewardType.coins)//coins, gems, xp
            {
                a.MoveNow(Stats.transform.GetChild(0).GetChild(0).transform);
            }
            else if (a.GetComponent<RewardScript>().rewardToCollect.reward == RewardType.gems)//coins, gems, xp
            {
                a.MoveNow(Stats.transform.GetChild(1).GetChild(0).transform);
            }
            else
            {
                a.MoveNow(Stats.transform.GetChild(2).GetChild(1).transform);
            }

            if(a.GetComponent<RewardScript>().rewardToCollect.reward == RewardType.vip)
            {
                ProfileManager.instance.currentPlayer.isVip = updatedProfile.isVip;
            }
        }
        ProfileManager.instance.currentPlayer.badges += updatedProfile.badges;
        ProfileManager.instance.currentPlayer.xp += updatedProfile.xp;
        ProfileManager.instance.currentPlayer.gems += updatedProfile.gems;
        ProfileManager.instance.currentPlayer.coins += updatedProfile.coins;
        ProfileManager.instance.currentPlayer.goldenCards += updatedProfile.goldenCards;
        ProfileManager.instance.currentPlayer.VIP_7days_count += updatedProfile.VIP_7days_count;
        ProfileManager.instance.currentPlayer.VIP_7days_Start_Day += updatedProfile.VIP_7days_Start_Day;
        
        ProfileManager.instance.SaveUserData();
        //   this.gameObject.SetActive(false);

        Invoke(nameof(Stats_Update), 1);
        
    }
    void Stats_Update()
    {
        Stats.transform.GetChild(0).GetChild(1).GetComponent<Text>().text = ProfileManager.instance.currentPlayer.coins.ToString();
        Stats.transform.GetChild(0).GetChild(1).GetComponent<Kozykin.MultiLanguageItem>().strItems[0] = ProfileManager.instance.currentPlayer.coins.ToString();
        Stats.transform.GetChild(0).GetChild(1).GetComponent<Kozykin.MultiLanguageItem>().strItems[1] = ProfileManager.instance.currentPlayer.coins.ToString();
        Stats.transform.GetChild(0).GetChild(1).GetComponent<Kozykin.MultiLanguageItem>().text = ProfileManager.instance.currentPlayer.coins.ToString();

        Stats.transform.GetChild(1).GetChild(1).GetComponent<Text>().text = ProfileManager.instance.currentPlayer.gems.ToString();
        Stats.transform.GetChild(1).GetChild(1).GetComponent<Kozykin.MultiLanguageItem>().strItems[0] = ProfileManager.instance.currentPlayer.gems.ToString();
        Stats.transform.GetChild(1).GetChild(1).GetComponent<Kozykin.MultiLanguageItem>().strItems[1] = ProfileManager.instance.currentPlayer.gems.ToString();
        Stats.transform.GetChild(1).GetChild(1).GetComponent<Kozykin.MultiLanguageItem>().text = ProfileManager.instance.currentPlayer.gems.ToString();

        Stats.transform.GetChild(2).GetChild(1).GetChild(0).GetComponent<Text>().text = ProfileManager.instance.currentPlayer.level.ToString();
        Stats.transform.GetChild(2).GetChild(1).GetChild(0).GetComponent<Kozykin.MultiLanguageItem>().strItems[0] = ProfileManager.instance.currentPlayer.level.ToString();
        Stats.transform.GetChild(2).GetChild(1).GetChild(0).GetComponent<Kozykin.MultiLanguageItem>().strItems[1] = ProfileManager.instance.currentPlayer.level.ToString();
        Stats.transform.GetChild(2).GetChild(1).GetChild(0).GetComponent<Kozykin.MultiLanguageItem>().text = ProfileManager.instance.currentPlayer.level.ToString();

        Stats.transform.GetChild(2).GetChild(0).GetChild(0).GetComponent<Image>().fillAmount = FillImageValue();
        //Stats.transform.GetChild(2).GetChild(0).GetChild(0).GetComponent<Image>().fillAmount = ProfileManager.instance.currentPlayer.xp / GameManager.instance.LevelUpData.levelsData[ProfileManager.instance.currentPlayer.level + 1].xpReq;

        Invoke(nameof(HomeScene), 2);
    }
    void HomeScene()
    {
        //GameManager.instance.sceneToLoad = "Home";
        //UnityEngine.SceneManagement.SceneManager.LoadScene("Home");
        StartCoroutine(OnClick());
    }
    void RewardVideo()
    {
        doubleUpButton.gameObject.SetActive(false);
        GoogleMobileAdsManager.Instance.ShowInterstitial();
        DoubleReward();
    }
    void DoubleReward()
    {
        for (int n = 0; n < selectedBox.boxItems.Count; n++)
        {
            selectedBox.boxItems[n].quantity *= 2;
        }
        for (int n = 0; n < rewardCarrier.Count; n++)
        {
            rewardCarrier[n].GetComponent<RewardScript>().GetReward(selectedBox.boxItems[n], passingSprite);
        }
    }
    public override void Update()
    {
        base.Update();
    }
}
[System.Serializable]
public class BoxRewards
{
    public string boxTitle;
    public List<BoxItems> boxItems;
    public float unlockTimer;
    public int unlockPrice;
    public BoxType boxType;
}