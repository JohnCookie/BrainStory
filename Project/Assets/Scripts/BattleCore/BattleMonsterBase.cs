using System;
using System.Collections.Generic;
using UnityEngine;

public enum TeamType{
	RightTeam,
	LeftTeam
}

public enum MonsterStatus{
	Idle,
	Prepared,
	Moving,
	Attacking,
	Dead
}

public abstract class BattleMonsterBase : BattleMonsterActionInterface
{
	public int battleUnitId;
	public long userMonsterId;
	public double monsterRealPosX;
	public double monsterRealPosY;
	public int monsterIndexX;
	public int monsterIndexY;
	public int monsterTargetIndexX;
	public int monsterTargetIndexY;
	public double movingX;
	public double movingY;
	public int icon;
	public double hp;
	public MonsterAtkType atkType;
	public MonsterDefType defType;
	public double atk;
	public double def;
	public List<int> skills = new List<int> ();
	public List<int> talents = new List<int>();
	public double intel;
	public double dex;
	public double agi;
	public double moveSpd;
	public double atkSpd;
	public int range;
	public TeamType team;
	public MonsterStatus status = MonsterStatus.Idle;
	public BattleMonsterBase targetMonster = null;
	
	protected double attackIntervalAddUp = 0;
	
	public BattleMonsterBase (UserMonster _monster, Vector2 _pos, TeamType _team){
		battleUnitId = BattleData.getInstance ().getBattleMonsterId ();
		userMonsterId = _monster.id;
		// pos info
		updateIndex ((int)_pos.x, (int)_pos.y);
		updateTargetIndex ((int)_pos.x, (int)_pos.y);
		monsterRealPosX = (_pos.x+1) * GameConfigs.map_grid_width - GameConfigs.map_grid_width * 0.5;
		monsterRealPosY = -(_pos.y+1) * GameConfigs.map_grid_width + GameConfigs.map_grid_width * 0.5;
		movingX = 0;
		movingY = 0;
		
		MonsterBase _baseInfo = MonsterDataUntility.getInstance ().getMonsterBaseInfoById (_monster.monster_id);
		icon = _baseInfo.icon;
		
		int monsterLv = LvExpDataUtility.getInstance ().getMonsterLv (_monster);
		hp = _baseInfo.hp + monsterLv * _baseInfo.hp_add;
		atkType = (MonsterAtkType)_baseInfo.atk_type;
		defType = (MonsterDefType)_baseInfo.def_type;
		atk = _baseInfo.atk + monsterLv * _baseInfo.atk_add;
		def = _baseInfo.def + monsterLv * _baseInfo.def_add;
		skills.Clear ();
		foreach(int skillId in _monster.skills){
			skills.Add(skillId);
		}
		talents.Clear ();
		foreach (int talentId in _monster.talents) {
			talents.Add(talentId);	
		}
		intel = _baseInfo.intel + monsterLv * _baseInfo.int_add;
		dex = _baseInfo.dex + monsterLv * _baseInfo.dex_add;
		agi = _baseInfo.agi + monsterLv * _baseInfo.agi_add;
		moveSpd = (GameConfigs.map_grid_width / _baseInfo.mov_spd);
		atkSpd = _baseInfo.atk_spd;
		range = _baseInfo.range;
		team = _team;
		
		setStatus (MonsterStatus.Idle);
	}

	protected void updateIndex(int newX, int newY){
		if (BattleData.getInstance ().battleMapData [monsterIndexX, monsterIndexY] == (int)MapTileType.Monster) {
			BattleData.getInstance ().battleMapData [monsterIndexX, monsterIndexY] = (int)MapTileType.None;
		}
		monsterIndexX = newX;
		monsterIndexY = newY;
		BattleData.getInstance().battleMapData[monsterIndexX, monsterIndexY] = (int)MapTileType.Monster;
	}

	protected void updateTargetIndex(int newX, int newY){
		if (BattleData.getInstance ().battleMapData [monsterTargetIndexX, monsterTargetIndexY] == (int)MapTileType.Occupied) {
			BattleData.getInstance ().battleMapData [monsterTargetIndexX, monsterTargetIndexY] = (int)MapTileType.None;
		}
		monsterTargetIndexX = newX;
		monsterTargetIndexY = newY;
		BattleData.getInstance ().battleMapData [monsterTargetIndexX, monsterTargetIndexY] = (int)MapTileType.Occupied;
	}

	public void setStatus(MonsterStatus _status){
		status = _status;
		Report_Status_Change ();
	}

	protected void checkingNewPosition(){
		int newMonsterIndexX = (int)BattleMapUtil.PositionInVector (monsterRealPosX, monsterRealPosY).x;
		int newMonsterIndexY = (int)BattleMapUtil.PositionInVector (monsterRealPosX, monsterRealPosY).y;
		if (newMonsterIndexX != monsterIndexX || newMonsterIndexY != monsterIndexY) {
			updateIndex(newMonsterIndexX,newMonsterIndexY);	
		}
	}
	protected bool checkIsArrived(){
		if (this.monsterIndexX == this.monsterTargetIndexX && this.monsterIndexY == this.monsterTargetIndexY) {
			return true;	
		} else {
			return false;
		}
	}
	
	protected bool checkTargetInRange(){
		if (targetMonster != null && BattleMapUtil.getDistanceBetween2BaseMonster (this, targetMonster) <= this.range) {
			return true;
		} else {
			return false;
		}
	}
	
	protected bool checkTargetIsAlive(){
		if (targetMonster.hp > 0) {
			return true;	
		} else {
			return false;
		}
	}

	// Born
	public abstract void Born ();
	protected abstract void beforeBorn ();
	protected abstract void afterBorn ();
	// Move
	public abstract void Move ();
	protected abstract void beforeMove ();
	protected abstract void afterMove ();
	// Search
	public abstract void Search ();
	// Attack
	public abstract void Attack ();
	protected abstract void fight();
	protected abstract void cast();
	protected abstract void beforeAttack();
	protected abstract void afterAttack();
	// BeHurted
	public abstract void BeHurted (int damage);
	protected abstract void beforeHurted ();
	protected abstract void afterHurted ();
	// BeHealed
	public abstract void BeHealed (int heal);
	protected abstract void beforeHealed ();
	protected abstract void afterHealed ();
	// Die
	public abstract void Die ();
	protected abstract void beforeDie ();
	protected abstract void afterDie ();

	public void updateSelfAction(){
		switch (status) {
		case MonsterStatus.Idle:
			Search();
			break;
		case MonsterStatus.Prepared:
			if(checkTargetInRange()){
				Debug.Log("Monster_"+battleUnitId+" detect: target in range");
				setStatus(MonsterStatus.Attacking);
			}else{
				Search();
			}
			break;
		case MonsterStatus.Moving:
			if(checkIsArrived()){
				Debug.Log("Monster_"+battleUnitId+" detect: arrive at target position ("+this.monsterTargetIndexX+","+this.monsterTargetIndexY+")");
				setStatus(MonsterStatus.Prepared);
			}
			break;
		case MonsterStatus.Attacking:
			if(checkTargetIsAlive()){
				Attack();
			}else{
				Debug.Log("Monster_"+battleUnitId+" detect: target die");
				setStatus(MonsterStatus.Prepared);
			}
			break;
		case MonsterStatus.Dead:
			Die();
			break;
		}
	}

	//reporter
	protected void Report_Born(){
		Debug.Log("[Report] Monster_"+battleUnitId+" born at ("+monsterIndexX+","+monsterIndexY+").");
		BattleReportGenerater.getInstance ().addEvent (battleUnitId, Math.Round(BattleData.getInstance ().currBattleTime,2), monsterIndexX, monsterIndexY, 0,  ReportActionType.Locate);
	}
	protected void Report_Status_Change(){
		Debug.Log ("[Report] Monster_" + battleUnitId + " switch status to " + status);
		if (status == MonsterStatus.Moving) {
			Debug.Log ("[Report] Monster_" + battleUnitId + " move to (" + monsterTargetIndexX + "," + monsterTargetIndexY + ")");
			BattleReportGenerater.getInstance().addEvent(battleUnitId, Math.Round(BattleData.getInstance ().currBattleTime,2), monsterTargetIndexX, monsterTargetIndexY, moveSpd, ReportActionType.Move);
		}
	}
	protected void Report_Fight(){
		Debug.Log ("[Report] Monster_" + battleUnitId + " fight on Monster_" + targetMonster.battleUnitId);
		BattleReportGenerater.getInstance().addEvent(battleUnitId, Math.Round(BattleData.getInstance ().currBattleTime,2), 0, 0, 0, ReportActionType.Attack);
	}
	protected void Report_Cast(){
		Debug.Log ("[Report] Monster_" + battleUnitId + " cast magic on Monster_" + targetMonster.battleUnitId);
		BattleReportGenerater.getInstance().addEvent(battleUnitId, Math.Round(BattleData.getInstance ().currBattleTime,2), 0, 0, 0, ReportActionType.Cast);
	}
	protected void Report_Hurted(int damage){
		Debug.Log ("[Report] Monster_" + battleUnitId + " being hurted, " + damage + " damage, current hp: " + hp);
		BattleReportGenerater.getInstance().addEvent(battleUnitId, Math.Round(BattleData.getInstance ().currBattleTime,2), damage, 0, 0, ReportActionType.Hurt);
	}
	protected void Report_Healed(int heal){
		Debug.Log ("[Report] Monster_" + battleUnitId + " being healed, " + heal + " heal, current hp: " + hp);
		BattleReportGenerater.getInstance().addEvent(battleUnitId, Math.Round(BattleData.getInstance ().currBattleTime,2), heal, 0, 0, ReportActionType.Heal);
	}
	protected void Report_Die(){
		Debug.Log ("[Report] Monster_" + battleUnitId + " die!");
		BattleReportGenerater.getInstance ().addEvent (battleUnitId, Math.Round(BattleData.getInstance ().currBattleTime,2), 0, 0, 0, ReportActionType.Die);
	}
}