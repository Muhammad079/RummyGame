using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class LoadingSceneScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Invoke(nameof(NextScene),2);  
    }
    void NextScene() {

      SceneManager.LoadScene (GameManager.instance.sceneToLoad);
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
