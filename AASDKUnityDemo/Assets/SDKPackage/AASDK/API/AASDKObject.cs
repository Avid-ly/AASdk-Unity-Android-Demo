using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AASDKMiniJSON;
using System;

namespace AASDK {
	public class AASDKObject : MonoBehaviour
	{
		private static AASDKObject instance = null;
		public static readonly string Unity_Callback_Class_Name = "AASDK_Callback_Object";
		public static readonly string Unity_Callback_Function_Name = "onNativeCallback";

		public static readonly string Unity_Callback_Message_Key_Function = "callbackMessageKeyFunctionName";
		public static readonly string Unity_Callback_Message_Key_Parameter = "callbackMessageKeyParameter";

		// android
        private readonly static string Unity_Callback_Message_Function_AASDK_Ggid_Login_Success = "AASDK_Ggid_Login_Success";
        private readonly static string Unity_Callback_Message_Function_AASDK_Ggid_Login_Fail    = "AASDK_Ggid_Login_Fail";
        private readonly static string Unity_Callback_Message_Function_AASDK_Token_Login_Success = "AASDK_Token_Login_Success";
        private readonly static string Unity_Callback_Message_Function_AASDK_Token_Login_Fail    = "AASDK_Token_Login_Fail";

		public static readonly string Unity_Callback_Message_Key_Mode = "callbackMessageKeyMode";
        public static readonly string Unity_Callback_Parameter = "paramter";
        public static readonly string Unity_Callback_Mode = "mode";
        
        // ios
        private readonly static string Unity_Callback_Message_Function_Login_Succeed_Complete   = "Init_Succeed_Complete";
        private readonly static string Unity_Callback_Message_Function_Login_Error_Complete     = "Init_Error_Complete";

		// 注销账号完成
		private readonly static string Unity_Callback_Message_Function_Delete_Account_Complete = "Delete_Complete";
		// 主动通知SDK注销账号，成功
		private readonly static string Unity_Callback_Message_Function_Notify_Deleted_Succeed_Complete = "Notify_Deleted_Succeed_Complete";
		// 主动通知SDK注销账号，失败
		private readonly static string Unity_Callback_Message_Function_Notify_Deleted_Error_Complete = "Notify_Deleted_Error_Complete";

		private readonly static string Unity_Callback_Message_Parameter_GameGuestId     = "gameGuestId";
        private readonly static string Unity_Callback_Message_Parameter_SignedRequest   = "signedRequest";
        private readonly static string Unity_Callback_Message_Parameter_LoginMode       = "loginMode";

		public static AASDKObject getInstance()
		{
			if (instance == null) {
				GameObject polyCallback = new GameObject (Unity_Callback_Class_Name);
				polyCallback.hideFlags = HideFlags.HideAndDontSave;
				DontDestroyOnLoad (polyCallback);

				instance = polyCallback.AddComponent<AASDKObject> ();
			}
			return instance;
		}

		Action<string,string> ggidLoginSucceedCallback;
		Action<string> ggidLoginFailCallback;
		Action<string,string> tokenLoginSucceedCallback;
		Action<string> tokenLoginFailCallback;

		Action<string, string> delegateAccountSucceedCallback;
		Action<string> notifyDelegateAccountSucceedCallback;
		Action<string> notifyDelegateAccountFailCallback;

		// Use this for initialization
		void Start ()
		{

		}

		// Update is called once per frame
		void Update ()
		{

		}

		public void setGgidLoginCallback(Action<string,string> success, Action<string> fail) {
			ggidLoginSucceedCallback = success;
			ggidLoginFailCallback = fail;
		}
		
		public void setTokenLoginCallback(Action<string,string> success, Action<string> fail) {
			tokenLoginSucceedCallback = success;
			tokenLoginFailCallback = fail;
		}

		public void setDelegateAccountSucceedCallback(Action<string, string> success){
			delegateAccountSucceedCallback = success;
		}

		public void setNotifyDelegateAccountCallback(Action<string> success, Action<string> fail)
		{
			notifyDelegateAccountSucceedCallback = success;
			notifyDelegateAccountFailCallback = fail;
		}

		// 将jsonobj转换了map<>
		public string getInnerJsonParamterValue(Hashtable jsonObj,string key) {
            string msg = "";
            if (jsonObj.ContainsKey(Unity_Callback_Message_Key_Parameter))
            {
                Hashtable innerJsonObj = (Hashtable)jsonObj[Unity_Callback_Message_Key_Parameter];
                if (innerJsonObj.ContainsKey(key)) { 
                    msg = (string)innerJsonObj[key];
                }
            }
            return msg;
        }

        public void onNativeCallback(string message) {

        	Debug.Log ("===> message : " + message);
			Hashtable jsonObj = (Hashtable)AASDKMiniJSON.MiniJSON.jsonDecode (message);

			if (jsonObj.ContainsKey (Unity_Callback_Message_Key_Function)) {

				string function = (string)jsonObj[Unity_Callback_Message_Key_Function];
				string msg = "";
                string mode = "";

                string strFu = System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                Debug.Log ("===> onTargetCallback function: " + function + ", msg:" + msg + ", time at:" + strFu);

				//callback
				if (function.Equals (Unity_Callback_Message_Function_AASDK_Ggid_Login_Success)) {
                    msg = getInnerJsonParamterValue(jsonObj,Unity_Callback_Parameter);
                    mode = getInnerJsonParamterValue(jsonObj, Unity_Callback_Mode);
                    if (ggidLoginSucceedCallback != null) {
						ggidLoginSucceedCallback (msg, mode);
					}
					else {
						Debug.Log ("===> can't run ggidLoginSucceedCallback(), no delegate object.");
					}
				} 
				else if (function.Equals (Unity_Callback_Message_Function_AASDK_Ggid_Login_Fail)) {
                    msg = getInnerJsonParamterValue(jsonObj, Unity_Callback_Parameter);
                    mode = getInnerJsonParamterValue(jsonObj, Unity_Callback_Mode);
                    if (ggidLoginFailCallback != null) {
						ggidLoginFailCallback (msg);
					}
					else {
						Debug.Log ("===> can't run ggidLoginFailCallback(), no delegate object.");
					}
				}
				else if (function.Equals (Unity_Callback_Message_Function_AASDK_Token_Login_Success)) {
                    msg = getInnerJsonParamterValue(jsonObj, Unity_Callback_Parameter);
                    mode = getInnerJsonParamterValue(jsonObj, Unity_Callback_Mode);

					if (tokenLoginSucceedCallback != null) {
						tokenLoginSucceedCallback (msg,mode);
					}
					else {
						Debug.Log ("===> can't run tokenLoginSucceedCallback(), no delegate object.");
					}
				}
				else if (function.Equals (Unity_Callback_Message_Function_AASDK_Token_Login_Fail)) {
                    msg = getInnerJsonParamterValue(jsonObj, Unity_Callback_Parameter);
                    mode = getInnerJsonParamterValue(jsonObj, Unity_Callback_Mode);
					if (tokenLoginFailCallback != null) {
						tokenLoginFailCallback (msg);
					}
					else {
						Debug.Log ("===> can't run tokenLoginFailCallback(), no delegate object.");
					}
				}
				else if (function.Equals (Unity_Callback_Message_Function_Login_Succeed_Complete)) {

					Debug.Log ("===> call Unity_Callback_Message_Function_Login_Succeed_Complete");

					if (jsonObj.ContainsKey (Unity_Callback_Message_Key_Parameter)) {
						Hashtable parameterObj = (Hashtable) jsonObj[Unity_Callback_Message_Key_Parameter];
						string gameGuestId = "";
						string signedRequest = "";
						string loginMode = "";
						if (parameterObj.ContainsKey(Unity_Callback_Message_Parameter_GameGuestId)) {
							gameGuestId = (string)parameterObj[Unity_Callback_Message_Parameter_GameGuestId];
						}
						if (parameterObj.ContainsKey(Unity_Callback_Message_Parameter_SignedRequest)) {
							signedRequest = (string)parameterObj[Unity_Callback_Message_Parameter_SignedRequest];
						}
						if (parameterObj.ContainsKey(Unity_Callback_Message_Parameter_LoginMode)) {
							loginMode = (string)parameterObj[Unity_Callback_Message_Parameter_LoginMode];
						}

						if (ggidLoginSucceedCallback != null) {
							ggidLoginSucceedCallback (gameGuestId, loginMode);
						}
						if (tokenLoginSucceedCallback != null) {
							tokenLoginSucceedCallback (signedRequest,loginMode);
						}
						else {
							Debug.Log ("===> can't run Init_Succeed_Complete");
						}
					}
				}
				else if (function.Equals (Unity_Callback_Message_Function_Login_Error_Complete)) {

					Debug.Log ("===> call Unity_Callback_Message_Function_Login_Error_Complete");

					string errorMsg = "";
					if (jsonObj.ContainsKey (Unity_Callback_Message_Key_Parameter)) {
						errorMsg = (string)jsonObj[Unity_Callback_Message_Key_Parameter];
					}
					// 回调方法二：通过传入进来的callback回调
					if (ggidLoginFailCallback != null) {
						ggidLoginFailCallback (errorMsg);
						ggidLoginFailCallback = null;
					}
					else if (tokenLoginFailCallback != null) {
						tokenLoginFailCallback (errorMsg);
						tokenLoginFailCallback = null;
					}
				}


				else if (function.Equals(Unity_Callback_Message_Function_Delete_Account_Complete))
				{

					Debug.Log("===> call Unity_Callback_Message_Function_Delete_Account_Complete");

					if (jsonObj.ContainsKey(Unity_Callback_Message_Key_Parameter))
					{
						Hashtable parameterObj = (Hashtable)jsonObj[Unity_Callback_Message_Key_Parameter];
						string gameGuestId = "";
						string signedRequest = "";
						string loginMode = "";
						if (parameterObj.ContainsKey(Unity_Callback_Message_Parameter_GameGuestId))
						{
							gameGuestId = (string)parameterObj[Unity_Callback_Message_Parameter_GameGuestId];
						}
						if (parameterObj.ContainsKey(Unity_Callback_Message_Parameter_SignedRequest))
						{
							signedRequest = (string)parameterObj[Unity_Callback_Message_Parameter_SignedRequest];
						}
						if (parameterObj.ContainsKey(Unity_Callback_Message_Parameter_LoginMode))
						{
							loginMode = (string)parameterObj[Unity_Callback_Message_Parameter_LoginMode];
						}

						if (delegateAccountSucceedCallback != null)
						{
							delegateAccountSucceedCallback(gameGuestId, loginMode);
						}
						else
						{
							Debug.Log("===> can't run Delete_Complete");
						}
					}
				}

				else if (function.Equals(Unity_Callback_Message_Function_Notify_Deleted_Succeed_Complete))
				{

					Debug.Log("===> call Unity_Callback_Message_Function_Notify_Deleted_Succeed_Complete");

					if (jsonObj.ContainsKey(Unity_Callback_Message_Key_Parameter))
					{
						if (notifyDelegateAccountSucceedCallback != null)
						{
							notifyDelegateAccountSucceedCallback("succeed");
						}
						else
						{
							Debug.Log("===> can't run Notify_Deleted_Succeed_Complete");
						}
					}
				}
				else if (function.Equals(Unity_Callback_Message_Function_Notify_Deleted_Error_Complete))
				{

					Debug.Log("===> call Unity_Callback_Message_Function_Notify_Deleted_Error_Complete");

					string errorMsg = "";
					if (jsonObj.ContainsKey(Unity_Callback_Message_Key_Parameter))
					{
						errorMsg = (string)jsonObj[Unity_Callback_Message_Key_Parameter];
					}
					// 回调方法二：通过传入进来的callback回调
					if (notifyDelegateAccountFailCallback != null)
					{
						notifyDelegateAccountFailCallback(errorMsg);
						notifyDelegateAccountFailCallback = null;
					}
					else
                    {
						Debug.Log("===> can't run Notify_Deleted_Error_Complete");
					}
				}

				//unkown call
				else {
					Debug.Log ("===> onTargetCallback unkown function:" + function);
				}
			}
        }
	}
}


