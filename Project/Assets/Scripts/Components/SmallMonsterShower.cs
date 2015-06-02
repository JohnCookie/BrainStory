using UnityEngine;
using System.Collections;

public delegate void ShowerClickCallback();

public class SmallMonsterShower : MonoBehaviour
{
	UISprite m_spriteCornerType;
	UILabel m_labelName;
	UIGrid m_gridQuality;
	UISprite m_spriteRoundFrame;
	UISprite m_spriteSquareFrame;
	UISprite m_spriteMonster;

	MonsterBase m_monsterData;
	ShowerClickCallback _callback;

	void Awake(){
		m_spriteCornerType = transform.FindChild("LeftTopCorner").FindChild("cornerSprite").GetComponent<UISprite>();
		m_labelName=transform.FindChild("MonsterName").GetComponent<UILabel>();
		m_gridQuality=transform.FindChild("QualityStars").FindChild("Grid").GetComponent<UIGrid>();
		m_spriteRoundFrame=transform.FindChild("CircleFrame").GetComponent<UISprite>();
		m_spriteSquareFrame=transform.FindChild("QualityFrame").GetComponent<UISprite>();
		m_spriteMonster=transform.FindChild("Monster").GetComponent<UISprite>();
	}

	public void Init(int _id){
		MonsterBase _monsterData = MonsterDataUntility.getInstance().getMonsterBaseInfoById(_id);
		m_monsterData = _monsterData;

		m_spriteCornerType.spriteName = ResourceNameHelper.getInstance().getAttackTypeName((MonsterAtkType)m_monsterData.atk_type);
		m_labelName.text = m_monsterData.name;
		m_spriteRoundFrame.spriteName = ResourceNameHelper.getInstance().getRoundFrameNameByQuality(m_monsterData.quality);
		m_spriteSquareFrame.spriteName = ResourceNameHelper.getInstance().getSquareFrameNameByQuality(m_monsterData.quality);
		if (m_spriteMonster.atlas.GetSprite (m_monsterData.icon.ToString ()) != null) {
			m_spriteMonster.spriteName = m_monsterData.icon.ToString();		
		} else {
			m_spriteMonster.spriteName = "0";
		}

		for(int i=0; i<m_gridQuality.transform.childCount; i++){
			if(i<m_monsterData.quality){
				m_gridQuality.transform.GetChild(i).gameObject.SetActive(true);
			}else{
				m_gridQuality.transform.GetChild(i).gameObject.SetActive(false);
			}
		}
	}

	public void setMonsterClickCallback(ShowerClickCallback callback){
		this._callback=callback;
		EventDelegate.Set(transform.GetComponent<UIButton>().onClick, monsterClickCallback);
	}

  	void monsterClickCallback(){
		if(_callback!=null){
			_callback();
		}
	}
}

