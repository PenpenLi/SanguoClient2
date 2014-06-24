using UnityEngine;
using System.Collections;

public class Panel_PlayerInfo : MonoBehaviour 
{
	void Awake ()
	{
		OnShow ();
	}
	// 如果被開啟時
	void OnShow()
	{
		LogMgr.DebugLog ("[Panel_PlayerInfo.Awake]");
//		// 取得大頭貼
//		UISprite SpriteIcon = GameUtility.GetComponentInChildren<UISprite> (this.gameObject, "Sprite_Icon") as UISprite;
//		SpriteIcon.spriteName = "npc10003";
		// 取得描述
		UILabel LabelDesc = GameUtility.GetComponentInChildren<UILabel> (this.gameObject, "Label_PlayerInfo") as UILabel;
		string strResult = PlayerAttr.instance().ToString ();
		LabelDesc.text = strResult;
	}

	// 被關閉的動作
//	void OnHide ()
//	{
//	}

	void _CloseUI ()
	{
		GameUtility.HideUI (this.gameObject);
	}

	void OnChildClick_Button_Close (object Data)
	{
		LogMgr.DebugLog ("[Panel_PlayerInfo] OnChildClick_Button_Close");
		_CloseUI ();
	}
}
