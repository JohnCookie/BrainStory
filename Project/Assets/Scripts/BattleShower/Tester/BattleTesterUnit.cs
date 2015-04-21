using UnityEngine;
using System.Collections;

public class BattleTesterUnit : MonoBehaviour
{
	public UISprite m_spriteMonster;
	public Animator m_animator;
	public UILabel m_hub;

	int move_hash, attack_hash, cast_hash, die_hash, hurted_hash;

	void Awake(){
		move_hash = Animator.StringToHash ("BattleTesterUnitAnimation");
		attack_hash = Animator.StringToHash ("BattleTesterUnitAttack");
		cast_hash = Animator.StringToHash ("BattleTesterUnitCast");
		die_hash = Animator.StringToHash ("BattleTesterUnitDie");
		hurted_hash = Animator.StringToHash ("BattleTesterUnitHurted");
	}

	// Use this for initialization
	void Start ()
	{

	}

	// Update is called once per frame
	void Update ()
	{

	}

	public void Move(){
		m_animator.Play (move_hash);
	}
	public void Attack(){
		m_animator.Play (attack_hash);
	}
	public void Cast(){
		m_animator.Play (cast_hash);
	}
	public void Die(){
		m_animator.Play (die_hash);
	}
	public void Healed(){
		m_hub.color = Color.green;
		m_hub.text = "+10";
		m_animator.Play (hurted_hash);
	}
	public void Hurted(){
		m_hub.color = Color.red;
		m_hub.text = "-10";
		m_animator.Play (hurted_hash);
	}
}

