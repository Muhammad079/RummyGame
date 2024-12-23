using UnityEngine;
using System.Collections;
using ArabicSupport;
using UnityEngine.UI;
using TMPro;

public class FixGUITextCS : MonoBehaviour {
	
	public string text;
	public bool tashkeel = true;
	public bool hinduNumbers = true;
	
	// Use this for initialization
	void Start () {
		
		
	}
	
	// Update is called once per frame
	public virtual void Update () {

		if (Manager.instance.m_CurrentLanguage == 1)
		{
			GetComponent<Kozykin.MultiLanguageItem>().strItems[1] = ArabicFixer.Fix(text, tashkeel, hinduNumbers);

			if(gameObject.GetComponent<Text>())
            {
				gameObject.GetComponent<Text>()/*.guiText*/.text = ArabicFixer.Fix(text, tashkeel, hinduNumbers);
			}
			if(gameObject.GetComponent<TextMeshProUGUI>())
            {
				gameObject.GetComponent<TextMeshProUGUI>()/*.guiText*/.text = ArabicFixer.Fix(text, tashkeel, hinduNumbers);
			}
		}
	}
}
