using System;
using System.Collections;
using System.Reflection;

namespace CarPark.Lib;

internal class Class3
{
	private static bool bool_0 = false;

	private static Hashtable hashtable_0 = new Hashtable();

	internal static void lLHifFIsCLsZtjvFfN0i()
	{
		if (!bool_0)
		{
			bool_0 = true;
			AppDomain.CurrentDomain.AssemblyResolve += smethod_0;
		}
	}

	private static Assembly smethod_0(object object_0, ResolveEventArgs resolveEventArgs_0)
	{
		return null;
	}

	private static string smethod_1(string string_0)
	{
		string text = string_0.Trim();
		int num = text.IndexOf(',');
		if (num >= 0)
		{
			text = text.Substring(0, num);
		}
		return text;
	}
}
