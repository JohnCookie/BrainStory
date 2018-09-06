using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using System.Threading;
using System.Reflection;

namespace JCFramework{
	public class JsonDB:JCSingleton<JsonDB>
	{
		public const string FILE_SUFFIX = ".json";
		public const string ENCRYPT_SUFFIX = ".byte";
		public const int SAVE_DIGIT_NUM = 2;

		public bool useThread = false;
		private object _lock;

		private Thread _thread;
		private bool _save2Db; //同步写入数据库的标记

		private List<BasePO> _cacheUpdates; //更新时 原数据库的备份
		private List<BasePO> _cacheSaves; //延迟存储时，需要删除的放在缓存中的备份

		private Dictionary<string, ITableInfo> _tableInfos; //json数据持久化使用
		private Dictionary<string, Dictionary<int, BasePO>> _allPOs; //从数据库取出的PO缓存

		private JsonDB(){
			_allPOs = new Dictionary<string, Dictionary<int, BasePO>> ();
			_tableInfos = new Dictionary<string, ITableInfo> ();
			_cacheSaves = new List<BasePO> ();
			_cacheUpdates = new List<BasePO> ();
			_lock = new object ();

			if (useThread) {
				_save2Db = false;
				_thread = new Thread (dealingDB);
				_thread.Start ();
			}
		}

		// 获取类名
		private string getTblName<T>() where T:BasePO{
			return typeof(T).Name;
		}

		// 获取该类的数据库表
		private TableInfo<T> getTblInfo<T>() where T:BasePO{
			string key = getTblName<T> ();
			if (!_tableInfos.ContainsKey (key)) {
				if (typeof(T).Name == typeof(BasePO).Name)
					Debug.LogError ("create base table Json is not allowed");
				TableInfo<T> info = new TableInfo<T> ();
				_tableInfos.Add (key, info);
			}
			return _tableInfos [key] as TableInfo<T>;
		}

		// 获取该类表的字典
		private Dictionary<int, BasePO> getDict<T>() where T:BasePO{
			string key = getTblName<T> ();
			if (!_allPOs.ContainsKey (key)) {
				Dictionary<int, BasePO> pos = new Dictionary<int, BasePO> ();
				_allPOs.Add (key, pos);
			}
			return _allPOs [key];
		}

		// 获取指定类的字典
		public Dictionary<int, BasePO> getDict<T>(T t) where T : BasePO{
			string key = t.GetType ().Name;
			if (_allPOs.ContainsKey (key)) {
				return _allPOs [key];
			}
			return null;
		}

		// 获取指定类的数据库表
		private ITableInfo getTblInfo2<T>(T t) where T:BasePO{
			var obj = getRefectResult (t, "getTblInfo");
			return obj as ITableInfo;
		}

		// 将新创建的PO加入缓存
		private void add2Dict<T>(T t) where T:BasePO{
			var tbl = getTblInfo<T> ();
			if (t.id == 0)
				t.id = ++tbl.MaxId;
			tbl.Tables.Add (t);
			var dict = getDict<T> ();
			if (!dict.ContainsKey (t.id)) {
				dict.Add (t.id, t);
			}
		}

		// 将表的写库标志位置为true
		private ITableInfo markFlag<T>(T t) where T:BasePO{
			var tbl = getTblInfo2<T> (t);
			tbl.Write2Disk = true;
			return tbl;
		}

		// 存库
		private void writeAll(){
			foreach (var o in _tableInfos.Values) {
				o.saveFile ();
			}
			_cacheSaves.Clear ();
			_cacheUpdates.Clear ();
		}

		private object getRefectResult<T>(T t, string methodName) where T:BasePO{
			var methodInfo = typeof(JsonDB).GetMethod (methodName, BindingFlags.NonPublic | BindingFlags.Instance);
			var genericMethod = methodInfo.MakeGenericMethod (t.GetType ());
			var obj = genericMethod.Invoke (JsonDB.getInstance(), new object[]{ });
			return obj;
		}

		private void saveFile(BasePO t, ITableInfo info){
			if (_cacheSaves.Contains (t)) {
				_cacheSaves.Remove (t);
			}
			if (_cacheUpdates.Contains (t)) {
				_cacheUpdates.Remove (t);
			}
			info.saveFile ();
		}

		public bool existTable<T>() where T:BasePO{
			return _allPOs.ContainsKey (getTblName<T> ());
		}

		public void createTable<T>() where T:BasePO{
			string tblName = getTblName<T> ();
			if(_allPOs.ContainsKey(tblName)){
				return;
			}
			var dict = getDict<T> ();
			var tblInfo = getTblInfo<T> ();
			tblInfo.readFile ();
			int maxId = 0;
			for (var i = 0; i < tblInfo.Tables.Count; i++) {
				var po = tblInfo.Tables [i];
				if (po.id > maxId) {
					maxId = po.id;
				}
				po.afterLoad ();
				dict.Add (po.id, po);
			}
			tblInfo.MaxId = maxId;
		}

		public void copyBackUp(){
			string folderPath = FileManager.creatDataBackUpFolder ();
			Type[] poTypes = TypeUtils.GetAssignableTypes (typeof(BasePO)); // 获取所有自动生成的配置class
			if(poTypes!=null){
				for (int i = 0; i < poTypes.Length; i++) {
					Type t = poTypes [i];
					var fileName = t.Name+JsonDB.ENCRYPT_SUFFIX;
					string path = FileManager.getFilePath (fileName);
					if (System.IO.File.Exists (path)) {
						System.IO.File.Copy (path, folderPath + "/" + fileName, true);
					}
				}
			}
		}

		public void deleteAll(Action callback){
			Type[] poTypes = TypeUtils.GetAssignableTypes (typeof(BasePO)); // 获取所有自动生成的配置class
			if(poTypes!=null){
				for (int i = 0; i < poTypes.Length; i++) {
					Type t = poTypes [i];
					var fileName = t.Name + JsonDB.FILE_SUFFIX;
					FileManager.delete (fileName);
					fileName = t.Name + JsonDB.ENCRYPT_SUFFIX;
					FileManager.delete (fileName);
				}
			}
			_allPOs.Clear ();
			_tableInfos.Clear ();
			_cacheSaves.Clear ();
			_cacheUpdates.Clear ();
			if (callback != null) {
				callback ();
			}
		}

		public void deletaTable<T>() where T:BasePO{
			if (existTable<T> ()) {
				var tblName = getTblName<T> ();
				_allPOs.Remove (tblName);
				_tableInfos.Remove (tblName);
				FileManager.delete (tblName);
			}
		}

		public T create<T>(int pk = 0) where T:BasePO,
		new(){
			if (pk > 0) {
				var dict = getDict<T> ();
				if (dict.ContainsKey (pk)) {
					return dict [pk] as T;
				}
			}
			T t = new T ();
			t.id = pk;
			t.init ();
			save (t, false);
			return t;
		}

		public T get<T>(int pk, E_WriteFlag flag = E_WriteFlag.NotWrite) where T:BasePO,
		new(){
			var dict = getDict<T> ();
			if (dict.ContainsKey (pk)) {
				return dict [pk] as T;
			}
			if (flag != E_WriteFlag.NotWrite) {
				T t = create<T> (pk);
				if (flag == E_WriteFlag.WriteImmediate) {
					write2DB ();
					return t;
				}
			}
			return null;
		}

		public List<T> getAll<T>() where T:BasePO{
			var tblInfo = getTblInfo<T> ();
			List<T> list = new List<T> ();
			for (var i = 0; i < tblInfo.Tables.Count; i++) {
				list.Add ((T)tblInfo.Tables [i]);
			}
			return list;
		}

		public void write2DB(){
			if (useThread) {
				lock (_lock) {
					_save2Db = true;
				}
			} else {
				writeAll ();
			}
		}

		public void deleteCache(){
			foreach (var p in _cacheSaves) {
				string key = p.GetType ().Name;
				if (_tableInfos.ContainsKey (key)) {
					var info = _tableInfos [key];
					info.Tables.Remove (p);
					info.Write2Disk = false;
					Dictionary<int, BasePO> pos = _allPOs [key];
					if (pos.ContainsKey (p.id)) {
						pos.Remove (p.id);
					}
				}
			}
			foreach (var p in _cacheUpdates) {
				string key = p.GetType ().Name;
				if (_tableInfos.ContainsKey (key)) {
					var tblInfo = _tableInfos [key];
					Dictionary<int, BasePO> dict = _allPOs [key];
					dict.Clear ();
					tblInfo.readFile ();
					int maxId = 0;
					for (var i = 0; i < tblInfo.Tables.Count; i++) {
						var po = tblInfo.Tables [i];
						if (po.id > maxId) {
							maxId = po.id;
						}
						po.afterLoad ();
						dict.Add (po.id, po);
					}
					tblInfo.MaxId = maxId;
				}
			}
			_cacheSaves.Clear ();
			_cacheUpdates.Clear ();
		}

		public void save<T>(T t, bool writeNow) where T:BasePO{
			add2Dict (t);
			var info = markFlag (t);
			if (writeNow) {
				saveFile (t, info);
			} else {
				if (!_cacheSaves.Contains (t)) {
					_cacheSaves.Add (t);
				}
			}
		}

		public void update<T>(T t, bool writeNow = true) where T:BasePO{
			add2Dict (t);
			var info = markFlag (t);
			if (writeNow) {
				saveFile (t, info);
			} else {
				if (!_cacheSaves.Contains (t)) {
					_cacheSaves.Add (t);
				}
			}
		}

		public void delete<T>(int id, bool save2Db = true) where T:BasePO{
			var dict = getDict<T> ();
			if (dict.ContainsKey (id)) {
				T p = (T)dict [id];
				delete (p, save2Db);
			}
		}

		public void delete<T>(T t, bool save2Db = true) where T:BasePO{
			var tblInfo = markFlag (t);
			var dict = getDict (t);
			if (dict.ContainsKey (t.id)) {
				dict.Remove (t.id);
				tblInfo.Tables.Remove (t);
			}
			if (_cacheSaves.Contains (t)) {
				_cacheSaves.Remove (t);
			}
			if (_cacheUpdates.Contains (t)) {
				_cacheUpdates.Remove (t);
			}
			for (var i = tblInfo.Tables.Count - 1; i >= 0; i++) {
				var info = tblInfo.Tables [i];
				if (info.id == t.id) {
					tblInfo.Tables.Remove (info);
					break;
				}
			}
			if (save2Db) {
				tblInfo.Write2Disk = true;
				tblInfo.saveFile ();
			}
		}

		public T find<T>(string propName, object propVal) where T:BasePO{
			var info = getTblInfo<T> ();
			foreach (var po in info.Tables) {
				if (po.compareVal (propName, propVal)) {
					return po as T;
				}
			}
			return null;
		}

		public List<T> findList<T>(string propName, object propVal) where T:BasePO{
			var info = getTblInfo<T> ();
			var list = new List<T> ();
			foreach (var po in info.Tables) {
				if (po.compareVal (propName, propVal)) {
					list.Add ((T)po);
				}
			}
			return list;
		}

		// 在另一个线程处理数据库
		private void dealingDB(){
			while (true) {
				lock (_lock) {
					if (_save2Db) {
						writeAll ();
						_save2Db = false;
					}
				}
				Thread.Sleep (1000);
			}
		}
	}

	interface ITableInfo{
		List<BasePO> Tables { get; set; }
		int MaxId { get; set; }
		bool Write2Disk { get; set; }
		void saveFile();
		void readFile();
	}

	class TableInfo<T> : ITableInfo where T : BasePO{
		string _tblName;
		bool _writeFlag;

		public TableInfo(){
			Tables = new List<BasePO> ();
			Write2Disk = false;
			_tblName = typeof(T).Name + JsonDB.FILE_SUFFIX;
		}

		public void saveFile(){
			if (Write2Disk) {
				var tables = new List<T> ();
				for (int i = 0; i < Tables.Count; i++) {
					tables.Add ((T)Tables [i]);
				}
				var jsonTbl = new SerializeList<T> (tables);
				var data = JsonUtility.ToJson (jsonTbl);
				// 加密或不加密 先按不加密处理
				FileManager.writeFile(_tblName, data);
				Write2Disk = false;     
			}
		}

		public void readFile(){
			string data = FileManager.readFile (_tblName);
			if (data == null) {
				FileManager.createFile (_tblName);
			} else {
				var sl = JsonUtility.FromJson<SerializeList<T>> (data);
				if (sl.Targets != null) {
					Tables.Clear ();
					for (int i = 0; i < sl.Targets.Count; i++) {
						Tables.Add (sl.Targets [i]);
					}
				}
			}
			Write2Disk = false;
		}

		void formatFloat(){
			var t = typeof(T);
			var fields = t.GetFields (BindingFlags.Public | BindingFlags.Instance);
			foreach (FieldInfo f in fields) {
				if (f.FieldType == typeof(float)) {
					for (var i = Tables.Count - 1; i >= 0; i--) {
						var po = Tables [i];
						var val = f.GetValue (po);
						float fVal = (float)val;
						int iVal = (int)(fVal*100);
						float newVal = iVal / 100;
						f.SetValue (po, newVal);
					}
				}
			}
		}

		public List<BasePO> Tables { get; set; }
		public int MaxId { get; set; }
		public bool Write2Disk{
			get { return _writeFlag; }
			set { _writeFlag = value; }
		}
	}

	// writedelay是先创建到内存,延迟写库 writeimmediate是立即写库
	public enum E_WriteFlag{
		NotWrite,
		WriteImmediate,
		WriteDelay,
	}

	// 下面是可序列化的列表和可序列化的字典
	public class SerializeList<T>{
		[SerializeField]
		List<T> _targets;

		public List<T> Targets { get { return _targets; }}

		public SerializeList(List<T> targets){
			_targets = targets;
		}
	}

	[Serializable]
	public class SerializeDict<TKey, TValue>: ISerializationCallbackReceiver
	{
		[SerializeField]
		List<TKey> keys;
		[SerializeField]
		List<TValue> values;

		Dictionary<TKey, TValue> _tables;
		public Dictionary<TKey, TValue> Tables{
			get { return _tables; }
		}

		public SerializeDict(Dictionary<TKey, TValue> targets){
			_tables = targets;
		}

		public void OnBeforeSerialize(){
			keys = new List<TKey> (_tables.Keys);
			values = new List<TValue> (_tables.Values);
		}

		public void OnAfterDeserialize(){
			var count = Mathf.Min (keys.Count, values.Count);
			_tables = new Dictionary<TKey, TValue> (count);
			for (var i = 0; i < count; i++) {
				_tables.Add (keys [i], values [i]);
			}
		}
	}
}
