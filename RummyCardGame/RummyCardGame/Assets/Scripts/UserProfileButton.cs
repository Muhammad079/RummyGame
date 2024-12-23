using UnityEngine;
using UnityEngine.UI;
public class UserProfileButton : MonoBehaviour
{
    [SerializeField] private Text t_userID = null;
    bool gotData = false;
    PlayerProfile player = null;
    void Start()
    {
        GetComponent<Button>().onClick.AddListener(OnClick);
    }

    void OnClick()
    {
        Debug.Log(t_userID.text);
        //if (t_userID.text != "bot")
            DatabaseFunctions.LoadOtherUsers(t_userID.text, Display);
        //else
        //    Display(new PlayerProfile());
    }
    void Display(PlayerProfile t)
    {
        player = t;
        Debug.Log("Fetched");
        gotData = true;
    }
    private void Update()
    {
        if (gotData)
        {
            gotData = false;
            GameManager.instance.userProfileHandler.DisplayProfileStats(player);
        }
    }
}
