using DG.Tweening;
using ExitGames.Client.Photon;
using Photon.Pun;
using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CardManager : MonoBehaviour
{
    public event System.Action performComparison = null;

    //Instance so we can access it from all scripts
    public static CardManager instance;
    public List<Sprite> cardImages = new List<Sprite>();   //ref to card sprites
    public List<Sprite> titledCards = new List<Sprite>();
    public int Length = 9;
    //important gameobjects
    [SerializeField] private GameObject cardHolder, cardPrefab, parentHolder, dummyCardPrefab;
    public Image carddroppoint;
    public CardView selectedCard, previousCard, nextCard;  //ref to cards
    int k, childCount;
    private GameObject dummyCardObj;
    private bool cardbool;
    public CardView SelectedCard { get => selectedCard; }
    public GameObject ParentHolder { get => parentHolder; }
    public static int count = 0;
    //public List<Text> cardnames;
    public int score;
    public int instCardNumber = 0;
    public GameObject cardObj;
    public int scorecount = 0;
    GameObject card;
    [SerializeField] private Transform residualCards = null;
    //    private List<Sprite> gameCards = new List<Sprite>();

    public GameObject Card_Chance;
    int gems_Deduction;
    int card_check = 0;
    string cardRemover;
    public ParticleSystem Gem_Card_Chance_Effect;
    public GameObject Deck;
    //int Deck_Counter;


    private void Awake()
    {
        gems_Deduction = 5;
        //Deck_Counter = 1;

        if (instance == null)
        {
            instance = this;
        }
    }
    private void OnDisable()
    {
        Gameplay_Manager.resetRound -= InitialStatus;
    }
    private void Start()
    {
        InitialStatus();
        Gameplay_Manager.resetRound += InitialStatus;
    }
    public void InitialStatus()
    {

        if (PhotonNetwork.IsMasterClient)
        {
            Gameplay_Manager.instance.gameCards.Clear();
            if (PhotonNetwork.IsMasterClient)
            {
                for (int n = 0; n < carddroppoint.transform.childCount; n++)
                {
                    Destroy(carddroppoint.transform.GetChild(n).gameObject);
                }
                int loop = 0;
                if (GameManager.instance.selectedTable.totalPlayers > 2)
                    loop = 2;
                else
                    loop = 2;//loop = 1;
                for (int i = 0; i < loop; i++)
                {
                    for (int n = 0; n < cardImages.Count; n++)
                    {
                        Gameplay_Manager.instance.gameCards.Add(n);
                    }
                }

                for (int n = 0; n < Gameplay_Manager.instance.gameCards.Count; n++)
                {
                    int c = Random.Range(0, Gameplay_Manager.instance.gameCards.Count - 1);
                    var a = Gameplay_Manager.instance.gameCards[c];
                    Gameplay_Manager.instance.gameCards[c] = Gameplay_Manager.instance.gameCards[n];
                    Gameplay_Manager.instance.gameCards[n] = a;
                }
            }


            cardbool = false;


        }
        FillDeck();
    }

    void FillDeck()
    {

        Debug.Log("Deck Filled");

        for (int i = 0; i < Length; i++)
        {
            count++;
            k = i;
            Debug.Log("Spawninig Cards");
            SpawnCard(cardImages[Gameplay_Manager.instance.gameCards[Gameplay_Manager.instance.gameCards.Count - 1]], i, Gameplay_Manager.instance.gameCards.Count - 1);
        }
        Invoke(nameof(CardComparison), 1);
        if (PhotonNetwork.IsMasterClient)
        {
            DeckCards dc = new DeckCards();
            dc.cards = Gameplay_Manager.instance.gameCards;
            dc.playerIndex = PhotonNetwork.LocalPlayer.ActorNumber + 1;
            string b = JsonUtility.ToJson(dc);
            object[] cont = new object[] { b }; // Array contains the target position and the IDs of the selected units
            RaiseEventOptions raiseEvent = new RaiseEventOptions { Receivers = ReceiverGroup.All }; // You would have to set the Receivers to All in order to receive this event on the local client as well
            PhotonNetwork.RaiseEvent(MultiPlayerEventsCallBack.DeckFiller, cont, raiseEvent, SendOptions.SendReliable);
        }
    }

    /// <summary>
    /// Method called when we tap on any card
    /// </summary>
    /// <param name="card">Reference of tapped card</param>
    public void SelectCard(CardView card)
    {
        //save the selected card SiblingIndex [Child Index for its parent]
        if (!Gameplay_Manager.settlingRound)
        {

            GamePlayPlayer.instance.Sq_Color_Logic_RESET();


            int selectedIndex = card.transform.GetSiblingIndex();
            selectedCard = card;                        //set selectedCard to card
            if (!Gameplay_Manager.instance.isTurn && selectedCard.tag == "catchcard" && cardHolder.transform.childCount < 10)
            {
                selectedCard = null;
                return;
            }
            else if (selectedCard.tag == "catchcard" && cardHolder.transform.childCount >= 10)
            {
                selectedCard = null;
                return;
            }
            else
            {
                #region Burhan_SelectedCard_Scaler_Alpha_Setter_And_other
                Card_Chance.SetActive(false);//other
                                             // StopCoroutine(nameof(Chance_wait));//other
                Card_Chance.transform.GetChild(0).gameObject.GetComponent<Text>().text = "3";//other

                selectedCard.transform.DOScale(new Vector3(2f, 2f, 2f), .3f).SetEase(Ease.InSine);//.OnComplete(Card_Scale_Out);
                                                                                                  //selectedCard.transform.d
                Color alpha = selectedCard.gameObject.GetComponent<Image>().color;
                alpha.a = 0.75f;
                selectedCard.gameObject.GetComponent<Image>().color = alpha;
                #endregion


                //if(selectedCard.CardName.Contains("Joker"))
                //{
                //    selectedCard.card.no = 0;
                //    selectedCard.Carddec = "Joker";
                //}
                for (int i = 0; i < cardHolder.transform.childCount; i++)
                {
                    if(cardHolder.transform.GetChild(i).GetComponent<CardView>().CardName.Contains("Joker"))
                    {
                        cardHolder.transform.GetChild(i).GetComponent<CardView>().card.no = 0;
                        cardHolder.transform.GetChild(i).GetComponent<CardView>().Carddec = "Joker";
                    }
                }


                selectedCard.ChildIndex = selectedIndex;    //set the selectedCard ChildIndex
                GetDummyCard().SetActive(true);             //activate dummy card
                GetDummyCard().transform.SetSiblingIndex(selectedIndex);    //set dummy card index
                                                                            //change the parent of selectedCard
                selectedCard.transform.SetParent(CardManager.instance.ParentHolder.transform);

                childCount = cardHolder.transform.childCount;

                //check if selectedIndex is less than total childCount
                if (selectedIndex + 1 < childCount)
                {
                    //set the next card of the selected card
                    nextCard = cardHolder.transform.GetChild(selectedIndex + 1).GetComponent<CardView>();
                }

                if (selectedIndex - 1 >= 0)
                {
                    //set the previous card of selected card
                    previousCard = cardHolder.transform.GetChild(selectedIndex - 1).GetComponent<CardView>();
                }
            }
        }

    }

    /// <summary>
    /// Method called on release of tap
    /// </summary>

    public void OnCardRelease()
    {


        if (!Gameplay_Manager.settlingRound)
        {
            GamePlayPlayer.instance.Sq_Color_Logic_RESET();
            //if SelectedCard is not null
            if (SelectedCard != null)
            {
                #region Burhan_SelectedCard_Scaler_Alpha_Reverter
                Color alpha = selectedCard.gameObject.GetComponent<Image>().color;
                alpha.a = 1f;
                selectedCard.gameObject.GetComponent<Image>().color = alpha;
                #endregion
                GetDummyCard().SetActive(false);        //Deactivate Dummy card
                                                        //set selectedCard parent

                //set selectedCard SetSiblingIndex

                if (Mathf.Abs(selectedCard.transform.position.y - GetDummyCard().transform.position.y) > 2)
                {
                    if (cardHolder.transform.childCount == 10)
                    {
                        if (Gameplay_Manager.instance.isTurn)
                        {

                            cardbool = true;
                            selectedCard.transform.SetParent(carddroppoint.transform); selectedCard.transform.DOScale(new Vector3(0.6f, 0.6f, 1f), 0.3f).SetEase(Ease.Linear);

                            // selectedCard.transform.position = new Vector3(Random.Range(carddroppoint.transform.position.x + 1, carddroppoint.transform.position.x - 1), Random.Range(carddroppoint.transform.position.y + 1, carddroppoint.transform.position.y - 1), 1); ;
                            selectedCard.transform.position = carddroppoint.transform.position;
                            int a = selectedCard.transform.GetSiblingIndex();
                            a = Mathf.Clamp(a, 0, 5);

                            selectedCard.transform.position = new Vector3(selectedCard.transform.position.x, selectedCard.transform.position.y + a * 0.025f);
                            //selectedCard.transform.localScale = new Vector3(1.2f,1.5f,1);
                            selectedCard.transform.localScale = new Vector3(1, 1, 1);
                            selectedCard.transform.rotation = Quaternion.Euler(0, 0, 0);
                            GetDummyCard().transform.SetParent(CardManager.instance.ParentHolder.transform);
                            selectedCard.GetComponent<CardView>().Dropped();
                            selectedCard.transform.GetChild(0).gameObject.SetActive(false);
                            Gameplay_Manager.instance.isTurn = false;
                            object[] content = new object[] { selectedCard.card.imgIndex }; // Array contains the target position and the IDs of the selected units
                            RaiseEventOptions raiseEventOptions = new RaiseEventOptions { Receivers = ReceiverGroup.All }; // You would have to set the Receivers to All in order to receive this event on the local client as well
                            PhotonNetwork.RaiseEvent(MultiPlayerEventsCallBack.throwCard, content, raiseEventOptions, SendOptions.SendReliable);
                            object[] cont = new object[] { Gameplay_Manager.playerTurn }; // Array contains the target position and the IDs of the selected units
                            RaiseEventOptions raiseEvent = new RaiseEventOptions { Receivers = ReceiverGroup.All }; // You would have to set the Receivers to All in order to receive this event on the local client as well
                            PhotonNetwork.RaiseEvent(MultiPlayerEventsCallBack.turnSwitching, cont, raiseEvent, SendOptions.SendReliable);
                            selectedCard.tag = "catchcard";

                            // count--;
                        }
                        else
                        {
                            selectedCard.transform.DOScale(new Vector3(1f, 1f, 1f), 0.3f).SetEase(Ease.Linear);
                            selectedCard.tag = "playingCard";
                            selectedCard.transform.SetParent(cardHolder.transform);
                            selectedCard.transform.SetSiblingIndex(GetDummyCard().transform.GetSiblingIndex());
                            GetDummyCard().transform.SetParent(CardManager.instance.ParentHolder.transform);
                        }
                    }
                    else
                    {
                        if (cardHolder.transform.childCount < 10)
                        {
                            selectedCard.transform.DOScale(new Vector3(1f, 1f, 1f), 0.3f).SetEase(Ease.Linear);
                            if (selectedCard.tag == "catchcard")
                            {
                                Debug.Log("Picking From Deck");

                                object[] cont = new object[] { selectedCard.card.imgIndex }; // Array contains the target position and the IDs of the selected units
                                RaiseEventOptions raiseEvent = new RaiseEventOptions { Receivers = ReceiverGroup.Others }; // You would have to set the Receivers to All in order to receive this event on the local client as well
                                PhotonNetwork.RaiseEvent(MultiPlayerEventsCallBack.pickingFromDeck, cont, raiseEvent, SendOptions.SendReliable);
                            }

                            selectedCard.tag = "playingCard";
                            cardbool = false;
                            selectedCard.transform.SetParent(cardHolder.transform);
                            GetDummyCard().transform.SetParent(ParentHolder.transform);
                            selectedCard.transform.SetSiblingIndex(selectedCard.ChildIndex);
                            GetDummyCard().transform.SetParent(ParentHolder.transform);

                        }
                    }
                }

                else
                {
                    if (cardHolder.transform.childCount <= 10)
                    {
                        selectedCard.transform.DOScale(new Vector3(1f, 1f, 1f), 0.3f).SetEase(Ease.Linear);
                        if (selectedCard.tag == "catchcard")
                        {


                            selectedCard.GetComponent<CardView>().InHand();
                            Debug.Log("Picking From Deck");
                            object[] cont = new object[] { selectedCard.card.imgIndex }; // Array contains the target position and the IDs of the selected units
                            RaiseEventOptions raiseEvent = new RaiseEventOptions { Receivers = ReceiverGroup.Others }; // You would have to set the Receivers to All in order to receive this event on the local client as well
                            PhotonNetwork.RaiseEvent(MultiPlayerEventsCallBack.pickingFromDeck, cont, raiseEvent, SendOptions.SendReliable);
                        }

                        selectedCard.tag = "playingCard";
                        selectedCard.transform.SetParent(cardHolder.transform);
                        selectedCard.transform.SetSiblingIndex(GetDummyCard().transform.GetSiblingIndex());
                        GetDummyCard().transform.SetParent(CardManager.instance.ParentHolder.transform);

                    }
                }

                selectedCard = null;    //make selectedCard null
                Invoke(nameof(CardComparison), 0.5f);
            }

        }
    }
    private void Update()
    {



        if (!Gameplay_Manager.settlingRound)
        {
            if (selectedCard != null)
            {
                //if (selectedCard.tag == "catchcard")
                //{
                //    GetDummyCard().SetActive(false);
                //    selectedCard.transform.position = carddroppoint.transform.position;
                //}
                if (cardHolder.transform.childCount > 10)
                {

                    GetDummyCard().SetActive(false);
                    selectedCard.transform.position = carddroppoint.transform.position;
                    selectedCard.transform.SetParent(carddroppoint.transform);

                }
            }
        }



        if (cardHolder.transform.childCount > 0)
        {
            if (cardHolder.transform.childCount < 10)
            {
                cardHolder.transform.GetChild(0).transform.rotation = Quaternion.Euler(0, 0, 30);
                cardHolder.transform.GetChild(1).transform.rotation = Quaternion.Euler(0, 0, 20);
                cardHolder.transform.GetChild(2).transform.rotation = Quaternion.Euler(0, 0, 10);
                cardHolder.transform.GetChild(3).transform.rotation = Quaternion.Euler(0, 0, 5);
                cardHolder.transform.GetChild(4).transform.rotation = Quaternion.Euler(0, 0, 0);
                cardHolder.transform.GetChild(5).transform.rotation = Quaternion.Euler(0, 0, -5);
                cardHolder.transform.GetChild(6).transform.rotation = Quaternion.Euler(0, 0, -10);
                cardHolder.transform.GetChild(7).transform.rotation = Quaternion.Euler(0, 0, -20);
                cardHolder.transform.GetChild(8).transform.rotation = Quaternion.Euler(0, 0, -30);
            }
            else
            {
                cardHolder.transform.GetChild(0).transform.rotation = Quaternion.Euler(0, 0, 30);
                cardHolder.transform.GetChild(1).transform.rotation = Quaternion.Euler(0, 0, 20);
                cardHolder.transform.GetChild(2).transform.rotation = Quaternion.Euler(0, 0, 10);
                cardHolder.transform.GetChild(3).transform.rotation = Quaternion.Euler(0, 0, 5);
                cardHolder.transform.GetChild(4).transform.rotation = Quaternion.Euler(0, 0, 0);
                cardHolder.transform.GetChild(5).transform.rotation = Quaternion.Euler(0, 0, 0);
                cardHolder.transform.GetChild(6).transform.rotation = Quaternion.Euler(0, 0, -5);
                cardHolder.transform.GetChild(7).transform.rotation = Quaternion.Euler(0, 0, -10);
                cardHolder.transform.GetChild(8).transform.rotation = Quaternion.Euler(0, 0, -20);
                cardHolder.transform.GetChild(9).transform.rotation = Quaternion.Euler(0, 0, -30);
            }
        }
    }
    /// <summary>
    /// Method called on drag of card
    /// </summary>
    /// <param name="position"></param>
    public void MoveSelectedCard(Vector2 position)
    {
        if (!Gameplay_Manager.settlingRound)
        {
            float Old_Pos = float.Parse(selectedCard.transform.position.x.ToString());

            position = Camera.main.ScreenToWorldPoint(position);

            if (selectedCard != null)                           //if SelectedCard is not null
            {
                selectedCard.transform.position = position;     //set selectedCard position


                if (selectedCard.transform.position.x > Old_Pos)
                {
                    CheckWithNextCard();
                }
                else
                {
                    CheckWithPreviousCard();
                }

                CheckWithNextCard();                            //check for next card
                CheckWithPreviousCard();                        //check for previous card
                                                                //selectedCard.MoveCard()
            }
        }
    }

    void CheckWithNextCard()
    {
        if (nextCard != null)
        {
            if (selectedCard.transform.position.x > nextCard.transform.position.x)
            {
                int index = nextCard.transform.GetSiblingIndex();
                //nextCard.transform.SetSiblingIndex(dummyCardObj.transform.GetSiblingIndex());
                dummyCardObj.transform.SetSiblingIndex(index);
                previousCard = nextCard;
                if (index + 1 < childCount)
                {
                    nextCard = cardHolder.transform.GetChild(index + 1).GetComponent<CardView>();
                }
                else
                {
                    nextCard = null;
                }
            }
        }
    }
    void CheckWithPreviousCard()
    {
        if (previousCard != null)
        {
            if (selectedCard.transform.position.x < previousCard.transform.position.x)
            {
                int index = previousCard.transform.GetSiblingIndex();
                // previousCard.transform.SetSiblingIndex(dummyCardObj.transform.GetSiblingIndex());
                dummyCardObj.transform.SetSiblingIndex(index);
                nextCard = previousCard;
                if (index - 1 >= 0)
                {
                    previousCard = cardHolder.transform.GetChild(index - 1).GetComponent<CardView>();
                }
                else
                {
                    previousCard = null;
                }
            }
        }
    }

    [SerializeField] private Text gemDeductionText = null;
    public void CardComparison()
    {
        performComparison?.Invoke();
    }
    int anim_Counter = 0;
    public void CardButton()
    {
        if (Gameplay_Manager.instance.isTurn)
        {
            if (!Gameplay_Manager.settlingRound)
            {
                if (cardHolder.transform.childCount == 9)
                {


                    StartCoroutine(nameof(Chance_wait));
                    Card_Chance.SetActive(true);
                    gemDeductionText.text = gems_Deduction.ToString();
                    //if (ProfileManager.instance.currentPlayer.gems>=gems_Deduction)
                    //{
                    //    Card_Chance.SetActive(true);
                    //    gemDeductionText.text = gems_Deduction.ToString();
                    //}


                    anim_Counter++;
                    if(anim_Counter==1)
                    {
                        Deck_Card_Animation();
                    }
                    

                    
                }
            }
        }
    }
    public void Deck_Card_Animation()
    {
        card = Instantiate(cardPrefab, Deck.transform);
        card.GetComponent<RectTransform>().sizeDelta = new Vector2(240, 290);
        card.transform.DOMove(cardHolder.transform.GetChild(cardHolder.transform.childCount - 1).transform.position, 0.3f).OnComplete(()=> {
            Destroy(card.gameObject);
            //SpawnCard();
            SameCardCheck();//Burhan_Same_Card_Check_Function_reference
            Invoke(nameof(GettingInstCardNum), 0.2f);
            // k++;
            count++;
            anim_Counter = 0;
        });
    }


    #region Burhan_Same_Card_Check_(Bilal_Modified)
    int cardAddIndex = 0;
    public void SameCardCheck()
    {
        if (!Gameplay_Manager.settlingRound)
        {

            card = Instantiate(cardPrefab, cardHolder.transform);
            //card.GetComponent<RectTransform>().sizeDelta = new Vector2(145, 200);
            card.GetComponent<RectTransform>().sizeDelta = new Vector2(300, 350);
            card.name = cardImages[Gameplay_Manager.instance.gameCards[Gameplay_Manager.instance.gameCards.Count - 1]].name;
            card.tag = "playingCard";

            card.GetComponent<CardView>().SetCardImg(cardImages[Gameplay_Manager.instance.gameCards[Gameplay_Manager.instance.gameCards.Count - 1]], Gameplay_Manager.instance.gameCards[Gameplay_Manager.instance.gameCards.Count - 1]);
            cardAddIndex = 0;


            object[] content = new object[] { cardAddIndex }; // Array contains the target position and the IDs of the selected units
            RaiseEventOptions raiseEventOptions = new RaiseEventOptions { Receivers = ReceiverGroup.All }; // You would have to set the Receivers to All in order to receive this event on the local client as well
            PhotonNetwork.RaiseEvent(MultiPlayerEventsCallBack.pickCard, content, raiseEventOptions, SendOptions.SendReliable);


        }
    }
    #endregion
    IEnumerator Chance_wait()
    {
        yield return new WaitForSeconds(1f);
        Card_Chance.transform.GetChild(0).gameObject.GetComponent<Text>().text = "2";
        yield return new WaitForSeconds(1f);
        Card_Chance.transform.GetChild(0).gameObject.GetComponent<Text>().text = "1";
        yield return new WaitForSeconds(1f);
        Card_Chance.SetActive(false);
        Card_Chance.transform.GetChild(0).gameObject.GetComponent<Text>().text = "3";
        //object[] content = new object[] { cardAddIndex }; // Array contains the target position and the IDs of the selected units
        //RaiseEventOptions raiseEventOptions = new RaiseEventOptions { Receivers = ReceiverGroup.All }; // You would have to set the Receivers to All in order to receive this event on the local client as well
        //PhotonNetwork.RaiseEvent(MultiPlayerEventsCallBack.pickCard, content, raiseEventOptions, SendOptions.SendReliable);

        StopCoroutine(nameof(Chance_wait));
    }
    public void CardButton_Chance()
    {
        if (!Gameplay_Manager.settlingRound)
        {
            if (ProfileManager.instance.currentPlayer.gems >= gems_Deduction)
            {
                if (Gameplay_Manager.instance.isTurn)
                {
                    ProfileManager.instance.currentPlayer.gems -= gems_Deduction;
                    gems_Deduction += 5;
                    card.name = cardImages[Gameplay_Manager.instance.gameCards[Gameplay_Manager.instance.gameCards.Count - 2]].name;
                    card.tag = "playingCard";
                    card.GetComponent<CardView>().SetCardImg(cardImages[Gameplay_Manager.instance.gameCards[Gameplay_Manager.instance.gameCards.Count - 2]], Gameplay_Manager.instance.gameCards[Gameplay_Manager.instance.gameCards.Count - 2]);
                    cardAddIndex = 1;
                    Gem_Card_Chance_Effect.Play();


                    //object[] content = new object[] { cardAddIndex }; // Array contains the target position and the IDs of the selected units
                    //RaiseEventOptions raiseEventOptions = new RaiseEventOptions { Receivers = ReceiverGroup.All }; // You would have to set the Receivers to All in order to receive this event on the local client as well
                    //PhotonNetwork.RaiseEvent(MultiPlayerEventsCallBack.pickCard, content, raiseEventOptions, SendOptions.SendReliable);


                }
            }
            else
            {
                GameManager.instance.Not_Enough_C_Panel.SetActive(true);
                GameManager.instance.Not_Enough_C_Panel.transform.GetChild(0).GetComponent<Text>().text = "Not Enough Gems";
                GameManager.instance.Not_Enough_C_Panel.transform.DOScale(new Vector3(3, 3, 3), 0.5f).SetEase(Ease.OutBounce);
                Debug.Log("Not Enough Gems");
            }
        }
    }
    public void GettingInstCardNum()
    {
        if (!Gameplay_Manager.settlingRound)
        {
            instCardNumber = cardObj.GetComponent<CardView>().card.no;
            GamePlayPlayer.instance.ScoreChecker();
            //int n = 0;

        }
    }

    #region Spawn Logic
    float yFactor = 0;
    void SpawnCard(Sprite newCard, int count, int cardIndex)
    {
        if (count < cardHolder.transform.childCount)
            card = cardHolder.transform.GetChild(count).gameObject;
        else
        {
            card = Instantiate(cardPrefab, cardHolder.transform);
            //card.GetComponent<RectTransform>().sizeDelta = new Vector2(145, 200);
            card.GetComponent<RectTransform>().sizeDelta = new Vector2(300, 350);
            //card.GetComponent<RectTransform>().SetSizeWithCurrentAnchors(1, 200);
        }



        cardObj = card;

        //if (cardObj.transform.GetSiblingIndex()<5)
        //{
        //    cardObj.transform.localPosition = new Vector3(cardObj.transform.localPosition.x+30*cardObj.transform.GetSiblingIndex(), yFactor +5, cardObj.transform.localPosition.z);
        //}
        //else
        //{
        //    cardObj.transform.localPosition = new Vector3(cardObj.transform.localPosition.x + 30 * cardObj.transform.GetSiblingIndex(), yFactor - 5  , cardObj.transform.localPosition.z);
        //}
        //yFactor = cardObj.transform.localPosition.y;

        card.name = newCard.name;
        card.tag = "playingCard";
        card.GetComponent<CardView>().SetCardImg(newCard, Gameplay_Manager.instance.gameCards[cardIndex]);
        Gameplay_Manager.instance.gameCards.RemoveAt(cardIndex);

    }
    public void RefillDeck()
    {
        Debug.LogError("Refilling");
        Debug.LogError(carddroppoint.transform.childCount);
        for (int n = carddroppoint.transform.childCount - 1; n >= 0; n--)
        {

            for (int m = 0; m < cardImages.Count; m++)
            {
                if (cardImages[m].name == carddroppoint.transform.GetChild(n).GetComponent<Image>().sprite.name)
                {
                    Gameplay_Manager.instance.gameCards.Add(m);
                    carddroppoint.transform.GetChild(n).SetParent(residualCards);
                    break;
                }
            }
        }
    }
    GameObject GetDummyCard()
    {
        if (dummyCardObj == null)
        {
            dummyCardObj = Instantiate(dummyCardPrefab, cardHolder.transform);
            //dummyCardObj.GetComponent<RectTransform>().sizeDelta = new Vector2(145, 200);
            dummyCardObj.GetComponent<RectTransform>().sizeDelta = new Vector2(300, 350);
            dummyCardObj.name = "DummyCard";
        }
        else
        {
            if (dummyCardObj.transform.parent != cardHolder.transform)
            {
                dummyCardObj.transform.SetParent(cardHolder.transform);
            }
            return dummyCardObj;
        }
        return dummyCardObj;
    }
    #endregion
}
