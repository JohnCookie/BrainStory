﻿using UnityEngine;
using System.Collections;
using JCFramework;

[System.Serializable]
public class CharacterPO:BasePO
{
	public int dataId;
	public string img;
	public string name;
	public int exp;
	public int job;
	public int attrSTA;
	public int attrAGI;
	public int attrINT;
	public int attrSPR;
	public int attrVIT;
	public int attrLUC;
	public int[] cards;

	public override void init ()
	{
		base.init ();
	}

	public override void afterLoad ()
	{
		base.afterLoad ();
	}

	public override void update (bool writeNow = false)
	{
		base.update (writeNow);
	}
}

