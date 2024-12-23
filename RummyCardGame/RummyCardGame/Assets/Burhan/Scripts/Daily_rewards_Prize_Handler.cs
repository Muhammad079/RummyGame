using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using System.Timers;
using UnityEngine.UI;

public class Daily_rewards_Prize_Handler : MonoBehaviour
{
    public Transform[] coins_Position, gems_Position;
    public Sprite /*Gem_image,*/ Coin_Image;
    public Button[] Collect_Btn;
    public Sprite[] Claim_Btn_Sprites; 
    public static int coin, gem;
    public GameObject[] Locks;
    bool isVip;

    DateTime today = DateTime.Today;


    GameObject[] Moveables = new GameObject[0];
    public Transform Coins_Container, midpoint;


    private void OnEnable()
    {
        
    }
    void Start()
    {
        transform.GetChild(0).GetComponent<ScrollRect>().enabled = false;

        
        coin = 0;
        gem = 0;
        Invoke(nameof(Day_Check_Display_Modified), 0.1f);
    }
    public void Day_Check_Display_Modified()
    {
        isVip = ProfileManager.instance.currentPlayer.isVip;
        for (int i = 1; i < 7; i++)
        {
            Debug.LogError(i);
            if ((int)today.DayOfWeek == i)
            {
                if (Collect_Btn[i-1])
                {
                    Collect_Btn[i - 1].interactable = true;
                    Collect_Btn[i - 1].GetComponent<Image>().sprite = Claim_Btn_Sprites[0];
                    Collect_Btn[i - 1].transform.GetChild(0).GetComponent<Kozykin.MultiLanguageItem>().strItems[0] = "Claim";
                    Collect_Btn[i - 1].transform.GetChild(0).GetComponent<Kozykin.MultiLanguageItem>().strItems[1] = "مطالبة";
                    Collect_Btn[i - 1].transform.GetChild(0).GetComponent<Kozykin.MultiLanguageItem>().text = "مطالبة";
                    Collect_Btn[i].GetComponent<Image>().sprite = Claim_Btn_Sprites[3];
                    Collect_Btn[i].transform.GetChild(0).GetComponent<Kozykin.MultiLanguageItem>().strItems[0] = "Tomorrow";
                    Collect_Btn[i].transform.GetChild(0).GetComponent<Kozykin.MultiLanguageItem>().strItems[1] = "الغد";
                    Collect_Btn[i].transform.GetChild(0).GetComponent<Kozykin.MultiLanguageItem>().text = "الغد";
                    Collect_Btn[i].interactable = false;
                }
            }
            else
            {
                if (Collect_Btn[i])
                {
                    Collect_Btn[i].interactable = false;
                    Collect_Btn[i].GetComponent<Image>().sprite = Claim_Btn_Sprites[1];
                    Collect_Btn[i].transform.GetChild(0).GetComponent<Kozykin.MultiLanguageItem>().strItems[0] = "Claim";
                    Collect_Btn[i].transform.GetChild(0).GetComponent<Kozykin.MultiLanguageItem>().strItems[1] = "مطالبة";
                    Collect_Btn[i].transform.GetChild(0).GetComponent<Kozykin.MultiLanguageItem>().text = "مطالبة";
                }
            }

            if(isVip)
            {
                Locks[i].SetActive(true);
            }
            else
            {
                Locks[i].SetActive(false);
            }
            if (ProfileManager.instance.currentPlayer.isVip)
            {
                gems_Position[i].GetComponent<Image>().sprite = GameManager.instance.VIP_Levels[ProfileManager.instance.currentPlayer.VIP_Level - 1];
                gems_Position[i].transform.parent.GetChild(0).GetComponent<Text>().text = "+" + GameManager.instance.VIP_Information.VIPs[ProfileManager.instance.currentPlayer.VIP_Level - 1].Daily_Bonus + " Coins";
                gems_Position[i].transform.parent.GetChild(0).GetComponent<Kozykin.MultiLanguageItem>().strItems[0] = "+" + GameManager.instance.VIP_Information.VIPs[ProfileManager.instance.currentPlayer.VIP_Level - 1].Daily_Bonus + " Coins";
                gems_Position[i].transform.parent.GetChild(0).GetComponent<Kozykin.MultiLanguageItem>().strItems[1] = "+" + GameManager.instance.VIP_Information.VIPs[ProfileManager.instance.currentPlayer.VIP_Level - 1].Daily_Bonus + " عملات معدنية";
                gems_Position[i].transform.parent.GetChild(0).GetComponent<Kozykin.MultiLanguageItem>().text = "+" + GameManager.instance.VIP_Information.VIPs[ProfileManager.instance.currentPlayer.VIP_Level - 1].Daily_Bonus + " عملات معدنية";
            }
                
            

            transform.GetChild(0).GetComponent<ScrollRect>().enabled = true;
        }
    }
    IEnumerator Animation(GameObject[] Moveables, int Coins_position, int Gems_Posititon)
    {
        Vector3[] waypoints = new Vector3[3];
        waypoints[1] = /*coins_Position[3].parent.transform.position;*/   midpoint.position;
        waypoints[2] = Coins_Container.position;
        if (Moveables.Length < 8)
        {
            waypoints[0] = coins_Position[Coins_position].position;
            Moveables[0].transform.DOPath(waypoints, 0.7f, PathType.CatmullRom).SetEase(Ease.Linear).OnComplete(() =>
             {
                 Destroy(Moveables[0].gameObject);
             });
            yield return new WaitForSeconds(0.05f);
            Moveables[1].transform.DOPath(waypoints, 0.7f, PathType.CatmullRom).SetEase(Ease.Linear).OnComplete(() =>
            {
                Destroy(Moveables[1].gameObject);
            });
            yield return new WaitForSeconds(0.05f);
            Moveables[2].transform.DOPath(waypoints, 0.7f, PathType.CatmullRom).SetEase(Ease.Linear).OnComplete(() =>
            {
                Destroy(Moveables[2].gameObject);
            });
            yield return new WaitForSeconds(0.05f);
            Moveables[3].transform.DOPath(waypoints, 0.7f, PathType.CatmullRom).SetEase(Ease.Linear).OnComplete(() =>
            {
                Destroy(Moveables[3].gameObject);
            });
            yield return new WaitForSeconds(0.05f);
            Moveables[4].transform.DOPath(waypoints, 0.7f, PathType.CatmullRom).SetEase(Ease.Linear).OnComplete(() =>
            {
                Destroy(Moveables[4].gameObject);
            });
            yield return new WaitForSeconds(0.05f);
            Moveables[5].transform.DOPath(waypoints, 0.7f, PathType.CatmullRom).SetEase(Ease.Linear).OnComplete(() =>
            {
                Destroy(Moveables[5].gameObject);

                
            });
            yield return new WaitForSeconds(0.05f);
            Coins_Container.parent.GetChild(1).GetComponent<Text>().text = ProfileManager.instance.currentPlayer.coins.ToString();
        }
        else
        {
            waypoints[0] = coins_Position[Coins_position].position;
            Moveables[0].transform.DOPath(waypoints, 0.7f, PathType.CatmullRom).SetEase(Ease.Linear).OnComplete(() =>
            {
                Destroy(Moveables[0].gameObject);
            });
            yield return new WaitForSeconds(0.05f);
            Moveables[1].transform.DOPath(waypoints, 0.7f, PathType.CatmullRom).SetEase(Ease.Linear).OnComplete(() =>
            {
                Destroy(Moveables[1].gameObject);
            });
            yield return new WaitForSeconds(0.05f);
            Moveables[2].transform.DOPath(waypoints, 0.7f, PathType.CatmullRom).SetEase(Ease.Linear).OnComplete(() =>
            {
                Destroy(Moveables[2].gameObject);
            });
            yield return new WaitForSeconds(0.05f);
            Moveables[3].transform.DOPath(waypoints, 0.7f, PathType.CatmullRom).SetEase(Ease.Linear).OnComplete(() =>
            {
                Destroy(Moveables[3].gameObject);
            });
            yield return new WaitForSeconds(0.05f);
            Moveables[4].transform.DOPath(waypoints, 0.7f, PathType.CatmullRom).SetEase(Ease.Linear).OnComplete(() =>
            {
                Destroy(Moveables[4].gameObject);
            });
            yield return new WaitForSeconds(0.05f);
            Moveables[5].transform.DOPath(waypoints, 0.7f, PathType.CatmullRom).SetEase(Ease.Linear).OnComplete(() =>
            {
                Destroy(Moveables[5].gameObject);
            });
            yield return new WaitForSeconds(0.05f);


            waypoints[0] = gems_Position[Gems_Posititon].position;
            Moveables[6].transform.DOPath(waypoints, 0.7f, PathType.CatmullRom).SetEase(Ease.Linear).OnComplete(() =>
            {
                Destroy(Moveables[6].gameObject);
            });
            yield return new WaitForSeconds(0.05f);
            Moveables[7].transform.DOPath(waypoints, 0.7f, PathType.CatmullRom).SetEase(Ease.Linear).OnComplete(() =>
            {
                Destroy(Moveables[7].gameObject);
            });
            yield return new WaitForSeconds(0.05f);
            Moveables[8].transform.DOPath(waypoints, 0.7f, PathType.CatmullRom).SetEase(Ease.Linear).OnComplete(() =>
            {
                Destroy(Moveables[8].gameObject);
            });
            yield return new WaitForSeconds(0.05f);
            Moveables[9].transform.DOPath(waypoints, 0.7f, PathType.CatmullRom).SetEase(Ease.Linear).OnComplete(() =>
            {
                Destroy(Moveables[9].gameObject);
            });
            yield return new WaitForSeconds(0.05f);
            Moveables[10].transform.DOPath(waypoints, 0.7f, PathType.CatmullRom).SetEase(Ease.Linear).OnComplete(() =>
            {
                Destroy(Moveables[10].gameObject);
            });
            yield return new WaitForSeconds(0.05f);
            Moveables[11].transform.DOPath(waypoints, 0.7f, PathType.CatmullRom).SetEase(Ease.Linear).OnComplete(() =>
            {
                Destroy(Moveables[11].gameObject);
            });
            yield return new WaitForSeconds(0.05f);
            Coins_Container.parent.GetChild(1).GetComponent<Text>().text = ProfileManager.instance.currentPlayer.coins.ToString();
        }
    }

    public void Daily_rewards_prize_1()
    {
        Moveables = new GameObject[6];
        if (isVip)
        {
            ProfileManager.instance.currentPlayer.gems += 2;
            Moveables = new GameObject[12];
        }
        coin = 1000;
        if(ProfileManager.instance.currentPlayer.isVip)
        {
            coin += GameManager.instance.VIP_Information.VIPs[ProfileManager.instance.currentPlayer.VIP_Level - 1].Daily_Bonus;
        }

        ProfileManager.instance.currentPlayer.coins += coin;
        Collect_Btn[0].interactable = false;
        Collect_Btn[0].GetComponent<Image>().sprite = Claim_Btn_Sprites[2];
        Collect_Btn[0].transform.GetChild(0).GetComponent<Kozykin.MultiLanguageItem>().strItems[0] = "Claimed";
        Collect_Btn[0].transform.GetChild(0).GetComponent<Kozykin.MultiLanguageItem>().strItems[1] = "ادعى";
        Collect_Btn[0].transform.GetChild(0).GetComponent<Kozykin.MultiLanguageItem>().text = "ادعى";
        ProfileManager.instance.currentPlayer.lastDailyRewardCollected = (int)DateTime.UtcNow.DayOfWeek;
        ProfileManager.instance.SaveUserData();


        //GameObject a = new GameObject();
        for(int i =0;i< Moveables.Length;i++)
        {
            GameObject a = new GameObject();
            if (i>7)
            {
                a.transform.SetParent(gems_Position[0]);
                a.AddComponent<Image>().sprite = Coin_Image; //gem image changed
                a.transform.position = gems_Position[0].position;
                a.transform.localScale = gems_Position[0].localScale;
                a.transform.GetComponent<RectTransform>().sizeDelta=new Vector2(30,30);
                a.GetComponent<Image>().preserveAspect = true;
                a.AddComponent<Canvas>().overrideSorting = true;
                a.GetComponent<Canvas>().sortingOrder = 10;
                Moveables[i] = a;
            }
            else
            {
                a.transform.SetParent(coins_Position[0]);
                a.AddComponent<Image>().sprite = Coin_Image;
                a.transform.position = coins_Position[0].position;
                a.transform.localScale = coins_Position[0].localScale;
                a.transform.GetComponent<RectTransform>().sizeDelta = new Vector2(30, 30);
                a.GetComponent<Image>().preserveAspect = true;
                a.AddComponent<Canvas>().overrideSorting = true;
                a.GetComponent<Canvas>().sortingOrder = 10;
                Moveables[i] = a;
            }
        }
        StartCoroutine(Animation(Moveables, 0, 0));

    }
    public void Daily_rewards_prize_2()
    {
        Moveables = new GameObject[6];
        if (isVip)
        {
            ProfileManager.instance.currentPlayer.gems += 2;
            Moveables = new GameObject[12];
        }
        coin = 1500;
        if (ProfileManager.instance.currentPlayer.isVip)
        {
            coin += GameManager.instance.VIP_Information.VIPs[ProfileManager.instance.currentPlayer.VIP_Level - 1].Daily_Bonus;
        }
        ProfileManager.instance.currentPlayer.coins += coin;
        Collect_Btn[1].interactable = false;
        Collect_Btn[1].GetComponent<Image>().sprite = Claim_Btn_Sprites[2];
        Collect_Btn[1].transform.GetChild(0).GetComponent<Kozykin.MultiLanguageItem>().strItems[0] = "Claimed";
        Collect_Btn[1].transform.GetChild(0).GetComponent<Kozykin.MultiLanguageItem>().strItems[1] = "ادعى";
        Collect_Btn[1].transform.GetChild(0).GetComponent<Kozykin.MultiLanguageItem>().text = "ادعى";
        ProfileManager.instance.currentPlayer.lastDailyRewardCollected = (int)DateTime.UtcNow.DayOfWeek; ProfileManager.instance.SaveUserData();


        
        for (int i = 0; i < Moveables.Length; i++)
        {
            GameObject a = new GameObject();
            if (i > 7)
            {
                a.transform.SetParent(gems_Position[1]);
                a.AddComponent<Image>().sprite = Coin_Image; //gem image changed
                a.transform.position = gems_Position[1].position;
                a.transform.localScale = gems_Position[1].localScale;
                a.transform.GetComponent<RectTransform>().sizeDelta = new Vector2(30, 30);
                a.GetComponent<Image>().preserveAspect = true;
                a.AddComponent<Canvas>().overrideSorting = true;
                a.GetComponent<Canvas>().sortingOrder = 10;
                Moveables[i] = a;
            }
            else
            {
                a.transform.SetParent(coins_Position[1]);
                a.AddComponent<Image>().sprite = Coin_Image;
                a.transform.position = coins_Position[1].position;
                a.transform.localScale = coins_Position[1].localScale;
                a.transform.GetComponent<RectTransform>().sizeDelta = new Vector2(30, 30);
                a.GetComponent<Image>().preserveAspect = true;
                a.AddComponent<Canvas>().overrideSorting = true;
                a.GetComponent<Canvas>().sortingOrder = 10;
                Moveables[i] = a;
            }
        }
        StartCoroutine(Animation(Moveables, 1, 1));
    }
    public void Daily_rewards_prize_3()
    {
        Moveables = new GameObject[6];
        if (isVip)
        {
            ProfileManager.instance.currentPlayer.gems += 2;
            Moveables = new GameObject[12];
        }
        coin = 2000;
        if (ProfileManager.instance.currentPlayer.isVip)
        {
            coin += GameManager.instance.VIP_Information.VIPs[ProfileManager.instance.currentPlayer.VIP_Level - 1].Daily_Bonus;
        }
        ProfileManager.instance.currentPlayer.coins += coin;
        Collect_Btn[2].interactable = false;
        Collect_Btn[2].GetComponent<Image>().sprite = Claim_Btn_Sprites[2];
        Collect_Btn[2].transform.GetChild(0).GetComponent<Kozykin.MultiLanguageItem>().strItems[0] = "Claimed";
        Collect_Btn[2].transform.GetChild(0).GetComponent<Kozykin.MultiLanguageItem>().strItems[1] = "ادعى";
        Collect_Btn[2].transform.GetChild(0).GetComponent<Kozykin.MultiLanguageItem>().text = "ادعى";
        ProfileManager.instance.currentPlayer.lastDailyRewardCollected = (int)DateTime.UtcNow.DayOfWeek; ProfileManager.instance.SaveUserData();


        //GameObject a = new GameObject();
        for (int i = 0; i < Moveables.Length; i++)
        {
            GameObject a = new GameObject();
            if (i > 7)
            {
                a.transform.SetParent(gems_Position[2]);
                a.AddComponent<Image>().sprite = Coin_Image; //gem image changed
                a.transform.position = gems_Position[2].position;
                a.transform.localScale = gems_Position[2].localScale;
                a.transform.GetComponent<RectTransform>().sizeDelta = new Vector2(30, 30);
                a.GetComponent<Image>().preserveAspect = true;
                a.AddComponent<Canvas>().overrideSorting = true;
                a.GetComponent<Canvas>().sortingOrder = 10;
                Moveables[i] = a;
            }
            else
            {
                a.transform.SetParent(coins_Position[2]);
                a.AddComponent<Image>().sprite = Coin_Image;
                a.transform.position = coins_Position[2].position;
                a.transform.localScale = coins_Position[2].localScale;
                a.transform.GetComponent<RectTransform>().sizeDelta = new Vector2(30, 30);
                a.GetComponent<Image>().preserveAspect = true;
                a.AddComponent<Canvas>().overrideSorting = true;
                a.GetComponent<Canvas>().sortingOrder = 10;
                Moveables[i] = a;
            }
        }
        StartCoroutine(Animation(Moveables, 2, 2));
    }
    public void Daily_rewards_prize_4()
    {
        Moveables = new GameObject[6];
        if (isVip)
        {
            ProfileManager.instance.currentPlayer.gems += 2;
            Moveables = new GameObject[12];
        }
        coin = 3000;
        if (ProfileManager.instance.currentPlayer.isVip)
        {
            coin += GameManager.instance.VIP_Information.VIPs[ProfileManager.instance.currentPlayer.VIP_Level - 1].Daily_Bonus;
        }
        ProfileManager.instance.currentPlayer.coins += coin;
        Collect_Btn[3].interactable = false;
        Collect_Btn[3].GetComponent<Image>().sprite = Claim_Btn_Sprites[2];
        Collect_Btn[3].transform.GetChild(0).GetComponent<Kozykin.MultiLanguageItem>().strItems[0] = "Claimed";
        Collect_Btn[3].transform.GetChild(0).GetComponent<Kozykin.MultiLanguageItem>().strItems[1] = "ادعى";
        Collect_Btn[3].transform.GetChild(0).GetComponent<Kozykin.MultiLanguageItem>().text = "ادعى";
        ProfileManager.instance.currentPlayer.lastDailyRewardCollected = (int)DateTime.UtcNow.DayOfWeek; ProfileManager.instance.SaveUserData();


        //GameObject a = new GameObject();
        for (int i = 0; i < Moveables.Length; i++)
        {
            GameObject a = new GameObject();
            if (i > 7)
            {
                a.transform.SetParent(gems_Position[3]);
                a.AddComponent<Image>().sprite = Coin_Image; //gem image changed
                a.transform.position = gems_Position[3].position;
                a.transform.localScale = gems_Position[3].localScale;
                a.transform.GetComponent<RectTransform>().sizeDelta = new Vector2(30, 30);
                a.GetComponent<Image>().preserveAspect = true;
                a.AddComponent<Canvas>().overrideSorting = true;
                a.GetComponent<Canvas>().sortingOrder = 10;
                Moveables[i] = a;
            }
            else
            {
                a.transform.SetParent(coins_Position[3]);
                a.AddComponent<Image>().sprite = Coin_Image;
                a.transform.position = coins_Position[3].position;
                a.transform.localScale = coins_Position[3].localScale;
                a.transform.GetComponent<RectTransform>().sizeDelta = new Vector2(30, 30);
                a.GetComponent<Image>().preserveAspect = true;
                a.AddComponent<Canvas>().overrideSorting = true;
                a.GetComponent<Canvas>().sortingOrder = 10;
                Moveables[i] = a;
            }
        }
        StartCoroutine(Animation(Moveables, 3, 3));
    }
    public void Daily_rewards_prize_5()
    {
        Moveables = new GameObject[6];
        if (isVip)
        {
            ProfileManager.instance.currentPlayer.gems += 2;
            Moveables = new GameObject[12];
        }
        coin = 4000;
        if (ProfileManager.instance.currentPlayer.isVip)
        {
            coin += GameManager.instance.VIP_Information.VIPs[ProfileManager.instance.currentPlayer.VIP_Level - 1].Daily_Bonus;
        }
        ProfileManager.instance.currentPlayer.coins += coin;
        Collect_Btn[4].interactable = false;
        Collect_Btn[4].GetComponent<Image>().sprite = Claim_Btn_Sprites[2];
        Collect_Btn[4].transform.GetChild(0).GetComponent<Kozykin.MultiLanguageItem>().strItems[0] = "Claimed";
        Collect_Btn[4].transform.GetChild(0).GetComponent<Kozykin.MultiLanguageItem>().strItems[1] = "ادعى";
        Collect_Btn[4].transform.GetChild(0).GetComponent<Kozykin.MultiLanguageItem>().text = "ادعى";
        ProfileManager.instance.currentPlayer.lastDailyRewardCollected = (int)DateTime.UtcNow.DayOfWeek; ProfileManager.instance.SaveUserData();


        //GameObject a = new GameObject();
        for (int i = 0; i < Moveables.Length; i++)
        {
            GameObject a = new GameObject();
            if (i > 7)
            {
                a.transform.SetParent(gems_Position[4]);
                a.AddComponent<Image>().sprite = Coin_Image; //gem image changed
                a.transform.position = gems_Position[4].position;
                a.transform.localScale = gems_Position[4].localScale;
                a.transform.GetComponent<RectTransform>().sizeDelta = new Vector2(30, 30);
                a.GetComponent<Image>().preserveAspect = true;
                a.AddComponent<Canvas>().overrideSorting = true;
                a.GetComponent<Canvas>().sortingOrder = 10;
                Moveables[i] = a;
            }
            else
            {
                a.transform.SetParent(coins_Position[4]);
                a.AddComponent<Image>().sprite = Coin_Image;
                a.transform.position = coins_Position[4].position;
                a.transform.localScale = coins_Position[4].localScale;
                a.transform.GetComponent<RectTransform>().sizeDelta = new Vector2(30, 30);
                a.GetComponent<Image>().preserveAspect = true;
                a.AddComponent<Canvas>().overrideSorting = true;
                a.GetComponent<Canvas>().sortingOrder = 10;
                Moveables[i] = a;
            }
        }
        StartCoroutine(Animation(Moveables, 4, 4));
    }
    public void Daily_rewards_prize_6()
    {
        Moveables = new GameObject[6];
        if (isVip)
        {
            ProfileManager.instance.currentPlayer.gems += 2;
            Moveables = new GameObject[12];
        }
        coin = 5000;
        if (ProfileManager.instance.currentPlayer.isVip)
        {
            coin += GameManager.instance.VIP_Information.VIPs[ProfileManager.instance.currentPlayer.VIP_Level - 1].Daily_Bonus;
        }
        ProfileManager.instance.currentPlayer.coins += coin;
        Collect_Btn[5].interactable = false;
        Collect_Btn[5].GetComponent<Image>().sprite = Claim_Btn_Sprites[2];
        Collect_Btn[5].transform.GetChild(0).GetComponent<Kozykin.MultiLanguageItem>().strItems[0] = "Claimed";
        Collect_Btn[5].transform.GetChild(0).GetComponent<Kozykin.MultiLanguageItem>().strItems[1] = "ادعى";
        Collect_Btn[5].transform.GetChild(0).GetComponent<Kozykin.MultiLanguageItem>().text = "ادعى";
        ProfileManager.instance.currentPlayer.lastDailyRewardCollected = (int)DateTime.UtcNow.DayOfWeek; ProfileManager.instance.SaveUserData();


        //GameObject a = new GameObject();
        for (int i = 0; i < Moveables.Length; i++)
        {
            GameObject a = new GameObject();
            if (i > 7)
            {
                a.transform.SetParent(gems_Position[5]);
                a.AddComponent<Image>().sprite = Coin_Image; //gem image changed
                a.transform.position = gems_Position[5].position;
                a.transform.localScale = gems_Position[5].localScale;
                a.transform.GetComponent<RectTransform>().sizeDelta = new Vector2(30, 30);
                a.GetComponent<Image>().preserveAspect = true;
                a.AddComponent<Canvas>().overrideSorting = true;
                a.GetComponent<Canvas>().sortingOrder = 10;
                Moveables[i] = a;
            }
            else
            {
                a.transform.SetParent(coins_Position[5]);
                a.AddComponent<Image>().sprite = Coin_Image;
                a.transform.position = coins_Position[5].position;
                a.transform.localScale = coins_Position[5].localScale;
                a.transform.GetComponent<RectTransform>().sizeDelta = new Vector2(30, 30);
                a.GetComponent<Image>().preserveAspect = true;
                a.AddComponent<Canvas>().overrideSorting = true;
                a.GetComponent<Canvas>().sortingOrder = 10;
                Moveables[i] = a;
            }
        }
        StartCoroutine(Animation(Moveables, 5, 5));
    }
    public void Daily_rewards_prize_7()
    {
        Moveables = new GameObject[6];
        if (isVip)
        {
            ProfileManager.instance.currentPlayer.gems += 2;
            Moveables = new GameObject[12];
        }
        coin = 10000;
        gem = 2;
        if (ProfileManager.instance.currentPlayer.isVip)
        {
            coin += GameManager.instance.VIP_Information.VIPs[ProfileManager.instance.currentPlayer.VIP_Level - 1].Daily_Bonus;
        }
        ProfileManager.instance.currentPlayer.coins += coin;
        ProfileManager.instance.currentPlayer.gems += gem;
        Collect_Btn[6].interactable = false;
        Collect_Btn[6].GetComponent<Image>().sprite = Claim_Btn_Sprites[2];
        Collect_Btn[6].transform.GetChild(0).GetComponent<Kozykin.MultiLanguageItem>().strItems[0] = "Claimed";
        Collect_Btn[6].transform.GetChild(0).GetComponent<Kozykin.MultiLanguageItem>().strItems[1] = "ادعى";
        Collect_Btn[6].transform.GetChild(0).GetComponent<Kozykin.MultiLanguageItem>().text = "ادعى";
        ProfileManager.instance.currentPlayer.lastDailyRewardCollected = (int)DateTime.UtcNow.DayOfWeek; ProfileManager.instance.SaveUserData();


        //GameObject a = new GameObject();
        for (int i = 0; i < Moveables.Length; i++)
        {
            GameObject a = new GameObject();
            if (i > 7)
            {
                a.transform.SetParent(gems_Position[6]);
                a.AddComponent<Image>().sprite = Coin_Image; //gem image changed
                a.transform.position = gems_Position[6].position;
                a.transform.localScale = gems_Position[6].localScale;
                a.transform.GetComponent<RectTransform>().sizeDelta = new Vector2(30, 30);
                a.GetComponent<Image>().preserveAspect = true;
                a.AddComponent<Canvas>().overrideSorting = true;
                a.GetComponent<Canvas>().sortingOrder = 10;
                Moveables[i] = a;
            }
            else
            {
                a.transform.SetParent(coins_Position[6]);
                a.AddComponent<Image>().sprite = Coin_Image;
                a.transform.position = coins_Position[6].position;
                a.transform.localScale = coins_Position[6].localScale;
                a.transform.GetComponent<RectTransform>().sizeDelta = new Vector2(30, 30);
                a.GetComponent<Image>().preserveAspect = true;
                a.AddComponent<Canvas>().overrideSorting = true;
                a.GetComponent<Canvas>().sortingOrder = 10;
                Moveables[i] = a;
            }
        }
        StartCoroutine(Animation(Moveables, 6, 6));
    }
}
