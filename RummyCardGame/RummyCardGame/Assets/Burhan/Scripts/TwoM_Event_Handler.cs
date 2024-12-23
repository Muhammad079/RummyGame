using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TwoM_Event_Handler : SceneLoader
{
    public Button Play_btn, Close_btn, Rules_btn, Rules_Close_btn;
    public GameObject Rules_Panel;
    public int Entry_fee;
    public GameObject[] Levels;
    public Text Timer;
    int days_left = 0;
    public GameObject Loading_Panel;
    public Text Loading_Text;

    private void OnEnable()
    {
        transform.DOScale(new Vector3(1, 1, 1), 0.2f);
    }
    // Start is called before the first frame update
    void Start()
    {
        GameManager.instance.selectedBid = Entry_fee;
        //GameManager.instance.selectedTable.totalPlayers = 2;
        GameManager.instance.selectedTournament = TournamentType.twoMEvents;
        Play_btn.interactable = true;

        if (!ProfileManager.instance.currentPlayer.TwoM_FeeCheck)
        {
            
            Play_btn.transform.GetChild(0).gameObject.SetActive(true);
            Play_btn.transform.GetChild(1).gameObject.SetActive(false);
            
        }
        else
        {
            Play_btn.transform.GetChild(0).gameObject.SetActive(false);
            Play_btn.transform.GetChild(1).gameObject.SetActive(true);
        }
        if(ProfileManager.instance.currentPlayer.TwoM_Loses>=3)
        {
            Play_btn.interactable = false;
        }
        else
        {
            Play_btn.interactable = true;
            Play_btn.onClick.AddListener(StartGame);
        }
        if(ProfileManager.instance.currentPlayer.TwoM_Wins>=12)
        {
            if(!ProfileManager.instance.currentPlayer.TwoM_Claim_Reward)
            {
                ProfileManager.instance.currentPlayer.TwoM_Claim_Reward = true;
                ProfileManager.instance.GetReward(RewardType.coins, 2000000); //REWARD
            }
            
            Play_btn.interactable = false;
        }
        for(int i=0;i< ProfileManager.instance.currentPlayer.TwoM_Wins;i++)
        {
            Levels[i].GetComponent<Image>().color = Color.green;
        }


        Close_btn.onClick.AddListener(Close_Panel);
        Rules_btn.onClick.AddListener(Rules_panel);
        Rules_Close_btn.onClick.AddListener(Close_Panel);


        int Days_Gone = DateTime.Today.ToUniversalTime().DayOfYear - ProfileManager.instance.currentPlayer.TwoM_Start_day;
        days_left = ProfileManager.instance.currentPlayer.TwoM_Duration - Days_Gone;
        Debug.Log("Days Left: " + days_left);
        if (days_left <= -1)
        {
            Debug.Log("2M Event Ended");
            Timer.text = "EVENT ENDED";
            Play_btn.interactable = false;

            Timer.GetComponent<Kozykin.MultiLanguageItem>().strItems[0] = "EVENT ENDED";
            Timer.GetComponent<Kozykin.MultiLanguageItem>().strItems[1] = "انتهى الحدث";
            Timer.GetComponent<Kozykin.MultiLanguageItem>().text = "انتهى الحدث";
        }





        ProfileManager.instance.SaveUserData();
    }

    private void Rules_panel()
    {
        Rules_Panel.SetActive(true);
        Rules_Panel.transform.DOScale(new Vector3(1, 1, 1), 0.2f);
    }

    private void Close_Panel()
    {
        if(!Rules_Panel.activeInHierarchy)
        {
            GameManager.instance.selectedTournament = TournamentType.empty;
            transform.DOScale(new Vector3(0, 0, 0), 0.2f).OnComplete(() => {
                StartCoroutine(OnClick());
            });
        }
        else
        {
            Rules_Panel.transform.DOScale(new Vector3(0, 0, 0), 0.2f).OnComplete(() =>
            {
                Rules_Panel.SetActive(false);
            });
        }
        
        
    }


    private void StartGame()
    {
        if (!ProfileManager.instance.currentPlayer.TwoM_FeeCheck)
        {
            if(ProfileManager.instance.currentPlayer.coins>= Entry_fee)
            {
                ProfileManager.instance.currentPlayer.coins -= Entry_fee;
                ProfileManager.instance.currentPlayer.TwoM_FeeCheck = true;


                GameManager.instance.selectedBid = Entry_fee;
                GameManager.instance.selectedTable.totalPlayers = 2;

                GameManager.instance.selectedTable.firstPosMultiplier = 1.6f;
                GameManager.instance.selectedTable.secondPosPercentage = 2;
                GameManager.instance.selectedTable.thirdPosPercentage = 1;
                GameManager.instance.selectedXPData = GameManager.instance.xpData_2Players;

                PhotonNetworkingManager.instance.JoinTable();
                //Searching_Panel.SetActive(true);

                ProfileManager.instance.SaveUserData();
            }
            else
            {
                GameManager.instance.Not_Enough_C_Panel.SetActive(true);
                GameManager.instance.Not_Enough_C_Panel.transform.DOScale(new Vector3(1, 1, 1), 0.5f).SetEase(Ease.OutBounce);
            }
            
        }
        else
        {
            GameManager.instance.selectedBid = Entry_fee;
            GameManager.instance.selectedTable.totalPlayers = 2;

            GameManager.instance.selectedTable.firstPosMultiplier = 1.6f;
            GameManager.instance.selectedTable.secondPosPercentage = 2;
            GameManager.instance.selectedTable.thirdPosPercentage = 1;
            GameManager.instance.selectedXPData = GameManager.instance.xpData_2Players;

            PhotonNetworkingManager.instance.JoinTable();
            //Searching_Panel.SetActive(true);

            ProfileManager.instance.SaveUserData();
        }
        
    }


    // Update is called once per frame
    public override void Update()
    {
        base.Update();

        if(gameObject.activeInHierarchy)
        {
            if (days_left > 0)
            {
                DateTime now = DateTime.Now;
                DateTime tomorrow = DateTime.Today.AddDays(days_left);
                TimeSpan remaining = tomorrow - now;
                string time = new DateTime(remaining.Ticks).ToString("dD|hh:mm:ss");
                Timer.text = "EVENT ENDS IN: " + time;


                Timer.GetComponent<Kozykin.MultiLanguageItem>().strItems[0] = "EVENT ENDS IN: " + time;
                Timer.GetComponent<Kozykin.MultiLanguageItem>().strItems[1] = "ينتهي الحدث في: " + time;
                Timer.GetComponent<Kozykin.MultiLanguageItem>().text = "ينتهي الحدث في: " + time;

            }

            
        }
    }
}
