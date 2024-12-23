using Firebase.Database;
using System.Threading.Tasks;
using UnityEngine;

public class GiftSendingManager : MonoBehaviour
{
    TaskScheduler taskScheduler;
    void Start()
    {
        DontDestroyOnLoad(this.gameObject);
        taskScheduler = TaskScheduler.FromCurrentSynchronizationContext();
        FirebaseDatabase.DefaultInstance.GetReference("GiftSendingInfo").ValueChanged += HandleValueChanged;
      }
     void HandleValueChanged(object sender, ValueChangedEventArgs args)
    {
        Debug.LogWarning("log");
        if (args.DatabaseError != null)
        {
            Debug.LogError(args.DatabaseError.Message);
            return;
        }
         FirebaseDatabase.DefaultInstance.GetReference("GiftSendingInfo").GetValueAsync().ContinueWith(task =>
        {
            if (task.IsCompleted)
            {
                DataSnapshot snapshot = task.Result;
                if (MainMenuStats.instance)
                {
                    Debug.Log("Sent");
                    GiftSendingMessege a = JsonUtility.FromJson<GiftSendingMessege>(snapshot.GetRawJsonValue());

                    MainMenuStats.instance.DisplayGiftMessege(a);
                    MainMenuStats.instance.pendingGiftMsg.Add(a);

                    //GameManager.instance.userProfileHandler.DisplayGiftMessege(a);
                    //GameManager.instance.userProfileHandler.pendingGiftMsg.Add(a);

                }
            }
        }, taskScheduler);

    }
}
[System.Serializable]
public class GiftSendingMessege
{
    public string senderId = "";
    public string recieverId = "";
    public GiftItems giftItems;
}
