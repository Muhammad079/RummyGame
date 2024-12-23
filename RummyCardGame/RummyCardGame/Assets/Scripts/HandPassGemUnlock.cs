using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class HandPassGemUnlock : MonoBehaviour
{
    [SerializeField] private Hand_Pass_Handler hand_Pass_Handler = null;
    [SerializeField] private GameObject parentPanel = null;
    [SerializeField] private GameObject notEnoughGems = null;
    void Start()
    {
        GetComponent<Button>().onClick.AddListener(Unlock);
    }
    void Unlock()
    {
        if (ProfileManager.instance.currentPlayer.gems >= 100)
        {
            ProfileManager.instance.currentPlayer.gems -= 100;
            ProfileManager.instance.currentPlayer.goldenCards += 16;
            ProfileManager.instance.SaveUserData();
            parentPanel.SetActive(false);
            hand_Pass_Handler.StartState();
        }
        else
        {
            notEnoughGems.SetActive(true);
        }
    }
}
