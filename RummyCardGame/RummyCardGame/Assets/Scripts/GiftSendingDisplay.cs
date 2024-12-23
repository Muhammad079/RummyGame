using UnityEngine;
using UnityEngine.UI;

public class GiftSendingDisplay : MonoBehaviour
{
    [SerializeField] private Text senderInfo = null, recieverInfo = null,xpIncrement=null;
    [SerializeField] private Image giftImage = null;
    void Start()
    {

    }
    public void PassData(GiftSendingMessege msg)
    {
        senderInfo.text = msg.senderId;
        recieverInfo.text = msg.recieverId;
        for (int n = 0; n < GameManager.instance.giftsDataFile.gifts.Count; n++)
        {
            if (msg.giftItems == GameManager.instance.giftsDataFile.gifts[n].gift)
            {
                giftImage.sprite = GameManager.instance.giftsDataFile.gifts[n].giftImage;
                xpIncrement.text ="+"+ GameManager.instance.giftsDataFile.gifts[n].charmXp.ToString();
                break;
            }
        }
    }
    void Update()
    {

    }
}
