using System;
using System.Data.Objects.DataClasses;
using System.Runtime.Serialization;

namespace CarPark.DB;

[Serializable]
[DataContract(IsReference = true)]
[EdmEntityType(NamespaceName = "CarPark.DB", Name = "VoidCharge")]
public class VoidCharge : EntityObject
{
	private int _AutoID;

	private int _ChargeRecordID;

	private string _StaffCode;

	private DateTime _CreateTime;

	private decimal _VoidChargeAmt;

	private string _ImagePath;

	private string _Auditor;

	private DateTime? _AuditTime;

	private int _Status;

	private string _Remark;

	[DataMember]
	[EdmScalarProperty(EntityKeyProperty = true, IsNullable = false)]
	public int AutoID
	{
		get
		{
			return _AutoID;
		}
		set
		{
			if (_AutoID != value)
			{
				ReportPropertyChanging("AutoID");
				_AutoID = StructuralObject.SetValidValue(value);
				ReportPropertyChanged("AutoID");
			}
		}
	}

	[EdmScalarProperty(EntityKeyProperty = false, IsNullable = false)]
	[DataMember]
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
	public decimal VoidChargeAmt
	{
		get
		{
			return _VoidChargeAmt;
		}
		set
		{
			ReportPropertyChanging("VoidChargeAmt");
			_VoidChargeAmt = StructuralObject.SetValidValue(value);
			ReportPropertyChanged("VoidChargeAmt");
		}
	}

	[DataMember]
	[EdmScalarProperty(EntityKeyProperty = false, IsNullable = true)]
	public string ImagePath
	{
		get
		{
			return _ImagePath;
		}
		set
		{
			ReportPropertyChanging("ImagePath");
			_ImagePath = StructuralObject.SetValidValue(value, isNullable: true);
			ReportPropertyChanged("ImagePath");
		}
	}

	[DataMember]
	[EdmScalarProperty(EntityKeyProperty = false, IsNullable = true)]
	public string Auditor
	{
		get
		{
			return _Auditor;
		}
		set
		{
			ReportPropertyChanging("Auditor");
			_Auditor = StructuralObject.SetValidValue(value, isNullable: true);
			ReportPropertyChanged("Auditor");
		}
	}

	[DataMember]
	[EdmScalarProperty(EntityKeyProperty = false, IsNullable = true)]
	public DateTime? AuditTime
	{
		get
		{
			return _AuditTime;
		}
		set
		{
			ReportPropertyChanging("AuditTime");
			_AuditTime = StructuralObject.SetValidValue(value);
			ReportPropertyChanged("AuditTime");
		}
	}

	[EdmScalarProperty(EntityKeyProperty = false, IsNullable = false)]
	[DataMember]
	public int Status
	{
		get
		{
			return _Status;
		}
		set
		{
			ReportPropertyChanging("Status");
			_Status = StructuralObject.SetValidValue(value);
			ReportPropertyChanged("Status");
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

	public static VoidCharge CreateVoidCharge(int autoID, int chargeRecordID, string staffCode, DateTime createTime, decimal voidChargeAmt, int status)
	{
		VoidCharge voidCharge = new VoidCharge();
		voidCharge.AutoID = autoID;
		voidCharge.ChargeRecordID = chargeRecordID;
		voidCharge.StaffCode = staffCode;
		voidCharge.CreateTime = createTime;
		voidCharge.VoidChargeAmt = voidChargeAmt;
		voidCharge.Status = status;
		return voidCharge;
	}
}
