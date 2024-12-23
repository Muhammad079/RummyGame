using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Trophy_Rewards_Handler_New : SceneLoader
{
    public Scriptable_Trophy_Rewards Scriptable_Trophy_Rewards;
    public GameObject Rewards;
    public Transform Content, Content_Update_Values;
    int count;
    public Button Back_btn;

    GameObject[] Rewards_All;

    private void OnEnable()
    {
        transform.DOScale(new Vector3(1,1,1), 0.2f);
    }
    // Start is called before the first frame update
    void Start()
    {
        count = 0;

        Back_btn.onClick.AddListener(()=> {
            transform.DOScale(new Vector3(0, 0, 0), 0.2f).OnComplete(()=> {
                //Loading_Screen = GameObject.Find("Loading_Screen");
                //Loading_animation = Loading_Screen.transform.GetChild(0).GetComponent<Text>();
                StartCoroutine(OnClick());
                //SceneManager.LoadScene("Home");
            });
        });

        for (int i = 0; i < Scriptable_Trophy_Rewards.Portion_Level.Count; i++)
        {
            for (int k = 0; k < Scriptable_Trophy_Rewards.Portion_Level[i].Portion_Rewards.Count; k++)
            {
                if (i == 0 && k == 0)
                {
                    Rewards.SetActive(true);
                    Rewards.GetComponent<Trophy_Rewards_Container_New>().Reward = Scriptable_Trophy_Rewards.Portion_Level[i].Portion_Rewards[k].rewards;
                    Rewards.GetComponent<Trophy_Rewards_Container_New>().Reward_Image = Scriptable_Trophy_Rewards.Portion_Level[i].Portion_Rewards[k].Reward_Images;
                    Rewards.GetComponent<Trophy_Rewards_Container_New>().Reward_Trophies= Scriptable_Trophy_Rewards.Portion_Level[i].Portion_Rewards[k].Trophies_Required.ToString();
                    Rewards.GetComponent<Trophy_Rewards_Container_New>().id = count;
                }
                else
                {
                    var Reward = Instantiate(Rewards, Content);
                    Reward.GetComponent<Trophy_Rewards_Container_New>().Reward = Scriptable_Trophy_Rewards.Portion_Level[i].Portion_Rewards[k].rewards;
                    Reward.GetComponent<Trophy_Rewards_Container_New>().Reward_Image = Scriptable_Trophy_Rewards.Portion_Level[i].Portion_Rewards[k].Reward_Images;
                    Reward.GetComponent<Trophy_Rewards_Container_New>().Reward_Trophies = Scriptable_Trophy_Rewards.Portion_Level[i].Portion_Rewards[k].Trophies_Required.ToString();
                    Reward.GetComponent<Trophy_Rewards_Container_New>().id = count;
                }
                count++;
            }
        }
        Rewards_All = new GameObject[Content.childCount];

        Invoke(nameof(refresh), 0.2f);

    }

    private void refresh()
    {
        for (int i = 0; i < Rewards_All.Length; i++)
        {
            Rewards_All[i] = Content.GetChild(i).gameObject;
        }

        int My_trophies = ProfileManager.instance.currentPlayer.trophies;
        for (int i = 0; i < Rewards_All.Length; i++)
        {
            if (int.Parse(Rewards_All[i].GetComponent<Trophy_Rewards_Container_New>().Reward_Trophies) > My_trophies)
            {
                float result = float.Parse(My_trophies.ToString()) / float.Parse(Rewards_All[i].GetComponent<Trophy_Rewards_Container_New>().Reward_Trophies);
                Rewards_All[i].GetComponent<Trophy_Rewards_Container_New>().Filler.fillAmount = result;
                break;
            }
            else
            {
                Rewards_All[i].GetComponent<Trophy_Rewards_Container_New>().Filler.fillAmount = 1;
                //My_trophies -= int.Parse(Rewards_All[i].GetComponent<Trophy_Rewards_Container_New>().Reward_Trophies);
            }

        }


        Content.GetComponent<RectTransform>().position = Content_Update_Values.GetComponent<RectTransform>().position;


            
    }

    // Update is called once per frame
    public override void Update()
    {
        base.Update();
    }
}
