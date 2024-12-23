using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CountrySelectionGrid : MonoBehaviour
{
     void Start()
    {
        for(int n = 0; n < GameManager.instance.countriesFlags.Count; n++)
        {
            if (n >= transform.childCount)
                Instantiate(transform.GetChild(0).gameObject, transform);
            transform.GetChild(n).GetComponent<CountrySelectionButton>().PassData(GameManager.instance.countriesFlags[n]);
        }
    }
 
}
