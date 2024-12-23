using ExitGames.Client.Photon;
using Photon.Chat;
using Photon.Pun;
using System;
using System.Collections;
using UnityEngine;

public class ChatManager : MonoBehaviour, IChatClientListener
{
    public static ChatManager instance;
    Invited_Tables_Data Data = new Invited_Tables_Data();
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

    }
    bool isConnected = false;
    ChatClient client;
    public event Action<string> r_SendPrivateMessege = null;



    public event Action<string, string> r_RecievePrivateMessege = null;


    public void DebugReturn(DebugLevel level, string message)
    {

    }

    public void OnChatStateChange(ChatState state)
    {

    }

    public void OnConnected()
    {
        Debug.Log("Connected to chat");
     
        string[] channel = { "WorldChat", "Tables" };
       
        client.Subscribe(channel);
        isConnected = true;
    }

    public void OnDisconnected()
    {
        isConnected = false;
    }

    public void OnGetMessages(string channelName, string[] senders, object[] messages)
    {
        Debug.Log("New public messege");


        if (channelName.Contains("Tables"))
        {

            for (int i = 0; i < messages.Length; i++)
            {
                Debug.Log("Message length: " + messages.Length);
                Debug.Log("Sender length: " + senders.Length);
                Debug.Log("sender data received: " + senders.GetValue(i).ToString());
                Debug.Log("table data received: " + messages.GetValue(i).ToString());
            }


            if (Data.Room_ID == "")
            {
                Data.Room_ID = messages[0].ToString();
            }
            else if (Data.Players_available == -1)
            {
                Data.Players_available = int.Parse(messages[0].ToString());
            }
            else if (Data.Max_Players == -2)
            {
                Data.Max_Players = int.Parse(messages[0].ToString());
            }
            else if (Data.Bid == -3)
            {
                Data.Bid = int.Parse(messages[0].ToString());
                ProfileManager.instance.currentPlayer.Tables_VIP_invited.Add(Data);
                StartCoroutine(Duration_to_Live(Data));

            }






        }
        else
        {
            for (int i = 0; i < senders.Length; i++)
            {
                if (senders[i] != ProfileManager.instance.currentPlayer.id)
                    r_RecievePrivateMessege?.Invoke(senders[i], messages[i].ToString());
                else
                    r_SendPrivateMessege?.Invoke(messages[i].ToString());
            }
        }



    }
    IEnumerator Duration_to_Live(Invited_Tables_Data Data_remove)
    {
        yield return new WaitForSeconds(10);
        ProfileManager.instance.currentPlayer.Tables_VIP_invited.Remove(Data_remove);
        Data = new Invited_Tables_Data();
    }
    public void OnPrivateMessage(string sender, object message, string channelName)
    {
        Debug.Log("Private Sender IS: " + sender);
        Debug.Log("Private Channel is: " + channelName);
        Debug.Log("Private Friend is: " + channelName.Split(":"[0]).GetValue(1));
        Debug.Log("Private Message is: "+message);
        
        if (message.ToString().Contains("Table"))
        {
            object Mod_message = message.ToString().Split(":"[0]).GetValue(0);
            Debug.Log("Private MOD MSG: " +Mod_message);
            if (Data.Room_ID == "")
            {
                Data.Room_ID = Mod_message.ToString();
            }
            else if (Data.Players_available == -1)
            {
                Data.Players_available = int.Parse(Mod_message.ToString());
            }
            else if (Data.Max_Players == -2)
            {
                Data.Max_Players = int.Parse(Mod_message.ToString());
            }
            else if (Data.Bid == -3)
            {
                Data.Bid = int.Parse(Mod_message.ToString());
                ProfileManager.instance.currentPlayer.Tables_VIPFriends_invited.Add(Data);
                StartCoroutine(Duration_to_Live2(Data));

            }
        }
        else
        {
            if (sender != ProfileManager.instance.currentPlayer.id)
                r_RecievePrivateMessege?.Invoke(sender, message.ToString());
            else
                r_SendPrivateMessege?.Invoke(message.ToString());
        }
    }
    IEnumerator Duration_to_Live2(Invited_Tables_Data Data_remove)
    {
        yield return new WaitForSeconds(10);
        ProfileManager.instance.currentPlayer.Tables_VIPFriends_invited.Remove(Data_remove);
        Data = new Invited_Tables_Data();
    }

    public void OnStatusUpdate(string user, int status, bool gotMessage, object message)
    {
        throw new System.NotImplementedException();
    }

    public void OnSubscribed(string[] channels, bool[] results)
    {
        Debug.Log("subscribed");
    }

    public void OnUnsubscribed(string[] channels)
    {
        Debug.Log("Unsubscribed");
    }

    public void OnUserSubscribed(string channel, string user)
    {
        Debug.Log("subscribed");
    }

    public void OnUserUnsubscribed(string channel, string user)
    {
        throw new System.NotImplementedException();
    }

    void Start()
    {
        ProfileManager.instance.currentPlayer.Tables_VIP_invited.Clear();
        ProfileManager.instance.currentPlayer.Tables_VIPFriends_invited.Clear();
        client = new ChatClient(this);
        JoinChat();
    }
    public void JoinChat()
    {
        client.Connect(PhotonNetwork.PhotonServerSettings.AppSettings.AppIdChat, PhotonNetwork.AppVersion, new AuthenticationValues(ProfileManager.instance.currentPlayer.id));
    }
    void Update()
    {

        client.Service();

    }
    public void PrivateMessegeSend(string friendID, string messege)
    {
        Debug.Log(friendID);
        client.SendPrivateMessage(friendID, messege);
    }
    public void PublicMessegeSend(string messege)
    {
        Debug.Log("Publishing messege: " + messege);
        if (messege.Contains("roomvip"))
        {
            client.PublishMessage("Tables", messege);
        }
        else
        {
            client.PublishMessage("WorldChat", messege);
        }


    }
    public void PublicTableInvite(string[] message)
    {
        Debug.Log("Sending msg: " + message);
        //client.PublishMessage("Tables", message);

        client.PublishMessage("Tables", message[0]);
        client.PublishMessage("Tables", message[1]);
        client.PublishMessage("Tables", message[2]);
        client.PublishMessage("Tables", message[3]);
    }
    public void PrivateTableSend(string friendID, string[] messege)
    {
        Debug.Log(friendID + ":Table");
        client.SendPrivateMessage(friendID, messege[0] + ":Table");
        client.SendPrivateMessage(friendID, messege[1] + ":Table");
        client.SendPrivateMessage(friendID, messege[2] + ":Table");
        client.SendPrivateMessage(friendID, messege[3] + ":Table");
    }

}
