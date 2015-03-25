using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class UserMonsterBoxPage : MonoBehaviour {
	UIButton m_btnBack;
	UIScrollView m_scrollMonsters;
	UIGrid m_gridMonsters;
	GameObject m_monsterItemObj;

	void Awake(){
		m_btnBack = transform.FindChild("BackBtn").GetComponent<UIButton>();
		m_scrollMonsters = transform.FindChild("MonsterScrollView").GetComponent<UIScrollView>();
		m_gridMonsters = m_scrollMonsters.transform.FindChild("MonsterGrid").GetComponent<UIGrid>();
		m_monsterItemObj = m_gridMonsters.transform.FindChild("MiniMonsterShower").gameObject;
	}

	void Start(){
		List<UserMonster> currUserMonsterList = UserDataGenerater.GetInstance ().UserMonsterDataList;
		currUserMonsterList.Sort (delegate(UserMonster x, UserMonster y) {
			MonsterBase baseX = MonsterDataUntility.getInstance().getMonsterBaseInfoById(x.monster_id);
			MonsterBase baseY = MonsterDataUntility.getInstance().getMonsterBaseInfoById(y.monster_id);
			return baseY.quality-baseX.quality;
		});
		resetGridToOriginal ();
		m_monsterItemObj.SetActive (false);
		for (int i=0; i<UserDataGenerater.GetInstance().UserMonsterDataList.Count; i++) {
			createOneItemToGrid(UserDataGenerater.GetInstance().UserMonsterDataList[i]);
		}
		m_gridMonsters.Reposition ();
	}

	void resetGridToOriginal(){
		for (int i=1; i<m_gridMonsters.transform.childCount; i++) {
			Destroy(m_gridMonsters.transform.GetChild(i));
		}
	}

	void createOneItemToGrid(UserMonster _monster){
		GameObject clonedMonsterItem = Instantiate (m_monsterItemObj) as GameObject;
		clonedMonsterItem.transform.parent = m_gridMonsters.transform;
		clonedMonsterItem.transform.localScale = Vector3.one;
		clonedMonsterItem.transform.localPosition = Vector3.zero;
		clonedMonsterItem.SetActive (true);
		UserMonsterBoxItem itemScirpt = clonedMonsterItem.GetComponent<UserMonsterBoxItem> ();
		itemScirpt.Init (_monster);
	}

	public void OnBack(){
		UISystem.getInstance ().showLastPage ();
	}
}
