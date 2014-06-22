using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using Newtonsoft.Json;

public class PanelMessageBox : MonoBehaviour 
{
	Dictionary<string, object> m_dictInfo = null;

	// 做 MessageBox 的顯示
	void ShowMessageBox (string JsonInfo)
	{
		Dictionary<string, object> dictInfo = JsonConvert.DeserializeObject<Dictionary<string, object>> (JsonInfo);
		if (dictInfo == null)
			return;
		string strTitle = "";
		if (dictInfo.ContainsKey("Title") == true)
		{
			strTitle = dictInfo["Title"].ToString();
		}
		string strMsg = "";
		if (dictInfo.ContainsKey ("Message") == true)
		{
			strMsg = dictInfo["Message"].ToString();
		}
		// 設定文字
		UILabel[] arrLabel = this.gameObject.GetComponentsInChildren<UILabel> ();
		foreach (UILabel Label in arrLabel)
		{
			if (Label.gameObject.name == "Label_MessageBox_Title")
			{
				Label.text = strTitle;
			}
			else if (Label.gameObject.name == "Label_MessageBox_Msg")
			{
				Label.text = strMsg;
			}
		}
		// 記錄資料
		m_dictInfo = dictInfo;
		// 顯示 UI
		GameUtility.ShowUI (this.gameObject);
	}

	#region 關閉的動作
	// 做關閉的動作
	void _CloseUI (object Data=null)
	{
		// 做關閉動作
		GameUtility.HideUI (this.gameObject, false);
		// 資料清掉
		m_dictInfo = null;
	}

	void OnClick ()
	{
		Close ();
	}
	
	void Close ()
	{
		OnChildClick_Button_Cancel ();
	}

	// 按下關閉的 X
	void OnChildClick_Sprite_Close (object Data=null)
	{
		OnChildClick_Button_Cancel (Data);
	}

	#endregion

	// 按下確定
	void OnChildClick_Button_OK (object Data=null)
	{
		// 取得要呼叫的參數
		if (m_dictInfo.ContainsKey ("OK_Method"))
		{
			GameUtility.CallObjectMethod (m_dictInfo["OK_Method"]);
		}
		// 都要關閉
		_CloseUI ();
	}

	void OnChildClick_Button_Cancel (object Data=null)
	{
		// 取得要呼叫的參數
		if (m_dictInfo.ContainsKey ("Cancel_Method"))
		{
			GameUtility.CallObjectMethod (m_dictInfo["Cancel_Method"]);
		}
		// 都要關閉
		_CloseUI ();
	}
}
