using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class LuckyBoxScreen : MonoBehaviour
{
    [SerializeField] private Button sideButton = null, Info_btn=null, Info_Panel_Close= null;
    bool isShowing = false;
    [SerializeField] public List<BoxBarScript> boxBars = new List<BoxBarScript>();

    [SerializeField] private Sprite[] box_Images = null;

    [SerializeField] private GameObject blackOverlay = null, Info_Panel=null;

    public Button Unlock_Slot;
    public int Unlock_Gems_Req;
    public DOTweenAnimation Slot_Animation;
    float speed = 300;
    float scroll = 5;
    bool scroll_Now = false;
    private void Awake()
    {
        if(ProfileManager.instance.currentPlayer.Box_Slots_unlocked > boxBars.Count)
        {
            for (int i = boxBars.Count; i < ProfileManager.instance.currentPlayer.Box_Slots_unlocked; i++)
            {
                var new_Box = Instantiate(boxBars[1].gameObject, boxBars[0].transform.parent.transform);
                boxBars.Add(new_Box.GetComponent<BoxBarScript>());
            }
        }

    }
    private void Update()
    {
        if (scroll_Now && scroll != 0)
        {
            //scroll_Now = false;
            float contentHeight = boxBars[0].transform.parent.parent.parent.GetComponent<ScrollRect>().content.sizeDelta.y;
            float contentShift = speed * scroll * Time.deltaTime;
            boxBars[0].transform.parent.parent.parent.GetComponent<ScrollRect>().verticalNormalizedPosition -= contentShift / contentHeight;
            Debug.Log(boxBars[0].transform.parent.parent.parent.GetComponent<ScrollRect>().verticalNormalizedPosition);
            if(boxBars[0].transform.parent.parent.parent.GetComponent<ScrollRect>().verticalNormalizedPosition<=0)
            {
                scroll_Now = false;
            }
        }
    }
    IEnumerator delay()
    {
        yield return new WaitForSeconds(0.2f);
        scroll = 5;
        scroll_Now = true;
    }
    //[SerializeField] private Text Info_Panel_text = null;
    void Start()
    {
        Unlock_Slot.onClick.AddListener(() => {
            if (ProfileManager.instance.currentPlayer.gems >= Unlock_Gems_Req)
            {
                ProfileManager.instance.currentPlayer.gems -= Unlock_Gems_Req;
                var new_Box = Instantiate(boxBars[1].gameObject, boxBars[0].transform.parent.transform);
                boxBars.Add(new_Box.GetComponent<BoxBarScript>());
                ProfileManager.instance.currentPlayer.Box_Slots_unlocked = boxBars.Count;
                ProfileManager.instance.SaveUserData();

                //float a = 1;
                //while(a>=0)
                //{
                //    a -= Time.deltaTime;
                //    Debug.Log(a);


                //}
                //boxBars[0].transform.parent.parent.parent.GetComponent<ScrollRect>().verticalNormalizedPosition = Mathf.Lerp(1, 0, 1);


                StartCoroutine(delay());


                //boxBars[0].transform.parent.transform.DOLocalMoveY(1,1.2f);
                //boxBars[0].transform.parent.transform.DOGoto(5000);
                //Slot_Animation.targetType = DOTweenAnimation.TargetType.Transform;
                //Slot_Animation.target = new_Box.transform;
                //Slot_Animation.targetGO = new_Box;
                //Slot_Animation.DOGoto(new_Box.transform.y)
                //Slot_Animation.DOPlayBackwards();
            }
            else
            {
                //Not Enough Coins
                GameManager.instance.Not_Enough_C_Panel.SetActive(true);
                GameManager.instance.Not_Enough_C_Panel.transform.DOScale(new Vector3(1, 1, 1), 0.5f).SetEase(Ease.OutBounce);
                GameManager.instance.Not_Enough_C_Panel.transform.GetChild(0).GetComponent<Text>().text = "Not Enough Gems";
                GameManager.instance.Not_Enough_C_Panel.transform.GetChild(0).GetComponent<Kozykin.MultiLanguageItem>().strItems[0] = "Not Enough Gems";
                GameManager.instance.Not_Enough_C_Panel.transform.GetChild(0).GetComponent<Kozykin.MultiLanguageItem>().strItems[1] = "الأحجار الكريمة غير كافية";
                GameManager.instance.Not_Enough_C_Panel.transform.GetChild(0).GetComponent<Kozykin.MultiLanguageItem>().text = "الأحجار الكريمة غير كافية";
            }
        });
        

        sideButton.onClick.AddListener(OnClick);
        blackOverlay.GetComponent<Button>().onClick.AddListener(OnClick);
        Info_btn.GetComponent<Button>().onClick.AddListener(Info_Panel_display);
    }

    private void Info_Panel_display()
    {
        //Audio_Manager.instance.Sound_Effects_Player.PlayOneShot(Audio_Manager.instance.Sound_Effects[1].Sound_Effect);

        for (int i = 0; i < boxBars.Count; i++)
        {
            boxBars[i].transform.GetChild(1).GetChild(2).gameObject.SetActive(false) ;//.GetComponent<Canvas>().overrideSorting = false;
        }

        Info_Panel.SetActive(true);
        Info_Panel.transform.GetChild(0).transform.DOMoveY(0, 0.5f);
        //Info_Panel_text.text = "Lucky Boxes";
        Info_Panel_Close.onClick.AddListener(() => {

            //Audio_Manager.instance.Sound_Effects_Player.PlayOneShot(Audio_Manager.instance.Sound_Effects[0].Sound_Effect);

            Info_Panel.transform.GetChild(0).transform.DOMoveY(10, 0.5f).OnComplete(() => {
                for (int i = 0; i < boxBars.Count; i++)
                {


                    boxBars[i].transform.GetChild(1).GetChild(2).gameObject.SetActive(true);//.GetComponent<Canvas>().overrideSorting = true;
                }
                Info_Panel.SetActive(false);
            });
        });
    }

    public void OnClick()
    {
        //Audio_Manager.instance.Sound_Effects_Player.PlayOneShot(Audio_Manager.instance.Sound_Effects[1].Sound_Effect);

        for (int i = 0; i < boxBars.Count; i++)
        {
            if (boxBars[i].reward.boxType == BoxType.bronze)
            {
                boxBars[i].transform.GetChild(1).GetChild(2).GetComponent<Image>().sprite = box_Images[0];

            }
            else if (boxBars[i].reward.boxType == BoxType.silver)
            {
                boxBars[i].transform.GetChild(1).GetChild(2).GetComponent<Image>().sprite = box_Images[1];
            }
            else if (boxBars[i].reward.boxType == BoxType.golden)
            {
                boxBars[i].transform.GetChild(1).GetChild(2).GetComponent<Image>().sprite = box_Images[2];
            }
        }
        if (isShowing)
        {
            transform.DOLocalMoveX(-250, 0.5f).OnComplete(() =>
            {

                blackOverlay.SetActive(false);

            });
            GetComponent<Shiny_Effect_Pauser>()?.StartShine();
            isShowing = false;
        }
        else
        {
              transform.DOLocalMove(Vector3.zero, 0.5f).OnComplete(() =>
            {
                blackOverlay.SetActive(true);
            });
            GetComponent<Shiny_Effect_Pauser>()?.Pause();
            isShowing = true;
        }
    }
    public void AddNewBox(BoxRewards rewardInBox)
    {
        foreach (var bb in boxBars)
        {
            if (!bb.isFilled)
            {
                bb.FillBar(rewardInBox, true);
                break;
            }
        }
    }

}
