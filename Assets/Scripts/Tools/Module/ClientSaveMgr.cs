/// <summary>
/// Author : dandanshih
/// Date : 2014/6/14
/// Desc : 做 Client 存檔的動作
/// </summary>
using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using Newtonsoft.Json;

public partial class Const
{
	public static string ClientSaveMgr = "ClientSaveMgr";
}

/// <summary>
/// 可以改寫成自己用自己的 Key 來做處理
/// </summary>
public class ClientSaveMgr : MonoBehaviour
{
	// 設定 Dirty Flag
	public static bool IsDirty = false;
	public static Dictionary<string, string> dictInfo = null;

	// 做存檔的動作
	public static void Update ()
	{
		if (dictInfo == null)
		{
			string jsonText = PlayerPrefs.GetString (Const.ClientSaveMgr, "");
			if (jsonText == "")
			{
				dictInfo = new Dictionary<string, string> ();
			}
			else
			{
				dictInfo = JsonConvert.DeserializeObject<Dictionary<string, string>> (jsonText);
			}
		}
		if (IsDirty == false)
			return;
		LogMgr.DebugLog ("[ClientSaveMgr] Update");
		IsDirty = false;
		PlayerPrefs.SetString (Const.ClientSaveMgr, JsonConvert.SerializeObject (dictInfo));
		PlayerPrefs.Save ();
	}

	#region 操作區

	public static void SetString (string strKey, string Value)
	{
		if (dictInfo == null)
			Update ();
		dictInfo[strKey] = Value;
		IsDirty = true;
	}
	public static string GetString (string strKey)
	{
		if (dictInfo == null)
			Update ();
		if (dictInfo.ContainsKey (strKey))
		    return dictInfo[strKey];
		else
		    return "";
	}

	#endregion
}
