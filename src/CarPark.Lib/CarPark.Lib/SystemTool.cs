using System;
using System.Linq;
using System.Reflection;
using CarPark.DB;
using log4net;

namespace CarPark.Lib;

public class SystemTool
{
	private static ILog Logger;

	static SystemTool()
	{
		Class2.sKBPqdpzNwCBA();
		Logger = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
	}

	public SystemTool()
	{
		Class2.sKBPqdpzNwCBA();
	}

	public static byte[] AppendBuff(byte[] byte1, byte[] byte2)
	{
		byte[] array = new byte[byte1.Length + byte2.Length];
		Array.Copy(byte1, 0, array, 0, byte1.Length);
		Array.Copy(byte2, 0, array, byte1.Length, byte2.Length);
		return array;
	}

	public static void CheckRole(MethodBase function)
	{
		string functionName = $"{function.ReflectedType.FullName}.{function.Name}";
		Logger.Debug("funcName:" + functionName);
		if (DataBuffer.CurrentRoles.FirstOrDefault((SysRole m) => m.RoleClass == functionName) == null)
		{
			throw new Exception("ERR_NO_RIGHT");
		}
	}
}
