using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Manager : MonoBehaviour
{
    public static Manager instance;
    public delegate void SwitchLanguage(int ind);
    public static SwitchLanguage onSwitchLanguage;
    //public Toggle[] flagToggles;
    public int m_CurrentLanguage = -1; // Default : English

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        if(GameManager.instance.selectedLanguage==Language.english)
        {
            PlayerPrefs.SetInt("Language", 0);
            //m_CurrentLanguage = 0;
        }
        if (GameManager.instance.selectedLanguage == Language.arabic)
        {
            PlayerPrefs.SetInt("Language", 1);
            //m_CurrentLanguage = 1;
        }

        m_CurrentLanguage = PlayerPrefs.GetInt("Language", 0);
        OnLanguageChange(m_CurrentLanguage);
        //flagToggles[m_CurrentLanguage].isOn = true;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void OnLanguageChange(int index)
    {
        Debug.Log("index: " + index);

        //if (flagToggles[index].isOn)
        if(m_CurrentLanguage!=-1)
        {
            Debug.Log("OnLanguageChange" + m_CurrentLanguage);
            PlayerPrefs.SetInt("Language", index);
            PlayerPrefs.Save();
            m_CurrentLanguage = index;

            // ClearQuizList();
            //ReadQuizFile();
            if (onSwitchLanguage != null)
                onSwitchLanguage(index);
        }
    }
}
