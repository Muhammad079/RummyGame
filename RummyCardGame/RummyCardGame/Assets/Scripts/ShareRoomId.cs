using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShareRoomId : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        GetComponent<Button>().onClick.AddListener(ShareRoomID);
    }

  void ShareRoomID()
    {
        //new NativeShare().SetSubject("Let's play Rummy").SetText("Join my Private Table usin table ID: "+PhotonNetwork.CurrentRoom.Name).Share();
    }
}
