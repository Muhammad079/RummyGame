using UnityEngine;
using UnityEngine.UI;

public class RichUsers : MonoBehaviour
{
    [SerializeField] private RankDisplayBox firstRank = null, secondRank = null, thirdRank = null;
    [SerializeField] private Transform displayGrid = null;
    [SerializeField] private GameObject richUser = null;
    [SerializeField] private Text My_Name = null, My_Trophies = null;
    [SerializeField] private Image My_Trophies_Image = null;
    [SerializeField] private Sprite Trophy_Sprite = null;
    [SerializeField] private Image Profile_Image = null, Profile_Frame = null;

    void Start()
    {
     
    }
    private void OnEnable()
    {
       if (RankingListHandler.instance)
            FillGrid();
    }
    void FillGrid()
    {
        if (RankingListHandler.instance.richUsers.Count > 0)
        {
            Debug.Log("fillingData   :" + RankingListHandler.instance.richUsers.Count);
            thirdRank.PassData(RankingListHandler.instance.richUsers[RankingListHandler.instance.richUsers.Count - 3]);
            firstRank.PassData(RankingListHandler.instance.richUsers[RankingListHandler.instance.richUsers.Count - 1]);
            secondRank.PassData(RankingListHandler.instance.richUsers[RankingListHandler.instance.richUsers.Count - 2]);
            if (RankingListHandler.instance.richUsers.Count - 4 >= 0)
            {
                for (int n = RankingListHandler.instance.richUsers.Count - 4; n >= 0; n--)
                {
                    if (n < displayGrid.childCount)
                        displayGrid.GetChild(n).GetComponent<RankDisplayBox>().PassData(RankingListHandler.instance.richUsers[n]);
                    else
                        Instantiate(richUser, displayGrid).GetComponent<RankDisplayBox>().PassData(RankingListHandler.instance.richUsers[n]);
                }
            }
        }
    }

    public void Update_Data_Onclick()
    {
        My_Name.text = ProfileManager.instance.currentPlayer.id;
        My_Trophies.text = ProfileManager.instance.currentPlayer.trophies.ToString();
        My_Trophies_Image.sprite = Trophy_Sprite;
    }
    public void Update_Profile()
    {
        Profile_Image.sprite = ProfileManager.instance.currentPlayer.profilePicture;
        Profile_Frame.sprite = ProfileManager.instance.framesDataFile.frames[ProfileManager.instance.currentPlayer.selectedFrameIndex].frameImage;
    }
}