using DG.Tweening;
using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Wheel_Rotator : MonoBehaviour
{
    public GameObject Wheel;
    public Button Spin;
    public Wheel_Rotator instance;
    [HideInInspector]
    public float speed_inc, speed_dec;
    bool spin_check = false;
    [SerializeField] private GameObject detector = null;
    [SerializeField] private Prize_Display prize_Display = null;
    internal int counter;

    // Start is called before the first frame update
    void Start()
    {
        detector.GetComponent<Collider2D>().enabled = false;
        speed_inc = 0f;
        speed_dec = 15f;

        if (!SceneManager.GetActiveScene().name.Contains("GoldenSpin")) //Dont remove check
        {
            if (ProfileManager.instance.currentPlayer.spinCounts > 0)
            {
                instance.enabled = true;
            }
            else
            {
                instance.enabled = false;
            }
        }
            
    }
   
    // Update is called once per frame
    void Update()
    {
        if (spin_check == true)
        {
            if (speed_inc >= 25)
            {

                speed_dec = speed_dec - 0.05f;
                Wheel.transform.Rotate(new Vector3(0, 0, speed_dec));
                RewardItems reward = new RewardItems();
                BoxItems boxItems = new BoxItems();
                if (speed_dec < 0)
                {
                    //int finalAngle = Mathf.RoundToInt(Wheel.transform.eulerAngles.z);
                    //Debug.Log("Angle: " + finalAngle);
                    //if (finalAngle <= 13 && finalAngle >= 340)
                    //{
                    //    Debug.Log("5 gems");
                    //    reward.reward = RewardType.gems;
                    //    reward.quantity = 5;
                    //}
                    //else if (finalAngle <= 49 && finalAngle >= 17)
                    //{
                    //    Debug.Log("5K coins");
                    //    reward.reward = RewardType.coins;
                    //    reward.quantity = 5000;
                    //}
                    //else if (finalAngle <= 85 && finalAngle >= 52)
                    //{
                    //    Debug.Log("7K coins"); 
                    //    reward.reward = RewardType.coins;
                    //    reward.quantity = 7000;
                    //}
                    //else if (finalAngle <= 125 && finalAngle >= 87)
                    //{
                    //    Debug.Log("3K coins");
                    //    reward.reward = RewardType.coins;
                    //    reward.quantity = 3000;
                    //}
                    //else if (finalAngle <= 161 && finalAngle >= 129)
                    //{
                    //    Debug.Log("Silver Box");
                    //    reward.reward = RewardType.box;
                    //    reward.boxReward.boxType = BoxType.silver;
                    //    reward.quantity = 1;
                    //    boxItems.reward = RewardType.coins;
                    //    boxItems.quantity = 1000;
                    //    reward.boxReward.boxItems.Add(boxItems);  
                    //}
                    //else if (finalAngle <= 195 && finalAngle >= 164)
                    //{
                    //    Debug.Log("VIP ticket");
                    //    reward.reward = RewardType.vip;
                    //    reward.quantity = 1;
                    //}
                    //else if (finalAngle <= 230 && finalAngle >= 198)
                    //{
                    //    Debug.Log("Silver Box");
                    //    reward.reward = RewardType.box;
                    //    reward.boxReward.boxType = BoxType.silver;
                    //    reward.quantity = 1;
                    //    boxItems.reward = RewardType.coins;
                    //    boxItems.quantity = 1000;
                    //    reward.boxReward.boxItems.Add(boxItems);
                    //}
                    //else if (finalAngle <= 264 && finalAngle >= 234)
                    //{
                    //    Debug.Log("2.5K Coins");
                    //    reward.reward = RewardType.coins;
                    //    reward.quantity = 2500;
                    //}
                    //else if (finalAngle <= 301 && finalAngle >= 267)
                    //{
                    //    Debug.Log("1K Coins");
                    //    reward.reward = RewardType.coins;
                    //    reward.quantity = 1000;
                    //}
                    //else if (finalAngle <= 337 && finalAngle >= 305)
                    //{
                    //    Debug.Log("Bronze Box");
                    //    Debug.Log("Silver Box");
                    //    reward.reward = RewardType.box;
                    //    reward.boxReward.boxType = BoxType.silver;
                    //    reward.quantity = 1;
                    //    boxItems.reward = RewardType.coins;
                    //    boxItems.quantity = 1000;
                    //    reward.boxReward.boxItems.Add(boxItems);
                    //}
                    detector.GetComponent<Collider2D>().enabled = true;
                    //                    prize_Display.UpdateValues(reward);
                    spin_check = false;
                    Spin.interactable = true;
                    
                    speed_inc = 0f;
                    speed_dec = 25f;
                    //transform.localRotation = Quaternion.Euler(0,0,transform.localRotation.eulerAngles.z);
                    if(detector.GetComponent<Collider2D>().enabled)
                    {
                        StartCoroutine(wait());
                    }
                }
            }
            if (speed_inc <= 25)
            {
                speed_inc = speed_inc + 0.05f;
                Wheel.transform.Rotate(new Vector3(0, 0, speed_inc));
            }
        }


    }

    IEnumerator wait()
    {

        yield return new WaitForSeconds(1);
        detector.GetComponent<Collider2D>().enabled = false;
        GetComponent<DOTweenAnimation>().enabled = true;
        GetComponent<DOTweenAnimation>().DOPlay();
    }

    public void spinWheel()
    {
        GetComponent<DOTweenAnimation>().DOPause();
        GetComponent<DOTweenAnimation>().enabled = false;
        //ProfileManager.instance.currentPlayer.luckySpinsToday++;
        detector.GetComponent<Collider2D>().enabled = false;
        Spin.interactable = false;
        spin_check = true;
    }
}
