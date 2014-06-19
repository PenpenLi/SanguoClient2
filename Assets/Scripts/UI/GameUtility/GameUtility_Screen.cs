// Author : dandnashih
// Date : 2014/6/12

using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public partial class GameUtility
{
	#region 取得畫面上的相關資料

	// 取得畫面上的長寛
	public static Vector2 GetScreenSize ()
	{
		return new Vector2 (Screen.width, Screen.height);
	}

//	// 設定畫面的轉向 ([problem] 失敗, 待處理)
//	public static void SetScreenRotation (ScreenOrientation SO)
//	{
//		if (SO == ScreenOrientation.LandscapeLeft)
//		{
//			Screen.autorotateToLandscapeLeft = true;
//			Screen.orientation = SO;
//		}
//		else if (SO == ScreenOrientation.LandscapeRight)
//		{
//			Screen.autorotateToLandscapeRight = true;
//			Screen.orientation = SO;
//		}
//		else if (SO == ScreenOrientation.Portrait)
//		{
//			Screen.autorotateToPortrait = true;
//			Screen.orientation = SO;
//		}
//		else if (SO == ScreenOrientation.PortraitUpsideDown)
//		{
//			Screen.autorotateToPortraitUpsideDown = true;
//			Screen.orientation = SO;
//		}
//	}

	#endregion
}
