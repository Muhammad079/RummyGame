using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class NotEnoughCoinsPanel : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        GameManager.instance.Not_Enough_C_Panel = this.gameObject;
        transform.Find("Close_btn").gameObject.GetComponent<Button>().onClick.AddListener(() =>
        {
            transform.GetChild(0).GetComponent<Text>().text = "Not Enough Coins";

            if(transform.GetChild(0).GetComponent<Kozykin.MultiLanguageItem>())
            {
                transform.GetChild(0).GetComponent<Kozykin.MultiLanguageItem>().strItems[0] = "Not Enough Coins";
                transform.GetChild(0).GetComponent<Kozykin.MultiLanguageItem>().strItems[1] = "عملات غير كافية";
                transform.GetChild(0).GetComponent<Kozykin.MultiLanguageItem>().text = "عملات غير كافية";
            }

            transform.DOScale(new Vector3(0, 0, 0), 0.5f).SetEase(Ease.OutBounce).OnComplete(() =>
            {
                this.gameObject.SetActive(false);
            });
        });
    }

}
