using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Pre_DefinedChatPanel : MonoBehaviour
{
    public GameObject Predefined_Prefab;
    [SerializeField] private List<string> Free_preDefinedMesseges = new List<string>();
    [SerializeField] private List<string> Free_Messeges_Arabic = new List<string>();
    [SerializeField] private List<string> Premium_preDefinedMesseges = new List<string>();
    [SerializeField] private List<string> Premium_Messeges_Arabic = new List<string>();
    [SerializeField] private List<string> VIP_preDefinedMesseges = new List<string>();
    [SerializeField] private List<string> VIP_Messeges_Arabic = new List<string>();
    [SerializeField] private GameObject preDefinedMessegePanel = null;
    [SerializeField] private ChatScreen chatScreen = null;
    //[SerializeField] private ChatScreen chatScreen = null;

    private void Awake()
    {
        

    }
    void OnEnable()
    {
        StartCoroutine(Delete_Create_Child(action => {
            if (action == 1 || action == 2)
            {
                ShowMesseges();
            }
        }));
        
    }
    IEnumerator Delete_Create_Child(Action<int> action)
    {
        int children = transform.childCount;
        Debug.Log("total childs: " + transform.childCount);
        if (transform.childCount > 0)
        {
            while(transform.childCount>0)
            {
                DestroyImmediate(transform.GetChild(0).gameObject);
            }
            //for (int i = 0; i < children; i++)
            //{
            //    DestroyImmediate(transform.GetChild(i).gameObject);
            //    Debug.Log("total childs: Destroyed " + transform.GetChild(i).gameObject);
            //    Debug.Log("total childs: loop Count " + i);
            //}
            action(1);
        }
        else
        {
            action(2);
        }
        Debug.Log("total childs: Remaining " + transform.childCount);
        //return true;
        
        yield return null;
    }

    void ShowMesseges()
    {
        
        int count = 0;
        
        if (Manager.instance.m_CurrentLanguage == 0)
        {

            for (int n = 0; n < Free_preDefinedMesseges.Count; n++)
            {
                //if (n >= transform.childCount)
                var pre = Instantiate(Predefined_Prefab, transform);
                pre.transform.GetChild(0).gameObject.SetActive(true);
                pre.transform.GetChild(1).gameObject.SetActive(false);
                pre.transform.GetComponent<Pre_DefinedMessegeBar>().PassMessege(Free_preDefinedMesseges[n], preDefinedMessegePanel, chatScreen);
                count++;
                if (SceneManager.GetActiveScene().name.Contains("GamePlay"))
                {
                    pre.transform.GetChild(0).GetComponent<TextMeshProUGUI>().fontSizeMax = 40;
                }
            }
            for (int n = 0; n < Premium_preDefinedMesseges.Count; n++)
            {
                var pre = Instantiate(Predefined_Prefab, transform);
                pre.transform.GetChild(0).gameObject.SetActive(true);
                pre.transform.GetChild(1).gameObject.SetActive(false);
                pre.GetComponent<Pre_DefinedMessegeBar>().PassMessege(Premium_preDefinedMesseges[n], preDefinedMessegePanel, chatScreen);
                count++;
                if (SceneManager.GetActiveScene().name.Contains("GamePlay"))
                {
                    pre.transform.GetChild(0).GetComponent<TextMeshProUGUI>().fontSizeMax = 40;
                }
            }
            if (ProfileManager.instance.currentPlayer.isVip)
            {
                for (int n = 0; n < VIP_preDefinedMesseges.Count; n++)
                {
                    var pre = Instantiate(Predefined_Prefab, transform);
                    pre.transform.GetChild(0).gameObject.SetActive(true);
                    pre.transform.GetChild(1).gameObject.SetActive(false);
                    pre.GetComponent<Pre_DefinedMessegeBar>().PassMessege(VIP_preDefinedMesseges[n], preDefinedMessegePanel, chatScreen);
                    count++;
                    if (SceneManager.GetActiveScene().name.Contains("GamePlay"))
                    {
                        pre.transform.GetChild(0).GetComponent<TextMeshProUGUI>().fontSizeMax = 40;
                    }
                }
            }
        }
        else
        {
            for (int n = 0; n < Free_Messeges_Arabic.Count; n++)
            {
                //if (n >= transform.childCount)
                var pre = Instantiate(Predefined_Prefab, transform);
                pre.transform.GetChild(0).gameObject.SetActive(false);
                pre.transform.GetChild(1).gameObject.SetActive(true);
                pre.transform.GetComponent<Pre_DefinedMessegeBar>().PassMessege(Free_Messeges_Arabic[n], preDefinedMessegePanel, chatScreen);
                count++;
                if(SceneManager.GetActiveScene().name.Contains("GamePlay"))
                {
                    pre.transform.GetChild(1).GetComponent<Text>().resizeTextMaxSize = 65;
                }
            }


            for (int n = 0; n < Premium_Messeges_Arabic.Count; n++)
            {
                var pre = Instantiate(Predefined_Prefab, transform);
                pre.transform.GetChild(0).gameObject.SetActive(false);
                pre.transform.GetChild(1).gameObject.SetActive(true);
                pre.GetComponent<Pre_DefinedMessegeBar>().PassMessege(Premium_Messeges_Arabic[n], preDefinedMessegePanel, chatScreen);
                count++;
                if (SceneManager.GetActiveScene().name.Contains("GamePlay"))
                {
                    pre.transform.GetChild(1).GetComponent<Text>().resizeTextMaxSize = 65;
                }
            }
            if (ProfileManager.instance.currentPlayer.isVip)
            {
                for (int n = 0; n < VIP_Messeges_Arabic.Count; n++)
                {
                    var pre = Instantiate(Predefined_Prefab, transform);
                    pre.transform.GetChild(0).gameObject.SetActive(false);
                    pre.transform.GetChild(1).gameObject.SetActive(true);
                    pre.GetComponent<Pre_DefinedMessegeBar>().PassMessege(VIP_Messeges_Arabic[n], preDefinedMessegePanel, chatScreen);
                    count++;
                    if (SceneManager.GetActiveScene().name.Contains("GamePlay"))
                    {
                        pre.transform.GetChild(1).GetComponent<Text>().resizeTextMaxSize = 65;
                    }
                }
            }
        }

        
        
        
    }
}