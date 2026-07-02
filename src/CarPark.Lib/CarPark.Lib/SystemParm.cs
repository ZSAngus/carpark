using System;

namespace CarPark.Lib;

public class SystemParm
{
	public static string LongTimeFormat;

	public static string ShortDateFormat;

	public static string ShortTimeFormat;

	public static string StationID => Environment.MachineName;

	static SystemParm()
	{
		Class2.sKBPqdpzNwCBA();
		ShortTimeFormat = "MM/dd HH:mm";
		ShortDateFormat = "yyyy-MM-dd";
		LongTimeFormat = "yyyy-MM-dd HH:mm:ss";
	}

	public SystemParm()
	{
		Class2.sKBPqdpzNwCBA();
	}
}
