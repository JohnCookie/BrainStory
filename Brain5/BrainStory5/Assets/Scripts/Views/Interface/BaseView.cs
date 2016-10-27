using UnityEngine;
using System.Collections;

public class BaseView : MonoBehaviour
{
	void Awake(){
		InitOnAwake ();
	}

	void Start(){
	
	}

	void OnEnable(){
		RefreshView ();
	}

	void OnDisable(){
		OnDisableView ();
	}

	void OnDestroy(){
		Release ();
	}

	public virtual void InitOnAwake(){}
	public virtual void RefreshView(){}
	public virtual void OnDisableView(){}
	public virtual void Release(){}

}

