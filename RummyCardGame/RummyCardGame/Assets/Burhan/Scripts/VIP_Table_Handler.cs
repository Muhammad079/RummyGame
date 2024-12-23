using DG.Tweening;
using Photon.Pun;
using Photon.Realtime;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
public class VIP_Table_Handler : MonoBehaviour
{
    public static bool no_coins = false;
    public Table selected_table;
    public Transform Join_Panel;
    public GameObject waitingPanel;
    public Button[] Next, Prev;
    public Text Players, Bet, Room_Name;
    int Players_int = 2, Bet_int = 1000;
    public Button Create, Close, Join, Close_Join_Panel, Joining;
    public InputField Room_Code;
    public TextMeshProUGUI prizeText = null;
    int[] Bets_avail = { 1000, 5000, 10000, 20000, 50000,
                         100000, 200000, 1000000, 50000000};
    int count = 0;

    private void OnEnable()
    {
        //if (ProfileManager.instance.currentPlayer.isVip)
        //{
        //    Create.interactable = true;
        //}
        //else
        //{
        //    Create.interactable = false;
        //}
    }
    // Start is called before the first frame update
    void Start()
    {
        Players_int = 2;
        Bet_int = 1000;
        PrizeCalculator();
        

        Next[0].onClick.AddListener(() =>
        {
            if (Players_int < 6)
            {
                Players_int += 2;
            }
            Players.text = Players_int.ToString();
            PrizeCalculator();
        });
        Prev[0].onClick.AddListener(() =>
        {
            if (Players_int > 2)
            {
                Players_int -= 2;
            }
            Players.text = Players_int.ToString();
            PrizeCalculator();
        });
        Next[1].onClick.AddListener(() =>
        {

            if (count != Bets_avail.Length - 1)
            {
                count++;
            }
            Bet_int = Bets_avail[count];
            Bet.text = Bet_int.ToString(); PrizeCalculator();
        });
        Prev[1].onClick.AddListener(() =>
        {

            if (count != Bets_avail.Length - Bets_avail.Length)
            {
                count--;
            }
            Bet_int = Bets_avail[count];
            Bet.text = Bet_int.ToString();
            PrizeCalculator();
        });

        Create.onClick.AddListener(() =>
        {
            Creating_Table(Players_int, Bet_int);

        });
        Close.onClick.AddListener(() =>
        {
            transform.DOScale(new Vector3(0, 0, 0), 0.2f).SetEase(Ease.InSine).OnComplete(() =>
            {
                gameObject.SetActive(false);
            });

        });
        Join.onClick.AddListener(() =>
        {
            transform.GetChild(1).DOScale(new Vector3(0, 0, 0), 0.2f).SetEase(Ease.InSine).OnComplete(() =>
            {
                transform.GetChild(1).gameObject.SetActive(false);
                Join_Panel.gameObject.SetActive(true);
                Join_Panel.DOScale(new Vector3(1, 1, 1), 0.2f).SetEase(Ease.OutSine);
            });

        });
        Close_Join_Panel.onClick.AddListener(() =>
        {
           transform.DOScale(new Vector3(0, 0, 0), 0.2f).SetEase(Ease.OutSine).OnComplete(() =>
            {
                 gameObject.SetActive(false);

            });
        });
        Joining.onClick.AddListener(() =>
        {
            Joining_Table(Room_Code.text);
        });
    }
    public void Show()
    {
        gameObject.SetActive(true);
        transform.DOScale(new Vector3(1,1,1), 0.2f).SetEase(Ease.OutSine);
    }
    void PrizeCalculator()
    {
        if (Players_int <= 4)
            prizeText.text = ((int)Bet_int * 2).ToString();
        else

            prizeText.text = ((int)Bet_int * 2.5f).ToString();
    }
    public void Joining_Table(string RoomName)
    {
        if (no_coins)
        {
            GameManager.instance.Not_Enough_C_Panel.SetActive(true);
            GameManager.instance.Not_Enough_C_Panel.transform.DOScale(new Vector3(1, 1, 1), 0.2f).SetEase(Ease.OutBounce);
        }
        else
        {
            RoomName = RoomName.ToLower();
            string Mod_RoomName = "roomvip" + RoomName;
            PhotonNetwork.JoinRoom(Mod_RoomName);
        }
    }

    private void Creating_Table(int Selected_Players, int Selected_Bet)
    {
        if(Selected_Players == 2)
        {
            selected_table.totalPlayers = 2;
            selected_table.firstPosMultiplier = 1.6f;
            selected_table.secondPosPercentage = 2;
            selected_table.thirdPosPercentage = 1;
            GameManager.instance.selectedXPData = GameManager.instance.xpData_2Players;
            GameManager.instance.selectedTable = selected_table;
        }
        else if (Selected_Players == 4)
        {
            selected_table.totalPlayers = 4;
            selected_table.firstPosMultiplier = 2;
            selected_table.secondPosPercentage = 2;
            selected_table.thirdPosPercentage = 1;
            GameManager.instance.selectedXPData = GameManager.instance.xpData;
            GameManager.instance.selectedTable = selected_table;
        }
        else if (Selected_Players == 6)
        {
            selected_table.totalPlayers = 6;
            selected_table.firstPosMultiplier = 2.5f;
            selected_table.secondPosPercentage = 2;
            selected_table.thirdPosPercentage = 1;
            GameManager.instance.selectedXPData = GameManager.instance.xpData;
            GameManager.instance.selectedTable = selected_table;
        }


        GameManager.instance.selectedBid = Selected_Bet;
        GameManager.instance.selectedTable.totalPlayers = Selected_Players;


        if (ProfileManager.instance.currentPlayer.coins >= Selected_Bet)
        {
            //string roomName = "roomvip" + UnityEngine.Random.Range(0, 1000);
            string roomName = "roomvip" + UnityEngine.Random.Range(10000, 99999);
            RoomOptions roomOptions = new RoomOptions();
            roomOptions.PlayerTtl = 20000;
            roomOptions.EmptyRoomTtl = 30000;
            roomOptions.IsOpen = true;
            roomOptions.IsVisible = true;
            roomOptions.CleanupCacheOnLeave = true;
            roomOptions.MaxPlayers = (byte)GameManager.instance.selectedTable.totalPlayers;
            //roomOptions.MaxPlayers = 2;
            ExitGames.Client.Photon.Hashtable bid = new ExitGames.Client.Photon.Hashtable();

            bid["Bid"] = GameManager.instance.selectedBid;
            roomOptions.CustomRoomProperties = bid;
            roomOptions.CustomRoomPropertiesForLobby = new string[]
            {
             "Bid"
            };
            roomName = roomName.ToLower();
            //info.CustomProperties["Bid"]= GameManager.instance.selectedBid;
            PhotonNetwork.CreateRoom(roomName, roomOptions);
            if (PhotonNetwork.InRoom)
            {
                PhotonNetwork.CurrentRoom.SetCustomProperties(bid);

            }
            Debug.Log("Room is: " + roomName);
            Room_Name.text = roomName;

            waitingPanel.SetActive(true);
        }
        else
        {
            GameManager.instance.Not_Enough_C_Panel.SetActive(true);
            GameManager.instance.Not_Enough_C_Panel.transform.DOScale(new Vector3(1, 1, 1), 0.2f).SetEase(Ease.OutBounce);
        }
    }

    //public static void Created_Table_Invitation(List<Transform> otherPlayerPositions)
    //{
    //    if(GameManager.instance.selectedTable.totalPlayers==2)
    //    {
    //        otherPlayerPositions[1].gameObject.AddComponent<Button>();
    //        otherPlayerPositions[1].gameObject.AddComponent<Image>().sprite =  Add_Btn_image;
    //    }
    //    else if(GameManager.instance.selectedTable.totalPlayers == 4)
    //    {

    //        otherPlayerPositions[1].gameObject.AddComponent<Button>();
    //        otherPlayerPositions[2].gameObject.AddComponent<Button>();
    //        otherPlayerPositions[3].gameObject.AddComponent<Button>();
    //    }
    //    else if (GameManager.instance.selectedTable.totalPlayers == 6)
    //    {
    //        otherPlayerPositions[1].gameObject.AddComponent<Button>();
    //        otherPlayerPositions[2].gameObject.AddComponent<Button>();
    //        otherPlayerPositions[3].gameObject.AddComponent<Button>();
    //        otherPlayerPositions[4].gameObject.AddComponent<Button>();
    //        otherPlayerPositions[5].gameObject.AddComponent<Button>();
    //    }
    //}
}