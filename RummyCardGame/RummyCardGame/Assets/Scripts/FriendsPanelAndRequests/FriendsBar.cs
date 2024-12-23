using UnityEngine;
using UnityEngine.UI;

public class FriendsBar : MonoBehaviour
{
    [SerializeField] private Text userId = null, userName = null, barCounter = null;
    PlayerProfile friendPlayer = new PlayerProfile();
    private float timer = 2;
    private bool gotData = false;

    public void ShowStats(string friendID)
    {
        
        DatabaseFunctions.LoadOtherUsers(friendID, PlayerData);
    }
    private void Start()
    {
    }
    void PlayerData(PlayerProfile player)
    {
        
        Debug.Log(player.id);
        friendPlayer = player;
        gotData = true;
        Debug.Log("Displaying Friend Data");

    }
    private void DisplayData()
    {
        userId.text = friendPlayer.id;
        userName.text = friendPlayer.name;
        barCounter.text = (transform.GetSiblingIndex() + 1).ToString();
    }
    private void Update()
    {
        if (gotData)
        {
            timer -= Time.deltaTime;
            if (timer < 0)
            {
                DisplayData();
                gotData = false;
            }
        }
    }
}