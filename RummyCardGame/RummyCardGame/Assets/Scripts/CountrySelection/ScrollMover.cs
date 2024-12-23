using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ScrollMover : MonoBehaviour
{
    [SerializeField] private swipeMenu swipeMenu = null;
    [SerializeField] private float scrollMultiplyer = 0;
    void Start()
    {
        GetComponent<Button>().onClick.AddListener(OnClick);
    }
    void OnClick()
    {
        swipeMenu.scroll_pos += scrollMultiplyer;
        Debug.Log(swipeMenu.scroll_pos);
    }
}