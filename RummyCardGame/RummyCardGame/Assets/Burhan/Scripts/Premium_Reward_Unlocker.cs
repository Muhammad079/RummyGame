using UnityEngine;
using UnityEngine.UI;

public class Premium_Reward_Unlocker : MonoBehaviour
{
    //public int id = 0;
    public HandPassReward reward = null;
    GameObject claimButton = null, lockImage = null, claimedImage = null;
    [SerializeField] private Hand_Pass_Handler hand_Pass_Handler = null;
    // Start is called before the first frame update
    void Start()
    {
        transform.GetChild(1).GetComponent<Image>().sprite = reward.Premium_sprites;

        
        transform.GetChild(2).GetComponent<Text>().text = reward.Premium_texts;
        transform.GetChild(2).GetComponent<Kozykin.MultiLanguageItem>().strItems[0] = reward.Premium_texts;
        transform.GetChild(2).GetComponent<Kozykin.MultiLanguageItem>().strItems[1] = reward.Premium_texts_Arabic;
        transform.GetChild(2).GetComponent<Kozykin.MultiLanguageItem>().text = reward.Premium_texts_Arabic;

        claimButton = transform.Find("Claim_btn").gameObject;
        lockImage = transform.Find("Lock_Image").gameObject;
        claimedImage = transform.Find("Claimed_Image").gameObject;
        claimButton.GetComponent<Button>().onClick.AddListener(CollecReward);
        CheckAvailability();
        hand_Pass_Handler.startState += CheckAvailability;

    }

    private void CollecReward()
    {
        claimButton.SetActive(false);
        claimedImage.SetActive(true);
        ProfileManager.instance.currentPlayer.claimedPremiumPassReward++;
        foreach (var a in reward.Rewards)
            ProfileManager.instance.GetReward(a.reward, a.quantity, a.boxReward);
    }
    void CheckAvailability()
    {
        if (transform.GetSiblingIndex() < Hand_Pass_Handler.passLevel
           && ProfileManager.instance.currentPlayer.Hand_Pass_purchase_Check)
        {
            if (ProfileManager.instance.currentPlayer.claimedPremiumPassReward > transform.GetSiblingIndex())
            {
                claimedImage.SetActive(true);
                claimButton.SetActive(false);
                lockImage.SetActive(false);
            }
            else
            {
                claimedImage.SetActive(false);
                claimButton.SetActive(true);
                lockImage.SetActive(false);
            }
        }
        else
        {
            claimedImage.SetActive(false);
            claimButton.SetActive(false);
            lockImage.SetActive(true);
        }
    }
}
