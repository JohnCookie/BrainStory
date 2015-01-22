using UnityEngine;
using System.Collections;

public enum CommonDialogStyle{
	OnlyConfirmStyle,
	ConfirmCancleStyle
}

public class CommonDialog : MonoBehaviour
{
	DialogCallback callback1;
	DialogCallback callback2;
	UILabel m_labelTitle;
	UILabel m_labelContent;
	UIButton m_btnCenter;
	UIButton m_btnLeft;
	UIButton m_btnRight;
	// Use this for initialization
	public void Init (CommonDialogStyle style, string title, string content, DialogCallback callback1, DialogCallback callback2)
	{

	}
}

