using UnityEngine;
using System.Collections;

public class UserMonsterBoxPage : MonoBehaviour {
	UIButton m_btnBack;
	UIScrollView m_scrollMonsters;
	UIGrid m_gridMonsters;
	GameObject m_monsterItemObj;

	void Awake(){
		m_btnBack = transform.FindChild("BackBtn").GetComponent<UIButton>();
		m_scrollMonsters = transform.FindChild("MosnterScrollView").GetComponent<UIScrollView>();
		m_gridMonsters = m_scrollMonsters.transform.FindChild("MonsterGrid").GetComponent<UIGrid>();
		m_monsterItemObj = m_gridMonsters.transform.FindChild("MiniMonsterShower").gameObject;
	}
}
