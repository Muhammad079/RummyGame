using Photon.Pun;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using DG.Tweening;

public class SceneLoader : MonoBehaviour
{
    public GameObject Loading_Screen;
    public Text Loading_animation;
    [SerializeField] public string scenToLoad = "";
    AsyncOperation operation;
    void Start()
    {
        GetComponent<Button>().onClick.AddListener(()=> {
            if (!SceneManager.GetActiveScene().name.Contains("Login"))
            {
                StartCoroutine(OnClick());
            }
            else
            {
                SceneManager.LoadScene(scenToLoad);
            }
            
        });
    }
    public IEnumerator OnClick()
    {
        if (PhotonNetwork.InRoom)
        {
            PhotonNetwork.LeaveRoom();

            if (GameManager.instance.selectedTournament == TournamentType.twoMEvents)
            {
                ProfileManager.instance.currentPlayer.TwoM_Loses++;
            }
            if (ProfileManager.instance.currentPlayer.TwoHK_Selected)
            {
                ProfileManager.instance.currentPlayer.TwoHK_LostTable_ID.Add(ProfileManager.instance.currentPlayer.TwoHK_Selected_TableID);
                ProfileManager.instance.currentPlayer.TwoHK_Selected_TableID = 0;
                //ProfileManager.instance.SaveUserData();
                ProfileManager.instance.currentPlayer.TwoHK_Loses++;
                //ProfileManager.instance.currentPlayer.TwoHK_Lost = true;
            }
            if (GameManager.instance.selectedTournament == TournamentType.twoMEvents)
            {
                GameManager.instance.selectedTournament = TournamentType.empty;
            }
            if(GameManager.instance.selectedTournament == TournamentType.normalEvents)
            {
                GameManager.instance.selectedTournament = TournamentType.empty;
                ProfileManager.instance.currentPlayer.normalTournamentLoses++;
            }


            ProfileManager.instance.currentPlayer.lossToday++;
            ProfileManager.instance.currentPlayer.consectiveWins=0;
            ProfileManager.instance.currentPlayer.totalLoss++;
            if (ProfileManager.instance.currentPlayer.trophies < 2)
                ProfileManager.instance.currentPlayer.trophies = 2;
            ProfileManager.instance.SaveUserData();
        }
        
        Loading_Screen.SetActive(true);
        operation = SceneManager.LoadSceneAsync(scenToLoad);
        yield return null;


    }
    public virtual void Update()
    {
        if (operation != null)
        {
            float progressValue = Mathf.Clamp01(operation.progress / 0.9f);

            if (Manager.instance.m_CurrentLanguage == 0)
            {
                Loading_animation.text = "LOADING: " + Mathf.Round(progressValue * 100) + "%";
            }
            else if (Manager.instance.m_CurrentLanguage == 1)
            {
                Loading_animation.GetComponent<Kozykin.MultiLanguageItem>().strItems[0] = "LOADING: " + Mathf.Round(progressValue * 100) + "%";
                Loading_animation.GetComponent<Kozykin.MultiLanguageItem>().strItems[1] = "جار التحميل: " + Mathf.Round(progressValue * 100) + "%";
                Loading_animation.GetComponent<Kozykin.MultiLanguageItem>().text = "جار التحميل: " + Mathf.Round(progressValue * 100) + "%";
            }


            



        }
    }
}
