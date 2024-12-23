using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Friends_Table_invite : MonoBehaviour
{
    public Transform Grid_Content;
    public GameObject Friends_display;
    GameObject[] Friends;

    public static bool friend_invite_selected = false;

    private void OnEnable()
    {
        friend_invite_selected = true;
        Friends_display.SetActive(false);
        Friends = new GameObject[ProfileManager.instance.currentPlayer.friends.Count];
        for (int i = 0; i < ProfileManager.instance.currentPlayer.friends.Count; i++)
        {
            if(i==0)
            {
                Friends[i] = Friends_display;
                Friends[i].SetActive(true);
                Friends[i].transform.GetChild(1).GetChild(0).GetComponent<TextMeshProUGUI>().text = ProfileManager.instance.currentPlayer.friends[i];
            }
            else
            {
                Friends[i] = Instantiate(Friends_display, Grid_Content);
                Friends[i].transform.GetChild(1).GetChild(0).GetComponent<TextMeshProUGUI>().text = ProfileManager.instance.currentPlayer.friends[i];
            }
        }
    }
    private void OnDisable()
    {
        friend_invite_selected = false;
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
