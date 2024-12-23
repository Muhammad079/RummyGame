using UnityEngine;
using UnityEngine.UI;

public class TrophiesReward : MonoBehaviour
{
    //[SerializeField] private Scriptable_TrophiesBasedReward rewardFile;
    [SerializeField] private Scriptable_Trophy_Rewards Trophy_Panel_Indicator;
    [SerializeField] private GameObject claimRewardText = null;
    [SerializeField] private Text t_Trophies = null;
    [SerializeField] private Image fillImage = null;
    int index = 0;
    bool fromStart = false;
    [SerializeField] private GameObject sparkleEffect = null;
    void Start()
    {
        Debug.Log(this.name);
        claimRewardText.SetActive(false);


        InvokeRepeating(nameof(RewardStatus), 5, 1);
        RewardStatus();
    }
    void RewardStatus()
    {
        int total_Rewards = 0;
        for (int k = 0; k < Trophy_Panel_Indicator.Portion_Level.Count; k++)
        {
            for (int i = 0; i < Trophy_Panel_Indicator.Portion_Level[k].Portion_Rewards.Count; i++)
            {
                if(ProfileManager.instance.currentPlayer.trophies >= Trophy_Panel_Indicator.Portion_Level[k].Portion_Rewards[i].Trophies_Required)
                {
                    total_Rewards++;
                    //if(i < ProfileManager.instance.currentPlayer.Reward_ID_container.Count)
                    //{
                    //    claimRewardText.SetActive(false);
                    //    fillImage.fillAmount = 0;
                    //    t_Trophies.text = ProfileManager.instance.currentPlayer.trophies.ToString();
                    //}
                    //else
                    //{
                    //    claimRewardText.SetActive(true);
                    //    fillImage.fillAmount = 1;
                    //}
                    
                }
                else
                {
                    
                }
            }
        }
        if (total_Rewards > ProfileManager.instance.currentPlayer.Reward_ID_container.Count)
        {
            claimRewardText.SetActive(true);
            fillImage.fillAmount = 1;
        }
        else
        {
            claimRewardText.SetActive(false);
            fillImage.fillAmount = 0;
            t_Trophies.text = ProfileManager.instance.currentPlayer.trophies.ToString();
            t_Trophies.GetComponent<Kozykin.MultiLanguageItem>().strItems[0] = ProfileManager.instance.currentPlayer.trophies.ToString();
            t_Trophies.GetComponent<Kozykin.MultiLanguageItem>().strItems[1] = ProfileManager.instance.currentPlayer.trophies.ToString();
            t_Trophies.GetComponent<Kozykin.MultiLanguageItem>().text = ProfileManager.instance.currentPlayer.trophies.ToString();
        }
    }
}
