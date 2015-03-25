using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public delegate void SelectMonsterCallback(string info);

public class SelectMonsterPage : MonoBehaviour {
	UIButton m_btnBack;
	UIScrollView m_scrollMonsters;
	UIGrid m_gridMonsters;
	GameObject m_monsterItemObj;

	List<UserMonster> m_listData;
	SelectMonsterCallback m_callback;
	int m_slotId = 0;
	
	void Awake(){
		m_btnBack = transform.FindChild("BackBtn").GetComponent<UIButton>();
		m_scrollMonsters = transform.FindChild("MonsterScrollView").GetComponent<UIScrollView>();
		m_gridMonsters = m_scrollMonsters.transform.FindChild("MonsterGrid").GetComponent<UIGrid>();
		m_monsterItemObj = m_gridMonsters.transform.FindChild("MiniMonsterShower").gameObject;
	}
	
	void Start(){
	}

	public void Init(List<UserMonster> _list, int _slotId, SelectMonsterCallback _callback){
		resetGridToOriginal ();
		m_callback = _callback;
		m_listData = _list;
		m_slotId = _slotId;
		m_listData.Sort (delegate(UserMonster x, UserMonster y) {
			MonsterBase baseX = MonsterDataUntility.getInstance().getMonsterBaseInfoById(x.monster_id);
			MonsterBase baseY = MonsterDataUntility.getInstance().getMonsterBaseInfoById(y.monster_id);
			return baseY.quality-baseX.quality;
		});
		m_monsterItemObj.SetActive (false);
		for (int i=0; i<m_listData.Count; i++) {
			createOneItemToGrid(m_listData[i]);
		}
		m_gridMonsters.Reposition ();
	}
	
	void resetGridToOriginal(){
		for (int i=1; i<m_gridMonsters.transform.childCount; i++) {
			Destroy(m_gridMonsters.transform.GetChild(i).gameObject);
		}
	}
	
	void createOneItemToGrid(UserMonster _monster){
		GameObject clonedMonsterItem = Instantiate (m_monsterItemObj) as GameObject;
		clonedMonsterItem.transform.parent = m_gridMonsters.transform;
		clonedMonsterItem.transform.localScale = Vector3.one;
		clonedMonsterItem.transform.localPosition = Vector3.zero;
		clonedMonsterItem.SetActive (true);
		SummonSlotSelectBoxItem itemScirpt = clonedMonsterItem.GetComponent<SummonSlotSelectBoxItem> ();
		itemScirpt.Init (_monster, gameObject);
	}
	
	public void OnBack(){
		UISystem.getInstance ().showLastPage ();
	}

	public void OnReceiveMonsterSelected(string _id){
		m_callback(m_slotId.ToString()+"_"+_id);
		UISystem.getInstance ().showLastPage ();
	}
}
