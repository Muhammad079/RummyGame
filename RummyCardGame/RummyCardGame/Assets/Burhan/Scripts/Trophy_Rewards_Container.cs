using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Trophy_Rewards_Container : MonoBehaviour
{
    public Trophy_Rewards_popup_Handler trophy_Rewards_Popup_Handler;

    public RewardItems Reward;
    public Sprite Reward_Image;
    public string Reward_Trophies;
    public int id;
    public GameObject Lock;

    // Start is called before the first frame update
    void Start()
    {
        transform.GetChild(0).GetChild(0).GetComponent<Image>().sprite = Reward_Image;
        transform.GetChild(1).GetChild(0).GetComponent<Text>().text = Reward_Trophies;

        GetComponent<Button>().onClick.AddListener(delegate { trophy_Rewards_Popup_Handler.Getting_Setting_Data(Reward, Reward_Image, Reward_Trophies, id); });

        if (int.Parse(Reward_Trophies)>ProfileManager.instance.currentPlayer.trophies)
        {
            Lock.SetActive(true);
        }
        else
        {
            Lock.SetActive(false);
        }
        if(id==0)
        {
            Invoke(nameof(refresh), 0.2f);
        }
    }

    private void refresh()
    {
        transform.GetChild(0).GetChild(0).GetComponent<Image>().sprite = Reward_Image;
        transform.GetChild(1).GetChild(0).GetComponent<Text>().text = Reward_Trophies;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
