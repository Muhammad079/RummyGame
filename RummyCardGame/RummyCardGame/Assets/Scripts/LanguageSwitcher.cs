using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LanguageSwitcher : MonoBehaviour
{
    public Sprite Selected_Lang, UnSelected_Lang;
    [SerializeField] private Toggle otherLang = null;
    [SerializeField] private Language thisLang;

    private void OnEnable()
    {
        if(SceneManager.GetActiveScene().name.Contains("Home") || SceneManager.GetActiveScene().name.Contains("GamePlay"))
        {
            if (Manager.instance.m_CurrentLanguage == 0)
            {
                if (Selected_Lang != null && UnSelected_Lang != null && thisLang == Language.english)
                {
                    GetComponent<Image>().sprite = Selected_Lang;
                }
                else
                {
                    GetComponent<Image>().sprite = UnSelected_Lang;
                }
            }
            else if (Manager.instance.m_CurrentLanguage == 1)
            {
                if (Selected_Lang != null && UnSelected_Lang != null && thisLang == Language.arabic)
                {
                    GetComponent<Image>().sprite = Selected_Lang;
                }
                else
                {
                    GetComponent<Image>().sprite = UnSelected_Lang;
                }
            }
        }
    }
    void Start()
    {
        if(GetComponent<Toggle>())
        {
            GetComponent<Toggle>().onValueChanged.AddListener(OnValueUpdate);
        }
        
        if(GetComponent<Button>())
        {
            GetComponent<Button>().onClick.AddListener(OnButtonValueUpdate);
        }
    }
    void OnValueUpdate(bool value)
    {
        otherLang.isOn = !value;
        GameManager.instance.selectedLanguage = thisLang;
        Manager.instance.OnLanguageChange((int)thisLang);
        
    }
    void OnButtonValueUpdate()
    {
        GameManager.instance.selectedLanguage = thisLang;
        Manager.instance.OnLanguageChange((int)thisLang);
    }
}

public enum Language
{
    english, arabic
}
