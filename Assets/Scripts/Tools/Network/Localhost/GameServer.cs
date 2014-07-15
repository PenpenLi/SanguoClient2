#define CLIENT

using System;
using System.Reflection;
using System.Collections;
using System.Collections.Generic;
using Newtonsoft.Json;

// 每一個 Server 的封包
public class ServerPacket
{
	// 呼叫的 FunctionName
	public string m_MethodName;
	// 帶的參數
	public Dictionary<string, object> m_Args;
	// 完成要呼叫 Client 的 callback
	public CompleteCallback m_callback;
	// Client 記錄的狀態
	public object UserState = null;
}

// 單機版的 Client Game Server
public partial class GameServer : Singleton<GameServer>
{
	// 存放封包的地方
	List<ServerPacket> m_QueuePacket = new List<ServerPacket> ();
	// API 的串接口
	IServerAPI ServerAPI = null;

	private GameServer ()
	{
		// 做初使化的動作
#if CLIENT
		ServerAPI = new ClientAPI ();
#endif
		InitData ();
		ServerAPI.InitData();
	}

	// 如果是單機版會在這裏做更新
	public void Update ()
	{
		if (m_QueuePacket.Count == 0)
			return;
		// 做 Server 更新和資料 (一次只做一個封包)
		ServerPacket sp = m_QueuePacket[0];
		m_QueuePacket.RemoveAt (0);
		// 取得資料做處理
		Dictionary<string, object> dictResult = DynamicCallGameServer (sp.m_MethodName, sp.m_Args);
		// 做處理的動作
#if CLIENT
		ServerAPI.PostResultToClient (sp, dictResult);
#endif
		ServerAPI.Update ();
	}

	// 動態呼叫
	Dictionary<string, object> DynamicCallGameServer(string strMethodName, Dictionary<string, object> dictInfo)
	{
		Type callMethod = typeof(GameServer);
		// 做呼叫前的 Log
		ServerAPI.DebugLog ("[Do][{0}] Args:{1}", strMethodName, JsonConvert.SerializeObject (dictInfo));
		// 真正做呼叫的動作
		return callMethod.InvokeMember(strMethodName, BindingFlags.InvokeMethod, null, this, new object[] { dictInfo }) as Dictionary<string, object>;
	}

	// 被傳送過來的資料
	public void Receive (string strMethodName, Dictionary<string, object> dictArgs, CompleteCallback callback, object UserState)
	{
		ServerAPI.DebugLog ("[Receive] Method:{0}, Args:{1}", strMethodName, JsonConvert.SerializeObject (dictArgs));
		ServerPacket sp = new ServerPacket ();
		sp.m_MethodName = strMethodName;
		sp.m_Args = dictArgs;
		sp.m_callback = callback;
		sp.UserState = UserState;
		// 放進去等待處理
		m_QueuePacket.Add (sp);
	}
}
