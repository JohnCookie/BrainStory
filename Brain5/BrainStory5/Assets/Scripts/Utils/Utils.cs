using UnityEngine;
using System.Collections;

namespace JCFramework{
	public class Utils
	{
		public static string getAppPath(string fileName)
		{
			string assetPath = "";
			#if UNITY_EDITOR
			assetPath = string.Format(@"Assets/StreamingAssets/{0}", fileName);
			#elif UNITY_ANDROID
			assetPath = "jar:file://" + Application.dataPath + "!/assets/" + fileName;
			#elif UNITY_IOS
			assetPath = Application.dataPath + "/Raw/" + fileName;
			#else
			assetPath = Application.streamingAssetsPath + "/" + fileName;
			#endif
			return assetPath;
		}
	}
}
