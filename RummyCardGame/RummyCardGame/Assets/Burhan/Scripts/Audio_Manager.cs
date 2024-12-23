using System.Collections.Generic;
using UnityEngine;
public class Audio_Manager : MonoBehaviour
{
    public static Audio_Manager instance;
    public AudioSource Music_Player, Sound_Effects_Player;
    public List<Sound_Effects> Sound_Effects = new List<Sound_Effects>();
    


    private void OnEnable()
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
        Music_Player.Play();
        Music_Player.loop = true;
        Music_Player.mute = ProfileManager.instance.currentPlayer.isMusicActive;
        Music_Player.volume =(float) ProfileManager.instance.currentPlayer.Music_Volume/10;
        Sound_Effects_Player.volume = (float)ProfileManager.instance.currentPlayer.Sound_Effects_Volume/10;
        Sound_Effects_Player.mute = ProfileManager.instance.currentPlayer.isSoundActive;
    }

    // Update is called once per frame
    void Update()
    {
        if(ProfileManager.instance.currentPlayer !=null)
        {
            Music_Player.volume = (float)ProfileManager.instance.currentPlayer.Music_Volume/10;
            Music_Player.mute = ProfileManager.instance.currentPlayer.isMusicActive;

            Sound_Effects_Player.volume = (float)ProfileManager.instance.currentPlayer.Sound_Effects_Volume/10;
            Sound_Effects_Player.mute = ProfileManager.instance.currentPlayer.isSoundActive;
        }
    }
}

[System.Serializable]
public class Sound_Effects
{
    public AudioClip Sound_Effect;
    public string Sound_Name;
}
