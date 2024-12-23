using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneInit : MonoBehaviour
{
    [SerializeField] private string sceneObjectPath = "";
    void Start()
    {
        Instantiate(Resources.Load<GameObject>(sceneObjectPath), transform);
    //    Resources.UnloadUnusedAssets();
    }
}
