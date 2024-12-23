using UnityEngine;

public class ChatFriendsPanel : MonoBehaviour
{
    [SerializeField] private GameObject friendBar = null;
    [SerializeField] private Transform contentParent = null;
    [SerializeField] private ChatScreen chatScreen = null;
    [SerializeField] private Transform chatGrid=null;
    [SerializeField] private GameObject noFriendText = null;
    private void OnEnable()
    {
        ShowRecords();
    }
    void ShowRecords()
    {
        if (ProfileManager.instance.currentPlayer.friends.Count > 0)
        {
            noFriendText.SetActive(false);
            for (int n = 0; n < ProfileManager.instance.currentPlayer.friends.Count; n++)
            {
                GameObject bar = new GameObject();
                if (n < contentParent.childCount)
                {
                    bar = contentParent.GetChild(n).gameObject;
                }
                else
                {
                    bar = Instantiate(friendBar, contentParent);
                }
                bar.SetActive(true);
                bar.GetComponent<ChatFriendBar>().PassData(ProfileManager.instance.currentPlayer.friends[n], chatScreen, chatGrid);
            }
        }
        //else
        //{
        //    transform.GetChild(0).gameObject.SetActive(true);
        //}
    }
}
