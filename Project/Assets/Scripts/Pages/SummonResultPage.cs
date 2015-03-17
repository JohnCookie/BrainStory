using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SummonResultPage : MonoBehaviour
{
	GameObject m_objBoxPart;
	GameObject m_objShowerPart;

	SmallMonsterShower m_shower;

	List<int> cardIds;
	int currIndex = 0;

	void Awake(){
		m_objBoxPart = transform.FindChild("CenterAnchor").FindChild("MonsterBoxPart").gameObject;
		m_objShowerPart = transform.FindChild("CenterAnchor").FindChild("MonsterPart").gameObject;

		m_shower = m_objShowerPart.transform.FindChild("SmallMonsterShower").GetComponent<SmallMonsterShower>();
		m_shower.setMonsterClickCallback(showMonster);
	}

	public void Init(List<int> _ids){
		currIndex = 0;
		cardIds = _ids;
		showMonster();
	}

	void showMonster(){
		if(currIndex>cardIds.Count-1){
			UISystem.getInstance().showLastPage();
		}else{
			m_shower.Init(cardIds[currIndex]);
			currIndex++;
		}
	}


}

