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
}
