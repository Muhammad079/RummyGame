using UnityEngine;
using UnityEngine.SceneManagement;
public class UIManager : MonoBehaviour
{
    public GameObject GameWinPanel;
    public GameObject GameLoosePanel;
    [SerializeField] private int scoreUpdater = 0;

    // Start is called before the first frame update
    void Start()
    {
        GameWinPanel.SetActive(false);
        GameLoosePanel.SetActive(false);
    }

    // Update is called once per frame
    void Update()

    {
        if (CardManager.instance.score >= scoreUpdater)
        {
            Winpanel();
        }
        if (CardManager.instance.score <= -scoreUpdater)
        {
            LoosePanel();
        }

    }
    public void Winpanel()
    {
        if (!GameWinPanel.activeSelf)
        {
            GameWinPanel.SetActive(true);
            GameLoosePanel.SetActive(false);
            //     GameManager.instance.GameWin(1000);
        }
    }
    public void LoosePanel()
    {
        //if (!GameLoosePanel.activeSelf)
        //{
        //    if (GameManager.instance.Replayable())
        //    {
        //        GameLoosePanel.SetActive(true);
        //        GameWinPanel.SetActive(false);
        //    }
        //    else
        //    {
        //        GameManager.instance.GameLoss();
        //    }
        //}
    }
    public void RestartButton()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
