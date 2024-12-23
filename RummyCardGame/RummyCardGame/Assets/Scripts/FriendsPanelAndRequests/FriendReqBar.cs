using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FriendReqBar : MonoBehaviour
{
    float timer = 2;
    bool gotData = false;
    event Action<PlayerProfile> dataFound = null;
    internal FriendReqPanel friendReqPanel = null;
    internal string reqUserID = "";
    [SerializeField] private Button selectionButton = null;
    [SerializeField] private Image profilePicture = null;
    [SerializeField] private Text userName = null,userId=null;
    [SerializeField] private GameObject selectionIndicator = null;
    PlayerProfile reqUser = new PlayerProfile();
    [SerializeField] private Text barCounter = null;
    public void ShowStats(string friendID)
    {
        Debug.Log(friendID);
        reqUserID = friendID;
        dataFound += PlayerData;
        DatabaseFunctions.LoadOtherUsers(friendID, dataFound);  
    }
    private void Start()
    {
        selectionButton.onClick.AddListener(ReqSelection);
    }
    void PlayerData(PlayerProfile player)
    {
        dataFound -= PlayerData;
        Debug.Log(player.id);
        reqUser = player;
        gotData = true;
    }
    void ReqSelection()
    {
        for (int n = 0; n < transform.parent.childCount; n++)
        {
            transform.parent.GetChild(n).GetComponent<FriendReqBar>().UnselectReq();
        }
        FriendReqPanel.selectedFriend = this.gameObject.GetComponent<FriendReqBar>();
        friendReqPanel.ReqSelection(reqUser);
        selectionIndicator.SetActive(true);
    }
    private IEnumerator GetFBPicture(string facebookId)
    {
        Debug.Log("Getting Image");
        var www = new WWW("http://graph.facebook.com/" + facebookId + "/picture?width=720&height=720&type=square&redirect=true");
        yield return www;
        if (www.isDone)
        {
            Debug.Log("waiting" + www.bytesDownloaded);
            Texture2D tempPic = new Texture2D(720, 720);
            tempPic = www.texture;
            profilePicture.sprite = Sprite.Create(tempPic, new Rect(0, 0, tempPic.width, tempPic.height), new Vector2());
        }
        StopCoroutine(nameof(GetFBPicture));
    }
    public void UnselectReq()
    {
        selectionIndicator.SetActive(false);
    }
    private void Update()
    {
        if (gotData)
        {
            timer -= Time.deltaTime;
            if (timer < 0)
            {
                DisplayData();
                gotData = false;
            }
        }
    }
    void DisplayData()
    {
        userId.text = reqUser.id;
        userName.text = reqUser.name;
        barCounter.text = (transform.GetSiblingIndex() + 1).ToString();
        StartCoroutine(GetFBPicture(reqUser.id));
    }
}
