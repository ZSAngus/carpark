using System;
using System.Reflection;

namespace CarPark.Core;

internal class Class1
{
	internal delegate void Delegate0(object o);

	internal static Module module_0;

	static Class1()
	{
		Class2.p1nVpZ8zqUSUX();
		module_0 = typeof(Class1).Assembly.ManifestModule;
	}

	public Class1()
	{
		Class2.p1nVpZ8zqUSUX();
	}

	internal static void IPWVpZ88fBw25(int typemdt)
	{
		Type type = module_0.ResolveType(33554432 + typemdt);
		FieldInfo[] fields = type.GetFields();
		foreach (FieldInfo fieldInfo in fields)
		{
			MethodInfo method = (MethodInfo)module_0.ResolveMethod(fieldInfo.MetadataToken + 100663296);
			fieldInfo.SetValue(null, (MulticastDelegate)Delegate.CreateDelegate(type, method));
		}
	}
}
