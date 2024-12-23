using Photon.Pun;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class GamePlayPlayer : MonoBehaviour
{
    public Sprite Yellow_Overlay, Blue_Overlay, Green_Overlay;
    public static GamePlayPlayer instance;
    [SerializeField] private Text t_Name = null;
    [SerializeField] private Text t_Score = null;
    public Transform gridParent = null;
    [SerializeField] private Button knockOut = null;
    public Sprite[] Hand_btn_sprites;
    [HideInInspector] public int Hand_btn_sprites_selector;

    public InGamePlayer player = new InGamePlayer();

    int first_Hand_wait = 0;
    bool first_time = false;


    private void Awake()
    {
        instance = this;
    }
    private void Start()
    {
        //Color_Counter = 0;
        first_Hand_wait = 0;
        Hand_btn_sprites_selector = 0;
        InitialStatus();
        Gameplay_Manager.resetRound += InitialStatus;
        CardManager.instance.performComparison += CheckSequences;
        Gameplay_Manager.instance.mainPlayer = this;
        InvokeRepeating(nameof(Logic), 0.5f, 0.5f);
        //InvokeRepeating(nameof(Sq_Color_Logic_RESET), 0.5f, 0.5f);
        InvokeRepeating(nameof(Sq_Color_General), 0.5f, 0.5f);
        InvokeRepeating(nameof(Sq_Color_Logic3_NEW), 0.5f, 0.5f);
        InvokeRepeating(nameof(Sq_Color_Logic1_NEW), 0.5f, 0.5f);
        InvokeRepeating(nameof(Sq_Color_Logic2), 0.5f, 0.5f);
    }

    private void Sq_Color_General()
    {
        CardView[] cards_In_hand= new CardView[gridParent.childCount];
        for (int i = 0; i < gridParent.childCount; i++)
        {
            if (!gridParent.GetChild(i).name.Contains("Dummy"))
            {
                cards_In_hand[i] = gridParent.GetChild(i).GetComponent<CardView>();
            }
        }
        for (int i = 0; i < cards_In_hand.Length; i++)
        {
            if (!gridParent.GetChild(i).name.Contains("Dummy"))
            {
                if (cards_In_hand[i].card.matched)
                {
                    if (i < 3)
                    {
                        cards_In_hand[i].transform.GetChild(0).GetComponent<Image>().sprite = Blue_Overlay;
                    }
                    else if (i >= 3 && i < 6)
                    {
                        cards_In_hand[i].transform.GetChild(0).GetComponent<Image>().sprite = Blue_Overlay;
                    }
                    else if (i >= 6 && i < 9)
                    {
                        cards_In_hand[i].transform.GetChild(0).GetComponent<Image>().sprite = Yellow_Overlay;
                    }
                }
            }
           
        }
    }

    public void Sq_Color_Logic_RESET()
    {
        for (int i = 0; i < gridParent.childCount; i++)
        {
            if (!gridParent.GetChild(i).name.Contains("Dummy"))
            {
                gridParent.GetChild(i).GetChild(0).gameObject.SetActive(false);
            }
        }
    }

    private void Sq_Color_Logic3_NEW()
    {
        CardView first = null;
        int color = 0;
        for (int i = 0; i < gridParent.childCount; i++)
        {
            CardView second = null;
            CardView third = null;
            if (!gridParent.GetChild(i).name.Contains("Dummy"))
            {
                first = gridParent.GetChild(i).GetComponent<CardView>();
                int second_card = first.transform.GetSiblingIndex() + 1;



                if (second_card < gridParent.childCount)
                {
                    if (!gridParent.GetChild(second_card).name.Contains("Dummy"))
                    {
                        second = gridParent.GetChild(second_card).GetComponent<CardView>();
                        int third_card = second.transform.GetSiblingIndex() + 1;
                        if (third_card < gridParent.childCount)
                        {
                            if (!gridParent.GetChild(third_card).name.Contains("Dummy"))
                            {
                                third = gridParent.GetChild(third_card).GetComponent<CardView>();
                            }

                        }
                    }

                    
                }
            }
            

            

            



            if (second != null && third != null)
            {
                if (first.card.no == second.card.no && second.card.no == third.card.no && third.card.no == first.card.no
                                //|| first.Carddec == "Joker" && second.card.no == third.card.no && third.card.no == second.card.no
                                //|| second.Carddec == "Joker" && first.card.no == third.card.no && third.card.no == first.card.no
                                //|| third.Carddec == "Joker" && first.card.no == second.card.no && second.card.no == first.card.no)
                                )
                {
                    if (color == 0)
                    {
                        first.transform.GetChild(0).gameObject.SetActive(true);
                        second.transform.GetChild(0).gameObject.SetActive(true);
                        third.transform.GetChild(0).gameObject.SetActive(true);
                        first.transform.GetChild(0).GetComponent<Image>().sprite = Blue_Overlay;//.color = Color.blue;
                        second.transform.GetChild(0).GetComponent<Image>().sprite = Blue_Overlay;//.color = Color.blue;
                        third.transform.GetChild(0).GetComponent<Image>().sprite = Blue_Overlay;//.color = Color.blue;
                        i += 2;
                        color = 1;
                    }
                    else if (color == 1)
                    {
                        first.transform.GetChild(0).gameObject.SetActive(true);
                        second.transform.GetChild(0).gameObject.SetActive(true);
                        third.transform.GetChild(0).gameObject.SetActive(true);
                        first.transform.GetChild(0).GetComponent<Image>().sprite = Yellow_Overlay;//.color = Color.blue;
                        second.transform.GetChild(0).GetComponent<Image>().sprite = Yellow_Overlay;//.color = Color.blue;
                        third.transform.GetChild(0).GetComponent<Image>().sprite = Yellow_Overlay;//.color = Color.blue;
                        i += 2;
                        color = 2;
                    }
                    else if (color == 2)
                    {
                        first.transform.GetChild(0).gameObject.SetActive(true);
                        second.transform.GetChild(0).gameObject.SetActive(true);
                        third.transform.GetChild(0).gameObject.SetActive(true);
                        first.transform.GetChild(0).GetComponent<Image>().sprite = Green_Overlay;//.color = Color.blue;
                        second.transform.GetChild(0).GetComponent<Image>().sprite = Green_Overlay;//.color = Color.blue;
                        third.transform.GetChild(0).GetComponent<Image>().sprite = Green_Overlay;//.color = Color.blue;
                        i += 2;
                        color = 0;
                    }
                }
                else if (first.Carddec == second.Carddec && second.Carddec == third.Carddec && third.Carddec == first.Carddec
                        //|| first.Carddec == "Joker" && second.Carddec == third.Carddec && third.Carddec == second.Carddec
                        //|| second.Carddec == "Joker" && first.Carddec == third.Carddec && third.Carddec == first.Carddec
                        //|| third.Carddec == "Joker" && second.Carddec == first.Carddec && first.Carddec == second.Carddec)
                        )
                {
                    if (first.card.no == second.card.no + 1 && second.card.no == third.card.no + 1 && third.card.no == first.card.no - 2
                        || first.card.no == second.card.no - 1 && second.card.no == third.card.no - 1 && third.card.no == first.card.no + 2
                        /*|| first.CardName.Contains("Joker") && */)
                    {
                        if (color == 0)
                        {
                            first.transform.GetChild(0).gameObject.SetActive(true);
                            second.transform.GetChild(0).gameObject.SetActive(true);
                            third.transform.GetChild(0).gameObject.SetActive(true);
                            first.transform.GetChild(0).GetComponent<Image>().sprite = Blue_Overlay;//.color = Color.blue;
                            second.transform.GetChild(0).GetComponent<Image>().sprite = Blue_Overlay;//.color = Color.blue;
                            third.transform.GetChild(0).GetComponent<Image>().sprite = Blue_Overlay;//.color = Color.blue;
                            i += 2;
                            color = 1;
                        }
                        else if (color == 1)
                        {
                            first.transform.GetChild(0).gameObject.SetActive(true);
                            second.transform.GetChild(0).gameObject.SetActive(true);
                            third.transform.GetChild(0).gameObject.SetActive(true);
                            first.transform.GetChild(0).GetComponent<Image>().sprite = Yellow_Overlay;//.color = Color.blue;
                            second.transform.GetChild(0).GetComponent<Image>().sprite = Yellow_Overlay;//.color = Color.blue;
                            third.transform.GetChild(0).GetComponent<Image>().sprite = Yellow_Overlay;//.color = Color.blue;
                            i += 2;
                            color = 2;
                        }
                        else if (color == 2)
                        {
                            first.transform.GetChild(0).gameObject.SetActive(true);
                            second.transform.GetChild(0).gameObject.SetActive(true);
                            third.transform.GetChild(0).gameObject.SetActive(true);
                            first.transform.GetChild(0).GetComponent<Image>().sprite = Green_Overlay;//.color = Color.blue;
                            second.transform.GetChild(0).GetComponent<Image>().sprite = Green_Overlay;//.color = Color.blue;
                            third.transform.GetChild(0).GetComponent<Image>().sprite = Green_Overlay;//.color = Color.blue;
                            i += 2;
                            color = 0;
                            //if(i>gridParent.childCount)
                        }
                    }


                }
            }

            
        }
    }

    private void Sq_Color_Logic1_NEW()
    {
        CardView first = null;
        bool next_Color = false;
        for (int i = 0; i < gridParent.childCount; i++)
        {
            CardView second = null;
            CardView third = null;
            CardView forth = null;
            if (!gridParent.GetChild(i).name.Contains("Dummy"))
            {
                first = gridParent.GetChild(i).GetComponent<CardView>();
                int second_card = first.transform.GetSiblingIndex() + 1;



                if (second_card < gridParent.childCount)
                {
                    if (!gridParent.GetChild(second_card).name.Contains("Dummy"))
                    {
                        second = gridParent.GetChild(second_card).GetComponent<CardView>();
                        int third_card = second.transform.GetSiblingIndex() + 1;
                        if (third_card < gridParent.childCount)
                        {
                            if (!gridParent.GetChild(third_card).name.Contains("Dummy"))
                            {
                                third = gridParent.GetChild(third_card).GetComponent<CardView>();
                                int forth_card = third.transform.GetSiblingIndex() + 1;
                                if (forth_card < gridParent.childCount)
                                {
                                    if (!gridParent.GetChild(forth_card).name.Contains("Dummy"))
                                    {
                                        forth = gridParent.GetChild(forth_card).GetComponent<CardView>();
                                    }

                                }
                            }

                            
                        }
                    }



                    

                }
            }

            
            

            if (second != null && third != null && forth != null)
            {
                if (first.card.no == second.card.no && second.card.no == third.card.no && third.card.no == forth.card.no && forth.card.no == first.card.no
                            //|| first.Carddec == "Joker" && second.card.no == third.card.no && third.card.no == forth.card.no && forth.card.no == second.card.no
                            //|| second.Carddec == "Joker" && first.card.no == third.card.no && third.card.no == forth.card.no && forth.card.no == first.card.no
                            //|| third.Carddec == "Joker" && first.card.no == second.card.no && second.card.no == forth.card.no && forth.card.no == first.card.no
                            //|| forth.Carddec == "Joker" && first.card.no == second.card.no && second.card.no == third.card.no && third.card.no == first.card.no
                            )
                {
                    if (!next_Color)
                    {
                        first.transform.GetChild(0).gameObject.SetActive(true);
                        second.transform.GetChild(0).gameObject.SetActive(true);
                        third.transform.GetChild(0).gameObject.SetActive(true);
                        forth.transform.GetChild(0).gameObject.SetActive(true);
                        first.transform.GetChild(0).GetComponent<Image>().sprite = Green_Overlay;//.color = Color.blue;
                        second.transform.GetChild(0).GetComponent<Image>().sprite = Green_Overlay;//.color = Color.blue;
                        third.transform.GetChild(0).GetComponent<Image>().sprite = Green_Overlay;//.color = Color.blue;
                        forth.transform.GetChild(0).GetComponent<Image>().sprite = Green_Overlay;//.color = Color.blue;
                        i += 3;
                        next_Color = true;
                    }
                    else
                    {
                        first.transform.GetChild(0).gameObject.SetActive(true);
                        second.transform.GetChild(0).gameObject.SetActive(true);
                        third.transform.GetChild(0).gameObject.SetActive(true);
                        forth.transform.GetChild(0).gameObject.SetActive(true);
                        first.transform.GetChild(0).GetComponent<Image>().sprite = Blue_Overlay;//.color = Color.green;
                        second.transform.GetChild(0).GetComponent<Image>().sprite = Blue_Overlay;//.color = Color.green;
                        third.transform.GetChild(0).GetComponent<Image>().sprite = Blue_Overlay;//.color = Color.green;
                        forth.transform.GetChild(0).GetComponent<Image>().sprite = Blue_Overlay;//.color = Color.green;
                        next_Color = false;
                    }

                }
            }
            
        }
    }

    public void Sq_Color_Logic1()
    {
        CardView first = null;
        CardView second = null;
        CardView third = null;
        CardView forth = null;
        bool next_Color = false ;

        for (int i = 0; i < gridParent.childCount; i++)
        {
            if (!gridParent.GetChild(i).name.Contains("Dummy"))
            {
                if (gridParent.GetChild(i).GetComponent<CardView>().card.matched)
                {
                    if (first == null)
                    {
                        first = gridParent.GetChild(i).GetComponent<CardView>();
                        if(next_Color)
                            first.transform.GetChild(0).GetComponent<Image>().color = Color.white;
                    }
                    else if (second == null)
                    {
                        second = gridParent.GetChild(i).GetComponent<CardView>();
                        if (next_Color)
                            second.transform.GetChild(0).GetComponent<Image>().color = Color.white;
                    }
                    else if (third == null)
                    {
                        third = gridParent.GetChild(i).GetComponent<CardView>();
                        if (next_Color)
                            third.transform.GetChild(0).GetComponent<Image>().color = Color.white;
                    }
                    else if (forth == null)
                    {
                        forth = gridParent.GetChild(i).GetComponent<CardView>();

                        if(forth.transform.GetSiblingIndex()-1 != third.transform.GetSiblingIndex())
                        {
                            forth = null;
                        }

                        if (next_Color)
                            forth.transform.GetChild(0).GetComponent<Image>().color = Color.white;
                    }

                    if (forth != null)
                    {
                        if (first.card.no == second.card.no && second.card.no == third.card.no && third.card.no == forth.card.no && forth.card.no == first.card.no
                            || first.Carddec == "Joker" && second.card.no == third.card.no && third.card.no == forth.card.no && forth.card.no == second.card.no
                            || second.Carddec == "Joker" && first.card.no == third.card.no && third.card.no == forth.card.no && forth.card.no == first.card.no
                            || third.Carddec == "Joker" && first.card.no == second.card.no && second.card.no == forth.card.no && forth.card.no == first.card.no
                            || forth.Carddec == "Joker" && first.card.no == second.card.no && second.card.no == third.card.no && third.card.no == first.card.no)
                        {
                            if(!next_Color)
                            {
                                first.transform.GetChild(0).GetComponent<Image>().sprite = Blue_Overlay;//.color = Color.blue;
                                second.transform.GetChild(0).GetComponent<Image>().sprite = Blue_Overlay;//.color = Color.blue;
                                third.transform.GetChild(0).GetComponent<Image>().sprite = Blue_Overlay;//.color = Color.blue;
                                forth.transform.GetChild(0).GetComponent<Image>().sprite = Blue_Overlay;//.color = Color.blue;
                                first = null;
                                second = null;
                                third = null;
                                forth = null;
                                next_Color = true;
                                Debug.Log("Color sequence: first");
                                Debug.Log("Color sequence: Card: " + first);
                                Debug.Log("Color sequence: Card: " + second);
                                Debug.Log("Color sequence: Card: " + third);
                                Debug.Log("Color sequence: Card: " + forth);
                            }
                            else if (next_Color)
                            {
                                first.transform.GetChild(0).GetComponent<Image>().sprite = Green_Overlay;//.color = Color.green;
                                second.transform.GetChild(0).GetComponent<Image>().sprite = Green_Overlay;//.color = Color.green;
                                third.transform.GetChild(0).GetComponent<Image>().sprite = Green_Overlay;//.color = Color.green;
                                forth.transform.GetChild(0).GetComponent<Image>().sprite = Green_Overlay;//.color = Color.green;
                                first = null;
                                second = null;
                                third = null;
                                forth = null;
                                next_Color = false;
                                Debug.Log("Color sequence: second");
                                Debug.Log("Color sequence: Card: " + first);
                                Debug.Log("Color sequence: Card: " + second);
                                Debug.Log("Color sequence: Card: " + third);
                                Debug.Log("Color sequence: Card: " + forth);
                                break;
                            }

                        }
                        
                    }
                }
            }
        }
    
    } //Prev Backup C_Logic1

    public void Sq_Color_Logic2()
    {
        int logic2_count = 0;

        for (int i = 0; i < gridParent.childCount; i++)
        {
            if (!gridParent.GetChild(i).name.Contains("Dummy"))
            {

                if (gridParent.GetChild(0).GetComponent<CardView>().Carddec.Equals(gridParent.GetChild(i).GetComponent<CardView>().Carddec))
                {
                    //Debug.Log("Match Found: " + gridParent.GetChild(i).GetComponent<CardView>().Carddec);
                    logic2_count++;
                    //Debug.Log("Match Found: " + logic2_count);

                }
                if (gridParent.GetChild(i).GetComponent<CardView>().card.no == 0)
                {
                    logic2_count++;
                }
            }
        }
        if (logic2_count == 9)
        {
            for (int i = 0; i < logic2_count; i++)
            {
                if (!gridParent.GetChild(i).name.Contains("Dummy"))
                {
                    if (gridParent.GetChild(0).GetComponent<CardView>().Carddec.Equals(gridParent.GetChild(i).GetComponent<CardView>().Carddec))
                    {
                        gridParent.GetChild(i).GetChild(0).GetComponent<Image>().sprite = Blue_Overlay;//.color = Color.blue;

                    }
                    else
                    {
                        gridParent.GetChild(i).GetChild(0).GetComponent<Image>().sprite = Yellow_Overlay;//.color = Color.white;
                    }

                    if (gridParent.GetChild(i).GetComponent<CardView>().card.no == 0)
                    {
                        gridParent.GetChild(i).GetChild(0).GetComponent<Image>().sprite = Blue_Overlay;//.color = Color.blue;
                    }

                }

            }
        }
    }


    public void Sq_Color_Logic3()
    {
        int Color_Counter = 0;
        

        CardView first = null;
        CardView second = null;
        CardView third = null;
        //CardView forth = null;

        for (int i = 0; i < gridParent.childCount; i++)
        {
            if (!gridParent.GetChild(i).name.Contains("Dummy"))
            {

                //if (gridParent.GetChild(0).GetComponent<CardView>().Carddec.Equals(gridParent.GetChild(i).GetComponent<CardView>().Carddec))
                //{
                //    //Debug.Log("Match Found: " + gridParent.GetChild(i).GetComponent<CardView>().Carddec);
                //    logic2_count++;
                //    //Debug.Log("Match Found: " + logic2_count);

                //}
                //if (gridParent.GetChild(i).GetComponent<CardView>().card.no == 0)
                //{
                //    logic2_count++;
                //}



                if (gridParent.GetChild(i).GetComponent<CardView>().card.matched)
                {
                    Color_Counter++;
                    if (first == null)
                    {
                        first = gridParent.GetChild(i).GetComponent<CardView>();
                    }
                    else if (second == null)
                    {
                        second = gridParent.GetChild(i).GetComponent<CardView>();
                    }
                    else if (third == null)
                    {
                        third = gridParent.GetChild(i).GetComponent<CardView>();
                    }
                    if (Color_Counter == 3)
                    {
                        first.transform.GetChild(0).GetComponent<Image>().sprite = Yellow_Overlay;//.color = Color.white;
                        second.transform.GetChild(0).GetComponent<Image>().sprite = Yellow_Overlay;//.color = Color.white;
                        third.transform.GetChild(0).GetComponent<Image>().sprite = Yellow_Overlay;//.color = Color.white;
                        first = null;
                        second = null;
                        third = null;
                    }
                    else if (Color_Counter == 6)
                    {
                        first.transform.GetChild(0).GetComponent<Image>().sprite = Green_Overlay;//.color = Color.green;
                        second.transform.GetChild(0).GetComponent<Image>().sprite = Green_Overlay;//.color = Color.green;
                        third.transform.GetChild(0).GetComponent<Image>().sprite = Green_Overlay;//.color = Color.green;
                        first = null;
                        second = null;
                        third = null;
                    }
                    else if (Color_Counter == 9)
                    {
                        first.transform.GetChild(0).GetComponent<Image>().sprite = Blue_Overlay;//.color = Color.blue;
                        second.transform.GetChild(0).GetComponent<Image>().sprite = Blue_Overlay;//.color = Color.blue;
                        third.transform.GetChild(0).GetComponent<Image>().sprite = Blue_Overlay;//.color = Color.blue;
                        first = null;
                        second = null;
                        third = null;
                    }
                }


                




            }

        }





        //if (logic2_count == 9)
        //{
        //    for (int i = 0; i < logic2_count; i++)
        //    {
        //        if (!gridParent.GetChild(i).name.Contains("Dummy"))
        //        {
        //            if (gridParent.GetChild(0).GetComponent<CardView>().Carddec.Equals(gridParent.GetChild(i).GetComponent<CardView>().Carddec))
        //            {
        //                gridParent.GetChild(i).GetChild(0).GetComponent<Image>().color = Color.blue;

        //            }
        //            else
        //            {
        //                gridParent.GetChild(i).GetChild(0).GetComponent<Image>().color = Color.white;
        //            }

        //            if (gridParent.GetChild(i).GetComponent<CardView>().card.no == 0)
        //            {
        //                gridParent.GetChild(i).GetChild(0).GetComponent<Image>().color = Color.blue;
        //            }


        //        }

        //    }
        //}






    } //Prev Backup C_Logic3


    private void Logic()
    {
        int Count = 0, logic2_count = 0;

        for (int i = 0; i < gridParent.childCount; i++)
        {
            if(!gridParent.GetChild(i).name.Contains("Dummy"))
            {
                if (gridParent.GetChild(i).GetChild(0).gameObject.activeInHierarchy)
                {
                    Count++;
                }
            }
            
        }

        for (int i = 0; i < gridParent.childCount; i++)
        {
            if (!gridParent.GetChild(i).name.Contains("Dummy"))
            {
                if (gridParent.GetChild(0).GetComponent<CardView>().Carddec.Equals(gridParent.GetChild(i).GetComponent<CardView>().Carddec))
                {
                    //Debug.Log("Match Found: " + gridParent.GetChild(i).GetComponent<CardView>().Carddec);
                    logic2_count++;
                    //Debug.Log("Match Found: " + logic2_count);

                }
                if (gridParent.GetChild(i).GetComponent<CardView>().card.no == 0)
                {
                    logic2_count++;
                }
            }
        }
        ScoreChecker();

        //if (logic2_count == CardManager.instance.transform.childCount)
        if (logic2_count == 9)
        {
            for (int i = 0; i < logic2_count; i++)
            {
                if (!gridParent.GetChild(i).name.Contains("Dummy"))
                {
                    if (gridParent.GetChild(0).GetComponent<CardView>().Carddec.Equals(gridParent.GetChild(i).GetComponent<CardView>().Carddec))
                    {
                        gridParent.GetChild(i).GetChild(0).gameObject.SetActive(true);
                        gridParent.GetChild(i).GetComponent<CardView>().InSequence();

                    }
                    else
                    {
                        gridParent.GetChild(i).GetChild(0).gameObject.SetActive(false);
                    }

                    if(gridParent.GetChild(i).GetComponent<CardView>().card.no==0)
                    {
                        gridParent.GetChild(i).GetChild(0).gameObject.SetActive(true);
                        
                        CardView card_COPY = gridParent.GetChild(0).GetComponent<CardView>();
                        gridParent.GetChild(i).GetComponent<CardView>().Carddec = card_COPY.Carddec;
                        gridParent.GetChild(i).GetComponent<CardView>().card.no = 13;
                        gridParent.GetChild(i).GetComponent<CardView>().InSequence();
                    }

                }

            }



            if (Gameplay_Manager.instance.isTurn)
            {
                first_Hand_wait++;
                if(first_time)
                {
                    first_time = false;
                    knockOut.gameObject.SetActive(true);
                    knockOut.transform.GetComponent<Image>().sprite = Hand_btn_sprites[2];
                    
                }

                
            }
            else
            {
                knockOut.gameObject.SetActive(false);
                if(first_Hand_wait >0)
                {
                    first_time = true;
                }
            }
        }

        //else if (Count >= CardManager.instance.transform.childCount)
        else if (Count == 9
            || Count == 10)
        {
            if (Gameplay_Manager.instance.isTurn)
            {
                first_Hand_wait++;
                if (first_time)
                {
                    knockOut.gameObject.SetActive(true);
                    knockOut.transform.GetComponent<Image>().sprite = Hand_btn_sprites[0];
                }
            }
            else
            {
                knockOut.gameObject.SetActive(false);
                if (first_Hand_wait > 0)
                {
                    first_time = true;
                }
            }

        }
        else if (player.score == 1)
        //else if (Count==8)
        {
            Debug.Log("Current Player Score: " + player.score);
            if (Gameplay_Manager.instance.isTurn)
            {
                first_Hand_wait++;
                if (first_time)
                {
                    knockOut.gameObject.SetActive(true);
                    knockOut.transform.GetComponent<Image>().sprite = Hand_btn_sprites[1];
                }
            }
            else
            {
                knockOut.gameObject.SetActive(false);
                if (first_Hand_wait > 0)
                {
                    first_time = true;
                }
            }

        }
        //else
        //{
        //    knockOut.gameObject.SetActive(false);
        //    first_Hand_wait = 0;
        //    first_time = false;
        //}
        if(!Gameplay_Manager.instance.isTurn)
        {
            knockOut.gameObject.SetActive(false);
        }

        Debug.Log("First Hand Wait Value: " + first_Hand_wait);
        Debug.Log("First Hand Wait bool: " + first_time);
    }
    void InitialStatus()
    {
        player.ID = ProfileManager.instance.currentPlayer.id;
        knockOut.gameObject.SetActive(false);
    }
    public void ScoreChecker()
    {
        player.score = 0;
        for (int n = 0; n < gridParent.childCount; n++)
        {
            if (gridParent.GetChild(n).GetComponent<CardView>())
                if (!gridParent.GetChild(n).GetComponent<CardView>().card.matched) 
                {
                    player.score += gridParent.GetChild(n).GetComponent<CardView>().card.no;
                }
        }
        //t_Score.text = player.score + "/10";
        t_Score.text = Gameplay_Manager.instance.RSPoints_container;   // this line was on but Ahmed commented it
        if (player.score <= 1)
        {
            if (Gameplay_Manager.instance.isTurn)
            {
                //knockOut.gameObject.SetActive(true);
                //knockOut.transform.GetComponent<Image>().sprite = Hand_btn_sprites[1];
            }
            else
            {
                //knockOut.gameObject.SetActive(false);
            }

        }
        else
        {
            //knockOut.gameObject.SetActive(false);
        }
    }

    private void CheckSequences()
    {
        player.seq = 0;
        for (int n = 2; n < gridParent.childCount - 2; n++)
        {
            if (n - 2 >= 0 && n + 2 < gridParent.childCount)
            {
                CardView current = gridParent.GetChild(n).GetComponent<CardView>();
                CardView firstPrev = gridParent.GetChild(n - 1).GetComponent<CardView>();
                CardView secondPrev = gridParent.GetChild(n - 2).GetComponent<CardView>();
                CardView firstNext = gridParent.GetChild(n + 1).GetComponent<CardView>();
                CardView secondNext = gridParent.GetChild(n + 2).GetComponent<CardView>();

                if (!CheckNumberMatch(firstPrev, current, secondPrev))
                {
                    if (!CheckDeckMatch(firstPrev, current, secondPrev))
                    {
                        if (!CheckNumberMatch(firstNext, current, secondNext))
                        {
                            if (!CheckNumberMatch(firstNext, current, firstPrev))
                            {
                                if (!CheckDeckMatch(firstNext, current, firstPrev))
                                {
                                    if (CheckDeckMatch(firstNext, current, secondNext))
                                        n += 2;
                                }
                                else n += 1;
                            }
                            else n += 1;
                        }
                        else n += 2;
                    }
                }
            }
            else if (n == gridParent.childCount - 2)
            {
                Debug.Log("Second last Checking");
                CardView current = gridParent.GetChild(n).GetComponent<CardView>();
                CardView firstPrev = gridParent.GetChild(n - 1).GetComponent<CardView>();
                CardView secondPrev = gridParent.GetChild(n - 2).GetComponent<CardView>();
                CardView firstNext = gridParent.GetChild(n + 1).GetComponent<CardView>();
                if (!CheckNumberMatch(firstPrev, current, secondPrev))
                {
                    if (!CheckDeckMatch(firstPrev, current, secondPrev))
                    {
                        if (!CheckNumberMatch(firstPrev, current, firstNext))
                        {
                            if (CheckDeckMatch(firstPrev, current, firstNext))
                            {
                                n += 1;
                            }
                        }
                        else n += 1;
                    }
                }
            }
            else if (n == gridParent.childCount - 1)
            {
                Debug.Log("Last Checking");
                CardView current = gridParent.GetChild(n).GetComponent<CardView>();
                CardView firstPrev = gridParent.GetChild(n - 1).GetComponent<CardView>();
                CardView secondPrev = gridParent.GetChild(n - 2).GetComponent<CardView>();
                if (!CheckNumberMatch(firstPrev, current, secondPrev))
                {
                    CheckDeckMatch(firstPrev, current, secondPrev);
                }
            }
        }
        player.cards.Clear();
        player.actorNo = PhotonNetwork.LocalPlayer.ActorNumber;
        for (int n = 0; n < gridParent.childCount; n++)
        {
            player.cards.Add(gridParent.GetChild(n).GetComponent<CardView>().card);
        }
        for (int n = 0; n < Gameplay_Manager.instance.totalPlayers.Count; n++)
        {
            if (player.actorNo == Gameplay_Manager.instance.totalPlayers[n].actorNo)
            {
                Gameplay_Manager.instance.totalPlayers[n] = player;
                break;
            }
        }

    }
    bool CheckNumberMatch(CardView first, CardView current, CardView second)
    {
        if (first.card.no == current.card.no && second.card.no == current.card.no
            || first.Carddec == "Joker" && second.card.no == current.card.no
            || second.Carddec == "Joker" && first.card.no == current.card.no
            || current.Carddec == "Joker" && first.card.no == second.card.no)
        {

            if(first.Carddec == "Joker" && first.card.no == 0 && second.card.no == current.card.no)
            {
                first.card.no = current.card.no;
            }
            else if (second.Carddec == "Joker" && second.card.no == 0 && first.card.no == current.card.no)
            {
                second.card.no = current.card.no;
            }
            else if (current.Carddec == "Joker" && current.card.no == 0 && first.card.no == second.card.no)
            {
                current.card.no = first.card.no;
            }

            if (first.card.no == current.card.no && second.card.no == current.card.no)
            {
                Debug.Log(current.name);
                current.InSequence();
                first.InSequence();
                second.InSequence();
                player.seq++;


                return true;
            }
            else
            {
                current.OutSequence();
                first.OutSequence();
                second.OutSequence();


                return false;
            }
            
        }
        else
        {
            current.OutSequence();
            first.OutSequence();
            second.OutSequence();


            return false;
        }
    }
    bool CheckDeckMatch(CardView first, CardView current, CardView second)
    {
        if (first.Carddec == current.Carddec && second.Carddec == current.Carddec
            || first.Carddec == "Joker" && second.Carddec == current.Carddec
            || second.Carddec == "Joker" && first.Carddec == current.Carddec
            || current.Carddec == "Joker" && first.card.no == second.card.no)
        {
            if(first.Carddec == "Joker" && second.card.no == current.card.no + 2)
            {
                Debug.Log("CheckDeckMatch : JOKER : 1");

                first.Carddec = second.Carddec;
                first.card.no = current.card.no + 1;

                Debug.Log(current.name);
                player.seq++;
                current.InSequence();
                first.InSequence();
                second.InSequence();
                return true;
            }
            else if(first.Carddec == "Joker" && second.card.no == current.card.no - 2)
            {
                Debug.Log("CheckDeckMatch : JOKER : 2");

                first.Carddec = second.Carddec;
                first.card.no = current.card.no - 1;

                Debug.Log(current.name);
                player.seq++;
                current.InSequence();
                first.InSequence();
                second.InSequence();
                return true;
            }
            else if(second.Carddec == "Joker" && first.card.no == current.card.no + 1)
            {
                Debug.Log("CheckDeckMatch : JOKER : 3");


                CardView Check_next = gridParent.GetChild(second.transform.GetSiblingIndex() + 1).GetComponent<CardView>();
                if (Check_next.card.no == current.card.no)
                {
                    second.card.no = current.card.no - 1;
                }
                else
                {
                    second.card.no = current.card.no + 2;
                }


                second.Carddec = first.Carddec;
                //second.card.no = current.card.no + 2;

                Debug.Log(current.name);
                player.seq++;
                current.InSequence();
                first.InSequence();
                second.InSequence();
                return true;
            }
            else if(second.Carddec == "Joker" && first.card.no == current.card.no - 1)
            {
                Debug.Log("CheckDeckMatch : JOKER : 4");

                second.Carddec = first.Carddec;
                second.card.no = current.card.no - 2;

                Debug.Log(current.name);
                player.seq++;
                current.InSequence();
                first.InSequence();
                second.InSequence();
                return true;
            }
            //else if(current.Carddec == "Joker" && first.card.no + 1 == second.card.no)
            //{
            //    Debug.Log("CheckDeckMatch : JOKER : 5");
            //    Debug.Log(current.name);
            //    player.seq++;
            //    current.InSequence();
            //    first.InSequence();
            //    second.InSequence();
            //    return true;
            //}
            //else if(current.Carddec == "Joker" && second.card.no - 1 == first.card.no)
            //{
            //    Debug.Log("CheckDeckMatch : JOKER : 6");
            //    Debug.Log(current.name);
            //    player.seq++;
            //    current.InSequence();
            //    first.InSequence();
            //    second.InSequence();
            //    return true;
            //}
            //else if (current.Carddec == "Joker" && first.card.no - 1 == second.card.no)
            //{
            //    Debug.Log("CheckDeckMatch : JOKER : 7");
            //    Debug.Log(current.name);
            //    player.seq++;
            //    current.InSequence();
            //    first.InSequence();
            //    second.InSequence();
            //    return true;
            //}
            //else if (current.Carddec == "Joker" && second.card.no + 1 == first.card.no)
            //{
            //    Debug.Log("CheckDeckMatch : JOKER : 8");
            //    Debug.Log(current.name);
            //    player.seq++;
            //    current.InSequence();
            //    first.InSequence();
            //    second.InSequence();
            //    return true;
            //}




            else if (first.card.no == current.card.no + 1 && second.card.no == current.card.no + 2)
            {
                Debug.Log("CheckDeckMatch : JOKER : Without 1");
                Debug.Log(current.name);
                player.seq++;
                current.InSequence();
                first.InSequence();
                second.InSequence();

                CardView joker = gridParent.GetChild(second.transform.GetSiblingIndex() + 1).GetComponent<CardView>();
                Debug.Log("CheckDeckMatch : JOKER : Joker in second gridINDEX: " + second.transform.GetSiblingIndex());
                Debug.Log("CheckDeckMatch : JOKER : Joker in gridINDEX: " + joker.transform.GetSiblingIndex());
                Debug.Log("CheckDeckMatch : JOKER : CURRENT.card: " + current);
                //if (joker.card.no == 0)
                if (joker.card.no == 0)
                {
                    joker.InSequence();
                    joker.Carddec = current.Carddec;

                    

                    joker.card.no = second.card.no + 1;
                }

                return true;
            }
            else if (first.card.no == current.card.no - 1 && second.card.no == current.card.no - 2)
            {
                Debug.Log("CheckDeckMatch : JOKER : Without 2");
                Debug.Log(current.name);
                current.InSequence();
                first.InSequence();
                second.InSequence();
                player.seq++;

                CardView joker = gridParent.GetChild(second.transform.GetSiblingIndex() + 1+2).GetComponent<CardView>();
                Debug.Log("CheckDeckMatch : JOKER : Joker in second gridINDEX: " + second.transform.GetSiblingIndex());
                Debug.Log("CheckDeckMatch : JOKER : Joker in gridINDEX: "+joker.transform.GetSiblingIndex());
                if (joker.card.no == 0)
                {
                    joker.InSequence();
                    joker.Carddec = current.Carddec;
                    joker.card.no = current.card.no +1;
                }

                return true;
            }

            else
            {
                current.OutSequence();
                first.OutSequence();
                second.OutSequence();
                return false;
            }
        }
        else
        {
            current.OutSequence();
            first.OutSequence();
            second.OutSequence();
            return false;
        }
    }
    private void OnDisable()
    {
        Gameplay_Manager.resetRound -= InitialStatus;
    }

    internal void DisplayPlayerData(Photon.Realtime.Player player)
    {
        t_Name.text = player.NickName;
    }

}
[Serializable]
public class InGamePlayer
{
    public int actorNo = 0;
    public int tablePos = 0;
    public int totalPoint = 0;
    public int seq = 0;
    public string ID = "";
    public List<GameCard> cards = new List<GameCard>();
    public int lossPoint = 0, winPoint = 0;
    public int score;
    public bool isBot = false;
    public bool VIP_isReady = false;

    public void CalculatetotalPoint()
    {
        totalPoint += winPoint + lossPoint;
    }
}






