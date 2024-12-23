using ExitGames.Client.Photon;
using Photon.Pun;
using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class Chat_BOX : MonoBehaviour
{
    void Start()
    {
     GetComponent<Button>().onClick.AddListener(Msg_Sent);
    }
    void Msg_Sent()
    {
        Debug.Log("selected btn is: " + transform.name);
        string msg = transform.GetChild(0).GetComponent<TextMeshProUGUI>().text;
        //string msg = this.transform.GetChild(0).gameObject.GetComponent<Text>().text;

        //string msg = Input_text.GetComponent<Text>().text;
        
        object[] msg_text = new object[] { msg };
        RaiseEventOptions raiseEventOptions = new RaiseEventOptions { Receivers = ReceiverGroup.All };
        PhotonNetwork.RaiseEvent(MultiPlayerEventsCallBack.msg_from_Player, msg_text, raiseEventOptions, SendOptions.SendReliable);
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
