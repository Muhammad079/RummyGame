using UnityEngine;
using UnityEngine.UI;

public class LeagueBox : MonoBehaviour
{
    [SerializeField] private Image leagueImage = null;
    [SerializeField] private Text t_Name = null, t_Prize = null,t_Trophies=null;
    [SerializeField] private GameObject selected = null, gems = null, coins = null;
    private Leagues league = null;
    public void PassData(Leagues leagueFile)
    {
        league = leagueFile;
        t_Name.text = league.name;
        leagueImage.sprite = leagueFile.leagueImage;
        t_Prize.text = "Prize : " + league.firstPosReward.quantity;
        t_Trophies.text = "+" + league.trophiesReq;
        if (LeaguesHandler.currentLeague == transform.GetSiblingIndex())
            selected.SetActive(true);
        else
            selected.SetActive(false);
        if (league.firstPosReward.reward == RewardType.coins)
        {
            coins.SetActive(true);
            gems.SetActive(false);
        }
        else
        {
            coins.SetActive(false);
            gems.SetActive(true);
        }
    }
}
