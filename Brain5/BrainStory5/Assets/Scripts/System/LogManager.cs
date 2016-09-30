using UnityEngine;
using System.Collections;

namespace JCFramework{
	public enum LogLevel{
		Information,
		Warning,
		Error
	}

	public class LogManager : JCMonoSingleton<LogManager> {
		private bool printLog = true;

		private LogManager(){
			
		}

		public void Log(string log, LogLevel level = LogLevel.Information){
			if (printLog) {
				switch (level) {
				case LogLevel.Information:
					Debug.Log (log);
					break;
				case LogLevel.Warning:
					Debug.LogWarning (log);
					break;
				case LogLevel.Error:
					Debug.LogError (log);
					break;
				}
			}
		}
	}
}
