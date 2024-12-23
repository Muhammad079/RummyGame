using DG.Tweening;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DailyQuestManager : MonoBehaviour
{
    [SerializeField] private List<string> dailyQuests = new List<string>();
    float secs = 0, resetTimer = 0;
    [SerializeField] private float unlockTimerInHour = 0;
    [SerializeField] private Transform gridParent = null;
    [SerializeField] private Button purchaseMoreQuests = null;
    public event Action resetDailyQuest = null;

    void Start()
    {
        purchaseMoreQuests.onClick.AddListener(PurchaseQuests);
        QuestTimeUpdater();
        Init();
    }
    void QuestTimeUpdater()
    {
        if (!PlayerPrefs.HasKey(this.gameObject.name + "QuestDisplayTime"))
            PlayerPrefs.SetString(this.gameObject.name + "QuestDisplayTime", DateTime.UtcNow.ToString());
        if (!PlayerPrefs.HasKey(this.gameObject.name + "QuestUpdateTime"))
            PlayerPrefs.SetString(this.gameObject.name + "QuestUpdateTime", DateTime.UtcNow.ToString());
        secs = (float)(DateTime.Parse(PlayerPrefs.GetString(this.gameObject.name + "QuestUpdateTime")).AddHours(unlockTimerInHour) - DateTime.UtcNow).TotalSeconds;
        resetTimer = (float)(DateTime.Parse(PlayerPrefs.GetString(this.gameObject.name + "QuestDisplayTime")).AddHours(24) - DateTime.UtcNow).TotalSeconds;
    }
    
    private void Init()
    {

        if (ProfileManager.instance.currentPlayer.completedDailyQuests == 0)
            DisplayQuests(4);
        else

            DisplayQuests(ProfileManager.instance.currentPlayer.displayingDailyQuests);

    }
    private void Update()
    {
        if (secs > 0)
        {
            secs -= Time.deltaTime;
        }
        else
        {
            PlayerPrefs.SetString(this.gameObject.name + "QuestUpdateTime", DateTime.UtcNow.ToString());
            QuestTimeUpdater();
            //   resetDailyQuest?.Invoke();
            ClearGrid();
            //   ProfileManager.instance.currentPlayer.completedDailyQuests = 0;
            CompletedQuests();
        }
        if (resetTimer > 0)
        {
            resetTimer -= Time.deltaTime;
        }
        else
        {
            PlayerPrefs.SetString(this.gameObject.name + "QuestDisplayTime", DateTime.UtcNow.ToString());
            QuestTimeUpdater();
            resetDailyQuest?.Invoke();
            ClearGrid();
            ProfileManager.instance.currentPlayer.completedDailyQuests = 0;
            DisplayQuests(4);
        }
    }
    void PurchaseQuests()
    {
        if (ProfileManager.instance.currentPlayer.gems >= 10)
        {
            ProfileManager.instance.currentPlayer.gems -= 10;
            int counter = gridParent.childCount + 4;
            DisplayQuests(counter);
        }
    }
    void DisplayQuests(int maxCounter)
    {
        var a = this.gameObject;
        for (int n = 0; n < maxCounter; n++)
        {
            if (n >= gridParent.childCount)
                Instantiate(Resources.Load<GameObject>(dailyQuests[n]), gridParent);
            a = gridParent.GetChild(n).gameObject;
            a.GetComponent<DQ_QuestBar>().questManager = this;
            a.SetActive(true);
        }

        ProfileManager.instance.currentPlayer.displayingDailyQuests = gridParent.childCount;
    }
    void ClearGrid()
    {
        for (int n = 0; n > gridParent.childCount; n++)
        {
            gridParent.GetChild(n).gameObject.SetActive(false);
        }
    }
    void CompletedQuests()
    {
        int increment = 0;
        for (int n = 0; n < gridParent.childCount; n++)
        {
            if (gridParent.GetChild(n).GetComponent<DQ_QuestBar>().collectionInformation > 0)
                increment++;
        }
        DisplayQuests(gridParent.childCount + increment);
    }
    public void ShowQuest()
    {
        this.gameObject.SetActive(true);
        transform.DOScale(1, 0.5f);
    }
    public void Hide()
    {
        this.gameObject.SetActive(false);
        transform.DOScale(0, 0.5f);
    }
}
