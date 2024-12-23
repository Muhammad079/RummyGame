using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SettingsPanel : MonoBehaviour
{
    //public Sprite Selected_Lang, UnSelected_Lang;
    bool toggle_Reminder = false;
    [SerializeField] private string policyUrl = "";
    [SerializeField] private Button privacyPolicyButton = null;
    [SerializeField] private Button reminderToggle = null;
    [SerializeField] private Sprite reminderActive =null, reminderDeactive=null;
    [SerializeField] private Slider Music_Volume = null, Sound_Effects_Volume = null;
    [SerializeField] private Button Music_btn = null, Sound_Effects_btn = null;
    [SerializeField] private Sprite[] Music_Switch_Sprite = null, Sound_Effects_Switch=null;
    void Start()
    {
        if (toggle_Reminder)
            reminderToggle.GetComponent<Image>().sprite= reminderActive ;
        else
            reminderToggle.GetComponent<Image>().sprite = reminderDeactive;
        privacyPolicyButton.onClick.AddListener(OpenPrivacyPolicy);
        reminderToggle.onClick.AddListener(ToggleClick);

        if(SceneManager.GetActiveScene().name.Contains("Home"))
        {
            Music_btn.onClick.AddListener(Music_btn_Handling);
        }
        else
        {
            Music_btn.onClick.AddListener(Music_btn_Handling_GAMEPLAY);
        }
        
        Music_Volume.onValueChanged.AddListener(Music_Handling);
        Sound_Effects_btn.onClick.AddListener(Sound_Effects_btn_Handling);
        Sound_Effects_Volume.onValueChanged.AddListener(Sound_Effects_Handling);

        if(ProfileManager.instance.currentPlayer.isMusicActive)
        {
            Music_btn.GetComponent<Image>().sprite = Music_Switch_Sprite[0];
        }
        else
        {
            Music_btn.GetComponent<Image>().sprite = Music_Switch_Sprite[1];
        }
        if(ProfileManager.instance.currentPlayer.isSoundActive)
        {
            Sound_Effects_btn.GetComponent<Image>().sprite = Sound_Effects_Switch[0];
        }
        else
        {
            Sound_Effects_btn.GetComponent<Image>().sprite = Sound_Effects_Switch[1];
        }
    }

    private void Sound_Effects_Handling(float value)
    {
        if(value ==0)
        {
            Sound_Effects_btn.GetComponent<Image>().sprite = Sound_Effects_Switch[0];
            ProfileManager.instance.currentPlayer.isSoundActive = true;
        }
        else
        {
            Sound_Effects_btn.GetComponent<Image>().sprite = Sound_Effects_Switch[1];
            ProfileManager.instance.currentPlayer.isSoundActive = false;
        }
        ProfileManager.instance.currentPlayer.Sound_Effects_Volume = (int)(value*10);
        //ProfileManager.instance.SaveUserData();
    }

    private void Sound_Effects_btn_Handling()
    {
        if (ProfileManager.instance.currentPlayer.isSoundActive)
        {
            Sound_Effects_btn.GetComponent<Image>().sprite = Sound_Effects_Switch[0];
            ProfileManager.instance.currentPlayer.isSoundActive = false;
        }
        else if (!ProfileManager.instance.currentPlayer.isSoundActive)
        {
            Sound_Effects_btn.GetComponent<Image>().sprite = Sound_Effects_Switch[1];
            ProfileManager.instance.currentPlayer.isSoundActive = true;
            Sound_Effects_Volume.value = ProfileManager.instance.currentPlayer.Sound_Effects_Volume;

        }
        //ProfileManager.instance.SaveUserData();
    }

    private void Music_Handling(float value)
    {
        if(value==0)
        {
            Music_btn.GetComponent<Image>().sprite = Music_Switch_Sprite[0];
            ProfileManager.instance.currentPlayer.isMusicActive = true;

        }
        else
        {
            Music_btn.GetComponent<Image>().sprite = Music_Switch_Sprite[1];
            ProfileManager.instance.currentPlayer.isMusicActive = false;
        }
        ProfileManager.instance.currentPlayer.Music_Volume = (int)(value * 10); 
        //ProfileManager.instance.SaveUserData();
    }

    private void Music_btn_Handling_GAMEPLAY()
    {
        if (ProfileManager.instance.currentPlayer.isMusicActive)
        {
            Music_btn.GetComponent<Image>().sprite = Music_Switch_Sprite[0];
            ProfileManager.instance.currentPlayer.isMusicActive = true;
        }
        else if (!ProfileManager.instance.currentPlayer.isMusicActive)
        {
            Music_btn.GetComponent<Image>().sprite = Music_Switch_Sprite[1];
            ProfileManager.instance.currentPlayer.isMusicActive = false;
            Music_Volume.value = ProfileManager.instance.currentPlayer.Music_Volume;

        }
        //ProfileManager.instance.SaveUserData();
    }

    private void Music_btn_Handling()
    {
        if(ProfileManager.instance.currentPlayer.isMusicActive)
        {
            Music_btn.GetComponent<Image>().sprite = Music_Switch_Sprite[0];
            ProfileManager.instance.currentPlayer.isMusicActive = false;
        }
        else if (!ProfileManager.instance.currentPlayer.isMusicActive)
        {
            Music_btn.GetComponent<Image>().sprite = Music_Switch_Sprite[1];
            ProfileManager.instance.currentPlayer.isMusicActive = true;
            Music_Volume.value = ProfileManager.instance.currentPlayer.Music_Volume;

        }
        //ProfileManager.instance.SaveUserData();
    }

    void ToggleClick()
    {
        if (toggle_Reminder)
        {
            toggle_Reminder = false;
            reminderToggle.GetComponent<Image>().sprite = reminderDeactive;
        }
        else
        {
            reminderToggle.GetComponent<Image>().sprite = reminderActive;
            toggle_Reminder = true;
        }
    }
    void OpenPrivacyPolicy()
    {
        Application.OpenURL(policyUrl);
    }
    public void Show()
    {
        if(SceneManager.GetActiveScene().name.Contains("GamePlay"))
        {
            this.gameObject.SetActive(true);
            transform.DOScale(new Vector3(2.5f, 2.5f, 2.5f), 0.2f).SetEase(Ease.InCubic);
        }
        else
        {
            this.gameObject.SetActive(true);
            transform.DOScale(Vector3.one, 0.2f).SetEase(Ease.InCubic);
        }
       
    }
    public void Cross()
    {
        transform.DOScale(Vector3.zero, 0.2f).SetEase(Ease.InCubic) ;
    }
}
