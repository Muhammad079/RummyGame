using DG.Tweening;
using Photon.Pun;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Selected_Tournament_Handler : MonoBehaviour
{
    public Scriptable_Tour_Rewards _Rewards;
    [SerializeField] Image Current_Badge, Next_Badge;
    [SerializeField] private Scriptable_TournamentXP scriptable_TournamentXP = null;
    public Slider Rewards_Unlocker_SliderBar, LevelUp_Unlocker_SliderBar;
    public GameObject Main_Parent, Close_Btn;
    public GameObject[] Chances_Used_Display;
    public Text Win_Match_Display, Level_Display;
    public Text Tournament_Name_Display;
    public Transform twoM_Win_Rewards_Container, twoOOK_Win_Rewards_Container, Regular_Win_Rewards_Container;
    public GameObject Win_Rewards;
    public GameObject Play_Continue_btn;
    public GameObject Info_Panel;
    public Button Info_Btn;
    int Level_Display_Counter_R;
    int M_Instantiate_chk, K_Instantiate_chk, Regular_Instantiate_chk;
    public GameObject Search_Panel;
    //public GameObject waitingPanel;
    private void Start()
    {
        Level_Display_Counter_R = 0;
        Rewards_Unlocker_SliderBar.value = 0;
        LevelUp_Unlocker_SliderBar.value = 0;

        Info_Btn.onClick.AddListener(() => {
            Info_Panel.SetActive(true);
            Info_Panel.transform.DOScale(new Vector3(1, 1, 1), 0.2f).SetEase(Ease.OutBack);
        });
        Info_Panel.transform.GetChild(2).gameObject.GetComponent<Button>().onClick.AddListener(() => {
            Info_Panel.transform.DOScale(new Vector3(0, 0, 0), 0.2f).SetEase(Ease.InBack).OnComplete(() => {
                Info_Panel.SetActive(false);
            });
        });

        M_Instantiate_chk = 0; K_Instantiate_chk = 0; Regular_Instantiate_chk = 0;

        Play_Continue_btn.GetComponent<Button>().onClick.AddListener(Play_or_Continue_Check_Decision);
        Close_Btn.GetComponent<Button>().onClick.AddListener(Closing);

        
    }
    private void OnEnable()
    {
        if (!PhotonNetworkingManager.instance.bid.ContainsKey("Tournament"))
        {
            PhotonNetworkingManager.instance.bid.Add("Tournament", tag);
            Debug.Log("Tournament Key added");

        }
        Invoke(nameof(Refresh), 0.2f);
    }
    private void OnDisable()
    {
        if (PhotonNetworkingManager.instance.bid.ContainsKey("Tournament"))
        {
            PhotonNetworkingManager.instance.bid.Remove("Tournament");
            Debug.Log("Tournament Key removed");

        }
    }
    private void Closing()
    {
        Main_Parent.transform.DOScale(new Vector3(0, 0, 0), 0.2f).SetEase(Ease.OutQuad).OnComplete(Fade_Out);
    }

    private void Fade_Out()
    {
        Main_Parent.SetActive(false);
    }

    private void Play_or_Continue_Check_Decision()
    {
        if (TournamentSelection.Tournament_Name == "Regular Tournament")
        {
            if (Play_Continue_btn.transform.GetChild(0).gameObject.activeInHierarchy)
            {
                ProfileManager.instance.currentPlayer.R_Count++;

                
                

                PhotonNetworkingManager.instance.JoinTable();
                //waitingPanel.SetActive(true);


                //SceneManager.LoadScene("GamePlay");
            }
            else if (Play_Continue_btn.transform.GetChild(1).gameObject.activeInHierarchy)
            {
                if (ProfileManager.instance.currentPlayer.gems < ProfileManager.instance.currentPlayer.Gems_deduction_R)
                {
                    Debug.Log("Not enough gems");

                    Invoke(nameof(break_Matchmake),1);

                    
                    GameManager.instance.Not_Enough_C_Panel.SetActive(true);
                    GameManager.instance.Not_Enough_C_Panel.transform.GetChild(0).GetComponent<Text>().text = "Not Enough Gems";
                    GameManager.instance.Not_Enough_C_Panel.transform.GetChild(0).GetComponent<Kozykin.MultiLanguageItem>().strItems[0] = "Not Enough Gems";
                    GameManager.instance.Not_Enough_C_Panel.transform.GetChild(0).GetComponent<Kozykin.MultiLanguageItem>().strItems[1] = "الأحجار الكريمة غير كافية";
                    GameManager.instance.Not_Enough_C_Panel.transform.GetChild(0).GetComponent<Kozykin.MultiLanguageItem>().text = "الأحجار الكريمة غير كافية";

                    GameManager.instance.Not_Enough_C_Panel.transform.DOScale(new Vector3(1, 1, 1), 0.5f).SetEase(Ease.OutBounce);
                }
                else
                {
                    ProfileManager.instance.currentPlayer.gems -= ProfileManager.instance.currentPlayer.Gems_deduction_R;
                    ProfileManager.instance.currentPlayer.Gems_deduction_R += 12;
                    ProfileManager.instance.currentPlayer.R_Count++;
                    ProfileManager.instance.currentPlayer.normalTournamentLoses = 0;
                    for (int i = 0; i < 3; i++)
                    {
                        Chances_Used_Display[i].SetActive(false);
                    }
                    ProfileManager.instance.currentPlayer.first_try_R = true;
                    PhotonNetworkingManager.instance.JoinTable();
                    Refresh();
                   
                }
            }
        }


    }
    public void break_Matchmake()
    {
        Search_Panel.SetActive(false);
    }







    private void Refresh()
    {
        Rewards_Unlocker_SliderBar.value = 0.1f;
        #region Tournaments_Rewards_Instantiating
        if (TournamentSelection.Tournament_Name == "Regular Tournament")
        {
            Regular_Win_Rewards_Container.gameObject.SetActive(true);
            twoM_Win_Rewards_Container.gameObject.SetActive(false);
            twoOOK_Win_Rewards_Container.gameObject.SetActive(false);
            //Win_Rewards_Container.GetComponent<HorizontalLayoutGroup>().padding.left = 35;
            //Win_Rewards_Container.GetComponent<HorizontalLayoutGroup>().spacing = 26;
            if (Regular_Instantiate_chk == 0)
            {
                Regular_Instantiate_chk++;
                for (int i = 0; i < _Rewards.tournament_Rewards.Count; i++)
                {
                    var a = Instantiate(Win_Rewards, Regular_Win_Rewards_Container);
                    a.GetComponent<Tour_Rewards_Container>().reward = _Rewards.tournament_Rewards[i];
                }
            }

        }
        #endregion





        #region 1st_Free_Chance
        if (TournamentSelection.Tournament_Name == "Regular Tournament")
        {
            //if (Play_Continue_btn.transform.GetChild(0).gameObject.activeInHierarchy)
            if (ProfileManager.instance.currentPlayer.first_try_R)
            {
                Play_Continue_btn.SetActive(true);
                Play_Continue_btn.transform.GetChild(0).gameObject.SetActive(true);
                Play_Continue_btn.transform.GetChild(1).gameObject.SetActive(false);
                //SceneManager.LoadScene("GamePlay");
            }
            //else if (Play_Continue_btn.transform.GetChild(1).gameObject.activeInHierarchy)
            else
            {
                Play_Continue_btn.SetActive(true);
                Play_Continue_btn.transform.GetChild(0).gameObject.SetActive(false);
                Play_Continue_btn.transform.GetChild(1).gameObject.SetActive(true);
                Play_Continue_btn.transform.GetChild(1).GetChild(1).gameObject.GetComponent<Text>().text = ProfileManager.instance.currentPlayer.Gems_deduction_R.ToString();
                Play_Continue_btn.transform.GetChild(1).GetChild(1).gameObject.GetComponent<Kozykin.MultiLanguageItem>().strItems[0] = ProfileManager.instance.currentPlayer.Gems_deduction_R.ToString();
                Play_Continue_btn.transform.GetChild(1).GetChild(1).gameObject.GetComponent<Kozykin.MultiLanguageItem>().strItems[1] = ProfileManager.instance.currentPlayer.Gems_deduction_R.ToString();
                Play_Continue_btn.transform.GetChild(1).GetChild(1).gameObject.GetComponent<Kozykin.MultiLanguageItem>().text = ProfileManager.instance.currentPlayer.Gems_deduction_R.ToString();
            }
        }
        #endregion





        #region Chances_Implementation
        if (TournamentSelection.Tournament_Name == "Regular Tournament")
        {
            if (ProfileManager.instance.currentPlayer.normalTournamentLoses > 0)
            {
                for (int i = 0; i < ProfileManager.instance.currentPlayer.normalTournamentLoses; i++)
                {
                    Chances_Used_Display[i].SetActive(true);
                }
                if (ProfileManager.instance.currentPlayer.normalTournamentLoses % 3 == 0)
                {
                    Play_Continue_btn.SetActive(true);
                    Play_Continue_btn.transform.GetChild(0).gameObject.SetActive(false);
                    Play_Continue_btn.transform.GetChild(1).gameObject.SetActive(true);
                    Play_Continue_btn.transform.GetChild(1).GetChild(1).gameObject.GetComponent<Text>().text = ProfileManager.instance.currentPlayer.Gems_deduction_R.ToString();
                }
            }
        }


        #endregion




        #region Win_Match_And_Level_Display_And_Coins_Distribution_And_EXp_Handling
        if (TournamentSelection.Tournament_Name == "Regular Tournament")
        {
            int counter = 0;
            if (ProfileManager.instance.currentPlayer.normalTournamentWins > 10)
                counter = ProfileManager.instance.currentPlayer.normalTournamentWins % 10;
            else
                counter = ProfileManager.instance.currentPlayer.normalTournamentWins;
            for (int n = 0; n < Regular_Win_Rewards_Container.childCount; n++)
            {
                if (n < counter)
                {
                    Regular_Win_Rewards_Container.GetChild(n).gameObject.SetActive(true);
                    Rewards_Unlocker_SliderBar.value += 1;
                }
                else
                    Regular_Win_Rewards_Container.GetChild(n).gameObject.SetActive(false);
            }
            int a = 0;
            while (ProfileManager.instance.currentPlayer.tournamentXp > scriptable_TournamentXP.tournamentMedals[a].xpReq)
            {
                Debug.Log("Level up");
                a++;
            }
            float oo = ((float)ProfileManager.instance.currentPlayer.tournamentXp / (float)scriptable_TournamentXP.tournamentMedals[a].xpReq) * 10;
            Debug.Log(oo);
            LevelUp_Unlocker_SliderBar.value = oo;
            Win_Match_Display.text = ProfileManager.instance.currentPlayer.normalTournamentWins.ToString();
            Level_Display.text = (a + 1).ToString();


            for(int i=0;i<scriptable_TournamentXP.tournamentMedals.Count;i++)
            {
                if(a==i)
                {
                    if(i==0)
                    {
                        Current_Badge.enabled = false;
                        Next_Badge.sprite = scriptable_TournamentXP.tournamentMedals[i].tournamentMedal;
                    }
                    else
                    {
                        Debug.Log("Badge value is: " + i);
                        Current_Badge.enabled = true;
                        Current_Badge.sprite = scriptable_TournamentXP.tournamentMedals[i - 1].tournamentMedal;
                        Next_Badge.sprite = scriptable_TournamentXP.tournamentMedals[i].tournamentMedal;
                    }
                    
                }
            }
        }
        #endregion
    }



    // Update is called once per frame
    void Update()
    {
        Tournament_Name_Display.text = TournamentSelection.Tournament_Name;
    }
}
