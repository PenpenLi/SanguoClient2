// Author : dandanshih
// Date : 2014/6/13
// Desc : 希望可以不用每一個地方都丟進戈 Update

using UnityEngine;
using System.Collections;

public class SingletonUpdate : MonoBehaviour
{
	// 建立
	void Awake ()
	{
		// 應該要建表讀入才對
	}

	void Update ()
	{
		// 存檔功能
		ClientSaveMgr.Update ();
		// 網路功能
		GameService.instance().Update ();
	}
}
