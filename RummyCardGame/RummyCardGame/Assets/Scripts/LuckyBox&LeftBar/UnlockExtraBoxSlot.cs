using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UnlockExtraBoxSlot : MonoBehaviour
{
    [SerializeField] private List<BoxBarScript> boxBars = new List<BoxBarScript>();
   [SerializeField] private int reqGems;

    private void Start()
    {
        GetComponent<Button>().onClick.AddListener(OnClick);
    }
    void OnClick()
    {
        if (ProfileManager.instance.currentPlayer.gems > reqGems)
        {
            foreach (var bar in boxBars)
            {
                if (!bar.isFilled)
                {
                    ProfileManager.instance.currentPlayer.gems -= reqGems;
                   // bar.FillBar("Lucky Box",false);
                    break;
                }
            }
        }
        else
        {
            Debug.Log("Not enough gems");
        }
    }
}
