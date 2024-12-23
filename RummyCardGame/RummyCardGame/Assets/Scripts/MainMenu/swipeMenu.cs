using Coffee.UIEffects;
using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;
public class swipeMenu : MonoBehaviour
{
    //public GameObject[] Children;
    public GameObject scrollbar;
    public float scroll_pos;
    float[] pos;
    int Lcount = 0, Fcount = 4; int instanstiated_Last_sibling_index = 0, instanstiated_First_sibling_index = 0;
    string Direction;
    public Vector3 Initial_rect { get; private set; }


    private void Start()
    {
        scrollbar.GetComponent<Scrollbar>().value = 0;
    }
    //private void OnEnable()
    //{
    //    Lcount = 0;
    //    Fcount = Children.Length-1;
    //    Debug.Log("children length: "+ Children.Length);
    //}
    //// Start is called before the first frame update
    //void Start()
    //{


    //    for (int i = Children.Length-1; i>=0; i--)
    //    {
    //        var child = Instantiate(Children[i], transform);
    //        child.transform.SetAsFirstSibling();
    //    }
    //    for (int i = 0; i < Children.Length; i++)
    //    {
    //        var child = Instantiate(Children[i], transform);
    //        child.transform.SetAsLastSibling();
    //    }
    //    instanstiated_First_sibling_index = transform.GetChild(0).transform.GetSiblingIndex();
    //    instanstiated_Last_sibling_index = transform.GetChild(transform.childCount - 1).transform.GetSiblingIndex();
    //    scrollbar.GetComponent<Scrollbar>().value = .35f;
    //    InvokeRepeating(nameof(Child_Check), 0.1f, 0.1f);
    //}

    //private void Child_Check()
    //{
    //    Debug.Log("Instantiated child Index: " + instanstiated_Last_sibling_index);
    //    Debug.Log("2nd Last Child: " + transform.GetChild(transform.childCount - 2).transform.GetSiblingIndex());
    //    int value = transform.GetChild(transform.childCount - 2).transform.GetSiblingIndex() - instanstiated_Last_sibling_index;
    //    Debug.Log("Sutract value: " + value);
    //    int value2 = transform.GetChild(transform.childCount - 1).transform.GetSiblingIndex() - instanstiated_Last_sibling_index;
    //    Debug.Log("Sutract value 2: " + value2);


    //    int Fvalue = transform.GetChild(1).transform.GetSiblingIndex() - instanstiated_First_sibling_index;
    //    Debug.Log("Sutract First value: " + Fvalue);
    //    int Fvalue2 = transform.GetChild(0).transform.GetSiblingIndex() - instanstiated_First_sibling_index;
    //    Debug.Log("Sutract First value 2: " + Fvalue2);

    //    if (/*transform.GetChild(transform.childCount - 2).gameObject.GetComponent<Button>().interactable ||*/ transform.GetChild(transform.childCount - 2).transform.localScale.x > 1
    //        && transform.GetChild(transform.childCount - 2).transform.GetSiblingIndex() - instanstiated_Last_sibling_index == value//-1
    //        ||
    //        /*transform.GetChild(transform.childCount - 1).gameObject.GetComponent<Button>().interactable ||*/ transform.GetChild(transform.childCount - 1).transform.localScale.x > 1
    //        && transform.GetChild(transform.childCount - 1).transform.GetSiblingIndex() - instanstiated_Last_sibling_index == value2) //0)
    //    {

    //        var child = Instantiate(Children[Lcount], transform);
    //        instanstiated_Last_sibling_index = child.transform.GetSiblingIndex();
    //        Lcount++;
    //        if(Lcount == /*5*/ Children.Length)
    //        {
    //            Lcount = 0;
    //        }
    //    }
    //    else if(/*transform.GetChild(1).gameObject.GetComponent<Button>().interactable ||*/ transform.GetChild(1).transform.localScale.x > 1
    //            && transform.GetChild(1).transform.GetSiblingIndex() - instanstiated_First_sibling_index == Fvalue//-1
    //            ||
    //            /*transform.GetChild(0).gameObject.GetComponent<Button>().interactable ||*/ transform.GetChild(0).transform.localScale.x > 1
    //            && transform.GetChild(0).transform.GetSiblingIndex() - instanstiated_First_sibling_index == Fvalue2)
    //    {
    //        var child = Instantiate(Children[Fcount], transform);
    //        child.transform.SetAsFirstSibling();
    //        instanstiated_First_sibling_index = child.transform.GetSiblingIndex();
    //        Fcount--;
    //        if (Fcount == -1)
    //        {
    //            Fcount = Children.Length-1 /*4*/;
    //        }
    //    }


    //    if (scrollbar.GetComponent<Scrollbar>().value > 0.50f)
    //    {
    //        Direction = "Right";
    //    }
    //    else if (scrollbar.GetComponent<Scrollbar>().value < 0.50f)
    //    {
    //        Direction = "Left";
    //    }
    //    for (int i = 0; i < Children.Length; i++)
    //    {


    //        if (transform.childCount > 15 && Children[i].transform.localScale.x > 1/*.GetComponent<Button>().interactable*/ && Direction == "Right")
    //        {
    //            Destroy(transform.GetChild(0).gameObject);
    //        }
    //        else if (transform.childCount > 15 && Children[i].transform.localScale.x > 1/*.GetComponent<Button>().interactable*/ && Direction == "Left")
    //        {
    //            Destroy(transform.GetChild(transform.childCount - 1).gameObject);
    //        }
    //    }

    //}
    // Update is called once per frame
    void Update()
    {
        
        

        pos = new float[transform.childCount];
        float distance = 1f / (pos.Length - 1f);
        for (int i = 0; i < pos.Length; i++)
        {
            pos[i] = distance * i;
        }
        scroll_pos = scrollbar.GetComponent<Scrollbar>().value;
        if (Input.GetMouseButton(0))
        {
            scroll_pos = scrollbar.GetComponent<Scrollbar>().value;
        }
        else
        {
            //if (scroll_pos == 1)
            //{
            //    transform.position = Initial_rect;
            //    scrollbar.GetComponent<Scrollbar>().value = Mathf.Lerp(scrollbar.GetComponent<Scrollbar>().value, pos[0], 0f);
            //}
            //else
            //{
                for (int i = 0; i < pos.Length; i++)
                {
                    if (scroll_pos < pos[i] + (distance / 2) && scroll_pos > pos[i] - (distance / 2))
                    {
                        scrollbar.GetComponent<Scrollbar>().value = Mathf.Lerp(scrollbar.GetComponent<Scrollbar>().value, pos[i], 0f);
                    }
                }
            //}
            
        }


        for (int i = 0; i < pos.Length; i++)
        {
            if (scroll_pos < pos[i] + (distance / 2) && scroll_pos > pos[i] - (distance / 2))
            {
                transform.GetChild(i).localScale = Vector2.Lerp(transform.GetChild(i).localScale, new Vector2(1.2f, 1.2f), 0.1f);
                if (transform.GetChild(i).GetComponent<Button>())
                    transform.GetChild(i).GetComponent<Button>().interactable = true;
                //if(transform.GetChild(i).GetComponent<UIShiny>())
                //   transform.GetChild(i).GetComponent<UIShiny>().enabled = true;
                for (int j = 0; j < pos.Length; j++)
                {
                    if (j != i)
                    {
                        transform.GetChild(j).localScale = Vector2.Lerp(transform.GetChild(j).localScale, new Vector2(0.8f, 0.8f), 0.1f);
                        if (transform.GetChild(i).GetComponent<Button>())
                            transform.GetChild(j).GetComponent<Button>().interactable = false;
                        if (transform.GetChild(j).GetComponent<UIShiny>())
                            transform.GetChild(j).GetComponent<UIShiny>().enabled = false;
                    }
                }
            }
        }

    }
}