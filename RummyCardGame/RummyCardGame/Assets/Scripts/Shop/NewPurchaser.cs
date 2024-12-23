//using System;
//using System.Collections.Generic;
//using UnityEngine;
//using UnityEngine.Purchasing;

//public class NewPurchaser : MonoBehaviour, IStoreListener
//{
//    public static NewPurchaser Instance { set; get; }

//    private static IStoreController m_StoreController;
//    private static IExtensionProvider m_StoreExtensionProvider;


//    //public static string Gold20K = "gold20";
//    //public static string Gold50K = "gold50";
//    //public static string Gold100K = "gold100";
//    public static string Masks10 = "masks10";
//    public static string Masks20 = "masks20";
//    public static string Masks30 = "masks30";
//    public static string Masks40 = "masks40";
//    public static string Masks50 = "masks50";

//    public static string NoAds = "noads";

//    private void Awake()
//    {
//        Instance = this;
//    }

//    void Start()
//    {
//        if (m_StoreController == null)
//        {

//            InitializePurchasing();
//        }
//    }

//    public void InitializePurchasing()
//    {

//        if (IsInitialized())
//        {

//            return;
//        }


//        var builder = ConfigurationBuilder.Instance(StandardPurchasingModule.Instance());


//        //builder.AddProduct(Gold20K, ProductType.Consumable);
//        //builder.AddProduct(Gold50K, ProductType.Consumable);
//        //builder.AddProduct(Gold100K, ProductType.Consumable);

//        builder.AddProduct(Masks10, ProductType.Consumable);
//        builder.AddProduct(Masks20, ProductType.Consumable);
//        builder.AddProduct(Masks30, ProductType.Consumable);
//        builder.AddProduct(Masks40, ProductType.Consumable);
//        builder.AddProduct(Masks50, ProductType.Consumable); 
       
//        //builder.AddProduct(PowerUp, ProductType.NonConsumable);
//        builder.AddProduct(NoAds, ProductType.NonConsumable);

//        UnityPurchasing.Initialize(this, builder);
//    }


//    private bool IsInitialized()
//    {

//        return m_StoreController != null && m_StoreExtensionProvider != null;
//    }

//    public void BuyNoads()
//    {
//        BuyProductID(NoAds);

//    }
//    public void BuyMasks10()
//    {
//        BuyProductID(Masks10);
//    }
//    public void BuyMasks20()
//    {
//        BuyProductID(Masks20);
//    }
//    public void BuyMasks30()
//    {
//       BuyProductID(Masks30);
//    }
//    public void BuyMasks40()
//    {
//       BuyProductID(Masks40);
//    }
//    public void BuyMasks50()
//    {
//        BuyProductID(Masks50);
//    }


//    void BuyProductID(string productId)
//    {
//        if (IsInitialized())
//        {

//            Product product = m_StoreController.products.WithID(productId);

//            if (product != null && product.availableToPurchase)
//            {
//                Debug.Log(string.Format("Purchasing product asychronously: '{0}'", product.definition.id));

//                m_StoreController.InitiatePurchase(product);
//            }
//            else
//            {
//                Debug.Log("BuyProductID: FAIL. Not purchasing product, either is not found or is not available for purchase");
//            }
//        }
//        else
//        {
//            Debug.Log("BuyProductID FAIL. Not initialized.");
//        }
//    }

//    public void RestorePurchases()
//    {
//        if (!IsInitialized())
//        {
//            Debug.Log("RestorePurchases FAIL. Not initialized.");
//            return;
//        }

//        if (Application.platform == RuntimePlatform.IPhonePlayer ||
//            Application.platform == RuntimePlatform.OSXPlayer)
//        {
//            Debug.Log("RestorePurchases started ...");

//            var apple = m_StoreExtensionProvider.GetExtension<IAppleExtensions>();
//            apple.RestoreTransactions((result) => {
//                Debug.Log("RestorePurchases continuing: " + result + ". If no further messages, no purchases available to restore.");
//            });
//        }
//        else
//        {
//            Debug.Log("RestorePurchases FAIL. Not supported on this platform. Current = " + Application.platform);
//        }
//    }




//    public void OnInitialized(IStoreController controller, IExtensionProvider extensions)
//    {
//        Debug.Log("OnInitialized: PASS");

//        m_StoreController = controller;
//        m_StoreExtensionProvider = extensions;
//    }


//    public void OnInitializeFailed(InitializationFailureReason error)
//    {
//        Debug.Log("OnInitializeFailed InitializationFailureReason:" + error);
//    }


//    public PurchaseProcessingResult ProcessPurchase(PurchaseEventArgs args)
//    {
//        //if (String.Equals(args.purchasedProduct.definition.id, Gold20K, StringComparison.Ordinal))
//        //{
//        //    Debug.Log(string.Format("ProcessPurchase: PASS. Product: '{0}'", args.purchasedProduct.definition.id));
//        //    Debug.Log("Bought Gold20K");
//        //    //MenuManager.coins += 20000;
//        //    PlayerPrefs.SetInt("CustomCoin", /*MenuManager.coins*/ + 5000);
//        //}

//        //else if (String.Equals(args.purchasedProduct.definition.id, Gold50K, StringComparison.Ordinal))
//        //{
//        //    Debug.Log(string.Format("ProcessPurchase: PASS. Product: '{0}'", args.purchasedProduct.definition.id));
//        //    Debug.Log("Bought Gold50K");
//        //    //MenuManager.coins += 50000;
//        //    PlayerPrefs.SetInt("CustomCoin", /*MenuManager.coins*/ + 10000);
//        //}
//        //else if (String.Equals(args.purchasedProduct.definition.id, Gold100K, StringComparison.Ordinal))
//        //{
//        //    Debug.Log(string.Format("ProcessPurchase: PASS. Product: '{0}'", args.purchasedProduct.definition.id));
//        //    Debug.Log("Bought Gold100K");
//        //    //MenuManager.coins += 100000;
//        //    //PlayerPrefs.SetInt("CustomCoin", MenuManager.coins);
//        //}
//        //else if (String.Equals(args.purchasedProduct.definition.id, PowerUp, StringComparison.Ordinal))
//        //{
//        //    Debug.Log(string.Format("ProcessPurchase: PASS. Product: '{0}'", args.purchasedProduct.definition.id));
//        //    Debug.Log("Bought PowerUp");
//        //    //MenuManager.witchLevel = 9;
//        //    //MenuManager.casperLevel = 9;
//        //    //PlayerPrefs.SetInt("WitchLevel", 9);
//        //    //PlayerPrefs.GetInt("CasperLevel", 9);
//        //}
//        //else if (String.Equals(args.purchasedProduct.definition.id, NoAds, StringComparison.Ordinal))
//        //{
//        //    Debug.Log(string.Format("ProcessPurchase: PASS. Product: '{0}'", args.purchasedProduct.definition.id));
//        //    Debug.Log("Bought NoAds");
//        //    //PlayerPrefs.SetInt("PurchaseNoAds", 1);
//        //}

//        if (String.Equals(args.purchasedProduct.definition.id, NoAds, StringComparison.Ordinal))
//        {
//            Debug.Log(string.Format("ProcessPurchase: PASS. Product: '{0}'", args.purchasedProduct.definition.id));
//            Debug.Log("RemoveAds Purchased");
//            //PlayerPrefs.SetInt("PurchaseChar2", 1);
//            //StoreManager.rubbies = StoreManager.rubbies + 20;
//            // PlayerPrefs.SetInt("Rubbies", StoreManager.rubbies);
//            PlayerPrefs.SetInt("RemoveAds", 1);
//            //UIMANager.Instance.disableRemoveAdBUtton();
//            PlayerPrefs.Save();
//        }
//        else if (String.Equals(args.purchasedProduct.definition.id, Masks10, StringComparison.Ordinal))
//        {
//            Debug.Log(string.Format("ProcessPurchase: PASS. Product: '{0}'", args.purchasedProduct.definition.id));
//            UIController.instance.AddMasks(10);


//        }
//        else if (String.Equals(args.purchasedProduct.definition.id, Masks20, StringComparison.Ordinal))
//        {
//            Debug.Log(string.Format("ProcessPurchase: PASS. Product: '{0}'", args.purchasedProduct.definition.id));
//            UIController.instance.AddMasks(20);

//        }
//        else if (String.Equals(args.purchasedProduct.definition.id, Masks30, StringComparison.Ordinal))
//        {
//            Debug.Log(string.Format("ProcessPurchase: PASS. Product: '{0}'", args.purchasedProduct.definition.id));
//            UIController.instance.AddMasks(30);

//        }
//        else if (String.Equals(args.purchasedProduct.definition.id, Masks40, StringComparison.Ordinal))
//        {
//            Debug.Log(string.Format("ProcessPurchase: PASS. Product: '{0}'", args.purchasedProduct.definition.id));
//            UIController.instance.AddMasks(40);

//        }
//        else if (String.Equals(args.purchasedProduct.definition.id, Masks50, StringComparison.Ordinal))
//        {
//            Debug.Log(string.Format("ProcessPurchase: PASS. Product: '{0}'", args.purchasedProduct.definition.id));
//            UIController.instance.AddMasks(50);

//        }
        
//        else
//        {
//            Debug.Log(string.Format("ProcessPurchase: FAIL. Unrecognized product: '{0}'", args.purchasedProduct.definition.id));
//        }

//        return PurchaseProcessingResult.Complete;
//    }


//    public void OnPurchaseFailed(Product product, PurchaseFailureReason failureReason)
//    {
//        Debug.Log(string.Format("OnPurchaseFailed: FAIL. Product: '{0}', PurchaseFailureReason: {1}", product.definition.storeSpecificId, failureReason));
//    }
//}