using UnityEngine;
using System.Collections;
using JCFramework;

[System.Serializable]
public class CardPO:BasePO
{
	public int dataId;
	public int owner; // 属于哪个角色

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

