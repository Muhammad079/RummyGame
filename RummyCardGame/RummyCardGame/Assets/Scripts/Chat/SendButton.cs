using UnityEngine;
using UnityEngine.UI;

public class SendButton : MonoBehaviour
{
    [SerializeField] private InputField messegeInput = null;
    [SerializeField] private ChatScreen chatScreen = null;
    void Start()
    {
        GetComponent<Button>().onClick.AddListener(OnClick);
    }

    void OnClick()
    {
        messegeInput.text = messegeInput.text.ToLower();
        messegeInput.text = messegeInput.text.Replace("fuck", "****");
        messegeInput.text = messegeInput.text.Replace("shit", "****");
        messegeInput.text = messegeInput.text.Replace("dick", "****");
        messegeInput.text = messegeInput.text.Replace("pussy", "****");
        messegeInput.text = messegeInput.text.Replace("gay", "****");
        messegeInput.text = messegeInput.text.Replace("dick head", "****");
        messegeInput.text = messegeInput.text.Replace("asshole", "****");
        messegeInput.text = messegeInput.text.Replace("bitch", "****");
        messegeInput.text = messegeInput.text.Replace("son of bitch", "****");
        messegeInput.text = messegeInput.text.Replace("bastard", "****");
        messegeInput.text = messegeInput.text.Replace("bollocks", "****");
        messegeInput.text = messegeInput.text.Replace("bugger", "****");
        messegeInput.text = messegeInput.text.Replace("hell", "****");
        messegeInput.text = messegeInput.text.Replace("bloody hell", "****");
        messegeInput.text = messegeInput.text.Replace("choad", "****");
        messegeInput.text = messegeInput.text.Replace("wanker", "****");
        messegeInput.text = messegeInput.text.Replace("wank", "****");
        messegeInput.text = messegeInput.text.Replace("get stuffed", "****");
        messegeInput.text = messegeInput.text.Replace("suck", "****");
        messegeInput.text = messegeInput.text.Replace("hore", "****");
        messegeInput.text = messegeInput.text.Replace("whore", "****");
        messegeInput.text = messegeInput.text.Replace("cum", "****");
        messegeInput.text = messegeInput.text.Replace("trumpet", "****");
        messegeInput.text = messegeInput.text.Replace("piss", "****");
        messegeInput.text = messegeInput.text.Replace("twat", "****");
        messegeInput.text = messegeInput.text.Replace("shitbag", "****");
        messegeInput.text = messegeInput.text.Replace("shit", "****");
        messegeInput.text = messegeInput.text.Replace("tits", "****");
        messegeInput.text = messegeInput.text.Replace("virginia", "****");
        messegeInput.text = messegeInput.text.Replace("boobs", "****");
        messegeInput.text = messegeInput.text.Replace("orgasm", "****");
        messegeInput.text = messegeInput.text.Replace("orgi", "****");
        messegeInput.text = messegeInput.text.Replace("weed", "****");
        messegeInput.text = messegeInput.text.Replace("cook", "****");
        messegeInput.text = messegeInput.text.Replace("cooc", "****");
        messegeInput.text = messegeInput.text.Replace("coc", "****");
        messegeInput.text = messegeInput.text.Replace("ass", "****");
        messegeInput.text = messegeInput.text.Replace("nipple", "****");
        messegeInput.text = messegeInput.text.Replace("drug", "****");
        messegeInput.text = messegeInput.text.Replace("sex", "****");
        messegeInput.text = messegeInput.text.Replace("porn", "****");
        messegeInput.text = messegeInput.text.Replace("fagit", "****");

        if (messegeInput.text != "")
        {
            chatScreen.SendButton(messegeInput.text);
            messegeInput.text = "";
        }

        
    }
}
