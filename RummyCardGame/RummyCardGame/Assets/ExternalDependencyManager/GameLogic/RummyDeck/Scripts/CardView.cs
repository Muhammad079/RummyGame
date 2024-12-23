using Coffee.UIEffects;
using DG.Tweening;
using System;
using UnityEngine;
using UnityEngine.UI;
[System.Serializable]
public class CardView : MonoBehaviour
{
    public GameCard card;
    private Image img;
    private int childIndex;
    CardView firstPrev;
    CardView secondPrev;
    CardView firstNext;
    CardView secondNext;
    private Vector3 targetPos;
    bool move = false;
    public string CardName;
    public string Carddec;

    public int ChildIndex { get => childIndex; set => childIndex = value; }
    private void Awake()
    {
        img = GetComponent<Image>();
    }
    private void Start()
    {
        InitialStatus();
    }
    void InitialStatus()
    {
        OutSequence();
        GetComponent<UIShiny>().effectPlayer.initialPlayDelay = transform.GetSiblingIndex();
        GetComponent<UIShiny>().effectPlayer.loopDelay = 4;
        CardName = this.gameObject.name;
        string[] Splitarray = CardName.Split(char.Parse(" "));
        Carddec = Splitarray[0];
        if (Splitarray.Length > 1)
            card.no = int.Parse(Splitarray[1]);
    }
    public void SetCardImg(Sprite sprite, int index)
    {
        card.imgIndex = index;
        img.sprite = sprite;
        InitialStatus();
    }
    public void Dropped()
    {
        img.sprite = CardManager.instance.titledCards[card.imgIndex];
        transform.DOScale(new Vector3(0.6f, 0.6f, 1), 0.3f);
        GetComponent<CardView>().enabled = false;

    }
    public void InHand()
    {
        img.sprite = CardManager.instance.cardImages[card.imgIndex];
        transform.DOScale(Vector3.one, 0.25f);
    }
    public void MoveCard()
    {
        Invoke(nameof(CompareSurroundingCards), 0.25f);
    }
    void CompareSurroundingCards()
    {

    }

    public void InSequence()
    {
        Invoke(nameof(Aposd), 0.25f);
    }
    void Aposd()
    {
        card.matched = true;
        transform.GetChild(0).gameObject.SetActive(true);
        GamePlayPlayer.instance.ScoreChecker();
        CancelInvoke(nameof(Aposd));
    }
    public void OutSequence()
    {
        card.matched = false;
        transform.GetChild(0).gameObject.SetActive(false);
        GamePlayPlayer.instance.ScoreChecker();
    }
    public void MoveToTarget(Vector3 target)
    {
        targetPos = new Vector2(target.x + (transform.GetSiblingIndex()) * 70, target.y);
        move = true;
    }
    private void Update()
    {
        if (move)
        {
            if (Vector3.Distance(transform.position, targetPos) > 0.1f)
            {
                transform.localPosition = Vector2.MoveTowards(transform.localPosition, targetPos, Vector2.Distance(transform.localPosition, targetPos) / 10);
                if (transform.localScale.x > 0.5f)
                    transform.localScale = new Vector3(transform.localScale.x - 0.01f, transform.localScale.y - 0.01f, transform.localScale.z - 0.01f);
            }
            else
            {
                move = false;
            }
        }
    }
}
[Serializable]
public class GameCard
{
    public int no;
    public bool matched = false;
    public int imgIndex = 0;
    public string cardcategory;
}