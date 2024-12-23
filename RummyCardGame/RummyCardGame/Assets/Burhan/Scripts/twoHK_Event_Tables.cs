using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class twoHK_Event_Tables : MonoBehaviour
{
    public two_HK_Values two_HK_data;
    public Text Prize, Lock_trophies, Win_trophies, Loss_trophies, In_Filler_trophies, Play_btn_Coins, Play_btn_Gems;
    public GameObject Lock;//, Search_opponent_Panel;
    public Button Play_btn;
    public Image Filler;
    public int ID;

    // Start is called before the first frame update
    void Start()
    {
        Invoke(nameof(refresh), 0.1f);
        
    }

    private void refresh()
    {
        //Prize.text = two_HK_data.Total_Prize.ToString();
        //Lock_trophies.text = two_HK_data.Crowns_Req_to_Unlock.ToString();
        //Win_trophies.text = "+"+ two_HK_data.Crowns_Win.ToString();
        //Loss_trophies.text = "-" + two_HK_data.Crowns_Loss.ToString();
        //In_Filler_trophies.text = ProfileManager.instance.currentPlayer.TwoHK_Crowns.ToString();
        //Play_btn_Coins.text = two_HK_data.Entry_fee.ToString();

        if (Manager.instance.m_CurrentLanguage == 0)
        {
            Prize.GetComponent<Kozykin.MultiLanguageItem>().strItems[0] = two_HK_data.Total_Prize.ToString();
            Lock_trophies.GetComponent<Kozykin.MultiLanguageItem>().strItems[0] = two_HK_data.Crowns_Req_to_Unlock.ToString();
            Win_trophies.GetComponent<Kozykin.MultiLanguageItem>().strItems[0] = "+" + two_HK_data.Crowns_Win.ToString();
            Loss_trophies.GetComponent<Kozykin.MultiLanguageItem>().strItems[0] = "-" + two_HK_data.Crowns_Loss.ToString();
            In_Filler_trophies.GetComponent<Kozykin.MultiLanguageItem>().strItems[0] = ProfileManager.instance.currentPlayer.TwoHK_Crowns.ToString();
            Play_btn_Coins.GetComponent<Kozykin.MultiLanguageItem>().strItems[0] = two_HK_data.Entry_fee.ToString();
        }
        else if (Manager.instance.m_CurrentLanguage == 1)
        {
            Prize.GetComponent<Kozykin.MultiLanguageItem>().strItems[1] = two_HK_data.Total_Prize.ToString();
            Lock_trophies.GetComponent<Kozykin.MultiLanguageItem>().strItems[1] = two_HK_data.Crowns_Req_to_Unlock.ToString();
            Win_trophies.GetComponent<Kozykin.MultiLanguageItem>().strItems[1] = "+" + two_HK_data.Crowns_Win.ToString();
            Loss_trophies.GetComponent<Kozykin.MultiLanguageItem>().strItems[1] = "-" + two_HK_data.Crowns_Loss.ToString();
            In_Filler_trophies.GetComponent<Kozykin.MultiLanguageItem>().strItems[1] = ProfileManager.instance.currentPlayer.TwoHK_Crowns.ToString();
            Play_btn_Coins.GetComponent<Kozykin.MultiLanguageItem>().strItems[1] = two_HK_data.Entry_fee.ToString();

            Prize.GetComponent<Kozykin.MultiLanguageItem>().text = two_HK_data.Total_Prize.ToString();
            Lock_trophies.GetComponent<Kozykin.MultiLanguageItem>().text = two_HK_data.Crowns_Req_to_Unlock.ToString();
            Win_trophies.GetComponent<Kozykin.MultiLanguageItem>().text = "+" + two_HK_data.Crowns_Win.ToString();
            Loss_trophies.GetComponent<Kozykin.MultiLanguageItem>().text = "-" + two_HK_data.Crowns_Loss.ToString();
            In_Filler_trophies.GetComponent<Kozykin.MultiLanguageItem>().text = ProfileManager.instance.currentPlayer.TwoHK_Crowns.ToString();
            Play_btn_Coins.GetComponent<Kozykin.MultiLanguageItem>().text = two_HK_data.Entry_fee.ToString();
        }
        //InvokeRepeating(nameof(Play_btn_refresh), 1, 1);
        InvokeRepeating(nameof(Play_btn_refresh), 1, 1);


    }

    private void Play_btn_refresh()
    {
        if (ID == 1)
        {
            two_HK_data.Gems_Recovery = ProfileManager.instance.currentPlayer.GetTwoHK_Gems_Change.Table1_Gems;
        }
        else if (ID == 2)
        {
            two_HK_data.Gems_Recovery = ProfileManager.instance.currentPlayer.GetTwoHK_Gems_Change.Table2_Gems;
        }
        else if (ID == 3)
        {
            two_HK_data.Gems_Recovery = ProfileManager.instance.currentPlayer.GetTwoHK_Gems_Change.Table3_Gems;
        }
        else if (ID == 4)
        {
            two_HK_data.Gems_Recovery = ProfileManager.instance.currentPlayer.GetTwoHK_Gems_Change.Table4_Gems;
        }

        if (ProfileManager.instance.currentPlayer.TwoHK_LostTable_ID.Contains(ID))
        {
            if (ProfileManager.instance.currentPlayer.gems < two_HK_data.Gems_Recovery)
            {
                Play_btn.interactable = false;
            }
            else
            {
                Play_btn.interactable = true;
            }
            Play_btn.transform.GetChild(1).gameObject.SetActive(true);
            Play_btn.transform.GetChild(1).GetChild(0).GetComponent<Text>().text = two_HK_data.Gems_Recovery.ToString();
            Play_btn.transform.GetChild(1).GetChild(0).GetComponent<Kozykin.MultiLanguageItem>().strItems[0] = two_HK_data.Gems_Recovery.ToString();
            Play_btn.transform.GetChild(1).GetChild(0).GetComponent<Kozykin.MultiLanguageItem>().strItems[1] = two_HK_data.Gems_Recovery.ToString();
            Play_btn.transform.GetChild(1).GetChild(0).GetComponent<Kozykin.MultiLanguageItem>().text = two_HK_data.Gems_Recovery.ToString();
            Play_btn.transform.GetChild(0).gameObject.SetActive(false);
            Play_btn.transform.GetChild(2).gameObject.SetActive(false);

        }
        else
        {
            if (ProfileManager.instance.currentPlayer.TwoHK_FeeDeducted_ID.Contains(ID))
            {
                Play_btn.transform.GetChild(2).gameObject.SetActive(true);
                Play_btn.transform.GetChild(1).gameObject.SetActive(false);
                Play_btn.transform.GetChild(0).gameObject.SetActive(false);
            }
            else
            {
                Play_btn.transform.GetChild(2).gameObject.SetActive(false);
                Play_btn.transform.GetChild(1).gameObject.SetActive(false);
                Play_btn.transform.GetChild(0).gameObject.SetActive(true);
            }
        }

        if (two_HK_Event_Handler.is_TwoHK_ended)
        {
            Play_btn.interactable = false;
            Play_btn.transform.GetChild(0).GetComponent<Image>().color = Color.red;
            Play_btn.transform.GetChild(1).GetComponent<Image>().color = Color.red;
            Play_btn.transform.GetChild(2).GetComponent<Image>().color = Color.red;
        }
        else
        {
            if(ProfileManager.instance.currentPlayer.TwoHK_Crowns >= two_HK_data.Crowns_Req_to_Unlock)
            {
                Play_btn.interactable = true;
                Play_btn.transform.GetChild(0).GetComponent<Image>().color = Color.white;
                Play_btn.transform.GetChild(1).GetComponent<Image>().color = Color.white;
                Play_btn.transform.GetChild(2).GetComponent<Image>().color = Color.white;
                Play_btn.onClick.AddListener(TwoHK_play_Game);
            }
            else
            {
                Play_btn.interactable = false;
                Play_btn.transform.GetChild(0).GetComponent<Image>().color = Color.red;
                Play_btn.transform.GetChild(1).GetComponent<Image>().color = Color.red;
                Play_btn.transform.GetChild(2).GetComponent<Image>().color = Color.red;
            }

            
        }
    }
    public static bool TwoHK_Ready;
    private void TwoHK_play_Game()
    {
        GameManager.instance.selectedBid = two_HK_data.Entry_fee;
        GameManager.instance.selectedTable.totalPlayers = 2;
        if (ProfileManager.instance.currentPlayer.TwoHK_LostTable_ID.Contains(ID))
        {
            if(ProfileManager.instance.currentPlayer.gems>=two_HK_data.Gems_Recovery)
            {
                if (ProfileManager.instance.currentPlayer.TwoHK_Table_Selected == 1)
                {
                    two_HK_data.Gems_Recovery = ProfileManager.instance.currentPlayer.GetTwoHK_Gems_Change.Table1_Gems;
                    ProfileManager.instance.currentPlayer.GetTwoHK_Gems_Change.Table1_Gems += 5;
                    Debug.Log("Deduction applied: " + ProfileManager.instance.currentPlayer.TwoHK_Table_Selected);
                }
                else if (ProfileManager.instance.currentPlayer.TwoHK_Table_Selected == 2)
                {
                    two_HK_data.Gems_Recovery = ProfileManager.instance.currentPlayer.GetTwoHK_Gems_Change.Table2_Gems;
                    ProfileManager.instance.currentPlayer.GetTwoHK_Gems_Change.Table2_Gems += 5;
                    Debug.Log("Deduction applied: " + ProfileManager.instance.currentPlayer.TwoHK_Table_Selected);
                }
                else if (ProfileManager.instance.currentPlayer.TwoHK_Table_Selected == 3)
                {
                    two_HK_data.Gems_Recovery = ProfileManager.instance.currentPlayer.GetTwoHK_Gems_Change.Table3_Gems;
                    ProfileManager.instance.currentPlayer.GetTwoHK_Gems_Change.Table3_Gems += 5;
                    Debug.Log("Deduction applied: " + ProfileManager.instance.currentPlayer.TwoHK_Table_Selected);
                }
                else if (ProfileManager.instance.currentPlayer.TwoHK_Table_Selected == 4)
                {
                    two_HK_data.Gems_Recovery = ProfileManager.instance.currentPlayer.GetTwoHK_Gems_Change.Table4_Gems;
                    ProfileManager.instance.currentPlayer.GetTwoHK_Gems_Change.Table4_Gems += 5;
                    Debug.Log("Deduction applied: " + ProfileManager.instance.currentPlayer.TwoHK_Table_Selected);
                }

                ProfileManager.instance.currentPlayer.gems -= two_HK_data.Gems_Recovery;
                //two_HK_data.Gems_Recovery += 5;
                TwoHK_Ready = true;
                ProfileManager.instance.currentPlayer.TwoHK_LostTable_ID.Remove(ID);

                GameManager.instance.selectedBid = two_HK_data.Entry_fee;
                GameManager.instance.selectedTable.totalPlayers = 2;
                GameManager.instance.selectedTable.firstPosMultiplier = 1.6f;
                GameManager.instance.selectedTable.secondPosPercentage = 2;
                GameManager.instance.selectedTable.thirdPosPercentage = 1;
                GameManager.instance.selectedXPData = GameManager.instance.xpData_2Players;

                PhotonNetworkingManager.instance.JoinTable();
                ProfileManager.instance.currentPlayer.TwoHK_Selected_TableID = ID;
                ProfileManager.instance.SaveUserData();
            }
            else
            {
                TwoHK_Ready = false;
            }
            

        }
        else
        {
            if (ProfileManager.instance.currentPlayer.TwoHK_FeeDeducted_ID.Contains(ID))
            {
                TwoHK_Ready = true;
                GameManager.instance.selectedBid = two_HK_data.Entry_fee;
                GameManager.instance.selectedTable.totalPlayers = 2;
                GameManager.instance.selectedTable.firstPosMultiplier = 1.6f;
                GameManager.instance.selectedTable.secondPosPercentage = 2;
                GameManager.instance.selectedTable.thirdPosPercentage = 1;
                GameManager.instance.selectedXPData = GameManager.instance.xpData_2Players;

                PhotonNetworkingManager.instance.JoinTable();
                ProfileManager.instance.currentPlayer.TwoHK_Selected_TableID = ID;
            }
            else
            {
                if(ProfileManager.instance.currentPlayer.coins>= two_HK_data.Entry_fee)
                {
                    TwoHK_Ready = true;
                    ProfileManager.instance.currentPlayer.coins -= two_HK_data.Entry_fee;
                    ProfileManager.instance.currentPlayer.TwoHK_FeeDeducted_ID.Add(ID);
                    GameManager.instance.selectedBid = two_HK_data.Entry_fee;
                    GameManager.instance.selectedTable.totalPlayers = 2;
                    GameManager.instance.selectedTable.firstPosMultiplier = 1.6f;
                    GameManager.instance.selectedTable.secondPosPercentage = 2;
                    GameManager.instance.selectedTable.thirdPosPercentage = 1;
                    GameManager.instance.selectedXPData = GameManager.instance.xpData_2Players;

                    PhotonNetworkingManager.instance.JoinTable();
                    ProfileManager.instance.currentPlayer.TwoHK_Selected_TableID = ID;
                }
                else
                {
                    GameManager.instance.Not_Enough_C_Panel.SetActive(true);
                    GameManager.instance.Not_Enough_C_Panel.transform.DOScale(new Vector3(1, 1, 1), 0.5f).SetEase(Ease.OutBounce);
                }

                
            }
        }

            
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
[System.Serializable]
public class TwoHK_Gems_Change
{
    public int Table1_Gems=5;
    public int Table2_Gems=5;
    public int Table3_Gems=5;
    public int Table4_Gems=5;
}