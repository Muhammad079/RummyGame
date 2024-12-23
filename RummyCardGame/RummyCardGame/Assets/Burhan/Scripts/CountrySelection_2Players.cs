using UnityEngine;
using UnityEngine.UI;

public class CountrySelection_2Players : MonoBehaviour
{
    [SerializeField] private Text winTrophies = null, lossTrophies = null, playersText = null, prizeText = null, bidText = null, currentTrophies = null;
    [SerializeField] private Image fillingBar = null;
    void OnEnable()
    {
        var data = GameManager.instance.selectedXPData.data[transform.GetSiblingIndex()];
        winTrophies.text = "+" + data.trophiesOnWin.ToString();
        lossTrophies.text = "-" + data.trophiesOnLoss.ToString();
        playersText.text = "PLAYERS" + "\n" + GameManager.instance.selectedTable.totalPlayers.ToString();
        prizeText.text = "PRIZE" + "\n" + ((float)(data.coinsBid * GameManager.instance.selectedTable.firstPosMultiplier) / 1000).ToString() + "K";
        bidText.text = ((float)data.coinsBid / 1000).ToString() + "K";
        currentTrophies.text = ProfileManager.instance.currentPlayer.trophies + "/" + data.maxTrophiesWin;
        fillingBar.fillAmount = (float)ProfileManager.instance.currentPlayer.trophies / (float)data.maxTrophiesWin;
    }
}
