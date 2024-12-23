using DG.Tweening;
using ExitGames.Client.Photon;
using Photon.Pun;
using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PhotonNetworkingManager : MonoBehaviourPunCallbacks, IConnectionCallbacks, IMatchmakingCallbacks
{
    bool loadlevel_Check;
    public static PhotonNetworkingManager instance;
    public ExitGames.Client.Photon.Hashtable bid = new ExitGames.Client.Photon.Hashtable();

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(this.gameObject);
    }
    void Start()
    {
        DontDestroyOnLoad(this.gameObject);
        Debug.Log("Making a COnnection");
        DatabaseFunctions.loginComplete += Connect;
        PhotonNetwork.AutomaticallySyncScene = true;
    }
    public void Connect()
    {
        PhotonNetwork.ConnectUsingSettings();
    }
    public override void OnConnectedToMaster()
    {
        

        Debug.Log("OnConnectedToMaster() was called by PUN.");
        PhotonNetwork.LocalPlayer.NickName = ProfileManager.instance.currentPlayer.name;



        ExitGames.Client.Photon.Hashtable Player_ID = new ExitGames.Client.Photon.Hashtable();
        Player_ID.Add("ID", ProfileManager.instance.currentPlayer.id);

        PhotonNetwork.LocalPlayer.SetCustomProperties(Player_ID);
        Debug.Log("myUser ID modified: " + PhotonNetwork.LocalPlayer.CustomProperties["ID"]);
        Debug.Log(PhotonNetwork.LocalPlayer.NickName);
        Debug.Log(PhotonNetwork.CloudRegion);


        
    }
    public void JoinTable(System.Action roomFullCallback = null)
    {
        if (!PhotonNetwork.InRoom)
        {
            //ExitGames.Client.Photon.Hashtable bid = new ExitGames.Client.Photon.Hashtable() { { "Bid", GameManager.instance.selectedBid } };
            if (bid.ContainsKey("Tournament"))
            {
                if (!bid.ContainsKey("Bid"))
                {
                    bid.Add("Bid", 10000);
                }

                Debug.Log(bid["Bid"]);
                MainMenuStats.instance.ShowMessege("Joining Tournament room");
                PhotonNetwork.JoinRandomRoom(bid, 2);
            }
            else
            {
                if (!bid.ContainsKey("Bid"))
                {
                    bid.Add("Bid", GameManager.instance.selectedBid);
                }

                Debug.Log(bid["Bid"]);
                MainMenuStats.instance.ShowMessege("Joining random room");

                PhotonNetwork.JoinRandomRoom(bid, (byte)GameManager.instance.selectedTable.totalPlayers);
                //PhotonNetwork.JoinRandomRoom(bid, 2);
            }

            //   PhotonNetwork.JoinRandomRoom(bid, 2);
        }
    }
    
    public override void OnCreatedRoom()
    {
        if(MainMenuStats.instance!=null)
        {
            MainMenuStats.instance.ShowMessege("Room Created /n Room no: " + PhotonNetwork.CurrentRoom.Name);
        }
        
        Debug.Log("Room Created");
        if(PhotonNetwork.CurrentRoom.Name.Contains("roomvip"))
        {
            //InGamePlayer Mplayer = new InGamePlayer();
            //Mplayer.tablePos = /*index*/0;
            //Mplayer.actorNo = PhotonNetwork.CurrentRoom.GetPlayer(/*index*/0 + 1).ActorNumber;
            //Mplayer.isBot = false;
            //Gameplay_Manager.instance.totalPlayers.Add(Mplayer);
            StartCoroutine(nameof(LoadLevel));
        }
        else
        {
            StartCoroutine(nameof(AllowBots));
        }
        

    }
    IEnumerator AllowBots()
    {
        
        if(!PhotonNetwork.CurrentRoom.Name.Contains("roomvip"))
        {
            if(!PhotonNetwork.CurrentRoom.Name.Contains("tutorial"))
            {
                yield return new WaitForSeconds(15);
                Search_Opponent.instance.Bot_Entered();
            }
            
        }
        
        StartCoroutine(nameof(LoadLevel));
    }
    //void AddBotPlayer()
    //{
    //    if (PhotonNetwork.CurrentRoom.PlayerCount < PhotonNetwork.CurrentRoom.MaxPlayers)
    //    {
    //        Player p = new Player ("BotPlayer", PhotonNetwork.CurrentRoom.PlayerCount + 1, false);
    //        PhotonNetwork.CurrentRoom.AddPlayer(p);
    //        AddBotPlayer();
    //    }
    //    else StopCoroutine(nameof(AllowBots));
    //}
    public override void OnCreateRoomFailed(short returnCode, string message)
    {
        Debug.Log("Room creation failed");
    }
    public override void OnJoinedRoom()
    {
        if(PhotonNetwork.CurrentRoom.Name.Contains("tutorial"))
        {
            Gameplay_Manager.instance.StartGame();
        }


        if(!PhotonNetwork.LocalPlayer.IsMasterClient)
        {
            if (PhotonNetwork.CurrentRoom.Name.Contains("Room"))
            {
                Search_Opponent.instance.On_joining_Side_Entry();
            }
        }
        

        loadlevel_Check = true;
        if (!PhotonNetwork.CurrentRoom.CustomProperties.ContainsKey("Tournament"))
        {
            if(PhotonNetwork.CurrentRoom.Name.Contains("roomvip"))
            {
                if ((int)PhotonNetwork.CurrentRoom.CustomProperties["Bid"] > ProfileManager.instance.currentPlayer.coins)
                {
                    loadlevel_Check = false;
                    PhotonNetwork.LeaveRoom();
                    Debug.Log("returning");
                    VIP_Table_Handler.no_coins = true;
                    GameManager.instance.Not_Enough_C_Panel.SetActive(true);
                    GameManager.instance.Not_Enough_C_Panel.transform.DOScale(new Vector3(1, 1, 1), 0.5f).SetEase(Ease.OutBounce).OnComplete(() =>
                    {
                        return;
                    });
                }
            }
            
        }

        VIP_Table_Handler.no_coins = false;
        Debug.Log("Room Joined: " + PhotonNetwork.CurrentRoom.Name);

        if(PhotonNetwork.CurrentRoom.Name.Contains("roomvip"))
        {

            GameManager.instance.selectedBid = (int)PhotonNetwork.CurrentRoom.CustomProperties["Bid"];
            if (PhotonNetwork.CurrentRoom.MaxPlayers < 3)
            {
                GameManager.instance.selectedXPData = GameManager.instance.xpData_2Players;
            }
            else
            {
                GameManager.instance.selectedXPData = GameManager.instance.xpData;
            }



            if (!PhotonNetwork.LocalPlayer.IsMasterClient)
            {
                for (int i = 1; i < PhotonNetwork.CurrentRoom.Players.Count; i++)
                {
                    var Old_Pal= Instantiate(Gameplay_Manager.instance.playerDisplay, Gameplay_Manager.instance.otherPlayerPositions[i]);
                    InGamePlayer player = new InGamePlayer();
                    Old_Pal.GetComponent<PlayerStatus>().nameText.text = PhotonNetwork.CurrentRoom.GetPlayer(i).CustomProperties["ID"].ToString();
                    player.tablePos = i;
                    player.actorNo = PhotonNetwork.CurrentRoom.GetPlayer(i).ActorNumber;
                    player.isBot = false;
                    Gameplay_Manager.instance.totalPlayers.Add(player);
                }
                InGamePlayer Mplayer = new InGamePlayer();
                Mplayer.tablePos = 0;
                Mplayer.actorNo = PhotonNetwork.CurrentRoom.GetPlayer(PhotonNetwork.CurrentRoom.Players.Count).ActorNumber;
                Mplayer.isBot = false;
                Gameplay_Manager.instance.totalPlayers.Add(Mplayer); //Dont fix null reference here if any


                if (PhotonNetwork.CurrentRoom.PlayerCount == PhotonNetwork.CurrentRoom.MaxPlayers)
                {
                    PhotonNetwork.CurrentRoom.IsOpen = false;
                    PhotonNetwork.CurrentRoom.IsVisible = false;
                    //Gameplay_Manager.instance.StartGame();

                    


                    Gameplay_Manager.instance.VIP_Invite_Panel.SetActive(true);
                    Gameplay_Manager.instance.VIP_Invite_Panel.transform.GetChild(0).gameObject.SetActive(false);
                    Gameplay_Manager.instance.VIP_Invite_Panel.transform.GetChild(3).gameObject.SetActive(true);

                    string Mod_RoomName = "";
                    for (int f = 0; f < PhotonNetwork.CurrentRoom.Name.Length; f++)
                    {
                        if(char.IsDigit(PhotonNetwork.CurrentRoom.Name[f]))
                        {
                            Mod_RoomName += PhotonNetwork.CurrentRoom.Name[f];
                        }
                    }

                    Gameplay_Manager.instance.VIP_Invite_Panel.transform.GetChild(3).GetChild(0).GetChild(0).GetComponent<Text>().text = Mod_RoomName; //PhotonNetwork.CurrentRoom.Name;
                    Gameplay_Manager.instance.VIP_Invite_Panel.transform.GetChild(3).GetChild(1).GetComponent<Button>().onClick.AddListener(() => {
                        //Mplayer.VIP_isReady = true;
                        Gameplay_Manager.instance.totalPlayers[Gameplay_Manager.instance.totalPlayers.Count - 1].VIP_isReady = true;
                        Debug.Log("Index of Current player: "+ Gameplay_Manager.instance.totalPlayers[Gameplay_Manager.instance.totalPlayers.Count - 1].VIP_isReady);
                        //Gameplay_Manager.instance.totalPlayers[Gameplay_Manager.instance.totalPlayers.IndexOf(Mplayer)].VIP_isReady = true;
                        object[] ready = new object[] { Gameplay_Manager.instance.totalPlayers[Gameplay_Manager.instance.totalPlayers.Count - 1].VIP_isReady }; // Array contains the target position and the IDs of the selected units
                        RaiseEventOptions raiseEvent = new RaiseEventOptions { Receivers = ReceiverGroup.Others }; // You would have to set the Receivers to All in order to receive this event on the local client as well
                        PhotonNetwork.RaiseEvent(MultiPlayerEventsCallBack.VIP_isReady, ready, raiseEvent, SendOptions.SendReliable);
                        Gameplay_Manager.instance.VIP_Invite_Panel.SetActive(false);
                    });
                }
                
            }
            


            



        }
        else
        {
            if (PhotonNetwork.CurrentRoom.PlayerCount == PhotonNetwork.CurrentRoom.MaxPlayers)
            {
                if (loadlevel_Check)
                {
                    //  PhotonNetwork.LoadLevel("GamePlay");
                    StartCoroutine(nameof(LoadLevel));
                    Debug.Log("OnJOinedRoom : Loading Level");
                }
            }
            else
            {
                Debug.Log("Current bid: " + PhotonNetwork.CurrentRoom.CustomProperties["Bid"]);
                Debug.Log(PhotonNetwork.CurrentRoom.MaxPlayers);
                Debug.Log(PhotonNetwork.CurrentRoom.PlayerCount);
                int waitingPlayers = PhotonNetwork.CurrentRoom.MaxPlayers - PhotonNetwork.CurrentRoom.PlayerCount;
                Debug.Log("wait for " + waitingPlayers + " players to join");

                if(MainMenuStats.instance!=null)
                {
                    MainMenuStats.instance.ShowMessege("waiting for " + waitingPlayers + " players to join in room " + PhotonNetwork.CurrentRoom.Name);
                }
                

            }
        }
       
    }
    IEnumerator LoadLevel()
    {
        if (!PhotonNetwork.CurrentRoom.Name.Contains("tutorial"))
        {
            yield return new WaitForSeconds(2);
        }
        PhotonNetwork.LoadLevel("GamePlay");


        if (PhotonNetwork.CurrentRoom.Name.Contains("roomvip"))
        {
            yield return new WaitForSeconds(4);
            InGamePlayer Mplayer = new InGamePlayer();
            Mplayer.tablePos = /*index*/0;
            Mplayer.actorNo = PhotonNetwork.CurrentRoom.GetPlayer(/*index*/0 + 1).ActorNumber;
            Mplayer.isBot = false;
            Gameplay_Manager.instance.totalPlayers.Add(Mplayer);
           
        }


    }
    IEnumerator Waiting()
    {
        yield return new WaitForSeconds(5);
        Debug.Log("Players in room: " + PhotonNetwork.CurrentRoom.PlayerCount);
        if (PhotonNetwork.CurrentRoom.PlayerCount == PhotonNetwork.CurrentRoom.MaxPlayers)
        {
            if(PhotonNetwork.CurrentRoom.Name.Contains("roomvip"))
            {
                PhotonNetwork.CurrentRoom.IsOpen = false;
                PhotonNetwork.CurrentRoom.IsVisible = false;
                //Gameplay_Manager.instance.VIP_Invite_Panel.transform.GetChild(0).GetChild(4).GetComponent<Text>().text = "start";
                //Gameplay_Manager.instance.VIP_Invite_Panel.transform.GetChild(0).GetChild(4).GetComponent<Button>().interactable = true;
                //Gameplay_Manager.instance.VIP_Invite_Panel.transform.GetChild(0).GetChild(4).GetComponent<Button>().onClick.AddListener(() => {
                //    Gameplay_Manager.instance.StartGame();
                //});



                //Gameplay_Manager.instance.StartGame();
            }
            else
            {
                if (loadlevel_Check)
                {
                    PhotonNetwork.LoadLevel("GamePlay");
                    Debug.Log("WAiting : Loading Level");
                }
            }
            
        }
        else
        {
            if (PhotonNetwork.CurrentRoom.Name.Contains("roomvip"))
            {
                //Gameplay_Manager.instance.VIP_Invite_Panel.transform.GetChild(0).GetChild(4).GetComponent<Button>().interactable = false;
                //Gameplay_Manager.instance.VIP_Invite_Panel.transform.GetChild(0).GetChild(4).GetComponent<Button>()
            }
            else
            {
                Debug.Log("Current bid: " + PhotonNetwork.CurrentRoom.CustomProperties["Bid"]);
                Debug.Log(PhotonNetwork.CurrentRoom.MaxPlayers);
                Debug.Log(PhotonNetwork.CurrentRoom.PlayerCount);
                int waitingPlayers = PhotonNetwork.CurrentRoom.MaxPlayers - PhotonNetwork.CurrentRoom.PlayerCount;
                Debug.Log("wait for " + waitingPlayers + " players to join");
                MainMenuStats.instance.ShowMessege("wait for " + waitingPlayers + " players to join in room " + PhotonNetwork.CurrentRoom.Name);
            }
                
        }
    }
    public override void OnJoinRoomFailed(short returnCode, string message)
    {
        MainMenuStats.instance.ShowMessege("Room join failed");
        JoinTable();
    }
    public override void OnJoinRandomFailed(short returnCode, string message)
    {
        if (bid.ContainsKey("Tournament"))
        {
            MainMenuStats.instance.ShowMessege("Creating Tournament Room");
            string roomName = "Room" + Random.Range(0, 1000);
            RoomOptions roomOptions = new RoomOptions();
            roomOptions.PlayerTtl = 20000;
            //roomOptions.EmptyRoomTtl = 30000;

            roomOptions.IsOpen = true;
            roomOptions.IsVisible = true;
            roomOptions.MaxPlayers = 2;
            //ExitGames.Client.Photon.Hashtable bid = new ExitGames.Client.Photon.Hashtable();
            bid["Bid"] = 10000;
            roomOptions.CustomRoomProperties = bid;
            roomOptions.CustomRoomPropertiesForLobby = new string[]
            {
             "Bid",
             "Tournament"
            };
            PhotonNetwork.CreateRoom(roomName, roomOptions);
        }
        else
        {
            MainMenuStats.instance.ShowMessege("Creating Room");
            string roomName = "Room" + Random.Range(0, 1000);
            RoomOptions roomOptions = new RoomOptions();
            roomOptions.PlayerTtl = 20000;
            //roomOptions.EmptyRoomTtl = 30000;
            roomOptions.IsOpen = true;
            roomOptions.IsVisible = true;
            roomOptions.MaxPlayers = (byte)GameManager.instance.selectedTable.totalPlayers;
            //roomOptions.MaxPlayers = 2;
            //ExitGames.Client.Photon.Hashtable bid = new ExitGames.Client.Photon.Hashtable();
            bid["Bid"] = GameManager.instance.selectedBid;
            roomOptions.CustomRoomProperties = bid;
            roomOptions.CustomRoomPropertiesForLobby = new string[]
            {
             "Bid"
            };
            PhotonNetwork.CreateRoom(roomName, roomOptions);
        }

    }
    public override void OnLeftRoom()
    {
        Debug.Log("Room left");
    }
    public override void OnConnected()
    {
        MainMenuStats.instance?.ShowMessege("Connected to server");
        Debug.Log("COnnected to photon");
    }
    public override void OnDisconnected(DisconnectCause cause)
    {
        Debug.Log("Disconnected");
        MainMenuStats.instance?.ShowMessege("disconnected to server");

        if (SceneManager.GetActiveScene().name.Contains("GamePlay"))
        {
            PhotonNetwork.ReconnectAndRejoin();
            ChatManager.instance.JoinChat();

            
        }
        else
        {
            PhotonNetwork.Reconnect();
        }

        //PhotonNetwork.ConnectUsingSettings();
    }
    public override void OnRegionListReceived(RegionHandler regionHandler)
    {
    }
    public override void OnCustomAuthenticationResponse(Dictionary<string, object> data)
    {
        throw new System.NotImplementedException();
    }
    public override void OnCustomAuthenticationFailed(string debugMessage)
    {
        throw new System.NotImplementedException();
    }
    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        if(PhotonNetwork.CurrentRoom.Name.Contains("Room"))
        {
            Debug.Log("Player added: "+newPlayer.CustomProperties["ID"].ToString());
            Search_Opponent.instance.Found_Players.Add(newPlayer.CustomProperties["ID"].ToString());
            Search_Opponent.instance.Opponents_check.Add(true);
            Search_Opponent.instance.Opponents_check[Search_Opponent.instance.Found_Players.IndexOf(newPlayer.CustomProperties["ID"].ToString())] = true;
        }


        if (PhotonNetwork.CurrentRoom.Name.Contains("roomvip"))
        {
            Instantiate(Gameplay_Manager.instance.playerDisplay, Gameplay_Manager.instance.otherPlayerPositions[Gameplay_Manager.instance.VIP_seat_ID]);
            InGamePlayer player = new InGamePlayer();
            player.tablePos = Gameplay_Manager.instance.VIP_seat_ID;
            player.actorNo = PhotonNetwork.CurrentRoom.GetPlayer(Gameplay_Manager.instance.VIP_seat_ID + 1).ActorNumber;
            player.isBot = false;
            Gameplay_Manager.instance.totalPlayers.Add(player);



            DeckCards dc = new DeckCards();
            dc.cards = Gameplay_Manager.instance.gameCards;
            dc.playerIndex = newPlayer.ActorNumber;
            string b = JsonUtility.ToJson(dc);
            object[] cont = new object[] { b }; // Array contains the target position and the IDs of the selected units
            RaiseEventOptions raiseEvent = new RaiseEventOptions { Receivers = ReceiverGroup.All }; // You would have to set the Receivers to All in order to receive this event on the local client as well
            PhotonNetwork.RaiseEvent(MultiPlayerEventsCallBack.DeckFiller, cont, raiseEvent, SendOptions.SendReliable);

        }


        loadlevel_Check = true;
        StartCoroutine(Waiting());
        Debug.Log("Players in room: " + PhotonNetwork.CurrentRoom.PlayerCount);
    }
    public override void OnPlayerLeftRoom(Player otherPlayer)
    {
        if (PhotonNetwork.CurrentRoom.Name.Contains("Room"))
        {
            Search_Opponent.instance.Opponents_check.RemoveAt(Search_Opponent.instance.Found_Players.IndexOf(otherPlayer.CustomProperties["ID"].ToString()));
            Search_Opponent.instance.Found_Players.Remove(otherPlayer.CustomProperties["ID"].ToString());
            
        }

        loadlevel_Check = false;
        Gameplay_Manager.instance?.Remove_Left_Player(otherPlayer.ActorNumber);
        Debug.Log("Players in room: " + PhotonNetwork.CurrentRoom.PlayerCount);
    }
}