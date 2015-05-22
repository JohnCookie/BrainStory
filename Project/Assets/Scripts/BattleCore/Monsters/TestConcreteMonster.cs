using System;
using UnityEngine;

public class TestConcreteMonster : BattleMonsterBase
{
	public TestConcreteMonster(UserMonster _monster, Vector2 _pos, TeamType _team) : base(_monster, _pos, _team) {
		
	}

	#region implemented abstract members of BattleMonsterBase
	public override void Born ()
	{
		beforeBorn ();
		Report_Born ();
		afterBorn ();
	}
	protected override void beforeBorn ()
	{
	}
	protected override void afterBorn ()
	{
	}

	public override void Move ()
	{
		beforeMove ();
		if (status == MonsterStatus.Moving) {
			monsterRealPosX += movingX * GameConfigs.battle_tick_step;
			monsterRealPosY += movingY * GameConfigs.battle_tick_step;
			checkingNewPosition ();
		}
		afterMove ();
	}
	protected override void beforeMove ()
	{
	}
	protected override void afterMove ()
	{
	}

	public override void Search ()
	{
		Vector2 searchResult = BattleMapUtil.getNextStepPosition (this);
		if (this.monsterIndexX == (int)searchResult.x && this.monsterIndexY == (int)searchResult.y) {
			// same position
			setStatus(MonsterStatus.Prepared);
		} else {
			updateTargetIndex ((int)searchResult.x, (int)searchResult.y);
			setStatus(MonsterStatus.Moving);
			if(this.monsterTargetIndexX - this.monsterIndexX > 0){
				this.movingX = moveSpd;
			}else if(this.monsterTargetIndexX - this.monsterIndexX < 0){
				this.movingX = -moveSpd;
			}else{
				this.movingX = 0;
			}
			
			if(this.monsterTargetIndexY - this.monsterIndexY > 0){
				this.movingY = -moveSpd;
			}else if(this.monsterTargetIndexY - this.monsterIndexY < 0){
				this.movingY = moveSpd;
			}else{
				this.movingY = 0;
			}
		}
	}

	public override void Attack ()
	{
		beforeAttack ();
		attackIntervalAddUp += GameConfigs.battle_tick_step;
		if (attackIntervalAddUp >= atkSpd) {
			if (skills.Count > 0) {
				double cast_percent = intel * GameConfigs.intel_per_cast > GameConfigs.cast_max_percent ? GameConfigs.cast_max_percent : intel * GameConfigs.intel_per_cast;
				if (UnityEngine.Random.Range (0.0f, 1.0f) < cast_percent) {
					cast ();
				} else {
					fight ();
				}
			} else {
				fight ();
			}
			attackIntervalAddUp -= atkSpd;
		}
		afterAttack ();
	}
	protected override void fight(){
		Report_Fight ();
		int damage = 0;
		double crit_percent = agi * GameConfigs.agi_per_crit > GameConfigs.crit_max_percent ? GameConfigs.crit_max_percent : agi * GameConfigs.agi_per_crit;
		if (UnityEngine.Random.Range (0.0f, 1.0f) < crit_percent) {
			damage = (int)(atk*1.5-targetMonster.def) > 0 ? (int)(atk*1.5-targetMonster.def) : 0;
		} else {
			damage = (int)(atk - targetMonster.def) > 0 ? (int)(atk - targetMonster.def) : 0;
		}
		targetMonster.BeHurted (damage);
	}
	protected override void cast(){
		Report_Cast ();
	}
	protected override void beforeAttack ()
	{
	}
	protected override void afterAttack ()
	{
	}

	public override void BeHurted (int damage)
	{
		beforeHurted ();
		hp -= damage;
		if (hp <= 0) {
			setStatus(MonsterStatus.Dead);
		}
		Report_Hurted (damage);
		afterHurted ();
	}
	protected override void beforeHurted ()
	{
	}
	protected override void afterHurted ()
	{
	}

	public override void BeHealed (int heal)
	{
		beforeHealed ();
		Report_Healed (heal);
		afterHealed ();
	}
	protected override void beforeHealed ()
	{
	}
	protected override void afterHealed ()
	{
	}

	public override void Die ()
	{
		beforeDie ();
		if (team == TeamType.LeftTeam) {
			BattleData.getInstance().enermyBattleMosnterTeam.removeOneMonster(targetMonster);
			Debug.Log("Right Team has "+BattleData.getInstance().enermyBattleMosnterTeam.getMonsterNum()+" monster(s) left.");
		}
		if (team == TeamType.RightTeam) {
			BattleData.getInstance().playerBattleMonsterTeam.removeOneMonster(targetMonster);
			Debug.Log("Left Team has "+BattleData.getInstance().playerBattleMonsterTeam.getMonsterNum()+" monster(s) left.");
		}
		BattleData.getInstance ().battleMapData [monsterIndexX, monsterIndexY] = (int)MapTileType.None;
		BattleData.getInstance ().battleMapData [monsterTargetIndexX, monsterTargetIndexY] = (int)MapTileType.None;

		Report_Die ();
		afterDie ();
	}
	protected override void beforeDie ()
	{
	}
	protected override void afterDie ()
	{
	}
	#endregion
}
