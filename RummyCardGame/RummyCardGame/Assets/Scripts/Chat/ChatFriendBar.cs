using UnityEngine;
using UnityEngine.UI;

public class ChatFriendBar : MonoBehaviour
{
    string id = "";
    [SerializeField] private Text userId = null;
    [SerializeField] private Button chatInvite = null;
    private ChatScreen chatScreen = null;
    Transform contentParent;
    PlayerProfile friendProfile = new PlayerProfile();
    void Start()
    {
        chatInvite.onClick.AddListener(OnClick);
    }
    public void PassData(string friendID, ChatScreen chat_Screen, Transform gridParent)
    {
        if (friendProfile.id == "")
        {
            userId.text = friendID;
            DatabaseFunctions.LoadOtherUsers(friendID, PlayerFound);
        }
        else
            PlayerFound(friendProfile);
        contentParent = gridParent;
        chatScreen = chat_Screen;
    }
    void PlayerFound(PlayerProfile profile)
    {
        friendProfile = profile;
        Debug.Log("Passing data");
    }
    void OnClick()
    {
        chatScreen.FriendSelected(friendProfile.id, contentParent);
    }
}
