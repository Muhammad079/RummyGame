using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class VIP_Information_NEW : SceneLoader
{
    public scriptable_VIP_information scriptable_VIP_Information;
    public VIP_Page_Referencer Page;
    public Button Left_btn, Right_btn, back_btn;

    Transform[] Pages;
    int page_No;
    int counter;

    private void OnEnable()
    {
        page_No = 0;
        counter = 1;
        Pages = new Transform[scriptable_VIP_Information.VIPs.Count];

        transform.DOScale(new Vector3(1, 1, 1), 0.2f);

        back_btn.onClick.AddListener(()=> {
            transform.DOScale(new Vector3(0, 0, 0), 0.2f).OnComplete(()=> {
                StartCoroutine(OnClick());
                //SceneManager.LoadScene("Home");
            });
        });


        for (int i = 0; i < scriptable_VIP_Information.VIPs.Count; i++)
        {
            if(i==0)
            {
                Page.gameObject.SetActive(true);
                Page.VIP_Badge.sprite = GameManager.instance.VIP_Levels[i];

                Page.Current_VIP.text = "VIP " + (i+1);
                Page.Current_VIP.GetComponent<Kozykin.MultiLanguageItem>().strItems[0] = "VIP " + (i + 1);
                Page.Current_VIP.GetComponent<Kozykin.MultiLanguageItem>().strItems[1] = "VIP " + (i + 1);
                Page.Current_VIP.GetComponent<Kozykin.MultiLanguageItem>().text = "VIP " + (i + 1);

                Page.Next_VIP.text = "VIP " + (i+2);
                Page.Next_VIP.GetComponent<Kozykin.MultiLanguageItem>().strItems[0] = "VIP " + (i + 2);
                Page.Next_VIP.GetComponent<Kozykin.MultiLanguageItem>().strItems[1] = "VIP " + (i + 2);
                Page.Next_VIP.GetComponent<Kozykin.MultiLanguageItem>().text = "VIP " + (i + 2);

                Page.Points_Required.text = scriptable_VIP_Information.VIPs[i].VIP_Points.ToString()+" VIP";
                Page.Points_Required.GetComponent<Kozykin.MultiLanguageItem>().strItems[0] = scriptable_VIP_Information.VIPs[i].VIP_Points.ToString() + " VIP";
                Page.Points_Required.GetComponent<Kozykin.MultiLanguageItem>().strItems[1] = scriptable_VIP_Information.VIPs[i].VIP_Points.ToString() + " VIP";
                Page.Points_Required.GetComponent<Kozykin.MultiLanguageItem>().text = scriptable_VIP_Information.VIPs[i].VIP_Points.ToString() + " VIP";

                Page.Feature1.text = scriptable_VIP_Information.VIPs[i].Daily_Bonus.ToString();
                Page.Feature1.GetComponent<Kozykin.MultiLanguageItem>().strItems[0] = scriptable_VIP_Information.VIPs[i].Daily_Bonus.ToString();
                Page.Feature1.GetComponent<Kozykin.MultiLanguageItem>().strItems[1] = scriptable_VIP_Information.VIPs[i].Daily_Bonus.ToString();
                Page.Feature1.GetComponent<Kozykin.MultiLanguageItem>().text = scriptable_VIP_Information.VIPs[i].Daily_Bonus.ToString();

                Page.Feature2.text = scriptable_VIP_Information.VIPs[i].Friend_List.ToString();
                Page.Feature2.GetComponent<Kozykin.MultiLanguageItem>().strItems[0] = scriptable_VIP_Information.VIPs[i].Friend_List.ToString();
                Page.Feature2.GetComponent<Kozykin.MultiLanguageItem>().strItems[1] = scriptable_VIP_Information.VIPs[i].Friend_List.ToString();
                Page.Feature2.GetComponent<Kozykin.MultiLanguageItem>().text = scriptable_VIP_Information.VIPs[i].Friend_List.ToString();

                Page.Feature3.text = scriptable_VIP_Information.VIPs[i].Gift_Exchange_PER.ToString() + "%";
                Page.Feature3.GetComponent<Kozykin.MultiLanguageItem>().strItems[0] = scriptable_VIP_Information.VIPs[i].Gift_Exchange_PER.ToString() + "%";
                Page.Feature3.GetComponent<Kozykin.MultiLanguageItem>().strItems[1] = scriptable_VIP_Information.VIPs[i].Gift_Exchange_PER.ToString() + "%";
                Page.Feature3.GetComponent<Kozykin.MultiLanguageItem>().text = scriptable_VIP_Information.VIPs[i].Gift_Exchange_PER.ToString() + "%";

                Page.Feature4.text = scriptable_VIP_Information.VIPs[i].Golden_Spin.ToString();
                Page.Feature4.GetComponent<Kozykin.MultiLanguageItem>().strItems[0] = scriptable_VIP_Information.VIPs[i].Golden_Spin.ToString();
                Page.Feature4.GetComponent<Kozykin.MultiLanguageItem>().strItems[1] = scriptable_VIP_Information.VIPs[i].Golden_Spin.ToString();
                Page.Feature4.GetComponent<Kozykin.MultiLanguageItem>().text = scriptable_VIP_Information.VIPs[i].Golden_Spin.ToString();

                Page.Feature5.text = "+" + scriptable_VIP_Information.VIPs[i].Coins_Purchase_PER.ToString() + "%";
                Page.Feature5.GetComponent<Kozykin.MultiLanguageItem>().strItems[0] = "+" + scriptable_VIP_Information.VIPs[i].Coins_Purchase_PER.ToString() + "%";
                Page.Feature5.GetComponent<Kozykin.MultiLanguageItem>().strItems[1] = "+" + scriptable_VIP_Information.VIPs[i].Coins_Purchase_PER.ToString() + "%";
                Page.Feature5.GetComponent<Kozykin.MultiLanguageItem>().text = "+" + scriptable_VIP_Information.VIPs[i].Coins_Purchase_PER.ToString() + "%";

                Pages[i] = Page.transform;
            }
            else
            {
                var New_Page = Instantiate(Page, Page.transform.parent.transform);
                New_Page.gameObject.SetActive(false);

                New_Page.VIP_Badge.sprite = GameManager.instance.VIP_Levels[i];

                New_Page.Current_VIP.text = "VIP " + (i + 1);
                New_Page.Current_VIP.GetComponent<Kozykin.MultiLanguageItem>().strItems[0] = "VIP " + (i + 1);
                New_Page.Current_VIP.GetComponent<Kozykin.MultiLanguageItem>().strItems[1] = "VIP " + (i + 1);
                New_Page.Current_VIP.GetComponent<Kozykin.MultiLanguageItem>().text = "VIP " + (i + 1);

                New_Page.Next_VIP.text = "VIP " + (i + 2);
                New_Page.Next_VIP.GetComponent<Kozykin.MultiLanguageItem>().strItems[0] = "VIP " + (i + 2);
                New_Page.Next_VIP.GetComponent<Kozykin.MultiLanguageItem>().strItems[1] = "VIP " + (i + 2);
                New_Page.Next_VIP.GetComponent<Kozykin.MultiLanguageItem>().text = "VIP " + (i + 2);

                New_Page.Points_Required.text = scriptable_VIP_Information.VIPs[i].VIP_Points.ToString() + " VIP";
                New_Page.Points_Required.GetComponent<Kozykin.MultiLanguageItem>().strItems[0] = scriptable_VIP_Information.VIPs[i].VIP_Points.ToString() + " VIP";
                New_Page.Points_Required.GetComponent<Kozykin.MultiLanguageItem>().strItems[1] = scriptable_VIP_Information.VIPs[i].VIP_Points.ToString() + " VIP";
                New_Page.Points_Required.GetComponent<Kozykin.MultiLanguageItem>().text = scriptable_VIP_Information.VIPs[i].VIP_Points.ToString() + " VIP";

                New_Page.Feature1.text = scriptable_VIP_Information.VIPs[i].Daily_Bonus.ToString();
                New_Page.Feature1.GetComponent<Kozykin.MultiLanguageItem>().strItems[0] = scriptable_VIP_Information.VIPs[i].Daily_Bonus.ToString();
                New_Page.Feature1.GetComponent<Kozykin.MultiLanguageItem>().strItems[1] = scriptable_VIP_Information.VIPs[i].Daily_Bonus.ToString();
                New_Page.Feature1.GetComponent<Kozykin.MultiLanguageItem>().text = scriptable_VIP_Information.VIPs[i].Daily_Bonus.ToString();

                New_Page.Feature2.text = scriptable_VIP_Information.VIPs[i].Friend_List.ToString();
                New_Page.Feature2.GetComponent<Kozykin.MultiLanguageItem>().strItems[0] = scriptable_VIP_Information.VIPs[i].Friend_List.ToString();
                New_Page.Feature2.GetComponent<Kozykin.MultiLanguageItem>().strItems[1] = scriptable_VIP_Information.VIPs[i].Friend_List.ToString();
                New_Page.Feature2.GetComponent<Kozykin.MultiLanguageItem>().text = scriptable_VIP_Information.VIPs[i].Friend_List.ToString();

                New_Page.Feature3.text = scriptable_VIP_Information.VIPs[i].Gift_Exchange_PER.ToString() + "%";
                New_Page.Feature3.GetComponent<Kozykin.MultiLanguageItem>().strItems[0] = scriptable_VIP_Information.VIPs[i].Gift_Exchange_PER.ToString() + "%";
                New_Page.Feature3.GetComponent<Kozykin.MultiLanguageItem>().strItems[1] = scriptable_VIP_Information.VIPs[i].Gift_Exchange_PER.ToString() + "%";
                New_Page.Feature3.GetComponent<Kozykin.MultiLanguageItem>().text = scriptable_VIP_Information.VIPs[i].Gift_Exchange_PER.ToString() + "%";

                New_Page.Feature4.text = scriptable_VIP_Information.VIPs[i].Golden_Spin.ToString();
                New_Page.Feature4.GetComponent<Kozykin.MultiLanguageItem>().strItems[0] = scriptable_VIP_Information.VIPs[i].Golden_Spin.ToString();
                New_Page.Feature4.GetComponent<Kozykin.MultiLanguageItem>().strItems[1] = scriptable_VIP_Information.VIPs[i].Golden_Spin.ToString();
                New_Page.Feature4.GetComponent<Kozykin.MultiLanguageItem>().text = scriptable_VIP_Information.VIPs[i].Golden_Spin.ToString();

                New_Page.Feature5.text = "+" + scriptable_VIP_Information.VIPs[i].Coins_Purchase_PER.ToString() + "%";
                New_Page.Feature5.GetComponent<Kozykin.MultiLanguageItem>().strItems[0] = "+" + scriptable_VIP_Information.VIPs[i].Coins_Purchase_PER.ToString() + "%";
                New_Page.Feature5.GetComponent<Kozykin.MultiLanguageItem>().strItems[1] = "+" + scriptable_VIP_Information.VIPs[i].Coins_Purchase_PER.ToString() + "%";
                New_Page.Feature5.GetComponent<Kozykin.MultiLanguageItem>().text = "+" + scriptable_VIP_Information.VIPs[i].Coins_Purchase_PER.ToString() + "%";

                Pages[i] = New_Page.transform;
            }
        }


        if (ProfileManager.instance.currentPlayer.isVip)
        {
            int VIP_Player_Points = ProfileManager.instance.currentPlayer.vipXp;

            for (int i = 0; i < Pages.Length; i++)
            {
                if (scriptable_VIP_Information.VIPs[i].VIP_Points > VIP_Player_Points)
                {
                    Pages[i].GetComponent<VIP_Page_Referencer>().Points_Earned.text = VIP_Player_Points.ToString();
                    Pages[i].GetComponent<VIP_Page_Referencer>().Points_Earned.GetComponent<Kozykin.MultiLanguageItem>().strItems[0] = VIP_Player_Points.ToString();
                    Pages[i].GetComponent<VIP_Page_Referencer>().Points_Earned.GetComponent<Kozykin.MultiLanguageItem>().strItems[1] = VIP_Player_Points.ToString();
                    Pages[i].GetComponent<VIP_Page_Referencer>().Points_Earned.GetComponent<Kozykin.MultiLanguageItem>().text = VIP_Player_Points.ToString();

                    Pages[i].GetComponent<VIP_Page_Referencer>().Filler.fillAmount = (float)((float)VIP_Player_Points / (float)scriptable_VIP_Information.VIPs[i].VIP_Points);
                    //VIP_Player_Points -= scriptable_VIP_Information.VIPs[i].VIP_Points;

                    break;
                }
                else
                {
                    Pages[i].GetComponent<VIP_Page_Referencer>().Filler.fillAmount = 1;
                    VIP_Player_Points -= scriptable_VIP_Information.VIPs[i].VIP_Points;
                    Pages[i].GetComponent<VIP_Page_Referencer>().Points_Earned.text = "Completed";
                    Pages[i].GetComponent<VIP_Page_Referencer>().Points_Earned.GetComponent<Kozykin.MultiLanguageItem>().strItems[0] = "Completed";
                    Pages[i].GetComponent<VIP_Page_Referencer>().Points_Earned.GetComponent<Kozykin.MultiLanguageItem>().strItems[1] = "مكتمل";
                    Pages[i].GetComponent<VIP_Page_Referencer>().Points_Earned.GetComponent<Kozykin.MultiLanguageItem>().text = "مكتمل";
                }
                counter++;
            }
        }
        else
        {
            for (int i = 0; i < Pages.Length; i++)
            {
                Pages[i].GetComponent<VIP_Page_Referencer>().Points_Earned.text = "NOT VIP";
                Pages[i].GetComponent<VIP_Page_Referencer>().Points_Earned.GetComponent<Kozykin.MultiLanguageItem>().strItems[0] = "NOT VIP";
                Pages[i].GetComponent<VIP_Page_Referencer>().Points_Earned.GetComponent<Kozykin.MultiLanguageItem>().strItems[1] = "ليس من كبار الشخصيات";
                Pages[i].GetComponent<VIP_Page_Referencer>().Points_Earned.GetComponent<Kozykin.MultiLanguageItem>().text = "ليس من كبار الشخصيات";
            }
        }




        Pages[0].gameObject.SetActive(true);

        for (int i = 1; i < Pages.Length; i++)
        {
            Pages[i].transform.DOMoveX(-100, 0.01f);
        }

        


        Right_btn.onClick.AddListener(()=> {
            page_No++;
            if (page_No < Pages.Length && page_No<= counter)
            {
                
                Pages[page_No].gameObject.SetActive(true);
                Pages[page_No-1].transform.DOMoveX(100, 0.2f).OnComplete(() => {
                    Pages[page_No-1].gameObject.SetActive(false);
                    Pages[page_No].transform.DOMoveX(0, 0.2f);

                });
                
            }
            else
            {
                page_No = counter;
            }
            if(page_No >=Pages.Length)
            {
                page_No = Pages.Length - 1;
            }
        });
        Left_btn.onClick.AddListener(() => {
            page_No--;
            if (page_No < Pages.Length)
            {

                Pages[page_No].gameObject.SetActive(true);
                Pages[page_No + 1].transform.DOMoveX(-100, 0.2f).OnComplete(() => {
                    Pages[page_No + 1].gameObject.SetActive(false);
                    Pages[page_No].transform.DOMoveX(0, 0.2f);

                });

            }
            if (page_No < 0)
            {
                page_No = Pages.Length + 1;
            }
        });

        



        InvokeRepeating(nameof(Button_Interaction),0.1f,0.1f);
    }

    private void Button_Interaction()
    {
        #region Right Left Interaction
        if (Pages[0].gameObject.activeInHierarchy)
        {
            Left_btn.interactable = false;
        }
        else
        {
            Left_btn.interactable = true;
        }
        if (Pages[Pages.Length - 1].gameObject.activeInHierarchy)
        {
            Right_btn.interactable = false;
        }
        else
        {
            Right_btn.interactable = true;
        }
        #endregion
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    public override void Update()
    {
        base.Update();
    }
}
