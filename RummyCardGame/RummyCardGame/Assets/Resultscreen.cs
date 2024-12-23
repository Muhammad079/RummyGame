using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Resultscreen : MonoBehaviour
{
    public Sprite Winner1;
    public Sprite Looser0;
    public Button Okay;

    public Image BannerImage;

    public Result_Panel_animation_Controller result_Panel_Animation_Controller;

    private void Awake()
    {
        Okay.onClick.AddListener(() => OnOkaybtn());
    }

    private void OnOkaybtn()
    {
        result_Panel_Animation_Controller.gameObject.SetActive(true);
        this.gameObject.SetActive(false);

    }

    public void SetImage(int status)
    {
        switch(status)
        {
            case 1:
                BannerImage.sprite = Winner1;
                break;
            case 0:
                BannerImage.sprite = Looser0;
                break;

        }
    }
}
