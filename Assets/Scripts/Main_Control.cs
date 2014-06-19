/// <summary>
/// Author : dandanshih
/// Date : 2014/6/16
/// Desc : 做主流程的控制
/// </summary>
using UnityEngine;
using System.Text;
using System.Text.RegularExpressions;
using System.Collections;
using System.Collections.Generic;

public class Main_Control : MonoBehaviour 
{
//	void Awake ()
//	{
//		LogMgr.DebugLog ("--- Awake Start ---");
//		StartCoroutine (UnityCoroutine());
//		LogMgr.DebugLog ("--- Awake End ---");
//	}
//	IEnumerator UnityCoroutine()
//	{
//		LogMgr.DebugLog ("--- UnityCoroutine Start ---");
//		yield return new WaitForSeconds(5);
//		LogMgr.DebugLog ("--- UnityCoroutine End ---");
//	}



	// Use this for initialization
	void Start () 
	{
	}

	// Update is called once per frame
	void Update () 
	{
#if UNITY_EDITOR
		if (Input.GetKeyDown (KeyCode.A) == true)
		{
			Application.Quit ();
		}
#endif
	}
}
