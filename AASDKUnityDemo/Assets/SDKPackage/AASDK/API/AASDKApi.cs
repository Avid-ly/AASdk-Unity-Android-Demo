using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

namespace AASDK
{

	public class AASDKApi
	{
		public readonly static string iOS_SDK_Version = "2.0.0.4";
		public readonly static string Android_SDK_Version = "2.0.0.5";
		public readonly static string Unity_Package_Version = "2.0.0.7";

		// 是否已经初始化
		private static bool isInited; 
		private static AASDKCall polyCall = null;


		private static void instanceOfCall() {
			if (polyCall == null) {
				polyCall = new AASDKCall (); 
			}
		}


		public static void initSDK(string productId) {
			if (isInited) {
				return;
			}

			if (null == polyCall) {
				instanceOfCall ();
			}
			polyCall.initSdk(productId);
			isInited = true;
		}

		/**
		 * 判断SDK是否被初始化(Android与Ios均支持)
		 */
		public static bool isAASDKInited() {
			return isInited;
		}

		
		public static void accountLogin() {
			if (isInited) {
				polyCall.accountLogin ();
			} else {
				Debug.Log ("Fail to call accountLogin(), please initialize the  AASDK first!!!");
			}
		}

		public static void accountLogin(bool isVisible) {
			if (isInited) {
				polyCall.accountLogin (isVisible);
			} else {
				Debug.Log ("Fail to call accountLogin(), please initialize the  AASDK first!!!");
			}
		}

		public static void unAwareLogin()
		{
			if (isInited)
			{
				polyCall.unAwareLogin();
			}
			else
			{
				Debug.Log("Fail to call unAwareLogin(), please initialize the  AASDK first!!!");
			}
		}


		public static void showUserManagerUI() {
			if (isInited) {
				polyCall.showUserManagerUI ();
			} else {
				Debug.Log ("Fail to call showUserManagerUI(), please initialize the  AASDK first!!!");
			}
		}

		
		public static string getLoginedGGid() {
			if (null == polyCall) {
				instanceOfCall ();
			}
			return polyCall.getLoginedGGid ();
		}

		public static string getFacebookLoginedToken() {
			if (null == polyCall) {
				instanceOfCall ();
			}
			return polyCall.getFacebookLoginedToken ();
		}
	
		public static void getAAUGgidData (Action<string,string> success, Action<string> fail) {
			if (polyCall != null) {
				polyCall.getAAUGgidData ( success, fail);
			}
		}


		public static void getAAUTokenData (Action<string,string> success, Action<string> fail) {
			if (polyCall != null ) {
				polyCall.getAAUTokenData (success, fail);
			}
		}

        public static void setGPLoginVisible(bool isVisible)
        {
            Debug.Log("setGPLoginVisible in aasdk api"+isVisible);
            if (polyCall != null)
            {
                polyCall.setGPLoginVisible(isVisible);
            }
        }
		public static void enableDeleteAccount(bool isVisible, Action<string, string> success)
		{
			if (polyCall != null)
			{
				polyCall.enableDeleteAccount(isVisible, success);
			}
		}

		public static void notifySDKDeletedAccount(Action<string> success, Action<string> fail)
		{
			if (polyCall != null)
			{
				polyCall.notifySDKDeletedAccount(success, fail);
			}
		}
	}

}
