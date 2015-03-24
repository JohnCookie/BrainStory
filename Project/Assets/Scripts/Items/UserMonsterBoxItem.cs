using UnityEngine;
using System.Collections;

public class UserMonsterBoxItem : MonoBehaviour {
	UISprite m_spriteMonster;
	UISprite m_spriteFrame;
	UILabel m_labelLv;

	void Awake(){
		m_spriteMonster = transform.FindChild("monster").GetComponent<UISprite>();
		m_spriteFrame = transform.FindChild("frame").GetComponent<UISprite>();
		m_labelLv = transform.FindChild("lv").GetComponent<UILabel>();
	}

	public void OnMonsterItemClick(){

	}
}
