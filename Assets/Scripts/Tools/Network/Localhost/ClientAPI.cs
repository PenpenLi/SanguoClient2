// 隔離 Client 和 Server 的相關 Code

#define CLIENT

using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using Newtonsoft.Json;

// Client 所使用的資料 
public enum ClientSaveKey : int
{
	// 要存放的資料
	ClientSaveKey = 0,
	// 玩家屬性
	PlayerAttr = 1,
}

// 玩家屬性所擁有的內容
public enum DICT_PlayerAttr : int
{
	PlayerName = 1,
	Money = 2,
	Coin = 3,
	LV = 4,
	Exp = 5,
}

public partial class ClientAPI : IServerAPI
{
	public ClientAPI ()
	{
		InitData ();
	}
	public void DebugLog (string strMsg, params object[] args)
	{
		strMsg = string.Format (strMsg, args);
		LogMgr.DebugLog ("[Server] " + strMsg);
	}

	// 必須從 Server 來使用
	public void InitData ()
	{
		LogMgr.DebugLog ("[Server][InitData]");
	}

	// 回傳資料給 Client
	public void PostResultToClient (ServerPacket sp, Dictionary<string, object> dictResult)
	{
		object UserState = null;
		if (sp != null)
			UserState = sp.UserState;
		ClientService.ProtocolCompleteCallback (ErrorType.Success, dictResult, UserState, null);
	}

	#region Client 版存取資料的方式

	bool IsDirty = false;
	public void Update ()
	{
		if (IsDirty == false)
			return;
		IsDirty = false;
		// 自己可以決定是不是每次都做存檔的動作
		PlayerPrefs.Save ();
	}

	// 存資料
	public string _GetValueByString (object oKey)
	{
		string strKey = oKey.ToString();
		string strSaveValue = PlayerPrefs.GetString (strKey, "");
		return strSaveValue;
	}

	public T _GetValue<T> (object oKey)
	{
		string strKey = oKey.ToString();
//		string strSaveValue = PlayerPrefs.GetString (strKey, "");
		return JsonConvert.DeserializeObject<T> ( PlayerPrefs.GetString (strKey, "") );
	}

	// 寫資料
	public void _SetValue (object oKey, object strValue)
	{
		string strKey = oKey.ToString();
		PlayerPrefs.SetString (strKey, JsonConvert.SerializeObject (strValue));
		IsDirty = true;
	}

	public T GetDataFromDict<T> (Dictionary<string, object> dictArgs, object oKey, T defaultValue)
	{
		string strKey = oKey.ToString();
		if (dictArgs.ContainsKey (strKey) == false)
		{
			//dictArgs[strKey] = defaultValue;
			SetDataFromDict (dictArgs, oKey, defaultValue);
			return defaultValue;
		}
		return (T)System.Convert.ChangeType (dictArgs[strKey], typeof(T));
		//return (T) dictArgs[strKey];
	}
	
	public void SetDataFromDict (Dictionary<string, object> dictArgs, object oKey, object Value)
	{
		string strKey = oKey.ToString();
		dictArgs[strKey] = Value;
		_SetValue (dictArgs["ClientSaveKey"], dictArgs);
	}
	
	#endregion
}
