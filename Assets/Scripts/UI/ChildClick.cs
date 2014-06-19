// Author : dandnashih
// Date : 2014/6/12

//[History]
// 2014/6/17 
using UnityEngine;
using System.Collections;

// 如果 Child 被做行為要通知 Parent
public class ChildClick : MonoBehaviour 
{
	void OnClick ()
	{
		LogMgr.DebugLog ("[ChildClick] {0} is OnClick", this.name);
		// 把資料轉發給 Parent 去做處理
		GameObject Parent = GameUtility.GetParent (this.gameObject);
		// 呼叫另一個
		Parent.SendMessage (string.Format ("OnChildClick_{0}", this.name), this.name, SendMessageOptions.DontRequireReceiver);
	}
}
