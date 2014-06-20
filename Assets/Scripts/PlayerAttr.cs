/// <summary>
/// 玩家屬性的存放區
/// </summary>

using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public partial class Const
{
	// 玩家性名
	public static string Tag_PlayerName = "Tag_PlayerName";
	// 遊戲幣
	public static string Tag_GameMoney = "Tag_GameMoney";
	// 靈石
	public static string Tag_GameCoin = "Tag_GameCoin";
	// MessageBox
	public static string Tag_Panel_MessageBox = "Tag_Panel_MessageBox";
}

public partial class PlayerAttr
{
	# region PlayerAttr 基本操作
	static Dictionary<string, object> m_dictAttr = new Dictionary<string, object> ();
	// 做清除的動作
	public static void Reset ()
	{
		m_dictAttr.Clear ();
	}

	// 取得屬性
	static object _GetAttr (string Key, object Default = null)
	{
		if (Default == null)
			Default = "";
		if (m_dictAttr.ContainsKey (Key) == false)
		{
			m_dictAttr[Key] = Default;
		}
		return m_dictAttr[Key];
	}

	// 設定屬性
	static void _SetAttr  (string Key, object Value)
	{
		m_dictAttr[Key] = Value;
	}

	#endregion

	// 帳號
	public static string Account
	{
		get
		{
			return _GetAttr ("Account", "").ToString();
		}
		set
		{
			_SetAttr ("Account", value);
		}
	}

	// AccountID
	public static int AccountID
	{
		get
		{
			return System.Convert.ToInt32 (_GetAttr ("AccountID", 0));
		}
		set
		{
			_SetAttr ("AccountID", value);
		}
	}

	// PlayerID
	public static int PlayerID
	{
		get
		{
			return System.Convert.ToInt32 (_GetAttr ("PlayerID", 0));
		}
		set
		{
			_SetAttr ("PlayerID", value);
		}
	}

	// SessionKey
	public static string SessionKey
	{
		get
		{
			return _GetAttr ("SessionKey", "").ToString();
		}
		set
		{
			_SetAttr ("SessionKey", value);
		}
	}

	// PlayerName
	public static string PlayerName
	{
		get
		{
			return _GetAttr ("PlayerName", "").ToString();
		}
		set
		{
			_SetAttr ("PlayerName", value);
			// 通知做畫面更新
			GameUtility.SetTagValue (Const.Tag_PlayerName, value);
		}
	}

	// GameMoney
	public static int GameMoney
	{
		get
		{
			return System.Convert.ToInt32 (_GetAttr ("GameMoney", 0));
		}
		set
		{
			_SetAttr ("GameMoney", value);
			GameUtility.SetTagValue (Const.Tag_GameMoney, value);
		}
	}

	// GameCoin
	public static int GameCoin
	{
		get
		{
			return System.Convert.ToInt32 (_GetAttr ("GameCoin", 0));
		}
		set
		{
			_SetAttr ("GameCoin", value);
			GameUtility.SetTagValue (Const.Tag_GameCoin, value);
		}
	}
	// [problem] PlayerLV
	public static int PlayerLV
	{
		get
		{
			return System.Convert.ToInt32 (_GetAttr ("PlayerLV", 0));
		}
		set
		{
			_SetAttr ("PlayerLV", value);
		}
	}
	// [problem] PlayerExp
	public static int PlayerExp
	{
		get
		{
			return System.Convert.ToInt32 (_GetAttr ("PlayerExp", 0));
		}
		set
		{
			_SetAttr ("PlayerExp", value);
		}
	}
}
