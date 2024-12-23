using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HowToPlayGamePlayHandler : MonoBehaviour
{
    public GameObject ParentInstructiuons;

    public List<GameObject> InstructionScreens;

    public int Count;

    public Button RightBtn;
    public Button LeftBtn;

    // Start is called before the first frame update
    void Start()
    {

        RightBtn.onClick.AddListener(OnRightBtn);
        LeftBtn.onClick.AddListener(OnLeftBtn);


    }
    private void OnEnable()
    {
        Count = 0;
        TurnScreenActive(0);
    }
    private void OnLeftBtn()
    {
        Count = Count- 1;
        
        Debug.LogError("On left btn");
        if(Count <0)
        {
            Count = 0;
        }
        TurnScreenActive(Count);
    }

    private void OnRightBtn()
    {
        Debug.LogError("OnRightBtn");
        Count = Count+ 1;
        TurnScreenActive(Count);
    }

    void TurnScreenActive(int screenNo)
    {
        if (screenNo >= 6)
        {
            ParentInstructiuons.gameObject.SetActive(false);
        }
        else
        {
            InstructionScreens.ForEach(x => x.SetActive(false));
            InstructionScreens[Count].gameObject.SetActive(true);
        }
    }

    private void OnDisable()
    {
        InstructionScreens.ForEach(x => x.SetActive(false));

    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
