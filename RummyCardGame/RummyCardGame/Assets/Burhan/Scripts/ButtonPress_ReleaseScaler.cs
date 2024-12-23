using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ButtonPress_ReleaseScaler : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        //InvokeRepeating(scaler,6);

    }
    public void scaler()
    {
        //Audio_Manager.instance.Sound_Effects_Player.PlayOneShot(Audio_Manager.instance.Sound_Effects[0].Sound_Effect);
        EventSystem.current.currentSelectedGameObject.transform.DOScale(new Vector3(0.9f, 0.9f, 0.9f), 0.15f).SetEase(Ease.InOutCubic).OnComplete(scalerReset);
    }
    public void scalerReset()
    {
        EventSystem.current.currentSelectedGameObject.transform.DOScale(new Vector3(1, 1, 1), 0.15f).SetEase(Ease.InOutCubic);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
            if (ButtonClicked())
            {
                scaler();
            }

    }
    bool ButtonClicked()
    {
        
        bool clicked = false;
        if (EventSystem.current)
        {
            if (EventSystem.current.currentSelectedGameObject)
            {
                if (EventSystem.current.currentSelectedGameObject.GetComponent<Button>())
                    if(!EventSystem.current.currentSelectedGameObject.tag.Contains("Don't"))
                    clicked = true;
            }
        } 
        return clicked;
    }
}
