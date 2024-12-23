using DG.Tweening;
using System;
using UnityEngine;
using UnityEngine.UI;
public class BoxBarScript : SceneLoader
{
    [SerializeField] private bool dailyReward = false;
    [SerializeField] private GameObject filled = null, empty = null, locked = null, unLocked = null, warningPanel;
    [SerializeField] private BoxRewardScreen boxOpeningScreen = null;
    public bool isFilled = false, isLocked = false;
    public float unlockTimerSeconds;
    [SerializeField] private Text boxBarTitle = null;
    [SerializeField] private Text timer = null;
    [SerializeField] private Button collectButton = null;
    [SerializeField] private int unlockPrice = 0;
    [SerializeField] public BoxRewards reward = null;
    [SerializeField] private LuckyBoxScreen luckyBoxScreen = null;
    int secsInOneDay = 86400;
    void Start()
    {
        if (ProfileManager.instance.currentPlayer.boxRewardInQue.Count == 0)
        {
            if (dailyReward)
            {
                ProfileManager.instance.currentPlayer.boxRewardInQue.Add(reward);
            }
        }
        if (transform.GetSiblingIndex() < ProfileManager.instance.currentPlayer.boxRewardInQue.Count)
        {
            reward = ProfileManager.instance.currentPlayer.boxRewardInQue[transform.GetSiblingIndex()];
            FillBar(reward, true);
        }
        else
        {
            PlayerPrefs.SetInt((this.gameObject.name + "Filled"), 0);
        }
        GettingDataFromPrefs();
        BoxesStatus();
        if (isLocked)
        {
            unlockTimerSeconds -= TimeElapsed();
        }
    }
    void GettingDataFromPrefs()
    {
        if (PlayerPrefs.HasKey(this.gameObject.name + "Filled"))
        {
            if (PlayerPrefs.GetInt(this.gameObject.name + "Filled") > 0)
                isFilled = true;
            else
                isFilled = false;
        }
        else
        {
            if (isFilled)
                PlayerPrefs.SetInt((this.gameObject.name + "Filled"), 1);
            else
                PlayerPrefs.SetInt((this.gameObject.name + "Filled"), 0);
        }

        if (PlayerPrefs.HasKey(this.gameObject.name + "Locked"))
        {
            if (PlayerPrefs.GetInt(this.gameObject.name + "Locked") > 0)
                isLocked = true;
            else
                isLocked = false;
        }
        else
        {
            if (isLocked)
                PlayerPrefs.SetInt((this.gameObject.name + "Locked"), 1);
            else
                PlayerPrefs.SetInt((this.gameObject.name + "Locked"), 0);
        }
    }
    private void BoxesStatus()
    {
        if (isFilled)
        {
            filled.SetActive(true);
            empty.SetActive(false);
            collectButton.onClick.RemoveAllListeners();
            if (isLocked)
            {
                locked.SetActive(true);
                unLocked.SetActive(false);
                collectButton.onClick.AddListener(UnlockWithGems);
            }
            else
            {
                locked.SetActive(false);
                unLocked.SetActive(true);
                collectButton.onClick.AddListener(CollectReward);
            }
        }
        else
        {
            filled.SetActive(false);
            empty.SetActive(true);
            PlayerPrefs.SetFloat(this.gameObject.name + "RemTime", -10);
        }
    }
    public void FillBar(BoxRewards box, bool lockStatus)
    {
        reward = box;
        boxBarTitle.text = box.boxTitle;
        PlayerPrefs.SetInt((this.gameObject.name + "Filled"), 1);
        if (lockStatus)
            PlayerPrefs.SetInt((this.gameObject.name + "Locked"), 1);
        else
            PlayerPrefs.SetInt((this.gameObject.name + "Locked"), 0);
        if (PlayerPrefs.GetFloat(this.gameObject.name + "RemTime") < 0)
        {
            Debug.Log("Box actual unlocking time:    " + box.unlockTimer);

            PlayerPrefs.SetFloat(this.gameObject.name + "RemTime", box.unlockTimer);
        }
        Debug.Log(this.gameObject.name + PlayerPrefs.GetFloat(this.gameObject.name + "RemTime"));
        unlockTimerSeconds = PlayerPrefs.GetFloat(this.gameObject.name + "RemTime");
        unlockPrice = box.unlockPrice;
        GettingDataFromPrefs();
        BoxesStatus();
    }
    public override void Update()
    {
        base.Update();
        if (isFilled)
        {
            if (isLocked)
            {
                if (unlockTimerSeconds > 0)
                {

                    unlockTimerSeconds -= Time.deltaTime;
                    DisplayTime(unlockTimerSeconds);
                }
                else
                {
                    DisplayTime(0);
                    isLocked = false;
                    BoxesStatus();
                }

            }
        }
    }
    void DisplayTime(float secs)
    {
        var ts = TimeSpan.FromSeconds(secs);
        timer.text = String.Format("{0:00}:{1:00}:{2:00}", ts.Hours, ts.Minutes, ts.Seconds);
        timer.GetComponent<Kozykin.MultiLanguageItem>().strItems[0] = String.Format("{0:00}:{1:00}:{2:00}", ts.Hours, ts.Minutes, ts.Seconds);
        timer.GetComponent<Kozykin.MultiLanguageItem>().strItems[1] = String.Format("{0:00}:{1:00}:{2:00}", ts.Hours, ts.Minutes, ts.Seconds);
        timer.GetComponent<Kozykin.MultiLanguageItem>().text = String.Format("{0:00}:{1:00}:{2:00}", ts.Hours, ts.Minutes, ts.Seconds);
    }
    void CollectReward()
    {
        Debug.Log("COllect Reward");
        if (!isLocked)
        {
            Debug.Log("Caliming");
            Debug.Log(dailyReward);

            if (!dailyReward)
            {
                if (ProfileManager.instance.currentPlayer.boxRewardInQue.Contains(reward))
                    Debug.Log("Reward found");
                else
                    Debug.Log("Reward not found");
                PlayerPrefs.SetInt((this.gameObject.name + "Filled"), 0);
                PlayerPrefs.SetInt((this.gameObject.name + "Locked"), 1);
                if (transform.GetSiblingIndex() < ProfileManager.instance.currentPlayer.boxRewardInQue.Count)
                    ProfileManager.instance.currentPlayer.boxRewardInQue.RemoveAt(transform.GetSiblingIndex());
                else
                    ProfileManager.instance.currentPlayer.boxRewardInQue.RemoveAt(ProfileManager.instance.currentPlayer.boxRewardInQue.Count - 1);
            }
            else
            {
                Debug.Log("Daily");
                unlockTimerSeconds = secsInOneDay;
                PlayerPrefs.SetInt((this.gameObject.name + "Filled"), 1);
                PlayerPrefs.SetInt((this.gameObject.name + "Locked"), 1);
            }
            // boxOpeningScreen.PassReward(reward); PlayerPrefs.SetFloat(this.gameObject.name + "RemTime", -10);
            if (PlayerPrefs.HasKey(this.gameObject.name + "RemTime"))
                Debug.Log("Has Key RemTIme   " + PlayerPrefs.GetFloat(this.gameObject.name + "RemTime"));
            luckyBoxScreen.OnClick();
            GettingDataFromPrefs();
            BoxesStatus();
            GameManager.instance.boxReward = reward;

            StartCoroutine(OnClick());

            //GameManager.instance.sceneToLoad = "BoxOpening";
            //UnityEngine.SceneManagement.SceneManager.LoadScene("BoxOpening");
        }
    }



    void UnlockWithGems()
    {
        Debug.Log("UNlock ");
        if (isFilled)
        {
            if (ProfileManager.instance.currentPlayer.gems > reward.unlockPrice)
            {
                ProfileManager.instance.currentPlayer.gems -= reward.unlockPrice;
                isLocked = false;
                unlockTimerSeconds = 0;
                collectButton.onClick.RemoveListener(UnlockWithGems);
                BoxesStatus();
            }
            else
            {
                GameManager.instance.Not_Enough_C_Panel.SetActive(true);
                GameManager.instance.Not_Enough_C_Panel.transform.DOScale(new Vector3(1, 1, 1), 0.5f).SetEase(Ease.OutBounce);
                GameManager.instance.Not_Enough_C_Panel.transform.GetChild(0).GetComponent<Text>().text = "Not Enough Gems. You need "+ reward.unlockPrice+" Gems to unlock.";
                GameManager.instance.Not_Enough_C_Panel.transform.GetChild(0).GetComponent<Kozykin.MultiLanguageItem>().strItems[0] = "Not Enough Gems. You need " + reward.unlockPrice + " Gems to unlock.";
                GameManager.instance.Not_Enough_C_Panel.transform.GetChild(0).GetComponent<Kozykin.MultiLanguageItem>().strItems[1] = "الأحجار الكريمة غير كافية. انت تحتاج " + reward.unlockPrice + " الجواهر لفتح.";
                GameManager.instance.Not_Enough_C_Panel.transform.GetChild(0).GetComponent<Kozykin.MultiLanguageItem>().text = "الأحجار الكريمة غير كافية. انت تحتاج " + reward.unlockPrice + " الجواهر لفتح.";
            }
        }
    }
    private void OnDisable()
    {
        if (isFilled && isLocked)
            PlayerPrefs.SetFloat(this.gameObject.name + "RemTime", unlockTimerSeconds);
        PlayerPrefs.SetString(this.gameObject.name + "LastPlayed", DateTime.UtcNow.ToString());
    }
    float TimeElapsed()
    {
        var currentTime = DateTime.UtcNow;
        if (PlayerPrefs.HasKey(this.gameObject.name + "LastPlayed"))
        {
            var lastPlayed = DateTime.Parse(PlayerPrefs.GetString(this.gameObject.name + "LastPlayed"));
            var timeElapsed = currentTime - lastPlayed;
            return (timeElapsed.Days * 86400 + timeElapsed.Hours * 3600 + timeElapsed.Minutes * 60 + timeElapsed.Seconds);
        }
        else
            return 0;
    }
}
