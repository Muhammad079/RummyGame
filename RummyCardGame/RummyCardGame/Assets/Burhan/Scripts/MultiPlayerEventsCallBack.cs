using ExitGames.Client.Photon;
using Photon.Pun;
using Photon.Realtime;
using System.Collections.Generic;
using UnityEngine;
public class MultiPlayerEventsCallBack : MonoBehaviourPun, IPunObservable, IOnEventCallback
{
    public const byte throwCard = 2;
    public const byte pickCard = 3;

    public const byte knockOut = 4;
    public const byte roundSettlement = 5;
    public const byte winGame = 6;
    public const byte lossGame = 7;
    public const byte reEnterGame = 8;
    public const byte nextRound = 9;
    public const byte turnSwitching = 10;
    public const byte pickingFromDeck = 11;
    public const byte msg_from_Player = 12;
    public const byte Mic_Switch_Notfier = 13;
    public const byte Vip_Notfier = 14;
    public const byte Names_Notifier = 15;
    public const byte Timer_notifier = 16;
    public const byte Mic_Volume_notifier = 17;
    public const byte Player_lvl_notifier = 18;
    public const byte RSPoints_notifier = 19;
    public const byte DeckFiller = 20;
    public const byte CardsShuffled = 21;
    public const byte VIP_isReady = 22;
    private void Start()
    {
        PhotonNetwork.AddCallbackTarget(this);
    }
    public void OnEvent(EventData photonEvent)
    {
        byte eventCode = photonEvent.Code;
        object[] data = (object[])photonEvent.CustomData;

        if(eventCode == VIP_isReady)
        {
            Gameplay_Manager.instance.VIP_isReady(photonEvent.Sender, data);
        }
        else if (eventCode == throwCard)
        {
            Audio_Manager.instance.Sound_Effects_Player
                .PlayOneShot(Audio_Manager.instance.Sound_Effects[1].Sound_Effect);
            Gameplay_Manager.instance.CardThrown(Gameplay_Manager.playerTurn, data);
        }
        else if (eventCode == turnSwitching)
        {
            TurnSwitching((int)data[0]);
        }
        else if (eventCode == pickCard)
        {
            Audio_Manager.instance.Sound_Effects_Player
                .PlayOneShot(Audio_Manager.instance.Sound_Effects[1].Sound_Effect);

            Gameplay_Manager.instance.PickedCard(Gameplay_Manager.playerTurn, data);
        }
        else if (eventCode == pickingFromDeck)
        {
            Audio_Manager.instance.Sound_Effects_Player
                .PlayOneShot(Audio_Manager.instance.Sound_Effects[1].Sound_Effect);
            Gameplay_Manager.instance.PickingFromDeck(photonEvent.Sender, data);
        }
        else if (eventCode == knockOut)
        {
            Gameplay_Manager.instance.KnockedOut();
        }
        else if (eventCode == roundSettlement)
        {
            Debug.Log("Calling Settlement");
            Gameplay_Manager.instance.CallRoundSettlement(photonEvent.Sender, data);
        }
        else if (eventCode == winGame)
        {
            Debug.Log("win game");
        }
        else if (eventCode == lossGame)
        {
            Debug.Log("loss game");
        }
        else if (eventCode == reEnterGame)
        {
            Debug.Log("re enter game");
        }
        else if (eventCode == nextRound)
        {
            Debug.Log("next round");
        }
        else if (eventCode == msg_from_Player)
        {
            Debug.Log("msg from player");
            Gameplay_Manager.instance.Display_Msg(photonEvent.Sender, data);
        }
        else if (eventCode == Mic_Switch_Notfier)
        {
            Gameplay_Manager.instance.Mic_Switch_Notfied(photonEvent.Sender, data);
        }
        else if (eventCode == Vip_Notfier)
        {
            Gameplay_Manager.instance.Vip_Notified(photonEvent.Sender, data);
        }
        else if (eventCode == Names_Notifier)
        {
            Gameplay_Manager.instance.Name_Notified(photonEvent.Sender, data);
        }
        else if (eventCode == Timer_notifier)
        {
            Gameplay_Manager.instance.Timer_Notified(photonEvent.Sender, data);
        }
        else if (eventCode == Mic_Volume_notifier)
        {
            Gameplay_Manager.instance.Mic_Volume_Notified(photonEvent.Sender, data);
        }
        else if (eventCode == Player_lvl_notifier)
        {
            Gameplay_Manager.instance.Player_lvl_notified(photonEvent.Sender, data);
        }
        else if (eventCode == RSPoints_notifier)
        {
            Gameplay_Manager.instance.RSPoints_notified(photonEvent.Sender, data);
        }
        else if (eventCode == DeckFiller)
        {
            DeckCards a = JsonUtility.FromJson<DeckCards>(data[0].ToString());
            Gameplay_Manager.instance.gameCards.Clear();
            for (int n = 0; n < a.cards.Count; n++)
            {
                Gameplay_Manager.instance.gameCards.Add(a.cards[n]);
            }
            if (a.playerIndex <= PhotonNetwork.CurrentRoom.PlayerCount && !PhotonNetwork.IsMasterClient)
            {
                 if (a.playerIndex == PhotonNetwork.CurrentRoom.PlayerCount)
                {
                     CardManager.instance.InitialStatus();
                    DeckCards dc = new DeckCards();
                    dc.cards = Gameplay_Manager.instance.gameCards;
                    dc.playerIndex = PhotonNetwork.LocalPlayer.ActorNumber + 1;
                    string b = JsonUtility.ToJson(dc);
                    object[] cont = new object[] { b }; // Array contains the target position and the IDs of the selected units
                    RaiseEventOptions raiseEvent = new RaiseEventOptions { Receivers = ReceiverGroup.Others }; // You would have to set the Receivers to All in order to receive this event on the local client as well
                    PhotonNetwork.RaiseEvent(DeckFiller, cont, raiseEvent, SendOptions.SendReliable);
                }
            }
            else if (a.playerIndex <= Gameplay_Manager.instance.totalPlayers.Count && PhotonNetwork.IsMasterClient)
            {
                 if (Gameplay_Manager.instance.totalPlayers[a.playerIndex - 1].isBot)
                {
                    for (int n = 0; n < 9; n++)
                    {
                        GameCard g = new GameCard();
                        g.imgIndex = Gameplay_Manager.instance.gameCards[Gameplay_Manager.instance.gameCards.Count - 1];
                        string[] Splitarray = CardManager.instance.cardImages[Gameplay_Manager.instance.gameCards[Gameplay_Manager.instance.gameCards.Count - 1]].name.Split(char.Parse(" "));
                        if (Splitarray.Length > 1)
                        {
                            g.no = int.Parse(Splitarray[1]);
                            g.cardcategory = Splitarray[0];
                        }
                        g.no = Mathf.Abs(g.no);
                        g.matched = false;
                        Gameplay_Manager.instance.totalPlayers[a.playerIndex - 1].cards.Add(g);
                        Gameplay_Manager.instance.gameCards.RemoveAt(Gameplay_Manager.instance.gameCards.Count - 1);
                    }
                    DeckCards dc = new DeckCards();
                    dc.cards = Gameplay_Manager.instance.gameCards;
                    dc.playerIndex = a.playerIndex + 1;
 
                    string b = JsonUtility.ToJson(dc);
                    object[] cont = new object[] { b }; // Array contains the target position and the IDs of the selected units
                    RaiseEventOptions raiseEvent = new RaiseEventOptions { Receivers = ReceiverGroup.All }; // You would have to set the Receivers to All in order to receive this event on the local client as well
                    PhotonNetwork.RaiseEvent(DeckFiller, cont, raiseEvent, SendOptions.SendReliable);
                }
            }
            else
            {
                 object[] cont = new object[] { "" }; // Array contains the target position and the IDs of the selected units
                RaiseEventOptions raiseEvent = new RaiseEventOptions { Receivers = ReceiverGroup.All }; // You would have to set the Receivers to All in order to receive this event on the local client as well
                PhotonNetwork.RaiseEvent(CardsShuffled, cont, raiseEvent, SendOptions.SendReliable);
            }
        }

        else if (eventCode == CardsShuffled)
        {
            if(!PhotonNetwork.CurrentRoom.Name.Contains("roomvip"))
            {
                Gameplay_Manager.instance.StartGame();
            }
            
        }

    }
    public void TurnSwitching(int senderIndex)
    {
         Gameplay_Manager.instance.TurnSwitch(senderIndex);
    }
    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.IsWriting)
        {
            bool writting = false;
            stream.Serialize(ref writting);
         }
    }
    private void OnDisable()
    {
        PhotonNetwork.RemoveCallbackTarget(this);
    }
}
[System.Serializable]
public class DeckCards
{
    public int playerIndex = 0;
    public List<int> cards = new List<int>();
}