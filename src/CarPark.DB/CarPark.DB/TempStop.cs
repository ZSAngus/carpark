using System;
using System.Data.Objects.DataClasses;
using System.Runtime.Serialization;

namespace CarPark.DB;

[Serializable]
[EdmEntityType(NamespaceName = "CarPark.DB", Name = "TempStop")]
[DataContract(IsReference = true)]
public class TempStop : EntityObject
{
	private int _ID;

	private string _SubjectNo;

	private string _SubjectName;

	private DateTime _StartDateTime;

	private DateTime _EndDateTime;

	private string _Description;

	private DateTime _CreateTime;

	private string _CreateStaffCode;

	private string _AllowGateID;

	private bool _IsDelete;

	[EdmScalarProperty(EntityKeyProperty = true, IsNullable = false)]
	[DataMember]
	public int ID
	{
		get
		{
			return _ID;
		}
		set
		{
			if (_ID != value)
			{
				ReportPropertyChanging("ID");
				_ID = StructuralObject.SetValidValue(value);
				ReportPropertyChanged("ID");
			}
		}
	}

	[EdmScalarProperty(EntityKeyProperty = false, IsNullable = true)]
	[DataMember]
	public string SubjectNo
	{
		get
		{
			return _SubjectNo;
		}
		set
		{
			ReportPropertyChanging("SubjectNo");
			_SubjectNo = StructuralObject.SetValidValue(value, isNullable: true);
			ReportPropertyChanged("SubjectNo");
		}
	}

	[DataMember]
	[EdmScalarProperty(EntityKeyProperty = false, IsNullable = true)]
	public string SubjectName
	{
		get
		{
			return _SubjectName;
		}
		set
		{
			ReportPropertyChanging("SubjectName");
			_SubjectName = StructuralObject.SetValidValue(value, isNullable: true);
			ReportPropertyChanged("SubjectName");
		}
	}

	[EdmScalarProperty(EntityKeyProperty = false, IsNullable = false)]
	[DataMember]
	public DateTime StartDateTime
	{
		get
		{
			return _StartDateTime;
		}
		set
		{
			ReportPropertyChanging("StartDateTime");
			_StartDateTime = StructuralObject.SetValidValue(value);
			ReportPropertyChanged("StartDateTime");
		}
	}

	[DataMember]
	[EdmScalarProperty(EntityKeyProperty = false, IsNullable = false)]
	public DateTime EndDateTime
	{
		get
		{
			return _EndDateTime;
		}
		set
		{
			ReportPropertyChanging("EndDateTime");
			_EndDateTime = StructuralObject.SetValidValue(value);
			ReportPropertyChanged("EndDateTime");
		}
	}

	[EdmScalarProperty(EntityKeyProperty = false, IsNullable = true)]
	[DataMember]
	public string Description
	{
		get
		{
			return _Description;
		}
		set
		{
			ReportPropertyChanging("Description");
			_Description = StructuralObject.SetValidValue(value, isNullable: true);
			ReportPropertyChanged("Description");
		}
	}

	[DataMember]
	[EdmScalarProperty(EntityKeyProperty = false, IsNullable = false)]
	public DateTime CreateTime
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

	[EdmScalarProperty(EntityKeyProperty = false, IsNullable = false)]
	[DataMember]
	public string CreateStaffCode
	{
		get
		{
			return _CreateStaffCode;
		}
		set
		{
			ReportPropertyChanging("CreateStaffCode");
			_CreateStaffCode = StructuralObject.SetValidValue(value, isNullable: false);
			ReportPropertyChanged("CreateStaffCode");
		}
	}

	[DataMember]
	[EdmScalarProperty(EntityKeyProperty = false, IsNullable = true)]
	public string AllowGateID
	{
		get
		{
			return _AllowGateID;
		}
		set
		{
			ReportPropertyChanging("AllowGateID");
			_AllowGateID = StructuralObject.SetValidValue(value, isNullable: true);
			ReportPropertyChanged("AllowGateID");
		}
	}

	[EdmScalarProperty(EntityKeyProperty = false, IsNullable = false)]
	[DataMember]
	public bool IsDelete
	{
		get
		{
			return _IsDelete;
		}
		set
		{
			ReportPropertyChanging("IsDelete");
			_IsDelete = StructuralObject.SetValidValue(value);
			ReportPropertyChanged("IsDelete");
		}
	}

	public static TempStop CreateTempStop(int id, DateTime startDateTime, DateTime endDateTime, DateTime createTime, string createStaffCode, bool isDelete)
	{
		TempStop tempStop = new TempStop();
		tempStop.ID = id;
		tempStop.StartDateTime = startDateTime;
		tempStop.EndDateTime = endDateTime;
		tempStop.CreateTime = createTime;
		tempStop.CreateStaffCode = createStaffCode;
		tempStop.IsDelete = isDelete;
		return tempStop;
	}
}
