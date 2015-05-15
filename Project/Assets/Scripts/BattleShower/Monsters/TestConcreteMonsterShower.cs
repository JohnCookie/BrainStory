using UnityEngine;
using System.Collections;

public class TestConcreteMonsterShower : BaseMonsterShower
{
	double movingTime = 0;
	Vector3 targetPosition = Vector3.zero;
	Vector3 originPosition = Vector3.zero;
	double movSpd = 0;
	bool isMoving = false;

	void Awake(){
		idle_hash = Animator.StringToHash ("TestConcreteMonsterIdle");
		move_hash = Animator.StringToHash ("TestConcreteMonsterMove");
		attack_hash = Animator.StringToHash ("TestConcreteMonsterAttack");
		cast_hash = Animator.StringToHash ("TestConcreteMonsterCast");
		die_hash = Animator.StringToHash ("TestConcreteMonsterDie");
		hp_hash = Animator.StringToHash ("TestConcreteMonsterHpChange");
	}
	
	// Use this for initialization
	void Start ()
	{
		
	}
	
	// Update is called once per frame
	void Update ()
	{
		if (isMoving) {
			if (movSpd > 0) {
					transform.localPosition = Vector3.Lerp (originPosition, targetPosition, (float)(movingTime / (GameConfigs.map_grid_width / movSpd)));
					movingTime += Time.deltaTime;
			}
			if(movingTime > 1.1){
				isMoving = false;
			}
		}
	}
	#region implemented abstract members of BaseMonsterShower

	public override void Idle ()
	{
		m_animator.Play (idle_hash);
	}

	public override void Move (double x, double y, double spd)
	{
		m_animator.Play (move_hash);
		isMoving = true;
		originPosition = transform.localPosition;
		targetPosition = new Vector3 ((float)(x * GameConfigs.map_grid_width), (float)(-y * GameConfigs.map_grid_width), transform.localPosition.z);
		movSpd = spd;
		movingTime = 0;
	}

	public override void Attack ()
	{
		m_animator.Play (attack_hash);
	}

	public override void Cast (double skillId)
	{
		m_animator.Play (cast_hash);
	}

	public override void Die ()
	{
		m_animator.Play (die_hash);
	}

	public override void Healed (double heal)
	{
		m_hub.color = Color.green;
		m_hub.text = "+"+heal.ToString ();
		m_animator.Play (hp_hash);
	}

	public override void Hurted (double damage)
	{
		m_hub.color = Color.red;
		m_hub.text = "-" + damage.ToString ();
		m_animator.Play (hp_hash);
	}

	#endregion


}

