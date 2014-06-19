// Author : dandanshih
// Date : 2014/6/12

using UnityEngine;
using System.Collections;

public partial class Const
{
	public static string Input_Account = "Input_Account";
	public static string Input_Password = "Input_Password";
}

public class PanelLogin : MonoBehaviour 
{
	void Awake ()
	{
#if DanDan
		LogMgr.DebugLog ("[Test] Global Macro");
#endif
		LogMgr.DebugLog ("[PanelLogin] Awake");
		// 把帳號密碼給塞進去
		//ClientSaveMgr.SetString (Const.Input_Account, "dandan");
		//ClientSaveMgr.SetString (Const.Input_Password, "silver");
		GameUtility.SetInputText (Const.Input_Account, ClientSaveMgr.GetString (Const.Input_Account));
		GameUtility.SetInputText (Const.Input_Password, ClientSaveMgr.GetString (Const.Input_Password));
	}

	//  按下取消
	void OnChildClick_Cancel (object Data)
	{
		LogMgr.DebugLog ("[PanelLogin] OnChildClick_Cancel");
		Application.Quit ();
	}

	// 按下確定
	void OnChildClick_OK (object Data)
	{
		LogMgr.DebugLog ("[PanelLogin] OnChildClick_OK");
		// 送出帳號檢查
		GameService.Account_Check (GameUtility.GetInputText (Const.Input_Account)
		                           , GameUtility.GetInputText (Const.Input_Password)
		                           );
	}
}
