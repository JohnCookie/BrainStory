using UnityEngine;
using System.Collections;

public class BattleTesterControl : MonoBehaviour
{
	BattleTesterUnit m_battleUnit;
	BaseMonsterShower m_concretMonsterShower;

	void Awake(){
		m_battleUnit = transform.FindChild ("BattleTesterUnit").GetComponent<BattleTesterUnit> ();

		GameObject factoryObj;
		ResourceSystem.getInstance().loadRes("Prefabs/Battle/BattleMonsterFactory", (obj)=>{
			GameObject factory = obj as GameObject;
			factoryObj = (GameObject)Instantiate (factory);
			factoryObj.name = "BattleMonsterFactory";
			factoryObj.transform.parent = transform;
			factoryObj.transform.localPosition = Vector3.zero;
			factoryObj.transform.localScale = Vector3.one;
		});

		TimerHelper.getInstance().DelayFunc(1.0f, ()=>{
			m_concretMonsterShower = BattleMonsterPrefabFactory.getInstance().createMonsterShower(1);
			m_concretMonsterShower.transform.parent = transform;
			m_concretMonsterShower.transform.localScale=Vector3.one;
			m_concretMonsterShower.transform.localPosition=Vector3.zero;
		});

	}

	// Use this for initialization
	void Start ()
	{

	}

	public void OnShowMove(){
		m_battleUnit.Move ();
		m_concretMonsterShower.Move(1,1,1);
	}
	public void OnShowAtk(){
		m_battleUnit.Attack ();
		m_concretMonsterShower.Attack();
	}
	public void OnShowCast(){
		m_battleUnit.Cast ();
		m_concretMonsterShower.Cast(1);
	}
	public void OnShowDie(){
		m_battleUnit.Die ();
		m_concretMonsterShower.Die();
	}
	public void OnShowHurted(){
		m_battleUnit.Hurted ();
		m_concretMonsterShower.Hurted(10);
	}
	public void OnShowHeal(){
		m_battleUnit.Healed ();
		m_concretMonsterShower.Healed(10);
	}
	public void OnBack(){
		UISystem.getInstance ().showLastPage ();
	}
}

