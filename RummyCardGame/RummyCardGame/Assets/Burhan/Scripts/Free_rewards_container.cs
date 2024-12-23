using UnityEngine;
using UnityEngine.UI;

public class Free_rewards_container : MonoBehaviour
{
    public FreeHandPassReward reward = null;
    [SerializeField] private Hand_Pass_Handler hand_Pass_Handler = null;
    GameObject claimButton = null, lockImage = null, claimedImage = null;
    // Start is called before the first frame update
    void Start()
    {
       
        if (reward.Free_sprites)
        {
            transform.GetChild(1).GetComponent<Image>().sprite = reward.Free_sprites;
        }
        else
        {
            transform.GetChild(1).GetComponent<Image>().enabled = false;
        }

        transform.GetChild(2).GetComponent<Text>().text = reward.Free_texts;
        transform.GetChild(2).GetComponent<Kozykin.MultiLanguageItem>().strItems[0] = reward.Free_texts;
        transform.GetChild(2).GetComponent<Kozykin.MultiLanguageItem>().strItems[1] = reward.Free_texts_Arabic;
        transform.GetChild(2).GetComponent<Kozykin.MultiLanguageItem>().text = reward.Free_texts_Arabic;

        claimButton = this.transform.Find("Claim_btn").gameObject;
        claimedImage = this.transform.Find("Claimed_Image").gameObject;
        lockImage = transform.Find("Lock_Image").gameObject;
        claimButton.GetComponent<Button>().onClick.AddListener(CollecReward);
        Invoke(nameof(CheckAvailability), 0.5f);
        hand_Pass_Handler.startState += CheckAvailability;
    }

    private void CollecReward()
    {
        claimButton.gameObject.SetActive(false);
        claimedImage.gameObject.SetActive(true);
        ProfileManager.instance.currentPlayer.claimedFreePassReward++;
        foreach (var a in reward.Rewards)
            ProfileManager.instance.GetReward(a.reward, a.quantity, a.boxReward);
    }

    void CheckAvailability()
    {
        Debug.Log("Moverrr");
        if (transform.GetSiblingIndex() < Hand_Pass_Handler.passLevel)
        {

            if (ProfileManager.instance.currentPlayer.claimedFreePassReward > transform.GetSiblingIndex())
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
            lockImage.SetActive(false);
            if (reward.Free_sprites)
            {
                lockImage.SetActive(true);
            }
        }
    }
}
