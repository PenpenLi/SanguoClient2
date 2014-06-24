/// <summary>
/// 主介面的控制
/// </summary>
using UnityEngine;
using System.Collections;

public class Panel_Main : MonoBehaviour 
{
	// Use this for initialization
	void Awake () 
	{
		// 做資料的初使化

	}
	
	// Update is called once per frame
	void Update () 
	{
		// 根據 PlayerAttr 去設定資料
	}

	#region Clild Click

	void OnChildClick_Button_Info (object Data )
	{
		LogMgr.DebugLog ("[OnChildClick_Button_Info]");
		// 開啟面版
		GameUtility.ShowUI ("Panel_PlayerInfo");
	}

	void OnChildClick_Button_Backpack (object Data )
	{
		LogMgr.DebugLog ("[OnChildClick_Button_Backpack]");
	}
	
	void OnChildClick_Button_Team (object Data )
	{
		LogMgr.DebugLog ("[OnChildClick_Button_Team]");
	}
	
	void OnChildClick_Button_Other (object Data )
	{
		LogMgr.DebugLog ("[OnChildClick_Button_Other]");
	}
	
	void OnChildClick_Sprite_AddGem (object Data )
	{
		LogMgr.DebugLog ("[OnChildClick_Sprite_AddGem]");
	}
	
	#endregion
}
