// Author : dandnashih
// Date : 2014/6/12

using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public partial class GameUtility
{
	// 取得老爸的 GameObject
	public static GameObject GetParent (GameObject Child)
	{
		return Child.gameObject.transform.parent.gameObject;
	}

	// 關閑程式
	public static void ExitGame ()
	{
		if (Application.isPlaying == true)
		{
		}
		Application.Quit ();
	}
}
