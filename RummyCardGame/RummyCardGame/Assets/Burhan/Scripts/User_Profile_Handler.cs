using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class User_Profile_Handler : SceneLoader
{
    [SerializeField] private GiftSendingDisplay giftSendingDisplay = null;
    public Transform Gifts_transform;

    [SerializeField] public Text Name = null, Coins = null, XP = null, Trophies = null, Charm_XP = null, Badge = null, ID = null, Level = null;
    public Transform medalArea = null, giftsArea = null;
    [SerializeField] private Image countryFlag = null;
    public PlayerProfile displayingProfile = null;
    [SerializeField] private Button deleteButton = null, addFriend = null, report = null, close_btn = null;
    [SerializeField] public Image tournamentBadge = null, avatarImage = null, frameImage = null;
    [SerializeField] private Image vipDiamond = null, cardCollection = null;
    [SerializeField] private Sprite sevenDays = null, thirtyDays = null;
    public GameObject Popup_msg_Panel;

    public List<string> Reported_users = new List<string>();

    public List<GiftSendingMessege> pendingGiftMsg = new List<GiftSendingMessege>();
    bool giftInfoShowing = false;


    #region Ahmed
    public GameObject GiftPrefab;

    #endregion

    public void DisplayGiftMessege(GiftSendingMessege giftMsg)
    {
        if (!giftInfoShowing)
        {
            Gifts_transform.gameObject.SetActive(true);
            giftInfoShowing = true;
            Debug.Log("Called");
            var a = Instantiate(giftSendingDisplay, Gifts_transform);
            a.gameObject.SetActive(true);
            a.PassData(giftMsg);
            Invoke(nameof(ResetGiftInfoValue), 7);
        }
    }
    void ResetGiftInfoValue()
    {
        giftInfoShowing = false;
        Gifts_transform.gameObject.SetActive(false); ;
    }


    public void OnEnable()
    {

        Invoke(nameof(refresh), 0.1f);
    }
    void refresh()
    {

        report.interactable = true;
        addFriend.interactable = true;
        for (int i = 0; i < Reported_users.Count; i++)
        {
            if (displayingProfile.id == Reported_users[i])
            {
                report.interactable = false;
                break;
            }
        }
        for (int i = 0; i < ProfileManager.instance.currentPlayer.pendingReq.Count; i++)
        {
            if (displayingProfile.id == ProfileManager.instance.currentPlayer.pendingReq[i])
            {
                addFriend.interactable = false;
                break;
            }
        }
    }
    public void Start()
    {
        GameManager.instance.userProfileHandler = this;
        if(SceneManager.GetActiveScene().name == "UserProfile")
        {
            GameManager.instance.userProfileHandler.DisplayProfileStats(GameManager.instance.User_Profile);
        }
        deleteButton.onClick.AddListener(DeleteFriend);
        report.onClick.AddListener(Report);
        addFriend.onClick.AddListener(AddFriend);
        close_btn.onClick.AddListener(Close_Panel);
    }
    public override void Update()
    {
        base.Update();
    }
    void Close_Panel()
    {
        if (SceneManager.GetActiveScene().name == "UserProfile")
        {
            transform.DOScale(new Vector3(0, 0, 0), 0.2f).OnComplete(() => {
                StartCoroutine(OnClick());
                //Resources.UnloadUnusedAssets();
                //GameManager.instance.sceneToLoad = "Home";
                //SceneManager.LoadScene("Home");

            });
        }
        else
        {
            transform.DOScale(new Vector3(0, 0, 0), 0.2f);
        }
    }
    public void DisplayProfileStats(PlayerProfile player)
    {
        OnEnable();
        this.gameObject.SetActive(true);
        transform.DOScale(new Vector3(1, 1, 1), 0.2f);

        //transform.localScale = Vector3.one;
        //this.gameObject.SetActive(true);
        displayingProfile = player;
        CheckProfile();
        this.gameObject.SetActive(true);

        if(player.isVip)
        {
            vipDiamond.sprite = GameManager.instance.VIP_Levels[player.VIP_Level - 1];
        }
        else
        {
            vipDiamond.gameObject.SetActive(false);
        }

        //if (player.VIP_30days_count > 0)
        //{
        //    vipDiamond.transform.parent.gameObject.SetActive(true);
        //    vipDiamond.sprite = thirtyDays;
        //}
        //else if (player.VIP_7days_count > 0)
        //{
        //    vipDiamond.transform.parent.gameObject.SetActive(true);
        //    vipDiamond.sprite = sevenDays;
        //}
        //else
        //{
        //    vipDiamond.transform.parent.gameObject.SetActive(false);
        //}
        int a = CardCollectionIndex();
        if (a >= 0)
        {
            cardCollection.transform.parent.gameObject.SetActive(true);
            cardCollection.sprite = GameManager.instance.cardsCollection.cardsCollection[a].collectionImage;
        }
        else
        {
            cardCollection.transform.parent.gameObject.SetActive(false);
        }
        avatarImage.sprite = ProfileManager.instance.avatarDataFile.avatars[player.selectedAvatarIndex].avatarImage;
        frameImage.sprite = ProfileManager.instance.framesDataFile.frames[player.selectedFrameIndex].frameImage;
        Name.text = player.name;
        Coins.text = player.coins.ToString();
        Coins.GetComponent<Kozykin.MultiLanguageItem>().strItems[0] = player.coins.ToString();
        Coins.GetComponent<Kozykin.MultiLanguageItem>().strItems[1] = player.coins.ToString();
        Coins.GetComponent<Kozykin.MultiLanguageItem>().text = player.coins.ToString();

        XP.text = player.xp.ToString();
        Trophies.text = player.trophies.ToString();
        Charm_XP.text = player.charmXp.ToString();
        Charm_XP.GetComponent<Kozykin.MultiLanguageItem>().strItems[0] = player.charmXp.ToString();
        Charm_XP.GetComponent<Kozykin.MultiLanguageItem>().strItems[1] = player.charmXp.ToString();
        Charm_XP.GetComponent<Kozykin.MultiLanguageItem>().text = player.charmXp.ToString();

        Badge.text = player.badges.ToString();
        ID.text = player.id;
        Level.text = player.level.ToString();
        Level.GetComponent<Kozykin.MultiLanguageItem>().strItems[0] = player.level.ToString();
        Level.GetComponent<Kozykin.MultiLanguageItem>().strItems[1] = player.level.ToString();
        Level.GetComponent<Kozykin.MultiLanguageItem>().text = player.level.ToString();

        countryFlag.sprite = GameManager.instance.FlagData(player.country);
        DisplayMedal(player);
        DisplayGifts();
    }
    int CardCollectionIndex()
    {
        int a = 0;
        for (int n = 0; n < GameManager.instance.cardsCollection.cardsCollection.Count; n++)
        {
            if (GameManager.instance.cardsCollection.cardsCollection[n].cards[GameManager.instance.cardsCollection.cardsCollection[n].cards.Count - 1].club.cardCount >= 10 &&
                GameManager.instance.cardsCollection.cardsCollection[n].cards[GameManager.instance.cardsCollection.cardsCollection[n].cards.Count - 1].spade.cardCount >= 10 &&
                GameManager.instance.cardsCollection.cardsCollection[n].cards[GameManager.instance.cardsCollection.cardsCollection[n].cards.Count - 1].heart.cardCount >= 10 &&
                GameManager.instance.cardsCollection.cardsCollection[n].cards[GameManager.instance.cardsCollection.cardsCollection[n].cards.Count - 1].diamond.cardCount >= 10)
            {
                Debug.Log("this is filled");
                a = -1;
            }
            else
            {
                for (int m = 0; m < GameManager.instance.cardsCollection.cardsCollection[n].cards.Count; m++)
                {
                    if (GameManager.instance.cardsCollection.cardsCollection[n].cards[m].club.cardCount >= 10 &&
                GameManager.instance.cardsCollection.cardsCollection[n].cards[m].spade.cardCount >= 10 &&
                GameManager.instance.cardsCollection.cardsCollection[n].cards[m].heart.cardCount >= 10 &&
                GameManager.instance.cardsCollection.cardsCollection[n].cards[m].diamond.cardCount >= 10)
                    {
                        Debug.Log("This is filled too");
                        a = -1;
                    }
                    else
                    {
                        a = n - 1;
                        break;
                    }
                }
                break;
            }
        }
        return a;
    }
    void CheckProfile()
    {
        if (ProfileManager.instance.currentPlayer.friends.Contains(displayingProfile.id))
        {
            addFriend.gameObject.SetActive(false);
            deleteButton.gameObject.SetActive(true);
        }
        else
        {
            addFriend.gameObject.SetActive(true);
            deleteButton.gameObject.SetActive(false);
        }
        //report.gameObject.SetActive(true);

    }
    void DisplayMedal(PlayerProfile player)
    {
        for (int i = 0; i < GameManager.instance.scriptable_4Medals.Medals.Count; i++)
        {
            if (i == 0)
            {
                medalArea.GetChild(i).GetChild(i).GetComponent<Image>().sprite = GameManager.instance.scriptable_4Medals.Medals[i].Medal_Image;
                if (player.consectiveLogin >= 365)
                {
                    medalArea.GetChild(i).GetChild(i + 1).gameObject.SetActive(false);
                }
                else
                {
                    medalArea.GetChild(i).GetChild(i + 1).gameObject.SetActive(true);
                }
            }
            else
            {
                var Medals = Instantiate(medalArea.GetChild(0).gameObject, medalArea);
                Medals.transform.GetChild(0).GetComponent<Image>().sprite = GameManager.instance.scriptable_4Medals.Medals[i].Medal_Image;
                if (i == 1)
                {
                    if (player.totalWins >= 1000)
                    {
                        Medals.transform.GetChild(1).gameObject.SetActive(false);
                    }
                    else
                    {
                        Medals.transform.GetChild(1).gameObject.SetActive(true);
                    }
                }
                else if (i == 2)
                {
                    if (player.sendGifts >= 10000000)
                    {
                        Medals.transform.GetChild(1).gameObject.SetActive(false);
                    }
                    else
                    {
                        Medals.transform.GetChild(1).gameObject.SetActive(true);
                    }
                }
                else if (i == 3)
                {
                    if (player.coins >= 999000000)
                    {
                        Medals.transform.GetChild(1).gameObject.SetActive(false);
                    }
                    else
                    {
                        Medals.transform.GetChild(1).gameObject.SetActive(true);
                    }
                }
            }
        }



        //for (int n = 0; n < GameManager.instance.tournamentMedalData.tournamentMedals.Count; n++)
        //{
        //    if (n >= medalArea.childCount)
        //        Instantiate(medalArea.GetChild(0).gameObject, medalArea);
        //    medalArea.GetChild(n).GetChild(0).GetComponent<Image>().sprite = GameManager.instance.tournamentMedalData.tournamentMedals[n].tournamentMedal;
        //    tournamentBadge.sprite = GameManager.instance.tournamentMedalData.tournamentMedals[player.tournamentLevel].tournamentMedal;
        //    tournamentBadge.transform.GetChild(0).GetComponent<Text>().text = "Level: " + player.tournamentLevel;
        //    medalArea.GetChild(n).gameObject.SetActive(true);
        //    if (player.tournamentXp >= GameManager.instance.tournamentMedalData.tournamentMedals[n].xpReq)
        //    {
        //        medalArea.GetChild(n).GetChild(1).gameObject.SetActive(false);
        //    }
        //    else
        //    {
        //        medalArea.GetChild(n).GetChild(1).gameObject.SetActive(true);
        //    }
        //}
    }
    void AddFriend()
    {
        //addFriend.gameObject.SetActive(false);
        addFriend.interactable = false;
        ProfileManager.instance.currentPlayer.pendingReq.Add(displayingProfile.id);
        displayingProfile.friendReq.Add(ProfileManager.instance.currentPlayer.id);
        DatabaseFunctions.SaveDataInDB(displayingProfile);
        ProfileManager.instance.SaveUserData();
        StartCoroutine(Toast(1));
    }
    IEnumerator Toast(int val)
    {
        if (val == 1)
        {
            Popup_msg_Panel.transform.GetChild(0).GetComponent<Text>().text = "Friend Request Sent";
            Popup_msg_Panel.transform.GetChild(0).GetComponent<Kozykin.MultiLanguageItem>().strItems[0] = "Friend Request Sent";
            Popup_msg_Panel.transform.GetChild(0).GetComponent<Kozykin.MultiLanguageItem>().strItems[1] = "تم ارسال طلب صداقة";
            Popup_msg_Panel.transform.GetChild(0).GetComponent<Kozykin.MultiLanguageItem>().text = "تم ارسال طلب صداقة";

        }
        else if (val == 2)
        {
            Popup_msg_Panel.transform.GetChild(0).GetComponent<Text>().text = "Friend Removed";
            Popup_msg_Panel.transform.GetChild(0).GetComponent<Kozykin.MultiLanguageItem>().strItems[0] = "Friend Removed";
            Popup_msg_Panel.transform.GetChild(0).GetComponent<Kozykin.MultiLanguageItem>().strItems[1] = "صديق إزالة";
            Popup_msg_Panel.transform.GetChild(0).GetComponent<Kozykin.MultiLanguageItem>().text = "صديق إزالة";
        }

        Popup_msg_Panel.SetActive(true);
        Popup_msg_Panel.transform.DOLocalMoveY(0, 0.3f).SetEase(Ease.OutSine);
        yield return new WaitForSeconds(3);
        Popup_msg_Panel.transform.DOLocalMoveY(-334, 0.3f).SetEase(Ease.InSine).OnComplete(() =>
        {
            Popup_msg_Panel.SetActive(false);
        });
    }
    void Report()
    {
        //transform.DOScale(new Vector3(0, 0, 0), 0.5f);
    }
    void DeleteFriend()
    {
        //deleteButton.gameObject.SetActive(false);
        deleteButton.interactable = false;
        displayingProfile.friends.Remove(ProfileManager.instance.currentPlayer.id);
        ProfileManager.instance.currentPlayer.friends.Remove(displayingProfile.id);
        DatabaseFunctions.SaveDataInDB(displayingProfile);
        ProfileManager.instance.SaveUserData();
        StartCoroutine(Toast(2));
    }
    void DisplayGifts()
    {
        for (int n = 0; n < GameManager.instance.giftsDataFile.gifts.Count; n++)
        {
            //if (n >= giftsArea.childCount)
                GameObject gift = Instantiate(GiftPrefab, giftsArea);
                gift.GetComponent<GiftBox>().PassData(GameManager.instance.giftsDataFile.gifts[n], displayingProfile);
        }
    }

}
