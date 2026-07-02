using System;
using System.Data.Objects.DataClasses;
using System.Runtime.Serialization;

namespace CarPark.DB;

[Serializable]
[DataContract(IsReference = true)]
[EdmEntityType(NamespaceName = "CarPark.DB", Name = "CustomFreeRecord")]
public class CustomFreeRecord : EntityObject
{
	private string _FreeImagePath;

	private int? _FormType;

	private int _FreeRecordID;

	private int _FreeType;

	private int? _FreeTenatID;

	private int _FreeMinutes;

	private decimal _FreeCharge;

	private int _ShiftID;

	private string _StaffCode;

	private DateTime _FreeTime;

	private int _ChargeRecordID;

	private string _Remark;

	private string _BarCode;

	private bool _IsDelete;

	[DataMember]
	[EdmScalarProperty(EntityKeyProperty = false, IsNullable = true)]
	public string FreeImagePath
	{
		get
		{
			return _FreeImagePath;
		}
		set
		{
			ReportPropertyChanging("FreeImagePath");
			_FreeImagePath = StructuralObject.SetValidValue(value, isNullable: true);
			ReportPropertyChanged("FreeImagePath");
		}
	}

	[DataMember]
	[EdmScalarProperty(EntityKeyProperty = false, IsNullable = true)]
	public int? FormType
	{
		get
		{
			return _FormType;
		}
		set
		{
			ReportPropertyChanging("FormType");
			_FormType = StructuralObject.SetValidValue(value);
			ReportPropertyChanged("FormType");
		}
	}

	[DataMember]
	[EdmScalarProperty(EntityKeyProperty = true, IsNullable = false)]
	public int FreeRecordID
	{
		get
		{
			return _FreeRecordID;
		}
		set
		{
			if (_FreeRecordID != value)
			{
				ReportPropertyChanging("FreeRecordID");
				_FreeRecordID = StructuralObject.SetValidValue(value);
				ReportPropertyChanged("FreeRecordID");
			}
		}
	}

	[DataMember]
	[EdmScalarProperty(EntityKeyProperty = false, IsNullable = false)]
	public int FreeType
	{
		get
		{
			return _FreeType;
		}
		set
		{
			ReportPropertyChanging("FreeType");
			_FreeType = StructuralObject.SetValidValue(value);
			ReportPropertyChanged("FreeType");
		}
	}

	[EdmScalarProperty(EntityKeyProperty = false, IsNullable = true)]
	[DataMember]
	public int? FreeTenatID
	{
		get
		{
			return _FreeTenatID;
		}
		set
		{
			ReportPropertyChanging("FreeTenatID");
			_FreeTenatID = StructuralObject.SetValidValue(value);
			ReportPropertyChanged("FreeTenatID");
		}
	}

	[DataMember]
	[EdmScalarProperty(EntityKeyProperty = false, IsNullable = false)]
	public int FreeMinutes
	{
		get
		{
			return _FreeMinutes;
		}
		set
		{
			ReportPropertyChanging("FreeMinutes");
			_FreeMinutes = StructuralObject.SetValidValue(value);
			ReportPropertyChanged("FreeMinutes");
		}
	}

	[DataMember]
	[EdmScalarProperty(EntityKeyProperty = false, IsNullable = false)]
	public decimal FreeCharge
	{
		get
		{
			return _FreeCharge;
		}
		set
		{
			ReportPropertyChanging("FreeCharge");
			_FreeCharge = StructuralObject.SetValidValue(value);
			ReportPropertyChanged("FreeCharge");
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
	public DateTime FreeTime
	{
		get
		{
			return _FreeTime;
		}
		set
		{
			ReportPropertyChanging("FreeTime");
			_FreeTime = StructuralObject.SetValidValue(value);
			ReportPropertyChanged("FreeTime");
		}
	}

	[DataMember]
	[EdmScalarProperty(EntityKeyProperty = false, IsNullable = false)]
	public int ChargeRecordID
	{
		get
		{
			return _ChargeRecordID;
		}
		set
		{
			ReportPropertyChanging("ChargeRecordID");
			_ChargeRecordID = StructuralObject.SetValidValue(value);
			ReportPropertyChanged("ChargeRecordID");
		}
	}

	[EdmScalarProperty(EntityKeyProperty = false, IsNullable = true)]
	[DataMember]
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

	[EdmScalarProperty(EntityKeyProperty = false, IsNullable = true)]
	[DataMember]
	public string BarCode
	{
		get
		{
			return _BarCode;
		}
		set
		{
			ReportPropertyChanging("BarCode");
			_BarCode = StructuralObject.SetValidValue(value, isNullable: true);
			ReportPropertyChanged("BarCode");
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

	public static CustomFreeRecord CreateCustomFreeRecord(int freeRecordID, int freeType, int freeTenatID, int freeMinutes, decimal freeCharge, int shiftID, string staffCode, DateTime freeTime, int chargeRecordID, bool isDelete)
	{
		CustomFreeRecord customFreeRecord = new CustomFreeRecord();
		customFreeRecord.FreeRecordID = freeRecordID;
		customFreeRecord.FreeType = freeType;
		customFreeRecord.FreeTenatID = freeTenatID;
		customFreeRecord.FreeMinutes = freeMinutes;
		customFreeRecord.FreeCharge = freeCharge;
		customFreeRecord.ShiftID = shiftID;
		customFreeRecord.StaffCode = staffCode;
		customFreeRecord.FreeTime = freeTime;
		customFreeRecord.ChargeRecordID = chargeRecordID;
		customFreeRecord.IsDelete = isDelete;
		return customFreeRecord;
	}
}
