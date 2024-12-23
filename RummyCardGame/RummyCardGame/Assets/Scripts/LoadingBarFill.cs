using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LoadingBarFill : MonoBehaviour
{
    [SerializeField] private Image fillingImage = null;
    float n = 0;
    bool incremental = false;
    [SerializeField] private float barSpeed = 0;
    void Start()
    {
        fillingImage.fillClockwise = true;
        incremental = true;
    }
    void LateUpdate()
    {
        if (n >= 1)
        {
            incremental = false;
         //   fillingImage.fillClockwise = false;
        }
        else if (n <= 0)
        {
            incremental = true;
            fillingImage.fillClockwise = true;
        }
        if (incremental)
        {
            n += barSpeed;
        }
        else
        {
            n -= barSpeed;
        }
        fillingImage.fillAmount = n;
    }
}
