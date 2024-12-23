using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class VIP_Information_Handler : MonoBehaviour
{
    public GameObject[] VIP_Levels, VIP_Points, Daily_Bonus, Friend_List, Gift__Exchange, Golden_Spin, Coins_Purchase;
    public GameObject Highlighter;
    public Button Back;

    public scriptable_VIP_information VIP_Information;

    


    private void OnEnable()
    {
        transform.DOScale(new Vector3(1, 1, 1), 0.2f);
    }
    // Start is called before the first frame update
    void Start()
    {
        Highlighter.SetActive(false);

        if(ProfileManager.instance.currentPlayer.isVip)
        {
            ProfileManager.instance.currentPlayer.VIP_Level =1;
        }





        for (int i = 0;i< VIP_Information.VIPs.Count;i++)
        {
            VIP_Points[i].transform.GetChild(0).GetChild(0).GetComponent<Image>().fillAmount = 0;



            int Points_value = VIP_Information.VIPs[i].VIP_Points;
            string Conv_Points = null;
            if (Points_value / 1000 != 0)
            {
                Points_value = Points_value / 1000;
                Conv_Points = Points_value + "K";
                float Float_Points = Points_value;
                if (Float_Points / 1000 >= 1)
                {
                   
                    Float_Points = Float_Points / 1000;
                    Conv_Points = Float_Points + "M";
                }
            }
            int PlayerXP = ProfileManager.instance.currentPlayer.vipXp;
            string conv_Pxp=null;
            if (PlayerXP/1000!=0)
            {
                PlayerXP = PlayerXP / 1000;
                conv_Pxp = PlayerXP + "K";
                float PlayerXP_float = PlayerXP;
                if (PlayerXP_float / 1000 >= 1)
                {
                    PlayerXP_float = PlayerXP_float / 1000;
                    conv_Pxp = PlayerXP_float + "M";
                }
            }
            VIP_Points[i].transform.GetChild(1).GetComponent<Text>().text = conv_Pxp + "/" + Conv_Points;
            VIP_Points[i].transform.GetChild(1).GetComponent<Kozykin.MultiLanguageItem>().strItems[0] = conv_Pxp + "/" + Conv_Points;
            VIP_Points[i].transform.GetChild(1).GetComponent<Kozykin.MultiLanguageItem>().strItems[1] = conv_Pxp + "/" + Conv_Points;
            VIP_Points[i].transform.GetChild(1).GetComponent<Kozykin.MultiLanguageItem>().text = conv_Pxp + "/" + Conv_Points;


            int Daily_Bonus_val = VIP_Information.VIPs[i].Daily_Bonus;
            string Conv_Points2 = null;
            if (Daily_Bonus_val/1000 !=0)
            {
                Daily_Bonus_val = Daily_Bonus_val / 1000;
                Conv_Points2 = Daily_Bonus_val + "K";
            }
            Daily_Bonus[i].transform.GetChild(0).GetComponent<Text>().text = Conv_Points2;
            Daily_Bonus[i].transform.GetChild(0).GetComponent<Kozykin.MultiLanguageItem>().strItems[0] = Conv_Points2;
            Daily_Bonus[i].transform.GetChild(0).GetComponent<Kozykin.MultiLanguageItem>().strItems[1] = Conv_Points2;
            Daily_Bonus[i].transform.GetChild(0).GetComponent<Kozykin.MultiLanguageItem>().text = Conv_Points2;

            Friend_List[i].transform.GetChild(0).GetComponent<Text>().text = VIP_Information.VIPs[i].Friend_List.ToString();
            Friend_List[i].transform.GetChild(0).GetComponent<Kozykin.MultiLanguageItem>().strItems[0] = VIP_Information.VIPs[i].Friend_List.ToString();
            Friend_List[i].transform.GetChild(0).GetComponent<Kozykin.MultiLanguageItem>().strItems[1] = VIP_Information.VIPs[i].Friend_List.ToString();
            Friend_List[i].transform.GetChild(0).GetComponent<Kozykin.MultiLanguageItem>().text = VIP_Information.VIPs[i].Friend_List.ToString();

            Gift__Exchange[i].transform.GetChild(0).GetComponent<Text>().text = "+"+VIP_Information.VIPs[i].Gift_Exchange_PER+"%";
            Golden_Spin[i].transform.GetChild(0).GetComponent<Text>().text = VIP_Information.VIPs[i].Golden_Spin.ToString();
            Coins_Purchase[i].transform.GetChild(0).GetComponent<Text>().text = "+"+VIP_Information.VIPs[i].Coins_Purchase_PER + "%";


            if(ProfileManager.instance.currentPlayer.vipXp >= VIP_Information.VIPs[i].VIP_Points)
            {
                ProfileManager.instance.currentPlayer.VIP_Level++;
                VIP_Points[i].transform.GetChild(0).GetChild(0).GetComponent<Image>().fillAmount = 1;
            }
        }

        if(ProfileManager.instance.currentPlayer.isVip)
        {
            Highlighter.SetActive(true);
            Highlighter.GetComponent<HorizontalLayoutGroup>().padding.left = 185;
            for (int i = 1; i < ProfileManager.instance.currentPlayer.VIP_Level; i++)
            {
                Highlighter.GetComponent<HorizontalLayoutGroup>().padding.left += 110;
            }


        }
        Back.onClick.AddListener(() =>
        {
            transform.DOScale(new Vector3(0, 0, 0), 0.2f).OnComplete(()=> {
                SceneManager.LoadScene("Home");
            });
        });
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
