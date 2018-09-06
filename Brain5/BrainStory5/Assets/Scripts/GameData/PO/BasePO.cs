using UnityEngine;
using System.Collections;
using LitJson;

namespace JCFramework{
	public class BasePO
	{
		public int id;

		// 数据第一次创建
		public virtual void init(){
			
		}

		// 读取数据时
		public virtual void afterLoad(){
		
		}

		public void save(bool writeNow = true){
			JsonDB.getInstance ().save (this, writeNow);
		}

		public void delete(bool writeNow = true){
			JsonDB.getInstance ().delete (this, writeNow);
		}

		public virtual void update(bool writeNow = false){
			Debug.LogError (this.GetType());
			JsonDB.getInstance ().update (this, writeNow);
		}

		public bool compareVal(string propName, object propVal){
			var field = this.GetType ().GetField (propName);
			if (field == null) {
				return false;
			}
			object val = field.GetValue (this);
			return val == propVal;
		}

		public string TableName { get { return this.GetType ().Name; }}
	}
}
