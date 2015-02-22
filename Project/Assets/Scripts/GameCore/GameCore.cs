using UnityEngine;
using System.Collections;

public class GameCore : MonoBehaviour {

	void Start () {
		UISystem.getInstance ();
		ResourceSystem.getInstance ();
		TextUtils.getInstance();
	}

}
