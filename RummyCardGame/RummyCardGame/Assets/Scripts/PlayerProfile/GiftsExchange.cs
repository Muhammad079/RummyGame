using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class GiftsExchange : MonoBehaviour
{
    public static GiftsExchange instance;
    [SerializeField] private Transform giftArea = null;
    [SerializeField] public Text[] Price = null;
    [SerializeField] private Button Close_btn,Exchange_btn;
    [SerializeField] public GameObject[] selection;
    public int Exchange_Rate = 0;
    [SerializeField] public int selected_gift_price = 0;
    [SerializeField] private Text Exchange_Rate_Display;
    public int selected_gift_id;

    private void OnEnable()
    {
        gameObject.SetActive(true);
        transform.DOScale(new Vector3(1, 1, 1), 0.2f).SetEase(Ease.Linear);

        Exchange_Rate = GameManager.instance.VIP_Information.VIPs[ProfileManager.instance.currentPlayer.VIP_Level - 1].Gift_Exchange_PER;
    }
    private void Start()
    {
        Exchange_Rate_Display.text = Exchange_Rate.ToString() + "%";
        Exchange_Rate_Display.GetComponent<Kozykin.MultiLanguageItem>().strItems[0] = Exchange_Rate.ToString() + "%";
        Exchange_Rate_Display.GetComponent<Kozykin.MultiLanguageItem>().strItems[1] = Exchange_Rate.ToString() + "%";
        Exchange_Rate_Display.GetComponent<Kozykin.MultiLanguageItem>().text = Exchange_Rate.ToString() + "%";

        selection = new GameObject[GameManager.instance.giftsDataFile.gifts.Count];
        instance = this;
        DisplayData();
        
         
        Close_btn.onClick.AddListener(()=> {
            transform.DOScale(new Vector3(0, 0, 0), 0.2f).SetEase(Ease.Linear).OnComplete(()=> {
                gameObject.SetActive(false);
            });
            
        });

        
        Exchange_btn.onClick.AddListener(() => {
            
            
            //ProfileManager.instance.GetReward(RewardType.coins, selected_gift_price);
            //selected_gift_price = 0;
            Price[0].text = "0K";
            Price[1].text = "0K";

            Price[0].GetComponent<Kozykin.MultiLanguageItem>().strItems[0] = "0K";
            Price[0].GetComponent<Kozykin.MultiLanguageItem>().strItems[1] = "0K";
            Price[0].GetComponent<Kozykin.MultiLanguageItem>().text = "0K";

            Price[1].GetComponent<Kozykin.MultiLanguageItem>().strItems[0] = "0K";
            Price[1].GetComponent<Kozykin.MultiLanguageItem>().strItems[1] = "0K";
            Price[1].GetComponent<Kozykin.MultiLanguageItem>().text = "0K";

            for (int i = 0; i < giftArea.childCount; i++)
            {
                if (giftArea.GetChild(i).GetChild(0).gameObject.activeInHierarchy)
                {
                    //int total_amount = giftArea.GetChild(i).GetComponent<Gift_Click>().Gift_Quantity * giftArea.GetChild(i).GetComponent<Gift_Click>().Gift_Price;
                    //selected_gift_price = total_amount;

                    DbGifts dbGifts = new DbGifts();
                    dbGifts.gift = giftArea.GetChild(i).GetComponent<Gift_Click>().Gift_ID;
                    dbGifts.count = giftArea.GetChild(i).GetComponent<Gift_Click>().Gift_Quantity;
                    ProfileManager.instance.currentPlayer.recievedGifts.Remove(dbGifts);

                    for (int z = 0; z < ProfileManager.instance.currentPlayer.recievedGifts.Count; z++)
                    {
                        if (ProfileManager.instance.currentPlayer.recievedGifts[z].gift == selected_gift_id)
                        {
                            Debug.Log("Removing: " + ProfileManager.instance.currentPlayer.recievedGifts[z].gift);
                            ProfileManager.instance.currentPlayer.recievedGifts.RemoveAt(z);
                            break;
                        }
                    }
                    ProfileManager.instance.GetReward(RewardType.coins, selected_gift_price);
                    selected_gift_price = 0;
                    Destroy(giftArea.GetChild(i).gameObject);

                    //if(giftArea.GetChild(i).GetComponent<Gift_Click>().Gift_Quantity>1)
                    //{
                    //    giftArea.GetChild(i).GetComponent<Gift_Click>().Gift_Quantity--;
                    //    giftArea.GetChild(i).GetChild(3).gameObject.GetComponent<Text>().text = giftArea.GetChild(i).GetComponent<Gift_Click>().Gift_Quantity.ToString()+"x";
                    //}
                    //else
                    //{
                    //    Destroy(giftArea.GetChild(i).gameObject);
                    //}

                }
            }

        });
    }
    private void Update()
    {
        if (giftArea.childCount <=1)
        {
            Exchange_btn.interactable = false;
        }
        else
        {
            Exchange_btn.interactable = true;
        }
    }
    void DisplayData()
    {
        for (int n = 0; n < ProfileManager.instance.currentPlayer.recievedGifts.Count; n++)
        {
            for (int i=0;i< GameManager.instance.giftsDataFile.gifts.Count;i++)
            {
                if (ProfileManager.instance.currentPlayer.recievedGifts[n].gift== (int)GameManager.instance.giftsDataFile.gifts[i].gift)
                {
                    var gift = Instantiate(giftArea.GetChild(0), giftArea);
                    gift.gameObject.SetActive(true);
                    gift.GetChild(2).gameObject.GetComponent<Image>().sprite = GameManager.instance.giftsDataFile.gifts[i].giftImage;
                    //gift.GetChild(1).gameObject.GetComponent<Text>().text = GameManager.instance.giftsDataFile.gifts[i].charmXp.ToString();
                    gift.GetChild(1).gameObject.GetComponent<Text>().text = (GameManager.instance.giftsDataFile.gifts[i].price ).ToString();
                    gift.GetChild(1).gameObject.GetComponent<Kozykin.MultiLanguageItem>().strItems[0] = (GameManager.instance.giftsDataFile.gifts[i].price).ToString();
                    gift.GetChild(1).gameObject.GetComponent<Kozykin.MultiLanguageItem>().strItems[1] = (GameManager.instance.giftsDataFile.gifts[i].price).ToString();
                    gift.GetChild(1).gameObject.GetComponent<Kozykin.MultiLanguageItem>().text = (GameManager.instance.giftsDataFile.gifts[i].price).ToString();
                    gift.gameObject.GetComponent<Gift_Click>().Gift_Price = GameManager.instance.giftsDataFile.gifts[i].price/Exchange_Rate;
                    gift.gameObject.GetComponent<Gift_Click>().Gift_Quantity = ProfileManager.instance.currentPlayer.recievedGifts[n].count;
                    gift.gameObject.GetComponent<Gift_Click>().Gift_ID = ProfileManager.instance.currentPlayer.recievedGifts[n].gift;
                    gift.GetChild(3).gameObject.GetComponent<Text>().text = gift.gameObject.GetComponent<Gift_Click>().Gift_Quantity.ToString() + "x";
                    gift.GetChild(3).gameObject.GetComponent<Kozykin.MultiLanguageItem>().strItems[0] = gift.gameObject.GetComponent<Gift_Click>().Gift_Quantity.ToString() + "x";
                    gift.GetChild(3).gameObject.GetComponent<Kozykin.MultiLanguageItem>().strItems[1] = gift.gameObject.GetComponent<Gift_Click>().Gift_Quantity.ToString() + "x";
                    gift.GetChild(3).gameObject.GetComponent<Kozykin.MultiLanguageItem>().text = gift.gameObject.GetComponent<Gift_Click>().Gift_Quantity.ToString() + "x";
                    selection[i] = gift.GetChild(0).gameObject;
                }
            }
        }
        //for(int i=0;i< GameManager.instance.giftsDataFile.gifts.Count;i++)
        //{
        //    Debug.Log("Loop Outer value: " + i);
        //    if (i==0)
        //    {
        //        giftArea.GetChild(0).GetChild(2).gameObject.GetComponent<Image>().sprite = GameManager.instance.giftsDataFile.gifts[0].giftImage;
        //        giftArea.GetChild(0).GetChild(1).gameObject.GetComponent<Text>().text= GameManager.instance.giftsDataFile.gifts[0].charmXp.ToString();
        //        giftArea.GetChild(0).gameObject.GetComponent<Gift_Click>().Gift_Price = GameManager.instance.giftsDataFile.gifts[0].price;
        //        selection[0] = giftArea.GetChild(0).GetChild(0).gameObject;
        //    }
        //    else
        //    {
        //        var gift = Instantiate(giftArea.GetChild(0),giftArea);
        //        gift.GetChild(2).gameObject.GetComponent<Image>().sprite = GameManager.instance.giftsDataFile.gifts[i].giftImage;
        //        gift.GetChild(1).gameObject.GetComponent<Text>().text = GameManager.instance.giftsDataFile.gifts[i].charmXp.ToString();
        //        gift.gameObject.GetComponent<Gift_Click>().Gift_Price = GameManager.instance.giftsDataFile.gifts[i].price;
        //        selection[i] = gift.GetChild(0).gameObject;
        //    }
            
        //}
    }
}
