using System;
using System.Collections;
using System.Collections.Generic;

// 單機版的 Client Game Server
public partial class GameServer
{
	// 存放玩家資料
	Dictionary<string, object> m_Player = new Dictionary<string, object> ();
	
	void InitData ()
	{
		// 載入玩家資料
		string strResult = "";
		strResult = ServerAPI._GetValueByString (ClientSaveKey.PlayerAttr);
		if (strResult != null && strResult != "")
		{
			m_Player = Json.Deserialize<Dictionary<string, object>> (strResult);
		}
		else
		{
			// 把自己給標記起來
			ServerAPI.SetDataFromDict (m_Player, ClientSaveKey.ClientSaveKey, ClientSaveKey.PlayerAttr.ToString());
		}
	}

	// 做加錢的動作
	public void GM_AddGameMoney (int Counter=1000)
	{
		int OldMoney = ServerAPI.GetDataFromDict<int> (m_Player, DICT_PlayerAttr.Money, 0);
		ServerAPI.SetDataFromDict (m_Player, DICT_PlayerAttr.Money, OldMoney + Counter);
		int Money = ServerAPI.GetDataFromDict<int> (m_Player, DICT_PlayerAttr.Money, 0);
		// 通知 Client
		Dictionary<string, object> dictResult = new Dictionary<string, object> ();
		ClientAction.AddClientAction (dictResult, ClientActionID.Game_Money, Money);
		ServerAPI.PostResultToClient (null, dictResult);
	}
}
