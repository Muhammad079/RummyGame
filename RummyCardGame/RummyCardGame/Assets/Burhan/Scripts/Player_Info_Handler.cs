using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player_Info_Handler : MonoBehaviour
{
    [SerializeField] Text[] Amount_Display = null;
    [SerializeField] Button[] Add_btn = null;
    //[SerializeField] Image Shop_Coin_btn = null, Shop_gem_btn = null;
    [SerializeField] Text Level_Display = null;
    [SerializeField] GameObject Shop = null/*, Coins_partition = null, Gems_Partition = null, VIP_badge = null, Timer=null, Result_Panel = null;*/;
    //[SerializeField] Sprite[] Shop_coin_sprite_selection = null, Shop_gem_sprite_selection = null;

    // Start is called before the first frame update
    void Start()
    {
        //Amount_Display[0].text = ProfileManager.instance.currentPlayer.coins.ToString();
        //Amount_Display[1].text = ProfileManager.instance.currentPlayer.gems.ToString();

        Add_btn[0].onClick.AddListener(() => {
            Shop.SetActive(true);
            //Shop_Coin_btn.sprite = Shop_coin_sprite_selection[1];
            //Shop_gem_btn.sprite = Shop_gem_sprite_selection[0];
            //Coins_partition.SetActive(true);
            //Gems_Partition.SetActive(false);
        });
        Add_btn[1].onClick.AddListener(() => {
            Shop.SetActive(true);
            //Shop_Coin_btn.sprite = Shop_coin_sprite_selection[0];
            //Shop_gem_btn.sprite = Shop_gem_sprite_selection[1];
            //Coins_partition.SetActive(false);
            //Gems_Partition.SetActive(true);
        });
    }

    // Update is called once per frame
    void Update()
    {
        //Amount_Display[0].text = ProfileManager.instance.currentPlayer.coins.ToString();
        Amount_Display[0].GetComponent<Kozykin.MultiLanguageItem>().strItems[0] = ProfileManager.instance.currentPlayer.coins.ToString();
        Amount_Display[0].GetComponent<Kozykin.MultiLanguageItem>().strItems[1] = ProfileManager.instance.currentPlayer.coins.ToString();
        Amount_Display[0].GetComponent<Kozykin.MultiLanguageItem>().text = ProfileManager.instance.currentPlayer.coins.ToString();

        //Amount_Display[1].text = ProfileManager.instance.currentPlayer.gems.ToString();
        Amount_Display[1].GetComponent<Kozykin.MultiLanguageItem>().strItems[0] = ProfileManager.instance.currentPlayer.gems.ToString();
        Amount_Display[1].GetComponent<Kozykin.MultiLanguageItem>().strItems[1] = ProfileManager.instance.currentPlayer.gems.ToString();
        Amount_Display[1].GetComponent<Kozykin.MultiLanguageItem>().text = ProfileManager.instance.currentPlayer.gems.ToString();

        Level_Display.transform.parent.GetComponent<Image>().fillAmount = (float)ProfileManager.instance.currentPlayer.xp / (float)GameManager.instance.LevelUpData.levelsData[ProfileManager.instance.currentPlayer.level].xpReq; //(ProfileManager.instance.currentPlayer.xp % 10) *0.1f;

        //Level_Display.text = "Level "+ProfileManager.instance.currentPlayer.level.ToString();
        Level_Display.GetComponent<Kozykin.MultiLanguageItem>().strItems[0] = "Level " + ProfileManager.instance.currentPlayer.level.ToString();
        Level_Display.GetComponent<Kozykin.MultiLanguageItem>().strItems[1] = "مستوى " + ProfileManager.instance.currentPlayer.level.ToString();
        Level_Display.GetComponent<Kozykin.MultiLanguageItem>().text = "مستوى " + ProfileManager.instance.currentPlayer.level.ToString();

        //if (Shop.activeInHierarchy || Result_Panel.activeInHierarchy)
        //{
        //    VIP_badge.GetComponent<Canvas>().sortingOrder = -1;
        //    Timer.GetComponent<Canvas>().sortingOrder = -1;
        //}
        //else
        //{
        //    VIP_badge.GetComponent<Canvas>().sortingOrder = 1;
        //    Timer.GetComponent<Canvas>().sortingOrder = 1;
        //}
    }
}
