// Author : dandnashih
// Date : 2014/6/12

// 只移動 x 軸的 HideUI
#define Move_Only_X

using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

// 常用的幾個面版
public partial class Const
{
	// 登入面版 (用的少就不使用 Tag)
	public static string Panel_Login = "Panel_Login";
	// 主面版(用的少就使用 Tag)
	public static string Panel_Main = "Panel_Main";
}

public partial class GameUtility
{
	#region 和InputText的互動
	
	// 設定 Input 的文字
	public static void SetInputText (object UI, string Text)
	{
		UIInput obj = null;
		if (UI is string)
		{
			obj = GameObject.Find (UI as string).GetComponent<UIInput> ();
		}
		else
		{
			obj = (UI as GameObject).GetComponent<UIInput> ();
		}
		obj.value = Text;
	}
	
	public static string GetInputText (object UI)
	{
		UIInput obj = null;
		if (UI is string)
		{
			obj = GameObject.Find (UI as string).GetComponent<UIInput>();
		}
		else
		{
			obj = (UI as GameObject).GetComponent<UIInput> ();
		}
		return obj.value;
	}
	
	#endregion
	
	#region UI的操作行為
	
	static TweenScale _GetTweenScale (GameObject Target)
	{
		// 如果沒有腳本就幫忙掛上
		TweenScale TS = Target.GetComponent<TweenScale> ();
		if (TS == null)
		{
			Target.AddComponent<TweenScale>();
			TS = Target.GetComponent<TweenScale> ();
		}
		return TS;
	}

	public static void ShowUI (object UI, bool IsNow = false)
	{
		string UIName = "";
		GameObject Target = null;
		if (UI is string)
		{
			UIName = UI as string;
			Target = GameObject.Find (UIName);
		}
		else
		{
			Target = UI as GameObject;
			UIName = Target.name;
		}
		// 檢杳是不是打開了
		Vector3 v3Scale = Target.gameObject.transform.localScale;
		if (v3Scale.x > 0.9 && v3Scale.y > 0.9)
		{
			LogMgr.DebugLog("[UIOpen] Opened, no thing to do");
			return;
		}
		// 做放大和綻小的動作
		TweenScale TS = _GetTweenScale (Target);
		// 做設定
		TS.from.x = 0;
		TS.from.y = 0;
		TS.from.z = 0;
		TS.to.x = 1;
		TS.to.y = 1;
		TS.to.z = 1;
		if (IsNow == false)
			TS.duration = 0.3f;
		else
			TS.duration = 0.01f;
		// 做播放
		TS.PlayForward ();
	}
	
	public static void HideUI (object UI, bool IsNow = false)
	{
		string UIName = "";
		GameObject Target = null;
		if (UI is string)
		{
			UIName = UI as string;
			Target = GameObject.Find (UIName);
		}
		else
		{
			Target = UI as GameObject;
			UIName = Target.name;
		}
		Vector3 v3Scale = Target.gameObject.transform.localScale;
		if (v3Scale.x < 0.1 && v3Scale.y < 0.1)
		{
			LogMgr.DebugLog("[UIClose] Closed, no thing to do");
			return;
		}
		// 做放大和綻小的動作
		TweenScale TS = _GetTweenScale (Target);
		// 做設定
		TS.from.x = 1;
		TS.from.y = 1;
		TS.from.z = 1;
		TS.to.x = 0;
		TS.to.y = 0;
		TS.to.z = 0;
		if (IsNow == false)
			TS.duration = 0.3f;
		else
			TS.duration = 0.01f;
		// 做播放
		TS.PlayForward ();
	}

	// 設定 Tag 的值
	public static void SetTagValue (string Tag, object Value)
	{
		GameObject[] TagObjects = GameObject.FindGameObjectsWithTag (Tag);
		if (TagObjects == null)
			return;
		if (TagObjects.Length == 0)
			return;
		foreach (GameObject TagObject in TagObjects)
		{
			UILabel UI = TagObject.GetComponent<UILabel> ();
			if (UI == null)
				continue;
			UI.text = Value.ToString();
		}
	}
	
	#endregion
}
