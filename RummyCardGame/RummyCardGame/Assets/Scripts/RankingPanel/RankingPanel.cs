using DG.Tweening;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class RankingPanel : SceneLoader
{
    [SerializeField] private GameObject blackOverlay = null;//, Info_Panel = null;
    //public Button info_btn, Chat_UI_Info_Close;
    //public Text Info_Panel_text;

    private void Start()
    {
        blackOverlay.SetActive(false);
        blackOverlay.GetComponent<Button>().onClick.AddListener(HidePanel);
        if(SceneManager.GetActiveScene().name== "RankingScreen")
        {
            ShowPanel();
        }
        //info_btn.onClick.AddListener(()=> {
        //    Info_Panel.SetActive(true);
        //    Info_Panel.transform.GetChild(1).transform.DOMoveY(0, 1);
        //    Info_Panel_text.text = "Ranking Panel";
        //    Chat_UI_Info_Close.onClick.AddListener(() => {
        //        Info_Panel.transform.GetChild(1).transform.DOMoveY(10, 1).OnComplete(() => {
        //            Info_Panel.SetActive(false);
        //        });
        //    });
        //});
    }
    public void ShowPanel()
    {

        transform.localScale = Vector3.one;
        transform.DOLocalMove(Vector3.zero, 1f).OnComplete(() =>
        {
            blackOverlay.SetActive(true);
        });
        GetComponent<Shiny_Effect_Pauser>()?.Pause();
    }
    public void HidePanel()
    {
        //blackOverlay.transform.DOScale(Vector3.zero, 0.5f).SetEase(Ease.InCubic);
        blackOverlay.transform.DOLocalMoveX(293f, 1).OnComplete(() =>
        {

            blackOverlay.SetActive(false);

        });
        transform.DOLocalMoveX(600, 1f).OnComplete(() =>
        {
            transform.localScale = Vector3.zero;
            StartCoroutine(OnClick());
            //Resources.UnloadUnusedAssets();
            //GameManager.instance.sceneToLoad = "Home";
            //SceneManager.LoadScene("Home");

        });
        GetComponent<Shiny_Effect_Pauser>()?.StartShine();

        

    }
    public override void Update()
    {
        base.Update();
    }
}
