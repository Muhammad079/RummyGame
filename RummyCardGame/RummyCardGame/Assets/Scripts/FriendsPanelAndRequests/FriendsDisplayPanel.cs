using UnityEngine;
using UnityEngine.UI;

public class FriendsDisplayPanel : MonoBehaviour
{
    [SerializeField] private GameObject friendBar = null;
    [SerializeField] private Transform contentParent = null;
    //public int Panel;
     
    public void ShowRecords(int Panel)
    {
        if (Panel == 1)
        {
            for (int n = 0; n < ProfileManager.instance.currentPlayer.friends.Count; n++)
            {
                GameObject bar = new GameObject();
                if (n < contentParent.childCount)
                {
                    bar = contentParent.GetChild(n).gameObject;
                }
                else
                {
                    bar = Instantiate(friendBar, contentParent);
                }
                bar.GetComponent<FriendsBar>().ShowStats(ProfileManager.instance.currentPlayer.friends[n]);
            }
        }
        else if (Panel == 2)
        {
            if (ProfileManager.instance.currentPlayer.facebookLogin)
            {
                if(ProfileManager.instance.currentPlayer.FBfriends_list.Count>0)
                {
                    contentParent.GetChild(0).gameObject.SetActive(false);

                    for (int i = 0; i < ProfileManager.instance.currentPlayer.FBfriends_list.Count; i++)
                    {
                        Instantiate(friendBar, contentParent);
                    }

                }
                else
                {
                    contentParent.GetChild(0).gameObject.SetActive(true);
                    Debug.Log("No Friends Found");
                    contentParent.GetChild(0).GetComponent<Text>().text = "No Friends Found";
                }

                
            }
            else
            {
                contentParent.GetChild(0).gameObject.SetActive(true);
                Debug.Log("Login From Facebook First");
                contentParent.GetChild(0).GetComponent<Text>().text = "Login From Facebook First";
            }
        }
    }
}
