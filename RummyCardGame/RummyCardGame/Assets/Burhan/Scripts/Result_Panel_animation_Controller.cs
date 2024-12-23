using DG.Tweening;
using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Result_Panel_animation_Controller : SceneLoader
{
    public GameObject Win_title, Lose_title;
    public GameObject Prize_Grid;
    public GameObject temporary_reward;
    public static GameObject[] Instantiated_Rewards = new GameObject[4];
    public GameObject XP_Container, Confirm;
    public Transform Coin_destination;
    public Text Player_Coins;
    public static int coins_Won;
    public GameObject Loading_Panel;
    public PointsChecker PointsChecker;
    // Start is called before the first frame update
    void Start()
    {
        if(PhotonNetwork.CurrentRoom.Name.Contains("tutorial"))
        {
            Lose_title.SetActive(false);
        }

        for (int i = 0; i < Instantiated_Rewards.Length; i++)
        {
            if (Instantiated_Rewards[i] != null)
            {
                Instantiated_Rewards[i].SetActive(false);
                Color32 color = new Color32(255, 255, 255, 0);
                Instantiated_Rewards[i].GetComponent<Image>().color = color;
                Instantiated_Rewards[i].transform.localScale = new Vector3(1.3f, 1.3f, 1.3f);
            }
        }


        XP_Container.SetActive(false);
        XP_Container.transform.localPosition = new Vector3(0, 700, 0);

        Confirm.SetActive(false);
        Color32 color2 = new Color32(255, 255, 255, 0);
        Confirm.GetComponent<Image>().color = color2;
        Confirm.transform.localScale = new Vector3(1.3f, 1.3f, 1.3f);



        if(PointsChecker.looser == false)
        {
            Lose_title.gameObject.SetActive(false);
            Win_title.gameObject.SetActive(true);
        }
        else
        {
            Win_title.gameObject.SetActive(false);
            Lose_title.gameObject.SetActive(true);
        }


        if (Win_title.activeInHierarchy)
        {
            Win_title.transform.localPosition = new Vector3(0, 0, 0);
            //Win_title.transform.localScale = new Vector3(1.5f, 1.5f, 1.5f);
            Win_title.transform.DOScale(new Vector3(1.5f, 1.5f, 1.5f), 0.5f);
            Win_title.GetComponent<Image>().DOFade(1, 0.7f).OnComplete(() =>
            {
                Win_title.transform.DOScale(new Vector3(1, 1, 1), 0.5f).OnComplete(() =>
                {
                    Win_title.transform.DOLocalMoveY(320, 0.4f).OnComplete(() =>
                    {
                        XP_Container.SetActive(true);
                        XP_Container.transform.localScale = new Vector3(1.3f, 1.3f, 1.3f);
                        XP_Container.transform.DOMoveY(-2, 0.4f);
                        XP_Container.transform.DOScale(new Vector3(1, 1, 1), 0.5f).SetDelay(0.2f).OnComplete(() =>
                        {

                            StartCoroutine(Wait_Delay());





                        });





                    });
                });

            });
            if (PhotonNetwork.CurrentRoom.Name.Contains("tutorial"))
            {
                Confirm.GetComponent<Button>().onClick.AddListener(() => {
                    StartCoroutine(OnClick());
                    /*Loading_Panel.SetActive(true); SceneManager.LoadScene("Home");*/
                 
                });
            }
            else
            {
                Confirm.GetComponent<Button>().onClick.AddListener(() => { StartCoroutine(Coins_Animation()); });
            }
            
        }
        else if (Lose_title.activeInHierarchy)
        {
            if (PhotonNetwork.InRoom)
            {
                PhotonNetwork.LeaveRoom();
            }

            Lose_title.transform.localPosition = new Vector3(0, 0, 0);
            //Lose_title.transform.localScale = new Vector3(1.5f, 1.5f, 1.5f);
            Lose_title.transform.DOScale(new Vector3(1.5f, 1.5f, 1.5f), 0.5f);
            Lose_title.GetComponent<Image>().DOFade(1, 0.7f).OnComplete(() =>
            {
                Lose_title.transform.DOScale(new Vector3(1, 1, 1), 0.5f).OnComplete(() =>
                {
                    Lose_title.transform.DOLocalMoveY(320, 0.4f).OnComplete(() =>
                    {
                        XP_Container.SetActive(true);
                        XP_Container.transform.localScale = new Vector3(1.3f, 1.3f, 1.3f);
                        XP_Container.transform.DOMoveY(-2, 0.4f);
                        XP_Container.transform.DOScale(new Vector3(1, 1, 1), 0.5f).SetDelay(0.2f).OnComplete(() =>
                        {

                            StartCoroutine(Wait_Delay());





                        });





                    });
                });

            });
            Confirm.GetComponent<Button>().onClick.AddListener(() => {
                StartCoroutine(OnClick());
                //Loading_Panel.SetActive(true); SceneManager.LoadScene("Home");
            });
        }

        
    }
    IEnumerator Wait_Delay()
    {


        for (int i = 0; i < Instantiated_Rewards.Length; i++)
        {
            if (Instantiated_Rewards[i] != null)
            {
                Instantiated_Rewards[i].SetActive(true);
                Instantiated_Rewards[i].transform.DOScale(new Vector3(1, 1, 1), 0.2f);
                Instantiated_Rewards[i].GetComponent<Image>().DOFade(1, 0.2f);
                yield return new WaitForSeconds(0.5f);

            }
        }
        yield return new WaitForSeconds(1);
        Confirm.SetActive(true);
        Confirm.transform.DOScale(new Vector3(1, 1, 1), 0.2f);
        Confirm.GetComponent<Image>().DOFade(1, 0.2f);


    }
    // Update is called once per frame
    public override void Update()
    {
        base.Update();
    }
    public IEnumerator Coins_Animation()
    {
        GameObject[] Moveables = new GameObject[6];
        for (int i = 0; i < Moveables.Length; i++)
        {
            GameObject a = new GameObject();
            a.transform.SetParent(Confirm.transform);
            a.AddComponent<Image>().sprite = Coin_destination.GetComponent<Image>().sprite; //gem image changed
            a.transform.position = Confirm.transform.position;
            a.transform.localScale = new Vector3(2.5f,2.5f,2.5f);
            a.transform.GetComponent<RectTransform>().sizeDelta = new Vector2(30, 30);
            a.GetComponent<Image>().preserveAspect = true;
            a.AddComponent<Canvas>().overrideSorting = true;
            a.GetComponent<Canvas>().sortingOrder = 10;
            Moveables[i] = a;

        }


        Vector3[] waypoints = new Vector3[3];
        waypoints[0] = Confirm.transform.position;
        waypoints[1] = transform.position;
        waypoints[2] = Coin_destination.position;
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

            //ProfileManager.instance.currentPlayer.coins += coins_Won;
            Player_Coins.text = ProfileManager.instance.currentPlayer.coins.ToString();


        });
        yield return new WaitForSeconds(0.05f);

        Moveables[5].transform.DOPath(waypoints, 0.7f, PathType.CatmullRom).SetEase(Ease.Linear).OnComplete(() =>
        {
            Destroy(Moveables[5].gameObject);
            Loading_Panel.SetActive(true);


        });
        yield return new WaitForSeconds(1.5f);

        //SceneManager.LoadScene("Home");
        StartCoroutine(OnClick());
    }
}