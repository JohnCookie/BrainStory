using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public delegate void ShowPageCallback (GameObject page);

public enum PagePositions{
	MainPage,
	CommonPage,
	CommonDialog,
	TopDialog,
	OutOfCamera
}

public class PageObjectRef{
	int _index;
	string _pageName;
	GameObject m_objPage;

	public PageObjectRef(int index, string name, GameObject obj){
		this._index = index;
		this._pageName = name;
		this.m_objPage = obj;
	}

	public int Index{
		get{
			return _index;
		}
		set{
			_index=value;
		}
	}

	public string Name{
		get{
			return _pageName;
		}
		set{
			_pageName=value;
		}
	}

	public GameObject Page{
		get{
			return m_objPage;
		}
	}
}

public class UISystem: MonoBehaviour {
	private static UISystem _instance;

	public Dictionary<string, PageObjectRef> mPageDict = new Dictionary<string, PageObjectRef>();
	public List<PageObjectRef> mPageList = new List<PageObjectRef> ();
	string mCurrPageName = "";

	GameObject m_mainPanel;
	Camera m_mainCamera;

	private UISystem(){
	}

	void Init(){
		mPageList.Clear ();
		mPageDict.Clear ();

		GameObject mainPage = GameObject.Find ("MainPageUI").gameObject;
		PageObjectRef tmpRef = new PageObjectRef (0, "MainPageUI", mainPage);
		mPageList.Add (tmpRef);
		mPageDict.Add ("MainPageUI", tmpRef);

		Debug.Log ("UI System Inited.");
	}

	public static UISystem getInstance(){
		if (_instance == null) {
			_instance=GameObject.Find ("UISystem").gameObject.GetComponent<UISystem>();
			_instance.Init();
		}
		return _instance;
	}

	public GameObject MainPanel {
		get {
			if (m_mainPanel == null) {
				m_mainPanel = GameObject.Find ("MainPanel").gameObject;
			}
			return m_mainPanel;
		}
	}

	public Camera MainCamera {
		get {
			if (m_mainCamera==null) {
				m_mainCamera = GameObject.Find("MainCamera").camera;
			}
			return m_mainCamera;
		}
	}

	public void showPage(string pageName){
		showPage (pageName, null);
	}
	public void showPage(string pageName, ShowPageCallback callback){
		GameObject targetPage=null;
		GameObject resRetObj=null;
		if (isPageLoaded (pageName)) {
			Debug.LogError ("The Page Is Loaded Already");
			int indexOfPage = mPageDict[pageName].Index;
			switchPageToTopByIndex(indexOfPage);
			return;	
		} else {
			ResourceSystem.getInstance().loadRes(pageName, delegate(Object obj) {
				resRetObj = obj as GameObject;
				targetPage = (GameObject) Instantiate(resRetObj);
				targetPage.name = getNameFromPath(pageName);
				targetPage.transform.parent = MainPanel.transform;
				targetPage.transform.localPosition = getPageLocation(PagePositions.CommonPage);
				targetPage.transform.localScale = Vector3.one;

				mCurrPageName = pageName;

				PageObjectRef tmpObj = new PageObjectRef(mPageList.Count, pageName, targetPage);
				mPageDict.Add(pageName, tmpObj);
				mPageList.Add(tmpObj);

				// hide last page
				hidePageAtIndex(mPageList.Count-2);
			});
		}

		if (callback != null) {
			callback(targetPage);	
		}
	}

	public void showMainPage(){
		// clear all page
		for (int i=mPageList.Count-1; i>0; i--) {
			removePageAtIndex(i);
		}
	}

	public void showLastPage(){
		removePageAtIndex (mPageList.Count - 1);
	}

	private void removePageAtIndex(int _index){
		if (_index >= mPageList.Count || _index<0) {
			Debug.LogWarning("Beyond List Count Index!!!");
			return;
		}
		if (_index == 0) {
			Debug.LogWarning("Only 1 Page Remaining, can't remove");
			return;
		}
		Destroy (mPageList [_index].Page);
		string removePageName = mPageList [_index].Name;
		mPageDict.Remove (removePageName);
		mPageList.RemoveAt (_index);

		showPageAtIndex (_index - 1);
	}

	private void showPageAtIndex(int _index){
		mCurrPageName = mPageList [_index].Name;
		mPageList [_index].Page.SetActive (true);
	}

	private void hidePageAtIndex(int _index){
		mPageList [_index].Page.SetActive (false);
	}

	private void switchPageToTopByIndex(int _index){
		PageObjectRef refToSwitch = mPageList [_index];

		for (int i=_index; i<mPageList.Count-2; i++) {
			PageObjectRef tmpRef = mPageList[i+1];
			string movedPageName = tmpRef.Name;
			tmpRef.Index = i;
			mPageList[i] = tmpRef;

			PageObjectRef tmpRefInDic = mPageDict[movedPageName];
			tmpRefInDic.Index = i;
			mPageDict[movedPageName]=tmpRefInDic;

			tmpRef.Page.SetActive(false);
		}

		refToSwitch.Index = mPageList.Count - 1;
		mPageList [mPageList.Count - 1] = refToSwitch;
		mPageList [mPageList.Count - 1].Page.SetActive (true);
	}

	private bool isPageLoaded(string pageName){
		if (mPageDict.ContainsKey (pageName)) {
			return true;
		}
		return false;
	}

	private string getNameFromPath(string path){
		string[] strArr = path.Split ('/');
		return strArr [strArr.Length - 1];
	}

	private Vector3 getPageLocation(PagePositions type){
		Vector3 v3;
		switch (type) {
		case PagePositions.MainPage:
			v3 = new Vector3(0, 0, -200);
			break;
		case PagePositions.CommonPage:
			v3 = new Vector3(0, 0, -500);
			break;
		case PagePositions.CommonDialog:
			v3 = new Vector3(0, 0, -800);
			break;
		case PagePositions.TopDialog:
			v3 = new Vector3(0, 0, -1000);
			break;
		case PagePositions.OutOfCamera:
			v3 = new Vector3(0, 0, 10000);
			break;
		default:
			v3 = new Vector3(0, 0, 0);
			break;
		}
		return v3;
	}

	private int getStoredPageIndex(string pageName){
		return 0;
	}
}
