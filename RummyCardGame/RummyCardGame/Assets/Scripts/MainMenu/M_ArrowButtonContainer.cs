using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
public class M_ArrowButtonContainer : MonoBehaviour
{
    [SerializeField] private GameObject panel = null;
    bool isShowing = false;
    // Start is called before the first frame update
    void Start()
    {
        GetComponent<Button>().onClick.AddListener(OnClick);
    }
    void OnClick()
    {
        //Audio_Manager.instance.Sound_Effects_Player.PlayOneShot(Audio_Manager.instance.Sound_Effects[1].Sound_Effect);

        if (!isShowing)
        {
            panel.transform.DOScale(new Vector3(1,1,1), 0.25f);
            panel.SetActive(true);
            isShowing = true;
        }
        else
        {
            isShowing = false;
            panel.transform.DOScale(new Vector3(0,0,0), 0.25f).OnComplete(Deactivate);
        }
    }
    void Deactivate()
    {
        panel.SetActive(false);
    }
}
