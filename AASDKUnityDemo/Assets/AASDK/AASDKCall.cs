
using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace AASDK
{
    public class AASDKCall {



#if UNITY_IOS && !UNITY_EDITOR

			[DllImport("__Internal")]
			private static extern void initAnalysisSDKForIos(string gameName, string funName, string productid, string channelid, int zone);


			[DllImport("__Internal")]
			private static extern void disableAccessPrivacyInformationForIos();

			[DllImport("__Internal")]
			private static extern string getUserIdForIos();

			[DllImport("__Internal")]
			private static extern string getOpenIdForIos();


			[DllImport("__Internal")]
			private static extern void logMapForIos(string key, string json);

			[DllImport("__Internal")]
			private static extern void logStringForIos(string key, string value);

			[DllImport("__Internal")]
			private static extern void logKeyForIos(string key);

			[DllImport("__Internal")]
			private static extern void countKeyForIos(string key);

			[DllImport("__Internal")]
			private static extern void logZFServerIapAndExtra(string playerId, string gameAccountServer, string receiptDataString, bool isbase64, string json);

			[DllImport("__Internal")]
			private static extern void logZFPlayerIapAndExtra(string playerId, string receiptDataString, bool isbase64, string json);


			[DllImport("__Internal")]
			private static extern void thirdpartyLogZFWithPlayerId(string playerId, string thirdparty, string receiptDataString);  

			[DllImport("__Internal")]
			private static extern void thirdpartyLogZFWithServerPlayerId(string playerId, string gameAccountServer, string thirdparty, string receiptDataString); 

			[DllImport("__Internal")]
			private static extern void commonIosLogin(string type, string playerId, string token);

			[DllImport("__Internal")]
			private static extern void guestLoginWithGameId(string playerId);

			[DllImport("__Internal")]
			private static extern void facebookLoginWithGameId(string playerId, string openId, string openToken);	

			[DllImport("__Internal")]
			private static extern void twitterLoginWithPlayerId(string playerId, string twitterId, string twitterUserName, string twitterAuthToken);

			[DllImport("__Internal")]
			private static extern void portalLoginWithPlayerId(string playerId, string portalId);

			[DllImport("__Internal")]
			private static extern void initDurationReportForIos(string serverName, string serverZone, string uid, string ggid);

			[DllImport("__Internal")]
			private static extern void becomeActiveForIos();

			[DllImport("__Internal")]
			private static extern void resignActiveForIos();

			[DllImport("__Internal")]
			private static extern void getConversionDataForIos(string gameName, string funName, string afConversionData);

			

#elif UNITY_ANDROID && !UNITY_EDITOR
			private static AndroidJavaClass jc = null;
			private readonly static string JavaClassName = "com.aas.sdk.account.unity.AASDKProxy";
			private readonly static string JavaClassStaticMethod_initSdk = "initSdk";
			private readonly static string JavaClassStaticMethod_accountLogin = "accountLogin";
			private readonly static string JavaClassStaticMethod_showUserManagerUI = "showUserManagerUI";
			private readonly static string JavaClassStaticMethod_getLoginedGGid = "getLoginedGGid";
			private readonly static string JavaClassStaticMethod_getFacebookLoginedToken = "getFacebookLoginedToken";

			private readonly static string JavaClassStaticMethod_AAUGgidLogin = "AAUGgidLogin";
			private readonly static string JavaClassStaticMethod_AAUTokenLogin = "AAUTokenLogin";


#else
        // "do nothing";
#endif



        public AASDKCall() {
            AASDKObject.getInstance();
#if UNITY_IOS && !UNITY_EDITOR
				Debug.Log ("===> AASDKCall instanced.");
#elif UNITY_ANDROID && !UNITY_EDITOR
			if (jc == null) {
				Debug.Log ("===> AASDKCall instanced.");
				jc = new AndroidJavaClass (JavaClassName);
			}
#endif
        }



        public void initSdk(string productId) {

            if (null == productId || productId.Length == 0) {
                Debug.Log("===> UPTraceCall.initTtraceSDK(), error: the value of parameter productId is null or empty.");
                return;
            }


#if UNITY_IOS && !UNITY_EDITOR
			initSdk(AASDKObject.Unity_Callback_Class_Name,
				AASDKObject.Unity_Callback_Function_Name,
				productId);
		
#elif UNITY_ANDROID && !UNITY_EDITOR
			if (jc == null) {
				//Debug.Log (JavaClassName);
				jc = new AndroidJavaClass (JavaClassName);
			}
			jc.CallStatic (JavaClassStaticMethod_initSdk, 
				AASDKObject.Unity_Callback_Class_Name,
				AASDKObject.Unity_Callback_Function_Name,
				productId);
#endif
        }


        public string getLoginedGGid() {

#if UNITY_IOS && !UNITY_EDITOR
			Debug.Log ("===> getCustomerId() is not supported by IOS." );
			return "";
#elif UNITY_ANDROID && !UNITY_EDITOR
			if (jc != null) 
			{
				return jc.CallStatic<string> (JavaClassStaticMethod_getLoginedGGid);
			}
			return "";
#else
            return "";
#endif
        }

        public string getFacebookLoginedToken() {

#if UNITY_IOS && !UNITY_EDITOR
			Debug.Log ("===> getCustomerId() is not supported by IOS." );
			return "";
#elif UNITY_ANDROID && !UNITY_EDITOR
			if (jc != null) 
			{
				return jc.CallStatic<string> (JavaClassStaticMethod_getFacebookLoginedToken);
			}
			return "";
#else
            return "";
#endif
        }



        public void accountLogin() {

#if UNITY_IOS && !UNITY_EDITOR
			 getUserIdForIos();
#elif UNITY_ANDROID && !UNITY_EDITOR
			if (jc != null) 
			{
				jc.CallStatic(JavaClassStaticMethod_accountLogin);
			}
#endif

        }

        public void showUserManagerUI() {

#if UNITY_IOS && !UNITY_EDITOR
			return getUserIdForIos();
#elif UNITY_ANDROID && !UNITY_EDITOR
			if (jc != null) 
			{
				jc.CallStatic(JavaClassStaticMethod_showUserManagerUI);
			}
#endif

        }


        public void getAAUGgidData(Action<string, string> success, Action<string> fail) {
            // 设置callback回调
            AASDKObject.getInstance().setGgidLoginCallback(success, fail);
            // 调用原生的方法
#if UNITY_IOS && !UNITY_EDITOR
				getConversionDataForIos(UPTraceObject.Unity_Callback_Class_Name,UPTraceObject.Unity_Callback_Function_Name,afConversionData);
#elif UNITY_ANDROID && !UNITY_EDITOR
			if (jc != null) {
				jc.CallStatic (JavaClassStaticMethod_AAUGgidLogin);
			}
#endif
            }


            public void getAAUTokenData(Action<string, string> success, Action<string> fail)
            {
                // 设置callback回调
                AASDKObject.getInstance().setTokenLoginCallback(success, fail);
                // 调用原生的方法
#if UNITY_IOS && !UNITY_EDITOR
				getConversionDataForIos(UPTraceObject.Unity_Callback_Class_Name,UPTraceObject.Unity_Callback_Function_Name,afConversionData);
#elif UNITY_ANDROID && !UNITY_EDITOR
			if (jc != null) {
				jc.CallStatic (JavaClassStaticMethod_AAUTokenLogin);
			}
#endif
            }

        }
    
}
