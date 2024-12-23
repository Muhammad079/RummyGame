using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class CardsCollection
{
    public string name = "";
    public string name_Arabic = "";
    public Sprite collectionImage = null;
    public List<CardSet> cards = new List<CardSet>();
    public Sprite lockImage;
}
[System.Serializable]
public class CardSet
{
    public string name;
    public Card diamond;
    public Card club;
    public Card spade;
    public Card heart;
}
[System.Serializable]
public class Card
{
    public Sprite cardImage;
    public int cardCount;
    public int rewardStatus = -1;
}
[System.Serializable]
public class DBCardSet
{
    public string name = "";
    public DBCard spade;
    public DBCard club;
    public DBCard heart;
    public DBCard diamond;
}
[System.Serializable]
public class DBCard
{
    public int rewardStatus = -1;
    public int cardCount;
}
[System.Serializable]
public class DbCardCollection
{
    public string name = "";
    public List<DBCardSet> dbCards = new List<DBCardSet>();
}

