using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CustomUserMonsterPage : MonoBehaviour
{
	UIButton m_btnLeft;
	UIButton m_btnRight;
	UIButton m_btnAdd;
	UIButton m_btnBack;

	SmallMonsterShower m_shower;

	List<MonsterBase> m_monstersList;
	int currIndex = 0;

	void Awake()
	{
		m_btnLeft = transform.FindChild ("LeftBtn").GetComponent<UIButton> ();
		m_btnRight = transform.FindChild("RightBtn").GetComponent<UIButton>();
		m_btnBack = transform.FindChild("BackBtn").GetComponent<UIButton>();
		m_btnAdd = transform.FindChild("AddBtn").GetComponent<UIButton>();

		m_shower = transform.FindChild("SmallMonsterShower").GetComponent<SmallMonsterShower>();
	}

	// Use this for initialization
	void Start ()
	{
		m_monstersList = MonsterDataUntility.getInstance ().getAllMonster ();
		showMonsterAtIndex (currIndex);
	}

    void showMonsterAtIndex(int index){
		m_shower.Init (m_monstersList [index].id);
	}

	public void showLeft(){
		if (currIndex == 0) {
			currIndex = m_monstersList.Count - 1;
		} else {
			currIndex -= 1;
		}
		showMonsterAtIndex (currIndex);
	}
	public void showRight(){
		if (currIndex == m_monstersList.Count - 1) {
			currIndex = 0;
		} else {
			currIndex += 1;
		}
		showMonsterAtIndex (currIndex);
	}
	public void goBack(){
		UISystem.getInstance ().showLastPage ();
	}
	public void addCurrMonster(){
		UISystem.getInstance ().showCommonDialog (CommonDialogStyle.OnlyConfirmStyle, "", "Add Monster Success", null, null, (msg) => {
			UserDataGenerater.GetInstance ().AddNewMonsterById (m_monstersList [currIndex].id);
		});
	}
}

