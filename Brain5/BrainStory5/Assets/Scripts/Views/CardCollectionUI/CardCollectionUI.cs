using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CardCollectionUI : BaseView
{
	public Transform contentRoot;
	public GameObject cardItemPrefab;

	public override void InitOnAwake ()
	{
		base.InitOnAwake ();
		// 读取玩家数据
	}

	public override void RefreshView ()
	{
		base.RefreshView ();
		// 刷新玩家列表
		for(int i=0; i<contentRoot.childCount; i++){
			Destroy (contentRoot.GetChild (i).gameObject);
		}
		contentRoot.DetachChildren ();
//		for (int i = 0; i < CharacterController.getInstance ().AllCharacterList.Count; i++) {
//			GameObject obj = Instantiate (charItemPrefab) as GameObject;
//			CharacterHeadItem chi = obj.GetComponent<CharacterHeadItem> ();
//			chi.Init(CharacterController.getInstance().AllCharacterList[i]);
//			obj.transform.SetParent (contentRoot);
//			obj.transform.localPosition = Vector3.zero;
//			obj.transform.localScale = Vector3.one;
//			obj.name = "CharItem" + i;
//		}
	}


}

