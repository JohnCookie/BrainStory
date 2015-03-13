using UnityEngine;
using System.Collections;

public enum CommonDialogStyle{
	OnlyConfirmStyle,
	ConfirmCancleStyle
}

public class CommonDialog : MonoBehaviour
{
	DialogCallback leftCallback;
	DialogCallback rightCallback;
	DialogCallback middleCallback;
	UILabel m_labelTitle;
	UILabel m_labelContent;
	UIButton m_btnCenter;
	UIButton m_btnLeft;
	UIButton m_btnRight;

	void Awake(){
		m_labelTitle = gameObject.transform.FindChild("DialogTitle").GetComponent<UILabel>();
		m_labelContent = gameObject.transform.FindChild("DialogContent").GetComponent<UILabel>();
		m_btnLeft = gameObject.transform.FindChild("ButtonLeft").GetComponent<UIButton>();
		m_btnRight = gameObject.transform.FindChild("ButtonRight").GetComponent<UIButton>();
		m_btnCenter = gameObject.transform.FindChild("ButtonCenter").GetComponent<UIButton>();
	}
	// Use this for initialization
	public void Init (CommonDialogStyle style, string title, string content, DialogCallback _leftCallback, DialogCallback _rightCallback, DialogCallback _middleCallback)
	{
		m_labelTitle.text = title;
		m_labelContent.text = content;
		this.leftCallback = _leftCallback;
		this.rightCallback = _rightCallback;
		this.middleCallback = _middleCallback;
		switch (style) {
		case CommonDialogStyle.ConfirmCancleStyle:
			m_btnCenter.gameObject.SetActive(false);
			m_btnLeft.gameObject.SetActive(true);
			m_btnRight.gameObject.SetActive(true);
			m_btnLeft.transform.FindChild("Label").GetComponent<UILabel>().text = "Confirm";
			m_btnRight.transform.FindChild("Label").GetComponent<UILabel>().text = "Cancel";
			break;
		case CommonDialogStyle.OnlyConfirmStyle:
			m_btnCenter.gameObject.SetActive(true);
			m_btnLeft.gameObject.SetActive(false);
			m_btnRight.gameObject.SetActive(false);
			m_btnCenter.transform.FindChild("Label").GetComponent<UILabel>().text = "Close";
			break;
		}
	}

	public void OnLeftBtnClick(){
		if (leftCallback != null) {
			leftCallback (null);
		}
		Destroy (gameObject);
	}

	public void OnRightBtnClick(){
		if (rightCallback != null) {
			rightCallback (null);	
		}
		Destroy (gameObject);
	}

	public void OnMiddleBtnClick(){
		if (middleCallback != null) {
			middleCallback (null);
		}
		Destroy (gameObject);
	}

	public void SetTitle(string _title){
		m_labelTitle.text = _title;
	}

	public void SetContent(string _content){
		m_labelContent.text = _content;
	}

	public void SetLeftBtnText(string _value){
		m_btnLeft.transform.FindChild("Label").GetComponent<UILabel>().text = _value;
	}

	public void SetRightBtnText(string _value){
		m_btnRight.transform.FindChild("Label").GetComponent<UILabel>().text = _value;
	}

	public void SetMiddleBtnText(string _value){
		m_btnCenter.transform.FindChild("Label").GetComponent<UILabel>().text = _value;
	}
}

