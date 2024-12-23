using System;
using UnityEngine;
using UnityEngine.UI;
public class ProfileEditHandler : MonoBehaviour
{
    [SerializeField] private InputField userInputField = null;
    [SerializeField] private Button Change_btn = null;
    [SerializeField] private Transform framesContent = null, avatarContent = null;
    [SerializeField] private GameObject frameObject = null, avatarObject = null;
    [SerializeField] private GameObject buyPanel = null;
    [SerializeField] private Toggle male = null, female = null;
    [SerializeField] private Text countryName = null;
    [SerializeField] private Image countryImage = null;
    [SerializeField] private GameObject countryDropDown = null;
    [SerializeField] private Image Frames_Vip, Avatars_Vip;
    public void InitCheck()
    {
        if (ProfileManager.instance.currentPlayer.Vip30_Rename)
        {
            userInputField.interactable = true;
        }
        else
        {
            userInputField.interactable = false;
        }
    }
    void Start()
    {
        Frames_Vip.sprite = GameManager.instance.VIP_Levels[ProfileManager.instance.currentPlayer.VIP_Level - 1];
        Avatars_Vip.sprite = GameManager.instance.VIP_Levels[ProfileManager.instance.currentPlayer.VIP_Level - 1];

        Change_btn.onClick.AddListener(() => {
            if (userInputField.interactable && userInputField.text != "")
            {
                ProfileManager.instance.currentPlayer.name = userInputField.text;
                Debug.Log("Name Changed");
                ProfileManager.instance.currentPlayer.Vip30_Rename = false;
                userInputField.interactable = false;
                ProfileManager.instance.SaveUserData();
            }
            else
            {
                Debug.Log("Purchase 30 days vip first");
            }
        });
        countryName.text = ProfileManager.instance.currentPlayer.country;
        countryImage.sprite = GameManager.instance.FlagData(ProfileManager.instance.currentPlayer.country);
        if (ProfileManager.instance.currentPlayer.gender == "male")
            male.isOn = true;
        else
            male.isOn = false;
        male.onValueChanged.AddListener(MaleGender);
        female.onValueChanged.AddListener(FemaleGender);
        if (ProfileManager.instance.currentPlayer.frames.Count <= 0)
            PushDataFrames();
        else
            GetDataFrames();
        if (ProfileManager.instance.currentPlayer.avatars.Count <= 0)
            PushDataAvatars();
        else
            GetDataAvatars();
    }
    private void PushDataAvatars()
    {
        Debug.Log("Pushing data");
        for (int n = 0; n < ProfileManager.instance.avatarDataFile.avatars.Count; n++)
        {
            DbAvatar av = new DbAvatar();
            av.name = ProfileManager.instance.avatarDataFile.avatars[n].avatarImage.name;
            av.lockStatus = ProfileManager.instance.avatarDataFile.avatars[n].lockStatus;
            av.equipStatus = ProfileManager.instance.avatarDataFile.avatars[n].equipStatus;
            ProfileManager.instance.currentPlayer.avatars.Add(av);
        }
        DisplayAvatars();
    }
    private void GetDataAvatars()
    {
        for (int n = 0; n < ProfileManager.instance.currentPlayer.avatars.Count; n++)
        {
            ProfileManager.instance.avatarDataFile.avatars[n].equipStatus = ProfileManager.instance.currentPlayer.avatars[n].equipStatus;
            ProfileManager.instance.avatarDataFile.avatars[n].lockStatus = ProfileManager.instance.currentPlayer.avatars[n].lockStatus;
            if (ProfileManager.instance.currentPlayer.avatars[n].unlockDuration != "")
            {
                ProfileManager.instance.avatarDataFile.avatars[n].lockStatus = CheckForUnlockTimer(ProfileManager.instance.currentPlayer.avatars[n]);
            }
        }
        DisplayAvatars();
    }
    public void PushDataFrames()
    {
        Debug.Log("Pushing data");
        for (int n = 0; n < ProfileManager.instance.framesDataFile.frames.Count; n++)
        {
            DbFrames av = new DbFrames();
            av.name = ProfileManager.instance.framesDataFile.frames[n].frameImage.name;
            av.lockStatus = ProfileManager.instance.framesDataFile.frames[n].lockStatus;
            av.equipStatus = ProfileManager.instance.framesDataFile.frames[n].equipStatus;
            ProfileManager.instance.currentPlayer.frames.Add(av);
        }
        DisplayFrames();
    }
    void MaleGender(bool val)
    {
        if (val)
        {
            ProfileManager.instance.currentPlayer.gender = "male";
            female.isOn = false;
        }
        else
        {
            female.isOn = true;
        }
    }
    void FemaleGender(bool val)
    {
        if (val)
        {
            ProfileManager.instance.currentPlayer.gender = "female";
            male.isOn = false;
        }
        else
        {
            male.isOn = true;
        }
    }
    void GetDataFrames()
    {
        for (int n = 0; n < ProfileManager.instance.currentPlayer.frames.Count; n++)
        {
            ProfileManager.instance.framesDataFile.frames[n].equipStatus = ProfileManager.instance.currentPlayer.frames[n].equipStatus;
            ProfileManager.instance.framesDataFile.frames[n].lockStatus = ProfileManager.instance.currentPlayer.frames[n].lockStatus;
            if (ProfileManager.instance.currentPlayer.frames[n].unlockDuration != "")
            {
                ProfileManager.instance.framesDataFile.frames[n].lockStatus = CheckForUnlockTimer(ProfileManager.instance.currentPlayer.frames[n]);
            }
        }
        DisplayFrames();
    }
    void DisplayAvatars()
    {
        for (int n = 0; n < ProfileManager.instance.avatarDataFile.avatars.Count; n++)
        {
            if (n >= avatarContent.childCount)
                Instantiate(avatarContent.GetChild(0).gameObject, avatarContent).GetComponent<AvatarObject>().PassData(ProfileManager.instance.avatarDataFile.avatars[n], buyPanel);
            else
                avatarContent.GetChild(n).GetComponent<AvatarObject>().PassData(ProfileManager.instance.avatarDataFile.avatars[n], buyPanel);
        }
    }
    void DisplayFrames()
    {
        for (int n = 0; n < ProfileManager.instance.framesDataFile.frames.Count; n++)
        {
            if (n >= framesContent.childCount)
                Instantiate(framesContent.GetChild(0).gameObject, framesContent).GetComponent<FrameObjectScript>().PassData(ProfileManager.instance.framesDataFile.frames[n], buyPanel);
            else
                framesContent.GetChild(n).GetComponent<FrameObjectScript>().PassData(ProfileManager.instance.framesDataFile.frames[n], buyPanel);
        }
    }
    bool CheckForUnlockTimer(DbFrames f)
    {
        if ((DateTime.Now - DateTime.Parse(f.unlockDuration)).TotalSeconds <= 0)
            return false;
        else
            return true;
    }
    bool CheckForUnlockTimer(DbAvatar f)
    {
        if ((DateTime.Now - DateTime.Parse(f.unlockDuration)).TotalSeconds <= 0)
            return false;
        else
            return true;
    }
    public void ChangeCountry(Sprite image,string c_Name)
    {
        countryImage.sprite = image;
        countryName.text = c_Name;
        ProfileManager.instance.currentPlayer.country = c_Name;
        ProfileManager.instance.SaveUserData();
    }

    public void ShowCountryDropdown()
    {
        countryDropDown.SetActive(!countryDropDown.activeSelf);
    }
}