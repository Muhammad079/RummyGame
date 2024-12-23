using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LostPanel : MonoBehaviour
{
    [SerializeField] private GameObject bloackOverLay = null;
  public  void PanelShow()
    {
        bloackOverLay.SetActive(true);
        transform.DOScale(new Vector3(01, 01, 01), .5f).SetEase(Ease.OutQuad) ;
    }
   public void PanelHide()
    {
 
       transform.DOScale(new Vector3(0, 0, 0), .5f).SetEase(Ease.OutQuad).OnComplete(Inactive);
    }
    public void Inactive()
    {
        TIme_Countdown.ticking = false;
       this.gameObject.SetActive(false);
        bloackOverLay.SetActive(false);
    }
    private void OnEnable()
    {
        PanelShow();
    }
    public void GameToHome()
    {
        TIme_Countdown.ticking = false;
        UnityEngine.SceneManagement.SceneManager.LoadScene("Home");
    }
}
