using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Lucky_Spin_UI_Popup : MonoBehaviour
{
    public GameObject Lucky_Spin_UI;
    
      
   public void DisplayPanel()
    {
        Lucky_Spin_UI.transform.DOLocalMoveY(-5, 0.5f).SetEase(Ease.OutQuad);//.OnComplete(fader_Inactive);
    }
    public void Hide()
    {
        Lucky_Spin_UI.transform.DOMoveY(1000, 0.4f).SetEase(Ease.OutQuad);
    }
    private void Start()
    {
        DisplayPanel();
    }
}
