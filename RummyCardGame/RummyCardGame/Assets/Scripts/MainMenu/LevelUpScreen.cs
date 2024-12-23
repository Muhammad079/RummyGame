using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class LevelUpScreen : MonoBehaviour
{
    [SerializeField] private Text levelTitle = null, levelCount = null, rewardQuantity = null;
    [SerializeField] private Image rewardSprite = null;
    private PlayerProfile updatedProfile=new PlayerProfile();
    public GameObject User_Gem_Location;//, daily_reward;
    bool collected;

    private void OnEnable()
    {
        User_Gem_Location.transform.parent.GetComponent<Canvas>().overrideSorting = true;
        User_Gem_Location.transform.parent.GetComponent<Canvas>().sortingOrder = 1;
    }
    private void OnDisable()
    {
        User_Gem_Location.transform.parent.GetComponent<Canvas>().overrideSorting = false;
        User_Gem_Location.transform.parent.GetComponent<Canvas>().sortingOrder = 0;
    }

    public  void LevelUp()
    {
        collected = false;
        rewardQuantity.text = "+" + GameManager.instance.LevelUpData.levelsData[ProfileManager.instance.currentPlayer.level].Rewards.quantity;
        rewardQuantity.GetComponent<Kozykin.MultiLanguageItem>().strItems[0] = "+" + GameManager.instance.LevelUpData.levelsData[ProfileManager.instance.currentPlayer.level].Rewards.quantity;
        rewardQuantity.GetComponent<Kozykin.MultiLanguageItem>().strItems[1] = "+" + GameManager.instance.LevelUpData.levelsData[ProfileManager.instance.currentPlayer.level].Rewards.quantity;
        rewardQuantity.GetComponent<Kozykin.MultiLanguageItem>().text = "+" + GameManager.instance.LevelUpData.levelsData[ProfileManager.instance.currentPlayer.level].Rewards.quantity;

        levelTitle.text = "Level " + ProfileManager.instance.currentPlayer.level;
        levelTitle.GetComponent<Kozykin.MultiLanguageItem>().strItems[0] = "Level " + ProfileManager.instance.currentPlayer.level;
        levelTitle.GetComponent<Kozykin.MultiLanguageItem>().strItems[1] = "مستوى " + ProfileManager.instance.currentPlayer.level;
        levelTitle.GetComponent<Kozykin.MultiLanguageItem>().text = "مستوى " + ProfileManager.instance.currentPlayer.level;

        levelCount.text = ProfileManager.instance.currentPlayer.level.ToString();
        levelCount.GetComponent<Kozykin.MultiLanguageItem>().strItems[0] = ProfileManager.instance.currentPlayer.level.ToString();
        levelCount.GetComponent<Kozykin.MultiLanguageItem>().strItems[1] = ProfileManager.instance.currentPlayer.level.ToString();
        levelCount.GetComponent<Kozykin.MultiLanguageItem>().text = ProfileManager.instance.currentPlayer.level.ToString();

        //    rewardQuantity.text ="+" +reward.boxItems[0].quantity;
        //    SettingRewardImage(reward.boxItems[0]);
        this.gameObject.SetActive(true);
    }
    private void Update()
    {
        //if(daily_reward.activeInHierarchy)
        //{
        //    User_Gem_Location.transform.parent.GetComponent<Canvas>().overrideSorting = false;
        //    User_Gem_Location.transform.parent.GetComponent<Canvas>().sortingOrder = 0;
        //}
        //else
        //{
        //    User_Gem_Location.transform.parent.GetComponent<Canvas>().overrideSorting = true;
        //    User_Gem_Location.transform.parent.GetComponent<Canvas>().sortingOrder = 1;
        //}


        if (Input.GetMouseButtonDown(0) && !collected)
        {
            collected = true;
            CollectReward();
        }
    }
    void SettingRewardImage(RewardItems rewards)
    {
        if (rewards.reward == RewardType.goldenCards)
        {
           
            rewardSprite.sprite = GameManager.instance.r_GoldenCard;
            updatedProfile.goldenCards = rewards.quantity;
        }
        else if (rewards.reward == RewardType.coins)
        {
            
            rewardSprite.sprite = GameManager.instance.r_Coins;
            updatedProfile.coins = rewards.quantity;
        }
        else if (rewards.reward == RewardType.gems)
        {
           
            rewardSprite.sprite = GameManager.instance.r_Gems;
            updatedProfile.gems = rewards.quantity;
        }
        else if (rewards.reward == RewardType.xp)
        {
        
            rewardSprite.sprite = GameManager.instance.r_Xp;
            updatedProfile.xp = rewards.quantity;
        }
        else if (rewards.reward == RewardType.badges)
        {
            rewardSprite.sprite = GameManager.instance.r_Badges;
            updatedProfile.badges = rewards.quantity;
        }
     }
    void CollectReward()
    {
        
        ProfileManager.instance.GetReward(RewardType.gems, GameManager.instance.LevelUpData.levelsData[ProfileManager.instance.currentPlayer.level].Rewards.quantity);

        

        GameObject[] Moveables = new GameObject[7];
        for(int i=0;i<7;i++)
        {
            GameObject a = new GameObject();
            a.transform.SetParent(rewardSprite.transform);
            a.AddComponent<Image>().sprite = rewardSprite.sprite;
            a.transform.position = rewardSprite.transform.position;
            a.transform.localScale = rewardSprite.transform.localScale;
            a.transform.GetComponent<RectTransform>().anchorMin=new Vector2(0.5f, 0.5f);
            a.transform.GetComponent<RectTransform>().SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal,29);
            a.transform.GetComponent<RectTransform>().SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, 29);
            Moveables[i] = a;

            
        }
        StartCoroutine(Wait_Delay(Moveables));

        //ProfileManager.instance.currentPlayer.badges += updatedProfile.badges;
        //ProfileManager.instance.currentPlayer.xp += updatedProfile.xp;
        //ProfileManager.instance.currentPlayer.gems += updatedProfile.gems;
        //ProfileManager.instance.currentPlayer.coins += updatedProfile.coins;
        // ProfileManager.instance.currentPlayer.goldenCards += updatedProfile.goldenCards;
        //ProfileManager.instance.SaveUserData();
        //this.gameObject.SetActive(false);
    }
    IEnumerator Wait_Delay(GameObject[] Moveables)
    {
        Vector3[] wayPoints = new Vector3[3];
        wayPoints[0] = rewardSprite.transform.position;
        wayPoints[1] = transform.position;
        wayPoints[2] = User_Gem_Location.transform.position;

        Moveables[0].transform.DOPath(wayPoints, 0.5f, PathType.CatmullRom).OnComplete(()=> {
            Destroy(Moveables[0]);
        });
        yield return new WaitForSeconds(0.05f);
        Moveables[1].transform.DOPath(wayPoints, 0.5f, PathType.CatmullRom).OnComplete(() => {
            Destroy(Moveables[1]);
        });
        yield return new WaitForSeconds(0.05f);
        Moveables[2].transform.DOPath(wayPoints, 0.5f, PathType.CatmullRom).OnComplete(() => {
            Destroy(Moveables[2]);
        });
        yield return new WaitForSeconds(0.05f);
        Moveables[3].transform.DOPath(wayPoints, 0.5f, PathType.CatmullRom).OnComplete(() => {
            Destroy(Moveables[3]);
        });
        yield return new WaitForSeconds(0.05f);
        Moveables[4].transform.DOPath(wayPoints, 0.5f, PathType.CatmullRom).OnComplete(() => {
            Destroy(Moveables[4]);

            User_Gem_Location.transform.parent.GetChild(1).GetComponent<Text>().text = ProfileManager.instance.currentPlayer.gems.ToString();


        });
        yield return new WaitForSeconds(0.05f);
        Moveables[5].transform.DOPath(wayPoints, 0.5f, PathType.CatmullRom).OnComplete(() => {
            Destroy(Moveables[5]);
        });
        yield return new WaitForSeconds(0.05f);
        Moveables[6].transform.DOPath(wayPoints, 0.5f, PathType.CatmullRom).OnComplete(() => {
            Destroy(Moveables[6]);
        });
        yield return new WaitForSeconds(0.05f);
        rewardSprite.transform.DOPath(wayPoints, 0.5f, PathType.CatmullRom).OnComplete(() => {
            Destroy(rewardSprite.gameObject);




            gameObject.GetComponent<DOTweenAnimation>().DOPlayBackwards();
        });
        yield return new WaitForSeconds(2);
        gameObject.GetComponent<DOTweenAnimation>().DOKill();
        gameObject.SetActive(false);
    }
}
