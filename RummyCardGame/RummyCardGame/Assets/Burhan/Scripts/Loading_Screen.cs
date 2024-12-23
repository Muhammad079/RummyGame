using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Loading_Screen : MonoBehaviour
{
    //public static Loading_Screen instance;


    private void Awake()
    {
        //if (instance == null)
        //{
        //    instance = this;
        //    DontDestroyOnLoad(gameObject);
        //}
        //else
        //{
        //    Destroy(gameObject);
        //}
    }
    // Start is called before the first frame update
    void Start()
    {
        transform.SetAsLastSibling();
        gameObject.SetActive(false);
        //SceneManager.activeSceneChanged += Turn_On_Loading;
    }

    private void Turn_On_Loading(Scene arg0, Scene arg1)
    {
        gameObject.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        //if(SceneManager.activeSceneChanged)
    }
}
