using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Trophy_Rewards_Container_New : SceneLoader
{
    //public Trophy_Rewards_popup_Handler trophy_Rewards_Popup_Handler;

    public RewardItems Reward;
    public Sprite Reward_Image;
    public string Reward_Trophies;
    public int id;
    public GameObject Lock;
    public Image Filler, Claimed;
    public Button Claim_btn;

    //int My_trophies = 0;

    // Start is called before the first frame update
    void Start()
    {
        //My_trophies = ProfileManager.instance.currentPlayer.trophies;

        //GetComponent<Button>().onClick.AddListener(delegate { trophy_Rewards_Popup_Handler.Getting_Setting_Data(Reward, Reward_Image, Reward_Trophies, id); });

        
        Invoke(nameof(refresh), 0.1f);
    }

    private void refresh()
    {
        if (int.Parse(Reward_Trophies) > ProfileManager.instance.currentPlayer.trophies)
        {
            Lock.SetActive(true);
            //float result = float.Parse( My_trophies.ToString()) / float.Parse(Reward_Trophies);
            //Filler.fillAmount = result;
        }
        else
        {

            Lock.SetActive(false);


            if (!ProfileManager.instance.currentPlayer.Reward_ID_container.Contains(id))
            {
                Claim_btn.gameObject.SetActive(true);
                Claim_btn.onClick.AddListener(() => {
                    ProfileManager.instance.currentPlayer.Reward_ID_container.Add(id);


                    if (Reward.reward == RewardType.box)
                    {
                        GameManager.instance.boxReward = Reward.boxReward;
                        StartCoroutine(OnClick());
                    }
                    else
                    {
                        ProfileManager.instance.GetReward(Reward.reward, Reward.quantity);
                    }




                    
                    //if (Reward.reward == RewardType.box)
                    //{
                    //    foreach (var a in Reward.boxReward.boxItems)
                    //    {
                    //        ProfileManager.instance.GetReward(a.reward, a.quantity);
                    //    }
                    //}
                    //else
                    //{
                    //    ProfileManager.instance.GetReward(Reward.reward, Reward.quantity);
                    //}

                    Debug.Log("Claimed ID: " + id);
                    Claim_btn.gameObject.SetActive(false);
                    Claimed.gameObject.SetActive(true);
                });
            }
            else
            {
                Claimed.gameObject.SetActive(true);

            }


            //Filler.fillAmount = 1;
            //My_trophies -= int.Parse(Reward_Trophies);
        }
        transform.GetChild(1).GetChild(0).GetComponent<Image>().sprite = Reward_Image;

        if(Reward.reward == RewardType.box)
        {
            transform.GetChild(1).GetChild(2).GetComponent<Text>().text = Reward.boxReward.boxTitle;
            transform.GetChild(1).GetChild(2).GetComponent<Kozykin.MultiLanguageItem>().strItems[0] = Reward.boxReward.boxTitle;
            if(Reward.boxReward.boxTitle.Contains("Mega"))
            {
                transform.GetChild(1).GetChild(2).GetComponent<Kozykin.MultiLanguageItem>().strItems[1] = "مربع فاخر ضخم";
                transform.GetChild(1).GetChild(2).GetComponent<Kozykin.MultiLanguageItem>().text = "مربع فاخر ضخم";
            }
            else if (Reward.boxReward.boxTitle.Contains("Bronze"))
            {
                transform.GetChild(1).GetChild(2).GetComponent<Kozykin.MultiLanguageItem>().strItems[1] = "صندوق برونزي";
                transform.GetChild(1).GetChild(2).GetComponent<Kozykin.MultiLanguageItem>().text = "صندوق برونزي";
            }
            else if (Reward.boxReward.boxTitle.Contains( "Silver"))
            {
                transform.GetChild(1).GetChild(2).GetComponent<Kozykin.MultiLanguageItem>().strItems[1] = "صندوق فضي";
                transform.GetChild(1).GetChild(2).GetComponent<Kozykin.MultiLanguageItem>().text = "صندوق فضي";
            }
            else if (Reward.boxReward.boxTitle.Contains( "Golden"))
            {
                transform.GetChild(1).GetChild(2).GetComponent<Kozykin.MultiLanguageItem>().strItems[1] = "الصندوق الذهبي";
                transform.GetChild(1).GetChild(2).GetComponent<Kozykin.MultiLanguageItem>().text = "الصندوق الذهبي";
            }
            else if (Reward.boxReward.boxTitle .Contains( "Fancy"))
            {
                transform.GetChild(1).GetChild(2).GetComponent<Kozykin.MultiLanguageItem>().strItems[1] = "صندوق فاخر";
                transform.GetChild(1).GetChild(2).GetComponent<Kozykin.MultiLanguageItem>().text = "صندوق فاخر";
            }
        }
        else if( Reward.reward == RewardType.coins)
        {
            transform.GetChild(1).GetChild(2).GetComponent<Text>().text = Reward.quantity + " Coins";
            transform.GetChild(1).GetChild(2).GetComponent<Kozykin.MultiLanguageItem>().strItems[0] = Reward.quantity + " Coins";
            transform.GetChild(1).GetChild(2).GetComponent<Kozykin.MultiLanguageItem>().strItems[1] = Reward.quantity + " عملات معدنية";
            transform.GetChild(1).GetChild(2).GetComponent<Kozykin.MultiLanguageItem>().text = Reward.quantity + " عملات معدنية";
        }
        else if (Reward.reward == RewardType.gems)
        {
            transform.GetChild(1).GetChild(2).GetComponent<Text>().text = Reward.quantity + " Gems";
            transform.GetChild(1).GetChild(2).GetComponent<Kozykin.MultiLanguageItem>().strItems[0] = Reward.quantity + " Gems";
            transform.GetChild(1).GetChild(2).GetComponent<Kozykin.MultiLanguageItem>().strItems[1] = Reward.quantity + " الجواهر";
            transform.GetChild(1).GetChild(2).GetComponent<Kozykin.MultiLanguageItem>().text = Reward.quantity + " الجواهر";
        }
        else if (Reward.reward == RewardType.luckySpin)
        {
            transform.GetChild(1).GetChild(2).GetComponent<Text>().text = Reward.quantity + " Lucky Spin";
            transform.GetChild(1).GetChild(2).GetComponent<Kozykin.MultiLanguageItem>().strItems[0] = Reward.quantity + " Lucky Spin";
            transform.GetChild(1).GetChild(2).GetComponent<Kozykin.MultiLanguageItem>().strItems[1] = Reward.quantity + " لاكي سبين";
            transform.GetChild(1).GetChild(2).GetComponent<Kozykin.MultiLanguageItem>().text = Reward.quantity + " لاكي سبين";
        }
        else if (Reward.reward == RewardType.GoldenSpin)
        {
            transform.GetChild(1).GetChild(2).GetComponent<Text>().text = Reward.quantity + " Golden Spin";
            transform.GetChild(1).GetChild(2).GetComponent<Kozykin.MultiLanguageItem>().strItems[0] = Reward.quantity + " Golden Spin";
            transform.GetChild(1).GetChild(2).GetComponent<Kozykin.MultiLanguageItem>().strItems[1] = Reward.quantity + " غولدن سبين";
            transform.GetChild(1).GetChild(2).GetComponent<Kozykin.MultiLanguageItem>().text = Reward.quantity + " غولدن سبين";
        }
        else if (Reward.reward == RewardType.Frames)
        {
            transform.GetChild(1).GetChild(2).GetComponent<Text>().text = Reward.quantity + " Frame";
            transform.GetChild(1).GetChild(2).GetComponent<Kozykin.MultiLanguageItem>().strItems[0] = Reward.quantity + " Frame";
            transform.GetChild(1).GetChild(2).GetComponent<Kozykin.MultiLanguageItem>().strItems[1] = Reward.quantity + " إطار";
            transform.GetChild(1).GetChild(2).GetComponent<Kozykin.MultiLanguageItem>().text = Reward.quantity + " إطار";
        }
        else if (Reward.reward == RewardType.Gifts)
        {
            transform.GetChild(1).GetChild(2).GetComponent<Text>().text = Reward.quantity + " Gifts";
            transform.GetChild(1).GetChild(2).GetComponent<Kozykin.MultiLanguageItem>().strItems[0] = Reward.quantity + " Gifts";
            transform.GetChild(1).GetChild(2).GetComponent<Kozykin.MultiLanguageItem>().strItems[1] = Reward.quantity + " الهدايا";
            transform.GetChild(1).GetChild(2).GetComponent<Kozykin.MultiLanguageItem>().text = Reward.quantity + " الهدايا";
        }

        transform.GetChild(3).GetComponent<Text>().text = Reward_Trophies;
        transform.GetChild(3).GetComponent<Kozykin.MultiLanguageItem>().strItems[0] = Reward_Trophies;
        transform.GetChild(3).GetComponent<Kozykin.MultiLanguageItem>().strItems[1] = Reward_Trophies;
        transform.GetChild(3).GetComponent<Kozykin.MultiLanguageItem>().text = Reward_Trophies;
    }

    // Update is called once per frame
    public override void Update()
    {
        base.Update();
    }
}
