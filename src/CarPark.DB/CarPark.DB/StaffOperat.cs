using System;
using System.Data.Objects.DataClasses;
using System.Runtime.Serialization;

namespace CarPark.DB;

[Serializable]
[EdmEntityType(NamespaceName = "CarPark.DB", Name = "StaffOperat")]
[DataContract(IsReference = true)]
public class StaffOperat : EntityObject
{
	private int _OperationID;

	private DateTime _OperationTime;

	private string _StaffCode;

	private int _ShiftID;

	private int _OperationCode;

	private string _Remark;

	[DataMember]
	[EdmScalarProperty(EntityKeyProperty = true, IsNullable = false)]
	public int OperationID
	{
		get
		{
			return _OperationID;
		}
		set
		{
			if (_OperationID != value)
			{
				ReportPropertyChanging("OperationID");
				_OperationID = StructuralObject.SetValidValue(value);
				ReportPropertyChanged("OperationID");
			}
		}
	}

	[EdmScalarProperty(EntityKeyProperty = false, IsNullable = false)]
	[DataMember]
	public DateTime OperationTime
	{
		get
		{
			return _OperationTime;
		}
		set
		{
			ReportPropertyChanging("OperationTime");
			_OperationTime = StructuralObject.SetValidValue(value);
			ReportPropertyChanged("OperationTime");
		}
	}

	[EdmScalarProperty(EntityKeyProperty = false, IsNullable = false)]
	[DataMember]
	public string StaffCode
	{
		get
		{
			return _StaffCode;
		}
		set
		{
			ReportPropertyChanging("StaffCode");
			_StaffCode = StructuralObject.SetValidValue(value, isNullable: false);
			ReportPropertyChanged("StaffCode");
		}
	}

	[EdmScalarProperty(EntityKeyProperty = false, IsNullable = false)]
	[DataMember]
	public int ShiftID
	{
		get
		{
			return _ShiftID;
		}
		set
		{
			ReportPropertyChanging("ShiftID");
			_ShiftID = StructuralObject.SetValidValue(value);
			ReportPropertyChanged("ShiftID");
		}
	}

	[DataMember]
	[EdmScalarProperty(EntityKeyProperty = false, IsNullable = false)]
	public int OperationCode
	{
		get
		{
			return _OperationCode;
		}
		set
		{
			ReportPropertyChanging("OperationCode");
			_OperationCode = StructuralObject.SetValidValue(value);
			ReportPropertyChanged("OperationCode");
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

	public static StaffOperat CreateStaffOperat(int operationID, DateTime operationTime, string staffCode, int shiftID, int operationCode)
	{
		StaffOperat staffOperat = new StaffOperat();
		staffOperat.OperationID = operationID;
		staffOperat.OperationTime = operationTime;
		staffOperat.StaffCode = staffCode;
		staffOperat.ShiftID = shiftID;
		staffOperat.OperationCode = operationCode;
		return staffOperat;
	}
}
