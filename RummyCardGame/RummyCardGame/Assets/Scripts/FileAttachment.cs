using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static NativeGallery;

public class FileAttachment : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        GetComponent<Button>().onClick.AddListener(OnClick);
    }

    // Update is called once per frame
    void Update()
    {

    }
    void OnClick()
    {
       
    }

    private void ImageGetCallBack(string[] paths)
    {
        Texture2D aa = NativeGallery.LoadImageAtPath(paths[0], -1, false, true, false);
        GetComponent<Image>().sprite = Sprite.Create(aa, new Rect(Vector2.zero, new Vector2(aa.width, aa.height)), new Vector2(0, 0));
    }
}
