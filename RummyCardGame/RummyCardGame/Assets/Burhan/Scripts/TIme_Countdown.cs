using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using DG.Tweening;

public class TIme_Countdown : MonoBehaviour
{
    public Text time_Countdown;
    public GameObject YouLost_UI, Fader;
    public float time;
    internal static bool ticking;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        time -= Time.deltaTime;
        int conv = (int)time;
        //ime_Countdown -= Time.deltaTime;
        time_Countdown.text = conv.ToString()+" SEC";

        if(conv==0)
        {
            OnTimeReachZero();
        }
        
    }
    public void OnTimeReachZero()
    {
        YouLost_UI.transform.DOScale(new Vector3(0, 0, 0), .5f).SetEase(Ease.OutQuad).OnComplete(Inactive);
    }
    public void Inactive()
    {
        YouLost_UI.SetActive(false);
        Fader.SetActive(false); 
        UnityEngine.SceneManagement.SceneManager.LoadScene("Home");
    }
}
