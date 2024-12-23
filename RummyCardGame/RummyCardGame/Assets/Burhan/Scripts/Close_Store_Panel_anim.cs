using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Close_Store_Panel_anim : MonoBehaviour
{
    public GameObject Store_Panel_UI;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void OnCloseBTNclick()
    {
        Store_Panel_UI.transform.DOScale(new Vector3(0, 0, 0), 0.4f).SetEase(Ease.OutQuad).OnComplete(fader_Inactive);
    }
    public void fader_Inactive()
    {
        //fader.SetActive(false);
        Store_Panel_UI.SetActive(false);
        //Store_Panel_UI.transform.localScale = new Vector3(1,1,1); 
    }
}
