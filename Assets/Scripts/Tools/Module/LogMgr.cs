/// <summary>
/// Author : dandanshih
/// Date : 2014/6/13
/// Desc : 做寫檔的 Singleton
/// </summary>

//[History]
// 2014/6/17 替 Log 加上時間
// 2014/6/17 加入 DebugLog
// 2014/6/17 加入 ErrorLog

using UnityEngine;
using System.Collections;

//[History]
// 2014/6/17 替 Log 加上時間
public class LogMgr
{
	static string _GetString (string Msg, object[] Args=null)
	{
		string Result = Msg;
		if (Args != null && Args.Length > 0)
			Result = string.Format (Msg.ToString(), Args);
		// 取得時間 2014/6/17
		Result = string.Format ("[{0}] {1}", System.DateTime.Now.ToString ("MM/dd hh:mm:ss"), Result);
		return Result;
	}

	public static void Log (string Msg, params object[] Args)
	{
		string Result = _GetString (Msg, Args);
		// 做出輸出的動作
		Debug.Log (Result);
	}

	public static void ErrorLog (string Msg, params object[] Args)
	{
		string Result = _GetString (Msg, Args);
		// 做出輸出的動作
		Debug.LogError (Result);
	}

	public static void DebugLog (string Msg, params object[] Args)
	{
#if UNITY_EDITOR
		string Result = _GetString (Msg, Args);
		// 做出輸出的動作
		Debug.Log (Result);
#endif
	}
}
