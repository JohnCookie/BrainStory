using System;
using System.Collections.Generic;
using System.Reflection;

namespace JCFramework
{
	/// <summary>
	/// Utility class for types.
	/// </summary>
	public static class TypeUtils
	{
		public static string GetVarName(System.Linq.Expressions.Expression<Func<string, string>> exp)
		{
			return ((System.Linq.Expressions.MemberExpression)exp.Body).Member.Name;
		}
		/// <summary>
		/// Determines whether <paramref name="potentialDescendant"/> is the same
		/// or a subclass of <paramref name="potentialBase"/>.
		/// </summary>
		/// <param name="potentialBase">Potential base type.</param>
		/// <param name="potentialDescendant">Potential descendant type.</param>
		/// <returns>Boolean.</returns>
		public static bool IsAssignable(Type potentialBase, Type potentialDescendant)
		{
			return potentialBase.IsAssignableFrom(potentialDescendant);
		}

		/// <summary>
		/// Gets all types assignable from a given <paramref name="baseType"/>.
		/// </summary>
		/// <remarks>
		/// Excludes any types in assemblies from Unity or Mono.
		/// </remarks>
		/// <param name="baseType">Base type from which the types in the namespace must be assignable.</param>
		/// <returns>The assignable types in the namespace.</returns>
		public static Type[] GetAssignableTypes(Type baseType)
		{
			return GetAssignableTypes(baseType, string.Empty, false);
		}

		/// <summary>
		/// Gets all types assignable from a given <paramref name="baseType"/> 
		/// in a given <paramref name="namespaceName"/>.
		/// </summary>
		/// <remarks>
		/// Excludes any types in assemblies from Unity or Mono.
		/// </remarks>
		/// <param name="baseType">Base type from which the types in the namespace must be assignable.</param>
		/// <param name="namespaceName">Namespace name.</param>
		/// <returns>The assignable types in the namespace.</returns>
		public static Type[] GetAssignableTypes(Type baseType, string namespaceName)
		{
			return GetAssignableTypes(baseType, namespaceName, false);
		}

		/// <summary>
		/// Gets all types assignable from a given <paramref name="baseType"/> 
		/// in a given <paramref name="namespaceName"/>.
		/// </summary>
		/// <remarks>
		/// Excludes any types in assemblies from Unity or Mono.
		/// </remarks>
		/// <param name="baseType">Base type from which the types in the namespace must be assignable.</param>
		/// <param name="namespaceName">Namespace name.</param>
		/// <param name="includeChildren">Indicates whether child namespaces should be included.</param>
		/// <returns>The assignable types in the namespace.</returns>
		public static Type[] GetAssignableTypes(Type baseType, string namespaceName, bool includeChildren)
		{
			var typesToBind = new List<Type>();

			//Looks for assignable types in all available assemblies.
			var assemblies = AppDomain.CurrentDomain.GetAssemblies();
			for(int i = 0; i < assemblies.Length; i++)
			{
				var assembly = assemblies[i];
				if(assembly.FullName.StartsWith("Unity") || assembly.FullName.StartsWith("Boo") ||
				   assembly.FullName.StartsWith("Mono") || assembly.FullName.StartsWith("System") || assembly.FullName.StartsWith("mscorlib"))
					continue;
				try
				{
					var allTypes = assemblies[i].GetTypes();
					for(int typeIndex = 0; typeIndex < allTypes.Length; typeIndex++)
					{
						var type = allTypes[typeIndex];
						var isTypeInNamespace = (string.IsNullOrEmpty(namespaceName)) || (!includeChildren && type.Namespace == namespaceName) ||
							(includeChildren && !string.IsNullOrEmpty(type.Namespace) && type.Namespace.StartsWith(namespaceName));
						if(isTypeInNamespace && type.IsClass && TypeUtils.IsAssignable(baseType, type))
						{
							typesToBind.Add(type);
						}
					}
				}
				catch(ReflectionTypeLoadException)
				{
					//If the assembly can't be read, just continue.
					continue;
				}
			}
			return typesToBind.ToArray();
		}

		/// <summary>
		/// Gets a type from a type name.
		/// </summary>
		/// <remarks>
		/// Excludes any type in assemblies from Unity or Mono.
		/// </remarks>
		/// <param name="typeName">Type name.</param>
		/// <returns>The type or NULL.</returns>
		public static Type GetType(string typeName)
		{
			return GetType(string.Empty, typeName);
		}

		/// <summary>
		/// Gets a type from a namespace and type names.
		/// </summary>
		/// <remarks>
		/// Excludes any type in assemblies from Unity or Mono.
		/// </remarks>
		/// <param name="namespaceName">Namespace name.</param>
		/// <param name="typeName">Type name.</param>
		/// <returns>The type or NULL.</returns>
		public static Type GetType(string namespaceName, string typeName)
		{
			string fullName = null;
			if(!string.IsNullOrEmpty(typeName))
			{
				if(string.IsNullOrEmpty(namespaceName) || namespaceName == "-")
				{
					fullName = typeName;
				}
				else
				{
					fullName = string.Format("{0}.{1}", namespaceName, typeName);
				}
			}

			if(string.IsNullOrEmpty(fullName))
				return null;
			
			//Looks for the type in all available assemblies.
			var assemblies = AppDomain.CurrentDomain.GetAssemblies();
			for(int assemblyIndex = 0; assemblyIndex < assemblies.Length; assemblyIndex++)
			{
				var assembly = assemblies[assemblyIndex];
				
				if(assembly.FullName.StartsWith("Unity") ||
				   assembly.FullName.StartsWith("Boo") ||
				   assembly.FullName.StartsWith("Mono") ||
				   assembly.FullName.StartsWith("System") ||
				   assembly.FullName.StartsWith("mscorlib"))
				{
					continue;
				}
				
				try
				{
					var allTypes = assemblies[assemblyIndex].GetTypes();
					for(int typeIndex = 0; typeIndex < allTypes.Length; typeIndex++)
					{
						var type = allTypes[typeIndex];
						if(type.FullName == fullName)
						{
							return type;
						}
					}
				}
				catch(ReflectionTypeLoadException)
				{
					//If the assembly can't be read, just continue.
					continue;
				}
			}
			return null;
		}

		//获取const属性，const不属于property，const是属于field
		public static string[] GetFieldNames(Type type, out int[] fieldValues)
		{
			//添加BindFalgs获取继承属性，否则只获取当前类的属性
			FieldInfo[] fileds = type.GetFields(BindingFlags.FlattenHierarchy | BindingFlags.Public | BindingFlags.Static);
			string[] fieleNames = new string[fileds.Length+1];
			fieleNames[0] = "未绑定消息";
			fieldValues = new int[fileds.Length+1];
			fieldValues[0] = -1;
			for(int i=0; i<fileds.Length; i++)
			{
                FieldInfo info = fileds[i];
                //info.IsStatic 是获取静态属性，literal是const属性
                if (info.IsLiteral)  
				{
					fieleNames[i+1] = info.Name;
					fieldValues[i+1] = (int)info.GetValue(null);
				}
			}    
			return fieleNames;
		}

		//获取const属性，const不属于property，const是属于field
//		public static object[] getFieldValues(Type type)
//		{
//			//添加BindFalgs获取继承属性，否则只获取当前类的属性
//			FieldInfo[] fileds = type.GetFields(BindingFlags.FlattenHierarchy | BindingFlags.Public | BindingFlags.Static);
//			object[] fieldValues = new object[fileds.Length];
//			for(int i=0; i<fieldValues.Length; i++)
//			{
//				FieldInfo info = fileds[i];
//				//info.IsStatic 是获取静态属性，literal是const属性
//				if (info.IsLiteral)  
//					fieldValues[i] = info.GetValue(null);
//			}    
//			return fieldValues;
//		}

		//下面代码是获取正常的属性
//		public static string[] GetPropertyNames(Type type)
//		{
//			PropertyInfo[] properties = type.GetProperties();
//			string[] propertyNames = new string[properties.Length];
//			for(int i=0; i<propertyNames.Length; i++)
//			{
//				PropertyInfo info = properties[i];
//				propertyNames[i] = info.Name;
//			}
//			return propertyNames;
//		}
	}
}