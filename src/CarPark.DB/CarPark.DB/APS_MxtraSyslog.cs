using System;
using System.Data.Objects.DataClasses;
using System.Runtime.Serialization;

namespace CarPark.DB;

[Serializable]
[DataContract(IsReference = true)]
[EdmEntityType(NamespaceName = "CarPark.DB", Name = "APS_MxtraSyslog")]
public class APS_MxtraSyslog : EntityObject
{
	private int _APSMxtraLogID;

	private DateTime _LogDateTime;

	private string _LogUName;

	private string _LogPCName;

	private string _LogFuct;

	private string _LogKeyCode;

	private int _LogAPSID;

	private int _LogType;

	private int _APS_AlarmCode;

	[EdmScalarProperty(EntityKeyProperty = true, IsNullable = false)]
	[DataMember]
	public int APSMxtraLogID
	{
		get
		{
			return _APSMxtraLogID;
		}
		set
		{
			if (_APSMxtraLogID != value)
			{
				ReportPropertyChanging("APSMxtraLogID");
				_APSMxtraLogID = StructuralObject.SetValidValue(value);
				ReportPropertyChanged("APSMxtraLogID");
			}
		}
	}

	[EdmScalarProperty(EntityKeyProperty = false, IsNullable = false)]
	[DataMember]
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

	[EdmScalarProperty(EntityKeyProperty = false, IsNullable = true)]
	[DataMember]
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

	[EdmScalarProperty(EntityKeyProperty = false, IsNullable = false)]
	[DataMember]
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

	[EdmScalarProperty(EntityKeyProperty = false, IsNullable = false)]
	[DataMember]
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

	public static APS_MxtraSyslog CreateAPS_MxtraSyslog(int aPSMxtraLogID, DateTime logDateTime, string logPCName, int logAPSID, int logType, int aPS_AlarmCode)
	{
		APS_MxtraSyslog aPS_MxtraSyslog = new APS_MxtraSyslog();
		aPS_MxtraSyslog.APSMxtraLogID = aPSMxtraLogID;
		aPS_MxtraSyslog.LogDateTime = logDateTime;
		aPS_MxtraSyslog.LogPCName = logPCName;
		aPS_MxtraSyslog.LogAPSID = logAPSID;
		aPS_MxtraSyslog.LogType = logType;
		aPS_MxtraSyslog.APS_AlarmCode = aPS_AlarmCode;
		return aPS_MxtraSyslog;
	}
}
