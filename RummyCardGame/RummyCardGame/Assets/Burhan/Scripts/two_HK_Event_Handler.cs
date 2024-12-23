using DG.Tweening;
using Photon.Pun;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class two_HK_Event_Handler : SceneLoader
{
    public scriptable_twoHK_Event two_HK_data;
    public GameObject[] Tables;
    public Button Back_btn, Info_btn;
    public Text Player_Crowns, Timer, Coins, Gems;
    public GameObject Info_Panel;

    public static bool is_TwoHK_ended;

    int Table1_Crowns;
    int Table2_Crowns;
    int Table3_Crowns;
    int Table4_Crowns;
    int days_left = 0;


    private void OnEnable()
    {
        transform.DOScale(new Vector3(1, 1, 1), 0.2f);
    }
    // Start is called before the first frame update
    void Start()
    {
        is_TwoHK_ended = false;

        

        ProfileManager.instance.currentPlayer.TwoHK_Selected = true;
        for (int i = 0; i < Tables.Length; i++)
        {
            Tables[i].GetComponent<twoHK_Event_Tables>().two_HK_data = two_HK_data.two_HK_Tables[i];
        }

        int Days_Gone = DateTime.Today.ToUniversalTime().DayOfYear - ProfileManager.instance.currentPlayer.TwoM_Start_day;
        days_left = ProfileManager.instance.currentPlayer.TwoM_Duration - Days_Gone;
        Debug.Log("Days Left: " + days_left);
        if (days_left <= -1)
        {
            Debug.Log("200K Event Ended");
            is_TwoHK_ended = true;

            Timer.GetComponent<Kozykin.MultiLanguageItem>().strItems[0] = "EVENT ENDED";
            Timer.GetComponent<Kozykin.MultiLanguageItem>().strItems[1] = "انتهى الحدث";
            Timer.GetComponent<Kozykin.MultiLanguageItem>().text = "انتهى الحدث";
                
            //Play_btn.interactable = false;
        }


        Back_btn.onClick.AddListener(Back);
        
        Info_btn.onClick.AddListener(Rules_panel);
        Invoke(nameof(refresh), 0.2f);
    }
    private void Rules_panel()
    {
        Info_Panel.SetActive(true);
        Info_Panel.transform.DOScale(new Vector3(1, 1, 1), 0.2f);

        Info_Panel.transform.GetChild(0).GetChild(1).GetComponent<Button>().onClick.AddListener(()=> {
            Info_Panel.transform.DOScale(new Vector3(0, 0, 0), 0.2f).OnComplete(() => {
                Info_Panel.SetActive(false);
            });
        });
    }
    private void refresh()
    {
        ProfileManager.instance.currentPlayer.TwoHK_Crowns = 0;
        ProfileManager.instance.currentPlayer.TwoHK_Table_Selected = 1;
        Table1_Crowns = 0;
        Table2_Crowns = 0;
        Table3_Crowns = 0;
        Table4_Crowns = 0;
        for (int i = 0; i < ProfileManager.instance.currentPlayer.TwoHK_Wins; i++)
        {
            ProfileManager.instance.currentPlayer.TwoHK_Crowns += 4;
        }
        for (int i = 0; i < ProfileManager.instance.currentPlayer.TwoHK_Loses; i++)
        {
            ProfileManager.instance.currentPlayer.TwoHK_Crowns -= 2;
            if(ProfileManager.instance.currentPlayer.TwoHK_Crowns<=0)
            {
                ProfileManager.instance.currentPlayer.TwoHK_Crowns = 0;
                ProfileManager.instance.currentPlayer.TwoHK_Loses = 0;
            }
        }
        

        for (int i = 0; i < Tables.Length; i++)
        {
            Tables[i].GetComponent<twoHK_Event_Tables>().In_Filler_trophies.text = "0";
            Tables[i].GetComponent<twoHK_Event_Tables>().In_Filler_trophies.GetComponent<Kozykin.MultiLanguageItem>().strItems[0] = "0";
            Tables[i].GetComponent<twoHK_Event_Tables>().In_Filler_trophies.GetComponent<Kozykin.MultiLanguageItem>().strItems[1] = "0";
            Tables[i].GetComponent<twoHK_Event_Tables>().In_Filler_trophies.GetComponent<Kozykin.MultiLanguageItem>().text = "0";



        }



        if (ProfileManager.instance.currentPlayer.TwoHK_Table_Selected == 1)
        {
            if (ProfileManager.instance.currentPlayer.TwoHK_Crowns > 50)
            {
                if(!ProfileManager.instance.currentPlayer.TwoHK_Claim_Reward_ID.Contains(1))
                {
                    ProfileManager.instance.currentPlayer.TwoHK_Claim_Reward_ID.Add(1);
                    ProfileManager.instance.GetReward(RewardType.coins, Tables[0].GetComponent<twoHK_Event_Tables>().two_HK_data.Total_Prize);
                }
                ProfileManager.instance.currentPlayer.TwoHK_Table_Selected = 2;

            }
            else
            {
                Table1_Crowns = ProfileManager.instance.currentPlayer.TwoHK_Crowns;
                Tables[0].GetComponent<twoHK_Event_Tables>().In_Filler_trophies.text = Table1_Crowns.ToString();
                Tables[0].GetComponent<twoHK_Event_Tables>().In_Filler_trophies.GetComponent<Kozykin.MultiLanguageItem>().strItems[0] = Table1_Crowns.ToString();
                Tables[0].GetComponent<twoHK_Event_Tables>().In_Filler_trophies.GetComponent<Kozykin.MultiLanguageItem>().strItems[1] = Table1_Crowns.ToString();
                Tables[0].GetComponent<twoHK_Event_Tables>().In_Filler_trophies.GetComponent<Kozykin.MultiLanguageItem>().text = Table1_Crowns.ToString();
                Tables[0].GetComponent<twoHK_Event_Tables>().Filler.fillAmount = (float)Table1_Crowns / 50;
                Tables[1].GetComponent<twoHK_Event_Tables>().Filler.fillAmount = 0;
                Tables[2].GetComponent<twoHK_Event_Tables>().Filler.fillAmount = 0;
                Tables[3].GetComponent<twoHK_Event_Tables>().Filler.fillAmount = 0;
            }
        }
        if (ProfileManager.instance.currentPlayer.TwoHK_Table_Selected == 2)
        {
            Table1_Crowns = 50;
            Tables[0].GetComponent<twoHK_Event_Tables>().In_Filler_trophies.text = Table1_Crowns.ToString();
            Tables[0].GetComponent<twoHK_Event_Tables>().In_Filler_trophies.GetComponent<Kozykin.MultiLanguageItem>().strItems[0] = Table1_Crowns.ToString();
            Tables[0].GetComponent<twoHK_Event_Tables>().In_Filler_trophies.GetComponent<Kozykin.MultiLanguageItem>().strItems[1] = Table1_Crowns.ToString();
            Tables[0].GetComponent<twoHK_Event_Tables>().In_Filler_trophies.GetComponent<Kozykin.MultiLanguageItem>().text = Table1_Crowns.ToString();
            Tables[0].GetComponent<twoHK_Event_Tables>().Filler.fillAmount = 1;
            if (ProfileManager.instance.currentPlayer.TwoHK_Crowns > 100)
            {
                if (!ProfileManager.instance.currentPlayer.TwoHK_Claim_Reward_ID.Contains(2))
                {
                    ProfileManager.instance.currentPlayer.TwoHK_Claim_Reward_ID.Add(2);
                    ProfileManager.instance.GetReward(RewardType.coins, Tables[1].GetComponent<twoHK_Event_Tables>().two_HK_data.Total_Prize);
                }
                ProfileManager.instance.currentPlayer.TwoHK_Table_Selected = 3;

            }
            else
            {
                Table2_Crowns = ProfileManager.instance.currentPlayer.TwoHK_Crowns - Table1_Crowns;
                Tables[1].GetComponent<twoHK_Event_Tables>().In_Filler_trophies.text = Table2_Crowns.ToString();
                Tables[1].GetComponent<twoHK_Event_Tables>().In_Filler_trophies.GetComponent<Kozykin.MultiLanguageItem>().strItems[0] = Table2_Crowns.ToString();
                Tables[1].GetComponent<twoHK_Event_Tables>().In_Filler_trophies.GetComponent<Kozykin.MultiLanguageItem>().strItems[1] = Table2_Crowns.ToString();
                Tables[1].GetComponent<twoHK_Event_Tables>().In_Filler_trophies.GetComponent<Kozykin.MultiLanguageItem>().text = Table2_Crowns.ToString();
                Tables[1].GetComponent<twoHK_Event_Tables>().Filler.fillAmount = (float)Table2_Crowns / 100;
                Tables[2].GetComponent<twoHK_Event_Tables>().Filler.fillAmount = 0;
                Tables[3].GetComponent<twoHK_Event_Tables>().Filler.fillAmount = 0;
            }

        }
        if (ProfileManager.instance.currentPlayer.TwoHK_Table_Selected == 3)
        {
            Table2_Crowns = 100;
            Tables[1].GetComponent<twoHK_Event_Tables>().In_Filler_trophies.text = Table2_Crowns.ToString();
            Tables[1].GetComponent<twoHK_Event_Tables>().In_Filler_trophies.GetComponent<Kozykin.MultiLanguageItem>().strItems[0] = Table2_Crowns.ToString();
            Tables[1].GetComponent<twoHK_Event_Tables>().In_Filler_trophies.GetComponent<Kozykin.MultiLanguageItem>().strItems[1] = Table2_Crowns.ToString();
            Tables[1].GetComponent<twoHK_Event_Tables>().In_Filler_trophies.GetComponent<Kozykin.MultiLanguageItem>().text = Table2_Crowns.ToString();
            Tables[1].GetComponent<twoHK_Event_Tables>().Filler.fillAmount = 1;
            if (ProfileManager.instance.currentPlayer.TwoHK_Crowns > 150)
            {
                if (!ProfileManager.instance.currentPlayer.TwoHK_Claim_Reward_ID.Contains(3))
                {
                    ProfileManager.instance.currentPlayer.TwoHK_Claim_Reward_ID.Add(3);
                    ProfileManager.instance.GetReward(RewardType.coins, Tables[2].GetComponent<twoHK_Event_Tables>().two_HK_data.Total_Prize);
                }
                ProfileManager.instance.currentPlayer.TwoHK_Table_Selected = 4;

            }
            else
            {
                Table3_Crowns = ProfileManager.instance.currentPlayer.TwoHK_Crowns - Table2_Crowns;
                Tables[2].GetComponent<twoHK_Event_Tables>().In_Filler_trophies.text = Table3_Crowns.ToString();
                Tables[2].GetComponent<twoHK_Event_Tables>().In_Filler_trophies.GetComponent<Kozykin.MultiLanguageItem>().strItems[0] = Table3_Crowns.ToString();
                Tables[2].GetComponent<twoHK_Event_Tables>().In_Filler_trophies.GetComponent<Kozykin.MultiLanguageItem>().strItems[1] = Table3_Crowns.ToString();
                Tables[2].GetComponent<twoHK_Event_Tables>().In_Filler_trophies.GetComponent<Kozykin.MultiLanguageItem>().text = Table3_Crowns.ToString();
                Tables[2].GetComponent<twoHK_Event_Tables>().Filler.fillAmount = (float)Table3_Crowns / 150;
                Tables[3].GetComponent<twoHK_Event_Tables>().Filler.fillAmount = 0;
            }

        }
        if (ProfileManager.instance.currentPlayer.TwoHK_Table_Selected == 4)
        {
            Table3_Crowns = 150;
            Tables[2].GetComponent<twoHK_Event_Tables>().In_Filler_trophies.text = Table3_Crowns.ToString();
            Tables[2].GetComponent<twoHK_Event_Tables>().In_Filler_trophies.GetComponent<Kozykin.MultiLanguageItem>().strItems[0] = Table3_Crowns.ToString();
            Tables[2].GetComponent<twoHK_Event_Tables>().In_Filler_trophies.GetComponent<Kozykin.MultiLanguageItem>().strItems[1] = Table3_Crowns.ToString();
            Tables[2].GetComponent<twoHK_Event_Tables>().In_Filler_trophies.GetComponent<Kozykin.MultiLanguageItem>().text = Table3_Crowns.ToString();
            Tables[2].GetComponent<twoHK_Event_Tables>().Filler.fillAmount = 1;
            Table4_Crowns = ProfileManager.instance.currentPlayer.TwoHK_Crowns - Table3_Crowns;
            Tables[3].GetComponent<twoHK_Event_Tables>().In_Filler_trophies.text = Table4_Crowns.ToString();
            Tables[3].GetComponent<twoHK_Event_Tables>().In_Filler_trophies.GetComponent<Kozykin.MultiLanguageItem>().strItems[0] = Table4_Crowns.ToString();
            Tables[3].GetComponent<twoHK_Event_Tables>().In_Filler_trophies.GetComponent<Kozykin.MultiLanguageItem>().strItems[1] = Table4_Crowns.ToString();
            Tables[3].GetComponent<twoHK_Event_Tables>().In_Filler_trophies.GetComponent<Kozykin.MultiLanguageItem>().text = Table4_Crowns.ToString();
            Tables[3].GetComponent<twoHK_Event_Tables>().Filler.fillAmount = (float)Table4_Crowns / 300;

            if(Table4_Crowns>=300)
            {
                if (!ProfileManager.instance.currentPlayer.TwoHK_Claim_Reward_ID.Contains(1))
                {
                    ProfileManager.instance.currentPlayer.TwoHK_Claim_Reward_ID.Add(1);
                    ProfileManager.instance.GetReward(RewardType.coins, Tables[0].GetComponent<twoHK_Event_Tables>().two_HK_data.Total_Prize);
                }
            }
        }


        for (int i = 0; i < Tables.Length; i++)
        {
            if (ProfileManager.instance.currentPlayer.TwoHK_Table_Selected >= Tables[i].GetComponent<twoHK_Event_Tables>().ID)
            {
                Tables[i].GetComponent<twoHK_Event_Tables>().Lock.SetActive(false);
                Tables[i].GetComponent<twoHK_Event_Tables>().Play_btn.interactable = true;
            }
            else
            {
                Tables[i].GetComponent<twoHK_Event_Tables>().Play_btn.interactable = false;
            }
        }
    }

    private void Back()
    {
        ProfileManager.instance.currentPlayer.TwoHK_Selected = false;
        transform.DOScale(new Vector3(0, 0, 0), 0.2f).OnComplete(()=> {
            StartCoroutine(OnClick());
            //if (PhotonNetwork.InRoom)
            //    PhotonNetwork.LeaveRoom();
            //Resources.UnloadUnusedAssets();
            //GameManager.instance.sceneToLoad = "Home";
            //SceneManager.LoadScene("Home");
        });
    }

    // Update is called once per frame
    public override void Update()
    {
        base.Update();
        if (gameObject.activeInHierarchy)
        {
            if (days_left > 0)
            {
                Coins.text = ProfileManager.instance.currentPlayer.coins.ToString();
                Gems.text = ProfileManager.instance.currentPlayer.gems.ToString();
                Player_Crowns.text = ProfileManager.instance.currentPlayer.TwoHK_Crowns.ToString();
                DateTime now = DateTime.Now;
                DateTime tomorrow = DateTime.Today.AddDays(days_left);
                TimeSpan remaining = tomorrow - now;
                string time = new DateTime(remaining.Ticks).ToString("dD|hh:mm:ss");
                Timer.text = "EVENT ENDS IN: " + time;


                Coins.GetComponent<Kozykin.MultiLanguageItem>().strItems[0] = ProfileManager.instance.currentPlayer.coins.ToString();
                Gems.GetComponent<Kozykin.MultiLanguageItem>().strItems[0] = ProfileManager.instance.currentPlayer.gems.ToString();
                Player_Crowns.GetComponent<Kozykin.MultiLanguageItem>().strItems[0] = ProfileManager.instance.currentPlayer.TwoHK_Crowns.ToString();
                Timer.GetComponent<Kozykin.MultiLanguageItem>().strItems[0] = "EVENT ENDS IN: " + time;

                Coins.GetComponent<Kozykin.MultiLanguageItem>().strItems[1] = ProfileManager.instance.currentPlayer.coins.ToString();
                Coins.GetComponent<Kozykin.MultiLanguageItem>().text = ProfileManager.instance.currentPlayer.coins.ToString();
                Gems.GetComponent<Kozykin.MultiLanguageItem>().strItems[1] = ProfileManager.instance.currentPlayer.gems.ToString();
                Gems.GetComponent<Kozykin.MultiLanguageItem>().text = ProfileManager.instance.currentPlayer.gems.ToString();
                Player_Crowns.GetComponent<Kozykin.MultiLanguageItem>().strItems[1] = ProfileManager.instance.currentPlayer.TwoHK_Crowns.ToString();
                Player_Crowns.GetComponent<Kozykin.MultiLanguageItem>().text = ProfileManager.instance.currentPlayer.TwoHK_Crowns.ToString();
                Timer.GetComponent<Kozykin.MultiLanguageItem>().strItems[1] = "ينتهي الحدث في: " + time;
                Timer.GetComponent<Kozykin.MultiLanguageItem>().text = "ينتهي الحدث في: " + time;


            }
            
        }
    }
}
