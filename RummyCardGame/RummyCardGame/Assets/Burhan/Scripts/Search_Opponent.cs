using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Realtime;
using UnityEngine.UI;
using Firebase.Database;
using System.Threading.Tasks;
using Photon.Pun;

public class Search_Opponent : MonoBehaviour
{
    public static Search_Opponent instance;
    public Text Message_Title;
    public GameObject[] Opponents;
    
    
    public List<string> Found_Players = new List<string>();
    public List<bool> Opponents_check = new List<bool>();


    public List<PlayerProfile> Opponents_data = new List<PlayerProfile>();
    public List<bool> data_applied_check = new List<bool>();

    public List<PlayerProfile> User = new List<PlayerProfile>();
    public List<bool> joinSide_Bool = new List<bool>();


    private void OnDisable()
    {
        Found_Players.Clear();
        Opponents_check.Clear();
        Opponents_data.Clear();
        data_applied_check.Clear();
        User.Clear();
        joinSide_Bool.Clear();
    }
    // Start is called before the first frame update
    void Start()
    {
        //joinSide_Bool = false;
        instance = this;

        for(int i=1;i<Opponents.Length;i++)
        {
            if(GameManager.instance.selectedTable.totalPlayers > i)
            {
                Opponents[i].SetActive(true);
            }
            else
            {
                Opponents[i].SetActive(false);
            }
        }


        Opponents[0].transform.GetChild(0).GetChild(0).GetComponent<Image>().sprite = ProfileManager.instance.currentPlayer.profilePicture;
        Opponents[0].transform.GetChild(0).GetChild(1).GetChild(0).GetComponent<Text>().text = ProfileManager.instance.currentPlayer.trophies.ToString();
        Opponents[0].transform.GetChild(0).GetChild(2).GetChild(0).GetComponent<Text>().text = ProfileManager.instance.currentPlayer.name;
    }

    public void Bot_Entered()
    {
        for(int i=1; i<Opponents.Length; i++)
        {
            if(Opponents[i].transform.GetChild(0).GetChild(1).GetChild(0).GetComponent<Text>().text.Contains("0"))
            {
                Opponents[i].transform.GetChild(0).GetChild(0).GetComponent<Image>().sprite = ProfileManager.instance.avatarDataFile.avatars[0].avatarImage;
                Opponents[i].transform.GetChild(0).GetChild(1).GetChild(0).GetComponent<Text>().text = "0";

                if(i==1)
                {
                    Opponents[i].transform.GetChild(0).GetChild(2).GetChild(0).GetComponent<Text>().text = "David";
                }
                else if (i == 2)
                {
                    Opponents[i].transform.GetChild(0).GetChild(2).GetChild(0).GetComponent<Text>().text = "John";
                }
                if (i == 3)
                {
                    Opponents[i].transform.GetChild(0).GetChild(2).GetChild(0).GetComponent<Text>().text = "Peter";
                }
            }
        }
    }
    public void On_joining_Side_Entry()
    {
        Bot_Entered();

        Debug.Log("Search opponent joining started");
        for (int i = 1; i <= PhotonNetwork.CurrentRoom.PlayerCount; i++)
        {
            Debug.Log("Search opponent Player Count: " + PhotonNetwork.CurrentRoom.PlayerCount);
            Debug.Log("Search opponent Player: " + i);
            if (PhotonNetwork.CurrentRoom.Players[i].ActorNumber != PhotonNetwork.LocalPlayer.ActorNumber)
            {
                Debug.Log("Getting data");
                Debug.Log("Getting data_PLAYERSID" + PhotonNetwork.CurrentRoom.Players[i].CustomProperties["ID"].ToString());
                FirebaseDatabase.DefaultInstance.GetReference("Players").Child(PhotonNetwork.CurrentRoom.Players[i].CustomProperties["ID"].ToString()).GetValueAsync().ContinueWith(task => {
                    if (task.IsFaulted)
                    {
                        Debug.Log(task.Result.ToString());
                        return;
                    }
                    if (task.IsCompleted)
                    {

                        User.Add(JsonUtility.FromJson<PlayerProfile>(task.Result.GetRawJsonValue()));
                        joinSide_Bool.Add(true);

                    }
                });




            }
        }

    }
    // Update is called once per frame
    void Update()
    {
        for (int i = 0; i < User.Count; i++)
        {
            if (User[i] != null)
            {
                if (joinSide_Bool[i])
                {
                    joinSide_Bool[i] = false;
                    {
                        Opponents[i + 1].transform.GetChild(0).GetChild(0).GetComponent<Image>().sprite = User[0].profilePicture;
                        Opponents[i + 1].transform.GetChild(0).GetChild(1).GetChild(0).GetComponent<Text>().text = User[0].trophies.ToString();
                        Opponents[i + 1].transform.GetChild(0).GetChild(2).GetChild(0).GetComponent<Text>().text = User[0].name;
                    }
                }
            }
        }




        #region Fetching Data
        if(Found_Players.Count == 0)
        {
            return;
        }
        else
        {
            for(int i=0;i< Found_Players.Count;i++)
            {
                if (Found_Players[i] != null)
                {
                    if (Opponents_check[i].Equals(true))
                    {
                        Opponents_check[i] = false;

                        FirebaseDatabase.DefaultInstance.GetReference("Players").Child(Found_Players[i]).GetValueAsync().ContinueWith(task => {
                            if (task.IsFaulted)
                            {
                                Debug.Log(task.Result.ToString());
                                return;
                            }
                            if (task.IsCompleted)
                            {
                                Opponents_data.Add(JsonUtility.FromJson<PlayerProfile>(task.Result.GetRawJsonValue()));
                                data_applied_check.Add(true);
                            }
                        });
                    }
                }
            }
        }

        #endregion

        #region Applying Data

        for (int i = 0; i < Opponents_data.Count; i++)
        {
            if (Opponents_data[i] != null)
            {
                if (data_applied_check[i])
                {
                    data_applied_check[i] = false;
                    {
                        Opponents[i+1].transform.GetChild(0).GetChild(0).GetComponent<Image>().sprite = Opponents_data[0].profilePicture;
                        Opponents[i+1].transform.GetChild(0).GetChild(1).GetChild(0).GetComponent<Text>().text = Opponents_data[0].trophies.ToString();
                        Opponents[i+1].transform.GetChild(0).GetChild(2).GetChild(0).GetComponent<Text>().text = Opponents_data[0].name;
                    }
                }
            }
        }

        #endregion

    }

}
