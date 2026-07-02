using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace CarPark2018;

public static class Config
{
	private static JObject Setting;

	public static string FreePicLocalPath { get; set; }

	public static string ShieldNum { get; set; }

	public static bool AutoLoginBackstage { get; set; }

	public static int MPPOSSyncTime { get; set; }

	public static string LicensePlatePath { get; set; }

	public static string FreePicFieldPath { get; set; }

	public static string ReportPath { get; set; }

	public static string BackupReportPath { get; set; }

	public static int TicketType { get; set; }

	public static int AreaCode { get; set; }

	public static void InitSettings(string JsonStr)
	{
		Setting = (JObject)JsonConvert.DeserializeObject(JsonStr);
		AreaCode = getSettingVal<int>("org", "AreaCode");
		AutoLoginBackstage = getSettingVal<bool>("org", "AutoLoginBackstage");
		FreePicFieldPath = getSettingVal<string>("org", "FreePicFieldPath");
		FreePicLocalPath = getSettingVal<string>("org", "FreePicLocalPath");
		LicensePlatePath = getSettingVal<string>("org", "LicensePlatePath");
		MPPOSSyncTime = getSettingVal<int>("org", "MPPOSSyncTime");
		ReportPath = getSettingVal<string>("org", "ReportPath");
		BackupReportPath = getSettingVal<string>("org", "BackupReportPath");
		ShieldNum = getSettingVal<string>("org", "ShieldNum");
		TicketType = getSettingVal<int>("org", "TicketType");
	}

	private static T getSettingVal<T>(string index, string key)
	{
		T result = default(T);
		object obj = Setting[index][key];
		if (obj == null)
		{
			return result;
		}
		return (T)Convert.ChangeType(obj.ToString(), typeof(T));
	}
}
