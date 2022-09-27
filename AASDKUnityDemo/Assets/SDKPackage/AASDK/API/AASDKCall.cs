
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
			private static extern void setIosCallbackWithClassAndFunction(string callbackClassName, string callbackFunctionName);

			[DllImport("__Internal")]
			private static extern void initIosSDKForAASDK(string productId);

			[DllImport("__Internal")]
			private static extern void setLoginCallback();

			[DllImport("__Internal")]
			private static extern void login();

			[DllImport("__Internal")]
			private static extern void loginWithVisible(bool visible);

			[DllImport("__Internal")]
			private static extern void loginWithUnAware();

			[DllImport("__Internal")]
			private static extern void showUserCenter();

			[DllImport("__Internal")]
			private static extern string getIosFacebookLoginedToken();

			[DllImport("__Internal")]
			private static extern string getGGID();

			[DllImport("__Internal")]
			private static extern void aasdkEnableDeleteAccount(bool visible);

			[DllImport("__Internal")]
			private static extern void aasdkNotifySDKDeletedAccount();

			
#elif UNITY_ANDROID && !UNITY_EDITOR
			private static AndroidJavaClass jc = null;
			private readonly static string JavaClassName = "com.aas.sdk.account.unity.AASDKProxy";
			private readonly static string JavaClassStaticMethod_initSdk = "initSdk";
			private readonly static string JavaClassStaticMethod_accountLogin = "accountLogin";
			private readonly static string JavaClassStaticMethod_showUserManagerUI = "showUserManagerUI";
			private readonly static string JavaClassStaticMethod_getLoginedGGid = "getLoginedGGid";
			private readonly static string JavaClassStaticMethod_getFacebookLoginedToken = "getFacebookLoginedToken";
            private readonly static string JavaClassStaticMethod_setGPLoginVisible = "setGPLoginVisible";

			private readonly static string JavaClassStaticMethod_AAUGgidLogin = "AAUGgidLogin";
			private readonly static string JavaClassStaticMethod_AAUTokenLogin = "AAUTokenLogin";
			private readonly static string JavaClassStaticMethod_unAwareLogin = "unAwareLogin";


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
                Debug.Log("===> AASDKCall.initSdk(), error: the value of parameter productId is null or empty.");
                return;
            }


#if UNITY_IOS && !UNITY_EDITOR
            setIosCallbackWithClassAndFunction(AASDKObject.Unity_Callback_Class_Name, AASDKObject.Unity_Callback_Function_Name);
			initIosSDKForAASDK(productId);
		
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
			return getGGID();

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
			return getIosFacebookLoginedToken();

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
			login();

#elif UNITY_ANDROID && !UNITY_EDITOR
			if (jc != null) 
			{
				jc.CallStatic(JavaClassStaticMethod_accountLogin);
			}
#endif

        }


        
        public void accountLogin(bool isVisible) {

#if UNITY_IOS && !UNITY_EDITOR
			loginWithVisible(isVisible);

#elif UNITY_ANDROID && !UNITY_EDITOR
			if (jc != null) 
			{
				jc.CallStatic(JavaClassStaticMethod_accountLogin,isVisible);
			}
#endif

        }

		public void unAwareLogin()
		{

#if UNITY_IOS && !UNITY_EDITOR
			loginWithUnAware();

#elif UNITY_ANDROID && !UNITY_EDITOR
			if (jc != null) 
			{
				jc.CallStatic(JavaClassStaticMethod_unAwareLogin);
			}
#endif

		}

		public void showUserManagerUI() {

#if UNITY_IOS && !UNITY_EDITOR
			showUserCenter();

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
            setLoginCallback();

#elif UNITY_ANDROID && !UNITY_EDITOR
			if (jc != null) {
				jc.CallStatic (JavaClassStaticMethod_AAUGgidLogin);
			}
#endif
        }


        public void getAAUTokenData(Action<string, string> success, Action<string> fail) {
            // 设置callback回调
            AASDKObject.getInstance().setTokenLoginCallback(success, fail);
            // 调用原生的方法
#if UNITY_IOS && !UNITY_EDITOR
			setLoginCallback();

#elif UNITY_ANDROID && !UNITY_EDITOR
			if (jc != null) {
				jc.CallStatic (JavaClassStaticMethod_AAUTokenLogin);
			}
#endif
        }

        public void setGPLoginVisible(bool isVisible)
        {
            Debug.Log("===> setGPLoginVisible in aasdkcall."+isVisible);
#if UNITY_IOS && !UNITY_EDITOR
			

#elif UNITY_ANDROID && !UNITY_EDITOR
			if (jc != null) 
			{
				jc.CallStatic(JavaClassStaticMethod_setGPLoginVisible,isVisible);
			}
#endif
        }

		public void enableDeleteAccount(bool isVisible, Action<string, string> success)
		{

#if UNITY_IOS && !UNITY_EDITOR
			AASDKObject.getInstance().setDelegateAccountSucceedCallback(success);
			aasdkEnableDeleteAccount(isVisible);

#elif UNITY_ANDROID && !UNITY_EDITOR
#endif

		}

		public void notifySDKDeletedAccount(Action<string> success, Action<string> fail)
		{

#if UNITY_IOS && !UNITY_EDITOR
			// 设置callback回调
            AASDKObject.getInstance().setNotifyDelegateAccountCallback(success, fail);
			aasdkNotifySDKDeletedAccount();

#elif UNITY_ANDROID && !UNITY_EDITOR
#endif

		}
	}
}
