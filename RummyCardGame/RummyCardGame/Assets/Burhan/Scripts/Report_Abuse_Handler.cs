using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;
using System;
using System.Net.Mail;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;

public class Report_Abuse_Handler : MonoBehaviour
{
    ReportData reportData = new ReportData();

    public InputField Complaint;
    public Button Close_btn, Attachment_btn, Report_btn;
    public Text ID, Complaint_Characters;
    public Image profile_Image;
    public GameObject Popup_msg_Panel;

    public Toggle[] Option_Boxes;
    
    string filePath = "";

    // Start is called before the first frame update

    private void OnEnable()
    {
        if(SceneManager.GetActiveScene().name.Contains("GamePlay"))
        {
            transform.DOScale(new Vector3(2, 2, 2), 0.2f);
        }
        else
        {
            transform.DOScale(new Vector3(1, 1, 1), 0.2f);
        }
        //reportData.Other_Pid = GameManager.instance.userProfileHandler.displayingProfile.id;
        reportData.Other_Pid = GameManager.instance.userProfileHandler.ID.text;
        ID.text = "ID: " + reportData.Other_Pid;
        profile_Image.sprite = GameManager.instance.userProfileHandler.avatarImage.sprite;
        Report_btn.interactable = true;
    }
    void Start()
    {
        
        
        Complaint.characterLimit = 100;

        Close_btn.onClick.AddListener(()=> {
            transform.DOScale(new Vector3(0, 0, 0), 0.2f).OnComplete(()=> {
                gameObject.SetActive(false);



            });
        });
        Complaint.onValueChanged.AddListener(delegate { Character_Updating(); });
        Option_Boxes[0].onValueChanged.AddListener(R1Option1);
        Option_Boxes[1].onValueChanged.AddListener(R1Option2);
        Option_Boxes[2].onValueChanged.AddListener(R2Option1);
        Option_Boxes[3].onValueChanged.AddListener(R2Option2);
        Option_Boxes[4].onValueChanged.AddListener(R3Option1);
        Option_Boxes[5].onValueChanged.AddListener(R3Option2);

        Report_btn.onClick.AddListener(Report_Upload);
        Attachment_btn.onClick.AddListener(Attachments);
    }
    [Serializable]
    public class ReportData
    {
        public bool R1Option1 = false, R1Option2 = false, R2Option1 = false, R2Option2 = false, R3Option1 = false, R3Option2 = false;
        public string input_feedBack = "", Other_Pid= "";

    }
    private void Character_Updating()
    {
        Complaint_Characters.text = (Complaint.characterLimit - Complaint.text.Length).ToString() + "/100";
    }
    void Report_Upload()
    {
        reportData.input_feedBack = JsonUtility.ToJson(Complaint.text);
        string Subject = ProfileManager.instance.currentPlayer.id + "Reported " + reportData.Other_Pid;
        string Options =    "Pornography: " + reportData.R1Option1+ "\n" +
                            "Insult: " + reportData.R1Option2 + "\n" +
                            "Bad Advertising: " + reportData.R2Option1 + "\n" +
                            "Others: " + reportData.R2Option2 + "\n" +
                            //"R3Option1: " + reportData.R3Option1 + "\n" +
                            //"R3Option2: " + reportData.R3Option2 + "\n" +
                            "Complaint: " + reportData.input_feedBack;
        


        var mail = new MailMessage("test.bloombigstudio@gmail.com", "test.bloombigstudio@gmail.com");
        if (filePath != "")
            mail.Attachments.Add(new Attachment(filePath));
        mail.Body = Options;
        Debug.Log(mail.ToString());
        mail.Priority = MailPriority.High;
        Options = MyEscapeURL(Options);
        Application.OpenURL("mailto:" + "test.bloombigstudio@gmail.com" + "?subject=" + Subject + "&body=" + Options);
        
        GameManager.instance.userProfileHandler.Reported_users.Add(reportData.Other_Pid);
        Report_btn.interactable = false;
        //StartCoroutine(nameof(Toast));

    }
    IEnumerator Toast()
    {
        Popup_msg_Panel.transform.GetChild(0).GetComponent<Text>().text = "Player reported";
        Popup_msg_Panel.transform.GetChild(0).GetComponent<Kozykin.MultiLanguageItem>().strItems[0] = "Player reported";
        Popup_msg_Panel.transform.GetChild(0).GetComponent<Kozykin.MultiLanguageItem>().strItems[1] = "أبلغ اللاعب";
        Popup_msg_Panel.transform.GetChild(0).GetComponent<Kozykin.MultiLanguageItem>().text = "أبلغ اللاعب";

        Popup_msg_Panel.SetActive(true);
        Popup_msg_Panel.transform.DOLocalMoveY(0, 0.3f).SetEase(Ease.OutSine);
        yield return new WaitForSeconds(3);
        Popup_msg_Panel.transform.DOLocalMoveY(-334, 0.3f).SetEase(Ease.InSine).OnComplete(() =>{
            Popup_msg_Panel.SetActive(false);
        });
        
    }
    public string MyEscapeURL(string url)
    {
        return UnityWebRequest.EscapeURL(url).Replace("+", "%20");
    }
    void Attachments()
    {
        if (!NativeGallery.IsMediaPickerBusy())
        {
            NativeGallery.GetImagesFromGallery(ImageGetCallBack, "report", "image/*");
        }
    }
    private void ImageGetCallBack(string[] paths)
    {
        filePath = paths[0];
    }
    void R1Option1(bool val)
    {
        reportData.R1Option1 = val;
    }
    void R1Option2(bool val)
    {
        reportData.R1Option2 = val;
    }
    void R2Option1(bool val)
    {
        reportData.R2Option1 = val;
    }
    void R2Option2(bool val)
    {
        reportData.R2Option2 = val;
    }
    void R3Option1(bool val)
    {
        reportData.R3Option1 = val;
    }
    void R3Option2(bool val)
    {
        reportData.R3Option2 = val;
    }
}
