using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Spins_Counter : MonoBehaviour
{
    [HideInInspector]
    public Text spin_count_display;
    // Start is called before the first frame update
    void Start()
    {
        //spin_count = 1;
        
    }

    // Update is called once per frame
    void Update()
    {
        //spin_count_display.text = spin_count.ToString();
        spin_count_display.text = ProfileManager.instance.currentPlayer.spinCounts.ToString();
    }
}
