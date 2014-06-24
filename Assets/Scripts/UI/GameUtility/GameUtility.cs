// Author : dandnashih
// Date : 2014/6/12

using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using Newtonsoft.Json;

public partial class GameUtility
{
	// 取得老爸的 GameObject
	public static GameObject GetParent (GameObject Child)
	{
		return Child.gameObject.transform.parent.gameObject;
	}

	// 關閑程式
	public static void ExitGame ()
	{
		if (Application.isPlaying == true)
		{
		}
		Application.Quit ();
	}

	// 取得下面小的
	public static Component GetComponentInChildren<T> (GameObject obj, string ChildName)
	{
		Component[] arrT = obj.gameObject.GetComponentsInChildren (typeof (T));
		foreach (Component UI in arrT)
		{
			if (UI.name == ChildName)
				return UI;
		}
		return default(Component);
	}

	#region 和 GameObject 的互動

	// 統一使用這個方式做 Message 的傳送
	public static void CallObjectMethod (object jsonData)
	{
		List<string> listOK = JsonConvert.DeserializeObject<List<string>> (jsonData.ToString());
		GameObject obj = GameObject.Find (listOK[0]);
		string Method = listOK[1];
		string Args = listOK[2];
		if (obj != null)
		{
			obj.SendMessage (Method, Args, SendMessageOptions.DontRequireReceiver);
		}
	}

	public static void CallGameObjectWithTag (string Data, string MethodName, object oArgs)
	{
		GameObject obj = GameObject.FindGameObjectWithTag (Data);
		CallGameObject (obj, MethodName, oArgs);
	}

	public static void CallGameObject (object Data, string MethodName, object oArgs)
	{
		GameObject obj = null;
		if (Data is string)
		{
			obj = GameObject.Find (Data.ToString());
		}
		else
		{
			obj = Data as GameObject;
		}
		if (obj == null)
		{
			return;
		}
		string Args = "";
		if (oArgs is string)
		{
			Args = oArgs.ToString();
		}
		else
		{
			Args = JsonConvert.SerializeObject (oArgs);
		}
		obj.SendMessage (MethodName, Args);
	}

	#endregion

	#region 顯示 MessageBox
	public static void ShowMessageBox (string Message
	                                   , string Title = ""
	                                   , string OKName = ""
	                                   , string OKMethod = ""
	                                   , string OKArgs = ""
	                                   , string CancelName = ""
	                                   , string CancelMethod = ""
	                                   , string CancelArgs = ""
	                              	)
	{
		// 轉換參數
		List<string> listOK = new List<string> (){OKName, OKMethod, OKArgs};
		List<string> listCancel = new List<string> (){CancelName, CancelMethod, CancelArgs};
		// 產生參數
		Dictionary<string, object> dictArgs = new Dictionary<string, object> ();
		dictArgs["Message"] = Message;
		if (Title != "")
		{
			dictArgs["Title"] = Title;
		}
		if (OKName != "")
		{
			dictArgs["OK_Method"] = JsonConvert.SerializeObject (listOK);
		}
		if (CancelName != "")
		{
			dictArgs["Cancel_Method"] = JsonConvert.SerializeObject (listCancel);
		}
		CallGameObjectWithTag (Const.Tag_Panel_MessageBox, "ShowMessageBox", dictArgs);
	}

	#endregion
	
}
