using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Close_YouLost_Anim : MonoBehaviour
{
    public GameObject YouLost_UI, Fader ;
    public void OnCloseBTNclick()
    {
        YouLost_UI.transform.DOScale(new Vector3(0, 0, 0), .5f).SetEase(Ease.OutQuad).OnComplete(Inactive);
    }
    public void Inactive()
    {
        YouLost_UI.SetActive(false);
        Fader.SetActive(false);
        UnityEngine.SceneManagement.SceneManager.LoadScene("Home");
    }
}
