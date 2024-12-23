using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FriendReqPanel : MonoBehaviour
{
   
    public static FriendReqBar selectedFriend;
    [SerializeField] private GameObject friendReqBar = null;
    [SerializeField] private Transform contentParent = null;
    [SerializeField] private Button acceptButton = null, declineButton = null;
    PlayerProfile userReq = new PlayerProfile();
    void Start()
    {
        acceptButton.onClick.AddListener(ReqAccept);
        declineButton.onClick.AddListener(ReqDecline);
        acceptButton.gameObject.SetActive(false);
        declineButton.gameObject.SetActive(false);
    }
    
    public void ReqSelection(PlayerProfile reqUser)
    {
        userReq = reqUser;
        acceptButton.gameObject.SetActive(true);
        declineButton.gameObject.SetActive(true);

    }
   public void ShowFriendRequests()
    {
        for (int n = 0; n < ProfileManager.instance.currentPlayer.friendReq.Count; n++)
        {
            if (!ProfileManager.instance.currentPlayer.friends.Contains(ProfileManager.instance.currentPlayer.friendReq[n]))
            {
                int m = n - 1;
                GameObject bar = new GameObject();
                if (n < contentParent.childCount)
                {
                    bar = contentParent.GetChild(n).gameObject;
                }
                else
                {
                    bar = Instantiate(friendReqBar, contentParent);
                }
                bar.GetComponent<FriendReqBar>().friendReqPanel = this;
                bar.GetComponent<FriendReqBar>().ShowStats(ProfileManager.instance.currentPlayer.friendReq[n]);
            }
        }
    }
    void ReqAccept()
    {
        if (selectedFriend)
        {
            ProfileManager.instance.currentPlayer.friendReq.Remove(selectedFriend.reqUserID);
            if (!ProfileManager.instance.currentPlayer.friends.Contains(selectedFriend.reqUserID))
                ProfileManager.instance.currentPlayer.friends.Add(selectedFriend.reqUserID);
            if (!userReq.friends.Contains(ProfileManager.instance.currentPlayer.id))
                userReq.friends.Add(ProfileManager.instance.currentPlayer.id);
            userReq.pendingReq.Remove(ProfileManager.instance.currentPlayer.id);
            DatabaseFunctions.SaveDataInDB(userReq);
           ProfileManager.instance.SaveUserData();
            selectedFriend.gameObject.SetActive(false);
            selectedFriend = null;
        }
    }
    void ReqDecline()
    {
        if (selectedFriend)
        {
            ProfileManager.instance.currentPlayer.friendReq.Remove(selectedFriend.reqUserID);
            userReq.pendingReq.Remove(ProfileManager.instance.currentPlayer.id);
           ProfileManager.instance.SaveUserData();
            DatabaseFunctions.SaveDataInDB(userReq);
            selectedFriend.gameObject.SetActive(false);
            selectedFriend = null;
        }
    }
}
