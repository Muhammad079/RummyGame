using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Trophy_Rewards_Handler : MonoBehaviour
{
    [SerializeField] private Scriptable_Trophy_Rewards Rewards_By_Portion = null;
    [SerializeField] private Transform Portion_Content_Holder = null;//, Rewards_Content_Holder = null;
    public Button close_btn;
    public Sprite Last_Reward_Holder;

    int count;

    // Start is called before the first frame update
    void Start()
    {
        transform.DOScale(Vector3.one, 0.2f).SetEase(Ease.OutSine);
        close_btn.onClick.AddListener(()=> {
            transform.DOScale(Vector3.zero, 0.2f).SetEase(Ease.OutSine).OnComplete(()=> {
                SceneManager.LoadScene("Home");
            });
           
        });


        count = 0;
        Transform[] Portions = new Transform[Rewards_By_Portion.Portion_Level.Count];
        List<PerPortionRewardsHolder> perPortionRewards = new List<PerPortionRewardsHolder>();

        #region Portions_instantiater
        for (int i=0;i<Rewards_By_Portion.Portion_Level.Count;i++)
        {
            if(i==0)
            {
                Portion_Content_Holder.GetChild(0).gameObject.SetActive(true);
                Portions[i] = Portion_Content_Holder.GetChild(0).transform;
            }
            else
            {
                Portions[i] = Instantiate(Portion_Content_Holder.GetChild(0),Portion_Content_Holder);
            }
            perPortionRewards.Add(new PerPortionRewardsHolder());
        }
        #endregion

        #region Rewards_Instantiater_Per_Portion
        for (int k = 0; k < Rewards_By_Portion.Portion_Level.Count; k++)
        {
            perPortionRewards[k].Rewards = new Transform[Rewards_By_Portion.Portion_Level[k].Portion_Rewards.Count];
            for (int i = 0; i < Rewards_By_Portion.Portion_Level[k].Portion_Rewards.Count; i++)
            {
                
                if (i == 0)
                {
                    Portions[k].GetChild(0).GetChild(0).GetChild(0).GetChild(0).gameObject.SetActive(true);

                    perPortionRewards[k].Rewards[i] = Portions[k].GetChild(0).GetChild(0).GetChild(0).GetChild(0).transform;
                    perPortionRewards[k].Rewards[i].GetComponent<Trophy_Rewards_Container>().Reward = Rewards_By_Portion.Portion_Level[k].Portion_Rewards[i].rewards;
                    perPortionRewards[k].Rewards[i].GetComponent<Trophy_Rewards_Container>().Reward_Image = Rewards_By_Portion.Portion_Level[k].Portion_Rewards[i].Reward_Images;
                    perPortionRewards[k].Rewards[i].GetComponent<Trophy_Rewards_Container>().Reward_Trophies = Rewards_By_Portion.Portion_Level[k].Portion_Rewards[i].Trophies_Required.ToString();
                    perPortionRewards[k].Rewards[i].GetComponent<Trophy_Rewards_Container>().id = count;

                }

                else if (i == Rewards_By_Portion.Portion_Level[k].Portion_Rewards.Count-1)
                {
                    //GameObject Last_Holder = new GameObject();
                    //Last_Holder.AddComponent<Image>().sprite = Last_Reward_Holder;

                    perPortionRewards[k].Rewards[i] = Instantiate(Portions[k].GetChild(0).GetChild(0).GetChild(0).GetChild(0), Portions[k].GetChild(0).GetChild(0).GetChild(0));

                    perPortionRewards[k].Rewards[i].GetChild(0).GetComponent<Image>().sprite = Last_Reward_Holder;
                    perPortionRewards[k].Rewards[i].GetChild(0).GetComponent<RectTransform>().anchorMin = new Vector2(0.5f, 0.5f);
                    perPortionRewards[k].Rewards[i].GetChild(0).GetComponent<RectTransform>().anchorMax = new Vector2(0.5f, 0.5f);
                    perPortionRewards[k].Rewards[i].GetChild(0).GetComponent<RectTransform>().SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal,130);
                    perPortionRewards[k].Rewards[i].GetChild(0).GetComponent<RectTransform>().SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, 130);
                    perPortionRewards[k].Rewards[i].GetChild(0).GetComponent<RectTransform>().anchoredPosition = new Vector3(0, -3,0);
                    perPortionRewards[k].Rewards[i].GetChild(0).GetComponent<Image>().preserveAspect = false;
                    perPortionRewards[k].Rewards[i].GetChild(1).gameObject.SetActive(false);

                    perPortionRewards[k].Rewards[i].GetComponent<Trophy_Rewards_Container>().Reward = Rewards_By_Portion.Portion_Level[k].Portion_Rewards[i].rewards;
                    perPortionRewards[k].Rewards[i].GetComponent<Trophy_Rewards_Container>().Reward_Image = Rewards_By_Portion.Portion_Level[k].Portion_Rewards[i].Reward_Images;
                    perPortionRewards[k].Rewards[i].GetComponent<Trophy_Rewards_Container>().Reward_Trophies = Rewards_By_Portion.Portion_Level[k].Portion_Rewards[i].Trophies_Required.ToString();
                    perPortionRewards[k].Rewards[i].GetComponent<Trophy_Rewards_Container>().id = count;
                }

                else
                {
                    perPortionRewards[k].Rewards[i] = Instantiate(Portions[k].GetChild(0).GetChild(0).GetChild(0).GetChild(0), Portions[k].GetChild(0).GetChild(0).GetChild(0));
                    perPortionRewards[k].Rewards[i].GetComponent<Trophy_Rewards_Container>().Reward = Rewards_By_Portion.Portion_Level[k].Portion_Rewards[i].rewards;
                    perPortionRewards[k].Rewards[i].GetComponent<Trophy_Rewards_Container>().Reward_Image = Rewards_By_Portion.Portion_Level[k].Portion_Rewards[i].Reward_Images;
                    perPortionRewards[k].Rewards[i].GetComponent<Trophy_Rewards_Container>().Reward_Trophies = Rewards_By_Portion.Portion_Level[k].Portion_Rewards[i].Trophies_Required.ToString();
                    perPortionRewards[k].Rewards[i].GetComponent<Trophy_Rewards_Container>().id = count;
                }
                Debug.Log("Inner I value: " + i);
                count++;
            }
            //break;
        }
        #endregion
    }

}


[System.Serializable]
public class PerPortionRewardsHolder
{
    public Transform[] Rewards;
}
