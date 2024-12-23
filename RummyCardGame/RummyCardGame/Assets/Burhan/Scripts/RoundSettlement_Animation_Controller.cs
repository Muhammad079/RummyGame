using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoundSettlement_Animation_Controller : MonoBehaviour
{
    public static RoundSettlement_Animation_Controller instance;

    public GameObject[] Win_gameCards, Lose_gameCards;
    public Transform Temp_Grid;
    public Transform Win_destination, Lose_Destination;
    public Transform screen_Movement, win_MOVE, Lose_MOVE;

    public bool start_anim;


    private void OnEnable()
    {
        instance = this;
        Win_gameCards = new GameObject[10];
        Lose_gameCards = new GameObject[10];
    }

    // Start is called before the first frame update
    void Start()
    {
        
        
    }

    // Update is called once per frame
    void Update()
    {
        if (start_anim)
        {
            start_anim = false;
            start_Animation();
        }
    }
    public void start_Animation()
    {
        StartCoroutine(waiting());
        
       
    }
    public IEnumerator waiting()
    {
        for (int i = 0; i < Win_gameCards.Length; i++)
        {
            if (Win_gameCards[i] != null)
            {
                
                Win_gameCards[i].transform.SetParent(screen_Movement);
                Win_gameCards[i].transform.DOPath(new Vector3[] { Temp_Grid.position/*, transform.position, win_MOVE.position*/, Win_destination.position }, 0.4f, PathType.CatmullRom).OnComplete(()=>
                //Win_gameCards[i].transform.DOMove(/*Win_destination.position*/transform.position, 0.2f).OnComplete(() =>
                {
                    Win_gameCards[i].transform.SetParent(Win_destination);

                });
                yield return new WaitForSeconds(0.4f);
            }
            //else
            //{
            //    break;
            //}

        }
        //yield return new WaitForSeconds(4);
        for (int i = 0; i < Lose_gameCards.Length; i++)
        {
            if (Lose_gameCards[i] != null)
            {
                
                Lose_gameCards[i].transform.SetParent(screen_Movement);
                Lose_gameCards[i].transform.DOPath(new Vector3[] { Temp_Grid.position/*, transform.position, Lose_MOVE.position*/, Lose_Destination.position }, 0.2f, PathType.CatmullRom).OnComplete(() => 
                //Lose_gameCards[i].transform.DOMove(/*Lose_Destination.position*/transform.position, 0.2f).OnComplete(() =>
                {
                    Lose_gameCards[i].transform.SetParent(Lose_Destination);
                });
                yield return new WaitForSeconds(0.4f);
            }
            //else
            //{
            //    break;
            //}
        }
        //yield return new WaitForSeconds(4);
        Win_gameCards = new GameObject[0];
        Lose_gameCards = new GameObject[0];
        Win_gameCards = new GameObject[10];
        Lose_gameCards = new GameObject[10];

    }
    //public IEnumerator Delay()
    //{
    //    yield return new WaitForSeconds(15);
    //}
}
