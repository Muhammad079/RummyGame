using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class dropscript : MonoBehaviour , IDropHandler
{

    public void OnDrop(PointerEventData eventData)
      {
       
        if (eventData.pointerDrag != null)
        {
            //CardManager.instance.ondropcard();
            //eventData.pointerDrag.GetComponent<RectTransform>().anchoredPosition = this.GetComponent<RectTransform>().anchoredPosition;
            Debug.Log("OnDrop");
        }

    }
}
