using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Purchasing;
using DG.Tweening;
using UnityEngine.UI;
using UnityEngine.Rendering;
using UnityEngine.SceneManagement;

public class IAP_Shop : MonoBehaviour
{
    public Sprite coins = null, gems = null;
    public Transform purchase_btn_REFERENCE = null;
    public Text Coins, Gems;

    #region Coins
    string Coins_25K_50K = "coins_25k_50k"; //$2.99
    string Coins_150K_300K = "coins_150k_300k";//$9.99
    string Coins_375K_750K = "coins_375k_750k";//$19.99
    string Coins_1_1M_2_2M = "coins_1_1m_2_2m";//$49.99
    string Coins_3M_6M = "coins_3m_6m";//$99.99
    #endregion

    #region Gems
    string Gems_100_200 = "gems_100_200";//$2.99
    string Gems_400_800 = "gems_400_800";//$9.99
    string Gems_1K_2K = "gems_1k_2k";//$19.99
    string Gems_3K_6K = "gems_3k_6k";//$49.99
    string Gems_8K_16K = "gems_8k_16k";//$99.99
    #endregion

    #region Coins_&_Gems
    string Coins_20K_Gems_50 = "coins_20k_gems_50";//$0.99
    string Coins_300K_Gems_200 = "coins_300k_gems_200";//$9.99
    string Coins_1M_Gems_1K = "coins_1m_gems_1k";//$19.99
    #endregion


    #region VIP
    string VIP_7Days = "vip_7days";//$4.99
    string VIP_30Days = "vip_30days";//$9.99
    #endregion
    public void On_Purchase_Complete(Product product )
    {
        if (product.definition.id.Contains("coins"))
        {
            Coins.text = ProfileManager.instance.currentPlayer.coins.ToString();
            Gems.text = ProfileManager.instance.currentPlayer.gems.ToString();
            string text = "coins";
            Coin_Animation(text);

        }
        if (product.definition.id.Contains("gems"))
        {
            Coins.text = ProfileManager.instance.currentPlayer.coins.ToString();
            Gems.text = ProfileManager.instance.currentPlayer.gems.ToString();
            string text = "gems";
            Coin_Animation(text);
        }
        if (product.definition.id.Contains("coins") && product.definition.id.Contains("gems"))
        {
            Coins.text = ProfileManager.instance.currentPlayer.coins.ToString();
            Gems.text = ProfileManager.instance.currentPlayer.gems.ToString();
            string text = "gems coins";
            Coin_Animation(text);
        }

        if (ProfileManager.instance.currentPlayer.isVip)
        {
            #region Coins
            if (product.definition.id == Coins_25K_50K)
            {
                Debug.Log("50K coins Purchased");
                ProfileManager.instance.GetReward(RewardType.coins, 50000);
            }
            else if (product.definition.id == Coins_150K_300K)
            {
                Debug.Log("300K coins Purchased");
                ProfileManager.instance.GetReward(RewardType.coins, 300000);
            }
            else if (product.definition.id == Coins_375K_750K)
            {
                Debug.Log("750K coins Purchased");
                ProfileManager.instance.GetReward(RewardType.coins, 750000);
            }
            else if (product.definition.id == Coins_1_1M_2_2M)
            {
                Debug.Log("2.2M coins Purchased");
                ProfileManager.instance.GetReward(RewardType.coins, 2200000);
            }
            else if (product.definition.id == Coins_3M_6M)
            {
                Debug.Log("6M coins Purchased");
                ProfileManager.instance.GetReward(RewardType.coins, 6000000);
            }
            #endregion

            #region Gems
            if (product.definition.id == Gems_100_200)
            {
                Debug.Log("200 Gems purchased");
                ProfileManager.instance.GetReward(RewardType.gems, 200);
            }
            else if (product.definition.id == Gems_400_800)
            {
                Debug.Log("800 Gems purchased");
                ProfileManager.instance.GetReward(RewardType.gems, 800);
            }
            else if (product.definition.id == Gems_1K_2K)
            {
                Debug.Log("2000 Gems purchased");
                ProfileManager.instance.GetReward(RewardType.gems, 2000);
            }
            else if (product.definition.id == Gems_3K_6K)
            {
                Debug.Log("6000 Gems purchased");
                ProfileManager.instance.GetReward(RewardType.gems, 6000);
            }
            else if (product.definition.id == Gems_8K_16K)
            {
                Debug.Log("16000 Gems purchased");
                ProfileManager.instance.GetReward(RewardType.gems, 16000);
            }
            #endregion

            #region VIP
            if (product.definition.id == VIP_7Days)
            {
                Debug.Log("Vip for 7 days purchased");
                ProfileManager.instance.GetReward(RewardType.vip, 7);
            }
            else if (product.definition.id == VIP_30Days)
            {
                Debug.Log("Vip for 30 days purchased");
                ProfileManager.instance.GetReward(RewardType.vip, 30);
            }
            #endregion

            #region Coins_&_Gems
            if (product.definition.id == Coins_20K_Gems_50)
            {
                Debug.Log("20K Coins and 50 Gems Purchased");
                ProfileManager.instance.GetReward(RewardType.coins, 20000);
                ProfileManager.instance.GetReward(RewardType.gems, 50);
            }
            else if (product.definition.id == Coins_300K_Gems_200)
            {
                Debug.Log("300K Coins and 200 Gems Purchased");
                ProfileManager.instance.GetReward(RewardType.coins, 300000);
                ProfileManager.instance.GetReward(RewardType.gems, 200);
            }
            else if (product.definition.id == Coins_1M_Gems_1K)
            {
                Debug.Log("1M Coins and 1000 Gems Purchased");
                ProfileManager.instance.GetReward(RewardType.coins, 1000000);
                ProfileManager.instance.GetReward(RewardType.gems, 1000);
            }
            #endregion
        }
        else
        {
            #region Coins
            if (product.definition.id == Coins_25K_50K)
            {
                Debug.Log("25K coins Purchased");
                ProfileManager.instance.GetReward(RewardType.coins, 25000);
            }
            else if (product.definition.id == Coins_150K_300K)
            {
                Debug.Log("150K coins Purchased");
                ProfileManager.instance.GetReward(RewardType.coins, 150000);
            }
            else if (product.definition.id == Coins_375K_750K)
            {
                Debug.Log("375K coins Purchased");
                ProfileManager.instance.GetReward(RewardType.coins, 375000);
            }
            else if (product.definition.id == Coins_1_1M_2_2M)
            {
                Debug.Log("1.1M coins Purchased");
                ProfileManager.instance.GetReward(RewardType.coins, 1100000);
            }
            else if (product.definition.id == Coins_3M_6M)
            {
                Debug.Log("3M coins Purchased");
                ProfileManager.instance.GetReward(RewardType.coins, 3000000);
            }
            #endregion

            #region Gems
            if (product.definition.id == Gems_100_200)
            {
                Debug.Log("100 Gems purchased");
                ProfileManager.instance.GetReward(RewardType.gems, 100);
            }
            else if (product.definition.id == Gems_400_800)
            {
                Debug.Log("400 Gems purchased");
                ProfileManager.instance.GetReward(RewardType.gems, 400);
            }
            else if (product.definition.id == Gems_1K_2K)
            {
                Debug.Log("1000 Gems purchased");
                ProfileManager.instance.GetReward(RewardType.gems, 1000);
            }
            else if (product.definition.id == Gems_3K_6K)
            {
                Debug.Log("3000 Gems purchased");
                ProfileManager.instance.GetReward(RewardType.gems, 3000);
            }
            else if (product.definition.id == Gems_8K_16K)
            {
                Debug.Log("8000 Gems purchased");
                ProfileManager.instance.GetReward(RewardType.gems, 8000);
            }
            #endregion

            #region VIP
            if (product.definition.id == VIP_7Days)
            {
                Debug.Log("Vip for 7 days purchased");
                ProfileManager.instance.GetReward(RewardType.vip, 7);
                
            }
            else if (product.definition.id == VIP_30Days)
            {
                Debug.Log("Vip for 30 days purchased");
                ProfileManager.instance.GetReward(RewardType.vip, 30);
                
            }
            #endregion

            #region Coins_&_Gems
            if (product.definition.id == Coins_20K_Gems_50)
            {
                Debug.Log("20K Coins and 50 Gems Purchased");
                ProfileManager.instance.GetReward(RewardType.coins, 20000);
                ProfileManager.instance.GetReward(RewardType.gems, 50);
            }
            else if (product.definition.id == Coins_300K_Gems_200)
            {
                Debug.Log("300K Coins and 200 Gems Purchased");
                ProfileManager.instance.GetReward(RewardType.coins, 300000);
                ProfileManager.instance.GetReward(RewardType.gems, 200);
            }
            else if (product.definition.id == Coins_1M_Gems_1K)
            {
                Debug.Log("1M Coins and 1000 Gems Purchased");
                ProfileManager.instance.GetReward(RewardType.coins, 1000000);
                ProfileManager.instance.GetReward(RewardType.gems, 1000);
            }
            #endregion
        }
    }
    private void Update()
    {
        Coins.text = ProfileManager.instance.currentPlayer.coins.ToString();
        Coins.GetComponent<Kozykin.MultiLanguageItem>().strItems[0] = ProfileManager.instance.currentPlayer.coins.ToString();
        Coins.GetComponent<Kozykin.MultiLanguageItem>().strItems[1] = ProfileManager.instance.currentPlayer.coins.ToString();
        Coins.GetComponent<Kozykin.MultiLanguageItem>().text = ProfileManager.instance.currentPlayer.coins.ToString();

        Gems.text = ProfileManager.instance.currentPlayer.gems.ToString();
        Gems.GetComponent<Kozykin.MultiLanguageItem>().strItems[0] = ProfileManager.instance.currentPlayer.gems.ToString();
        Gems.GetComponent<Kozykin.MultiLanguageItem>().strItems[1] = ProfileManager.instance.currentPlayer.gems.ToString();
        Gems.GetComponent<Kozykin.MultiLanguageItem>().text = ProfileManager.instance.currentPlayer.gems.ToString();
    }
    private void Coin_Animation(string product)
    {
        Vector3[] wayPoints = new Vector3[3];
        wayPoints[0] = purchase_btn_REFERENCE.transform.position;
        wayPoints[1] = transform.position;

        int loop_Length = 0 ;
        GameObject[] coins_Ins = new GameObject[0];
        if (product.Contains("coins") || product.Contains("gems"))
        {
            loop_Length = 6;
            coins_Ins = new GameObject[6];
        }
        else if(product.Contains("coins") && product.Contains("gems")) 
        {
            loop_Length = 12;
            coins_Ins = new GameObject[12];
        }
        Debug.Log("New Purhaser: loop length " + loop_Length);
        
        for (int i=0;i< loop_Length; i++)
        {
            GameObject a = new GameObject();
            a.transform.SetParent(transform);
            a.transform.localScale = new Vector3(1, 1, 1);
            a.transform.position = purchase_btn_REFERENCE.position;
            if (product.Contains("coins"))
            {
                Debug.Log("Coins?: " + product);
                a.AddComponent<Image>().sprite = coins;
                wayPoints[2] = Coins.transform.parent.parent.GetChild(1).transform.position;
            }
            else if (product.Contains("gems"))
            {
                Debug.Log("Gems?: "+product);
                a.AddComponent<Image>().sprite = gems;
                wayPoints[2] = Gems.transform.parent.parent.GetChild(1).transform.position;
            }
            else if (product.Contains("coins") && product.Contains("gems"))
            {
                if(i<5)
                {
                    Debug.Log("Coins?: " + product);
                    a.AddComponent<Image>().sprite = coins;
                    wayPoints[2] = Coins.transform.parent.parent.GetChild(1).transform.position;
                }
                else
                {
                    Debug.Log("Gems?: " + product);
                    a.AddComponent<Image>().sprite = gems;
                    wayPoints[2] = Gems.transform.parent.parent.GetChild(1).transform.position;
                }
            }
            a.GetComponent<Image>().preserveAspect = true;
            a.AddComponent<Canvas>().overrideSorting = true;
            a.GetComponent<Canvas>().sortingOrder = 10;

            if(SceneManager.GetActiveScene().name.Contains("Shop"))
            {
                a.GetComponent<RectTransform>().SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, 30);
                a.GetComponent<RectTransform>().SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, 30);
            }
            else
            {
                a.GetComponent<RectTransform>().SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, 100);
                a.GetComponent<RectTransform>().SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, 100);
            }
            
            coins_Ins[i] = a;
        }
        
        StartCoroutine(delay(coins_Ins, wayPoints));
    }
    IEnumerator delay(GameObject[] coins_Ins, Vector3[] wayPoints)
    {
        Debug.Log("New Purhaser: Coins length " + coins_Ins.Length);
        Debug.Log("New Purhaser: Waypoints length " + wayPoints.Length);

        if (coins_Ins.Length<7)
        {
            coins_Ins[0].transform.DOPath(wayPoints, 0.7f, PathType.CatmullRom).SetEase(Ease.Linear).OnComplete(() =>
            {
                Destroy(coins_Ins[0]);
            });
            yield return new WaitForSeconds(0.05f);
            coins_Ins[1].transform.DOPath(wayPoints, 0.7f, PathType.CatmullRom).SetEase(Ease.Linear).OnComplete(() =>
            {
                Destroy(coins_Ins[1]);
            });
            yield return new WaitForSeconds(0.05f);
            coins_Ins[2].transform.DOPath(wayPoints, 0.7f, PathType.CatmullRom).SetEase(Ease.Linear).OnComplete(() =>
            {
                Destroy(coins_Ins[2]);
            });
            yield return new WaitForSeconds(0.05f);
            coins_Ins[3].transform.DOPath(wayPoints, 0.7f, PathType.CatmullRom).SetEase(Ease.Linear).OnComplete(() =>
            {
                Destroy(coins_Ins[3]);
            });
            yield return new WaitForSeconds(0.05f);
            coins_Ins[4].transform.DOPath(wayPoints, 0.7f, PathType.CatmullRom).SetEase(Ease.Linear).OnComplete(() =>
            {
                Destroy(coins_Ins[4]);
            });
            yield return new WaitForSeconds(0.05f);
            coins_Ins[5].transform.DOPath(wayPoints, 0.7f, PathType.CatmullRom).SetEase(Ease.Linear).OnComplete(() =>
            {
                Destroy(coins_Ins[5]);
            });
        }
        else
        {
            coins_Ins[0].transform.DOPath(wayPoints, 0.7f, PathType.CatmullRom).SetEase(Ease.Linear).OnComplete(() =>
            {
                Destroy(coins_Ins[0]);
            });
            yield return new WaitForSeconds(0.05f);
            coins_Ins[1].transform.DOPath(wayPoints, 0.7f, PathType.CatmullRom).SetEase(Ease.Linear).OnComplete(() =>
            {
                Destroy(coins_Ins[1]);
            });
            yield return new WaitForSeconds(0.05f);
            coins_Ins[2].transform.DOPath(wayPoints, 0.7f, PathType.CatmullRom).SetEase(Ease.Linear).OnComplete(() =>
            {
                Destroy(coins_Ins[2]);
            });
            yield return new WaitForSeconds(0.05f);
            coins_Ins[3].transform.DOPath(wayPoints, 0.7f, PathType.CatmullRom).SetEase(Ease.Linear).OnComplete(() =>
            {
                Destroy(coins_Ins[3]);
            });
            yield return new WaitForSeconds(0.05f);
            coins_Ins[4].transform.DOPath(wayPoints, 0.7f, PathType.CatmullRom).SetEase(Ease.Linear).OnComplete(() =>
            {
                Destroy(coins_Ins[4]);
            });
            yield return new WaitForSeconds(0.05f);
            coins_Ins[5].transform.DOPath(wayPoints, 0.7f, PathType.CatmullRom).SetEase(Ease.Linear).OnComplete(() =>
            {
                Destroy(coins_Ins[5]);
            });



            yield return new WaitForSeconds(0.05f);
            coins_Ins[6].transform.DOPath(wayPoints, 0.7f, PathType.CatmullRom).SetEase(Ease.Linear).OnComplete(() =>
            {
                Destroy(coins_Ins[6]);
            });

            yield return new WaitForSeconds(0.05f);
            coins_Ins[7].transform.DOPath(wayPoints, 0.7f, PathType.CatmullRom).SetEase(Ease.Linear).OnComplete(() =>
            {
                Destroy(coins_Ins[7]);
            });

            yield return new WaitForSeconds(0.05f);
            coins_Ins[8].transform.DOPath(wayPoints, 0.7f, PathType.CatmullRom).SetEase(Ease.Linear).OnComplete(() =>
            {
                Destroy(coins_Ins[8]);
            });

            yield return new WaitForSeconds(0.05f);
            coins_Ins[9].transform.DOPath(wayPoints, 0.7f, PathType.CatmullRom).SetEase(Ease.Linear).OnComplete(() =>
            {
                Destroy(coins_Ins[9]);
            });

            yield return new WaitForSeconds(0.05f);
            coins_Ins[10].transform.DOPath(wayPoints, 0.7f, PathType.CatmullRom).SetEase(Ease.Linear).OnComplete(() =>
            {
                Destroy(coins_Ins[10]);
            });

            yield return new WaitForSeconds(0.05f);
            coins_Ins[11].transform.DOPath(wayPoints, 0.7f, PathType.CatmullRom).SetEase(Ease.Linear).OnComplete(() =>
            {
                Destroy(coins_Ins[11]);
            });
        }

        
    }
    public void On_Purchase_Failed(Product product, PurchaseFailureReason purchaseFailureReason )
    {
        Debug.Log("Purchase of " + product + " Failed due to " + purchaseFailureReason);
    }
}
