using UnityEngine;
using UnityEngine.UI;

public class TournamentUsers : MonoBehaviour
{
    [SerializeField] private RankDisplayBox firstRank = null, secondRank = null, thirdRank = null;
    [SerializeField] private Transform displayGrid = null;
    [SerializeField] private GameObject richUser = null;
    [SerializeField] private Text My_Name = null, My_Tour_Lvl = null;
    [SerializeField] private Image My_Trophies_Image = null;
    [SerializeField] private Sprite Trophy_Sprite = null;
    public Image My_Frame, My_Avatar;
    void Start()
    {
        //  RankingListHandler.instance.tournamentRankLoaded += FillGrid;
    }
    private void OnEnable()
    {
        //FillGrid();
        Invoke(nameof(FillGrid), 2);
        Update_Data_Onclick();
    }
    void FillGrid()
    {
        for (int n = 0; n < displayGrid.childCount; n++)
            displayGrid.GetChild(n).gameObject.SetActive(false);
        Debug.Log("DisplayingTournamentUsers");
        if (RankingListHandler.instance.tournamentsRank.Count >= 3)
            thirdRank.PassData(RankingListHandler.instance.tournamentsRank[RankingListHandler.instance.tournamentsRank.Count - 3]);
        else
            thirdRank.gameObject.SetActive(false);
        if (RankingListHandler.instance.tournamentsRank.Count >= 1)
            firstRank.PassData(RankingListHandler.instance.tournamentsRank[RankingListHandler.instance.tournamentsRank.Count - 1]);
        else
            firstRank.gameObject.SetActive(false);
        if (RankingListHandler.instance.tournamentsRank.Count >= 2)
            secondRank.PassData(RankingListHandler.instance.tournamentsRank[RankingListHandler.instance.tournamentsRank.Count - 2]);
        else
            secondRank.gameObject.SetActive(false);
        if (RankingListHandler.instance.tournamentsRank.Count - 4 >= 0)
        {
            for (int n = RankingListHandler.instance.tournamentsRank.Count - 4; n >= 0; n--)
            {
                //Debug.Log("Value of N: " + n);
                ////if (RankingListHandler.instance.tournamentsRank.Count - 4-n < displayGrid.childCount)
                //if (n == 0)
                //{
                //    Debug.Log("Index: " + (RankingListHandler.instance.tournamentsRank.Count - 4 - n));
                //    displayGrid.GetChild(n).GetComponent<RankDisplayBox>().PassData(RankingListHandler.instance.tournamentsRank[n]);
                //    displayGrid.GetChild(n).gameObject.SetActive(true);

                //}
                //else
                //{
                //    GameObject a = Instantiate(richUser, displayGrid);
                //    a.GetComponent<RankDisplayBox>().PassData(RankingListHandler.instance.tournamentsRank[n]);
                //    a.SetActive(true);

                //}
                GameObject a = Instantiate(richUser, displayGrid);
                a.GetComponent<RankDisplayBox>().PassData(RankingListHandler.instance.tournamentsRank[n]);
                a.SetActive(true);


            }
        }
    }
    public void Update_Data_Onclick()
    {
        My_Avatar.sprite = ProfileManager.instance.currentPlayer.profilePicture;
        My_Frame.sprite = ProfileManager.instance.framesDataFile.frames[ProfileManager.instance.currentPlayer.selectedFrameIndex].frameImage; 
        My_Name.text = ProfileManager.instance.currentPlayer.name;
        My_Tour_Lvl.text = ProfileManager.instance.currentPlayer.tournamentLevel.ToString();
        My_Trophies_Image.sprite = Trophy_Sprite;
    }
}
