/// <summary>
/// Author : dandanshih
/// Date : 2014/6/16
/// </summary>

using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using Newtonsoft.Json;
using System.Threading;

// 所使用到的常數
public partial class Const
{
	// 連線的 Http 位置
	// [problem] 以後應該要讀表或是從下載回來的資料得知
	//public static string ServerURL = "http://localhost/Sanguo/GameService.asmx";
	public static string ServerURL = "http://localhost:17044/Login/GameService.asmx";
}

// 每一個被放進來的 Client 行為
public class CClientActionData
{
	// 那個 ClientAction
	public ClientActionID m_CID;
	// ClientAction 所需要的參數 
	public object m_Value;
	// 不應該被使用的
	public Dictionary<string, object> m_dictProtocol;
	// 額外參數
	public object m_State;
	
	// 建構子
	public CClientActionData 
		(
			string ActionName
			, object oValue
			, Dictionary<string, object> dictProtocol
			, object State
			)
	{
		m_CID = (ClientActionID) System.Convert.ToInt32 (ActionName);
		m_Value = oValue;
		m_dictProtocol = dictProtocol;
		m_State = State;
	}
	public CClientActionData 
		(
			ClientActionID ActionName
			, object oValue
			, Dictionary<string, object> dictProtocol
			, object State
			)
	{
		m_CID = ActionName;
		m_Value = oValue;
		m_dictProtocol = dictProtocol;
		m_State = State;
	}

	public override string ToString ()
	{
		string strResult = string.Format ("[CClientActionData] CID:{0}, Value:{1}", m_CID.ToString(), m_Value);
		return strResult;
	}
}

// 開一條連線, 重覆一直用
public partial class ClientService : Singleton<ClientService>
{
	//  做 Thread Lock 用
	static Mutex m_MutexClientAction = new Mutex ();

	private ClientService ()
	{
	}

	// 統一回來的處理
	public static void ProtocolCompleteCallback(ErrorType errorCode, object result, object userState, CReqState state)
	{
		LogMgr.DebugLog ("[ClientService][ProtocolCompleteCallback] errorCode:{0}, result:{1}, userState:{2}, state:{3}", errorCode, result, userState, state);
		// 檢查是不是正確
		if (errorCode == ErrorType.Error)
		{
			LogMgr.ErrorLog ("[ClientService][ProtocolCompleteCallback] Error!! errorCode:{0}, result:{1}, userState:{2}, state:{3}", errorCode, result, userState, state);
			return;
		}
		else if (errorCode == ErrorType.Timeout)
		{
			// 做重送的動作?!
			LogMgr.ErrorLog ("[ClientService][ProtocolCompleteCallback] Timeout!! errorCode:{0}, result:{1}, userState:{2}, state:{3}", errorCode, result, userState, state);
			return;
		}
		else
		{
			// 取得參數 (From Network)
			Dictionary<string, object> dictResult = null;
			if (result is string)
			{
				dictResult = JsonConvert.DeserializeObject<Dictionary<string, object>> (result.ToString());
				dictResult = JsonConvert.DeserializeObject<Dictionary<string, object>> ( dictResult["d"].ToString());
			}
			// From Client Only Server
			else
			{
				dictResult = JsonConvert.DeserializeObject<Dictionary<string, object>> (JsonConvert.SerializeObject (result));
			}
			//Debug.Log (JsonConvert.SerializeObject( dictResult));
			if (dictResult.ContainsKey ("Result") == true)
			{
				ErrorID eid = (ErrorID)System.Convert.ToInt32 (dictResult["Result"]);
				if (eid != ErrorID.Success)
				{
					LogMgr.Log ("[ClientService][ProtocolCompleteCallback] Parser Error, ErrorID = {0}, Msg={1}", eid, IDMap.GetEnumAttribute (eid));
					//GameUtility.ShowMessageBox (IDMap.GetEnumAttribute (eid), "錯誤");
					//[problem]
					PushClientAction (ClientActionID.ShowMessage, IDMap.GetEnumAttribute (eid), dictResult, userState);
				}
			}
			// 取得 ClientAction
			if (dictResult.ContainsKey ("ClientAction") == false)
				return;
			//string jsonClientAction = dictResult["ClientAction"].ToString();
			List<KeyValuePair<string, object>> listAction = null;
			listAction = JsonConvert.DeserializeObject<List<KeyValuePair<string, object>>> (dictResult["ClientAction"].ToString());
			foreach (var ChildAction in listAction)
			{
				try
				{
					PushClientAction (ChildAction.Key, ChildAction.Value, dictResult, userState);
				}
				catch (Exception e)
				{
					LogMgr.ErrorLog ("[Error!!] {0}", e.ToString());
				}
			}
		}
	}

	#region 錯誤統一處理區
	
	#endregion
	
	// Update is called once per frame
	#region ClientAction 處理區
	static List<CClientActionData> m_ClientAction = new List<CClientActionData> ();

	// 取得 ClientAction
	public static void PushClientAction (string ClientActionName, object Value, Dictionary<string, object> dictProtocol, object State)
	{
		LogMgr.DebugLog ("[ClientService.PushClientAction] ClientActionName:{0}, Value:{1}", ClientActionName, Value);
		ModifyClientAction (new CClientActionData (ClientActionName, Value, dictProtocol, State));
	}
	public static void PushClientAction (ClientActionID ClientActionName, object Value, Dictionary<string, object> dictProtocol, object State)
	{
		LogMgr.DebugLog ("[ClientService.PushClientAction] ClientActionName:{0}, Value:{1}", ClientActionName.ToString(), Value);
		ModifyClientAction (new CClientActionData (ClientActionName, Value, dictProtocol, State));
	}

	// 塞入一個 / 取出一個
	static CClientActionData ModifyClientAction (CClientActionData Action = null)
	{
		m_MutexClientAction.WaitOne ();
		// 塞入一個
		if (Action != null)
		{
			m_ClientAction.Add (Action);
			m_MutexClientAction.ReleaseMutex ();
			return null;
		}
		// 取出一個
		CClientActionData Data = null;
		if (m_ClientAction.Count > 0)
		{
			Data = m_ClientAction[0];
			m_ClientAction.RemoveAt (0);
		}
		m_MutexClientAction.ReleaseMutex ();
		return Data;
	}
	// 計算數量

	#endregion

	// 更新
	public void Update () 
	{
		// 處理 callback
		WebPost.ProcessCallback ();
		// 沒有 Client Action 不需要處理
		if (m_ClientAction.Count == 0)
			return;
		// 取得要 Update 的資料
		CClientActionData Data = ModifyClientAction ();
		if (Data == null)
			return;
		ClientAction.RunClientAction (Data);
	}

}

