using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class Re_EnterMatch : MonoBehaviour
{
    [SerializeField] private CardManager cardManager = null;
    [SerializeField] private LostPanel lostPanel = null;
     void Start()
    {
        GetComponent<Button>().onClick.AddListener(OnClick);
    }
   
    
    void OnClick()
    {
        Debug.Log("Clicked");
        if (ProfileManager.instance.currentPlayer.gems >= GameManager.instance.replayGemCost)
        {
            Debug.Log("Renetering");
            ProfileManager.instance.currentPlayer.gems -= GameManager.instance.replayGemCost;
            lostPanel.PanelHide();
            cardManager.score = 0;
            cardManager.scorecount = 0;
        }
    }
}
