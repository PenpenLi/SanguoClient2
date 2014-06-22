using UnityEngine;

/// <summary>
/// 可以把畫面拉好
/// </summary>
public class SpriteProperty : MonoBehaviour 
{
	// 是否全營幕
	public bool IsFullScreen = false;

	// Use this for initialization
	void Awake () 
	{
		UISprite Picture = this.gameObject.GetComponent<UISprite> ();
		if (IsFullScreen == true)
		{
			// 把背景拉成全畫面
			Vector2 ScreenPos = GameUtility.GetScreenSize ();
			// 取得 Backgound
			Picture.SetDimensions (System.Convert.ToInt32 (ScreenPos.x), System.Convert.ToInt32 (ScreenPos.y));
		}
	}
}
