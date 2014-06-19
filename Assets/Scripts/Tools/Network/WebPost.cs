/// <summary>
/// Author : dandanshih
/// Date : 2014/6/17
/// </summary>

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.IO;
using System.Threading;
using Newtonsoft.Json;

// 網路出錯的類型
public enum ErrorType : int
{
	// 成功
	Success=0
	// 錯誤
	, Error
	// [problem] 目前有地方但還沒有辨法做判定
	, Timeout
}

/// <summary>
/// 使用的常數
/// </summary>
public partial class Const
{
	// Http 統一使用 Timeout
	public static int HttpGlobalTimeout = 1500;
	// 單一連線等待時
	public static int HttpReponseTimeout = 1000;
}

// 完成會呼叫的 FUNCTION 格式
public delegate void CompleteCallback(ErrorType errorCode, object result, object userState, WebPost.CReqState state);

/// <summary>
/// WebPost 的摘要描述
/// </summary>
public class WebPost
{
	// 回應的相關資料
	public class CReqState
	{
		public HttpWebRequest request;
		public string ConnectURL;
		public object userState=null;
		public event CompleteCallback callback;
		
		public void finishcallback(ErrorType errorCode, object result=null)
		{
			if (this.callback != null)
				this.callback(errorCode, result, userState, this);
			else
				System.Console.WriteLine(result.ToString());
		}
	}
	
	// Abort the request if the timer fires.
	private static void TimeoutCallback(object state, bool timedOut)
	{
		// 如果超時
		if (timedOut)
		{
			HttpWebRequest request = state as HttpWebRequest;
			if (request != null)
			{
				request.Abort();
			}
		}
	}
	
	// 要統一做處理
	static void FinishWebRequest(IAsyncResult result)
	{
		LogMgr.DebugLog ("[FinishWebRequest] {0}", result);
		CReqState asyncState = (CReqState)result.AsyncState;
		try
		{
			//LogMgr.DebugLog ("1");
			HttpWebResponse cRsp = (HttpWebResponse)asyncState.request.EndGetResponse(result);
			//LogMgr.DebugLog ("2");
			StreamReader cStmRdr = new StreamReader(cRsp.GetResponseStream());
			//LogMgr.DebugLog ("3");
			string output = string.Empty;
			//LogMgr.DebugLog ("4");
			output = cStmRdr.ReadToEnd();
			//LogMgr.DebugLog ("5");
			cStmRdr.Close();
			//LogMgr.DebugLog ("[FinishWebRequest] {0}", output);
			asyncState.finishcallback(ErrorType.Success, output);
		}
		// 取不到資料
		catch (Exception e)
		{
			LogMgr.ErrorLog ("[FinishWebRequest] Exception:{0}", e.ToString());
			asyncState.finishcallback(ErrorType.Error);
		}
	}
	
	public static void _PostHttp (string strConnectURL, string strContent, CompleteCallback callback=null)
	{
		// 產生連線
		HttpWebRequest cWebReq = (HttpWebRequest)WebRequest.Create(strConnectURL);
		// 設定連線內容
		cWebReq.AllowAutoRedirect = true;
		cWebReq.ServicePoint.Expect100Continue = true;
		cWebReq.Method = "POST";
		cWebReq.ContentLength = strContent.Length;
		cWebReq.ContentType = "application/json; charset=utf-8";
		// 設定 Timeout時間
		cWebReq.Timeout = Const.HttpGlobalTimeout;
		// 寫入參數
		System.IO.StreamWriter sw = new System.IO.StreamWriter(cWebReq.GetRequestStream());
		sw.Write(strContent);
		sw.Close();
		// 記錄相關資料
		CReqState myRequestState = new CReqState();
		myRequestState.request = cWebReq;
		myRequestState.ConnectURL = strConnectURL;
		if (callback != null)
			myRequestState.callback += callback;
		// 丟進去 Queue 裏等
		IAsyncResult result = cWebReq.BeginGetResponse(new AsyncCallback(FinishWebRequest), myRequestState);
		// 做等待的動作
		ThreadPool.RegisterWaitForSingleObject(result.AsyncWaitHandle, new WaitOrTimerCallback(TimeoutCallback), myRequestState, Const.HttpReponseTimeout, true);

	}
	
	// 做 Post 上去的動作
	public static void PostHttp(string strURL, string strMethodName, Dictionary<string, object> dictArgs, CompleteCallback callback)
	{
		LogMgr.DebugLog ("[WebPost][PostHttp] MethodName:{0}, Args:{1}", strMethodName, JsonConvert.SerializeObject(dictArgs));
		// 連線網址
		string strConnectURL = string.Format("{0}/{1}", strURL, strMethodName);
		Dictionary<string, object> dictResult = new Dictionary<string, object>();
		dictResult["strJson"] = JsonConvert.SerializeObject (dictArgs);
		// 參數內容
		string strContent = JsonConvert.SerializeObject (dictResult);
		// 呼叫做處理
		_PostHttp(strConnectURL, strContent, callback);
	}
}
