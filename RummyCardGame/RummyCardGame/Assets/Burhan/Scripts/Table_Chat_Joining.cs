using Photon.Pun;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Table_Chat_Joining : MonoBehaviour
{
    public Button join_btn;
    public Text Coins;
    public GameObject[] Players_Display;
    
    
    public Invited_Tables_Data data = new Invited_Tables_Data();



    bool updated;
    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < Players_Display.Length; i++)
        {
            Players_Display[i].SetActive(false);
        }
        updated = false;
        join_btn.onClick.AddListener(Joining_table);
    }

    private void Joining_table()
    {
        PhotonNetwork.JoinRoom(data.Room_ID);
    }

    // Update is called once per frame
    void Update()
    {
        if(!updated)
        {
            updated = true;
            Coins.text = data.Bid.ToString();
            for (int i = 0; i < data.Max_Players; i++)
            {
                Players_Display[i].SetActive(true);
            }
        }
    }
}
