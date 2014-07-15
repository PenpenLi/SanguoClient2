// 隔離 Client 和 Server 的相關 Code

#define CLIENT

using System;
using System.Collections;
using System.Collections.Generic;

public interface IServerAPI
{
	// 做 DebugLog 輸出
	void DebugLog (string strMsg, params object[] args);
	// 初使化的動作
	void InitData ();
	// 做更新和轉換
	void Update ();
	// 單機版專用 API, 回報 API
	void PostResultToClient (ServerPacket sp, Dictionary<string, object> dictResult);
	// 存取資料
	T GetDataFromDict<T> (Dictionary<string, object> dictArgs, object oKey, T defaultValue);
	void SetDataFromDict (Dictionary<string, object> dictArgs, object oKey, object Value);
	T _GetValue<T> (object oKey);
	string _GetValueByString (object oKey);
	void _SetValue (object oKey, object strValue);
}
