using UnityEngine;
using System.Collections;

public class MapTileObj : MonoBehaviour
{
	public UISprite m_spriteTileBg;
	public PathFinding2DTest player;

	string[] tileNameArr = {"ground8", "ground6", "ground3", "ground4", "ground5"};

	public void Init(int type){
		Debug.Log (type);
		m_spriteTileBg.spriteName = tileNameArr [type];
		if (type == 0) {
			gameObject.layer=8;
//			gameObject.layer = LayerMask.NameToLayer("Ground");
//			for(int i=0; i<transform.childCount; i++){
//				transform.GetChild(i).gameObject.layer = LayerMask.NameToLayer("Ground");
//			}
		} else {
			gameObject.layer=9;
//			gameObject.layer = LayerMask.NameToLayer("Obstacle");
//			for(int i=0; i<transform.childCount; i++){
//				transform.GetChild(i).gameObject.layer = LayerMask.NameToLayer("Obstacle");
//			}
		}
	}
	
	public void Clicked(){
		player.tryMoveTo (transform.position);
	}
}

