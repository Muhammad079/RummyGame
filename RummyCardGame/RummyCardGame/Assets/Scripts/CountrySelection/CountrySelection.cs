using UnityEngine;
using UnityEngine.UI;

public class CountrySelection : MonoBehaviour
{
    [SerializeField] private Text winTrophies = null, lossTrophies = null, playersText = null, prizeText = null, bidText = null,currentTrophies=null;
    [SerializeField] private Image fillingBar = null;
    public int ID;
    void OnEnable()
    {
        //var data = GameManager.instance.selectedXPData.data[transform.GetSiblingIndex()];
        var data = GameManager.instance.selectedXPData.data[ID];
        winTrophies.text ="+"+ data.trophiesOnWin.ToString();
        winTrophies.GetComponent<Kozykin.MultiLanguageItem>().strItems[0] = "+" + data.trophiesOnWin.ToString();
        winTrophies.GetComponent<Kozykin.MultiLanguageItem>().strItems[1] = "+" + data.trophiesOnWin.ToString();
        winTrophies.GetComponent<Kozykin.MultiLanguageItem>().text = "+" + data.trophiesOnWin.ToString();
        lossTrophies.text = "-" + data.trophiesOnLoss.ToString();
        lossTrophies.GetComponent<Kozykin.MultiLanguageItem>().strItems[0] = "-" + data.trophiesOnLoss.ToString();
        lossTrophies.GetComponent<Kozykin.MultiLanguageItem>().strItems[1] = "-" + data.trophiesOnLoss.ToString();
        lossTrophies.GetComponent<Kozykin.MultiLanguageItem>().text = "-" + data.trophiesOnLoss.ToString();
        playersText.text = "PLAYERS" + "\n" + GameManager.instance.selectedTable.totalPlayers.ToString();
        playersText.GetComponent<Kozykin.MultiLanguageItem>().strItems[0] = "PLAYERS" + "\n" + GameManager.instance.selectedTable.totalPlayers.ToString();
        playersText.GetComponent<Kozykin.MultiLanguageItem>().strItems[1] = "لاعببين" + "\n" + GameManager.instance.selectedTable.totalPlayers.ToString();
        playersText.GetComponent<Kozykin.MultiLanguageItem>().text = "لاعببين" + "\n" + GameManager.instance.selectedTable.totalPlayers.ToString();
        prizeText.text = "PRIZE" + "\n" + ((float)(data.coinsBid * GameManager.instance.selectedTable.firstPosMultiplier) / 1000).ToString() + "K";
        prizeText.GetComponent<Kozykin.MultiLanguageItem>().strItems[0]= "PRIZE" + "\n" + ((float)(data.coinsBid * GameManager.instance.selectedTable.firstPosMultiplier) / 1000).ToString() + "K";
        prizeText.GetComponent<Kozykin.MultiLanguageItem>().strItems[1] = "جائزة" + "\n" + ((float)(data.coinsBid * GameManager.instance.selectedTable.firstPosMultiplier) / 1000).ToString() + "كيلو";
        prizeText.GetComponent<Kozykin.MultiLanguageItem>().text = "جائزة" + "\n" + ((float)(data.coinsBid * GameManager.instance.selectedTable.firstPosMultiplier) / 1000).ToString() + "كيلو";
        bidText.text = ((float)data.coinsBid / 1000).ToString() + "K";
        bidText.GetComponent<Kozykin.MultiLanguageItem>().strItems[0] = ((float)data.coinsBid / 1000).ToString() + "K";
        bidText.GetComponent<Kozykin.MultiLanguageItem>().strItems[1] = ((float)data.coinsBid / 1000).ToString() + "كيلو";
        bidText.GetComponent<Kozykin.MultiLanguageItem>().text = ((float)data.coinsBid / 1000).ToString() + "كيلو";

        currentTrophies.text = ProfileManager.instance.currentPlayer.trophies + "/" + data.maxTrophiesWin;
        currentTrophies.GetComponent<Kozykin.MultiLanguageItem>().strItems[0] = ProfileManager.instance.currentPlayer.trophies + "/" + data.maxTrophiesWin;
        currentTrophies.GetComponent<Kozykin.MultiLanguageItem>().strItems[1] = ProfileManager.instance.currentPlayer.trophies + "/" + data.maxTrophiesWin;
        currentTrophies.GetComponent<Kozykin.MultiLanguageItem>().text = ProfileManager.instance.currentPlayer.trophies + "/" + data.maxTrophiesWin;
        fillingBar.fillAmount = (float)ProfileManager.instance.currentPlayer.trophies / (float)data.maxTrophiesWin;
    }
}
