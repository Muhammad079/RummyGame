using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class PlayerProfilePanel : MonoBehaviour
{
    [SerializeField] private Image profileImage = null, profileFrame = null, VIP_badge=null, Tournament_badge;
    [SerializeField] private Image Edit_profileImage = null, Edit_profileFrame = null;
    [SerializeField] private Text t_Username = null, t_Id = null, t_Coins = null, t_Gems = null, t_Trophies = null, t_TotalWins = null, t_GiftXp = null;
    [SerializeField] private GameObject profileDisplay = null, editProfile = null;
    [SerializeField] private Transform medalArea = null;
    [SerializeField] private Image countryFlag = null;
    [SerializeField] private Button edit_btn=null ,VIP_Collection=null;
    [SerializeField] private Transform Gifts_Grid = null;


    private void Start()
    {
        for (int n = 0; n < ProfileManager.instance.currentPlayer.Sent_Gifts.Count; n++)
        {

            for (int i = 0; i < GameManager.instance.giftsDataFile.gifts.Count; i++)
            {
                if (ProfileManager.instance.currentPlayer.Sent_Gifts[n].gift == (int)GameManager.instance.giftsDataFile.gifts[i].gift)
                {

                    //if (n == 0 && i == 0)
                    if (n == 0)
                    {
                        Gifts_Grid.GetChild(0).gameObject.SetActive(true);
                        Gifts_Grid.GetChild(0).GetChild(0).GetComponent<Image>().sprite = GameManager.instance.giftsDataFile.gifts[i].giftImage;
                        Gifts_Grid.GetChild(0).GetChild(1).GetComponent<Text>().text = ProfileManager.instance.currentPlayer.Sent_Gifts[n].count + "x";
                        Gifts_Grid.GetChild(0).GetChild(1).GetComponent<Kozykin.MultiLanguageItem>().strItems[0] = ProfileManager.instance.currentPlayer.Sent_Gifts[n].count + "x";
                        Gifts_Grid.GetChild(0).GetChild(1).GetComponent<Kozykin.MultiLanguageItem>().strItems[1] = ProfileManager.instance.currentPlayer.Sent_Gifts[n].count + "x";
                        Gifts_Grid.GetChild(0).GetChild(1).GetComponent<Kozykin.MultiLanguageItem>().text = ProfileManager.instance.currentPlayer.Sent_Gifts[n].count + "x";
                    }
                    else
                    {
                        var gift = Instantiate(Gifts_Grid.GetChild(0), Gifts_Grid);
                        gift.GetChild(0).GetComponent<Image>().sprite = GameManager.instance.giftsDataFile.gifts[i].giftImage;
                        gift.GetChild(1).GetComponent<Text>().text = ProfileManager.instance.currentPlayer.Sent_Gifts[n].count + "x";
                        gift.GetChild(1).GetComponent<Kozykin.MultiLanguageItem>().strItems[0] = ProfileManager.instance.currentPlayer.Sent_Gifts[n].count + "x";
                        gift.GetChild(1).GetComponent<Kozykin.MultiLanguageItem>().strItems[1] = ProfileManager.instance.currentPlayer.Sent_Gifts[n].count + "x";
                        gift.GetChild(1).GetComponent<Kozykin.MultiLanguageItem>().text = ProfileManager.instance.currentPlayer.Sent_Gifts[n].count + "x";
                    }
                }
            }


            //if (n == 0)
            //{
            //    Gifts_Grid.GetChild(0).gameObject.SetActive(true);
            //    Gifts_Grid.GetChild(0).GetChild(0).GetComponent<Image>().sprite = GameManager.instance.giftsDataFile.gifts[ProfileManager.instance.currentPlayer.Sent_Gifts[n].gift].giftImage;
            //    Gifts_Grid.GetChild(0).GetChild(1).GetComponent<Text>().text = ProfileManager.instance.currentPlayer.Sent_Gifts[i].count + "x";
            //}
            //else
            //{
            //    var gift = Instantiate(Gifts_Grid.GetChild(0), Gifts_Grid);
            //    gift.GetComponent<Image>().sprite = GameManager.instance.giftsDataFile.gifts[ProfileManager.instance.currentPlayer.Sent_Gifts[i].gift].giftImage;
            //    gift.GetChild(1).GetComponent<Text>().text = ProfileManager.instance.currentPlayer.Sent_Gifts[i].count + "x";
            //}
        }

        if (ProfileManager.instance.currentPlayer.isVip)
        {
            VIP_badge.gameObject.SetActive(true);
            VIP_badge.sprite = GameManager.instance.VIP_Levels[ProfileManager.instance.currentPlayer.VIP_Level - 1];
            VIP_Collection.transform.GetChild(0).GetComponent<Image>().sprite = GameManager.instance.VIP_Levels[ProfileManager.instance.currentPlayer.VIP_Level - 1];
            VIP_Collection.transform.GetChild(2).GetComponent<Text>().text = "VIP Level " + ProfileManager.instance.currentPlayer.VIP_Level;

            VIP_Collection.transform.GetChild(2).GetComponent<Kozykin.MultiLanguageItem>().strItems[0] = "VIP Level " + ProfileManager.instance.currentPlayer.VIP_Level;
            VIP_Collection.transform.GetChild(2).GetComponent<Kozykin.MultiLanguageItem>().strItems[1] = "مستوى كبار الشخصيات " + ProfileManager.instance.currentPlayer.VIP_Level;
            VIP_Collection.transform.GetChild(2).GetComponent<Kozykin.MultiLanguageItem>().text = "مستوى كبار الشخصيات " + ProfileManager.instance.currentPlayer.VIP_Level;
        }
        else
        {
            VIP_badge.gameObject.SetActive(false);
            VIP_Collection.transform.GetChild(2).GetComponent<Text>().text = "NOT VIP";

            VIP_Collection.transform.GetChild(2).GetComponent<Kozykin.MultiLanguageItem>().strItems[0] = "NOT VIP";
            VIP_Collection.transform.GetChild(2).GetComponent<Kozykin.MultiLanguageItem>().strItems[1] = "ليس من كبار الشخصيات";
            VIP_Collection.transform.GetChild(2).GetComponent<Kozykin.MultiLanguageItem>().text = "ليس من كبار الشخصيات";
        }
            
        
        

        Tournament_badge.sprite = GameManager.instance.tournamentMedalData.tournamentMedals[ProfileManager.instance.currentPlayer.tournamentLevel].tournamentMedal;
        Tournament_badge.transform.GetChild(0).GetComponent<Text>().text = "Level: " + ProfileManager.instance.currentPlayer.tournamentLevel;

        Show();
        edit_btn.onClick.AddListener(()=> {
            profileDisplay.transform.DOScale(new Vector3(0, 0, 0), 0.2f).SetEase(Ease.Linear).OnComplete(()=> {
                profileDisplay.SetActive(false);
                editProfile.SetActive(true);
                editProfile.transform.DOScale(new Vector3(1, 1, 1), 0.2f).SetEase(Ease.Linear);
            });
        });
    }

    void DisplayMedal()
    {
        for (int i = 0; i < GameManager.instance.scriptable_4Medals.Medals.Count; i++)
        {
            if(i==0)
            {
                medalArea.GetChild(i).GetChild(i).GetComponent<Image>().sprite = GameManager.instance.scriptable_4Medals.Medals[i].Medal_Image;
                if(ProfileManager.instance.currentPlayer.consectiveLogin >= 365)
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
                if(i==1)
                {
                    if(ProfileManager.instance.currentPlayer.totalWins >=1000)
                    {
                        Medals.transform.GetChild(1).gameObject.SetActive(false);
                    }
                    else
                    {
                        Medals.transform.GetChild(1).gameObject.SetActive(true);
                    }
                }
                else if(i==2)
                {
                    if (ProfileManager.instance.currentPlayer.sendGifts >= 10000000)
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
                    if (ProfileManager.instance.currentPlayer.coins >= 999000000)
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

        //for (int n = 0; n < GameManager.instance. tournamentMedalData.tournamentMedals.Count; n++)
        //{

        //    if (n >= medalArea.childCount)
        //        Instantiate(medalArea.GetChild(0).gameObject, medalArea);
        //    medalArea.GetChild(n).GetChild(0).GetComponent<Image>().sprite = GameManager.instance. tournamentMedalData.tournamentMedals[n].tournamentMedal;
        //    medalArea.GetChild(n).gameObject.SetActive(true);
        //    if (ProfileManager.instance.currentPlayer.tournamentXp >= GameManager.instance. tournamentMedalData.tournamentMedals[n].xpReq)
        //    {
        //        medalArea.GetChild(n).GetChild(1).gameObject.SetActive(false);
        //    }
        //    else
        //    {
        //        medalArea.GetChild(n).GetChild(1).gameObject.SetActive(true);
        //    }
        //}
    }
    public void Show()
    {
        this.gameObject.SetActive(true);
        profileDisplay.SetActive(true);
        profileDisplay.transform.localScale = new Vector3(1,1,1);
        transform.DOScale(Vector3.one, 0.2f).SetEase(Ease.InCubic);
        t_Username.text = ProfileManager.instance.currentPlayer.name;
        t_Id.text = ProfileManager.instance.currentPlayer.id;
        t_Coins.text = ProfileManager.instance.currentPlayer.coins.ToString();
        t_Gems.text = ProfileManager.instance.currentPlayer.gems.ToString();
        t_Trophies.text = ProfileManager.instance.currentPlayer.trophies.ToString();
        t_TotalWins.text = ProfileManager.instance.currentPlayer.totalWins.ToString();
        countryFlag.sprite = GameManager.instance.FlagData(ProfileManager.instance.currentPlayer.country);
        //profileImage.sprite = ProfileManager.instance.avatarDataFile.avatars[ProfileManager.instance.currentPlayer.selectedFrameIndex].avatarImage;
        profileImage.sprite = ProfileManager.instance.currentPlayer.profilePicture;
        profileFrame.sprite = ProfileManager.instance.framesDataFile.frames[ProfileManager.instance.currentPlayer.selectedFrameIndex].frameImage;

        t_Coins.GetComponent<Kozykin.MultiLanguageItem>().strItems[0] = ProfileManager.instance.currentPlayer.coins.ToString();
        t_Coins.GetComponent<Kozykin.MultiLanguageItem>().strItems[1] = ProfileManager.instance.currentPlayer.coins.ToString();
        t_Coins.GetComponent<Kozykin.MultiLanguageItem>().text = ProfileManager.instance.currentPlayer.coins.ToString();

        t_Gems.GetComponent<Kozykin.MultiLanguageItem>().strItems[0] = ProfileManager.instance.currentPlayer.gems.ToString();
        t_Gems.GetComponent<Kozykin.MultiLanguageItem>().strItems[1] = ProfileManager.instance.currentPlayer.gems.ToString();
        t_Gems.GetComponent<Kozykin.MultiLanguageItem>().text = ProfileManager.instance.currentPlayer.gems.ToString();

        t_Trophies.GetComponent<Kozykin.MultiLanguageItem>().strItems[0] = ProfileManager.instance.currentPlayer.trophies.ToString();
        t_Trophies.GetComponent<Kozykin.MultiLanguageItem>().strItems[1] = ProfileManager.instance.currentPlayer.trophies.ToString();
        t_Trophies.GetComponent<Kozykin.MultiLanguageItem>().text = ProfileManager.instance.currentPlayer.trophies.ToString();



        DisplayMedal();
    }
    private void Update()
    {
        if(editProfile.activeInHierarchy)
        {
            Edit_profileFrame.sprite = ProfileManager.instance.framesDataFile.frames[ProfileManager.instance.currentPlayer.selectedFrameIndex].frameImage;
            Edit_profileImage.sprite = ProfileManager.instance.currentPlayer.profilePicture;
        }
    }
    public void Cross()
    {
        //  transform.DOScale(Vector3.zero, 0.5f).SetEase(Ease.InCubic).OnComplete(DeactivatePanel);
        GameManager.instance.sceneToLoad = "Home";
        Debug.LogError("Loading");
        UnityEngine.SceneManagement.SceneManager.LoadScene("Home");
    }
    void DeactivatePanel()
    {
        this.gameObject.SetActive(false);
        editProfile.SetActive(false);
        editProfile.transform.localScale = new Vector3(0, 0, 0);
    }
}
