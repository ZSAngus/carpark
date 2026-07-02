using System;
using System.Data.Objects.DataClasses;
using System.Runtime.Serialization;

namespace CarPark.DB;

[Serializable]
[EdmEntityType(NamespaceName = "CarPark.DB", Name = "APS_Syslog")]
[DataContract(IsReference = true)]
public class APS_Syslog : EntityObject
{
	private int _APSLogID;

	private DateTime? _LogDateTime;

	private string _LogUName;

	private string _LogPCName;

	private string _LogFuct;

	private string _LogKeyCode;

	private int _LogAPSID;

	private int _LogType;

	private int _APS_AlarmCode;

	[EdmScalarProperty(EntityKeyProperty = true, IsNullable = false)]
	[DataMember]
	public int APSLogID
	{
		get
		{
			return _APSLogID;
		}
		set
		{
			if (_APSLogID != value)
			{
				ReportPropertyChanging("APSLogID");
				_APSLogID = StructuralObject.SetValidValue(value);
				ReportPropertyChanged("APSLogID");
			}
		}
	}

	[EdmScalarProperty(EntityKeyProperty = false, IsNullable = true)]
	[DataMember]
	public DateTime? LogDateTime
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

	[EdmScalarProperty(EntityKeyProperty = false, IsNullable = true)]
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
			_LogUName = StructuralObject.SetValidValue(value, isNullable: true);
			ReportPropertyChanged("LogUName");
		}
	}

	[DataMember]
	[EdmScalarProperty(EntityKeyProperty = false, IsNullable = false)]
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
	[EdmScalarProperty(EntityKeyProperty = false, IsNullable = true)]
	public string LogFuct
	{
		get
		{
			return _LogFuct;
		}
		set
		{
			ReportPropertyChanging("LogFuct");
			_LogFuct = StructuralObject.SetValidValue(value, isNullable: true);
			ReportPropertyChanged("LogFuct");
		}
	}

	[EdmScalarProperty(EntityKeyProperty = false, IsNullable = true)]
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
			_LogKeyCode = StructuralObject.SetValidValue(value, isNullable: true);
			ReportPropertyChanged("LogKeyCode");
		}
	}

	[DataMember]
	[EdmScalarProperty(EntityKeyProperty = false, IsNullable = false)]
	public int LogAPSID
	{
		get
		{
			return _LogAPSID;
		}
		set
		{
			ReportPropertyChanging("LogAPSID");
			_LogAPSID = StructuralObject.SetValidValue(value);
			ReportPropertyChanged("LogAPSID");
		}
	}

	[DataMember]
	[EdmScalarProperty(EntityKeyProperty = false, IsNullable = false)]
	public int LogType
	{
		get
		{
			return _LogType;
		}
		set
		{
			ReportPropertyChanging("LogType");
			_LogType = StructuralObject.SetValidValue(value);
			ReportPropertyChanged("LogType");
		}
	}

	[DataMember]
	[EdmScalarProperty(EntityKeyProperty = false, IsNullable = false)]
	public int APS_AlarmCode
	{
		get
		{
			return _APS_AlarmCode;
		}
		set
		{
			ReportPropertyChanging("APS_AlarmCode");
			_APS_AlarmCode = StructuralObject.SetValidValue(value);
			ReportPropertyChanged("APS_AlarmCode");
		}
	}

	public static APS_Syslog CreateAPS_Syslog(int aPSLogID, string logPCName, int logAPSID, int logType, int aPS_AlarmCode)
	{
		APS_Syslog aPS_Syslog = new APS_Syslog();
		aPS_Syslog.APSLogID = aPSLogID;
		aPS_Syslog.LogPCName = logPCName;
		aPS_Syslog.LogAPSID = logAPSID;
		aPS_Syslog.LogType = logType;
		aPS_Syslog.APS_AlarmCode = aPS_AlarmCode;
		return aPS_Syslog;
	}
}
