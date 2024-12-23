using ExitGames.Client.Photon;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine;
using UnityEngine.UI;

public class Gameplay_Handler : MonoBehaviour
{
    public static Gameplay_Handler instance;

    public Image /*Timer_Filler = null,*/ Points_Filler = null;
    [SerializeField] Sprite[] Filler_Sprites = null;
    [SerializeField] Text Level = null, Points = null;
    public GameObject[] Voice_Highlighter;



    float time = 1;
    float dec_time;


    private void Awake()
    {
        instance = this;
    }
    void Start()
    {
        dec_time = time;
        Points_Filler.sprite = Filler_Sprites[0];
        Points_Filler.fillAmount = 1;
        //Timer_Filler.gameObject.SetActive(true);
        //Timer_Filler.fillAmount = 1;
        Level.text = "LV." + ProfileManager.instance.currentPlayer.level;
        object[] Player_lvl = new object[] { ProfileManager.instance.currentPlayer.level };
        RaiseEventOptions raiseEventOptions = new RaiseEventOptions { Receivers = ReceiverGroup.All };
        PhotonNetwork.RaiseEvent(MultiPlayerEventsCallBack.Player_lvl_notifier, Player_lvl, raiseEventOptions, SendOptions.SendReliable);
        Points.text = Gameplay_Manager.instance.RSPoints_container;
    }

    // Update is called once per frame
    void Update()
    {
        //if (Gameplay_Manager.instance.isTurn)
        //{
        //    if (dec_time > 0)
        //    {
        //        dec_time -= 0.001f;
        //        Timer_Filler.fillAmount = dec_time;
        //        object[] time_send = new object[] { dec_time };
        //        RaiseEventOptions raiseEventOptions = new RaiseEventOptions { Receivers = ReceiverGroup.All };
        //        PhotonNetwork.RaiseEvent(MultiPlayerEventsCallBack.Timer_notifier, time_send, raiseEventOptions, SendOptions.SendReliable);
        //    }
        //    if (dec_time <= 0)
        //    {
        //        Gameplay_Manager.instance.isTurn = false;
        //        object[] cont = new object[] { Gameplay_Manager.playerTurn }; // Array contains the target position and the IDs of the selected units
        //        RaiseEventOptions raiseEvent = new RaiseEventOptions { Receivers = ReceiverGroup.All }; // You would have to set the Receivers to All in order to receive this event on the local client as well
        //        PhotonNetwork.RaiseEvent(MultiPlayerEventsCallBack.turnSwitching, cont, raiseEvent, SendOptions.SendReliable);

        //        dec_time = time;
        //        Timer_Filler.fillAmount = 1;
        //    }
        //}
        //else
        //{
        //    dec_time = time;
        //    Timer_Filler.fillAmount = 1;
        //}
    }
}
