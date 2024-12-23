using Firebase.Database;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public static class DatabaseFunctions
{
    public static event Action<PlayerProfile> profileUpdated = null;
    public static event Action loginComplete = null;
    static DatabaseReference reference;
    public static bool guestCreated = false;
    static PlayerProfile guest = new PlayerProfile();
    static TaskScheduler taskScheduler;
    // public static DateTime serverTime;
    public static void InitDB()
    {
        taskScheduler = TaskScheduler.FromCurrentSynchronizationContext();
        RemoveCallBacks();

        reference = FirebaseDatabase.DefaultInstance.RootReference;
        FirebaseDatabase.DefaultInstance.GetReference("Players").ValueChanged += HandleValueChanged;
        profileUpdated += BuildingGuestProfile;
        loginComplete += RemoveCallBacks;
        BuildingGuestProfile();
    }

    static void RemoveCallBacks()
    {
        if (profileUpdated != null)
            profileUpdated -= BuildingGuestProfile;
        if (loginComplete != null)
            loginComplete -= RemoveCallBacks;
    }
    static void BuildingGuestProfile(PlayerProfile p = null)
    {
        long count = 0;
        Debug.Log("Building guest Profile");
        

        if (Manager.instance.m_CurrentLanguage == 0)
        {
            SocialLogin.debugMessege = "Building Guest Profile";
        }
        else if (Manager.instance.m_CurrentLanguage == 1)
        {
            SocialLogin.debugMessege = "بناء ملف تعريف الضيف";
        }
        
        FirebaseDatabase.DefaultInstance.GetReference("Players").GetValueAsync().ContinueWith(task =>
         {
             if (task.IsCompleted)
             {
                 count = task.Result.ChildrenCount + 1;
                 guest.id = "PLAYER_RCG00" + count;
                 guest.name = "PLAYER_RCG00" + count;
                 Debug.Log("Profile: " + guest.id);
                 

                 if (Manager.instance.m_CurrentLanguage == 0)
                 {
                     SocialLogin.debugMessege = "Guest profile created with user id " + guest.id;
                 }
                 else if (Manager.instance.m_CurrentLanguage == 1)
                 {
                     SocialLogin.debugMessege = "تم إنشاء ملف تعريف الضيف بمعرف المستخدم " + guest.id;
                 }

                 guestCreated = true;
             }
         }, taskScheduler);
    }
    private static void RegisterNewUserInDB(PlayerProfile player, Action successCallBack = null)
    {
        

        if (Manager.instance.m_CurrentLanguage == 0)
        {
            SocialLogin.debugMessege = "Converting user database into database storeable data";
        }
        else if (Manager.instance.m_CurrentLanguage == 1)
        {
            SocialLogin.debugMessege = "تحويل قاعدة بيانات المستخدم إلى بيانات قابلة للتخزين في قاعدة البيانات";
        }

        Debug.Log(player.id);
        if (player.id != "")
        {
            string json = JsonUtility.ToJson(player);
            Debug.Log("Register user");
            SocialLogin.debugMessege = "User Registered successfully";

            if (Manager.instance.m_CurrentLanguage == 0)
            {
                SocialLogin.debugMessege = "User Registered successfully";
            }
            else if (Manager.instance.m_CurrentLanguage == 1)
            {
                SocialLogin.debugMessege = "تم تسجيل المستخدم بنجاح";
            }

            reference.Child("Players").Child(player.id).SetRawJsonValueAsync(json);
            SocialLogin.isLoggedIn = true;
            successCallBack?.Invoke();
        }
        else
        {
            

            if (Manager.instance.m_CurrentLanguage == 0)
            {
                SocialLogin.debugMessege = "Guest user id is null can't process guest login";
            }
            else if (Manager.instance.m_CurrentLanguage == 1)
            {
                SocialLogin.debugMessege = "معرف المستخدم الضيف فارغ لا يمكن معالجة تسجيل دخول الضيف";
            }

            Debug.Log("ID null");
        }
    }
    public static void LoadCurrentPlayerDataFromDB(string userID)
    {
        Debug.Log(userID);
        bool profileFound = false;
        PlayerProfile pp = new PlayerProfile();
        FirebaseDatabase.DefaultInstance.GetReference("Players").GetValueAsync().ContinueWith(task =>
        {
            if (task.IsFaulted)
            {
                Debug.Log("can't load");
                // Handle the error...
            }
            else if (task.IsCompleted)
            {
                DataSnapshot snapshot = task.Result;
                pp = new PlayerProfile();
                pp.id = userID;
                foreach (var a in snapshot.Children)
                {
                    pp = JsonUtility.FromJson<PlayerProfile>(a.GetRawJsonValue());
                    if (pp.id == userID)
                    {
                        Debug.Log(a.GetRawJsonValue());

                        profileFound = true;
                        SocialLogin.isLoggedIn = true;
                        break;
                    }
                }
                //Ahmed
                if (!profileFound)
                {
                    pp = new PlayerProfile(); 
                    pp.id = userID;
                    Debug.Log(pp.name);
                    RegisterNewUserInDB(pp);
                }
                ProfileManager.instance.SetCurrentPlayer(pp);
                Debug.Log("Logged In");

            }
        }, taskScheduler);
        loginComplete?.Invoke();
    }
    public static void LoadCurrentPlayerDataFromDB(PlayerProfile user)
    {
        Debug.Log(user.id);
        bool profileFound = false;
        PlayerProfile pp = new PlayerProfile();
        FirebaseDatabase.DefaultInstance.GetReference("Players").GetValueAsync().ContinueWith(task =>
        {
            if (task.IsFaulted)
            {
                Debug.Log("can't load");
                // Handle the error...
            }
            else if (task.IsCompleted)
            {
                DataSnapshot snapshot = task.Result;
                foreach (var a in snapshot.Children)
                {
                    pp = JsonUtility.FromJson<PlayerProfile>(a.GetRawJsonValue());
                    if (pp.id == user.id)
                    {
                        profileFound = true;
                        SocialLogin.isLoggedIn = true;
                        break;
                    }
                }
                if (!profileFound)
                {
                    pp = user;
                    Debug.Log(pp.name);
                    RegisterNewUserInDB(pp);
                }
                ProfileManager.instance.SetCurrentPlayer(pp);
                Debug.Log("Logged In");

            }
        }, taskScheduler);
        loginComplete?.Invoke();

    }
    public static void SaveDataInDB(PlayerProfile player)
    {
        if (player.id != "")
        {
            string js = JsonUtility.ToJson(player);
            Debug.Log(js);
            reference.Child("Players").Child(player.id).SetRawJsonValueAsync(js);
        }
    }
    public static void ContinueAsGuest()
    {
        

        if (Manager.instance.m_CurrentLanguage == 0)
        {
            SocialLogin.debugMessege = "Registering guest user with id " + guest.id;
        }
        else if (Manager.instance.m_CurrentLanguage == 1)
        {
            SocialLogin.debugMessege = "تسجيل مستخدم ضيف بالمعرف " + guest.id;
        }

        if (guest.id != "")
        {
            RegisterNewUserInDB(guest, GuestRegisterSuccessCallBack);
        }
        else
        {
            

            if (Manager.instance.m_CurrentLanguage == 0)
            {
                SocialLogin.debugMessege = "Waiting for guest id generation";
            }
            else if (Manager.instance.m_CurrentLanguage == 1)
            {
                SocialLogin.debugMessege = "في انتظار إنشاء هوية الضيف";
            }

            ContinueAsGuest();
        }
    }
    static void GuestRegisterSuccessCallBack()
    {
        loginComplete?.Invoke();
        ProfileManager.instance.SetCurrentPlayer(guest);
    }
    public static void LoadOtherUsers(string userID, System.Action<PlayerProfile> profileFoundCallBack)
    {
        bool profileFound = false;
        PlayerProfile pp = new PlayerProfile();

        FirebaseDatabase.DefaultInstance.GetReference("Players").GetValueAsync().ContinueWith(task =>
        {
            if (task.IsFaulted)
            {
                Debug.Log("can't load");
                // Handle the error...
            }
            else if (task.IsCompleted)
            {
                DataSnapshot snapshot = task.Result;
                //pp = new PlayerProfile();
                //pp.id = userID;
                foreach (var a in snapshot.Children)
                {
                    pp = JsonUtility.FromJson<PlayerProfile>(a.GetRawJsonValue());
                    if (pp.id == userID)
                    {
                        profileFound = true;
                        profileFoundCallBack(pp);
                        break;
                    }
                }
                if (!profileFound)
                {
                    Debug.Log(pp.name);
                    pp.name = userID;
                    pp.id = userID;
                    Debug.LogError(pp.id);
                    Debug.LogError(pp.name);
                    profileFoundCallBack(pp);
                    RegisterNewUserInDB(pp);
                }
            }
        }, taskScheduler);
    }
    static void HandleValueChanged(object sender, ValueChangedEventArgs args)
    {
        if (args.DatabaseError != null)
        {
            Debug.LogError(args.DatabaseError.Message);
            return;
        }
        if (ProfileManager.instance.currentPlayer.id != "")
        {
            FirebaseDatabase.DefaultInstance.GetReference("Players").Child(ProfileManager.instance.currentPlayer.id).GetValueAsync().ContinueWith(task =>
           {
               if (task.IsFaulted)
               {
                   Debug.Log("can't load");
                   // Handle the error...
               }
               else if (task.IsCompleted)
               {
                   DataSnapshot snapshot = task.Result;
                   ProfileManager.instance.currentPlayer = JsonUtility.FromJson<PlayerProfile>(snapshot.GetRawJsonValue());
                   Debug.Log("Loaded");
                   Debug.Log("Updated");
                   profileUpdated?.Invoke(null);
               }
           }, taskScheduler);
        }
    }
}
