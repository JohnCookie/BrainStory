using UnityEngine;
using System.Collections;

public class GameCore : MonoBehaviour {
	UISystem uiSystem;
	ResourceSystem resourceSystem;

	void Start () {
		uiSystem = GameObject.Find ("UISystem").gameObject.GetComponent<UISystem>();
		resourceSystem = GameObject.Find ("ResourceSystem").gameObject.GetComponent<ResourceSystem> ();

		UISystem.getInstance ();
		ResourceSystem.getInstance ();
	}

}
