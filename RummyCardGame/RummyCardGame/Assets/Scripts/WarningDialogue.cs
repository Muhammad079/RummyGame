using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WarningDialogue : MonoBehaviour
{
    [SerializeField] private Text message = null;
    public void ShowWarning(string warningMessege)
    {
        this.gameObject.SetActive(true);
        message.text = warningMessege;
    }
}
