using Firebase.Database;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
public class ChatMessegeDialogue : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI t_Messege = null;
    [SerializeField] private Text t_Messege_Arabic = null;
    [SerializeField] private Text t_UserId, t_DateTime;
    [SerializeField] private Image profileImage = null, profileFrame = null;
    bool changeFrame = false, changeAvatar = false, changeName = false;
    int frameVal = 0;
    int avatarVal = 0;
    string Name = "";
    public void DisplayMessege(string userID, string displayMessege)
    {
        if(!gameObject.name.Contains("Other"))
        {
            t_UserId.text = ProfileManager.instance.currentPlayer.name;
            profileImage.sprite = ProfileManager.instance.avatarDataFile.avatars[ProfileManager.instance.currentPlayer.selectedAvatarIndex].avatarImage;
            profileFrame.sprite = ProfileManager.instance.framesDataFile.frames[ProfileManager.instance.currentPlayer.selectedFrameIndex].frameImage;

            if (Manager.instance.m_CurrentLanguage == 0)
            {
                t_Messege.gameObject.SetActive(true);
                t_Messege.text = displayMessege;
            }
            else if(Manager.instance.m_CurrentLanguage == 1)
            {
                t_Messege_Arabic.gameObject.SetActive(true);
                t_Messege_Arabic.text = displayMessege;
                t_Messege_Arabic.GetComponent<Kozykin.MultiLanguageItem>().strItems[0] = displayMessege;
                t_Messege_Arabic.GetComponent<Kozykin.MultiLanguageItem>().strItems[1] = displayMessege;
                t_Messege_Arabic.GetComponent<Kozykin.MultiLanguageItem>().text = displayMessege;
            }

            
            t_DateTime.text = System.DateTime.Now.ToString();
        }
        else
        {
            FirebaseDatabase.DefaultInstance.GetReference("Players").Child(userID).Child("name").GetValueAsync().ContinueWith(task =>
            {

                if (task.IsCompleted)
                {

                    DataSnapshot snapshot = task.Result;
                    Name = snapshot.GetRawJsonValue();
                    changeName = true;
                }
                else if (task.IsFaulted)
                {
                    Debug.Log("failed");
                }
            });
            t_Messege.text = displayMessege;
            //t_UserId.text = userID;
            t_DateTime.text = System.DateTime.Now.ToString();
            if (userID == ProfileManager.instance.currentPlayer.name)
            {

                profileImage.sprite = ProfileManager.instance.avatarDataFile.avatars[ProfileManager.instance.currentPlayer.selectedAvatarIndex].avatarImage;
                profileFrame.sprite = ProfileManager.instance.framesDataFile.frames[ProfileManager.instance.currentPlayer.selectedFrameIndex].frameImage;
            }
            else
            {
                profileImage.gameObject.SetActive(false);
                profileFrame.gameObject.SetActive(false);
                Debug.Log("dfg");
                FirebaseDatabase.DefaultInstance.GetReference("Players").Child(userID).Child("selectedFrameIndex").GetValueAsync().ContinueWith(task =>
                {

                    if (task.IsCompleted)
                    {
                        DataSnapshot snapshot = task.Result;
                        frameVal = int.Parse(snapshot.GetRawJsonValue());
                        changeFrame = true;
                    }
                    else if (task.IsFaulted)
                    {
                        Debug.Log("failed");
                    }
                });
                FirebaseDatabase.DefaultInstance.GetReference("Players").Child(userID).Child("selectedAvatarIndex").GetValueAsync().ContinueWith(task =>
                {

                    if (task.IsCompleted)
                    {
                        DataSnapshot snapshot = task.Result;
                        avatarVal = int.Parse(snapshot.GetRawJsonValue());
                        changeAvatar = true;
                    }
                    else if (task.IsFaulted)
                    {
                        Debug.Log("failed");
                    }
                });
                //  StartCoroutine(GetFBPicture(userID));
            }
        }
        
    }
    void ChangeName()
    {
        t_UserId.text = Name;
        Debug.Log("User Name: " + Name);
    }
    void ChangeFrame(int n)
    {
       
        profileFrame.gameObject.SetActive(true);
        profileFrame.sprite = ProfileManager.instance.framesDataFile.frames[n].frameImage;
    }
    void ChangeAvatar(int n)
    {
        profileImage.gameObject.SetActive(true);
        profileImage.sprite = ProfileManager.instance.avatarDataFile.avatars[n].avatarImage;
    }
    public IEnumerator GetFBPicture(string facebookId)
    {
        var www = new WWW("http://graph.facebook.com/" + facebookId + "/picture?width=720&height=720&type=square&redirect=true");
        yield return www;

        if (www.isDone)
        {
            Debug.Log("waiting" + www.bytesDownloaded);
            Texture2D tempPic = new Texture2D(720, 720);
            tempPic = www.texture;
            if (tempPic != null)
                profileImage.sprite = Sprite.Create(tempPic, new Rect(0, 0, tempPic.width, tempPic.height), new Vector2());
            else
                profileFrame.sprite = ProfileManager.instance.framesDataFile.frames[ProfileManager.instance.currentPlayer.selectedFrameIndex].frameImage;
        }
        StopCoroutine(nameof(GetFBPicture));
        //FB.API("https" + "://graph.facebook.com/" + facebookId.ToString() + "/picture?type=medium", HttpMethod.GET, delegate (IGraphResult result)
        //{
        //    Debug.Log("Loaded Image");
        //    currentPlayer.profilePicture = Sprite.Create(result.Texture, new Rect(0, 0, 720, 720), new Vector2(0.5f, 0.5f));
        //});
    }
    private void Update()
    {
        if (changeFrame)
        {
            changeFrame = false;
            ChangeFrame(frameVal);
        }
        if (changeAvatar)
        {
            changeAvatar = false;
            ChangeAvatar(avatarVal);
        }
        if(changeName)
        {
            changeName = false;
            ChangeName();
        }
    }
}
