using DG.Tweening;
using Firebase.Database;
using UnityEngine;
using UnityEngine.UI;

public class GiftBox : MonoBehaviour
{
    [SerializeField] private Image giftImage = null;
    [SerializeField] private Text t_price = null;
    [SerializeField] private Text t_charmXp = null;
    Gifts gift;
    int giftCost = 0;
    int charmXpIncrement = 0;
    PlayerProfile user = null;
    [SerializeField] private Button sendButton = null, plus = null, minus = null;
    void Start()
    {
        sendButton.onClick.AddListener(SendGift);
        minus.onClick.AddListener(Decrement);
        plus.onClick.AddListener(Increment);
    }
    internal void PassData(Gifts gifts, PlayerProfile selectedUser)
    {
        gift = gifts;
        giftCost = gift.price;
        Debug.LogError(giftCost +"Gift Cost");
        charmXpIncrement = gift.charmXp;
        Debug.LogError(charmXpIncrement +"Gift charmpoint");
        user = selectedUser;
        Debug.LogError(user+"gift user id");
        giftImage.sprite = gifts.giftImage;
        DisplayData();
    }
    void DisplayData()
    {
        t_price.text = "" + giftCost;
        //t_price.GetComponent<Kozykin.MultiLanguageItem>().strItems[0] = "" + giftCost;
        //t_price.GetComponent<Kozykin.MultiLanguageItem>().strItems[1] = "" + giftCost;
        //t_price.GetComponent<Kozykin.MultiLanguageItem>().text = "" + giftCost;

        //t_charmXp.text = "" + charmXpIncrement;
        //t_charmXp.GetComponent<Kozykin.MultiLanguageItem>().strItems[0] = "" + charmXpIncrement;
        //t_charmXp.GetComponent<Kozykin.MultiLanguageItem>().strItems[1] = "" + charmXpIncrement;
        //t_charmXp.GetComponent<Kozykin.MultiLanguageItem>().text = "" + charmXpIncrement;
    }
    void Increment()
    {
        giftCost += gift.price;
        charmXpIncrement += gift.charmXp;
        DisplayData();
    }
    void Decrement()
    {
        giftCost -= gift.price;
        charmXpIncrement -= gift.charmXp;
        if (giftCost < 0)
        {
            giftCost = 0;
            charmXpIncrement = 0;
        }
        DisplayData();
    }
    void SendGift()
    {
        if (ProfileManager.instance.currentPlayer.coins >= giftCost)
        {
            ProfileManager.instance.currentPlayer.coins -= giftCost;
            ProfileManager.instance.currentPlayer.charmXp += charmXpIncrement;
            ProfileManager.instance.currentPlayer.charmXp_Monthly += charmXpIncrement;
            ProfileManager.instance.currentPlayer.charmXp_Weekly += charmXpIncrement;
            GiftSendingMessege msg = new GiftSendingMessege();
            msg.senderId = ProfileManager.instance.currentPlayer.name;
            msg.recieverId = user.name;
            msg.giftItems = gift.gift;
            bool giftAdded = false;

            GameManager.instance.userProfileHandler.DisplayGiftMessege(msg);

            for (int n = 0; n < user.recievedGifts.Count; n++)
            {
                if (user.recievedGifts[n].gift == (int)gift.gift)
                {
                    user.recievedGifts[n].count += (int)(giftCost / gift.price);
                    giftAdded = true;
                    break;
                }
            }
            if (!giftAdded)
            {
                DbGifts dbGifts = new DbGifts();
                dbGifts.gift = (int)gift.gift;
                dbGifts.count = 1;
                user.recievedGifts.Add(dbGifts);
            }

            bool gift_found = false ;
            if(ProfileManager.instance.currentPlayer.Sent_Gifts.Count>0)
            {
                for (int i = 0; i < ProfileManager.instance.currentPlayer.Sent_Gifts.Count; i++)
                {
                    if(ProfileManager.instance.currentPlayer.Sent_Gifts[i].gift == (int)gift.gift)
                    {
                        ProfileManager.instance.currentPlayer.Sent_Gifts[i].count++;
                        gift_found = true;
                        //break;
                    }
                    else
                    {
                        //DbGifts dbGifts = new DbGifts();
                        //dbGifts.gift = (int)gift.gift;
                        //dbGifts.count = 1;
                        //ProfileManager.instance.currentPlayer.Sent_Gifts.Add(dbGifts);
                        //break;
                    }
                }
            }
            else
            {
                DbGifts dbGifts = new DbGifts();
                dbGifts.gift = (int)gift.gift;
                dbGifts.count = 1;
                ProfileManager.instance.currentPlayer.Sent_Gifts.Add(dbGifts);
                gift_found = true;
            }
            if(!gift_found)
            {
                DbGifts dbGifts = new DbGifts();
                dbGifts.gift = (int)gift.gift;
                dbGifts.count = 1;
                ProfileManager.instance.currentPlayer.Sent_Gifts.Add(dbGifts);
            }


            ProfileManager.instance.currentPlayer.sendGifts += (int)(gift.price / giftCost);
            ProfileManager.instance.SaveUserData();
            //DatabaseFunctions.SaveDataInDB(user);
            FirebaseDatabase.DefaultInstance.GetReference("GiftSendingInfo").SetRawJsonValueAsync(JsonUtility.ToJson(msg));
        }
        else
        {
            GameManager.instance.Not_Enough_C_Panel.SetActive(true);
            GameManager.instance.Not_Enough_C_Panel.transform.DOScale(new Vector3(1, 1, 1), 0.5f).SetEase(Ease.OutBounce);
        }
    }
}