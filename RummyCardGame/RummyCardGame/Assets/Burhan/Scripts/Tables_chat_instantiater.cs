using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Tables_chat_instantiater : MonoBehaviour
{
    public GameObject Table_prefab;
    public Slider Players;
    float players_count;

    private void onValueChanged_func(int value)
    {
        for (int i = 0; i < value; i++)
        {
            var Table = Instantiate(Table_prefab, transform);
            Table.GetComponent<Table_Chat_Joining>().data = ProfileManager.instance.currentPlayer.Tables_VIP_invited[i];
        }
    }



    // Start is called before the first frame update
    void Start()
    {
        players_count = 0;
        ProfileManager.instance.currentPlayer.Tables_VIP_invited.Clear();
        Players.onValueChanged.AddListener(delegate { onValueChanged_func(ProfileManager.instance.currentPlayer.Tables_VIP_invited.Count); });
    }

    // Update is called once per frame
    void Update()
    {
        players_count = ProfileManager.instance.currentPlayer.Tables_VIP_invited.Count;
        if (players_count > 0)
        {
            players_count = players_count / 100;
            
        }
        Players.value = players_count;
    }
}
