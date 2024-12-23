using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChatChannelSelector : MonoBehaviour
{
    [SerializeField] private Transform contentParent = null;
    [SerializeField] private ChatScreen chatScreen = null;
    [SerializeField] private bool publicChat = false;
    void Start()
    {
        GetComponent<Button>().onClick.AddListener(OnClick);
    }

   void OnClick()
    {
        chatScreen.ChannelSelect(contentParent,publicChat);
    }
}
