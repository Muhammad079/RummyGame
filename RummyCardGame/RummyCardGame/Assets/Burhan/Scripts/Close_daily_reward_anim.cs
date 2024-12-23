using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Close_daily_reward_anim : MonoBehaviour
{
    public GameObject Daily_Reward_UI, Coins_Container;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void OnCloseBTNclick()
    {
        Daily_Reward_UI.transform.DOScale(new Vector3(0, 0, 0), 0.2f).SetEase(Ease.OutQuad).OnComplete(fader_Inactive);
    }
    public void fader_Inactive()
    {
        Coins_Container.GetComponent<Canvas>().overrideSorting = false;
        Coins_Container.GetComponent<Canvas>().sortingOrder = 0;
        Daily_Reward_UI.SetActive(false);
    }
}
