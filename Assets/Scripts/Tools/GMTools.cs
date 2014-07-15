// Author : dandanshi
// Date : 2014/7/15
// Desc : 做 GM Tools

using UnityEngine;
using System;
using System.Reflection;
using System.Collections;
using System.Collections.Generic;

public class GMTools : MonoBehaviour 
{
	static GMAction _Action = new GMAction ();
	
	void OnClick ()
	{
		string strName = this.gameObject.name;
		_Action.CallGMAction (strName);
	}
}
// 所有提供的 GMAction
public class GMAction
{
	public void CallGMAction (string strMethodName)
	{
		LogMgr.DebugLog ("[GMAction] CallGMMethod, MehtodName={0}", strMethodName);
		MethodInfo method = this.GetType().GetMethod (strMethodName);
		if (method == null)
		{
			LogMgr.DebugLog ("Cannot Found the GMAction");
			return;
		}
		method.Invoke(this, BindingFlags.Public, Type.DefaultBinder, new object[0],null);
	}

	// 做加遊戲幣的動作
	public void Label_GameMoney ()
	{
		GameServer.instance().GM_AddGameMoney ();
	}
}

