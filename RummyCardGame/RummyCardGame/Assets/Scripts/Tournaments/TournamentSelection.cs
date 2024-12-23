using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class TournamentSelection : MonoBehaviour
{
    public GameObject Tournament_Selection_Panel_UI, Selected_Tournament_UI;
    public static string Tournament_Name;
    //public static int entranceFee_amount;
    public TournamentType thisEvent;
    public string TournamentName;
    [SerializeField] private int entranceFee = 0;
    [SerializeField] private int noOfGamesToWin = 0;

    private void Update()
    {
       //if (TournamentName == "Regular Tournament" && ProfileManager.instance.currentPlayer.tournamentFeeCheck)
       // {
       //     this.GetComponent<Button>().interactable = true;
       // }
       // else if (ProfileManager.instance.currentPlayer.coins >= entranceFee )
       // {
       //     this.GetComponent<Button>().interactable = true;
       // }
       // else
       // {
       //     this.GetComponent<Button>().interactable = false;
       //     //Debug.Log("Not enough coins");
       // }



        //if (ProfileManager.instance.currentPlayer.trophies < 210)
        //{
        //    GetComponent<Button>().interactable = false;
        //}
        //else
        //{
        //    GetComponent<Button>().interactable = true;
        //}
    }
    void Start()
    {
        
        this.GetComponent<Button>().onClick.AddListener(OnClick);
    }
    void OnClick()
    {
        if (ProfileManager.instance.currentPlayer.trophies >= 210)
        {
            if (TournamentName == "Regular Tournament" && !ProfileManager.instance.currentPlayer.tournamentFeeCheck)
            {
                if (ProfileManager.instance.currentPlayer.coins >= entranceFee)
                {
                    if (TournamentName == "Regular Tournament")
                    {
                        if (!ProfileManager.instance.currentPlayer.tournamentFeeCheck)
                        {
                            ProfileManager.instance.currentPlayer.coins -= entranceFee;
                            ProfileManager.instance.currentPlayer.tournamentFeeCheck = true;
                        }
                    }
                    GameManager.instance.selectedTournament = thisEvent;
                    Tournament_Name = TournamentName;
                    Selected_Tournament_UI.SetActive(true);
                    Selected_Tournament_UI.transform.DOScale(new Vector3(1, 1, 1), 0.4f).SetEase(Ease.OutQuad);
                }
                else
                {
                    GameManager.instance.Not_Enough_C_Panel.SetActive(true);
                    GameManager.instance.Not_Enough_C_Panel.transform.DOScale(new Vector3(1, 1, 1), 0.5f).SetEase(Ease.OutBounce);
                }
            }
            else if (TournamentName == "Regular Tournament" && ProfileManager.instance.currentPlayer.tournamentFeeCheck)
            {
                GameManager.instance.selectedTournament = thisEvent;
                Tournament_Name = TournamentName;
                Selected_Tournament_UI.SetActive(true);
                Selected_Tournament_UI.transform.DOScale(new Vector3(1, 1, 1), 0.4f).SetEase(Ease.OutQuad);
            }
            else
            {
                GameManager.instance.Not_Enough_C_Panel.SetActive(true);
                GameManager.instance.Not_Enough_C_Panel.transform.DOScale(new Vector3(1, 1, 1), 0.5f).SetEase(Ease.OutBounce);
            }
        }
        else
        {
            GameManager.instance.Not_Enough_C_Panel.SetActive(true);
            GameManager.instance.Not_Enough_C_Panel.transform.GetChild(0).GetComponent<Text>().text = "Earn 210 Trophies to play Tournament";
            GameManager.instance.Not_Enough_C_Panel.transform.GetChild(0).GetComponent<Kozykin.MultiLanguageItem>().strItems[0] = "Earn 210 Trophies to play Tournament";
            GameManager.instance.Not_Enough_C_Panel.transform.GetChild(0).GetComponent<Kozykin.MultiLanguageItem>().strItems[1] = "اربح 210 كؤوس للعب البطولة";
            GameManager.instance.Not_Enough_C_Panel.transform.GetChild(0).GetComponent<Kozykin.MultiLanguageItem>().text = "اربح 210 كؤوس للعب البطولة";
            GameManager.instance.Not_Enough_C_Panel.transform.DOScale(new Vector3(1, 1, 1), 0.5f).SetEase(Ease.OutBounce);
        }

        
    }
    public void Fade_Out()
    {
      
    }
}
