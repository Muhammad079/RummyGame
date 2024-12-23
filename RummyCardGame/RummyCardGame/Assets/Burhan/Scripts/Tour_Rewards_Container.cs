using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Tour_Rewards_Container : MonoBehaviour
{
    public Tournament_Rewards reward = null;
    [SerializeField] private Text description = null;
    [SerializeField] private Image rewardImage = null;
    //public Button claim = null;
    // Start is called before the first frame update
    void Start()
    {
        //if(claim.transform.GetChild(0).gameObject.activeInHierarchy)
        //{
        //    claim.interactable = false;
        //}


        //thisonClick.AddListener(T_CollecReward);
        //T_CheckAvailability();
        rewardImage.sprite = reward.Sprites;
        description.text = reward.rewardTitle.ToString();
    }
    private void Update()
    {
        
    }
    private void T_CollecReward()
    {
        //GetComponent<Button>().interactable = false;
        foreach (var a in reward.Rewards)
            ProfileManager.instance.GetReward(a.reward, a.quantity, a.boxReward);
    }

    //void T_CheckAvailability()
    //{
    //    if (transform.GetSiblingIndex() < Hand_Pass_Handler.passLevel)
    //    {
    //        if (ProfileManager.instance.currentPlayer.claimedFreePassReward > transform.GetSiblingIndex())
    //        {
    //            T_claimButton.SetActive(true);
    //            claimButton.SetActive(false);
    //            lockImage.SetActive(false);
    //        }
    //        else
    //        {
    //            T_claimButton.SetActive(false);
    //            claimButton.SetActive(true);
    //            lockImage.SetActive(false);
    //        }
    //    }
    //    else
    //    {
    //        T_claimButton.SetActive(false);
    //        claimButton.SetActive(false);
    //        lockImage.SetActive(true);
    //    }
    //}
}
