using System;
using System.Data.Objects.DataClasses;
using System.Runtime.Serialization;

namespace CarPark.DB;

[Serializable]
[DataContract(IsReference = true)]
[EdmEntityType(NamespaceName = "CarPark.DB", Name = "SystemLog")]
public class SystemLog : EntityObject
{
	private int _SystemLogID;

	private int _LogType;

	private int _SystemCode;

	private string _UserCode;

	private string _Remark;

	private int _ErrorCode;

	private DateTimeOffset _CreateTime;

	[DataMember]
	[EdmScalarProperty(EntityKeyProperty = true, IsNullable = false)]
	public int SystemLogID
	{
		get
		{
			return _SystemLogID;
		}
		set
		{
			if (_SystemLogID != value)
			{
				ReportPropertyChanging("SystemLogID");
				_SystemLogID = StructuralObject.SetValidValue(value);
				ReportPropertyChanged("SystemLogID");
			}
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
	public int SystemCode
	{
		get
		{
			return _SystemCode;
		}
		set
		{
			ReportPropertyChanging("SystemCode");
			_SystemCode = StructuralObject.SetValidValue(value);
			ReportPropertyChanged("SystemCode");
		}
	}

	[EdmScalarProperty(EntityKeyProperty = false, IsNullable = false)]
	[DataMember]
	public string UserCode
	{
		get
		{
			return _UserCode;
		}
		set
		{
			ReportPropertyChanging("UserCode");
			_UserCode = StructuralObject.SetValidValue(value, isNullable: false);
			ReportPropertyChanged("UserCode");
		}
	}

	[DataMember]
	[EdmScalarProperty(EntityKeyProperty = false, IsNullable = true)]
	public string Remark
	{
		get
		{
			return _Remark;
		}
		set
		{
			ReportPropertyChanging("Remark");
			_Remark = StructuralObject.SetValidValue(value, isNullable: true);
			ReportPropertyChanged("Remark");
		}
	}

	[DataMember]
	[EdmScalarProperty(EntityKeyProperty = false, IsNullable = false)]
	public int ErrorCode
	{
		get
		{
			return _ErrorCode;
		}
		set
		{
			ReportPropertyChanging("ErrorCode");
			_ErrorCode = StructuralObject.SetValidValue(value);
			ReportPropertyChanged("ErrorCode");
		}
	}

	[DataMember]
	[EdmScalarProperty(EntityKeyProperty = false, IsNullable = false)]
	public DateTimeOffset CreateTime
	{
		get
		{
			return _CreateTime;
		}
		set
		{
			ReportPropertyChanging("CreateTime");
			_CreateTime = StructuralObject.SetValidValue(value);
			ReportPropertyChanged("CreateTime");
		}
	}

	public static SystemLog CreateSystemLog(int systemLogID, int logType, int systemCode, string userCode, int errorCode, DateTimeOffset createTime)
	{
		SystemLog systemLog = new SystemLog();
		systemLog.SystemLogID = systemLogID;
		systemLog.LogType = logType;
		systemLog.SystemCode = systemCode;
		systemLog.UserCode = userCode;
		systemLog.ErrorCode = errorCode;
		systemLog.CreateTime = createTime;
		return systemLog;
	}
}
