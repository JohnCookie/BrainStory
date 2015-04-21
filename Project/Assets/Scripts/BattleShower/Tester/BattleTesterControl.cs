using UnityEngine;
using System.Collections;

public class BattleTesterControl : MonoBehaviour
{
	BattleTesterUnit m_battleUnit;

	void Awake(){
		m_battleUnit = transform.FindChild ("BattleTesterUnit").GetComponent<BattleTesterUnit> ();
	}

	// Use this for initialization
	void Start ()
	{

	}

	public void OnShowMove(){
		m_battleUnit.Move ();
	}
	public void OnShowAtk(){
		m_battleUnit.Attack ();
	}
	public void OnShowCast(){
		m_battleUnit.Cast ();
	}
	public void OnShowDie(){
		m_battleUnit.Die ();
	}
	public void OnShowHurted(){
		m_battleUnit.Hurted ();
	}
	public void OnShowHeal(){
		m_battleUnit.Healed ();
	}
	public void OnBack(){
		UISystem.getInstance ().showLastPage ();
	}
}

