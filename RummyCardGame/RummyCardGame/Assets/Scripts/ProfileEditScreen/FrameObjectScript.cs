using UnityEngine;
using UnityEngine.UI;

public class FrameObjectScript : ProfileItemsBuy
{
    [SerializeField] private GameObject buyPanel = null;
    [SerializeField] private GameObject onlyVIP = null;
    public Frames frame = null;
    [SerializeField] private GameObject locked = null, equiped = null;
    [SerializeField] private Image frameImage = null;
    [SerializeField] private Image frameApply = null;
    void Start()
    {
        GetComponent<Button>().onClick.AddListener(OnClick);
    }
    void OnClick()
    {
        frameApply.sprite = frame.frameImage;
        if (!frame.lockStatus)
        {
            Debug.Log("CLicking to equip this frame");
            equiped.SetActive(true);
            Debug.Log(frame.frameImage.name);
            for (int n = 0; n < ProfileManager.instance.currentPlayer.frames.Count; n++)
            {
                if (ProfileManager.instance.currentPlayer.frames[n].name != frame.frameImage.name)
                {
                    Debug.Log(ProfileManager.instance.currentPlayer.frames[n].name + "  " + frame.frameImage.name);
                    transform.parent.GetChild(n).GetComponent<FrameObjectScript>().frame.equipStatus = false;
                    transform.parent.GetChild(n).GetComponent<FrameObjectScript>().FrameStatus();
                }
                else
                {
                    Debug.Log("In else");
                    frame.equipStatus = true;
                    FrameStatus();
                }
               
            }
        }
        else
        {
            InitUnlocking();
        }
    }
    void InitUnlocking()
    {
        buyPanel.SetActive(true);
        buyPanel.GetComponent<ItemBuyPanel>().PassData(GetComponent<ProfileItemsBuy>(), frame.frameImage, frame.priceInGems, frame.priceInCoins, frame.onlyForVip, frame.unlockDaysDuration, frame.fromEvent);
    }
    public override void PurchaseSuccess()
    {
        Debug.Log("Buy successfull");
        frame.lockStatus = false;
        ProfileManager.instance.currentPlayer.coins -= frame.priceInCoins;
        ProfileManager.instance.currentPlayer.gems -= frame.priceInGems;
        ProfileManager.instance.currentPlayer.frames[transform.GetSiblingIndex()].lockStatus = false;
        ProfileManager.instance.currentPlayer.frames[transform.GetSiblingIndex()].unlockDuration = System.DateTime.Now.AddDays(frame.unlockDaysDuration).ToString();
        FrameStatus();
        ProfileManager.instance.SaveUserData();
    }
    public void PassData(Frames passingFrame, GameObject buy)
    {
        frameImage.sprite = passingFrame.frameImage;
        buyPanel = buy;
        frame = passingFrame;
        onlyVIP.SetActive(frame.onlyForVip);
        FrameStatus();
    }
    void FrameStatus()
    {
        if (frame.lockStatus)
        {
            equiped.SetActive(false);
            locked.SetActive(true);
        }
        else if (frame.equipStatus)
        {
            equiped.SetActive(true);
            locked.SetActive(false);
            ProfileManager.instance.currentPlayer.selectedFrameIndex = transform.GetSiblingIndex();
        }
        else
        {
            equiped.SetActive(false);
            locked.SetActive(false);
        }
    }

}
