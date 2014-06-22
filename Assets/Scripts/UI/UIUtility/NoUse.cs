using UnityEngine;
using System.Collections;

public class NoUse : MonoBehaviour 
{
	public int LimitLV = 0;
	public bool UseEnable = false;

	void OnClick ()
	{
		LogMgr.DebugLog ("[NoUse.OnClick]");
		if (PlayerAttr.PlayerLV < LimitLV)
		{
			GameUtility.ShowMessageBox (string.Format ("等級還未到{0}級, 還未開放", LimitLV), "未開放");
			return;
		}
		if (UseEnable == false)
		{
			GameUtility.ShowMessageBox ("功能未開放, 請期待", "未開放");
			return;
		}
	}
}
