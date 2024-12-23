using DG.Tweening;
using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Hand_Pass_Handler : SceneLoader
{
    public GameObject Activate_Premium_Container;
    public Transform Premium_grid;
    public Transform Free_grid;
    public Transform Prize_Counting;

    public Scriptable_HandPass scriptable_HandPass;

    public Scriptable_Handpass_free scriptable_HandPass_free;
    //public static Hand_Pass_Handler _instance;

    public Text Lvl_Points, Timer;

    public event System.Action startState = null;
    public static int passLevel = 0;
    public Text Lvl_Count;
    public GameObject vertical_Line, points_Unhider, Content_Container;
    int vertical_Line_value;
    float points_Unhider_value;
    [SerializeField] private GameObject activatePremiumButton = null;
    [SerializeField]
    private Button Close_btn;
    public int Duration = 0;
    int days_left = 0;

    IEnumerator waiting()
    {
        yield return new WaitForSeconds(0.3f);
        Activate_Premium_Container.SetActive(false);
    }

    public void StartState()
    {
        StartCoroutine(nameof(SliderFillReset));
        Debug.Log("Unlocking");
        startState?.Invoke();
    }
    public void Start()
    {
        startState += Start;
        Activate_Premium_Container.transform.GetChild(1).GetChild(0).GetComponent<Button>().onClick.AddListener(() =>
        {
            Activate_Premium_Container.transform.GetChild(1).GetComponent<DOTweenAnimation>().DOPlayBackwards();
            StartCoroutine(waiting());
        });


        transform.DOLocalMoveY(0, 0.5f);
        Close_btn.onClick.AddListener(() =>
        {
            transform.DOLocalMoveY(400, 0.5f).OnComplete(() =>
            {
                Loading_Screen = GameObject.Find("Loading_Screen");
                Loading_animation = Loading_Screen.transform.GetChild(0).GetComponent<Text>();
                StartCoroutine(OnClick());
                //GameManager.instance.sceneToLoad = "Home";
                //Debug.LogError("Loading");
                //SceneManager.LoadScene("Home");
            });
        });


        if (ProfileManager.instance.currentPlayer.Hand_Pass_purchase_Check)
            activatePremiumButton.SetActive(false);
        else
            activatePremiumButton.SetActive(true);
        passLevel = ProfileManager.instance.currentPlayer.goldenCards / 16;
        Debug.Log("Hand pass level is: " + passLevel);
        int val = scriptable_HandPass.handpass_Rewards.Count;
        for (int i = 0; i < val; i++)
        {
            var a = Premium_grid.gameObject;

            if (i < Premium_grid.childCount)
            {
                a = Premium_grid.GetChild(i).gameObject;
                a.SetActive(true);

            }
            else
            {
                a = Instantiate(Premium_grid.GetChild(0).gameObject, Premium_grid);
            }
            //a.GetComponent<Premium_Reward_Unlocker>().id = scriptable_HandPass.handpass_Rewards[i].id;
            a.GetComponent<Premium_Reward_Unlocker>().reward = scriptable_HandPass.handpass_Rewards[i];
        }
        int val2 = scriptable_HandPass_free.Free_rewards.Count;
        for (int i = 0; i < val2; i++)
        {
            var f = Free_grid.gameObject;

            if (i < Free_grid.childCount)
            {
                f = Free_grid.GetChild(i).gameObject;
                f.SetActive(true);
            }
            else
            {
                f = Instantiate(Free_grid.GetChild(0).gameObject, Free_grid);
            }
            f.GetComponent<Free_rewards_container>().reward = scriptable_HandPass_free.Free_rewards[i];
        }
        int prize_Count = 0;
        if (val > val2)
        {
            prize_Count = val;
        }
        else if (val2 > val)
        {
            prize_Count = val2;
        }
        else
        {
            prize_Count = val;
        }
        for (int i = 0; i < prize_Count; i++)
        {
            if (i == 0)
            {
                Prize_Counting.GetChild(i).gameObject.SetActive(true);
            }
            else
            {
                var counter = Instantiate(Prize_Counting.GetChild(0), Prize_Counting);
                counter.GetChild(0).GetComponent<Text>().text = (counter.GetSiblingIndex() + 1).ToString();
                counter.GetChild(0).GetComponent<Kozykin.MultiLanguageItem>().strItems[0] = (counter.GetSiblingIndex() + 1).ToString();
                counter.GetChild(0).GetComponent<Kozykin.MultiLanguageItem>().strItems[1] = (counter.GetSiblingIndex() + 1).ToString();
                counter.GetChild(0).GetComponent<Kozykin.MultiLanguageItem>().text = (counter.GetSiblingIndex() + 1).ToString();
            }
        }


        if (ProfileManager.instance.currentPlayer.goldenCards > 16)
        {
            Lvl_Points.text = (ProfileManager.instance.currentPlayer.goldenCards % 16).ToString();
            Lvl_Points.GetComponent<Kozykin.MultiLanguageItem>().strItems[0] = (ProfileManager.instance.currentPlayer.goldenCards % 16).ToString();
            Lvl_Points.GetComponent<Kozykin.MultiLanguageItem>().strItems[1] = (ProfileManager.instance.currentPlayer.goldenCards % 16).ToString() + "/";
            Lvl_Points.GetComponent<Kozykin.MultiLanguageItem>().text = (ProfileManager.instance.currentPlayer.goldenCards % 16).ToString() + "/";
        }
            
        else
        {
            Lvl_Points.text = ProfileManager.instance.currentPlayer.goldenCards.ToString();
            Lvl_Points.GetComponent<Kozykin.MultiLanguageItem>().strItems[0] = ProfileManager.instance.currentPlayer.goldenCards.ToString();
            Lvl_Points.GetComponent<Kozykin.MultiLanguageItem>().strItems[1] = ProfileManager.instance.currentPlayer.goldenCards.ToString() + "/";
            Lvl_Points.GetComponent<Kozykin.MultiLanguageItem>().text = ProfileManager.instance.currentPlayer.goldenCards.ToString() + "/";
        }
            
        vertical_Line_value = 0;
        points_Unhider_value = 0;

        Lvl_Count.text = (Mathf.FloorToInt(ProfileManager.instance.currentPlayer.goldenCards / 16)).ToString();
        Lvl_Count.GetComponent<Kozykin.MultiLanguageItem>().strItems[0] = (Mathf.FloorToInt(ProfileManager.instance.currentPlayer.goldenCards / 16)).ToString();
        Lvl_Count.GetComponent<Kozykin.MultiLanguageItem>().strItems[1] = (Mathf.FloorToInt(ProfileManager.instance.currentPlayer.goldenCards / 16)).ToString();
        Lvl_Count.GetComponent<Kozykin.MultiLanguageItem>().text = (Mathf.FloorToInt(ProfileManager.instance.currentPlayer.goldenCards / 16)).ToString();

        for (int j = 0; j < (ProfileManager.instance.currentPlayer.goldenCards % 16); j++)
        {
            points_Unhider_value += 14.5f;
            points_Unhider.GetComponent<RectTransform>().offsetMin = new Vector2(points_Unhider_value, 0);
        }
        vertical_Line_value += (142 * passLevel);
        vertical_Line.GetComponent<RectTransform>().offsetMin = new Vector2(vertical_Line_value, 0);
        vertical_Line.GetComponent<RectTransform>().SetSizeWithCurrentAnchors(0, 50);




        ProfileManager.instance.currentPlayer.HandPass_Duration_Days = Duration;
        //if (ProfileManager.instance.currentPlayer.HandPass_Duration_Days == Duration)
        if (!ProfileManager.instance.currentPlayer.HandPass_First_Login)
        {

            DateTime dateTime = DateTime.Today;
            int day_of_year = dateTime.ToUniversalTime().DayOfYear;
            ProfileManager.instance.currentPlayer.HandPass_StartDate = day_of_year;
            ProfileManager.instance.currentPlayer.HandPass_First_Login = true;
        }
        int Days_Gone = DateTime.Today.ToUniversalTime().DayOfYear - ProfileManager.instance.currentPlayer.HandPass_StartDate;
        days_left = ProfileManager.instance.currentPlayer.HandPass_Duration_Days - Days_Gone;
        if (days_left == -1)
        {
            Debug.Log("Handpass Ended");
        }
        ProfileManager.instance.SaveUserData();
        //Premium_grid.parent.transform.GetComponent<RectTransform>().sizeDelta = new Vector2(Premium_grid.transform.GetComponent<Rect>().width, Premium_grid.parent.transform.GetComponent<Rect>().height);
        Invoke(nameof(refresh), 1);
    }

    private void refresh()
    {
        float width_value = Premium_grid.GetComponent<RectTransform>().rect.width;
        Debug.Log("Width value: " + width_value);
        Content_Container.GetComponent<RectTransform>().SetSizeWithCurrentAnchors(0, width_value + 30);
    }

    public override void Update()
    {
        base.Update();
        if (gameObject.activeInHierarchy)
        {
            DateTime now = DateTime.Now;
            DateTime tomorrow = DateTime.Today.AddDays(days_left);
            TimeSpan remaining = tomorrow - now;
            string time = new System.DateTime(remaining.Ticks).ToString("dD|hh:mm:ss");
            Timer.text = time;

            Timer.GetComponent<Kozykin.MultiLanguageItem>().strItems[0] = time;
            Timer.GetComponent<Kozykin.MultiLanguageItem>().strItems[1] = time;
            Timer.GetComponent<Kozykin.MultiLanguageItem>().text = time;
        }

    }
    public void Premium_pass_Purchase()
    {
        Invoke(nameof(Bug_fix), 1);
    }
    IEnumerator SliderFillReset()
    {
        Vector2 val = points_Unhider.GetComponent<RectTransform>().offsetMin;
        points_Unhider_value = 0;
        for (int j = 0; j < 16; j++)
        {
            points_Unhider_value += 14.5f;
            points_Unhider.GetComponent<RectTransform>().offsetMin = new Vector2(points_Unhider_value, 0);
            yield return new WaitForSeconds(0.025f);
        }
        points_Unhider.GetComponent<RectTransform>().offsetMin = val;
    }
    private void Bug_fix()
    {
        ProfileManager.instance.currentPlayer.Hand_Pass_purchase_Check = true;
        activatePremiumButton.SetActive(false);
        ProfileManager.instance.SaveUserData();
    }
}
