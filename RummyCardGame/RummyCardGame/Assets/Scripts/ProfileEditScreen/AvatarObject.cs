using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AvatarObject : ProfileItemsBuy
{
    [SerializeField] private GameObject onlyVIP = null;
    public Avatar avatar = null;
    [SerializeField] private GameObject locked = null, equiped = null;
    [SerializeField] private GameObject buyPanel = null;
    [SerializeField] private Image avatarImage = null;
    [SerializeField] private Image avatarApply = null;
   
    void Start()
    {
        GetComponent<Button>().onClick.AddListener(OnClick);
    }
    void OnClick()
    {
        avatarApply.overrideSprite = avatar.avatarImage;
        Debug.Log(avatarApply.sprite);
        if (!avatar.lockStatus)
        {
            Debug.Log("CLicking to equip this frame");
            equiped.SetActive(true);
         for(int n=0;n< ProfileManager.instance.currentPlayer.avatars.Count;n++){
                if (ProfileManager.instance.currentPlayer.avatars[n].name != avatar.avatarImage.name)
                {
                    ProfileManager.instance.currentPlayer.avatars[n].equipStatus = false;
                    transform.parent.GetChild(n).GetComponent<AvatarObject>().avatar.equipStatus = false;
                    transform.parent.GetChild(n).GetComponent<AvatarObject>().AvatarStatus();
                    Debug.Log("In if");
                }
                else
                {
                    Debug.Log("In else");
                    ProfileManager.instance.currentPlayer.avatars[n].equipStatus = true;
                    avatar.equipStatus = true;
                    AvatarStatus();
                }
               
            }

        }
        else
        {
            InitUnlocking();
        }
    } 
    public void PassData(Avatar passingAvatar,GameObject buy)
    {
        avatarImage.sprite = passingAvatar.avatarImage;
        buyPanel = buy;
        avatar = passingAvatar;
        onlyVIP.SetActive(avatar.onlyForVip);
        AvatarStatus();
    }
    void InitUnlocking()
    {
        buyPanel.SetActive(true);
        buyPanel.GetComponent<ItemBuyPanel>().PassData(GetComponent<ProfileItemsBuy>(), avatar.avatarImage, avatar.priceInGems, avatar.priceInCoins, avatar.onlyForVip, avatar.unlockDaysDuration,avatar.fromEvent);
    }
    public override void PurchaseSuccess()
    {
        Debug.Log("Buy successfull");
        avatar.lockStatus = false;
        ProfileManager.instance.currentPlayer.coins -= avatar.priceInCoins;
        ProfileManager.instance.currentPlayer.gems -= avatar.priceInGems;
        ProfileManager.instance.currentPlayer.frames[transform.GetSiblingIndex()].lockStatus = false;
        ProfileManager.instance.currentPlayer.frames[transform.GetSiblingIndex()].unlockDuration = System.DateTime.Now.AddDays(avatar.unlockDaysDuration).ToString();
        AvatarStatus();
        ProfileManager.instance.SaveUserData();
    }
    void AvatarStatus()
    {
        if (avatar.lockStatus)
        {
            equiped.SetActive(false);
            locked.SetActive(true);
        }
        else if (avatar.equipStatus)
        {
            equiped.SetActive(true);
            locked.SetActive(false);
            ProfileManager.instance.currentPlayer.selectedAvatarIndex = transform.GetSiblingIndex();
        }
        else
        {
            equiped.SetActive(false);
            locked.SetActive(false);
        }
    }

}
