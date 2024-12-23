using DG.Tweening;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class How_to_Play_Handler : SceneLoader
{
    [SerializeField] Transform[] Pages;
    //[SerializeField] Transform NewPanel;
    public Table selected_table;
    [SerializeField] Button Right_btn, Left_btn;
    int count = 0;
    // Start is called before the first frame update
    void OnEnable()
    {
        string roomName = "tutorial" + UnityEngine.Random.Range(0, 1000);
        RoomOptions roomOptions = new RoomOptions();
        ExitGames.Client.Photon.Hashtable bid = new ExitGames.Client.Photon.Hashtable();
        if (ProfileManager.instance.currentPlayer.First_Time_Login)
        {
            selected_table.totalPlayers = 2;
            selected_table.firstPosMultiplier = 0;
            selected_table.secondPosPercentage = 0;
            selected_table.thirdPosPercentage = 0;
            GameManager.instance.selectedXPData = GameManager.instance.xpData_2Players;
            GameManager.instance.selectedTable = selected_table;

            GameManager.instance.selectedBid = 1000;
            GameManager.instance.selectedTable.totalPlayers = 2;




            
            roomOptions.PlayerTtl = 20000;
            roomOptions.EmptyRoomTtl = 30000;
            roomOptions.IsOpen = true;
            roomOptions.IsVisible = true;
            roomOptions.CleanupCacheOnLeave = true;
            roomOptions.MaxPlayers = (byte)GameManager.instance.selectedTable.totalPlayers;
            //roomOptions.MaxPlayers = 2;
            

            bid["Bid"] = GameManager.instance.selectedBid;
            roomOptions.CustomRoomProperties = bid;
            roomOptions.CustomRoomPropertiesForLobby = new string[]
            {
                        "Bid"
            };
            roomName = roomName.ToLower();
        }
            











        //NewPanel.localScale = Vector3.zero;
        transform.localScale = Vector3.zero;
        for (int i = 1; i < Pages.Length; i++)
        {
            Pages[i].localScale = Vector3.one;
        }
        if (ProfileManager.instance.currentPlayer.First_Time_Login)
        {
            //ProfileManager.instance.currentPlayer.First_Time_Login = false;
            ProfileManager.instance.SaveUserData();
            transform.localScale = Vector3.one;
            Pages[0].gameObject.SetActive(true);
            transform.DOLocalMoveY(0, 1);
        }
        else
        {
            //ProfileManager.instance.currentPlayer.First_Time_Login = false;
            ProfileManager.instance.SaveUserData();
            transform.localScale = Vector3.one;
            Pages[0].gameObject.SetActive(true);
            transform.DOLocalMoveY(0, 1);
            //SceneManager.LoadSceneAsync("Home");

        }

        Right_btn.onClick.AddListener(() =>
        {
            if(count<0)
            {
                count = 0;
            }
            if (count < 6)
            {
                Pages[count].DOMoveX(30, 0.3f);//.OnComplete(() =>
                //{
                    Pages[count].gameObject.SetActive(false);
                    count++;
                    if (count < 6)
                    {
                        Pages[count].gameObject.SetActive(true);
                        Pages[count].DOMoveX(0, 0.3f);
                    }
                    else if (count == 6)
                    {
                        transform.DOLocalMoveY(400, 1).OnComplete(() =>
                        {

                            if (ProfileManager.instance.currentPlayer.First_Time_Login)
                            {
                                ProfileManager.instance.currentPlayer.First_Time_Login = false;


                                PhotonNetwork.CreateRoom(roomName, roomOptions);
                                if (PhotonNetwork.InRoom)
                                {
                                    PhotonNetwork.CurrentRoom.SetCustomProperties(bid);

                                }





                            }
                            else
                            {
                                //SceneManager.LoadScene("Home");

                                    Loading_Screen = GameObject.Find("Loading_Screen");
                                    Loading_animation = Loading_Screen.transform.GetChild(0).GetComponent<Text>();
                                    StartCoroutine(OnClick());
                                                             
                            }


                            //NewPanel.localScale = Vector3.one;
                            //NewPanel.DOLocalMoveY(0, 1);
                        });
                       
                    }
                //});

            }
        });
        Left_btn.onClick.AddListener(() =>
        {
            if (count > 0)
            {
                Pages[count].DOMoveX(-30, 0.3f);//.OnComplete(() =>
                //{
                    Pages[count].gameObject.SetActive(false);
                    count--;
                    if (count < 6)
                    {
                        Pages[count].gameObject.SetActive(true);
                        Pages[count].DOMoveX(0, 0.3f);
                    }
                //});

            }
        });

        //NewPanel.GetChild(0).GetChild(2).GetComponent<Button>().onClick.AddListener(() =>
        //{

        //    NewPanel.DOLocalMoveY(370, 1).OnComplete(() =>
        //    {
                

                
        //    });
        //    if (ProfileManager.instance.currentPlayer.First_Time_Login)
        //    {
        //        ProfileManager.instance.currentPlayer.First_Time_Login = false;

                
        //        PhotonNetwork.CreateRoom(roomName, roomOptions);
        //        if (PhotonNetwork.InRoom)
        //        {
        //            PhotonNetwork.CurrentRoom.SetCustomProperties(bid);

        //        }





        //    }
        //    else
        //    {
        //        SceneManager.LoadScene("Home");
        //    }

        //});
    }
    public override void Update()
    {
        base.Update();
    }
}
