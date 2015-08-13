using UnityEngine;
using System.Collections;

public class MapTile : MonoBehaviour {
	public PathFinding2DTest player;

	public void Clicked(){
		player.tryMoveTo (transform.position);
	}
}
