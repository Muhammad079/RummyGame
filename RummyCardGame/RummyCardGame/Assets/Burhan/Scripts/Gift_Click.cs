using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Gift_Click : MonoBehaviour
{
    public int Gift_Price, Gift_Quantity, Gift_ID;
    // Start is called before the first frame update
    void Start()
    {
        GetComponent<Button>().onClick.AddListener(() =>
        {
            for (int i = 0; i < GiftsExchange.instance.selection.Length; i++)
            {
                if(GiftsExchange.instance.selection[i]==null)
                {
                    continue;
                }
                else if (GiftsExchange.instance.selection[i].activeInHierarchy && GiftsExchange.instance.selection[i]!=null)
                {
                    GiftsExchange.instance.selection[i].SetActive(false);
                }
            }
            transform.GetChild(0).gameObject.SetActive(true);
            //GiftsExchange.instance.selected_gift_price = Gift_Price;
            //GiftsExchange.instance.selected_gift_quantity = Gift_Quantity;

            int total_amount = Gift_Quantity * Gift_Price;
            GiftsExchange.instance.selected_gift_price = total_amount;

            GiftsExchange.instance.selected_gift_id = Gift_ID;

            GiftsExchange.instance.Price[0].text = (total_amount ).ToString() ;
            GiftsExchange.instance.Price[1].text = (total_amount ).ToString();

            GiftsExchange.instance.Price[0].GetComponent<Kozykin.MultiLanguageItem>().strItems[0] = (total_amount).ToString();
            GiftsExchange.instance.Price[0].GetComponent<Kozykin.MultiLanguageItem>().strItems[1] = (total_amount).ToString();
            GiftsExchange.instance.Price[0].GetComponent<Kozykin.MultiLanguageItem>().text = (total_amount).ToString();

            GiftsExchange.instance.Price[1].GetComponent<Kozykin.MultiLanguageItem>().strItems[0] = (total_amount).ToString();
            GiftsExchange.instance.Price[1].GetComponent<Kozykin.MultiLanguageItem>().strItems[1] = (total_amount).ToString();
            GiftsExchange.instance.Price[1].GetComponent<Kozykin.MultiLanguageItem>().text = (total_amount).ToString();

            if (total_amount>=1000)
            {
                GiftsExchange.instance.Price[0].text = (total_amount / 1000).ToString() + "K";
                GiftsExchange.instance.Price[1].text = (total_amount / 1000).ToString() + "K";

                GiftsExchange.instance.Price[0].GetComponent<Kozykin.MultiLanguageItem>().strItems[0] = (total_amount / 1000).ToString() + "K";
                GiftsExchange.instance.Price[0].GetComponent<Kozykin.MultiLanguageItem>().strItems[1] = (total_amount / 1000).ToString() + "كيلو";
                GiftsExchange.instance.Price[0].GetComponent<Kozykin.MultiLanguageItem>().text = (total_amount / 1000).ToString() + "كيلو";

                GiftsExchange.instance.Price[1].GetComponent<Kozykin.MultiLanguageItem>().strItems[0] = (total_amount / 1000).ToString() + "K";
                GiftsExchange.instance.Price[1].GetComponent<Kozykin.MultiLanguageItem>().strItems[1] = (total_amount / 1000).ToString() + "كيلو";
                GiftsExchange.instance.Price[1].GetComponent<Kozykin.MultiLanguageItem>().text = (total_amount / 1000).ToString() + "كيلو";
            }
            
            
        });
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
