using System;
using System.Data.Objects.DataClasses;
using System.Runtime.Serialization;

namespace CarPark.DB;

[Serializable]
[EdmEntityType(NamespaceName = "CarPark.DB", Name = "SysLog")]
[DataContract(IsReference = true)]
public class SysLog : EntityObject
{
	private int _SysLogID;

	private DateTime _LogDateTime;

	private string _LogUName;

	private string _LogPCName;

	private string _LogFuncT;

	private string _LogKeyCode;

	private string _LogPlatFor;

	[EdmScalarProperty(EntityKeyProperty = true, IsNullable = false)]
	[DataMember]
	public int SysLogID
	{
		get
		{
			return _SysLogID;
		}
		set
		{
			if (_SysLogID != value)
			{
				ReportPropertyChanging("SysLogID");
				_SysLogID = StructuralObject.SetValidValue(value);
				ReportPropertyChanged("SysLogID");
			}
		}
	}

	[DataMember]
	[EdmScalarProperty(EntityKeyProperty = false, IsNullable = false)]
	public DateTime LogDateTime
	{
		get
		{
			return _LogDateTime;
		}
		set
		{
			ReportPropertyChanging("LogDateTime");
			_LogDateTime = StructuralObject.SetValidValue(value);
			ReportPropertyChanged("LogDateTime");
		}
	}

	[EdmScalarProperty(EntityKeyProperty = false, IsNullable = false)]
	[DataMember]
	public string LogUName
	{
		get
		{
			return _LogUName;
		}
		set
		{
			ReportPropertyChanging("LogUName");
			_LogUName = StructuralObject.SetValidValue(value, isNullable: false);
			ReportPropertyChanged("LogUName");
		}
	}

	[EdmScalarProperty(EntityKeyProperty = false, IsNullable = false)]
	[DataMember]
	public string LogPCName
	{
		get
		{
			return _LogPCName;
		}
		set
		{
			ReportPropertyChanging("LogPCName");
			_LogPCName = StructuralObject.SetValidValue(value, isNullable: false);
			ReportPropertyChanged("LogPCName");
		}
	}

	[DataMember]
	[EdmScalarProperty(EntityKeyProperty = false, IsNullable = false)]
	public string LogFuncT
	{
		get
		{
			return _LogFuncT;
		}
		set
		{
			ReportPropertyChanging("LogFuncT");
			_LogFuncT = StructuralObject.SetValidValue(value, isNullable: false);
			ReportPropertyChanged("LogFuncT");
		}
	}

	[EdmScalarProperty(EntityKeyProperty = false, IsNullable = false)]
	[DataMember]
	public string LogKeyCode
	{
		get
		{
			return _LogKeyCode;
		}
		set
		{
			ReportPropertyChanging("LogKeyCode");
			_LogKeyCode = StructuralObject.SetValidValue(value, isNullable: false);
			ReportPropertyChanged("LogKeyCode");
		}
	}

	[EdmScalarProperty(EntityKeyProperty = false, IsNullable = false)]
	[DataMember]
	public string LogPlatFor
	{
		get
		{
			return _LogPlatFor;
		}
		set
		{
			ReportPropertyChanging("LogPlatFor");
			_LogPlatFor = StructuralObject.SetValidValue(value, isNullable: false);
			ReportPropertyChanged("LogPlatFor");
		}
	}

	public static SysLog CreateSysLog(int sysLogID, DateTime logDateTime, string logUName, string logPCName, string logFuncT, string logKeyCode, string logPlatFor)
	{
		SysLog sysLog = new SysLog();
		sysLog.SysLogID = sysLogID;
		sysLog.LogDateTime = logDateTime;
		sysLog.LogUName = logUName;
		sysLog.LogPCName = logPCName;
		sysLog.LogFuncT = logFuncT;
		sysLog.LogKeyCode = logKeyCode;
		sysLog.LogPlatFor = logPlatFor;
		return sysLog;
	}
}
