using DG.Tweening;
using Photon.Pun;
using UnityEngine;
using UnityEngine.UI;
public class TableSelection : MonoBehaviour
{
    [SerializeField] private bool forVip = false;
    [SerializeField] private int minTrophiesRq = 0;
    [SerializeField] private int bidAmount = 0;
    [SerializeField] private int replayGemCost = 0;
    [SerializeField] private GameObject waitingPanel = null, Lock = null, Not_VIP_Popup = null , Not_VIP_Lock = null, Search_Opponent_Panel=null;
    void OnEnable()
    {
        if (ProfileManager.instance.currentPlayer.trophies >= minTrophiesRq)
        {
            Lock.SetActive(false);
            GetComponent<Button>().enabled = true;
            GetComponent<Image>().color = Color.white;
            //   this.gameObject.SetActive(true);
        }
        else
        {
            Lock.SetActive(true);
            Lock.transform.GetChild(1).GetChild(0).GetComponent<Text>().text = "Unlock at " + minTrophiesRq;
            Lock.transform.GetChild(1).GetChild(0).GetComponent<Kozykin.MultiLanguageItem>().strItems[0] = "Unlock at " + minTrophiesRq;
            Lock.transform.GetChild(1).GetChild(0).GetComponent<Kozykin.MultiLanguageItem>().strItems[1] = "يفتح بعد جمع " + minTrophiesRq;
            Lock.transform.GetChild(1).GetChild(0).GetComponent<Kozykin.MultiLanguageItem>().text = "يفتح بعد جمع " + minTrophiesRq;

            GetComponent<Button>().enabled = false;
            GetComponent<Image>().color = Color.red;

            //  this.gameObject.SetActive(false);
        }
        if(Not_VIP_Lock != null)
        {
            if(ProfileManager.instance.currentPlayer.isVip)
            {
                Not_VIP_Lock.SetActive(false);
            }
            else
            {
                Not_VIP_Lock.SetActive(true);
                Not_VIP_Lock.transform.GetChild(0).GetChild(2).GetComponent<Button>().onClick.AddListener(()=>
                {
                    Not_VIP_Popup.SetActive(true);
                    Not_VIP_Popup.transform.DOScale(new Vector3(1, 1, 1), 0.5f).SetEase(Ease.OutSine);
                });
            }
        }
    }
    private void Start()
    {
        GetComponent<Button>().onClick.AddListener(OnClick);
        if(Not_VIP_Popup!=null)
        {
            Not_VIP_Popup.transform.GetChild(1).GetChild(1).GetComponent<Button>().onClick.AddListener(()=> {
                Not_VIP_Popup.transform.DOScale(new Vector3(0, 0, 0), 0.5f).SetEase(Ease.Linear).OnComplete(()=> {
                    Not_VIP_Popup.SetActive(false);
                });
            });
        }
    }
    void OnClick()
    {
        GameManager.instance.replayGemCost = replayGemCost;
        GameManager.instance.TableSelection(bidAmount);
        if (forVip)
        {
            if (ProfileManager.instance.currentPlayer.isVip)
                PlayOnTThisTable();
            else
            {
                Debug.Log("Not VIP");
                Not_VIP_Popup.SetActive(true);
                Not_VIP_Popup.transform.DOScale(new Vector3(1, 1, 1), 0.5f).SetEase(Ease.OutSine);
            }
        }
        else
        {
            PlayOnTThisTable();
        }
    }
    private void PlayOnTThisTable()
    {
        if (ProfileManager.instance.currentPlayer.coins >= bidAmount)
        {
            PhotonNetworkingManager.instance.JoinTable(GamePlayStart);
            Search_Opponent_Panel.SetActive(true);
            //waitingPanel.SetActive(true);
        }
        else
        {
            GameManager.instance.Not_Enough_C_Panel.SetActive(true);
            GameManager.instance.Not_Enough_C_Panel.transform.DOScale(new Vector3(1, 1, 1), 0.5f).SetEase(Ease.OutBounce);
        }
    }
    void GamePlayStart()
    {
        PhotonNetwork.LoadLevel("GamePlay");
    }
}