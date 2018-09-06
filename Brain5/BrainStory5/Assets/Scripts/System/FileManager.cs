using UnityEngine;
using System.Collections;
using System.IO;
using System;
using System.Text;
using System.Runtime.Serialization.Formatters.Binary;

namespace JCFramework{
	public class FileManager
	{
		public static string getFilePath(string fileName)
		{
			#if UNITY_EDITOR
			string filePath = string.Format(@"Assets/StreamingAssets/{0}", fileName);
			#else
			string filePath = string.Format("{0}/{1}", Application.persistentDataPath, fileName);
			if (!File.Exists(filePath))
			{
				#if UNITY_ANDROID
				WWW loadDb = new WWW("jar:file://" + Application.dataPath + "!/assets/" + fileName); // this is the path to your StreamingAssets in android
				while (!loadDb.isDone) { } // CAREFUL here, for safety reasons you shouldn't let this while loop unattended, place a timer and error check
				File.WriteAllBytes(filePath, loadDb.bytes);
				#else
				string loadDb = Utils.getAppPath(fileName);
				if (File.Exists(loadDb))
				{
				File.Copy(loadDb, filePath);
			}
			#endif
		}
			#endif
			return filePath;
		}

		public static void writeLogFile(string msg)
		{
			string date = DateTime.Now.ToString();
			string logMsg = "[ " + DateTime.Now.ToString() + " ] " + msg;
			string logPath = Application.dataPath + "/gameLog";
			if (File.Exists(logPath))
			{
				StreamWriter sw = File.AppendText(logPath);
				sw.WriteLine(logMsg);
				sw.Close();
			}
			else
			{
				StreamWriter sw = File.CreateText(logPath);
				sw.WriteLine(logMsg);
				sw.Close();
			}
		}

		public static string  creatDataBackUpFolder()
		{
			string date = DateTime.Now.ToString("yyyy-MM-dd");
			string folderPath = FileManager.getFilePath("COL-" + date);
			if(!Directory.Exists(folderPath))
			{
				Directory.CreateDirectory(folderPath);
			}
			return folderPath;
		}

		public static bool replaceDB(string backDir)
		{
			string sourceDir = Utils.getAppPath(backDir);
			if (Directory.Exists(sourceDir))
			{
				foreach (string f in Directory.GetFileSystemEntries(sourceDir))
				{
					if (File.Exists(f))
					{
						FileInfo info = new FileInfo(f);
						#if UNITY_EDITOR
						string destPath = string.Format(@"Assets/StreamingAssets/{0}", info.Name);
						#else
						string destPath = string.Format("{0}/{1}", Application.persistentDataPath, info.Name);
						#endif
						File.Copy(f, destPath, true);
					}
				}
				return true;
			}
			return false;
		}

		public static void createFile(string fileName)
		{
			string filePath = getFilePath(fileName);
			File.WriteAllText(filePath, @"{}", Encoding.UTF8);
		}

		public static void writeFile(string fileName, byte[] datas)
		{
			BinaryFormatter bf = new BinaryFormatter();
			using(FileStream fs = new FileStream(getFilePath(fileName), FileMode.Create, FileAccess.Write))
			{
				bf.Serialize(fs, datas);
				fs.Close();
			}
		}

		public static void writeFile(string fileName, string data)
		{
			string filePath = getFilePath(fileName);
			File.WriteAllText(filePath, data, Encoding.UTF8);
		}

		public static bool isExist(string fileName, bool inConfig = false)
		{
			string filePath = getFilePath(fileName);
			if (inConfig)
				filePath = string.Format(@"Assets/Resources/Config/{0}", fileName);
			return File.Exists(filePath);
		}

		public static void delete(string fileName, bool inConfig = false)
		{
			if (isExist(fileName, inConfig))
			{
				string filePath = getFilePath(fileName);
				if (inConfig)
					filePath = string.Format(@"Assets/Resources/Config/{0}", fileName);
				File.Delete(filePath);
			}
		}

		public static string readFile(string fileName)
		{
			if (isExist(fileName))
				return File.ReadAllText(getFilePath(fileName));
			return null;
		}

		public static byte[] readBytes(string fileName)
		{
			string filePath = getFilePath(fileName);
			FileInfo file = new FileInfo(filePath);
			if (file.Exists)
			{
				try
				{
					using(FileStream fs = new FileStream(filePath, FileMode.Open, FileAccess.Read))
					{
						int size = (int) fs.Length;
						byte[] data = new byte[size];
						fs.Read(data, 0, size);
						fs.Close();
						return data;
					}
				}
				catch (Exception error)
				{
					Debug.Log(error.ToString() + file.Name);
					file.Delete();
					return null;
				}
			}
			return null;
		}

		public static void writeBytes(string fileName, byte[] bytes, bool inConfig = false)
		{
			string filePath = getFilePath(fileName);
			if (inConfig)
				filePath = string.Format(@"Assets/Resources/Config/{0}", fileName);
			try
			{
				using(FileStream fs = new FileStream(filePath, FileMode.Create, FileAccess.Write))
				{
					fs.Write(bytes, 0, bytes.Length);
					fs.Close();
				}
			}
			catch (Exception error)
			{
				Debug.LogError(error.ToString() + " save 2 file error! " + filePath);
			}
		}

		public static void deleteFolder(string dir)
		{
			foreach (string d in Directory.GetFileSystemEntries(dir))
			{
				if (File.Exists(d))
				{
					FileInfo fi = new FileInfo(d);
					if (fi.Attributes.ToString().IndexOf("ReadOnly", StringComparison.Ordinal) != -1)
						fi.Attributes = FileAttributes.Normal;
					File.Delete(d); //直接删除其中的文件   
				}
				else
				{
					DirectoryInfo d1 = new DirectoryInfo(d);
					if (d1.GetFiles().Length != 0)
					{
						deleteFolder(d1.FullName); ////递归删除子文件夹 
					}
					Directory.Delete(d);
				}
			}
			Directory.Delete(dir);
		}
	}
}
