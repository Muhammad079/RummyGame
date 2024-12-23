using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

public class Friends_Panel_Handler : MonoBehaviour
{
    public Button Close_btn;
    // Start is called before the first frame update
    void OnEnable()
    {
        gameObject.SetActive(true);
        transform.DOScale(new Vector3(1, 1, 1), 0.1f).SetEase(Ease.Linear);
        Close_btn.onClick.AddListener(()=> {
            transform.DOScale(new Vector3(0, 0, 0), 0.1f).SetEase(Ease.Linear).OnComplete(()=> {
                gameObject.SetActive(false);
            });
        });
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
