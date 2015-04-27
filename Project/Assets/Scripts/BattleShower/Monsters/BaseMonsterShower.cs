using UnityEngine;
using System.Collections;

public abstract class BaseMonsterShower : MonoBehaviour
{
	public UISprite m_spriteMonster;
	public Animator m_animator;
	public UILabel m_hub;
	
	protected int idle_hash,move_hash, attack_hash, cast_hash, die_hash, hp_hash;

	void Awake(){
		move_hash = Animator.StringToHash ("BattleTesterUnitAnimation");
		attack_hash = Animator.StringToHash ("BattleTesterUnitAttack");
		cast_hash = Animator.StringToHash ("BattleTesterUnitCast");
		die_hash = Animator.StringToHash ("BattleTesterUnitDie");
		hp_hash = Animator.StringToHash ("BattleTesterUnitHurted");
	}
	
	// Use this for initialization
	void Start ()
	{
		
	}
	
	// Update is called once per frame
	void Update ()
	{
		
	}

	public void Locate(int x, int y){
		transform.localPosition = new Vector3 ((float)(x * GameConfigs.map_grid_width), (float)(-y * GameConfigs.map_grid_width), 0.0f);
	}

	public abstract void Idle ();
	public abstract void Move (int x, int y, double spd);
	public abstract void Attack ();
	public abstract void Cast (int skillId);
	public abstract void Die ();
	public abstract void Healed (int damage);
	public abstract void Hurted (int heal);
}

