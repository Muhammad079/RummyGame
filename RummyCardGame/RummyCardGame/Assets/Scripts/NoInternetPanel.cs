using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NoInternetPanel : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        GameManager.instance.No_Internet_Panel = this.gameObject;
    
       transform.Find("Close_btn").gameObject.GetComponent<Button>().onClick.AddListener(() =>
        {
            transform.DOScale(new Vector3(0, 0, 0), 0.5f).SetEase(Ease.OutBounce).OnComplete(() =>
            {
              this.gameObject  .SetActive(false);
            GameManager.instance.    internet_Check = true;
            });
        });
    }


}
