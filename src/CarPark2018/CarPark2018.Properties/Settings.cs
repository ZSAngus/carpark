using System.CodeDom.Compiler;
using System.Configuration;
using System.Diagnostics;
using System.Runtime.CompilerServices;

namespace CarPark2018.Properties;

[CompilerGenerated]
[GeneratedCode("Microsoft.VisualStudio.Editors.SettingsDesigner.SettingsSingleFileGenerator", "16.10.0.0")]
internal sealed class Settings : ApplicationSettingsBase
{
	private static Settings defaultInstance;

	public static Settings Default => defaultInstance;

	[ApplicationScopedSetting]
	[DebuggerNonUserCode]
	[DefaultSettingValue("CashierA")]
	public string OnlyID => (string)this["OnlyID"];

	[ApplicationScopedSetting]
	[DebuggerNonUserCode]
	[DefaultSettingValue("EQMA")]
	public string ServerEQM => (string)this["ServerEQM"];

	[ApplicationScopedSetting]
	[DebuggerNonUserCode]
	[DefaultSettingValue("True")]
	public bool IsMPayByPAX => (bool)this["IsMPayByPAX"];

	[ApplicationScopedSetting]
	[DebuggerNonUserCode]
	[DefaultSettingValue("1")]
	public int WatchAreaID => (int)this["WatchAreaID"];

	[ApplicationScopedSetting]
	[DebuggerNonUserCode]
	[DefaultSettingValue("root")]
	public string UserName => (string)this["UserName"];

	[ApplicationScopedSetting]
	[DebuggerNonUserCode]
	[DefaultSettingValue("123456")]
	public string Pwd => (string)this["Pwd"];

	[ApplicationScopedSetting]
	[DebuggerNonUserCode]
	[DefaultSettingValue("127.0.0.1")]
	public string ServerIP2 => (string)this["ServerIP2"];

	[ApplicationScopedSetting]
	[DebuggerNonUserCode]
	[DefaultSettingValue("625019")]
	public string ShieldNum => (string)this["ShieldNum"];

	[ApplicationScopedSetting]
	[DebuggerNonUserCode]
	[DefaultSettingValue("300")]
	public int TimeOutNum => (int)this["TimeOutNum"];

	[ApplicationScopedSetting]
	[DebuggerNonUserCode]
	[DefaultSettingValue("http://192.168.11.176:8093/")]
	public string PaymentServerUrl => (string)this["PaymentServerUrl"];

	[ApplicationScopedSetting]
	[DebuggerNonUserCode]
	[DefaultSettingValue("10012")]
	public string ParkingLotNo => (string)this["ParkingLotNo"];

	[ApplicationScopedSetting]
	[DebuggerNonUserCode]
	[DefaultSettingValue("1")]
	public string AreaCodeP => (string)this["AreaCodeP"];

	[ApplicationScopedSetting]
	[DebuggerNonUserCode]
	[DefaultSettingValue("127.0.0.1")]
	public string ServerIP => (string)this["ServerIP"];

	[ApplicationScopedSetting]
	[DebuggerNonUserCode]
	[DefaultSettingValue("\r\n          metadata=res://*/CarParkData.csdl|res://*/CarParkData.ssdl|res://*/CarParkData.msl;provider=MySql.Data.MySqlClient;provider connection string=\"server=127.0.0.1;User Id=root;password=master123456;Persist Security Info=True;database=yinzuo;Pooling=true;Min Pool Size=1;Max Pool Size=20;\"\r\n\r\n        ")]
	public string DBConnection => (string)this["DBConnection"];

	[ApplicationScopedSetting]
	[DebuggerNonUserCode]
	[DefaultSettingValue("http://127.0.0.1:8080/")]
	public string ReportPath => (string)this["ReportPath"];

	[ApplicationScopedSetting]
	[DebuggerNonUserCode]
	[DefaultSettingValue("carparknew")]
	public string DBName => (string)this["DBName"];

	[ApplicationScopedSetting]
	[DebuggerNonUserCode]
	[DefaultSettingValue("0")]
	public string zhaji => (string)this["zhaji"];

	[ApplicationScopedSetting]
	[DebuggerNonUserCode]
	[DefaultSettingValue("0")]
	public string rent => (string)this["rent"];

	[ApplicationScopedSetting]
	[DebuggerNonUserCode]
	[DefaultSettingValue("0")]
	public string nocash => (string)this["nocash"];

	[ApplicationScopedSetting]
	[DebuggerNonUserCode]
	[DefaultSettingValue("0")]
	public string verify => (string)this["verify"];

	[ApplicationScopedSetting]
	[DebuggerNonUserCode]
	[DefaultSettingValue("")]
	public string BackupReportPath => (string)this["BackupReportPath"];

	static Settings()
	{
		defaultInstance = (Settings)SettingsBase.Synchronized(new Settings());
	}
}
