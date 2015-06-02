using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MonsterSummonCDItem : MonoBehaviour
{
	UIProgressBar m_progressTime;
	UISprite m_spriteMonster;
	UILabel m_labelTime;
	List<int> m_summonResult;
	CDEvent m_cdEvent;
	Collider2D m_collider;

	bool inCD = false;

	void Awake(){
		m_collider.gameObject.SetActive (false);
	}

	public void Init(int start, int duration, List<int> summonResult){
		m_summonResult = summonResult;
		m_cdEvent = new CDEvent (start, duration, OnCDEnd);
		inCD = true;
	}

	// Update is called once per frame
	void Update ()
	{
		if (inCD) {
			m_cdEvent.updateTime (Time.deltaTime);
		}
	}

	void updateView(){
		m_progressTime.value = m_cdEvent.getCurrProgress ();
		m_labelTime.text = m_cdEvent.getLeftTimeStr ();
	}

	public void SummonComplete(){
		// show result
		ShowSummonResult ();
	}

	void OnCDEnd(){
		inCD = false;
		m_collider.gameObject.SetActive (true);
	}

	void ShowSummonResult(){
		UISystem.getInstance().showPage("Prefabs/SummonResultPage", (page)=>{
			SummonResultPage pageScript = page.GetComponent<SummonResultPage>();
			pageScript.Init(m_summonResult);
		});
	}
}

