using UnityEngine;
using System.Collections;
using System.Collections.Generic;

// Base attack type
public enum UnitWeaponType{
	Sharp,
	Blunt,
	Arrow,
	Magic
}

// Base enchant type
public enum UnitAttackEnchant{
	NoEnchant,
	Fire,
	Ice,
	Thunder,
	Natural,
	Holy,
	Curse,
	Poison
}

// Base defence type
public enum UnitArmorType{
	NoArmor,
	LightArmor,
	HeavyArmor,
	EnhancedArmor
}

public class UnitData
{
	public int unit_id{ get; set;}
	public List<int> talents{ get; set;}
	public List<int> skills{ get; set;}
	public int exp{ get; set;}
}

