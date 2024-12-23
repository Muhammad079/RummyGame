using UnityEngine;
using UnityEngine.UI;

public class RoundSettlementPlayer : MonoBehaviour
{
    public Transform matchGrid = null, unmatchGrid = null;
    [SerializeField] private Text t_minValue = null, t_maxValue = null;
    void Start()
    {
        Debug.Log(matchGrid.localPosition);
        Debug.Log(unmatchGrid.localPosition);
    }
    public void ShowMinMax(int lossing, int winning)
    {

        t_minValue.text = lossing.ToString();
        t_minValue.GetComponent<Kozykin.MultiLanguageItem>().strItems[0] = lossing.ToString();
        t_minValue.GetComponent<Kozykin.MultiLanguageItem>().strItems[1] = lossing.ToString();
        t_minValue.GetComponent<Kozykin.MultiLanguageItem>().text = lossing.ToString();

        t_maxValue.text = winning.ToString();
        t_maxValue.GetComponent<Kozykin.MultiLanguageItem>().strItems[0] = winning.ToString();
        t_maxValue.GetComponent<Kozykin.MultiLanguageItem>().strItems[1] = winning.ToString();
        t_maxValue.GetComponent<Kozykin.MultiLanguageItem>().text = winning.ToString();
    }
    public void ShowMinMax(int total)
    {
        if (total > 0)
        {
            t_maxValue.transform.parent.gameObject.SetActive(true);
            t_minValue.transform.parent.gameObject.SetActive(false);
            t_maxValue.text = total.ToString();
            t_maxValue.GetComponent<Kozykin.MultiLanguageItem>().strItems[0] = total.ToString();
            t_maxValue.GetComponent<Kozykin.MultiLanguageItem>().strItems[1] = total.ToString();
            t_maxValue.GetComponent<Kozykin.MultiLanguageItem>().text = total.ToString();
        }
        else
        {
            t_maxValue.transform.parent.gameObject.SetActive(false);
            t_minValue.transform.parent.gameObject.SetActive(true);
            t_minValue.text = total.ToString();
            t_minValue.GetComponent<Kozykin.MultiLanguageItem>().strItems[0] = total.ToString();
            t_minValue.GetComponent<Kozykin.MultiLanguageItem>().strItems[1] = total.ToString();
            t_minValue.GetComponent<Kozykin.MultiLanguageItem>().text = total.ToString();
        }
    }
}
