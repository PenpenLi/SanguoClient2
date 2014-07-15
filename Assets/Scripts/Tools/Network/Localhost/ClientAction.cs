using System;
using System.Collections;
using System.Collections.Generic;

public partial class ClientAction
{
	//-------------------------------------------------------------
	// 做 Client 的行為定義
	static void AddClientAction(Dictionary<string, object> dictResult, object oClientAction)
	{
		if (dictResult.ContainsKey("ClientAction") == false)
		{
			dictResult["ClientAction"] = new List<object>();
		}
		List<object> listClientAction = dictResult["ClientAction"] as List<object>;
		listClientAction.Add(oClientAction);
	}
	//-------------------------------------------------------------
	// 做呼叫的動作
	public static void AddClientAction(Dictionary<string, object> dictResult, string ActionID, object Args = null)
	{
		// 先取得 Client Action
		Type eType = typeof(ClientActionID);
		System.Reflection.FieldInfo FieldInfo = eType.GetField(ActionID);
		object data = FieldInfo.GetRawConstantValue();
		ClientActionID CAID = (ClientActionID)data;
		// 加入 Client Action
		AddClientAction(dictResult, CAID, Args);
	}
	public static void AddClientAction(Dictionary<string, object> dictResult, ClientActionID CAID, object Args = null)
	{
		// 加入 Client Action
		AddClientAction(dictResult, new KeyValuePair<ClientActionID, object>(CAID, Args));
	}
	
}