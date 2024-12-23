using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

public class Close_Lucky_Spin_Anim : SceneLoader
{
    public GameObject Lucky_Spin_UI;
    //public Wheel_Rotator Wheel_Rotator;
    public Prize_Display prize_Display;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    public override void Update()
    {
        base.Update();
    }
    public void OnCloseBTNclick()
    {
        prize_Display.CollectReward();
        Lucky_Spin_UI.transform.DOLocalMoveY(400, 0.5f).SetEase(Ease.OutQuad).OnComplete(fader_Inactive);
    }
    public void fader_Inactive()
    {
        Loading_Screen = GameObject.Find("Loading_Screen");
        Loading_animation = Loading_Screen.transform.GetChild(0).GetComponent<Text>();
        StartCoroutine(OnClick());
        //GameManager.instance.sceneToLoad = "Home";
        //Debug.LogError("Loading");
        //UnityEngine.SceneManagement.SceneManager.LoadScene("Home");
    }
}
