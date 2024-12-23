using TMPro;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using UnityEngine.SceneManagement;

public class RankDisplayBox : SceneLoader
{
    [SerializeField] private Text userName = null;
    [SerializeField] private Text counter = null;
    [SerializeField] public Text Charm_XP = null;
    [SerializeField] private TextMeshProUGUI level = null;
    [SerializeField] private GameObject diamond = null;
    [SerializeField] private Image frame = null, avatar = null;
    [SerializeField] private Image countryFlag = null;
    private PlayerProfile player;
    internal void PassData(PlayerProfile playerProfile)
    {

        //if (Loading_Screen == null)
        //{
        //    //GameObject root = transform.root.Find("Loading_Screen").gameObject;
        //    Loading_Screen = transform.root.Find("Loading_Screen").gameObject;
        //}
        //if (Loading_animation == null)
        //{
        //    Loading_animation = Loading_Screen.transform.GetChild(0).GetComponent<Text>();
        //}


        this.gameObject.SetActive(true);
        transform.DOScale(Vector3.one, 0.5f);
        player = playerProfile;
        userName.text = playerProfile.name;
        if (counter)
        {
            counter.text = (transform.GetSiblingIndex() + 1).ToString();

            if (counter.transform.parent.name.Contains("GiftProfileBars"))
            {
                counter.text = (transform.GetSiblingIndex() + 4).ToString();
            }
        }
        if(Charm_XP)
        {
            //Charm_XP.text = playerProfile.charmXp.ToString() ;
        }
        if (diamond)
        {
            if (playerProfile.isVip)
            {
                diamond.SetActive(true);
                diamond.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = playerProfile.vipXp.ToString();
            }
        }
        if (level)
            level.text = GameManager.instance.LevelUpData.levelsData[playerProfile.level].name;
        frame.sprite = ProfileManager.instance.framesDataFile.frames[playerProfile.selectedFrameIndex].frameImage;
        avatar.sprite = ProfileManager.instance.avatarDataFile.avatars[playerProfile.selectedAvatarIndex].avatarImage;
        if (countryFlag)
            countryFlag.sprite = GameManager.instance.FlagData(player.country);
    }
    private void Start()
    {
        GetComponent<Button>().onClick.AddListener(Open_Profile);
    }
    void Open_Profile()
    {
        GameManager.instance.User_Profile = player;
        if(Loading_Screen==null)
        {
            Loading_Screen = transform.root.Find("Loading_Screen").gameObject;
        }
        if(Loading_animation==null)
        {
            Loading_animation = Loading_Screen.transform.GetChild(0).GetComponent<Text>();
        }

        StartCoroutine(OnClick());

        //Resources.UnloadUnusedAssets();
        //GameManager.instance.sceneToLoad = "UserProfile";
        //SceneManager.LoadScene("UserProfile");
        
        
        
        
        //GameManager.instance.userProfileHandler.DisplayProfileStats(player);
    }
    public override void Update()
    {
        base.Update();
    }
}