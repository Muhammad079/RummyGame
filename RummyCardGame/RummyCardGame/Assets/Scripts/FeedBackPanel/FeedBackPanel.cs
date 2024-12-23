using DG.Tweening;
using System.Net.Mail;
using UnityEngine;
using UnityEngine.UI;

public class FeedBackPanel : MonoBehaviour
{
    [SerializeField] private Toggle b_GameIssue = null;
    [SerializeField] private Toggle b_PaymentIssue = null;
    [SerializeField] private Toggle b_Others = null;
    [SerializeField] private Toggle b_FeedBack = null;
    [SerializeField] private Button reportButton = null;
    [SerializeField] private Button attachment = null;
    [SerializeField] private InputField emailInput = null;
    [SerializeField] private InputField feedBack = null;
    string filePath = "";
    private FeedBackData feedBackData = new FeedBackData();
   public void Show()
    {
      //  this.gameObject.SetActive(true);
        transform.DOScale(Vector3.one, 0.2f).SetEase(Ease.InCubic);
    }
    public void Cross()
    {
        transform.DOScale(Vector3.zero, 0.2f).SetEase(Ease.InCubic).OnComplete(Deactivate);
    }

    private void Deactivate()
    {
        this.gameObject.SetActive(false);
    }

    void Start()
    {

        b_GameIssue.onValueChanged.AddListener(GameIssueClick);
        b_PaymentIssue.onValueChanged.AddListener(PaymentIssueClick);
        b_Others.onValueChanged.AddListener(OtherIssueClick);
        b_FeedBack.onValueChanged.AddListener(FeedBackClick);
        reportButton.onClick.AddListener(PostFeedBack);
        attachment.onClick.AddListener(AttachFile);
    }

    void PostFeedBack()
    {
        if (emailInput.text == "" || !emailInput.text.Contains("@") || !emailInput.text.Contains("."))
        {
            Debug.Log("Invalid Email");
        }
        else
        {
            feedBackData.email = emailInput.text;
            feedBackData.input_feedBack = feedBack.text;
            string json = JsonUtility.ToJson(feedBackData);
            Debug.Log(json);


            //MailMessage mail = new MailMessage();
            //SmtpClient SmtpServer = new SmtpClient("smtp.gmail.com");
            //mail.From = new MailAddress(emailInput.text);
            //mail.To.Add("bb.muhammadbilal@gmail.com");
            //mail.Subject = ProfileManager.instance.currentPlayer.id + "User FeedBack";
            //mail.Body = feedBack.text;
            //if (filePath != "")
            //{
            //    Attachment attachment;
            //    attachment = new Attachment(filePath);
            //    mail.Attachments.Add(attachment);
            //}
            //SmtpServer.Port = 587;
            //SmtpServer.Credentials = new System.Net.NetworkCredential("bb.muhammadbilal@gmail.com", "hellolollipopfunniest") as ICredentialsByHost;
            //SmtpServer.EnableSsl = true;
            //SmtpServer.Send(mail);

            string subject = ProfileManager.instance.currentPlayer.id + "User FeedBack";
            string body = "Game Issue         " + feedBackData.gameIssue + "\n" +
                         "Payment Issue         " + feedBackData.paymentIssue + "\n" +
                         "Feed Back         " + feedBackData.feedback + "\n" +
                         "Other Issue         " + feedBackData.other + "\n" +
                         feedBack.text + "\n" + emailInput.text;
            // subject = MyEscapeURL(subject);
            var mail = new MailMessage("test.bloombigstudio@gmail.com", "test.bloombigstudio@gmail.com");
            if (filePath != "")
                mail.Attachments.Add(new Attachment(filePath));
            mail.Body = body;
            Debug.Log(mail.ToString());
            mail.Priority = MailPriority.High;
            body = MyEscapeURL(body);

            //Open the native default app
            Application.OpenURL("mailto:" + "test.bloombigstudio@gmail.com" + "?subject=" + subject + "&body=" + body);
            //SmtpClient mailServer = new SmtpClient("smtp.gmail.com", 587);
            //mailServer.EnableSsl = true;
            //mailServer.Credentials = new NetworkCredential("", "") as ICredentialsByHost;
            //ServicePointManager.ServerCertificateValidationCallback = delegate (object s, X509Certificate certificate, X509Chain chain, SslPolicyErrors sslPolicyErrors) {
            //    return true;
            //};
            //mailServer.SendAsync(mail, "");
        }
    }
    public string MyEscapeURL(string url)
    {
        return WWW.EscapeURL(url).Replace("+", "%20");
    }
    void GameIssueClick(bool value)
    {
        feedBackData.gameIssue = value;
    }
    void PaymentIssueClick(bool value)
    {
        feedBackData.paymentIssue = value;
    }
    void OtherIssueClick(bool value)
    {
        feedBackData.other = value;
    }
    void FeedBackClick(bool value)
    {
        feedBackData.feedback = value;
    }
    void AttachFile()
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
}
[System.Serializable]
public class FeedBackData
{
    public bool gameIssue = false, paymentIssue = false, other = false, feedback = false;
    public string email = "";
    public string input_feedBack = "";

}
