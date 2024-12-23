using DG.Tweening;
using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class TableType : MonoBehaviour
{
    [SerializeField] private GameObject mainParent = null, gameSelection = null;
    [SerializeField] private Table tableData = null;
    private void Start()
    {
        GetComponent<Button>().onClick.AddListener(OnClick);
    }
    void OnClick()
    {
            if (tableData.totalPlayers > 2)
                GameManager.instance.selectedXPData = GameManager.instance.xpData;
            else
                GameManager.instance.selectedXPData = GameManager.instance.xpData_2Players;
            GameManager.instance.selectedTable = tableData;
        if(mainParent !=null)
        {
            mainParent.SetActive(false);
        }
        Invoke(nameof(matchmaking),0.5f);
            
    }

    private void matchmaking()
    {
        if (gameSelection != null)
        {
            if (!SceneManager.GetActiveScene().name.Contains("Tournament"))
            {
                if(ProfileManager.instance.currentPlayer.TwoM_FeeCheck && SceneManager.GetActiveScene().name.Contains("2mEvent"))
                {
                    gameSelection.SetActive(true);
                }
                else if (ProfileManager.instance.currentPlayer.TwoHK_FeeDeducted_ID.Contains(ProfileManager.instance.currentPlayer.TwoHK_Table_Selected) && SceneManager.GetActiveScene().name.Contains("200kEvent"))
                {
                    if (ProfileManager.instance.currentPlayer.TwoHK_Selected)
                    {
                        if (twoHK_Event_Tables.TwoHK_Ready)
                        {
                            gameSelection.SetActive(true);
                        }
                        else
                        {
                            //Debug.Log("Setting Arabic Coins");
                            GameManager.instance.Not_Enough_C_Panel.SetActive(true);
                            GameManager.instance.Not_Enough_C_Panel.transform.GetChild(0).GetComponent<Text>().text = "Not Enough Gems";
                            GameManager.instance.Not_Enough_C_Panel.transform.GetChild(0).GetComponent<Kozykin.MultiLanguageItem>().strItems[0] = "Not Enough Gems";
                            GameManager.instance.Not_Enough_C_Panel.transform.GetChild(0).GetComponent<Kozykin.MultiLanguageItem>().strItems[1] = "الأحجار الكريمة غير كافية";
                            GameManager.instance.Not_Enough_C_Panel.transform.GetChild(0).GetComponent<Kozykin.MultiLanguageItem>().text = "الأحجار الكريمة غير كافية";
                            GameManager.instance.Not_Enough_C_Panel.transform.DOScale(new Vector3(1, 1, 1), 0.5f).SetEase(Ease.OutBounce);
                        }
                    }
                    else
                    {
                        gameSelection.SetActive(true);
                    }
                }
                else
                {
                    GameManager.instance.Not_Enough_C_Panel.SetActive(true);
                    GameManager.instance.Not_Enough_C_Panel.transform.DOScale(new Vector3(1, 1, 1), 0.5f).SetEase(Ease.OutBounce);
                    if(GameManager.instance.Not_Enough_C_Panel.transform.GetChild(0).GetComponent<Kozykin.MultiLanguageItem>())
                    {
                        GameManager.instance.Not_Enough_C_Panel.transform.GetChild(0).GetComponent<Kozykin.MultiLanguageItem>().strItems[0] = "Not Enough Coins";
                        GameManager.instance.Not_Enough_C_Panel.transform.GetChild(0).GetComponent<Kozykin.MultiLanguageItem>().strItems[1] = "عملات غير كافية";
                        GameManager.instance.Not_Enough_C_Panel.transform.GetChild(0).GetComponent<Kozykin.MultiLanguageItem>().text = "عملات غير كافية";
                    }
                    
                }
            }
            else
            {
                gameSelection.SetActive(true);
            }
        }
    }
}
[System.Serializable]
public class Table
{
    public int totalPlayers = 0;
    public float firstPosMultiplier = 0;
    public int secondPosPercentage = 0;
    public int thirdPosPercentage = 0;
}