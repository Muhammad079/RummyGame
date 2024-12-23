using DG.Tweening;
using UnityEngine;

public class CardsCollectionHandler : MonoBehaviour
{
    public static CardsCollectionHandler instance;
   
    public GameObject cardFeatureScreen = null, cardUnlockScreen = null;
    private void Awake()
    {
        instance = this;
    }

    void Start()
    {
        if (ProfileManager.instance.currentPlayer.cardsCollection.Count <= 0)
            PushData();
        else
            GetData();
        CheckCurrentCollection();
    }
    public void PushData()
    {
        Debug.Log("Pushing Data in database");
        for (int n = 0; n <  GameManager.instance.cardsCollection.cardsCollection.Count; n++)
        {
            DbCardCollection dCollection = new DbCardCollection();
            if (n >= ProfileManager.instance.currentPlayer.cardsCollection.Count)
                ProfileManager.instance.currentPlayer.cardsCollection.Add(dCollection);
            dCollection = ProfileManager.instance.currentPlayer.cardsCollection[n];
            for (int m = 0; m <  GameManager.instance.cardsCollection.cardsCollection[n].cards.Count; m++)
            {
                DBCardSet dCard = new DBCardSet();
                if (m >= dCollection.dbCards.Count)
                {
                    dCard.club = new DBCard();
                    dCard.diamond = new DBCard();
                    dCard.spade = new DBCard();
                    dCard.heart = new DBCard();
                    dCollection.dbCards.Add(dCard);
                }
                dCollection.name =  GameManager.instance.cardsCollection.cardsCollection[n].name;
                dCard = dCollection.dbCards[m];
                dCard.name =  GameManager.instance.cardsCollection.cardsCollection[n].cards[m].name;
                dCard.club.cardCount =  GameManager.instance.cardsCollection.cardsCollection[n].cards[m].club.cardCount;
                dCard.diamond.cardCount =  GameManager.instance.cardsCollection.cardsCollection[n].cards[m].diamond.cardCount;
                dCard.heart.cardCount =  GameManager.instance.cardsCollection.cardsCollection[n].cards[m].heart.cardCount;
                dCard.spade.cardCount =  GameManager.instance.cardsCollection.cardsCollection[n].cards[m].spade.cardCount;
                dCard.club.rewardStatus =  GameManager.instance.cardsCollection.cardsCollection[n].cards[m].club.rewardStatus;
                dCard.diamond.rewardStatus =  GameManager.instance.cardsCollection.cardsCollection[n].cards[m].diamond.rewardStatus;
                dCard.heart.rewardStatus =  GameManager.instance.cardsCollection.cardsCollection[n].cards[m].heart.rewardStatus;
                dCard.spade.rewardStatus =  GameManager.instance.cardsCollection.cardsCollection[n].cards[m].spade.rewardStatus;
            }
        }
        //foreach (var col in  GameManager.instance.cardsCollection.cardsCollection)
        //{
        //    var dCollection = new DbCardCollection();
        //    dCollection.name = col.name;
        //    foreach (var c in col.cards)
        //    {
        //        var dCard = new DBCardSet();
        //        dCard.club = new DBCard();
        //        dCard.diamond = new DBCard();
        //        dCard.spade = new DBCard();
        //        dCard.heart = new DBCard();
        //        dCard.name = c.name;
        //        dCard.club.cardCount = c.club.cardCount;
        //        dCard.diamond.cardCount = c.diamond.cardCount;
        //        dCard.heart.cardCount = c.heart.cardCount;
        //        dCard.spade.cardCount = c.spade.cardCount;
        //        dCard.club.rewardStatus = c.club.rewardStatus;
        //        dCard.diamond.rewardStatus = c.diamond.rewardStatus;
        //        dCard.heart.rewardStatus = c.heart.rewardStatus;
        //        dCard.spade.rewardStatus = c.spade.rewardStatus;
        //        dCollection.dbCards.Add(dCard);
        //    }
        //    ProfileManager.instance.currentPlayer.cardsCollection.Add(dCollection);
        //}
        ProfileManager.instance.SaveUserData();
    }
    void GetData()
    {
        Debug.Log("getting Data from database");
        for (int n = 0; n < ProfileManager.instance.currentPlayer.cardsCollection.Count; n++)
        {
            for (int m = 0; m < ProfileManager.instance.currentPlayer.cardsCollection[n].dbCards.Count; m++)
            {
                 GameManager.instance.cardsCollection.cardsCollection[n].cards[m].spade.cardCount = ProfileManager.instance.currentPlayer.cardsCollection[n].dbCards[m].spade.cardCount;
                 GameManager.instance.cardsCollection.cardsCollection[n].cards[m].spade.rewardStatus = ProfileManager.instance.currentPlayer.cardsCollection[n].dbCards[m].spade.rewardStatus;
                 GameManager.instance.cardsCollection.cardsCollection[n].cards[m].diamond.cardCount = ProfileManager.instance.currentPlayer.cardsCollection[n].dbCards[m].diamond.cardCount;
                 GameManager.instance.cardsCollection.cardsCollection[n].cards[m].diamond.rewardStatus = ProfileManager.instance.currentPlayer.cardsCollection[n].dbCards[m].diamond.rewardStatus;
                 GameManager.instance.cardsCollection.cardsCollection[n].cards[m].club.cardCount = ProfileManager.instance.currentPlayer.cardsCollection[n].dbCards[m].club.cardCount;
                 GameManager.instance.cardsCollection.cardsCollection[n].cards[m].club.rewardStatus = ProfileManager.instance.currentPlayer.cardsCollection[n].dbCards[m].club.rewardStatus;
                 GameManager.instance.cardsCollection.cardsCollection[n].cards[m].heart.cardCount = ProfileManager.instance.currentPlayer.cardsCollection[n].dbCards[m].heart.cardCount;
                 GameManager.instance.cardsCollection.cardsCollection[n].cards[m].heart.rewardStatus = ProfileManager.instance.currentPlayer.cardsCollection[n].dbCards[m].heart.rewardStatus;
            }
        }
    }
    public void ShowScreen()
    {
        transform.DOScale(new Vector3(1, 1, 1), 0.2f).SetEase(Ease.InOutCubic);
    }
    public void HideScreen()
    {
        transform.DOScale(new Vector3(0, 0, 0), 0.2f).SetEase(Ease.InOutCubic);
    }
    void CheckCurrentCollection()
    {
        for (int n = 0; n <  GameManager.instance.cardsCollection.cardsCollection.Count; n++)
        {
            if ( GameManager.instance.cardsCollection.cardsCollection[n].cards[ GameManager.instance.cardsCollection.cardsCollection[n].cards.Count - 1].club.cardCount >= 10 &&
                 GameManager.instance.cardsCollection.cardsCollection[n].cards[ GameManager.instance.cardsCollection.cardsCollection[n].cards.Count - 1].spade.cardCount >= 10 &&
                 GameManager.instance.cardsCollection.cardsCollection[n].cards[ GameManager.instance.cardsCollection.cardsCollection[n].cards.Count - 1].heart.cardCount >= 10 &&
                 GameManager.instance.cardsCollection.cardsCollection[n].cards[ GameManager.instance.cardsCollection.cardsCollection[n].cards.Count - 1].diamond.cardCount >= 10)
            {
                Debug.Log("this is filled");
            }
            else
            {
                for(int m=0;m <  GameManager.instance.cardsCollection.cardsCollection[n].cards.Count; m++)
                {
                    if ( GameManager.instance.cardsCollection.cardsCollection[n].cards[m].club.cardCount >= 10 &&
                 GameManager.instance.cardsCollection.cardsCollection[n].cards[m].spade.cardCount >= 10 &&
                 GameManager.instance.cardsCollection.cardsCollection[n].cards[m].heart.cardCount >= 10 &&
                 GameManager.instance.cardsCollection.cardsCollection[n].cards[m].diamond.cardCount >= 10)
                    {
                        Debug.Log("This is filled too");
                    }
                    else
                    {
                        if ( GameManager.instance.cardsCollection.cardsCollection[n].cards[m].spade.cardCount < 10)
                      GameManager.instance.      selectedCollectionCard =  GameManager.instance.cardsCollection.cardsCollection[n].cards[m].spade;
                        else if ( GameManager.instance.cardsCollection.cardsCollection[n].cards[m].club.cardCount < 10)
                            GameManager.instance.selectedCollectionCard =  GameManager.instance.cardsCollection.cardsCollection[n].cards[m].club;
                        else if ( GameManager.instance.cardsCollection.cardsCollection[n].cards[m].diamond.cardCount < 10)
                            GameManager.instance.selectedCollectionCard =  GameManager.instance.cardsCollection.cardsCollection[n].cards[m].diamond;
                        else if ( GameManager.instance.cardsCollection.cardsCollection[n].cards[m].heart.cardCount < 10)
                            GameManager.instance.selectedCollectionCard =   GameManager.instance.cardsCollection.cardsCollection[n].cards[m].heart;
                        break;
                    }
                }
                break;
            }
        }
    }
}
