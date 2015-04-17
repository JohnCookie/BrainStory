using System;
using UnityEngine;

public class TestMonster : BattleMonster
{
	public TestMonster(UserMonster _monster, Vector2 _pos, TeamType _team) : base(_monster, _pos, _team) {

	}

	public override void search ()
	{
		base.search ();
	}

	protected override void _fight ()
	{
		base._fight ();
		int damage = 0;
		double crit_percent = agi * GameConfigs.agi_per_crit > GameConfigs.crit_max_percent ? GameConfigs.crit_max_percent : agi * GameConfigs.agi_per_crit;
		if (UnityEngine.Random.Range (0.0f, 1.0f) < crit_percent) {
			damage = (int)(atk*1.5-targetMonster.def) > 0 ? (int)(atk*1.5-targetMonster.def) : 0;
		} else {
			damage = (int)(atk - targetMonster.def) > 0 ? (int)(atk - targetMonster.def) : 0;
		}
		targetMonster.beHurted (damage);
	}

	protected override void _cast ()
	{
		base._cast ();
	}

	public override void beHurted (int damage)
	{
		hp -= damage;
		if (hp <= 0) {
			setStatus(MonsterStatus.Dead);
		}
		base.beHurted (damage);
	}

	public override void beHealed (int heal)
	{
		base.beHealed (heal);
	}

	public override void updateDotDebuff ()
	{
		base.updateDotDebuff ();
	}

	public override void updateHotBuff ()
	{
		base.updateHotBuff ();
	}

	public override void die ()
	{
		base.die ();
	}
}

