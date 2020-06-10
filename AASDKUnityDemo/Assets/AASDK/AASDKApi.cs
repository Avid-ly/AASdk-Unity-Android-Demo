using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

namespace AASDK
{

	public class AASDKApi
	{

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
	}

}
