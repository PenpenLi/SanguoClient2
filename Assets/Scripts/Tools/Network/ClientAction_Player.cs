using UnityEngine;
using System;
using System.Reflection;
using System.Collections;
using System.Collections.Generic;

public partial class ClientAction
{
//	static Dictionary<ClientActionID, string> dictClientIDMap = new Dictionary<ClientActionID, string> ()
//	{
//		{ClientActionID.ToLogin, "ToLogin"}
//		, {ClientActionID.ToNewPlayer, "ToNewPlayer"}
//
//	};

	public static void RunClientAction (CClientActionData Data)
	{
		// 做轉換的動作
		string MethodName = Data.m_CID.ToString();
		// 取得 Reflection
		Type MyType = typeof (ClientAction);
		MethodInfo method = MyType.GetMethod (MethodName);
		if (method == null)
		{
			LogMgr.ErrorLog ("[ClientAction.RunClientAction] Error!! Cannot found [{0}]", MethodName);
			return;
		}
		method.Invoke (null, BindingFlags.Public | BindingFlags.Static, Type.DefaultBinder, new object[] {Data}, null);
	}

	#region 登入相關
	// Login
	public static void ToLogin (CClientActionData Data)
	{
		LogMgr.DebugLog ("[ClientAction.ToLogin]");
		// 先把 Login 面版關閉
		GameUtility.HideUI (Const.Panel_Login);
		// 記錄帳號 (Memory)
		string Account = Data.m_dictProtocol["Account"].ToString();
		PlayerAttr.Account = Account;
		// 記錄 AccountID
		int AccountID = System.Convert.ToInt32 (Data.m_dictProtocol["AccountID"].ToString());
		PlayerAttr.AccountID = AccountID;
		// 記錄 PlayerID
		int PlayerID = System.Convert.ToInt32 (Data.m_dictProtocol["PlayerID"].ToString());
		PlayerAttr.PlayerID = PlayerID;
		// 記錄 SessionKey
		string SessionKey = Data.m_dictProtocol["SessionKey"].ToString();
		PlayerAttr.SessionKey = SessionKey;
		// 記錄密碼
		ClientSaveMgr.SetString (Const.Input_Account, Account);
		ClientSaveMgr.SetString (Const.Input_Password, Data.m_dictProtocol["Password"].ToString());
		// 拉出 Panel
		GameUtility.ShowUI (Const.Panel_Main);
		// 從網路要資料
		GameService.Player_GetAttr ();
	}
	
	// 創角行為
	public static void ToNewPlayer (CClientActionData Data)
	{
		LogMgr.DebugLog ("[ClientAction.ToNewPlayer]");
	}

	#endregion

	#region 玩家屬性相關

	public static void Player_Name (CClientActionData Data)
	{
		LogMgr.DebugLog ("[ClientAction.Player_Name]");
		string PlayerName = Data.m_Value.ToString();
		PlayerAttr.PlayerName = PlayerName;
	}

	public static void Game_Money (CClientActionData Data)
	{
		LogMgr.DebugLog ("[ClientAction.Game_Money]");
		int GameMoney = System.Convert.ToInt32 (Data.m_Value);
		PlayerAttr.GameMoney = GameMoney;
	}

	public static void Game_Coin (CClientActionData Data)
	{
		LogMgr.DebugLog ("[ClientAction.Game_Gem]");
		int GameCoin = System.Convert.ToInt32 (Data.m_Value);
		PlayerAttr.GameCoin = GameCoin;
	}

	public static void Player_LV (CClientActionData Data)
	{
		LogMgr.DebugLog ("[ClientAction.Player_LV]");
		int PlayerLV = System.Convert.ToInt32 (Data.m_Value);
		PlayerAttr.PlayerLV = PlayerLV;
	}
	
	public static void Player_Exp (CClientActionData Data)
	{
		LogMgr.DebugLog ("[ClientAction.Player_Exp]");
		int PlayerExp = System.Convert.ToInt32 (Data.m_Value);
		PlayerAttr.PlayerExp = PlayerExp;
	}
	
	#endregion
}
