using UnityEngine;
using UnityEngine.UI;

public class Gift_Users : MonoBehaviour
{
    [SerializeField] private RankDisplayBox firstRank = null, secondRank = null, thirdRank = null;
    [SerializeField] private Transform displayGrid = null;
    [SerializeField] private GameObject Gift_Profile_Bar = null;

    [SerializeField] private Text My_Name = null, My_CharmXP = null;
    [SerializeField] private Image Profile_Image = null, Profile_Frame = null;
    //[SerializeField] private Image My_CharmXP_Image = null;
    //[SerializeField] private Sprite CharmXP_Sprite = null;

    [SerializeField] Button Total_btn = null, Monthly_btn = null, Weekly_btn = null;
    void Start()
    {
        Total_btn.onClick.AddListener(delegate { FillGrid(1); });
        Weekly_btn.onClick.AddListener(delegate { FillGrid(2); });
        Monthly_btn.onClick.AddListener(delegate { FillGrid(3); });
    }
    private void OnEnable()
    {


        if (RankingListHandler.instance)
            FillGrid(1); Update_Profile();
    }
    void FillGrid(int value)
    {
        for (int n = 0; n < displayGrid.childCount; n++)
            displayGrid.GetChild(n).gameObject.SetActive(false);
        if (value == 1)
        {
            Debug.Log("fillingData   :" + RankingListHandler.instance.giftRanks.Count);
            if (RankingListHandler.instance.giftRanks.Count >= 3)
                thirdRank.PassData(RankingListHandler.instance.giftRanks[RankingListHandler.instance.giftRanks.Count - 3]);
            else
                thirdRank.gameObject.SetActive(false);
            if (RankingListHandler.instance.giftRanks.Count >= 1)
                firstRank.PassData(RankingListHandler.instance.giftRanks[RankingListHandler.instance.giftRanks.Count - 1]);
            else
                firstRank.gameObject.SetActive(false);
            if (RankingListHandler.instance.giftRanks.Count >= 2)
                secondRank.PassData(RankingListHandler.instance.giftRanks[RankingListHandler.instance.giftRanks.Count - 2]);
            else
                secondRank.gameObject.SetActive(false);
            if (RankingListHandler.instance.giftRanks.Count - 4 >= 0)
            {

                for (int n = RankingListHandler.instance.giftRanks.Count - 4; n >= 0; n--)
                {
                    if (n < displayGrid.childCount)
                        displayGrid.GetChild(n).GetComponent<RankDisplayBox>().PassData(RankingListHandler.instance.giftRanks[n]);
                    else
                        Instantiate(Gift_Profile_Bar, displayGrid).GetComponent<RankDisplayBox>().PassData(RankingListHandler.instance.giftRanks[n]);
                    displayGrid.GetChild(n).GetComponent<RankDisplayBox>().Charm_XP.text = RankingListHandler.instance.giftRanks[n].charmXp.ToString();
                    displayGrid.GetChild(n).gameObject.SetActive(true);
                }
            }
        }
        else if (value == 2)
        {

            Debug.Log("fillingData   :" + RankingListHandler.instance.giftRanks_Weekly.Count);
            if (RankingListHandler.instance.giftRanks_Weekly.Count >= 3)
                thirdRank.PassData(RankingListHandler.instance.giftRanks_Weekly[RankingListHandler.instance.giftRanks_Weekly.Count - 3]);
            else
                thirdRank.gameObject.SetActive(false);
            if (RankingListHandler.instance.giftRanks_Weekly.Count >= 1)
                firstRank.PassData(RankingListHandler.instance.giftRanks_Weekly[RankingListHandler.instance.giftRanks_Weekly.Count - 1]);
            else
                firstRank.gameObject.SetActive(false);
            if (RankingListHandler.instance.giftRanks_Weekly.Count >= 2)
                secondRank.PassData(RankingListHandler.instance.giftRanks_Weekly[RankingListHandler.instance.giftRanks_Weekly.Count - 2]);
            else
                secondRank.gameObject.SetActive(false);
            if (RankingListHandler.instance.giftRanks_Weekly.Count - 4 >= 0)
            {

                for (int n = RankingListHandler.instance.giftRanks_Weekly.Count - 4; n >= 0; n--)
                {
                    if (n < displayGrid.childCount)
                        displayGrid.GetChild(n).GetComponent<RankDisplayBox>().PassData(RankingListHandler.instance.giftRanks_Weekly[n]);
                    else
                        Instantiate(Gift_Profile_Bar, displayGrid).GetComponent<RankDisplayBox>().PassData(RankingListHandler.instance.giftRanks_Weekly[n]);
                    displayGrid.GetChild(n).GetComponent<RankDisplayBox>().Charm_XP.text = RankingListHandler.instance.giftRanks_Weekly[n].charmXp_Weekly.ToString();
                    displayGrid.GetChild(n).gameObject.SetActive(true);
                }
            }
        }
        if (value == 3)
        {

            if (RankingListHandler.instance.giftRanks_Monthly.Count > 0)
            {
                Debug.Log("fillingData   :" + RankingListHandler.instance.giftRanks_Monthly.Count);
                if (RankingListHandler.instance.giftRanks_Monthly.Count >= 3)
                    thirdRank.PassData(RankingListHandler.instance.giftRanks_Monthly[RankingListHandler.instance.giftRanks_Monthly.Count - 3]);
                else
                    thirdRank.gameObject.SetActive(false);
                if (RankingListHandler.instance.giftRanks_Monthly.Count >= 1)
                    firstRank.PassData(RankingListHandler.instance.giftRanks_Monthly[RankingListHandler.instance.giftRanks_Monthly.Count - 1]);
                else
                    firstRank.gameObject.SetActive(false);
                if (RankingListHandler.instance.giftRanks_Monthly.Count >= 2)
                    secondRank.PassData(RankingListHandler.instance.giftRanks_Monthly[RankingListHandler.instance.giftRanks_Monthly.Count - 2]);
                else
                    secondRank.gameObject.SetActive(false);
                if (RankingListHandler.instance.giftRanks_Monthly.Count - 4 >= 0)
                {

                    for (int n = RankingListHandler.instance.giftRanks_Monthly.Count - 4; n >= 0; n--)
                    {
                        if (n < displayGrid.childCount)
                            displayGrid.GetChild(n).GetComponent<RankDisplayBox>().PassData(RankingListHandler.instance.giftRanks_Monthly[n]);
                        else
                            Instantiate(Gift_Profile_Bar, displayGrid).GetComponent<RankDisplayBox>().PassData(RankingListHandler.instance.giftRanks_Monthly[n]);
                        displayGrid.GetChild(n).GetComponent<RankDisplayBox>().Charm_XP.text = RankingListHandler.instance.giftRanks_Monthly[n].charmXp_Monthly.ToString();
                        displayGrid.GetChild(n).gameObject.SetActive(true);
                    }
                }
            }
        }
    }

    public void Update_Data_Onclick(int val)
    {
        My_Name.text = ProfileManager.instance.currentPlayer.name;
        if (val == 1)
        {
            My_CharmXP.text = ProfileManager.instance.currentPlayer.charmXp.ToString();
            //My_Trophies_Image.sprite = Trophy_Sprite;
        }
        else if (val == 2)
        {
            My_CharmXP.text = ProfileManager.instance.currentPlayer.charmXp_Monthly.ToString();
            //My_Trophies_Image.sprite = Trophy_Sprite;
        }
        else if (val == 3)
        {

            My_CharmXP.text = ProfileManager.instance.currentPlayer.charmXp_Weekly.ToString();
            //My_Trophies_Image.sprite = Trophy_Sprite;
        }
    }
    public void Update_Profile()
    {
        Profile_Image.sprite = ProfileManager.instance.currentPlayer.profilePicture;
        Profile_Frame.sprite = ProfileManager.instance.framesDataFile.frames[ProfileManager.instance.currentPlayer.selectedFrameIndex].frameImage;
    }
}