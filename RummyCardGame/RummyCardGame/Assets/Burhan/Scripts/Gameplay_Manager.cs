using DG.Tweening;
using ExitGames.Client.Photon;
using Photon.Pun;
using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using TMPro;
using UnityEngine;
using UnityEngine.UI;


[System.Serializable]
public class Gameplay_Manager : MonoBehaviourPun
{

    public static int playerTurn = 0;
    [SerializeField] Text Player_Name;
    public TextMeshProUGUI Deck_Card_Counter; public Text Deck_Card_Counter_Arabic;
    public static Gameplay_Manager instance;
    public List<int> gameCards = new List<int>();
    public List<InGamePlayer> totalPlayers = new List<InGamePlayer>();
    public GamePlayPlayer mainPlayer = new GamePlayPlayer();
    public int bid_table_amount;
    public Button knockOutButton = null;
    public event System.Action e_KnockOut = null;
    [SerializeField] private GameObject roundSettlementPlayer = null;
    [SerializeField] private Transform roundSettlementGrid = null;
    [SerializeField] private GameObject roundSettlementPanel = null;
    public static event System.Action resetRound = null;
    public static bool settlingRound = false;
    public GameObject winPanel = null, lossPanel = null;
    [SerializeField] public GameObject playerDisplay = null;
    [SerializeField] public List<Transform> otherPlayerPositions = new List<Transform>();
    public bool isTurn = false;
    [SerializeField] private GameObject cardObject = null;
    public InGamePlayer thisPlayer = null;
    [SerializeField] private GameObject Drop_Point = null, Pick_Deck_Point = null;
    private int addedDataIndex;
    private GameObject you_WIN_text;
    [SerializeField] private GameResultScene resultScreen = null;
    [SerializeField] private GameObject Msg_Prefab;
    bool playersAdjusted = false;
    public Transform[] Msg_Positions;
    [SerializeField] Sprite[] Vip_Images, Mic_switch_sprites;
    bool coroutine_check; //Dont remove Mic_Volume_notified depends on it
    public static int lastCard = 0;
    public string RSPoints_container;

    public Sprite Add_Btn_image;
    public GameObject VIP_Invite_Panel;
    public int VIP_seat_ID;
    int VIP_players_Ready = 0;
    bool VIP_GameStart = false;
    public bool VIP_GameStarted = false;
    bool game_Lost;
    bool setGameResult;

    public PointsChecker pointsChecker;

    void Awake()
    {
        instance = this;

    }
    private void Start()
    {

        Deck_Card_Counter.gameObject.SetActive(false);
        Deck_Card_Counter_Arabic.gameObject.SetActive(false);

        VIP_seat_ID = -1;
        VIP_players_Ready = 0;
        VIP_GameStart = false;
        VIP_GameStarted = false;

        //Player_Name.text = ProfileManager.instance.currentPlayer.id;
        Player_Name.text = ProfileManager.instance.currentPlayer.name;
        object[] PlayerNames = new object[] { Player_Name.text };
        RaiseEventOptions raiseEvent_Name = new RaiseEventOptions { Receivers = ReceiverGroup.Others };
        PhotonNetwork.RaiseEvent(MultiPlayerEventsCallBack.Names_Notifier, PlayerNames, raiseEvent_Name, SendOptions.SendReliable);

        //if(!PhotonNetwork.CurrentRoom.Name.Contains("roomvip"))
        //{
        //    ProfileManager.instance.currentPlayer.coins -= GameManager.instance.selectedBid;
        //}

        
        InitialStatus();
        playersAdjusted = true;
        //   e_KnockOut += CallRoundSettlement;
        resetRound += InitialStatus;
        PhotonNetwork.AddCallbackTarget(this);




    }
    public void StartGame()
    {
        game_Lost = false;
        setGameResult = false;
        playersChecked = 0;
        if (!PhotonNetwork.CurrentRoom.Name.Contains("tutorial") && GameManager.instance.selectedTournament!=TournamentType.twoMEvents && !ProfileManager.instance.currentPlayer.TwoHK_Selected)
        {
            ProfileManager.instance.currentPlayer.coins -= GameManager.instance.selectedBid;
            ProfileManager.instance.SaveUserData();
        }
        

        int Master_Index = 1;
        if (PhotonNetwork.LocalPlayer.IsMasterClient)
        {
            for (int i = 1; i < PhotonNetwork.CurrentRoom.Players.Count; i++)
            {
                if (PhotonNetwork.CurrentRoom.GetPlayer(i) == PhotonNetwork.LocalPlayer)
                {
                    Master_Index = i;
                }
            }
            object[] cont = new object[] { 1 };
            RaiseEventOptions raiseEvent = new RaiseEventOptions { Receivers = ReceiverGroup.All };
            PhotonNetwork.RaiseEvent(MultiPlayerEventsCallBack.turnSwitching, cont, raiseEvent, SendOptions.SendReliable);

        }
        StartCoroutine(Bot_Win());
    }


    void InitialStatus()
    {
        for (int i = 0; i < totalPlayers.Count; i++)
        {
            if (totalPlayers[i].ID == ProfileManager.instance.currentPlayer.id)
            {
                RSPoints_container = totalPlayers[i].totalPoint.ToString();
                object[] RSPoints = new object[] { totalPlayers[i].totalPoint.ToString() };
                RaiseEventOptions raiseEventOption = new RaiseEventOptions { Receivers = ReceiverGroup.All };
                PhotonNetwork.RaiseEvent(MultiPlayerEventsCallBack.RSPoints_notifier, RSPoints, raiseEventOption, SendOptions.SendReliable);
                break;
            }
        }
        Debug.Log("Initial Values");
        roundSettlementPanel.SetActive(false);
        knockOutButton.onClick.AddListener(()=> {
            KnockOut();
            pointsChecker.OnKnockOutButton();
            StopCoroutine(Bot_Win());
        });
        settlingRound = false;
        ClearSettlementGrid();
        ShowAllPlayers();

        if(RSPoints_container == "")
        {
            RSPoints_container = "0";
        }

    }



    void ClearSettlementGrid()
    {
        for (int n = 0; n < roundSettlementGrid.childCount; n++)
        {
            //Destroy(roundSettlementGrid.GetChild(n).gameObject);
        }
    }


    void KnockOut()
    {
        //object[] cont = new object[] { PhotonNetwork.LocalPlayer.ActorNumber }; // Array contains the target position and the IDs of the selected units
        //RaiseEventOptions raiseEvent = new RaiseEventOptions { Receivers = ReceiverGroup.All }; // You would have to set the Receivers to All in order to receive this event on the local client as well
        //PhotonNetwork.RaiseEvent(MultiPlayerEventsCallBack.turnSwitching, cont, raiseEvent, SendOptions.SendReliable);

        for (int i = 0; i < totalPlayers.Count; i++)
        {
            if (totalPlayers[i].ID == ProfileManager.instance.currentPlayer.id)
            {
                RSPoints_container = totalPlayers[i].totalPoint.ToString();
            }
        }

        string playerJson = JsonUtility.ToJson(thisPlayer);
        object[] content = new object[] { playerJson }; // Array contains the target position and the IDs of the selected units
        RaiseEventOptions raiseEventOptions = new RaiseEventOptions { Receivers = ReceiverGroup.All }; // You would have to set the Receivers to All in order to receive this event on the local client as well
        PhotonNetwork.RaiseEvent(MultiPlayerEventsCallBack.knockOut, content, raiseEventOptions, SendOptions.SendReliable);
        e_KnockOut?.Invoke();
    }
    internal void TurnSwitch(int index)
    {
        if (!settlingRound)
        {
            if (index >= totalPlayers.Count)
                index -= totalPlayers.Count;
            playerTurn = index + 1;

            //if (index > PhotonNetwork.CurrentRoom.PlayerCount)
            //    index -= PhotonNetwork.CurrentRoom.PlayerCount;
            //if (PhotonNetwork.CurrentRoom.GetPlayer(index).ActorNumber == PhotonNetwork.LocalPlayer.ActorNumber)

            if (totalPlayers[index].actorNo == PhotonNetwork.LocalPlayer.ActorNumber)
            {
                isTurn = true;
                //mainPlayer.GetComponent<PlayerStatus>().PlayerTurn();
            }
            else if (PhotonNetwork.IsMasterClient)
            {
                if (totalPlayers[index].isBot)
                {
                    //object[] cont = new object[] { index }; // Array contains the target position and the IDs of the selected units
                    //RaiseEventOptions raiseEvent = new RaiseEventOptions { Receivers = ReceiverGroup.All }; // You would have to set the Receivers to All in order to receive this event on the local client as well
                    //PhotonNetwork.RaiseEvent(MultiPlayerEventsCallBack.turnSwitching, cont, raiseEvent, SendOptions.SendReliable);
                    StartCoroutine(nameof(BotAI));
                }
            }


            for (int n = 0; n < totalPlayers.Count; n++)
            {
                Debug.Log(totalPlayers[n].actorNo + "  " + index);
                if (totalPlayers[n].actorNo == playerTurn)
                {
                    if (otherPlayerPositions[totalPlayers[n].tablePos].GetComponent<PlayerStatus>() == null)
                    {
                        otherPlayerPositions[totalPlayers[n].tablePos].GetChild(0).GetComponent<PlayerStatus>().PlayerTurn();
                        Debug.Log("child adjustment");
                    }
                    else
                    {
                        otherPlayerPositions[totalPlayers[n].tablePos].GetComponent<PlayerStatus>().PlayerTurn();
                        Debug.Log("parent adjustment");
                    }
                    Debug.Log("Turn of player " + totalPlayers[n].ID + " at table position " + n + " " + totalPlayers[n].tablePos);
                }
                else
                {
                    if (otherPlayerPositions[totalPlayers[n].tablePos].GetComponent<PlayerStatus>() == null)
                    {
                        otherPlayerPositions[totalPlayers[n].tablePos].GetChild(0).GetComponent<PlayerStatus>().TurnEnded();
                        Debug.Log("child adjustment");
                    }
                    else
                    {
                        otherPlayerPositions[totalPlayers[n].tablePos].GetComponent<PlayerStatus>().TurnEnded();
                        Debug.Log("parent adjustment");
                    }
                    Debug.Log("wainting " + totalPlayers[n].ID + " at table position " + n + " " + totalPlayers[n].tablePos);
                }
            }
        }
    }
    IEnumerator Bot_Win()
    {
        int[] bot_indexes = new int[PhotonNetwork.CurrentRoom.MaxPlayers];
        for(int i=0;i<totalPlayers.Count; i++)
        {
            bot_indexes[i] = -1;
            if(totalPlayers[i].isBot)
            {
                bot_indexes[i] = totalPlayers[i].tablePos;
            }
        }
        yield return new WaitForSeconds(Random.Range(240,300));
        //yield return new WaitForSeconds(Random.Range(30, 35));
        //yield return new WaitForSeconds(Random.Range(10, 15));

        int bot_select = -1;//Random.Range(0, PhotonNetwork.CurrentRoom.MaxPlayers);
        int bot_select2 = -1;// Random.Range(0, PhotonNetwork.CurrentRoom.MaxPlayers);
        int bot_select3 = -1;// Random.Range(0, PhotonNetwork.CurrentRoom.MaxPlayers);
        for (int i = 0; i < bot_indexes.Length; i++)
        {
            if(bot_indexes[i]!=-1)
            {
                if(bot_select==-1)
                {
                    bot_select = bot_indexes[i];
                }
                else if (bot_select2 == -1)
                {
                    bot_select2 = bot_indexes[i];
                }
                else if (bot_select3 == -1)
                {
                    bot_select3 = bot_indexes[i];
                }
            }
        }
        
        if(bot_select!=-1)
        {
            int count = 1;
            if (totalPlayers[bot_select].isBot)
            {
                for (int j = 0; j < totalPlayers[bot_select].cards.Count; j++)
                {
                    totalPlayers[bot_select].cards[j].no = count;
                    totalPlayers[bot_select].cards[j].imgIndex = count - 1;
                    totalPlayers[bot_select].cards[j].matched = true;
                    count++;
                }
            }
        }
        if (bot_select2 != -1)
        {
            int count2 = 13;
            if (totalPlayers[bot_select2].isBot)
            {
                for (int j = 0; j < totalPlayers[bot_select2].cards.Count; j++)
                {
                    totalPlayers[bot_select2].cards[j].no = count2;
                    totalPlayers[bot_select2].cards[j].imgIndex = count2 - 1;
                    totalPlayers[bot_select2].cards[j].matched = true;
                    count2++;
                    if (j == 2)
                    {
                        break;
                    }
                }
            }
        }
        if (bot_select3 != -1)
        {
            int count3 = 26;
            if (totalPlayers[bot_select3].isBot)
            {
                for (int j = 0; j < totalPlayers[bot_select3].cards.Count; j++)
                {
                    totalPlayers[bot_select3].cards[j].no = count3;
                    totalPlayers[bot_select3].cards[j].imgIndex = count3 - 1;
                    totalPlayers[bot_select3].cards[j].matched = true;
                    count3++;
                    if (j == 3)
                    {
                        break;
                    }
                }
            }
        }










            


        //for (int i = 0; i < totalPlayers.Count; i++)
        //{
        //    if(totalPlayers[i].isBot)
        //    {
        //        for (int j = 0; j < totalPlayers[i].cards.Count; j++)
        //        {
        //            totalPlayers[i].cards[j].no = count;
        //            totalPlayers[i].cards[j].imgIndex = count-1;
        //            totalPlayers[i].cards[j].matched = true;
        //            count++;
        //        }
        //    }
        //}
        KnockOut();
    }
    IEnumerator BotAI()
    {
        for (int i = 1; i < PhotonNetwork.CurrentRoom.MaxPlayers; i++)
        {
            if(otherPlayerPositions[i].GetChild(0))
            {
                if(i==1)
                {
                    if (otherPlayerPositions[i].GetChild(0).GetChild(3).GetChild(0).GetComponent<Text>().text == "bot")
                    {
                        otherPlayerPositions[i].GetChild(0).GetChild(3).GetChild(0).GetComponent<Text>().text = "David";
                    }
                }
                else if (i == 2)
                {
                    if (otherPlayerPositions[i].GetChild(0).GetChild(3).GetChild(0).GetComponent<Text>().text == "bot")
                    {
                        otherPlayerPositions[i].GetChild(0).GetChild(3).GetChild(0).GetComponent<Text>().text = "John";
                    }
                }
                else if (i == 3)
                {
                    if (otherPlayerPositions[i].GetChild(0).GetChild(3).GetChild(0).GetComponent<Text>().text == "bot")
                    {
                        otherPlayerPositions[i].GetChild(0).GetChild(3).GetChild(0).GetComponent<Text>().text = "Peter";
                    }
                }
            }
        }



        Debug.LogError("Bot Turn");
        yield return new WaitForSeconds(3);
        object[] cont = new object[] { 0 }; // Array contains the target position and the IDs of the selected units
        RaiseEventOptions raiseEventOpt = new RaiseEventOptions { Receivers = ReceiverGroup.All }; // You would have to set the Receivers to All in order to receive this event on the local client as well
        PhotonNetwork.RaiseEvent(MultiPlayerEventsCallBack.pickCard, cont, raiseEventOpt, SendOptions.SendReliable);
        yield return new WaitForSeconds(3);
        object[] content = new object[] { lastCard }; // Array contains the target position and the IDs of the selected units
        RaiseEventOptions raiseEventOptions = new RaiseEventOptions { Receivers = ReceiverGroup.All }; // You would have to set the Receivers to All in order to receive this event on the local client as well
        PhotonNetwork.RaiseEvent(MultiPlayerEventsCallBack.throwCard, content, raiseEventOptions, SendOptions.SendReliable);
        object[] con = new object[] { playerTurn }; // Array contains the target position and the IDs of the selected units
        RaiseEventOptions raiseEvent = new RaiseEventOptions { Receivers = ReceiverGroup.All }; // You would have to set the Receivers to All in order to receive this event on the local client as well
        PhotonNetwork.RaiseEvent(MultiPlayerEventsCallBack.turnSwitching, con, raiseEvent, SendOptions.SendReliable);
        StopCoroutine(nameof(BotAI));
    }
    void ScoreCalculator(InGamePlayer player)
    {
        bool negPoints = false;
        Debug.Log("Looping");
        //for (int m = 0; m < player.cards.Count; m++)
        //{
        //    if (!player.cards[m].matched)
        //    {
        //        int a = player.cards[m].no;
        //        a = Mathf.Clamp(a, 0, 10);
        //        player.lossPoint -= a;
        //    }
        //}
        //player.winPoint = player.seq * 10;

        int deckMatch = 0;
        for (int m = 0; m < player.cards.Count - 1; m++)
        {
            //string currentDeck = GamePlayPlayer.instance.gridParent.GetChild(m).GetComponent<CardView>().CardName;
            //string nextDeck = GamePlayPlayer.instance.gridParent.GetChild(m+1).GetComponent<CardView>().CardName;
            string[] Splitarray = CardManager.instance.cardImages[player.cards[m].imgIndex].name.Split(char.Parse(" "));
            string currentDeck = Splitarray[0];
            string[] Splitarray1 = CardManager.instance.cardImages[player.cards[m + 1].imgIndex].name.Split(char.Parse(" "));
            string nextDeck = Splitarray1[0];

            if (currentDeck == nextDeck)
                deckMatch++;
            else if( currentDeck == "Joker" || nextDeck == "Joker")
                deckMatch++;

        }
        if (player.ID == ProfileManager.instance.currentPlayer.id)
            Debug.LogError(deckMatch);
        if (deckMatch >= 7)
        {
            Debug.LogError("DeckMatched");
            player.winPoint = 20;
            for (int m = 0; m < player.cards.Count; m++)
            {
                player.cards[m].matched = true;
            }
        }
        else
        {
            for (int n = 0; n < player.cards.Count; n++)
            {
                if (!player.cards[n].matched)
                {
                    negPoints = true;
                    break;
                }
            }
            if (negPoints)
            {
                for (int m = 0; m < player.cards.Count; m++)
                {
                    if (!player.cards[m].matched)
                    {
                        int a = player.cards[m].no;
                        a = Mathf.Clamp(a, 0, 10);
                        player.lossPoint -= a;
                    }
                }
            }
            else
            {
                player.winPoint = 10;
            }
        }
    }
    int twoplayer = 2;
    int fourplayer = 1;
    int sixplayer = 0;
    IEnumerator loop_wait(int playersChecked)
    {
        if (addedDataIndex >= PhotonNetwork.CurrentRoom.PlayerCount)
        {
            ClearSettlementGrid();
            for (int n = 0; n < totalPlayers.Count; n++)
            {
                ScoreCalculator(totalPlayers[n]);
                totalPlayers[n].CalculatetotalPoint();

                RoundSettlementPlayer a = null ;
                if (totalPlayers.Count == 2)
                {

                    a = roundSettlementGrid.GetChild(twoplayer).gameObject.GetComponent<RoundSettlementPlayer>();
                    var k = roundSettlementGrid.GetChild(twoplayer).gameObject;
                    k.SetActive(true);
                    if (twoplayer == 2)
                    {
                        k.GetComponent<RoundSettlement_PlayerReferencer>().Name.text = ProfileManager.instance.currentPlayer.name;
                        k.GetComponent<RoundSettlement_PlayerReferencer>().Frame.sprite = ProfileManager.instance.framesDataFile.frames[ProfileManager.instance.currentPlayer.selectedFrameIndex].frameImage;
                        k.GetComponent<RoundSettlement_PlayerReferencer>().Avatar.sprite = ProfileManager.instance.avatarDataFile.avatars[ProfileManager.instance.currentPlayer.selectedFrameIndex].avatarImage;
                    }
                    else
                    {
                        bool bot = false;
                        for (int q = 0; q < totalPlayers.Count; q++)
                        {
                            if (totalPlayers[q].isBot)
                            {
                                bot = true;
                                break;
                            }
                            else if (totalPlayers[q].ID != ProfileManager.instance.currentPlayer.id)
                            {
                                PlayerProfile player_O = new PlayerProfile();
                                Firebase.Database.FirebaseDatabase.DefaultInstance.RootReference.Child("Players").Child(totalPlayers[q].ID).GetValueAsync().ContinueWith((task)=> { 
                                    if(task.IsCompleted)
                                    {
                                        player_O = JsonUtility.FromJson<PlayerProfile>(task.Result.GetRawJsonValue());
                                        k.GetComponent<RoundSettlement_PlayerReferencer>().Name.text = player_O.name;
                                        k.GetComponent<RoundSettlement_PlayerReferencer>().Frame.sprite = ProfileManager.instance.framesDataFile.frames[player_O.selectedFrameIndex].frameImage;
                                        k.GetComponent<RoundSettlement_PlayerReferencer>().Avatar.sprite = ProfileManager.instance.avatarDataFile.avatars[player_O.selectedFrameIndex].avatarImage;
                                    }
                                });
                                
                                
                            }
                        }
                        if(bot)
                        {
                            k.GetComponent<RoundSettlement_PlayerReferencer>().Name.text = "David";
                            k.GetComponent<RoundSettlement_PlayerReferencer>().Frame.sprite = ProfileManager.instance.framesDataFile.frames[0].frameImage;
                            k.GetComponent<RoundSettlement_PlayerReferencer>().Avatar.sprite = ProfileManager.instance.avatarDataFile.avatars[0].avatarImage;
                        }
                    }
                    twoplayer++;
                }
                else if (totalPlayers.Count == 4)
                {

                    a = roundSettlementGrid.GetChild(fourplayer).gameObject.GetComponent<RoundSettlementPlayer>();
                    var k = roundSettlementGrid.GetChild(fourplayer).gameObject;
                    k.SetActive(true);
                    if (fourplayer == 1)
                    {
                        k.GetComponent<RoundSettlement_PlayerReferencer>().Name.text = ProfileManager.instance.currentPlayer.name;
                        k.GetComponent<RoundSettlement_PlayerReferencer>().Frame.sprite = ProfileManager.instance.framesDataFile.frames[ProfileManager.instance.currentPlayer.selectedFrameIndex].frameImage;
                        k.GetComponent<RoundSettlement_PlayerReferencer>().Avatar.sprite = ProfileManager.instance.avatarDataFile.avatars[ProfileManager.instance.currentPlayer.selectedFrameIndex].avatarImage;
                    }
                    if (n != 0)
                    {
                        if(totalPlayers[n].isBot)
                        {
                            if (n == 1)
                                k.GetComponent<RoundSettlement_PlayerReferencer>().Name.text = "David";
                            if (n == 2)
                                k.GetComponent<RoundSettlement_PlayerReferencer>().Name.text = "John";
                            if (n == 3)
                                k.GetComponent<RoundSettlement_PlayerReferencer>().Name.text = "Peter";
                            k.GetComponent<RoundSettlement_PlayerReferencer>().Frame.sprite = ProfileManager.instance.framesDataFile.frames[0].frameImage;
                            k.GetComponent<RoundSettlement_PlayerReferencer>().Avatar.sprite = ProfileManager.instance.avatarDataFile.avatars[0].avatarImage;
                        }
                        else if(totalPlayers[n].ID != ProfileManager.instance.currentPlayer.id)
                        {
                            PlayerProfile player_O = new PlayerProfile();
                            Firebase.Database.FirebaseDatabase.DefaultInstance.RootReference.Child("Players").Child(totalPlayers[n].ID).GetValueAsync().ContinueWith((task) => {
                                if (task.IsCompleted)
                                {
                                    player_O = JsonUtility.FromJson<PlayerProfile>(task.Result.GetRawJsonValue());
                                    k.GetComponent<RoundSettlement_PlayerReferencer>().Name.text = player_O.name;
                                    k.GetComponent<RoundSettlement_PlayerReferencer>().Frame.sprite = ProfileManager.instance.framesDataFile.frames[player_O.selectedFrameIndex].frameImage;
                                    k.GetComponent<RoundSettlement_PlayerReferencer>().Avatar.sprite = ProfileManager.instance.avatarDataFile.avatars[player_O.selectedFrameIndex].avatarImage;
                                }
                            });
                        }
                    }
                    fourplayer++;
                }
                else if (totalPlayers.Count == 6)
                {

                    a = roundSettlementGrid.GetChild(sixplayer).gameObject.GetComponent<RoundSettlementPlayer>();
                    var k = roundSettlementGrid.GetChild(sixplayer).gameObject;
                    k.SetActive(true);
                    if (sixplayer == 0)
                    {
                        k.GetComponent<RoundSettlement_PlayerReferencer>().Name.text = ProfileManager.instance.currentPlayer.name;
                        k.GetComponent<RoundSettlement_PlayerReferencer>().Frame.sprite = ProfileManager.instance.framesDataFile.frames[ProfileManager.instance.currentPlayer.selectedFrameIndex].frameImage;
                        k.GetComponent<RoundSettlement_PlayerReferencer>().Avatar.sprite = ProfileManager.instance.avatarDataFile.avatars[ProfileManager.instance.currentPlayer.selectedFrameIndex].avatarImage;
                    }
                    if (n != 0)
                    {
                        if (totalPlayers[n].isBot)
                        {
                            if (n == 1)
                                k.GetComponent<RoundSettlement_PlayerReferencer>().Name.text = "David";
                            if (n == 2)
                                k.GetComponent<RoundSettlement_PlayerReferencer>().Name.text = "John";
                            if (n == 3)
                                k.GetComponent<RoundSettlement_PlayerReferencer>().Name.text = "Peter";
                            k.GetComponent<RoundSettlement_PlayerReferencer>().Frame.sprite = ProfileManager.instance.framesDataFile.frames[0].frameImage;
                            k.GetComponent<RoundSettlement_PlayerReferencer>().Avatar.sprite = ProfileManager.instance.avatarDataFile.avatars[0].avatarImage;
                        }
                        else if (totalPlayers[n].ID != ProfileManager.instance.currentPlayer.id)
                        {
                            PlayerProfile player_O = new PlayerProfile();
                            Firebase.Database.FirebaseDatabase.DefaultInstance.RootReference.Child("Players").Child(totalPlayers[n].ID).GetValueAsync().ContinueWith((task) => {
                                if (task.IsCompleted)
                                {
                                    player_O = JsonUtility.FromJson<PlayerProfile>(task.Result.GetRawJsonValue());
                                    k.GetComponent<RoundSettlement_PlayerReferencer>().Name.text = player_O.name;
                                    k.GetComponent<RoundSettlement_PlayerReferencer>().Frame.sprite = ProfileManager.instance.framesDataFile.frames[player_O.selectedFrameIndex].frameImage;
                                    k.GetComponent<RoundSettlement_PlayerReferencer>().Avatar.sprite = ProfileManager.instance.avatarDataFile.avatars[player_O.selectedFrameIndex].avatarImage;
                                }
                            });
                        }
                    }
                    sixplayer++;
                }

                //var a = Instantiate(roundSettlementPlayer, roundSettlementGrid).GetComponent<RoundSettlementPlayer>();
                int i = 0;
                while (i < totalPlayers[n].cards.Count)
                {
                    if (totalPlayers[n].cards[i].matched)
                    {
                        //var b = Instantiate(cardObject, a.matchGrid);
                        var b = Instantiate(cardObject, RoundSettlement_Animation_Controller.instance.Temp_Grid);
                        b.GetComponent<Image>().sprite = CardManager.instance.cardImages[totalPlayers[n].cards[i].imgIndex];
                        b.GetComponent<CardView>().enabled = true;
                        //b.GetComponent<CardView>().MoveToTarget(a.matchGrid.localPosition);
                        b.GetComponent<RectTransform>().localScale = new Vector3(1, 1, 1);
                        RoundSettlement_Animation_Controller.instance.Win_gameCards[i] = b;
                        RoundSettlement_Animation_Controller.instance.Win_destination = a.matchGrid;
                    }
                    else
                    {
                        //var b = Instantiate(cardObject, a.unmatchGrid);
                        var b = Instantiate(cardObject, RoundSettlement_Animation_Controller.instance.Temp_Grid);
                        b.GetComponent<Image>().sprite = CardManager.instance.cardImages[totalPlayers[n].cards[i].imgIndex];
                        b.GetComponent<CardView>().enabled = true;
                        //b.GetComponent<CardView>().MoveToTarget(a.unmatchGrid.localPosition);
                        b.GetComponent<RectTransform>().localScale = new Vector3(1, 1, 1);
                        RoundSettlement_Animation_Controller.instance.Lose_gameCards[i] = b;
                        RoundSettlement_Animation_Controller.instance.Lose_Destination = a.unmatchGrid;
                    }
                    i++;
                }
                // a.ShowMinMax(totalPlayers[n].lossPoint, totalPlayers[n].winPoint);
                a.ShowMinMax(totalPlayers[n].totalPoint);
                RoundSettlement_Animation_Controller.instance.start_anim = true;

                //if(game_Lost)
                //{
                //    if (n == totalPlayers.Count)
                //    {
                //        break;
                //    }
                //    else
                //    {
                //        yield return new WaitForSeconds(7);
                //    }
                //}
                //else
                //{
                //    if (n == totalPlayers.Count - 1)
                //    {
                //        break;
                //    }
                //    else
                //    {
                //        yield return new WaitForSeconds(7);
                //    }
                //}
                if (n == totalPlayers.Count - 1)
                {
                    break;
                }
                else
                {
                    yield return new WaitForSeconds(7);
                }

            }
        }
        roundSettlementPanel.transform.GetChild(roundSettlementPanel.transform.childCount - 1).gameObject.GetComponent<Button>().interactable = true;
        roundSettlementPanel.transform.GetChild(roundSettlementPanel.transform.childCount - 1).gameObject.GetComponent<Button>().onClick.AddListener(()=> {
            setGameResult = true;
            roundSettlementPanel.transform.GetChild(roundSettlementPanel.transform.childCount - 1).gameObject.GetComponent<Button>().interactable = false;
        });
        

        
    }
    int playersChecked = 0;
    public void CallRoundSettlement(int senderIndex, object[] data)
    {
        roundSettlementPanel.transform.GetChild(roundSettlementPanel.transform.childCount - 1).gameObject.GetComponent<Button>().interactable = false;

        int counter = 0;
        for (int i = 1; i < totalPlayers.Count; i++)
        {
            if (totalPlayers[0].totalPoint >= totalPlayers[i].totalPoint)
            {
                Debug.LogError("Game Win");
                counter++;
            }
        }
        
        if (counter>0)
        {
            roundSettlementPanel.transform.GetChild(roundSettlementPanel.transform.childCount - 3).gameObject.SetActive(true);
            roundSettlementPanel.transform.GetChild(roundSettlementPanel.transform.childCount - 2).gameObject.SetActive(false);
        }
        else
        {
            roundSettlementPanel.transform.GetChild(roundSettlementPanel.transform.childCount - 3).gameObject.SetActive(false);
            roundSettlementPanel.transform.GetChild(roundSettlementPanel.transform.childCount - 2).gameObject.SetActive(true);
        }

        Debug.Log("About to compute");
        StopCoroutine(nameof(BotAI));
        addedDataIndex++;
        InGamePlayer p = JsonUtility.FromJson<InGamePlayer>(data[0].ToString());
        //ScoreCalculator(p);
        //p.CalculatetotalPoint();
        for (int n = 0; n < totalPlayers.Count; n++)
        {
            if (totalPlayers[n].actorNo == senderIndex)
                totalPlayers[n] = p;
            break;
        }
        Debug.Log("Cards Swapped");
        settlingRound = true;
        roundSettlementPanel.SetActive(true);
        

        StartCoroutine(loop_wait(playersChecked));

        pointsChecker.OnKnockOutButton();

    }
    void NewRound()
    {
        if (totalPlayers.Count > 1)
        {
            resetRound?.Invoke();
            Debug.Log("Reset users");

        }
        else
        {
            PositionBaseReward(0);
        }

    }
    void GameResult()
    {
        for (int p = 0; p <= totalPlayers.Count - 2; p++)
        {
            for (int i = 0; i <= totalPlayers.Count - 2; i++)
            {
                if (totalPlayers[i].totalPoint > totalPlayers[i + 1].totalPoint)
                {
                    InGamePlayer t = totalPlayers[i + 1];
                    totalPlayers[i + 1] = totalPlayers[i];
                    totalPlayers[i] = t;
                }
            }
        }
        for (int n = 0; n < totalPlayers.Count; n++)
        {
            if (totalPlayers[n].ID == ProfileManager.instance.currentPlayer.id)
            {
                PositionBaseReward(n);
                break;
            }
        }
    }

    //Ahmed

    void PositionBaseReward(int position)
    {
        Debug.Log("Rewarding");
        int winPrize = 0;
        if (GameManager.instance.selectedTournament == TournamentType.empty)
        {
            if (position == totalPlayers.Count - 1)
            {
                winPrize = (int)(GameManager.instance.selectedTable.firstPosMultiplier * GameManager.instance.selectedBid) / 2; // this should give the half amount of total bid
                resultScreen.GameWin(winPrize);
            }
            else if (totalPlayers.Count - 3 >= 0)
            {
                if (position == totalPlayers.Count - 2 || position == totalPlayers.Count - 3)
                {
                    if (totalPlayers[totalPlayers.Count - 2].totalPoint == totalPlayers[totalPlayers.Count - 3].totalPoint)
                    {
                        winPrize = 0;//GameManager.instance.totalBid * 1 / 100;
                        ProfileManager.instance.currentPlayer.coins += winPrize;
                        lossPanel.SetActive(true);
                    }
                    else if (position == totalPlayers.Count - 2)
                    {
                        winPrize = 0; //GameManager.instance.totalBid * GameManager.instance.selectedTable.secondPosPercentage / 100; // for second position
                        ProfileManager.instance.currentPlayer.coins += winPrize;
                        lossPanel.SetActive(true);
                    }
                    else
                    {
                        winPrize = 0;//GameManager.instance.totalBid * GameManager.instance.selectedTable.thirdPosPercentage / 100; // for third position
                        ProfileManager.instance.currentPlayer.coins += winPrize;
                        lossPanel.SetActive(true);
                    }
                }
                else
                {
                    resultScreen.GameLoss(); lossPanel.SetActive(true);
                }
            }
            else
            {
                resultScreen.GameLoss(); lossPanel.SetActive(true);
            }
            GameManager.instance.totalBid -= winPrize;
        }
        else
        {
            if (position == totalPlayers.Count - 1)
            {
                resultScreen.GameWin(winPrize);
                //winPanel.SetActive(true);
            }
            else
            {
                resultScreen.GameLoss();
                lossPanel.SetActive(true);
            }
        }
    }
    private void OnDisable()
    {
        resetRound -= InitialStatus;
        PhotonNetwork.RemoveCallbackTarget(this);
    }





    public void ShowAllPlayers()
    {
        int playerIndex = 0;
        Transform selectedPosition = null;
        // int playerInRoom = PhotonNetwork.CurrentRoom.PlayerCount;
        int playerInRoom = PhotonNetwork.CurrentRoom.MaxPlayers;
        //  int posIncrement = otherPlayerPositions.Count / playerInRoom;
        InGamePlayer player = new InGamePlayer();
        for (int n = 1; n <= playerInRoom; n++)
        {
            if (n <= PhotonNetwork.CurrentRoom.PlayerCount)
            {
                if (PhotonNetwork.CurrentRoom.GetPlayer(n).ActorNumber == PhotonNetwork.LocalPlayer.ActorNumber)
                {
                    VoiceChat vChat = new VoiceChat();
                    vChat = PhotonNetwork.Instantiate("VoiceChatObject", Vector3.zero, Quaternion.identity).GetComponent<VoiceChat>();
                    otherPlayerPositions[0].GetComponent<PlayerStatus>().voiceChatManager = vChat;
                    playerIndex = n;

                }
            }
        }
        for (int n = 1; n <= playerInRoom; n++)
        {
            int index = playerInRoom - playerIndex + n;
            if (index >= playerInRoom)
                index -= playerInRoom;
            selectedPosition = otherPlayerPositions[index];
            player = new InGamePlayer();
            player.tablePos = index;

            Debug.Log("Calling Bots check");
            if (!PhotonNetwork.CurrentRoom.Name.StartsWith("roomvip"))
            {
                Debug.Log("With bots turned on");
                if (n <= PhotonNetwork.CurrentRoom.PlayerCount)
                {
                    player.actorNo = PhotonNetwork.CurrentRoom.GetPlayer(n).ActorNumber;
                    player.isBot = false;

                }
                else
                {
                    player.actorNo = n;
                    player.isBot = true;
                    
                }
                if (!playersAdjusted)
                {
                    if (player.actorNo != PhotonNetwork.LocalPlayer.ActorNumber)
                    {
                        var a = Instantiate(playerDisplay, selectedPosition);
                    }
                    totalPlayers.Add(player);

                }



                if(PhotonNetwork.CurrentRoom.MaxPlayers==2)
                {
                    otherPlayerPositions[1].position = otherPlayerPositions[2].position;
                    Msg_Positions[1].position = Msg_Positions[1].position;
                }



            }
            else
            {
                if (GameManager.instance.selectedTable != null)
                {
                    if (GameManager.instance.selectedTable.totalPlayers == /*2*/ PhotonNetwork.CurrentRoom.MaxPlayers)
                    {
                        if (index != 0)
                        {
                            if (PhotonNetwork.CurrentRoom.PlayerCount == PhotonNetwork.CurrentRoom.MaxPlayers)
                            {
                                //Starting Game (Dont remove check)

                            }
                            else
                            {
                                if (!otherPlayerPositions[index].gameObject.GetComponent<Image>())
                                {
                                    otherPlayerPositions[index].gameObject.AddComponent<Image>().sprite = Add_Btn_image;
                                    otherPlayerPositions[index].gameObject.AddComponent<Button>().onClick.AddListener(delegate { 
                                        VIP_invite_Panel(index);
                                        Audio_Manager.instance.Sound_Effects_Player.PlayOneShot(Audio_Manager.instance.Sound_Effects[0].Sound_Effect);
                                    });
                                }
                                else
                                {
                                    otherPlayerPositions[index].gameObject.GetComponent<Image>().sprite = Add_Btn_image;
                                    otherPlayerPositions[index].gameObject.GetComponent<Button>().onClick.AddListener(delegate { 
                                        VIP_invite_Panel(index);
                                        Audio_Manager.instance.Sound_Effects_Player.PlayOneShot(Audio_Manager.instance.Sound_Effects[0].Sound_Effect);
                                    });
                                }
                            }
                        }
                        //else
                        //{

                        //}

                    }
                }

            }


            for (int i = 0; i < totalPlayers.Count; i++)
            {
                if (totalPlayers[i].actorNo == player.actorNo)
                {
                    totalPlayers[i].tablePos = index;
                    break;
                }
            }
        }
    }
    private void Update()
    {
        if(setGameResult)
        {
            setGameResult = false;
            for (int n = 0; n < totalPlayers.Count; n++)
            //for (int n = 0; n < PhotonNetwork.CurrentRoom.MaxPlayers; n++)
            {
                if (totalPlayers[n].actorNo == PhotonNetwork.LocalPlayer.ActorNumber)
                {
                    if (totalPlayers[n].totalPoint >= 30)
                    {
                        Debug.LogError("Game Win");
                        GameResult();
                    }
                    else if (totalPlayers[n].totalPoint <= -30)
                    {
                        game_Lost = true;
                        Debug.LogError("Game Loss");
                        //totalPlayers.Remove(totalPlayers[n]);
                        resultScreen.GameLoss();
                    }
                    else
                    {
                        int ii = 0;
                        while (ii < totalPlayers.Count)
                        {
                            if (totalPlayers[ii].actorNo != PhotonNetwork.LocalPlayer.ActorNumber)
                            {
                                Debug.LogError("Looping");
                                if (totalPlayers[ii].totalPoint <= -30)
                                {

                                    Debug.Log("Game Loss");
                                    totalPlayers.Remove(totalPlayers[ii]);

                                }
                                else ii++;
                            }
                            else
                                ii++;
                        }
                    }

                }

                playersChecked++;
            }
            if (playersChecked == totalPlayers.Count && !game_Lost)
            {
                // reset gameplay     
                // GameResult();
                Invoke(nameof(NewRound), 4);
            }
        }

        //if(VIP_Invite_Panel.activeInHierarchy)
        //{
        //    if (VIP_Invite_Panel.transform.GetChild(0).gameObject.activeInHierarchy)
        //    {
        //        VIP_Invite_Panel.GetComponent<Image>().enabled = false;
        //    }
        //    else
        //    {
        //        VIP_Invite_Panel.GetComponent<Image>().enabled = true;
        //    }
        //}

        if (Manager.instance.m_CurrentLanguage == 0)
        {
            Deck_Card_Counter.gameObject.SetActive(true);
            Deck_Card_Counter_Arabic.gameObject.SetActive(false);
            Deck_Card_Counter.text = gameCards.Count.ToString();
        }
        else
        {
            Deck_Card_Counter_Arabic.gameObject.SetActive(true);
            Deck_Card_Counter.gameObject.SetActive(false);
            Deck_Card_Counter_Arabic.GetComponent<Kozykin.MultiLanguageItem>().strItems[0] = gameCards.Count.ToString();
            Deck_Card_Counter_Arabic.GetComponent<Kozykin.MultiLanguageItem>().strItems[1] = gameCards.Count.ToString();
            Deck_Card_Counter_Arabic.GetComponent<Kozykin.MultiLanguageItem>().text = gameCards.Count.ToString();
        }
        
        

        if (VIP_GameStart)
        {
            VIP_GameStarted = VIP_GameStart;
            VIP_GameStart = false;
            StartGame();
        }
    }

    //public void Continuous_GameStart_check()
    //{
    //    if (PhotonNetwork.CurrentRoom.PlayerCount < PhotonNetwork.CurrentRoom.MaxPlayers)
    //    {
    //        VIP_Invite_Panel.transform.GetChild(0).GetChild(4).GetComponent<Text>().text = "start";
    //        VIP_Invite_Panel.transform.GetChild(0).GetChild(4).GetComponent<Button>().interactable = false;
    //    }
    //    else
    //    {
    //        //VIP_Invite_Panel.transform.GetChild(0).GetChild(4).GetComponent<Text>().text = "start";
    //        VIP_Invite_Panel.transform.GetChild(0).GetChild(4).GetComponent<Button>().interactable = true;
    //        VIP_Invite_Panel.transform.GetChild(0).GetChild(4).GetComponent<Button>().onClick.AddListener(()=> {
    //            Gameplay_Manager.instance.StartGame();
    //        });
    //    }
    //}

    internal void VIP_isReady(int senderIndex, object[] data)
    {
        Debug.Log("Ready SENDER index: " + senderIndex);
        //totalPlayers[senderIndex].VIP_isReady = true;
        //VIP_players_Ready++;

        for (int i = 1; i < totalPlayers.Count; i++)
        {
            if (totalPlayers[i].actorNo == senderIndex)
            {
                totalPlayers[i].VIP_isReady = true;
                VIP_players_Ready++;
            }
        }
        if (VIP_players_Ready == totalPlayers.Count-1)
        {
            VIP_Invite_Panel.transform.GetChild(0).GetChild(3).GetComponent<Button>().interactable = true;
            VIP_Invite_Panel.transform.GetChild(0).GetChild(3).GetComponent<Button>().onClick.AddListener(() =>
            {
                totalPlayers[0].VIP_isReady = true;
                VIP_GameStart = true;
                VIP_Invite_Panel.SetActive(false);

                //StartGame();
            });
        }
    }

    public void VIP_invite_Panel(int index)
    {
        VIP_seat_ID = index;
        //char[] a = new char[5];
        string Mod_RoomName = "";
        VIP_Invite_Panel.SetActive(true);
        VIP_Invite_Panel.transform.GetChild(0).gameObject.SetActive(true);
        VIP_Invite_Panel.transform.GetChild(3).gameObject.SetActive(false);
        for (int i = 0; i < PhotonNetwork.CurrentRoom.Name.Length; i++)
        {
            if(char.IsDigit(PhotonNetwork.CurrentRoom.Name[i]))
            {
                Mod_RoomName += PhotonNetwork.CurrentRoom.Name[i];
            }
        }
        VIP_Invite_Panel.transform.GetChild(0).GetChild(0).GetChild(0).GetComponent<Text>().text = Mod_RoomName; //PhotonNetwork.CurrentRoom.Name;
        //VIP_Invite_Panel.transform.GetChild(1).GetChild(0).GetComponent<Text>().text = PhotonNetwork.CurrentRoom.Name;

        //InvokeRepeating(nameof(Continuous_GameStart_check),0.5f,0.5f);

        VIP_Invite_Panel.transform.GetChild(0).GetChild(3).GetComponent<Button>().interactable = false;



        VIP_Invite_Panel.transform.GetChild(0).GetChild(2).GetComponent<Button>().onClick.AddListener(() =>
        {
            StartCoroutine(Sharing());
        });
    }
    private IEnumerator Sharing()
    {
        yield return new WaitForEndOfFrame();
        //new NativeShare()
        //.SetSubject("Let's Play Rummy Game").SetText("Join Me on Table: "+ PhotonNetwork.CurrentRoom.Name)
        //.SetCallback((result, shareTarget) => Debug.Log("Share result: " + result + ", selected app: " + shareTarget))
        //.Share();


    }
    public void Display_Msg(int senderIndex, object[] data)
    {
        int n = 0;
        for (n = 0; n < totalPlayers.Count; n++)
        {
            if (totalPlayers[n].actorNo == senderIndex)
            {
                break;
            }
        }
        GameObject msg = Instantiate(Msg_Prefab, Msg_Positions[totalPlayers[n].tablePos].transform);

        if (Manager.instance.m_CurrentLanguage == 0)
        {
            msg.transform.GetChild(0).gameObject.SetActive(true);
            msg.transform.GetChild(1).gameObject.SetActive(false);
            msg.transform.GetChild(0).gameObject.GetComponent<TMPro.TextMeshProUGUI>().text = data.GetValue(0).ToString();
        }
        else if (Manager.instance.m_CurrentLanguage == 1)
        {
            msg.transform.GetChild(1).gameObject.SetActive(true);
            msg.transform.GetChild(0).gameObject.SetActive(false);
            msg.transform.GetChild(1).gameObject.GetComponent<Kozykin.MultiLanguageItem>().strItems[0] = data.GetValue(0).ToString();
            msg.transform.GetChild(1).gameObject.GetComponent<Kozykin.MultiLanguageItem>().strItems[1] = data.GetValue(0).ToString();
            msg.transform.GetChild(1).gameObject.GetComponent<Kozykin.MultiLanguageItem>().text = data.GetValue(0).ToString();
        }

        
        StartCoroutine(Msg_Wait_Delay(msg));
    }
    IEnumerator Msg_Wait_Delay(GameObject msg)
    {
        yield return new WaitForSeconds(2);
        Destroy(msg);
    }


    public void PickingFromDeck(int senderIndex, object[] data)
    {
        Debug.Log("Deck picking");
        int n = 0; ;
        for (n = 0; n < totalPlayers.Count; n++)
        {
            if (totalPlayers[n].actorNo == senderIndex)
            {
                break;
            }
        }
        var card = Drop_Point.transform.GetChild(Drop_Point.transform.childCount - 1).gameObject;
        //card.GetComponent<RectTransform>().sizeDelta = new Vector2(145, 200);
        card.GetComponent<RectTransform>().sizeDelta = new Vector2(300, 350);
        card.name = CardManager.instance.cardImages[System.Convert.ToInt32(data[0])].name;
        card.tag = "playingCard";
        card.GetComponent<CardView>().InHand();
        card.transform.SetParent(otherPlayerPositions[totalPlayers[n].tablePos].transform);
        card.transform.SetSiblingIndex(0);
        card.transform.DOMove(otherPlayerPositions[totalPlayers[n].tablePos].transform.position, 0.5f).OnComplete(() => Destroy(card));
        card.GetComponent<RectTransform>().localScale = new Vector3(1, 1, 1);
        card.transform.SetAsLastSibling();
    }
    public void PickedCard(int senderIndex, object[] data)
    {

        lastCard = gameCards[gameCards.Count - 1];
        int index = -1 ;
        

        if (lastCard == 52 || lastCard == 53)
        {
            //while (lastCard == 52 || lastCard == 53)
            //{
            //    index = Random.Range(0, gameCards.Count - 1);
            //    lastCard = gameCards[index];
            //}
            index = Random.Range(0, 51);
            lastCard = index;
        }


        gameCards.RemoveAt(gameCards.Count - 1 - (int)data[0]);
        if (gameCards.Count <= 0)
        {
            Debug.LogError("Calling");
            CardManager.instance.RefillDeck();
        }
        if (senderIndex != PhotonNetwork.LocalPlayer.ActorNumber)
        {
            int n = 0;
            for (n = 0; n < totalPlayers.Count; n++)
            {
                if (totalPlayers[n].actorNo == senderIndex)
                {
                    break;
                }
            }

            var card = Instantiate(cardObject, Pick_Deck_Point.transform);
            card.GetComponent<RectTransform>().sizeDelta = new Vector2(145, 200);
            //card.GetComponent<RectTransform>().sizeDelta = new Vector2(250, 300);
            card.tag = "playingCard";
            card.transform.SetParent(otherPlayerPositions[totalPlayers[n].tablePos].transform);
            card.transform.SetSiblingIndex(0);

            card.transform.DORotate(new Vector3(0, 0, 300), 0.2f).SetLoops(2);

            card.transform.DOMove(otherPlayerPositions[totalPlayers[n].tablePos].transform.position, 0.5f).OnComplete(() => Destroy(card));


        }
    }
    public void CardThrown(int senderIndex, object[] data)
    {
        if (senderIndex != PhotonNetwork.LocalPlayer.ActorNumber)
        {
            int n = 0;
            for (n = 0; n < totalPlayers.Count; n++)
            {
                if (totalPlayers[n].actorNo == senderIndex)
                {
                    break;
                }
            }

            var card = Instantiate(cardObject, otherPlayerPositions[totalPlayers[n].tablePos].transform);
            //card.GetComponent<RectTransform>().sizeDelta = new Vector2(145, 200);
            card.GetComponent<RectTransform>().sizeDelta = new Vector2(300, 350);
            card.GetComponent<RectTransform>().localScale = new Vector3(1, 1, 1);
            card.name = CardManager.instance.cardImages[System.Convert.ToInt32(data[0])].name;
            card.tag = "catchcard";
            card.GetComponent<CardView>().SetCardImg(CardManager.instance.cardImages[System.Convert.ToInt32(data[0])],
                System.Convert.ToInt32(data[0]));
            card.transform.SetParent(Drop_Point.transform);
            card.GetComponent<CardView>().Dropped();
            int a = Drop_Point.transform.childCount - 1;
            a = Mathf.Clamp(a, 0, 5);
            Vector3 pos = new Vector3(Drop_Point.transform.position.x, Drop_Point.transform.position.y + a * 0.025f);
            card.transform.DOMove(pos, 0.5f);
            card.transform.SetAsLastSibling();

            card.GetComponent<RectTransform>().localScale = new Vector3(1, 1, 1);
        }
    }
    public void KnockedOut()
    {
        for (int i = 0; i < totalPlayers.Count; i++)
        {
            if (totalPlayers[i].ID == ProfileManager.instance.currentPlayer.id)
            {
                RSPoints_container = totalPlayers[i].totalPoint.ToString();
            }
        }

        string playerJson = "";
        //ScoreCalculator(mainPlayer.player);
        //mainPlayer.player.CalculatetotalPoint();
        for (int n = 0; n < totalPlayers.Count; n++)
        {
            if (totalPlayers[n].actorNo == PhotonNetwork.LocalPlayer.ActorNumber)
            {
                playerJson = JsonUtility.ToJson(totalPlayers[n]);

            }
        }
        Debug.Log(playerJson.Length + "    " + playerJson);
        object[] content = new object[] { playerJson }; // Array contains the target position and the IDs of the selected units
        RaiseEventOptions raiseEventOptions = new RaiseEventOptions { Receivers = ReceiverGroup.All }; // You would have to set the Receivers to All in order to receive this event on the local client as well
        addedDataIndex = 0;
        PhotonNetwork.RaiseEvent(MultiPlayerEventsCallBack.roundSettlement, content, raiseEventOptions, SendOptions.SendReliable);
        Debug.Log("Data Sent");

    }
    public void Remove_Left_Player(int Removed_Pid)
    {
        ////Debug.Log("Player left : " + otherPlayer.ActorNumber);
        //Debug.Log("Player left : " + Removed_Pid);
        //int playerIndex = 0;
        //for (int n = 1; n <= PhotonNetwork.CurrentRoom.PlayerCount; n++)
        //{
        //    if (PhotonNetwork.CurrentRoom.GetPlayer(n) == PhotonNetwork.LocalPlayer)
        //        playerIndex = n;
        //}
        ////int index = PhotonNetwork.CurrentRoom.PlayerCount - playerIndex + otherPlayer.ActorNumber;
        //int index = PhotonNetwork.CurrentRoom.PlayerCount - playerIndex + Removed_Pid;
        //if (index >= PhotonNetwork.CurrentRoom.PlayerCount)
        //    index -= PhotonNetwork.CurrentRoom.PlayerCount;
        //otherPlayerPositions[index].gameObject.SetActive(false);
        ////you_WIN_text.GetComponent<Text>().text = "Player left : " + (index);
        //if (PhotonNetwork.CurrentRoom.PlayerCount < 2)
        //{
        //    GameResult();
        //}


        for (int n = 0; n < totalPlayers.Count; n++)
        {
            if (Removed_Pid == totalPlayers[n].actorNo)
            {
                totalPlayers[n].isBot = true;
                break;
            }
        }
    }

    public void Vip_Notified(int senderIndex, object[] data)
    {
        int playerIndex = 0;
        int playerInRoom = PhotonNetwork.CurrentRoom.PlayerCount;
        for (int n = 1; n <= playerInRoom; n++)
        {
            if (PhotonNetwork.CurrentRoom.GetPlayer(n) == PhotonNetwork.LocalPlayer)
                playerIndex = n;
        }
        int index = PhotonNetwork.CurrentRoom.PlayerCount - playerIndex + senderIndex;
        if (index >= PhotonNetwork.CurrentRoom.PlayerCount)
            index -= PhotonNetwork.CurrentRoom.PlayerCount;

        Debug.Log("Player index is: " + index);
        PlayerStatus p = GetPlayerStatus(index);

        if (data.GetValue(0).ToString() == "0")
        {

            p.diamond.gameObject.SetActive(false);
        }
        else
        {
            p.diamond.GetComponent<Image>().sprite = GameManager.instance.VIP_Levels[(int)data.GetValue(0) - 1];
        }
        //else if (data.GetValue(0).ToString() == "1")
        //{
        //    p.diamond.gameObject.SetActive(true);
        //    p.diamond.GetComponent<Image>().sprite = Vip_Images[0];
        //}
        //else if (data.GetValue(0).ToString() == "2")
        //{
        //    p.diamond.gameObject.SetActive(true);
        //    p.diamond.GetComponent<Image>().sprite = Vip_Images[1];
        //}
    }
    public void Mic_Switch_Notfied(int senderIndex, object[] data)
    {
        int playerIndex = 0;
        int playerInRoom = PhotonNetwork.CurrentRoom.PlayerCount;
        for (int n = 1; n <= playerInRoom; n++)
        {
            if (PhotonNetwork.CurrentRoom.GetPlayer(n) == PhotonNetwork.LocalPlayer)
                playerIndex = n;
        }
        int index = PhotonNetwork.CurrentRoom.PlayerCount - playerIndex + senderIndex;
        if (index >= PhotonNetwork.CurrentRoom.PlayerCount)
            index -= PhotonNetwork.CurrentRoom.PlayerCount;
        if (data.GetValue(0).ToString() == "True")
        {

            Debug.Log("(" + data.GetValue(0).ToString() + ")");
            Debug.Log("Player Index: " + index);
            if (otherPlayerPositions[index].GetComponent<PlayerStatus>())
            {
                //otherPlayerPositions[index].GetComponent<PlayerStatus>().Mic_BTN.transform.GetComponent<Image>().sprite = Mic_switch_sprites[2];
            }
            else
            {
                otherPlayerPositions[index].GetChild(0).GetComponent<PlayerStatus>().Mic_BTN.transform.GetComponent<Image>().sprite = Mic_switch_sprites[0];
                otherPlayerPositions[index].GetChild(0).GetComponent<PlayerStatus>().Mic_BTN.transform.GetChild(0).gameObject.SetActive(true);
            }

        }
        else
        {
            Debug.Log("(" + data.GetValue(0).ToString() + ")");
            Debug.Log("Player Index: " + index);

            if (otherPlayerPositions[index].GetComponent<PlayerStatus>())
            {
                otherPlayerPositions[index].GetComponent<PlayerStatus>().Mic_BTN.transform.GetComponent<Image>().sprite = Mic_switch_sprites[1];
            }
            else
            {
                otherPlayerPositions[index].GetChild(0).GetComponent<PlayerStatus>().Mic_BTN.transform.GetComponent<Image>().sprite = Mic_switch_sprites[1];
                otherPlayerPositions[index].GetChild(0).GetComponent<PlayerStatus>().Mic_BTN.transform.GetChild(0).gameObject.SetActive(false);
            }
        }
    }

    public void Name_Notified(int senderIndex, object[] data)
    {
        int playerIndex = 0;
        int playerInRoom = PhotonNetwork.CurrentRoom.PlayerCount;
        for (int n = 1; n <= playerInRoom; n++)
        {
            if (PhotonNetwork.CurrentRoom.GetPlayer(n) == PhotonNetwork.LocalPlayer)
                playerIndex = n;
        }
        int index = PhotonNetwork.CurrentRoom.PlayerCount - playerIndex + senderIndex;
        if (index >= PhotonNetwork.CurrentRoom.PlayerCount)
            index -= PhotonNetwork.CurrentRoom.PlayerCount;
        Debug.Log("Player index is: " + index);
        GetPlayerStatus(index).nameText.text = data.GetValue(0).ToString();
    }
    PlayerStatus GetPlayerStatus(int index)
    {
        if (otherPlayerPositions[index].GetComponent<PlayerStatus>())
        {
            return otherPlayerPositions[index].GetComponent<PlayerStatus>();
        }
        else
        {
            return otherPlayerPositions[index].GetChild(0).GetComponent<PlayerStatus>();
        }
    }
    internal void Timer_Notified(int senderIndex, object[] data)
    {
        int playerIndex = 0;
        int playerInRoom = PhotonNetwork.CurrentRoom.PlayerCount;
        for (int n = 1; n <= playerInRoom; n++)
        {
            if (PhotonNetwork.CurrentRoom.GetPlayer(n) == PhotonNetwork.LocalPlayer)
                playerIndex = n;
        }
        Debug.LogError(senderIndex + "Sender Index");
        int index = PhotonNetwork.CurrentRoom.PlayerCount - playerIndex + senderIndex;
        if (index >= PhotonNetwork.CurrentRoom.PlayerCount)
            index -= PhotonNetwork.CurrentRoom.PlayerCount;
        if (GetPlayerStatus(index).timerImage)
        {
            GetPlayerStatus(index).timerImage.fillAmount = (float)data.GetValue(0);
            Debug.LogError(data, GetPlayerStatus(index).gameObject);
        }

    }
    internal void Mic_Volume_Notified(int senderIndex, object[] data)
    {
        int playerIndex = 0;
        int playerInRoom = PhotonNetwork.CurrentRoom.PlayerCount;
        for (int n = 1; n <= playerInRoom; n++)
        {
            if (PhotonNetwork.CurrentRoom.GetPlayer(n) == PhotonNetwork.LocalPlayer)
                playerIndex = n;
        }
        int index = PhotonNetwork.CurrentRoom.PlayerCount - playerIndex + senderIndex;
        if (index >= PhotonNetwork.CurrentRoom.PlayerCount)
            index -= PhotonNetwork.CurrentRoom.PlayerCount;

        #region Mic_handling

        if ((float)data.GetValue(0) <= 0.005 && !coroutine_check)
        {

            for (int i = 0; i < Gameplay_Handler.instance.Voice_Highlighter.Length; i++)
            {
                if (i <= 0)
                {
                    if (!otherPlayerPositions[index].GetComponent<PlayerStatus>())
                    {
                        GetPlayerStatus(index).micHighligtherArea.GetChild(i).gameObject.SetActive(true);
                    }
                }
                else
                {
                    if (!otherPlayerPositions[index].GetComponent<PlayerStatus>())
                    {
                        GetPlayerStatus(index).micHighligtherArea.GetChild(i).gameObject.SetActive(false);
                    }
                }
            }
            StartCoroutine(Waiting());
        }
        else if ((float)data.GetValue(0) <= 0.010
            && (float)data.GetValue(0) > 0.005
            && !coroutine_check)
        {
            for (int i = 0; i < Gameplay_Handler.instance.Voice_Highlighter.Length; i++)
            {
                if (i <= 1)
                {
                    if (!otherPlayerPositions[index].GetComponent<PlayerStatus>())
                    {
                        GetPlayerStatus(index).micHighligtherArea.GetChild(i).gameObject.SetActive(true);
                    }
                }
                else
                {
                    if (!otherPlayerPositions[index].GetComponent<PlayerStatus>())
                    {
                        GetPlayerStatus(index).micHighligtherArea.GetChild(i).gameObject.SetActive(false);
                    }
                }

            }
            StartCoroutine(Waiting());
        }
        else if ((float)data.GetValue(0) <= 0.015
            && (float)data.GetValue(0) > 0.010
            && !coroutine_check)
        {
            for (int i = 0; i < Gameplay_Handler.instance.Voice_Highlighter.Length; i++)
            {
                if (i <= 2)
                {
                    if (!otherPlayerPositions[index].GetComponent<PlayerStatus>())
                    {
                        GetPlayerStatus(index).micHighligtherArea.GetChild(i).gameObject.SetActive(true);
                    }
                }
                else
                {
                    if (!otherPlayerPositions[index].GetComponent<PlayerStatus>())
                    {
                        GetPlayerStatus(index).micHighligtherArea.GetChild(i).gameObject.SetActive(false);
                    }
                }

            }
            StartCoroutine(Waiting());
        }
        else if ((float)data.GetValue(0) <= 0.020
            && (float)data.GetValue(0) > 0.015
            && !coroutine_check)
        {
            for (int i = 0; i < Gameplay_Handler.instance.Voice_Highlighter.Length; i++)
            {
                if (i <= 3)
                {
                    if (!otherPlayerPositions[index].GetComponent<PlayerStatus>())
                    {
                        GetPlayerStatus(index).micHighligtherArea.GetChild(i).gameObject.SetActive(true);
                    }
                }
                else
                {
                    if (!otherPlayerPositions[index].GetComponent<PlayerStatus>())
                    {
                        GetPlayerStatus(index).micHighligtherArea.GetChild(i).gameObject.SetActive(false);
                    }
                }

            }
            StartCoroutine(Waiting());
        }
        else if ((float)data.GetValue(0) > 0.020
            && !coroutine_check)
        {
            for (int i = 0; i < Gameplay_Handler.instance.Voice_Highlighter.Length; i++)
            {
                if (!otherPlayerPositions[index].GetComponent<PlayerStatus>())
                {
                    GetPlayerStatus(index).micHighligtherArea.GetChild(i).gameObject.SetActive(true);
                }
            }
            StartCoroutine(Waiting());
        }
        #endregion
    }
    IEnumerator Waiting()
    {
        coroutine_check = true;
        yield return new WaitForSeconds(2);
        StopCoroutine(Waiting());
        coroutine_check = false;
    }
    internal void Player_lvl_notified(int senderIndex, object[] data)
    {
        int playerIndex = 0;
        int playerInRoom = PhotonNetwork.CurrentRoom.PlayerCount;
        for (int n = 1; n <= playerInRoom; n++)
        {
            if (PhotonNetwork.CurrentRoom.GetPlayer(n) == PhotonNetwork.LocalPlayer)
                playerIndex = n;
        }
        int index = PhotonNetwork.CurrentRoom.PlayerCount - playerIndex + senderIndex;
        if (index >= PhotonNetwork.CurrentRoom.PlayerCount)
            index -= PhotonNetwork.CurrentRoom.PlayerCount;
        GetPlayerStatus(index).Player_lvl.transform.GetChild(0).GetComponent<Text>().text = "LV." + data.GetValue(0);

    }
    internal void RSPoints_notified(int senderIndex, object[] data)
    {
        int playerIndex = 0;
        int playerInRoom = PhotonNetwork.CurrentRoom.PlayerCount;
        for (int n = 1; n <= playerInRoom; n++)
        {
            if (PhotonNetwork.CurrentRoom.GetPlayer(n) == PhotonNetwork.LocalPlayer)
                playerIndex = n;
        }
        int index = PhotonNetwork.CurrentRoom.PlayerCount - playerIndex + senderIndex;
        if (index >= PhotonNetwork.CurrentRoom.PlayerCount)
            index -= PhotonNetwork.CurrentRoom.PlayerCount;

        Debug.Log("Total Points: " + data.GetValue(0).ToString() + " passed at index: " + index);
        GetPlayerStatus(index).pointText.text = data.GetValue(0).ToString();

    }
}
