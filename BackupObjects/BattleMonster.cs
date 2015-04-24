using System;
using System.Collections.Generic;
using UnityEngine;
/*
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
*/
public class BattleMonster
{
	public int battleUnitId;
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
	public TeamType team; // 1 right 2 left
	public MonsterStatus status = MonsterStatus.Idle;
	public BattleMonster targetMonster = null;

	private double attackIntervalAddUp = 0;

	public BattleMonster (UserMonster _monster, Vector2 _pos, TeamType _team){
		battleUnitId = BattleData.getInstance ().getBattleMonsterId ();
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

		Report_Born ();
	}

	public void calculateAddition(){
		_calculateTeamAddition ();
		_calculateTalentAddition ();
		_calculateSkillAddition ();
	}

	public void setStatus(MonsterStatus _status){
		status = _status;
		Report_Status_Change ();
	}

	public void updatePosition(){
		if (status == MonsterStatus.Moving) {
			monsterRealPosX += movingX * GameConfigs.battle_tick_step;
			monsterRealPosY += movingY * GameConfigs.battle_tick_step;
			checkingNewPosition ();
		}
	}

	public void updateSelfAction(){
		switch (status) {
		case MonsterStatus.Idle:
			search();
			break;
		case MonsterStatus.Prepared:
			if(checkTargetInRange()){
				Debug.Log("Monster_"+battleUnitId+" detect: target in range");
				setStatus(MonsterStatus.Attacking);
			}else{
				search();
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
				attack ();
			}else{
				Debug.Log("Monster_"+battleUnitId+" detect: target die");
				setStatus(MonsterStatus.Prepared);
			}
			break;
		case MonsterStatus.Dead:
			die();
			break;
		}
	}

	public virtual void search(){
		Vector2 searchResult = BattleMapUtil.getNextStepPosition (this);
		if (this.monsterIndexX == (int)searchResult.x && this.monsterIndexY == (int)searchResult.y) {
			// same position
			setStatus(MonsterStatus.Prepared);
		} else {
			setStatus(MonsterStatus.Moving);
			updateTargetIndex ((int)searchResult.x, (int)searchResult.y);
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

	public void attack(){
		attackIntervalAddUp += GameConfigs.battle_tick_step;
		if (attackIntervalAddUp >= atkSpd) {
			if (skills.Count > 0) {
				double cast_percent = intel * GameConfigs.intel_per_cast > GameConfigs.cast_max_percent ? GameConfigs.cast_max_percent : intel * GameConfigs.intel_per_cast;
				if (UnityEngine.Random.Range (0.0f, 1.0f) < cast_percent) {
					_cast ();
				} else {
					_fight ();
				}
			} else {
				_fight ();
			}
			attackIntervalAddUp -= atkSpd;
		}
	}

	protected virtual void _fight(){
		Report_Fight ();
	}

	protected virtual void _cast(){
		Report_Cast ();
	}
	
	public virtual void beHurted(int damage){
		Report_Hurted (damage);
	}

	public virtual void beHealed(int heal){
		Report_Healed (heal);
	}

	public virtual void updateDotDebuff(){
		//Debug.Log ("Monster_" + battleUnitId + " calculate impact by Dot/Debuff");
	}

	public virtual void updateHotBuff(){
		//Debug.Log ("Monster_" + battleUnitId + " calculate impact by Hot/Buff");
	}

	public virtual void die(){
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
	}

	void _calculateTeamAddition(){

	}

	void _calculateTalentAddition(){

	}

	void _calculateSkillAddition(){

	}

	void updateIndex(int newX, int newY){
		if (BattleData.getInstance ().battleMapData [monsterIndexX, monsterIndexY] == (int)MapTileType.Monster) {
			BattleData.getInstance ().battleMapData [monsterIndexX, monsterIndexY] = (int)MapTileType.None;
		}
		monsterIndexX = newX;
		monsterIndexY = newY;
		BattleData.getInstance().battleMapData[monsterIndexX, monsterIndexY] = (int)MapTileType.Monster;
	}

	void updateTargetIndex(int newX, int newY){
		if (BattleData.getInstance ().battleMapData [monsterTargetIndexX, monsterTargetIndexY] == (int)MapTileType.Occupied) {
			BattleData.getInstance ().battleMapData [monsterTargetIndexX, monsterTargetIndexY] = (int)MapTileType.None;
		}
		monsterTargetIndexX = newX;
		monsterTargetIndexY = newY;
		BattleData.getInstance ().battleMapData [monsterTargetIndexX, monsterTargetIndexY] = (int)MapTileType.Occupied;
		Debug.Log("Monster_"+ battleUnitId +" start move towards ("+monsterTargetIndexX+","+monsterTargetIndexY+")");
	}

	void checkingNewPosition(){
		int newMonsterIndexX = (int)BattleMapUtil.PositionInVector (monsterRealPosX, monsterRealPosY).x;
		int newMonsterIndexY = (int)BattleMapUtil.PositionInVector (monsterRealPosX, monsterRealPosY).y;
		if (newMonsterIndexX != monsterIndexX || newMonsterIndexY != monsterIndexY) {
			updateIndex(newMonsterIndexX,newMonsterIndexY);	
		}
	}

	bool checkIsArrived(){
		if (this.monsterIndexX == this.monsterTargetIndexX && this.monsterIndexY == this.monsterTargetIndexY) {
			return true;	
		} else {
			return false;
		}
	}

	bool checkTargetInRange(){
		if (targetMonster != null && BattleMapUtil.getDistanceBetween2Monster (this, targetMonster) <= this.range) {
			return true;
		} else {
			return false;
		}
	}

	bool checkTargetIsAlive(){
		if (targetMonster.hp > 0) {
			return true;	
		} else {
			return false;
		}
	}

	// generate report interface
	void Report_Born(){
		Debug.Log("[Report] Monster_"+battleUnitId+" born at ("+monsterIndexX+","+monsterIndexY+").");
		BattleReportGenerater.getInstance ().addEvent (battleUnitId, BattleData.getInstance ().currBattleTime, monsterIndexX, monsterIndexY, ReportActionType.Locate);
	}
	void Report_Status_Change(){
		Debug.Log ("[Report] Monster_" + battleUnitId + " switch status to " + status);
		if (status == MonsterStatus.Moving) {
			BattleReportGenerater.getInstance().addEvent(battleUnitId, BattleData.getInstance().currBattleTime, monsterTargetIndexX, monsterTargetIndexY, ReportActionType.Move);
		}
	}
	void Report_Fight(){
		Debug.Log ("[Report] Monster_" + battleUnitId + " fight on Monster_" + targetMonster.battleUnitId);
		BattleReportGenerater.getInstance().addEvent(battleUnitId, BattleData.getInstance().currBattleTime, 0, 0, ReportActionType.Attack);
	}
	void Report_Cast(){
		Debug.Log ("[Report] Monster_" + battleUnitId + " cast magic on Monster_" + targetMonster.battleUnitId);
		BattleReportGenerater.getInstance().addEvent(battleUnitId, BattleData.getInstance().currBattleTime, 0, 0, ReportActionType.Cast);
	}
	void Report_Hurted(int damage){
		Debug.Log ("[Report] Monster_" + battleUnitId + " being hurted, " + damage + " damage, current hp: " + hp);
		BattleReportGenerater.getInstance().addEvent(battleUnitId, BattleData.getInstance().currBattleTime, damage, 0, ReportActionType.Hurt);
	}
	void Report_Healed(int heal){
		Debug.Log ("[Report] Monster_" + battleUnitId + " being healed, " + heal + " heal, current hp: " + hp);
		BattleReportGenerater.getInstance().addEvent(battleUnitId, BattleData.getInstance().currBattleTime, heal, 0, ReportActionType.Heal);
	}
	void Report_Die(){
		Debug.Log ("[Report] Monster_" + battleUnitId + " die!");
		BattleReportGenerater.getInstance ().addEvent (battleUnitId, BattleData.getInstance ().currBattleTime, 0, 0, ReportActionType.Die);
	}
}
