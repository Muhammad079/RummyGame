using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class RateUS_PopUp : MonoBehaviour
{
    public GameObject RateUS_UI,Fader;
    // Start is called before the first frame update
    void Start()
    {
        //int Ran_Value= Random.Range(0,10);
        //Debug.Log("Random Value= "+ Ran_Value);
        //if(PlayerPrefs.GetInt("RateUS_Check")==0)
        //{
        //    if (Ran_Value >= 0 && Ran_Value <= 5)
        //    {
        //        PlayerPrefs.SetInt("RateUS_Check", 0);
        //        RateUS_UI.SetActive(true);
        //        Fader.SetActive(true);
        //    }
        //}
        
    }
    public void OnRateUSclick()
    {
        Vector3 scale = new Vector3(0, 0, 0);
        RateUS_UI.transform.DOScale(scale, 0.5f).SetEase(Ease.OutQuad);
        PlayerPrefs.SetInt("RateUS_Check", 1);
        StartCoroutine(nameof(RateUS_setInactive));
    }
    IEnumerator RateUS_setInactive()
    {
        yield return new WaitForSeconds(0.5f);
        RateUS_UI.SetActive(false);
        Fader.SetActive(false);
        StopCoroutine(nameof(RateUS_setInactive));
    }
}
