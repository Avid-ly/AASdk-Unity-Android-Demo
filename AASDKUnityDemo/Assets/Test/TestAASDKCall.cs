using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using System.IO;
using System.Text.RegularExpressions;

using AASDK;

public class TestAASDKCall : MonoBehaviour
{
	private bool inited;
	private const string PRODUCTID = "600027";
	// Use this for initialization
	void Start () {
	}

	// Update is called once per frame
	void Update () {

	}

	public void onInitClick() {
		if (inited) {
			return;
		}
		inited = true;

		AASDKApi.initSDK (PRODUCTID);
    }

    public void onAccountLoginClick() {
        AASDKApi.accountLogin();
    }

	public void onShowUserManagerUIClick() {
        AASDKApi.showUserManagerUI();
	}

	public void getGGid() {
        Text text = GameObject.Find("CallText").GetComponent<Text>();
        text.text = AASDKApi.getLoginedGGid();
       // AASDKApi.getLoginedGGid();
	}
    public void getFacebookLoginedToken() {
        Text text = GameObject.Find("CallText").GetComponent<Text>();
        text.text = AASDKApi.getFacebookLoginedToken();
    }
    

	public void onGetAAUGGidLogin() {
        Debug.Log("===> onGetAAUGGidLogin pressed ");
        AASDKApi.getAAUGgidData(
			new System.Action<string,string>(onAAUGgidLoginSuccess),
			new System.Action<string>(onAAUGgidLoginFail)
		);
	}

    public void onGetAAUTokenLogin()
    {
        Debug.Log("===> onGetAAUTokenLogin pressed ");

        AASDKApi.getAAUTokenData(
            new System.Action<string,string>(onAAUTokenLoginSuccess),
            new System.Action<string>(onAAUTokenLoginFail)
        );
    }
    private void onAAUGgidLoginSuccess(string ggid,string mode)
	{
        Text text = GameObject.Find("CallText").GetComponent<Text>();
        text.text = "ggid: "+ggid+"mode:"+mode;
        Debug.Log ("===> onAAUGgidLoginSuccess Callback at: " + ggid);
	}

	private void onAAUGgidLoginFail(string error)
	{
        Text text = GameObject.Find("CallText").GetComponent<Text>();
        text.text = "ggid error" + error;
        Debug.Log ("===> onAAUGgidLoginFail Callback at: " + error);
	}
    private void onAAUTokenLoginSuccess(string token,string mode)
    {
        Text text = GameObject.Find("CallText").GetComponent<Text>();
        text.text = "token: " + token+"mode: "+mode;
        Debug.Log("===> onAAUTokenLoginSuccess Callback at: " + token);
    }

    private void onAAUTokenLoginFail(string error)
    {
        Text text = GameObject.Find("CallText").GetComponent<Text>();
        text.text = "token error" + error;
        Debug.Log("===> onAAUTokenLoginFail Callback at: " + error);
    }
}

