// Author : dandanshih
// Date : 2014/6/12

using UnityEngine;
using System.Collections;

// 用來設定 Unity 屬性的功能
//[ExecuteInEditMode]
public class UnityProperty : MonoBehaviour 
{
	// 是否會被刪除
	public bool IsDestroy = true;
	public bool IsEnable = true;
	// 在 Awake 時做設定
	void Start ()
	{
		// 設成 Dont Destry
		if (IsDestroy == false)
		{
			DontDestroyOnLoad (this.gameObject);
		}
		// 是否 Hide 起來
		if (IsEnable == false)
		{
			GameUtility.HideUI (this.gameObject, true);
		}
	}
}
