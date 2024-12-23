using Photon.Pun;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Table_Invitation : MonoBehaviour
{
    public Button Back_btn, Friends_btn;

    //bool Remove_broadcast;
    string Room_ID;
    // Start is called before the first frame update
    void Start()
    {
        

        GetComponent<Button>().onClick.AddListener(Invitation_Sent);
    }

    private void Invitation_Sent()
    {
        Room_ID = PhotonNetwork.CurrentRoom.Name;
        if (Friends_Table_invite.friend_invite_selected)
        {
            string[] message = new string[] { Room_ID, PhotonNetwork.CurrentRoom.PlayerCount.ToString(), PhotonNetwork.CurrentRoom.MaxPlayers.ToString(), GameManager.instance.selectedBid.ToString() };
            //Max 4 parameters
            ChatManager.instance.PrivateTableSend(transform.parent.GetChild(0).GetComponent<TextMeshProUGUI>().text.ToString(),message);
        }
        else
        {
            string[] message = new string[] { Room_ID, PhotonNetwork.CurrentRoom.PlayerCount.ToString(), PhotonNetwork.CurrentRoom.MaxPlayers.ToString(), GameManager.instance.selectedBid.ToString() };
            //Max 4 parameters
            ChatManager.instance.PublicTableInvite(message);
            StartCoroutine(waiting());
        }

        
    }
    IEnumerator waiting()
    {
        Back_btn.interactable = false;
        Friends_btn.interactable = false;
        GetComponent<Button>().interactable = false;

        transform.GetChild(0).GetComponent<Text>().text = "waiting (10)";
        yield return new WaitForSeconds(1);
        transform.GetChild(0).GetComponent<Text>().text = "waiting (9)";
        yield return new WaitForSeconds(1);
        transform.GetChild(0).GetComponent<Text>().text = "waiting (8)";
        yield return new WaitForSeconds(1);
        transform.GetChild(0).GetComponent<Text>().text = "waiting (7)";
        yield return new WaitForSeconds(1);
        transform.GetChild(0).GetComponent<Text>().text = "waiting (6)";
        yield return new WaitForSeconds(1);
        transform.GetChild(0).GetComponent<Text>().text = "waiting (5)";
        yield return new WaitForSeconds(1);
        transform.GetChild(0).GetComponent<Text>().text = "waiting (4)";
        yield return new WaitForSeconds(1);
        transform.GetChild(0).GetComponent<Text>().text = "waiting (3)";
        yield return new WaitForSeconds(1);
        transform.GetChild(0).GetComponent<Text>().text = "waiting (2)";
        yield return new WaitForSeconds(1);
        transform.GetChild(0).GetComponent<Text>().text = "waiting (1)";
        yield return new WaitForSeconds(1);
        transform.GetChild(0).GetComponent<Text>().text = "World";

        GetComponent<Button>().interactable = true;
        Back_btn.interactable = true;
        Friends_btn.interactable = true ;
    }
    // Update is called once per frame
    void Update()
    {
        //if(PhotonNetwork.CurrentRoom.Players.Count == PhotonNetwork.CurrentRoom.MaxPlayers
        //    && !Remove_broadcast)
        //{
        //    Remove_broadcast = true;
        //    ChatManager.instance.PublicMessegeSend(Room_ID);
        //}
    }
}

[Serializable]
public class Invited_Tables_Data
{
    public string Room_ID = "";
    public int Players_available = -1;
    public int Max_Players = -2;
    public int Bid = -3;
}