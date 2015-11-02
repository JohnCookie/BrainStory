using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using LitJson;

public class TaskDataUtility
{
	private static TaskDataUtility _instance = null;
	
	AllTaskData m_taskBaseInfoList = null;
	Dictionary<int, List<TaskData>> m_taskRarityDict = new Dictionary<int, List<TaskData>>();
	Dictionary<int, TaskData> m_taskDict = new Dictionary<int, TaskData>();

	private TaskDataUtility(){
		m_taskBaseInfoList = JsonMapper.ToObject<AllTaskData>(TextUtils.getInstance ().ReadTextFromResources ("Texts/GameData/monster_base"));
		if (m_taskBaseInfoList != null) {
			Init ();
		} else {
			Debug.Log("Texts/GameData/monster_base read error");
		}
	}
	public static TaskDataUtility getInstance(){
		if (_instance == null) {
			_instance = new TaskDataUtility();
		}
		return _instance;
	}

	void Init(){
		m_taskRarityDict.Clear ();
		m_taskDict.Clear ();
		for(int i=0; i<m_taskBaseInfoList.data.Count; i++){
			if(m_taskDict.ContainsKey(m_taskBaseInfoList.data[i].id)){
				m_taskDict[m_taskBaseInfoList.data[i].id]=m_taskBaseInfoList.data[i];
			}else{
				m_taskDict.Add(m_taskBaseInfoList.data[i].id, m_taskBaseInfoList.data[i]);
			}

			if(m_taskDict.ContainsKey(m_taskBaseInfoList.data[i].task_rarity)){
				List<TaskData> td = m_taskRarityDict[m_taskBaseInfoList.data[i].task_rarity];
				td.Add(m_taskBaseInfoList.data[i]);
				m_taskRarityDict[m_taskBaseInfoList.data[i].task_rarity] = td;
			}else{
				List<TaskData> td = new List<TaskData>();
				td.Add(m_taskBaseInfoList.data[i]);
				m_taskRarityDict.Add(m_taskBaseInfoList.data[i].task_rarity, td);
			}
		}
	}
}

