using ExitGames.Client.Photon;
using Photon.Pun;
using Photon.Realtime;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
public class Pre_DefinedMessegeBar : MonoBehaviour
{
    [SerializeField] private string messege = null;
    GameObject preDefinedChatPanel = null;
    ChatScreen chat_Screen = null;
    private void Start()
    {
        GetComponent<Button>().onClick.AddListener(SendMessege);
    }
    internal void PassMessege(string v, GameObject preDefinedMessegePanel, ChatScreen chatScreen)
    {
        messege = v;
        //if (transform.GetChild(0).GetComponent<TextMeshProUGUI>())
        //{
        //    transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = messege;

        //}
        if (Manager.instance.m_CurrentLanguage == 0)
        {
            transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = messege;
        }
        else
        {
            transform.GetChild(1).GetComponent<Text>().text = messege;
            transform.GetChild(1).GetComponent<Kozykin.MultiLanguageItem>().strItems[0] = messege;
            transform.GetChild(1).GetComponent<Kozykin.MultiLanguageItem>().strItems[1] = messege;
            transform.GetChild(1).GetComponent<Kozykin.MultiLanguageItem>().text = messege;
        }

        preDefinedChatPanel = preDefinedMessegePanel;
        chat_Screen = chatScreen;
    }
    void SendMessege()
    {
        if (chat_Screen != null)
        {
            chat_Screen.SendButton(messege);
        }
        else
        {
            object[] content = new object[] { messege };
            RaiseEventOptions raiseEventOptions = new RaiseEventOptions { Receivers = ReceiverGroup.All };
            PhotonNetwork.RaiseEvent(MultiPlayerEventsCallBack.msg_from_Player, content, raiseEventOptions, SendOptions.SendReliable);
        }
        preDefinedChatPanel.SetActive(false);
    }
}