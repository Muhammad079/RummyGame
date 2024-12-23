using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Close_RateUS_Anim : MonoBehaviour
{
    public GameObject RateUS,Fader;
    private void Start()
    {
        //DOTweenAnimationType
    }
    public void OnRateUSclick()
    {
        Vector3 scale = new Vector3(0,0,0);
        RateUS.transform.DOScale(scale, 0.5f).SetEase(Ease.OutQuad);
        StartCoroutine(RateUS_setInactive());
    }
    //public void RateUS_setInactive()
    //{
    //    if(RateUS.transform.eulerAngles==new Vector3(0,0,0))
    //    {
    //        RateUS.SetActive(false);
    //    }
    //}
    IEnumerator RateUS_setInactive()
    {
        yield return new WaitForSeconds(0.5f);
        RateUS.SetActive(false);
        Fader.SetActive(false);
    }
}
