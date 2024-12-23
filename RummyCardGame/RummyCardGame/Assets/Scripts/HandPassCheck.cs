using UnityEngine;
using UnityEngine.UI;

public class HandPassCheck : MonoBehaviour
{
    [SerializeField] private Image fillImage = null;
    [SerializeField] private Text goldenCardCounter = null;
    [SerializeField] private GameObject collectReward = null;
    [SerializeField] private Text levelText = null;
    void Start()
    {
        ShowStat();
    }

    void ShowStat()
    {
        float remainder = 0;
        if (ProfileManager.instance.currentPlayer.goldenCards < 16)
            remainder = ProfileManager.instance.currentPlayer.goldenCards;
        else
            remainder = ProfileManager.instance.currentPlayer.goldenCards % 16;
        fillImage.fillAmount = remainder / 16;
        goldenCardCounter.text = (int)remainder + "/16";
        goldenCardCounter.GetComponent<Kozykin.MultiLanguageItem>().strItems[0] = (int)remainder + "/16";
        goldenCardCounter.GetComponent<Kozykin.MultiLanguageItem>().strItems[1] = (int)remainder + "/16";
        goldenCardCounter.GetComponent<Kozykin.MultiLanguageItem>().text = (int)remainder + "/16";

        levelText.text = ProfileManager.instance.currentPlayer.claimedFreePassReward.ToString();
        levelText.GetComponent<Kozykin.MultiLanguageItem>().strItems[0] = ProfileManager.instance.currentPlayer.claimedFreePassReward.ToString();
        levelText.GetComponent<Kozykin.MultiLanguageItem>().strItems[1] = ProfileManager.instance.currentPlayer.claimedFreePassReward.ToString();
        levelText.GetComponent<Kozykin.MultiLanguageItem>().text = ProfileManager.instance.currentPlayer.claimedFreePassReward.ToString();

        if (ProfileManager.instance.currentPlayer.claimedFreePassReward< (int)(ProfileManager.instance.currentPlayer.goldenCards / 16))
        {
             collectReward.SetActive(true);
        }
    }
}
