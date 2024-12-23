//using Facebook.Unity;
//using System.Collections.Generic;
//using UnityEngine;
//using UnityEngine.SceneManagement;
//using UnityEngine.UI;

//public class SocialLogin : MonoBehaviour
//{
//    public static bool isLoggedIn = false;
//    public static string debugMessege = "";
//    [SerializeField] private GameObject loadingBar = null;

//    [SerializeField] private Text debugText;
//    [SerializeField] private PlayerProfile profile = new PlayerProfile();


//    // Awake function from Unity's MonoBehavior
//    void Awake()
//    {
//        // FirebaseApp.DefaultInstance.SetEditorDatabaseUrl("https://YOURPROJECTNAME.firebaseio.com/");

//        if (!FB.IsInitialized)
//        {
//            // Initialize the Facebook SDK
//            FB.Init(InitCallback, OnHideUnity);
//        }
//        else
//        {
//            // Already initialized, signal an app activation App Event
//            FB.ActivateApp();
//        }
//    }
//    private void Start()
//    {
//        DatabaseFunctions.InitDB();
//        DatabaseFunctions.loginComplete += LoggedIn;
//        Invoke(nameof(CheckIfUserLoggedIn), 2);
//    }
//    private void InitCallback()
//    {
//        if (FB.IsInitialized)
//        {
//            // Signal an app activation App Event
//            FB.ActivateApp();
//            // Continue with Facebook SDK
//            // ...
//        }
//        else
//        {
//            Debug.Log("Failed to Initialize the Facebook SDK");
//        }
//    }

//    private void OnHideUnity(bool isGameShown)
//    {
//        if (!isGameShown)
//        {
//            // Pause the game - we will need to hide
//            Time.timeScale = 0;
//        }
//        else
//        {
//            // Resume the game - we're getting focus again
//            Time.timeScale = 1;
//        }
//    }
//    public void FacbookLogin()
//    {
//        debugMessege = "initilize facebook login";
//        loadingBar.SetActive(true);
//        var perms = new List<string>() { "public_profile" };
//        FB.LogInWithReadPermissions(perms, AuthCallback);
//    }
//    private void AuthCallback(ILoginResult result)
//    {
//        if (FB.IsLoggedIn)
//        {
//            debugMessege = "facebook callbacks";
//            Debug.Log("Facebook Login");
//            // AccessToken class will have session details
//            var aToken = AccessToken.CurrentAccessToken;
//            // Print current access token's User ID
//            Debug.Log(aToken.UserId);
//            // Print current access token's granted permissions
//            GetUserDataFromProfile(aToken.UserId);
//        }
//        else
//        {
//            debugMessege = "user cancelled login";
//            loadingBar.SetActive(false);
//            Debug.Log("User cancelled login");
//        }
//    }
//    void GetUserDataFromProfile(string userID)
//    {
//        debugMessege = "Getting data from facebook profile";
//        //  FB.API(userID + "/picture?type=square&height=128&width=128", HttpMethod.GET, FbGetPicture);
//        FB.API("/me?fields=id,first_name,last_name", HttpMethod.GET, APICallback);
//    }

//    private void APICallback(IGraphResult result)
//    {
//        debugMessege = "Got data";
//        profile.id = result.ResultDictionary["id"].ToString();
//        profile.name = result.ResultDictionary["first_name"].ToString() + " " + result.ResultDictionary["last_name"].ToString();
//        profile.facebookLogin = true;
//        ProfileManager.instance.SetCurrentPlayer(profile);
//        debugMessege = "Adding/Retrieving data from database";
//        DatabaseFunctions.LoadCurrentPlayerDataFromDB(profile);
//    }
//    public void GuestCreated()
//    {
//        DatabaseFunctions.guestCreated = false;
//        if (!PlayerPrefs.HasKey("isLoggedIn"))
//            loadingBar.SetActive(false);

//    }
//    private void CheckIfUserLoggedIn()
//    {
//        if (!PlayerPrefs.HasKey("isLoggedIn"))
//        {
//            debugMessege = "No previous user found";
//            PlayerPrefs.SetInt("isLoggedIn", 0);
//            //      loadingBar.SetActive(false);
//        }
//        else
//        {
//            Debug.Log("HasKey");
//            if (PlayerPrefs.GetInt("isLoggedIn") > 0)
//            {
//                Debug.Log(PlayerPrefs.GetString("UserId"));
//                Debug.Log("USerAdded");
//                loadingBar.SetActive(true);
//                debugMessege = "User Found";
//                if (PlayerPrefs.HasKey("UserId"))
//                {
//                    debugMessege = "Loading user from database";
//                    DatabaseFunctions.LoadCurrentPlayerDataFromDB(PlayerPrefs.GetString("UserId"));
//                }
//            }
//            else
//            {
//                loadingBar.SetActive(false);
//            }
//        }
//    }

//    void LoggedIn()
//    {
//        if (!LoginScreenHandler.timedOut)
//        {
//            debugMessege = "Login Complete";
//            PlayerPrefs.SetInt("isLoggedIn", 1);
//            DatabaseFunctions.loginComplete -= LoggedIn;
//            debugMessege = "Loading new scene this user name is " + ProfileManager.instance.currentPlayer.id;
//            Invoke(nameof(LoadNextLevel), 1);
//        }
//    }
//    void LoadNextLevel()
//    {
//        Debug.Log(ProfileManager.instance.currentPlayer.First_Time_Login);
//        if (isLoggedIn)
//        {
//            if (ProfileManager.instance.currentPlayer.First_Time_Login)
//            {
//                SceneManager.LoadSceneAsync("Instructions");
//            }
//            else
//            {
//                SceneManager.LoadSceneAsync("Home");
//            }
//        }
//        else
//            Invoke(nameof(LoadNextLevel), 1);
//    }
//    public void ContinueAsGuest()
//    {
//        debugMessege = "initilize guest login";
//        loadingBar.SetActive(true);
//        DatabaseFunctions.ContinueAsGuest();
//    }
//    private void OnDisable()
//    {
//        DatabaseFunctions.loginComplete -= LoggedIn;
//    }

//    private void Update()
//    {
//        debugText.text = debugMessege + "";
//        if (DatabaseFunctions.guestCreated)
//        {
//            GuestCreated();
//        }
//    }
//}


using Facebook.Unity;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SocialLogin : MonoBehaviour
{
    

    public static bool isLoggedIn = false;
    public static string debugMessege = "";
    [SerializeField] private GameObject loadingBar = null;

    [SerializeField] private Text debugText;
    [SerializeField] private PlayerProfile profile = new PlayerProfile();


    // Awake function from Unity's MonoBehavior
    void Awake()
    {
        // FirebaseApp.DefaultInstance.SetEditorDatabaseUrl("https://YOURPROJECTNAME.firebaseio.com/");

        if (!FB.IsInitialized)
        {
            // Initialize the Facebook SDK
            FB.Init(InitCallback, OnHideUnity);
        }
        else
        {
            // Already initialized, signal an app activation App Event
            FB.ActivateApp();
        }
    }
    private void Start()
    {
        DatabaseFunctions.InitDB();
        DatabaseFunctions.loginComplete += LoggedIn;
        Invoke(nameof(CheckIfUserLoggedIn), 2);
    }
    private void InitCallback()
    {
        if (FB.IsInitialized)
        {
            // Signal an app activation App Event
            FB.ActivateApp();
            // Continue with Facebook SDK
            // ...
        }
        else
        {
            Debug.Log("Failed to Initialize the Facebook SDK");
        }
    }

    private void OnHideUnity(bool isGameShown)
    {
        if (!isGameShown)
        {
            // Pause the game - we will need to hide
            Time.timeScale = 0;
        }
        else
        {
            // Resume the game - we're getting focus again
            Time.timeScale = 1;
        }
    }
    public void FacbookLogin()
    {
        if (Manager.instance.m_CurrentLanguage == 0)
        {
            debugMessege = "initialize facebook login";
        }
        else if (Manager.instance.m_CurrentLanguage == 1)
        {
            debugMessege = "تهيئة تسجيل الدخول إلى الفيسبوك";
        }
        
        loadingBar.SetActive(true);
        var perms = new List<string>() { "public_profile", "user_friends" };
        FB.LogInWithReadPermissions(perms, AuthCallback);
    }
    string localPlayeruserID;
    private void AuthCallback(ILoginResult result)
    {
        if (FB.IsLoggedIn)
        {
            
            if (Manager.instance.m_CurrentLanguage == 0)
            {
                debugMessege = "facebook callbacks";
            }
            else if (Manager.instance.m_CurrentLanguage == 1)
            {
                debugMessege = "عمليات إعادة الاتصال على الفيسبوك";
            }
            Debug.Log("Facebook Login");
            // AccessToken class will have session details
            var aToken = AccessToken.CurrentAccessToken;
            // Print current access token's User ID
            Debug.Log(aToken.UserId);
            // Print current access token's granted permissions
            GetUserDataFromProfile(aToken.UserId);
            localPlayeruserID = aToken.UserId;
        }
        else
        {
            
            if (Manager.instance.m_CurrentLanguage == 0)
            {
                debugMessege = "user cancelled login";
            }
            else if (Manager.instance.m_CurrentLanguage == 1)
            {
                debugMessege = "ألغى المستخدم تسجيل الدخول";
            }
            loadingBar.SetActive(false);
            Debug.Log("User cancelled login");
        }
    }
    void GetUserDataFromProfile(string userID)
    {
        
        if (Manager.instance.m_CurrentLanguage == 0)
        {
            debugMessege = "Getting data from facebook profile";
        }
        else if (Manager.instance.m_CurrentLanguage == 1)
        {
            debugMessege = "الحصول على البيانات من ملف تعريف الفيسبوك";
        }
        //FB.API(userID + "/picture?type=square&height=128&width=128", HttpMethod.GET, FbGetPicture);

        FB.API("/me?fields=id,first_name,last_name", HttpMethod.GET, APICallback);
        FB.API("/me/friends?fields=id,name", HttpMethod.GET, Friends_Callback);

        //StartCoroutine(fetchProfilePic(userID)); //Call the coroutine to download the photo

    }

    private void FbGetPicture(IGraphResult result)
    {
        Debug.LogError("");
        Debug.LogError(result.Texture);
        if (String.IsNullOrEmpty(result.Error) && !result.Cancelled)
        { //if there isn't an error
            var data = result.ResultDictionary["data"] as Dictionary<string,object>; //create a new data dictionary
            string photoURL = data["url"] as String; //add a URL field to the dictionary
            Debug.LogError(data["url"]);
            //StartCoroutine(fetchProfilePic(photoURL)); //Call the coroutine to download the photo
        }
    }



    private IEnumerator fetchProfilePic(string userId)
    {
        yield return new WaitForSeconds(1f);
        if (string.IsNullOrEmpty(ProfileManager.instance.currentPlayer.ProfilePictureString_FB))
        {
            string url = "https" + "://graph.facebook.com/" + userId + "/picture?type=large";
            WWW result = new WWW(url); //use the photo url to get the photo from the web
            yield return result; //wait until it has downloaded
            Debug.LogError(result.texture); //set your profilePic Image Component's sprite to the photo
            //ProfileManager.instance.currentPlayer.profilePicture =
            //        Sprite.Create(result.texture,
            //        new Rect(0, 0, result.texture.width, result.texture.height),
            //        new Vector2(0.5f, 0.5f),
            //        100f);
            ProfileManager.instance.currentPlayer.FbPicture =
                    Sprite.Create(result.texture,
                    new Rect(0, 0, result.texture.width, result.texture.height),
                    new Vector2(0.5f, 0.5f),
                    100f);

            var myTextureBytes = result.texture.EncodeToPNG();
            var myTextureBytesEncodedAsBase64 = System.Convert.ToBase64String(myTextureBytes);
            Debug.LogError(myTextureBytesEncodedAsBase64);
            ProfileManager.instance.currentPlayer.ProfilePictureString_FB = myTextureBytesEncodedAsBase64;
            ProfileManager.instance.SaveUserData();
        }

        //recursive
        //else { yield return new WaitForSeconds(1); }
        //Debug.LogError("Setting picture now");
        //if(ProfileManager.instance.currentPlayer.profilePicture == null)
        //{
        //    StartCoroutine(fetchProfilePic(localPlayeruserID));
        //}

    }


    private void Friends_Callback(IGraphResult result)
    {
        if (result.Error != null)
        {
            return;
        }
        else if(result.Cancelled)
        {
            return;
        }

        object dataList;
        if (result.ResultDictionary.TryGetValue("data", out dataList))
        {
            Debug.LogError(dataList);
            var friendsList = (List<object>) dataList;
            Debug.LogError(friendsList);
            //CacheFriends(friendsList);
        }


        
    }

    private void CacheFriends(List<object> friendsList)
    {
        
        for (int i = 0; i < friendsList.Count; i++)
        {
            ProfileManager.instance.currentPlayer.FBfriends_list.Add(JsonUtility.ToJson(friendsList[i]));
            Debug.LogError(friendsList[i]);
        }
        ProfileManager.instance.SaveUserData();
    }

    private void APICallback(IGraphResult result)
    {
        
        if (Manager.instance.m_CurrentLanguage == 0)
        {
            debugMessege = "Got data";
        }
        else if (Manager.instance.m_CurrentLanguage == 1)
        {
            debugMessege = "حصلت على البيانات";
        }
        profile.id = result.ResultDictionary["id"].ToString();
        profile.name = result.ResultDictionary["first_name"].ToString() + " " + result.ResultDictionary["last_name"].ToString();
        profile.facebookLogin = true;
        ProfileManager.instance.SetCurrentPlayer(profile);
        
        if (Manager.instance.m_CurrentLanguage == 0)
        {
            debugMessege = "Adding/Retrieving data from database";
        }
        else if (Manager.instance.m_CurrentLanguage == 1)
        {
            debugMessege = "إضافة / استرجاع البيانات من قاعدة البيانات";
        }
        DatabaseFunctions.LoadCurrentPlayerDataFromDB(profile);
    }

    public void GuestCreated()
    {
        DatabaseFunctions.guestCreated = false;
        if (!PlayerPrefs.HasKey("isLoggedIn"))
        {
            loadingBar.SetActive(false);
        }
    }

    private void CheckIfUserLoggedIn()
    {
        if (!PlayerPrefs.HasKey("isLoggedIn"))
        {
            
            if (Manager.instance.m_CurrentLanguage == 0)
            {
                debugMessege = "No previous user found";
            }
            else if (Manager.instance.m_CurrentLanguage == 1)
            {
                debugMessege = "لم يتم العثور على مستخدم سابق";
            }
            PlayerPrefs.SetInt("isLoggedIn", 0);
            loadingBar.SetActive(false);
        }
        else
        {
            Debug.Log("HasKey");
            if (PlayerPrefs.GetInt("isLoggedIn") > 0)
            {
                Debug.Log(PlayerPrefs.GetString("UserId"));
                Debug.Log("USerAdded");
                loadingBar.SetActive(true);
                
                if (Manager.instance.m_CurrentLanguage == 0)
                {
                    debugMessege = "User Found";
                }
                else if (Manager.instance.m_CurrentLanguage == 1)
                {
                    debugMessege = "تم العثور على المستخدم";
                }
                if (PlayerPrefs.HasKey("UserId"))
                {
                    
                    if (Manager.instance.m_CurrentLanguage == 0)
                    {
                        debugMessege = "Loading user from database";
                    }
                    else if (Manager.instance.m_CurrentLanguage == 1)
                    {
                        debugMessege = "تحميل المستخدم من قاعدة البيانات";
                    }
                    DatabaseFunctions.LoadCurrentPlayerDataFromDB(PlayerPrefs.GetString("UserId"));
                }

                Invoke("SetFbPicture", 1f);
            }
            else
            {
                loadingBar.SetActive(false);
            }
        }
    }

    void SetFbPicture()
    {
        byte[] imageByte = System.Convert.FromBase64String(ProfileManager
            .instance.currentPlayer.ProfilePictureString_FB);
        Texture2D texture2D = new Texture2D(128, 128);
        texture2D.LoadImage(imageByte);
        ProfileManager.instance.currentPlayer.profilePicture = Sprite.Create(texture2D,
                    new Rect(0, 0, texture2D.width, texture2D.height),
                    new Vector2(0.5f, 0.5f),
                    100f);

        //if(!ProfileManager.instance.currentPlayer.profilePicture)
        //{
        //    SetFbPicture();
        //}
    }

    void LoggedIn()
    {
        if (!LoginScreenHandler.timedOut)
        {
            
            if (Manager.instance.m_CurrentLanguage == 0)
            {
                debugMessege = "Login Complete";
            }
            else if (Manager.instance.m_CurrentLanguage == 1)
            {
                debugMessege = "اكتمل تسجيل الدخول";
            }
            PlayerPrefs.SetInt("isLoggedIn", 1);
            DatabaseFunctions.loginComplete -= LoggedIn;
            
            if (Manager.instance.m_CurrentLanguage == 0)
            {
                debugMessege = "Loading new scene this user name is " + ProfileManager.instance.currentPlayer.id;
            }
            else if (Manager.instance.m_CurrentLanguage == 1)
            {
                debugMessege = "تحميل مشهد جديد اسم المستخدم هذا " + ProfileManager.instance.currentPlayer.id;
            }
            Invoke(nameof(LoadNextLevel), 1);
        }
    }
    void LoadNextLevel()
    {
        Debug.Log(ProfileManager.instance.currentPlayer.First_Time_Login);
        if (isLoggedIn)
        {
            if (ProfileManager.instance.currentPlayer.First_Time_Login)
            {
                SceneManager.LoadSceneAsync("Instructions");
            }
            else
            {
                SceneManager.LoadSceneAsync("Home");
            }
        }
        else
            Invoke(nameof(LoadNextLevel), 1);
    }
    public void ContinueAsGuest()
    {
        
        if (Manager.instance.m_CurrentLanguage == 0)
        {
            debugMessege = "initilize guest login";
        }
        else if (Manager.instance.m_CurrentLanguage == 1)
        {
            debugMessege = "تهيئة تسجيل دخول الضيف";
        }
        loadingBar.SetActive(true);
        DatabaseFunctions.ContinueAsGuest();
    }
    private void OnDisable()
    {
        DatabaseFunctions.loginComplete -= LoggedIn;
    }

    private void Update()
    {
        debugText.text = debugMessege + "";
        debugText.GetComponent<Kozykin.MultiLanguageItem>().strItems[0] = debugMessege + "";
        debugText.GetComponent<Kozykin.MultiLanguageItem>().strItems[1] = debugMessege + "";
        debugText.GetComponent<Kozykin.MultiLanguageItem>().text = debugMessege + "";
        if (DatabaseFunctions.guestCreated)
            GuestCreated();
    }
}
