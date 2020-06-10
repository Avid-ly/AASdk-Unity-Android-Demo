using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TraceXXJSON;
using System;

namespace AASDK {
	public class AASDKObject : MonoBehaviour
	{
		private static AASDKObject instance = null;
		public static readonly string Unity_Callback_Class_Name = "AASDK_Callback_Object";
		public static readonly string Unity_Callback_Function_Name = "onNativeCallback";

		public static readonly string Unity_Callback_Message_Key_Function = "callbackMessageKeyFunctionName";
		public static readonly string Unity_Callback_Message_Key_Parameter = "callbackMessageKeyParameter";
        public static readonly string Unity_Callback_Parameter = "paramter";
        public static readonly string Unity_Callback_Mode = "mode";
        private readonly static string Unity_Callback_Message_Function_AASDK_Ggid_Login_Success = "AASDK_Ggid_Login_Success";
        private readonly static string Unity_Callback_Message_Function_AASDK_Ggid_Login_Fail    = "AASDK_Ggid_Login_Fail";
        private readonly static string Unity_Callback_Message_Function_AASDK_Token_Login_Success = "AASDK_Token_Login_Success";
        private readonly static string Unity_Callback_Message_Function_AASDK_Token_Login_Fail    = "AASDK_Token_Login_Fail";

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

  
        public void onNativeCallback(string message) {

        	Debug.Log (message);
			Hashtable jsonObj = (Hashtable)TraceXXJSON.MiniJSON.jsonDecode (message);

			if (jsonObj.ContainsKey (Unity_Callback_Message_Key_Function)) {

				string function = (string)jsonObj[Unity_Callback_Message_Key_Function];
				string msg = "";
                string mode = "";

                if (jsonObj.ContainsKey(Unity_Callback_Message_Key_Parameter))
                {
                    Hashtable  innerJsonObj= (Hashtable)jsonObj[Unity_Callback_Message_Key_Parameter];
                    if (innerJsonObj.ContainsKey(Unity_Callback_Parameter))
                    {
                        msg = (string)innerJsonObj[Unity_Callback_Parameter];
                    }
                    if (innerJsonObj.ContainsKey(Unity_Callback_Mode))
                    {
                        mode = (string)innerJsonObj[Unity_Callback_Mode];
                    }
                }

                string strFu = System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                Debug.Log ("===> onTargetCallback function: " + function + ", msg:" + msg + ", time at:" + strFu);

				//callback
				if (function.Equals (Unity_Callback_Message_Function_AASDK_Ggid_Login_Success)) {
                    
					if (ggidLoginSucceedCallback != null) {
						ggidLoginSucceedCallback (msg, mode);
						ggidLoginSucceedCallback = null;
					}
					else {
						Debug.Log ("===> can't run ggidLoginSucceedCallback(), no delegate object.");
					}
				} 
				else if (function.Equals (Unity_Callback_Message_Function_AASDK_Ggid_Login_Fail)) {

					if (ggidLoginFailCallback != null) {
						ggidLoginFailCallback (msg);
						ggidLoginFailCallback = null;
					}
					else {
						Debug.Log ("===> can't run ggidLoginFailCallback(), no delegate object.");
					}
				}else if (function.Equals (Unity_Callback_Message_Function_AASDK_Token_Login_Success)) {

					if (tokenLoginSucceedCallback != null) {
						tokenLoginSucceedCallback (msg,mode);
						tokenLoginSucceedCallback = null;
					}
					else {
						Debug.Log ("===> can't run tokenLoginSucceedCallback(), no delegate object.");
					}
				}else if (function.Equals (Unity_Callback_Message_Function_AASDK_Token_Login_Fail)) {

					if (tokenLoginFailCallback != null) {
						tokenLoginFailCallback (msg);
						tokenLoginFailCallback = null;
					}
					else {
						Debug.Log ("===> can't run tokenLoginFailCallback(), no delegate object.");
					}
				}
				else {
					Debug.Log ("===> onTargetCallback unkown function:" + function);
				}
			}
        

        }

	}
}


