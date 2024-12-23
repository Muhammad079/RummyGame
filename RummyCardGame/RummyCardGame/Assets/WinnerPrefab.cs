using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WinnerPrefab : MonoBehaviour
{
    public Text Score;
    public Text userName;
    public Image userAvatar;

    public void SetData(string _userName, string _score, int AvatarNo = 0)
    {
        Score.text = _score;
        userName.text = _userName;
        userAvatar.sprite = GetAvatarSprite(AvatarNo);
    }

    private Sprite GetAvatarSprite(int avatarNo)
    {
        return ProfileManager.instance.avatarDataFile.avatars[avatarNo].avatarImage;
    }
}