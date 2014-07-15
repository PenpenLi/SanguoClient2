// Author : dandanshih
// Date : 2014/6/13
// Desc : 希望可以不用每一個地方都丟進戈 Update

#define NOT_NETWORK

using UnityEngine;
using System.Collections;

public class SingletonUpdate : MonoBehaviour
{
	// 建立
	void Awake ()
	{
		// 應該要建表讀入才對
#if NOT_NETWORK
		GameServer.instance();
#endif
	}

	void Update ()
	{
		// 存檔功能
		ClientSaveMgr.Update ();
		// 網路功能
		ClientService.instance().Update ();
		// 如果是單機版本就掛上更新
#if NOT_NETWORK
		GameServer.instance().Update ();
#endif
	}

	// 刪除時要做的事情
	void Destroy ()
	{
	}
}
