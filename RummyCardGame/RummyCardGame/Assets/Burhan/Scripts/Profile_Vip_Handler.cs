using ExitGames.Client.Photon;
using Photon.Pun;
using Photon.Realtime;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Profile_Vip_Handler : MonoBehaviour
{
    [SerializeField] GameObject Vip_Icon;
    [SerializeField] Sprite[] Vip_Images;
    int vip;

    private void Update()
    {
        if (ProfileManager.instance.currentPlayer.isVip/* && ProfileManager.instance.currentPlayer.VIP_30days_count > 0*/)
        {
            Vip_Icon.GetComponent<Image>().sprite = GameManager.instance.VIP_Levels[ProfileManager.instance.currentPlayer.VIP_Level - 1];
            //Vip_Icon.GetComponent<Image>().sprite = Vip_Images[1];
            Vip_Icon.SetActive(true);
        }
        //else if (ProfileManager.instance.currentPlayer.isVip && ProfileManager.instance.currentPlayer.VIP_7days_count > 0)
        //{
        //    Vip_Icon.GetComponent<Image>().sprite = Vip_Images[0];
        //    Vip_Icon.SetActive(true);
        //}
        else
        {
            Vip_Icon.SetActive(false);
        }
    }
    private void Start()
    {
         if (ProfileManager.instance.currentPlayer.isVip /*&& ProfileManager.instance.currentPlayer.VIP_30days_count > 0*/)
        {
            vip = ProfileManager.instance.currentPlayer.VIP_Level;
            //vip = 2;
        }
        //else if (ProfileManager.instance.currentPlayer.isVip && ProfileManager.instance.currentPlayer.VIP_7days_count > 0)
        //{
        //    vip = 1;
        //}
        else
        {
            vip = 0;
        }
        object[] vip_enabled = new object[] { vip };
        RaiseEventOptions raiseEventOptions = new RaiseEventOptions { Receivers = ReceiverGroup.All };
        PhotonNetwork.RaiseEvent(MultiPlayerEventsCallBack.Vip_Notfier, vip_enabled, raiseEventOptions, SendOptions.SendReliable);
        Debug.Log("VIP value is: " + vip_enabled.GetValue(0).ToString());
    }
}
