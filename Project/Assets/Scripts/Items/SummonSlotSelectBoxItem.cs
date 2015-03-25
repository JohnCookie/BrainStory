using UnityEngine;
using System.Collections;

public class SummonSlotSelectBoxItem : MonoBehaviour {
	UISprite m_spriteMonster;
	UISprite m_spriteFrame;
	UILabel m_labelLv;
	
	UserMonster m_monsterData;
	GameObject parentPage;
	
	void Awake(){
		m_spriteMonster = transform.FindChild("monster").GetComponent<UISprite>();
		m_spriteFrame = transform.FindChild("frame").GetComponent<UISprite>();
		m_labelLv = transform.FindChild("lv").GetComponent<UILabel>();
	}
	
	public void Init(UserMonster _monster, GameObject _pageObj){
		parentPage = _pageObj;
		m_monsterData = _monster;
		MonsterBase _base = MonsterDataUntility.getInstance ().getMonsterBaseInfoById (m_monsterData.monster_id);
		m_spriteMonster.spriteName = _base.name;
		m_spriteFrame.spriteName = ResourceNameHelper.getInstance ().getSquareFrameNameByQuality (_base.quality);
		m_labelLv.text = "Lv.0";
	}
	
	public void OnMonsterItemClick(){
		parentPage.SendMessage ("OnReceiveMonsterSelected", m_monsterData.id.ToString (), SendMessageOptions.RequireReceiver);
	}
}
