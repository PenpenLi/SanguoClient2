/// <summary>
/// 玩家屬性的存放區
/// </summary>

using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public partial class PlayerAttr
{
	# region PlayerAttr 基本操作
	static Dictionary<string, object> m_dictAttr = new Dictionary<string, object> ();
	public static void Reset ()
	{
		m_dictAttr.Clear ();
	}

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

}
