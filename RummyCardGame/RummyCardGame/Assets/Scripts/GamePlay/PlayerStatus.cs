using DG.Tweening;
using ExitGames.Client.Photon;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine;
using UnityEngine.UI;
public class PlayerStatus : MonoBehaviourPun
{
    public static PlayerStatus instance;
    //[SerializeField] private GameObject turnIndicator = null;
    [SerializeField] private Sprite volumeOn = null, volumeOff = null;
    public Button Mic_BTN = null, Mute_BTN = null;
    public VoiceChat voiceChatManager = null;
    public GameObject Player_lvl = null;
    public Sprite[] mic_switch_sprites;
    public Image diamond = null;
    bool Mic_Switch;
    public Text nameText = null;
    float time = 10;
    float dec_time;
    bool playerTurn;
    public Image timerImage;
    public Image GreenTime;
    public Text pointText;
    public Transform micHighligtherArea = null;
    private void Awake()
    {
        instance = this;
    }

    void Start()
    {

        Mic_BTN?.onClick.AddListener(() =>
       {
           Mic_Switch = !Mic_Switch;
           if (Mic_Switch)
           {
               voiceChatManager.StartTransmission();
               Mic_BTN.GetComponent<Image>().sprite = mic_switch_sprites[0];
           }
           else
           {
               voiceChatManager.StopTransmission();
               Mic_BTN.GetComponent<Image>().sprite = mic_switch_sprites[1];
           }

           object[] Mic_Switch_Notifier = new object[] { Mic_Switch };
           RaiseEventOptions raiseEventOptions = new RaiseEventOptions { Receivers = ReceiverGroup.All };
           PhotonNetwork.RaiseEvent(MultiPlayerEventsCallBack.Mic_Switch_Notfier, Mic_Switch_Notifier, raiseEventOptions, SendOptions.SendReliable);
       });
        Mute_BTN?.onClick.AddListener(() =>
        {
            if (voiceChatManager.GetComponent<AudioSource>().mute)
            {
                Mute_BTN.GetComponent<Image>().sprite = volumeOn;
                voiceChatManager.GetComponent<AudioSource>().mute = false;
            }
            else
            {
                Mute_BTN.GetComponent<Image>().sprite = volumeOff;
                voiceChatManager.GetComponent<AudioSource>().mute = true;
            }
        });
    }

    public void PlayerTurn()
    {
        timerImage.GetComponent<Image>().fillAmount = 1;
        timerImage.gameObject.SetActive(true);
        GreenTime.gameObject.SetActive(true);
        //turnIndicator_Opponent.SetActive(false);
        //turnIndicator_Opponent.GetComponent<Image>().fillAmount = 0;

        Debug.LogError("Player Turn");
        dec_time = time;
        playerTurn = true;


    }
    public void TurnEnded()
    {
        playerTurn = false;
        timerImage.gameObject.SetActive(false);
        GreenTime.gameObject.SetActive(false);

        timerImage.GetComponent<Image>().fillAmount = 0;
        Debug.LogError("Player Turn Ended");

        //turnIndicator_Opponent.SetActive(true);
        //turnIndicator_Opponent.GetComponent<Image>().fillAmount = 1;
    }
    private void Update()
    {
        if (playerTurn)
        {
            if (dec_time > 0)
            {
                dec_time -= Time.deltaTime;
                float a = dec_time / 10;
                timerImage.GetComponent<Image>().fillAmount = a;
            }
            else if (dec_time <= 0)
            {

                //Gameplay_Manager.instance.isTurn = false;
                //Gameplay_Manager.instance.TurnSwitch(PhotonNetwork.LocalPlayer.ActorNumber);
                //object[] cont = new object[] { PhotonNetwork.LocalPlayer.ActorNumber };
                //RaiseEventOptions raiseEvent = new RaiseEventOptions { Receivers = ReceiverGroup.All };
                //PhotonNetwork.RaiseEvent(MultiPlayerEventsCallBack.turnSwitching, cont, raiseEvent, SendOptions.SendReliable);




                //TurnEnded();

                if(Gameplay_Manager.instance.isTurn)
                {
                    if (GetComponent<GamePlayPlayer>().gridParent.transform.childCount > 9)
                    {
                        CardView card = GetComponent<GamePlayPlayer>().gridParent.transform.GetChild(9).GetComponent<CardView>();


                        card.transform.SetParent(CardManager.instance.carddroppoint.transform); card.transform.DOScale(new Vector3(0.6f, 0.6f, 1f), 0.3f).SetEase(Ease.Linear);

                        // selectedCard.transform.position = new Vector3(Random.Range(carddroppoint.transform.position.x + 1, carddroppoint.transform.position.x - 1), Random.Range(carddroppoint.transform.position.y + 1, carddroppoint.transform.position.y - 1), 1); ;
                        card.transform.position = CardManager.instance.carddroppoint.transform.position;
                        int a = card.transform.GetSiblingIndex();
                        a = Mathf.Clamp(a, 0, 5);

                        card.transform.position = new Vector3(card.transform.position.x, card.transform.position.y + a * 0.025f);
                        //selectedCard.transform.localScale = new Vector3(1.2f,1.5f,1);
                        card.transform.localScale = new Vector3(1, 1, 1);
                        card.transform.rotation = Quaternion.Euler(0, 0, 0);


                        card.Dropped();
                        card.tag = "catchcard";
                        object[] content = new object[] { card.card.imgIndex }; // Array contains the target position and the IDs of the selected units
                        RaiseEventOptions raiseEventOptions = new RaiseEventOptions { Receivers = ReceiverGroup.All }; // You would have to set the Receivers to All in order to receive this event on the local client as well
                        PhotonNetwork.RaiseEvent(MultiPlayerEventsCallBack.throwCard, content, raiseEventOptions, SendOptions.SendReliable);
                    }
                }
                




                Gameplay_Manager.instance.isTurn = false;


                

                object[] cont = new object[] { Gameplay_Manager.playerTurn }; // Array contains the target position and the IDs of the selected units
                RaiseEventOptions raiseEvent = new RaiseEventOptions { Receivers = ReceiverGroup.All }; // You would have to set the Receivers to All in order to receive this event on the local client as well
                PhotonNetwork.RaiseEvent(MultiPlayerEventsCallBack.turnSwitching, cont, raiseEvent, SendOptions.SendReliable);


                playerTurn = false;
                dec_time = time;
                timerImage.GetComponent<Image>().fillAmount = 0;
            }
        }


    }
}