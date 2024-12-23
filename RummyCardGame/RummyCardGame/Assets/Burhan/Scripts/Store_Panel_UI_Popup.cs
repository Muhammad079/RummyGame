using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Store_Panel_UI_Popup : MonoBehaviour
{
    public GameObject Store_Panel_UI;
    
    void DisplayPanel()
    {
        Store_Panel_UI.transform.DOScale(new Vector3(1, 1, 1), 0.4f).SetEase(Ease.OutQuad);//.OnComplete(fader_Inactive);
        //chatPanel.DOLocalMoveY(0, 1);
    }
    // Start is called before the first frame update
    void Start()
    {
        DisplayPanel();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
