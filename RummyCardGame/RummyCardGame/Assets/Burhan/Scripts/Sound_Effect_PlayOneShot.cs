using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Sound_Effect_PlayOneShot : MonoBehaviour
{
    public int Sound_Effect_index;

    // Start is called before the first frame update
    void Start()
    {
        GetComponent<Button>().onClick.AddListener(()=> {
            Audio_Manager.instance.Sound_Effects_Player.PlayOneShot(Audio_Manager.instance.Sound_Effects[Sound_Effect_index].Sound_Effect);
        });
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
