using DG.Tweening;
using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LuckySpinScript : SceneLoader
{
    //[SerializeField] private Scriptable_LuckySpin rewardData = null;
    [SerializeField] private bool dailyReward = false;
    [SerializeField] private Text timer = null;
    [SerializeField] private int unlockTimerInHour = 0;
    [SerializeField] private SpinReward selectedReward = null;
    float timerInSeconds = 0;
    [SerializeField] private Transform prizeGrid = null;
    string lastCounterUpdate { get => PlayerPrefs.GetString(this.gameObject.name + "LuckySpinLastUpdate"); set => PlayerPrefs.SetString(this.gameObject.name + "LuckySpinLastUpdate", value); }
    [SerializeField] private Wheel_Rotator wheel_Rotator = null;
    [SerializeField] private Button spinButton = null;
    void Start()
    {
        wheel_Rotator.counter = ProfileManager.instance.currentPlayer.spinCounts;
        if (lastCounterUpdate=="")
            lastCounterUpdate = System.DateTime.UtcNow.ToString();
        spinButton.onClick.AddListener(Spin);
        //ProfileManager.instance.currentPlayer.spinCounts = 3;
    }
    private void LateUpdate()
    {
        if (dailyReward)
        {
            if (lastCounterUpdate != "")
            {
                float secs = (float)(DateTime.Parse(lastCounterUpdate).AddHours(unlockTimerInHour) - DateTime.UtcNow).TotalSeconds;
                DisplayTime(secs);
                CounterUpdater();
            }
        }
    }

    bool CheckIfAvailable()
    {
        bool available = false;
        if (ProfileManager.instance.currentPlayer.spinCounts > 0)
            available = true;
        return available;
    }
    void CounterUpdater()
    {
        if (dailyReward)
        {
            //  Logic for daily reward base spin
            if (PlayerPrefs.HasKey(this.gameObject.name + "LuckySpinLastUpdate"))
            {
                if (DateTimeAdjuster.TimeElapsedInSec(lastCounterUpdate) > DateTimeAdjuster.HoursIntoSeconds(24))
                {
                    AddToCounter(1);
                     ProfileManager.instance.SaveUserData();
                    lastCounterUpdate = DateTime.UtcNow.ToString();
                }
            }
            else
            {
                lastCounterUpdate = DateTime.UtcNow.ToString();
            }
        }
        else
        {
            //  Logic for paid spin
        }
    }
    void AddToCounter(int spinAddition)
    {
        ProfileManager.instance.currentPlayer.spinCounts += spinAddition;
    }
    void DisplayTime(float secs)
    {
        var ts = TimeSpan.FromSeconds(secs);
        timer.text = String.Format("{0:00}:{1:00}:{2:00}", ts.Hours, ts.Minutes, ts.Seconds);
    }
    void Spin()
    {
        if(!SceneManager.GetActiveScene().name.Contains("GoldenSpin")) //Dont remove check
        {
            if (CheckIfAvailable())
            {
                //   ChooseReward();
                //  spin the spinner
                Debug.Log("spinning");
                ProfileManager.instance.currentPlayer.spinCounts--;
                //wheel_Rotator.GetComponent<DOTweenAnimation>().enabled = false;
                wheel_Rotator.spinWheel();

            }
            else
            {
                transform.DOLocalMoveY(400, 0.5f).SetEase(Ease.OutQuad).OnComplete(()=> {
                    //GameManager.instance.sceneToLoad = "GoldenSpin";
                    //Debug.LogError("Loading");
                    //SceneManager.LoadScene("GoldenSpin");
                    Loading_Screen = GameObject.Find("Loading_Screen");
                    Loading_animation = Loading_Screen.transform.GetChild(0).GetComponent<Text>();
                    StartCoroutine(OnClick());
                });
            }
        }
    }
    public override void Update()
    {
        base.Update();
    }
    //void ChooseReward()
    //{
    //    int itemPicker = UnityEngine.Random.Range(0, rewardData.spinRewards.Count);
    //    if (rewardData.spinRewards[itemPicker].availabilityCounter != 0)
    //    {
    //        //  give this reward
    //        selectedReward = rewardData.spinRewards[itemPicker];
    //    }
    //    else
    //    {
    //        ChooseReward();
    //    }
    //}
    void CollectReward()
    {
        foreach (var r in selectedReward.reward)
        {
            ProfileManager.instance.GetReward(r.reward.reward, r.reward.quantity);
        }
    }

}
