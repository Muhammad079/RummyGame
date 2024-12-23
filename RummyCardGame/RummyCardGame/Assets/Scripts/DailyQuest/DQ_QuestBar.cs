using UnityEngine;
using UnityEngine.UI;

public class DQ_QuestBar : MonoBehaviour
{
    [SerializeField] private RewardItems reward = null;
    [SerializeField] private Image fillingImage = null;
    [SerializeField] private Text t_WinCondition = null;
    [SerializeField] private Text t_WinningReward = null;
    [SerializeField] private Button collectionButton = null;
    string condition = "";
    public int collectionInformation { get => PlayerPrefs.GetInt(ProfileManager.instance.currentPlayer.name + this.gameObject + "LastDisplayTime"); set => PlayerPrefs.SetInt(ProfileManager.instance.currentPlayer.name + this.gameObject + "LastDisplayTime", value); }
    public delegate bool WinCon();
    public DailyQuestManager questManager = null;
    private void Start()
    {
        questManager.resetDailyQuest += ResetPrefs;
        collectionButton.onClick.AddListener(ColledReward);
    }

    private void ResetPrefs()
    {
        collectionInformation = 0;
    }

    private void ColledReward()
    {
        WinReward();
    }

    public void WinStatus(WinCon ConditionSatisfier, string winningCondition)
    {
        winningCondition = winningCondition.ToLower();
       winningCondition= winningCondition.Replace("(clone)", "");
        if (winningCondition != "")
            condition = winningCondition;
        Debug.Log("Checking Status");
        t_WinningReward.text = reward.quantity.ToString();
        t_WinningReward.GetComponent<Kozykin.MultiLanguageItem>().strItems[0] = reward.quantity.ToString();
        t_WinningReward.GetComponent<Kozykin.MultiLanguageItem>().strItems[1] = reward.quantity.ToString();
        t_WinningReward.GetComponent<Kozykin.MultiLanguageItem>().text = reward.quantity.ToString();

        t_WinCondition.text = condition;
        if (collectionInformation > 0)
        {
            PrizeCollected();
        }
        else
        {
            collectionButton.interactable = true;
            if (collectionButton.transform.GetChild(0).GetComponent<Text>())
            {
                collectionButton.transform.GetChild(0).GetComponent<Text>().text = "Collect";
            }
            if (ConditionSatisfier())
            {
                Debug.Log("Condition Fulfill");
                collectionButton.gameObject.SetActive(true);
            }
            else
            {
                Debug.Log("Condition not Fulfilled");
                collectionButton.gameObject.SetActive(false);
            }
        }
    }

    public void WinReward()
    {
        ProfileManager.instance.currentPlayer.completedDailyQuests++;
        ProfileManager.instance.GetReward(reward.reward, reward.quantity, reward.boxReward);
        PrizeCollected();
        collectionInformation = 1;
        ProfileManager.instance.SaveUserData();
    }
    void PrizeCollected()
    {
        collectionButton.interactable = false;
        if (collectionButton.transform.GetChild(0).GetComponent<Text>())
        {
            collectionButton.transform.GetChild(0).GetComponent<Text>().text = "Collected";
        }
    }
}


