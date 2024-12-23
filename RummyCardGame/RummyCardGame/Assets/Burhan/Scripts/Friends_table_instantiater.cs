using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Friends_table_instantiater : MonoBehaviour
{
    public GameObject Table_prefab;
    public Slider Players;
    float players_count;

    public static Friends_table_instantiater instance;

    //private void Awake()
    //{
    //    if (instance == null)
    //    {
    //        instance = this;
    //        DontDestroyOnLoad(gameObject);
    //    }
    //    else
    //    {
    //        Destroy(gameObject);
    //    }
    //}
    private void onValueChanged_func(int value)
    {
        if (value > 0)
        {
            transform.DOLocalMove(new Vector3(0, 150, 0), 0.5f);
        }

        for (int i = 0; i < value; i++)
        {
            GameObject Table = Instantiate(Table_prefab, transform);
            Table.GetComponent<Table_Chat_Joining>().data = ProfileManager.instance.currentPlayer.Tables_VIPFriends_invited[i];
            StartCoroutine(Duration_to_Live(Table));
        }
    }
    IEnumerator Duration_to_Live(GameObject Table)
    {
        yield return new WaitForSeconds(8);
        transform.DOLocalMove(new Vector3(0, 300, 0), 0.5f);
        yield return new WaitForSeconds(2);
        Destroy(Table);
    }


    // Start is called before the first frame update
    void Start()
    {
        players_count = 0;
        //ProfileManager.instance.currentPlayer.Tables_VIP_invited.Clear();
        Players.onValueChanged.AddListener(delegate { onValueChanged_func(ProfileManager.instance.currentPlayer.Tables_VIPFriends_invited.Count); });
    }

    // Update is called once per frame
    void Update()
    {
        players_count = ProfileManager.instance.currentPlayer.Tables_VIPFriends_invited.Count;
        if (players_count > 0)
        {
            players_count = players_count / 100;

        }
        Players.value = players_count;
    }
}
