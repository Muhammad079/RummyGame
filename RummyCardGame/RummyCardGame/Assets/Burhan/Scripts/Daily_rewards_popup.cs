using DG.Tweening;
using System;
using UnityEngine;

public class Daily_rewards_popup : MonoBehaviour
{
    public GameObject Daily_rewards_UI, Coins_Container;
    float secs = 0;
    
    void DisplayPanel()
    {
        Coins_Container.GetComponent<Canvas>().overrideSorting = true;
        Coins_Container.GetComponent<Canvas>().sortingOrder = 1;

        Daily_rewards_UI.transform.DOScale(new Vector3(1, 1, 1), 0.2f).SetEase(Ease.OutQuad);//.OnComplete(fader_Inactive);
        //Invoke(nameof(Hide),5f);  //chatPanel.DOLocalMoveY(0, 1);
    }
    // Start is called before the first frame update
    void Start()
    {
        if ((int)DateTime.UtcNow.DayOfWeek > ProfileManager.instance.currentPlayer.lastDailyRewardCollected)
            DisplayPanel();
    }

    void Hide()
    {
        this.gameObject.SetActive(false);
    }
}
