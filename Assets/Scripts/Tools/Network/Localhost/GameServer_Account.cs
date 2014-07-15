using System;
using System.Collections;
using System.Collections.Generic;

// 單機版的 Client Game Server
public partial class GameServer
{
	// 帳號登入動作, 無條件做通過的動作
	public Dictionary<string, object> Account_Check (Dictionary<string, object> dictArgs)
	{
		Dictionary<string, object> dictResult = new Dictionary<string, object> ();
		// 加入成功
		dictResult["Result"] = ErrorID.Success;
		dictResult["Account"] = dictArgs["Account"];
		dictResult["Password"] = dictArgs["Password"];
		dictResult["AccountID"] = 0;
		dictResult["PlayerID"] = 0;
		dictResult["SessionKey"] = "";
		// 做登入的動作
		ClientAction.AddClientAction(dictResult, ClientActionID.ToLogin);
		// 傳回結果
		return dictResult;
	}
	
	// 取得玩家屬性
	public Dictionary<string, object> Player_GetAttr (Dictionary<string, object> dictArgs)
	{
		Dictionary<string, object> dictResult = new Dictionary<string, object> ();
		// 塞入結果
		dictResult ["Result"] = ErrorID.Success;
		// 姓名
		ClientAction.AddClientAction (dictResult, ClientActionID.Player_Name, ServerAPI.GetDataFromDict<string> (m_Player, DICT_PlayerAttr.PlayerName, "dandan"));
		// 遊戲幣
		ClientAction.AddClientAction (dictResult, ClientActionID.Game_Money, ServerAPI.GetDataFromDict<int> (m_Player, DICT_PlayerAttr.Money, 0));
		// 商城幣
		ClientAction.AddClientAction (dictResult, ClientActionID.Game_Coin, ServerAPI.GetDataFromDict<int> (m_Player, DICT_PlayerAttr.Coin, 0));
		// 玩家經驗值
		ClientAction.AddClientAction (dictResult, ClientActionID.Player_Exp, ServerAPI.GetDataFromDict<int> (m_Player, DICT_PlayerAttr.Exp, 0));
		// 等級
		ClientAction.AddClientAction (dictResult, ClientActionID.Player_LV, ServerAPI.GetDataFromDict<int> (m_Player, DICT_PlayerAttr.LV, 1));
		// 回傳結果
		return dictResult;
	}
}
