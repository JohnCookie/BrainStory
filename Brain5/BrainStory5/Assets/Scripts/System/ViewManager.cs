using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace JCFramework{
	public class ViewInfo{
		public string name;
		public GameObject obj;
		public int index;
		public ViewInfo(string name, GameObject obj, int index){
			this.name = name;
			this.obj = obj;
			this.index = index;
		}
	}

	public class ViewManager : JCMonoSingleton<ViewManager> {
		string prefixPath = "Prefab/View/";
		string mainViewName = "MainView";
		private Dictionary<string, ViewInfo> m_viewDict;
		private List<ViewInfo> m_viewList;
		private ViewInfo currView = null;
		private GameObject viewRoot;

		private ViewManager(){
			m_viewDict = new Dictionary<string, ViewInfo> ();
			m_viewList = new List<ViewInfo> ();
			viewRoot = GameObject.Find ("ViewRoot");
		}

		public void ShowView(string name){
			if (currView.name.Equals(name)) {
				LogManager.getInstance ().Log ("View is already showed");
				return;
			}
			if (m_viewDict.ContainsKey (name)) {
				ViewInfo info = m_viewDict [name];
				int index = info.index;
				for (int i = m_viewList.Count - 1; i > index; i--) {
					m_viewList [i].index -= 1;
				}
				currView.obj.SetActive (false);
				m_viewList.Remove (info);
				info.index = m_viewList.Count;
				m_viewList.Add (info);
				info.obj.SetActive (true);
				currView = info;
			}else{
				GameObject view = Instantiate (ResourceManager.getInstance ().getPrefab (prefixPath + name)) as GameObject;
				ViewInfo info = new ViewInfo (name, view, m_viewList.Count);
				view.name = name;
				view.transform.parent = viewRoot.transform;
				view.transform.localScale = Vector3.one;
				view.transform.localPosition = Vector3.zero;

				m_viewDict.Add (name, info);
				m_viewList.Add (info);

				currView = info;
			}
		}

		public void ShowLastView(){
			if (m_viewList.Count <= 1) {
				LogManager.getInstance ().Log ("Can't go back. It is the last view.");
				return;
			}
			if (currView.name.Equals (mainViewName)) {
				LogManager.getInstance ().Log ("MainView can't go back.");
				return;
			}
			m_viewDict.Remove (currView.name);
			m_viewList.Remove (currView);
			Destroy (currView.obj);
			currView = m_viewList [m_viewList.Count - 1];
			currView.obj.SetActive (true);
		}
	}
}