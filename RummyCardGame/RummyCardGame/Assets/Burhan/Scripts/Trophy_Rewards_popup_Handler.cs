using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Trophy_Rewards_popup_Handler : MonoBehaviour
{
    public Image Selected_Reward;
    public Text Selected_Reward_Name;
    public GameObject Left, Mid, Right;
    public Sprite[] Stored_Rewards_Images;
    public Button close_btn, Collect_btn;

    //public List<int> Reward_ID_container = new List<int>();

    // Start is called before the first frame update
    void Start()
    {
        close_btn.onClick.AddListener(()=> {
            transform.DOScale(Vector3.zero, 0.5f).SetEase(Ease.InFlash).OnComplete(()=> {
                gameObject.SetActive(false);

            });
        });

        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Getting_Setting_Data(RewardItems reward, Sprite Reward_Image, string Req_trophies, int Id)
    {
        gameObject.SetActive(true);
        transform.DOScale(Vector3.one, 0.5f).SetEase(Ease.OutBack);
        Collect_btn.interactable = true;


        Selected_Reward.sprite = Reward_Image;
        string Name = reward.reward.ToString();
        if(Name.Contains("box"))
        {
            Selected_Reward_Name.text = reward.boxReward.boxTitle;
        }
        else
        {
            Selected_Reward_Name.text = reward.reward.ToString();
        }

        if(reward.boxReward.boxItems.Count==1)
        {
            Left.SetActive(false);
            Right.SetActive(false);
            Mid.SetActive(true);

            Mid.transform.GetChild(1).GetComponent<Text>().text = reward.boxReward.boxItems[0].reward.ToString();

            if(reward.boxReward.boxItems[0].reward.ToString().Contains("coins"))
            {
                Mid.transform.GetChild(0).GetChild(0).GetComponent<Image>().sprite = Stored_Rewards_Images[0];
                Mid.transform.GetChild(0).GetChild(1).GetComponent<Text>().text = reward.boxReward.boxItems[0].quantity.ToString();
            }
            else if (reward.boxReward.boxItems[0].reward.ToString().Contains("cardCollection"))
            {
                Mid.transform.GetChild(0).GetChild(0).GetComponent<Image>().sprite = Stored_Rewards_Images[1];
                Mid.transform.GetChild(0).GetChild(1).GetComponent<Text>().text = reward.boxReward.boxItems[0].quantity.ToString();
            }
            else if (reward.boxReward.boxItems[0].reward.ToString().Contains("gems"))
            {
                Mid.transform.GetChild(0).GetChild(0).GetComponent<Image>().sprite = Stored_Rewards_Images[2];
                Mid.transform.GetChild(0).GetChild(1).GetComponent<Text>().text = reward.boxReward.boxItems[0].quantity.ToString();
            }
        }
        else if (reward.boxReward.boxItems.Count == 2)
        {
            Left.SetActive(true);
            Right.SetActive(true);
            Mid.SetActive(false);

            Left.transform.GetChild(1).GetComponent<Text>().text = reward.boxReward.boxItems[0].reward.ToString();

            if (reward.boxReward.boxItems[0].reward.ToString().Contains("coins"))
            {
                Left.transform.GetChild(0).GetChild(0).GetComponent<Image>().sprite = Stored_Rewards_Images[0];
                Left.transform.GetChild(0).GetChild(1).GetComponent<Text>().text = reward.boxReward.boxItems[0].quantity.ToString();
            }
            else if (reward.boxReward.boxItems[0].reward.ToString().Contains("cardCollection"))
            {
                Left.transform.GetChild(0).GetChild(0).GetComponent<Image>().sprite = Stored_Rewards_Images[1];
                Left.transform.GetChild(0).GetChild(1).GetComponent<Text>().text = reward.boxReward.boxItems[0].quantity.ToString();
            }
            else if (reward.boxReward.boxItems[0].reward.ToString().Contains("gems"))
            {
                Left.transform.GetChild(0).GetChild(0).GetComponent<Image>().sprite = Stored_Rewards_Images[2];
                Left.transform.GetChild(0).GetChild(1).GetComponent<Text>().text = reward.boxReward.boxItems[0].quantity.ToString();
            }

            Right.transform.GetChild(1).GetComponent<Text>().text = reward.boxReward.boxItems[1].reward.ToString();

            if (reward.boxReward.boxItems[1].reward.ToString().Contains("coins"))
            {
                Right.transform.GetChild(0).GetChild(0).GetComponent<Image>().sprite = Stored_Rewards_Images[0];
                Right.transform.GetChild(0).GetChild(1).GetComponent<Text>().text = reward.boxReward.boxItems[1].quantity.ToString();
            }
            else if (reward.boxReward.boxItems[1].reward.ToString().Contains("cardCollection"))
            {
                Right.transform.GetChild(0).GetChild(0).GetComponent<Image>().sprite = Stored_Rewards_Images[1];
                Right.transform.GetChild(0).GetChild(1).GetComponent<Text>().text = reward.boxReward.boxItems[1].quantity.ToString();
            }
            else if (reward.boxReward.boxItems[1].reward.ToString().Contains("gems"))
            {
                Right.transform.GetChild(0).GetChild(0).GetComponent<Image>().sprite = Stored_Rewards_Images[2];
                Right.transform.GetChild(0).GetChild(1).GetComponent<Text>().text = reward.boxReward.boxItems[1].quantity.ToString();
            }
        }
        else if (reward.boxReward.boxItems.Count == 3)
        {
            Left.SetActive(true);
            Right.SetActive(true);
            Mid.SetActive(true);

            Left.transform.GetChild(1).GetComponent<Text>().text = reward.boxReward.boxItems[0].reward.ToString();

            if (reward.boxReward.boxItems[0].reward.ToString().Contains("coins"))
            {
                Left.transform.GetChild(0).GetChild(0).GetComponent<Image>().sprite = Stored_Rewards_Images[0];
                Left.transform.GetChild(0).GetChild(1).GetComponent<Text>().text = reward.boxReward.boxItems[0].quantity.ToString();
            }
            else if (reward.boxReward.boxItems[0].reward.ToString().Contains("cardCollection"))
            {
                Left.transform.GetChild(0).GetChild(0).GetComponent<Image>().sprite = Stored_Rewards_Images[1];
                Left.transform.GetChild(0).GetChild(1).GetComponent<Text>().text = reward.boxReward.boxItems[0].quantity.ToString();
            }
            else if (reward.boxReward.boxItems[0].reward.ToString().Contains("gems"))
            {
                Left.transform.GetChild(0).GetChild(0).GetComponent<Image>().sprite = Stored_Rewards_Images[2];
                Left.transform.GetChild(0).GetChild(1).GetComponent<Text>().text = reward.boxReward.boxItems[0].quantity.ToString();
            }

            Mid.transform.GetChild(1).GetComponent<Text>().text = reward.boxReward.boxItems[1].reward.ToString();

            if (reward.boxReward.boxItems[1].reward.ToString().Contains("coins"))
            {
                Mid.transform.GetChild(0).GetChild(0).GetComponent<Image>().sprite = Stored_Rewards_Images[0];
                Mid.transform.GetChild(0).GetChild(1).GetComponent<Text>().text = reward.boxReward.boxItems[1].quantity.ToString();
            }
            else if (reward.boxReward.boxItems[1].reward.ToString().Contains("cardCollection"))
            {
                Mid.transform.GetChild(0).GetChild(0).GetComponent<Image>().sprite = Stored_Rewards_Images[1];
                Mid.transform.GetChild(0).GetChild(1).GetComponent<Text>().text = reward.boxReward.boxItems[1].quantity.ToString();
            }
            else if (reward.boxReward.boxItems[1].reward.ToString().Contains("gems"))
            {
                Mid.transform.GetChild(0).GetChild(0).GetComponent<Image>().sprite = Stored_Rewards_Images[2];
                Mid.transform.GetChild(0).GetChild(1).GetComponent<Text>().text = reward.boxReward.boxItems[1].quantity.ToString();
            }

            Right.transform.GetChild(1).GetComponent<Text>().text = reward.boxReward.boxItems[2].reward.ToString();

            if (reward.boxReward.boxItems[2].reward.ToString().Contains("coins"))
            {
                Right.transform.GetChild(0).GetChild(0).GetComponent<Image>().sprite = Stored_Rewards_Images[0];
                Right.transform.GetChild(0).GetChild(1).GetComponent<Text>().text = reward.boxReward.boxItems[2].quantity.ToString();
            }
            else if (reward.boxReward.boxItems[2].reward.ToString().Contains("cardCollection"))
            {
                Right.transform.GetChild(0).GetChild(0).GetComponent<Image>().sprite = Stored_Rewards_Images[1];
                Right.transform.GetChild(0).GetChild(1).GetComponent<Text>().text = reward.boxReward.boxItems[2].quantity.ToString();
            }
            else if (reward.boxReward.boxItems[2].reward.ToString().Contains("gems"))
            {
                Right.transform.GetChild(0).GetChild(0).GetComponent<Image>().sprite = Stored_Rewards_Images[2];
                Right.transform.GetChild(0).GetChild(1).GetComponent<Text>().text = reward.boxReward.boxItems[2].quantity.ToString();
            }
        }
        else
        {
            Left.SetActive(false);
            Right.SetActive(false);
            Mid.SetActive(true);

            Mid.transform.GetChild(1).GetComponent<Text>().text = reward.reward.ToString();
            Mid.transform.GetChild(0).GetChild(0).GetComponent<Image>().sprite = Reward_Image;
            Mid.transform.GetChild(0).GetChild(1).GetComponent<Text>().text = reward.quantity.ToString();

        }
        if (int.Parse(Req_trophies) > ProfileManager.instance.currentPlayer.trophies)
        {
            Collect_btn.gameObject.SetActive(false);
        }
        else
        {
            Collect_btn.gameObject.SetActive(true);

            if (ProfileManager.instance.currentPlayer.Reward_ID_container.Contains(Id))
            {
                Collect_btn.interactable = false;
            }
            else
            {
                Collect_btn.interactable = true;
                Collect_btn.onClick.RemoveAllListeners();
                Collect_btn.onClick.AddListener(() =>
                {
                    foreach (var a in reward.boxReward.boxItems)
                    {
                        ProfileManager.instance.GetReward(a.reward, a.quantity);
                    }
                    ProfileManager.instance.currentPlayer.Reward_ID_container.Add(Id);
                    Collect_btn.interactable = false;
                    ProfileManager.instance.SaveUserData();
                });
            }

        }
        
    }
}
