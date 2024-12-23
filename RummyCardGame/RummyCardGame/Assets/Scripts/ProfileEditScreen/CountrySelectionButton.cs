using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class CountrySelectionButton : MonoBehaviour
{
    public ProfileEditHandler profileEditHandler = null;
    string countryName = "";
    // Start is called before the first frame update
    void Start()
    {
        GetComponent<Button>().onClick.AddListener(OnClick);
    }
    public void PassData(Sprite countryImage)
    {
        GetComponent<Image>().sprite = countryImage;
        countryName = countryImage.name ;
    }
    void OnClick()
    {
        countryName = countryName.Replace("flag-of-", "");
        profileEditHandler.ShowCountryDropdown();
        profileEditHandler.ChangeCountry(GetComponent<Image>().sprite, countryName);
    }
}
