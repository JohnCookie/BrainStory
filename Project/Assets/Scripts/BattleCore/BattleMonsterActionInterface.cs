using System;

public interface BattleMonsterActionInterface
{
	void Born();
	void Move();
	void Search();
	void Attack();
	void BeHurted(int damage);
	void BeHealed(int heal);
	void Die();
}
