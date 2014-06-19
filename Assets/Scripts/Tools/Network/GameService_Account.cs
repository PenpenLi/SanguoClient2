/// <summary>
/// 和帳號相關的 Protocol
/// </summary>

using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Newtonsoft.Json;

public partial class GameService : Singleton<GameService>
{
	// 檢查帳號密碼
	public static void Account_Check (string Account, string Password)
	{
		LogMgr.DebugLog ("[Account_Check] Account:{0}, Password:{1}", Account, Password);
		// 產生參數
		Dictionary<string, object> dictResult = new Dictionary<string, object> ();
		dictResult["Account"] = Account;
		dictResult["Password"] = Password;
		// 送出去給網路做使用
		WebPost.PostHttp (Const.ServerURL, "Account_Check", dictResult, ProtocolCompleteCallback);
	}

	// 取得玩家屬性
	public static void Player_GetAttr ()
	{
		LogMgr.DebugLog ("[Player_GetAttr]");
		// 產生參數
		Dictionary<string, object> dictResult = new Dictionary<string, object> ();
		dictResult["SessionKey"] = PlayerAttr.SessionKey;
		// 送出去給網路做使用
		WebPost.PostHttp (Const.ServerURL, "Player_GetAttr", dictResult, ProtocolCompleteCallback);
	}
}
