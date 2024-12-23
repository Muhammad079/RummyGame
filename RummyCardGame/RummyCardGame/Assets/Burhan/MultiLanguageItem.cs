using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
namespace Kozykin
{
    public class MultiLanguageItem : FixGUITextCS
    {
        [Header("English - Arabic")]

        public string[] strItems;
        public Sprite[] spriteItems;
        public int selectedLanguage;
        void OnEnable()
        {
            Manager.onSwitchLanguage += SwitchLanguage;
            selectedLanguage = PlayerPrefs.GetInt("Language", 0);
            SwitchLanguage(PlayerPrefs.GetInt("Language", 0));
        }

        void OnDisable()
        {
            Manager.onSwitchLanguage -= SwitchLanguage;
        }

        public void SwitchLanguage(int index)
        {
            selectedLanguage = index;
            UnityEngine.UI.Text text = GetComponent<UnityEngine.UI.Text>();

            if (text != null)
            {
                text.text = strItems[index];
            }

            Image image = GetComponent<Image>();
            if (image != null)
            {
                if (spriteItems.Length > index && spriteItems[index] != null)
                    image.sprite = spriteItems[index];
            }

            TextMeshPro txtMshPro = GetComponent<TextMeshPro>();
            if (txtMshPro != null)
                txtMshPro.text = strItems[index];

            TextMeshProUGUI txtMshProGUI = GetComponent<TextMeshProUGUI>();
            if (txtMshProGUI != null)
                txtMshProGUI.text = strItems[index];
        }
        public override void Update()
        {
            base.Update();
            if (Manager.instance.m_CurrentLanguage == 0)
            {
                GetComponent<Text>().text = strItems[0]; 
            }
        }
    }
}
