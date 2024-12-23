using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class Chat_btn_Handler : MonoBehaviour
{
    public GameObject Chat_UI_Panel, Chat_UI_Info_Panel;
    public Text Chat_UI_Info_Text;
    public Button Chat_UI_Info_Close;

    // Start is called before the first frame update
    void Start()
    {
        GetComponent<Button>().onClick.AddListener(()=> {
            //if (/*ProfileManager.instance.currentPlayer.isVip &&*/ ProfileManager.instance.currentPlayer.level >= 5)
            //{
            Chat_UI_Panel.SetActive(true);
            //}
            //else
            //{
            //    Chat_UI_Info_Panel.SetActive(true);
            //    Chat_UI_Info_Panel.transform.GetChild(1).transform.DOMoveY(0, 0.2f);
            //    Chat_UI_Info_Text.text = "to unlock the chat you have to reach Level  5";
            //    Chat_UI_Info_Text.GetComponent<Kozykin.MultiLanguageItem>().strItems[0] = "to unlock the chat you have to reach Level 5";
            //    Chat_UI_Info_Text.GetComponent<Kozykin.MultiLanguageItem>().strItems[1] = "لفتح الدردشة ، يجب أن تصل إلى المستوى 5";
            //    Chat_UI_Info_Text.GetComponent<Kozykin.MultiLanguageItem>().text = "لفتح الدردشة ، يجب أن تصل إلى المستوى 5";
            //}
            //else if(!ProfileManager.instance.currentPlayer.isVip)
            //{
            //    Chat_UI_Info_Panel.SetActive(true);
            //    Chat_UI_Info_Panel.transform.GetChild(1).transform.DOMoveY(0, 0.2f);

            //    Chat_UI_Info_Text.text = "to unlock the chat you have to" +
            //    "  become  a  vip  member  and  have  more  privileges  (  purchase  vip  7  days  icon OR  purchase vip 30 days icon )  ";
            //}
            //else if(ProfileManager.instance.currentPlayer.isVip && ProfileManager.instance.currentPlayer.level < 5)
            //{
            //    Chat_UI_Info_Panel.SetActive(true);
            //    Chat_UI_Info_Panel.transform.GetChild(1).transform.DOMoveY(0, 0.2f);
            //    Chat_UI_Info_Text.text = "to unlock the chat you have to reach Level  5";
            //}
        });
        Chat_UI_Info_Close.onClick.AddListener(() => {
            Chat_UI_Info_Panel.transform.GetChild(1).transform.DOMoveY(10, 0.2f).OnComplete(()=> {
                Chat_UI_Info_Panel.SetActive(false);
            });
        });


        }

    // Update is called once per frame
    void Update()
    {
        
    }
}
