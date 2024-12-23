using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LeaveRoom : MonoBehaviour
{
    void Start()
    {
        GetComponent<Button>().onClick.AddListener(OnClick);       
    }

 void OnClick()
    {
        if (PhotonNetwork.InRoom)
            PhotonNetwork.LeaveRoom();
    }
}
