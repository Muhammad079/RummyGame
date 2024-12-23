using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoginScreenHandler : MonoBehaviour
{
    [SerializeField] private GameObject connectionTimeOut = null,loadingScreen;
    public static bool timedOut = false;
    
    void Start()
    {
        DatabaseFunctions.loginComplete += LoginEventFired;
        timedOut = false;
        connectionTimeOut.SetActive(false);
        loadingScreen.SetActive(true);
    }
    void LoginEventFired()
    {
        StartCoroutine(nameof(ShowTimedOut));
    }
    IEnumerator ShowTimedOut()
    {
        yield return new WaitForSeconds(30);
        timedOut = true;
        connectionTimeOut.SetActive(true);
        loadingScreen.SetActive(false);
    }
    private void OnDisable()
    {
        DatabaseFunctions.loginComplete -= LoginEventFired;
    }
    private void Update()
    {
        if (Application.internetReachability == NetworkReachability.NotReachable)
        {
            
            timedOut = true;
            connectionTimeOut.SetActive(true);
            loadingScreen.SetActive(false);
        }
    }
}
