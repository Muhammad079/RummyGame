using DG.Tweening;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ChatScreen : MonoBehaviour
{
    public static string friendId { get; private set; }
    public static Transform contentParent { get; private set; }
    [SerializeField] private Transform chatPanel = null;
    [SerializeField] private Button crossButton = null;
    [SerializeField] private GameObject sentMessege = null, recievedMessege = null;
    [SerializeField] private GameObject bottomPanel = null;
    [SerializeField] private GameObject worldChat, worldChatButton, inviteButton, invitePanel, chatButton, friendChatPanel;
    bool publicChat = false;
    private void Start()
    {
        
    }
    private void OnEnable()
    {
        ChatManager.instance.r_RecievePrivateMessege += RecieveMessege;
        ChatManager.instance.r_SendPrivateMessege += SendMessege;
        crossButton.onClick.AddListener(HidePanel);
        DisplayPanel();
    }
    void DisplayPanel()
    {
        if(SceneManager.GetActiveScene().name.Contains("GamePlay"))
        {
            chatPanel.DOLocalMoveX(0, 0.2f);
        }
        else
        {
            chatPanel.DOLocalMoveY(0, 0.2f);
        }
        
    }
    void HidePanel()
    {
        if (SceneManager.GetActiveScene().name.Contains("GamePlay"))
        {
            chatPanel.DOLocalMoveX(-1400, 0.2f).OnComplete(Deactivate);
        }
        else
        {
            chatPanel.DOLocalMoveY(-400, 0.2f).OnComplete(Deactivate);
        }
            
    }
    void Deactivate()
    {
        this.gameObject.SetActive(false);
    }
    public void ChannelSelect(Transform content, bool worldChat)
    {
        publicChat = worldChat;
        content.parent.gameObject.SetActive(true);
        content.gameObject.SetActive(true);
        contentParent = content;
    }
    public void FriendSelected(string friend_ID, Transform gridParent)
    {
        for (int n = 0; n < gridParent.childCount; n++)
        {
            gridParent.GetChild(n).gameObject.SetActive(false);
        }
        contentParent = gridParent;
        friendId = friend_ID;
        worldChat.SetActive(false);
        worldChatButton.SetActive(false);
        inviteButton.SetActive(false);
        invitePanel.SetActive(false);
        chatButton.SetActive(true);
        friendChatPanel.SetActive(true);
        bottomPanel.SetActive(true);
    }
    public void SendButton(string messege)
    {
        if (publicChat)
        {
            ChatManager.instance.PublicMessegeSend(messege);
        }
        else if (friendId != "")
        {
            ChatManager.instance.PrivateMessegeSend(friendId, messege);
        }
    }
    void SendMessege(string messege)
    {
        Instantiate(sentMessege, contentParent).GetComponent<ChatMessegeDialogue>().DisplayMessege(ProfileManager.instance.currentPlayer.name, messege);
    }
    void RecieveMessege(string sender, string messege)
    {
        Instantiate(recievedMessege, contentParent).GetComponent<ChatMessegeDialogue>().DisplayMessege(sender, messege);
    }
    void OnDisable()
    {
        crossButton.onClick.RemoveAllListeners();
        ChatManager.instance.r_RecievePrivateMessege -= RecieveMessege;
        ChatManager.instance.r_SendPrivateMessege -= SendMessege;
    }
}
