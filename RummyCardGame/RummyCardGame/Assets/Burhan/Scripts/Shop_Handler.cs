using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;
public class Shop_Handler : SceneLoader
{
    public Button back;
    public GameObject Timer, Timer2;
    int days_left_VIP7, days_left_VIP30;
    //public Text Coins, Gems;

    private void OnEnable()
    {
        gameObject.SetActive(true);
        transform.DOScale(new Vector3(1, 1, 1), 0.2f).SetEase(Ease.OutSine);
    }
    void Start()
    {
        Timer.SetActive(false);
        Timer2.SetActive(false);
        back.onClick.AddListener(() => {
            transform.DOScale(new Vector3(0, 0, 0), 0.2f).SetEase(Ease.InSine).OnComplete(() => {
                
                if(SceneManager.GetActiveScene().buildIndex==3)
                {
                    Loading_Screen = GameObject.Find("Loading_Screen");
                    Loading_animation = Loading_Screen.transform.GetChild(0).GetComponent<Text>();
                    StartCoroutine(OnClick());
                    //GameManager.instance.sceneToLoad = "Home";

                    //UnityEngine.SceneManagement.SceneManager.LoadScene("Home");
                }
                else
                {
                    gameObject.SetActive(false);
                }
            });
        });



        if(ProfileManager.instance.currentPlayer.isVip)
        {
            int Days_Gone_VIP7 = DateTime.Today.ToUniversalTime().DayOfYear - ProfileManager.instance.currentPlayer.VIP_7days_Start_Day;
            days_left_VIP7 = ProfileManager.instance.currentPlayer.VIP_7days_count - Days_Gone_VIP7;
            Debug.Log("Days Left: " + days_left_VIP7);
            if (days_left_VIP7 <= -1)
            {
                //ProfileManager.instance.currentPlayer.isVip = false;
                ProfileManager.instance.currentPlayer.VIP_7days_Start_Day = 0;
                ProfileManager.instance.currentPlayer.VIP_7days_count = 0;
                Debug.Log("VIPmini ended");
                Timer.GetComponent<Text>().text = "";
                Timer.GetComponent<Kozykin.MultiLanguageItem>().strItems[0] = "";
                Timer.GetComponent<Kozykin.MultiLanguageItem>().strItems[1] = "";
                Timer.GetComponent<Kozykin.MultiLanguageItem>().text = "";
            }


            int Days_Gone_VIP30 = DateTime.Today.ToUniversalTime().DayOfYear - ProfileManager.instance.currentPlayer.VIP_30days_Start_Day;
            days_left_VIP30 = ProfileManager.instance.currentPlayer.VIP_30days_count - Days_Gone_VIP30;
            Debug.Log("Days Left: " + days_left_VIP30);
            if (days_left_VIP30 <= -1)
            {
                //ProfileManager.instance.currentPlayer.isVip = false;
                ProfileManager.instance.currentPlayer.VIP_30days_Start_Day = 0;
                ProfileManager.instance.currentPlayer.VIP_30days_count = 0;
                Debug.Log("VIPmega ended");
                Timer2.GetComponent<Text>().text = "";
                Timer2.GetComponent<Kozykin.MultiLanguageItem>().strItems[0] = "";
                Timer2.GetComponent<Kozykin.MultiLanguageItem>().strItems[1] = "";
                Timer2.GetComponent<Kozykin.MultiLanguageItem>().text = "";

            }

            if (days_left_VIP7 <= -1 && days_left_VIP30 <= -1)
            {
                ProfileManager.instance.currentPlayer.isVip = false;
            }
        }
    }
    public override void Update()
    {
        base.Update();
        if (ProfileManager.instance.currentPlayer.isVip)
        {
            if (ProfileManager.instance.currentPlayer.VIP_7days_count > 0)
            {
                if (days_left_VIP7 > 0)
                {
                    Timer.SetActive(true);
                    DateTime now = DateTime.Now;
                    DateTime tomorrow = DateTime.Today.AddDays(days_left_VIP7);
                    TimeSpan remaining = tomorrow - now;
                    string time = new DateTime(remaining.Ticks).ToString("dD|hh:mm:ss");
                    Timer.GetComponent<Text>().text = "Your VIP Mini ends in: " + time;
                    Timer.GetComponent<Kozykin.MultiLanguageItem>().strItems[0] = "Your VIP Mini ends in: " + time;
                    Timer.GetComponent<Kozykin.MultiLanguageItem>().strItems[1] = "تنتهي مدة الـ VIP بعد: " + time;
                    Timer.GetComponent<Kozykin.MultiLanguageItem>().text = "تنتهي مدة الـ VIP بعد: " + time;
                }
                //Timer.SetActive(true);
                //Timer.GetComponent<Text>().text = "Your VIP Mini ends in: " + ProfileManager.instance.currentPlayer.VIP_7days_count + " Days";
            }
            if (ProfileManager.instance.currentPlayer.VIP_30days_count > 0)
            {
                if (days_left_VIP30 > 0)
                {
                    Timer2.SetActive(true);
                    DateTime now = DateTime.Now;
                    DateTime tomorrow = DateTime.Today.AddDays(days_left_VIP30);
                    TimeSpan remaining = tomorrow - now;
                    string time = new DateTime(remaining.Ticks).ToString("dD|hh:mm:ss");
                    Timer2.GetComponent<Text>().text = "Your VIP Mega ends in: " + time;
                    Timer2.GetComponent<Kozykin.MultiLanguageItem>().strItems[0] = "Your VIP Mini ends in: " + time;
                    Timer2.GetComponent<Kozykin.MultiLanguageItem>().strItems[1] = "تنتهي مدة الـ VIP بعد: " + time;
                    Timer2.GetComponent<Kozykin.MultiLanguageItem>().text = "تنتهي مدة الـ VIP بعد: " + time;
                }
                //Timer2.SetActive(true);
                //Timer2.GetComponent<Text>().text = "Your VIP Mega ends in: " + ProfileManager.instance.currentPlayer.VIP_30days_count + " Days";
            }
            
        }
    }
}