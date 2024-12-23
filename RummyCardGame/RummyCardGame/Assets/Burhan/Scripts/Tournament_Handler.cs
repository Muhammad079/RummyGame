using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using DG.Tweening;

public class Tournament_Handler : SceneLoader
{
    int count = 0;
    public GameObject[] Tournaments;
    public GameObject Tournament_Selection_Panel_UI ;

    public GameObject Info_Panel;
    public Button Info_Btn, Back_btn;
    //public Button Play_btn;
    
    // Start is called before the first frame update
    void Start()
    {
        Back_btn.onClick.AddListener(()=> {
            Tournament_Selection_Panel_UI.transform.DOScale(new Vector3(0, 0, 0), 0.2f).SetEase(Ease.OutQuad).OnComplete(()=> {
                GameManager.instance.selectedTournament = TournamentType.empty;
                StartCoroutine(OnClick());
                //GameManager.instance.sceneToLoad = "Home";
                //Debug.LogError("Loading");
                //UnityEngine.SceneManagement.SceneManager.LoadScene("Home");
            });
        });

        Tournament_Selection_Panel_UI.transform.DOScale(new Vector3(1, 1, 1), 0.2f).SetEase(Ease.OutQuad);
        
        Info_Btn.onClick.AddListener(() => {
            Info_Panel.SetActive(true);
            Info_Panel.transform.DOScale(new Vector3(1, 1, 1), 0.2f).SetEase(Ease.OutBack);
        });
        Info_Panel.transform.GetChild(2).gameObject.GetComponent<Button>().onClick.AddListener(() => {
            Info_Panel.transform.DOScale(new Vector3(0, 0, 0), 0.2f).SetEase(Ease.InBack).OnComplete(() => {
                Info_Panel.SetActive(false);
            });
        });
     
        if (System.DateTime.Now.DayOfWeek.ToString()=="Saturday" || System.DateTime.Now.DayOfWeek.ToString() == "Sunday")
        {
            Tournaments[0].SetActive(false);
        }
        else
        {
            Tournaments[0].SetActive(false);
        }
        if (System.DateTime.Now.DayOfWeek.ToString() == "Monday" || System.DateTime.Now.DayOfWeek.ToString() == "Friday")
        {
            Tournaments[1].SetActive(false);
        }
        else
        {
            Tournaments[1].SetActive(false);
        }
        for (int i = 6; i >= (int)DayOfWeek.Sunday; i--)
        {
            count++;
            if ((int)DateTime.Now.DayOfWeek == i)
            {
                break;
            }
        }
    }
    

    // Update is called once per frame
    public override void Update()
    {
        base.Update();
        
        if (Tournaments[0].activeInHierarchy)
        {
            DateTime now = DateTime.Now;
            DateTime tomorrow = DateTime.Today.AddDays(1);
            TimeSpan remaining = tomorrow - now;
            string time = new System.DateTime(remaining.Ticks).ToString("hh:mm:ss");
            Tournaments[0].transform.Find("Right_Container").Find("Timer").Find("Timer_Text")
                .gameObject.GetComponent<Text>().text = time;
        }
        if (Tournaments[1].activeInHierarchy)
        {
            DateTime now = DateTime.Now;
            DateTime tomorrow = DateTime.Today.AddDays(1);
            TimeSpan remaining = tomorrow - now;
            string time = new System.DateTime(remaining.Ticks).ToString("hh:mm:ss");
            Tournaments[1].transform.Find("Right_Container").Find("Timer").Find("Timer_Text")
                .gameObject.GetComponent<Text>().text = time;
        }
        if (Tournaments[2].activeInHierarchy)
        {
            DateTime now = DateTime.Now;
            DateTime week = DateTime.Today.AddDays(count);
            TimeSpan remaining = week - now;
            
            string time = new System.DateTime(remaining.Ticks).ToString("dD|hh:mm:ss");
            Tournaments[2].transform.Find("Right_Container").Find("Timer").Find("Timer_Text")
                .gameObject.GetComponent<Text>().text = time;

            Tournaments[2].transform.Find("Right_Container").Find("Timer").Find("Timer_Text")
                .gameObject.GetComponent<Kozykin.MultiLanguageItem>().strItems[0] = time;
            Tournaments[2].transform.Find("Right_Container").Find("Timer").Find("Timer_Text")
                .gameObject.GetComponent<Kozykin.MultiLanguageItem>().strItems[1] = time;
            Tournaments[2].transform.Find("Right_Container").Find("Timer").Find("Timer_Text")
                .gameObject.GetComponent<Kozykin.MultiLanguageItem>().text = time; 
        }
    }
}
