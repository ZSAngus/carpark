using System;
using System.Data.Objects.DataClasses;
using System.Runtime.Serialization;

namespace CarPark.DB;

[Serializable]
[DataContract(IsReference = true)]
[EdmEntityType(NamespaceName = "CarPark.DB", Name = "AdminCharge")]
public class AdminCharge : EntityObject
{
	private int _ID;

	private int? _ChargeTypeID;

	private decimal? _ChargeAmount;

	private int? _BillTypeID;

	private DateTime? _ChargeTime;

	private int? _CustomID;

	private int? _CardID;

	private string _Remark;

	private string _CreateStaffCode;

	private DateTime? _CreateDate;

	[DataMember]
	[EdmScalarProperty(EntityKeyProperty = true, IsNullable = false)]
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

	[DataMember]
	[EdmScalarProperty(EntityKeyProperty = false, IsNullable = true)]
	public int? ChargeTypeID
	{
		get
		{
			return _ChargeTypeID;
		}
		set
		{
			ReportPropertyChanging("ChargeTypeID");
			_ChargeTypeID = StructuralObject.SetValidValue(value);
			ReportPropertyChanged("ChargeTypeID");
		}
	}

	[DataMember]
	[EdmScalarProperty(EntityKeyProperty = false, IsNullable = true)]
	public decimal? ChargeAmount
	{
		get
		{
			return _ChargeAmount;
		}
		set
		{
			ReportPropertyChanging("ChargeAmount");
			_ChargeAmount = StructuralObject.SetValidValue(value);
			ReportPropertyChanged("ChargeAmount");
		}
	}

	[EdmScalarProperty(EntityKeyProperty = false, IsNullable = true)]
	[DataMember]
	public int? BillTypeID
	{
		get
		{
			return _BillTypeID;
		}
		set
		{
			ReportPropertyChanging("BillTypeID");
			_BillTypeID = StructuralObject.SetValidValue(value);
			ReportPropertyChanged("BillTypeID");
		}
	}

	[EdmScalarProperty(EntityKeyProperty = false, IsNullable = true)]
	[DataMember]
	public DateTime? ChargeTime
	{
		get
		{
			return _ChargeTime;
		}
		set
		{
			ReportPropertyChanging("ChargeTime");
			_ChargeTime = StructuralObject.SetValidValue(value);
			ReportPropertyChanged("ChargeTime");
		}
	}

	[EdmScalarProperty(EntityKeyProperty = false, IsNullable = true)]
	[DataMember]
	public int? CustomID
	{
		get
		{
			return _CustomID;
		}
		set
		{
			ReportPropertyChanging("CustomID");
			_CustomID = StructuralObject.SetValidValue(value);
			ReportPropertyChanged("CustomID");
		}
	}

	[EdmScalarProperty(EntityKeyProperty = false, IsNullable = true)]
	[DataMember]
	public int? CardID
	{
		get
		{
			return _CardID;
		}
		set
		{
			ReportPropertyChanging("CardID");
			_CardID = StructuralObject.SetValidValue(value);
			ReportPropertyChanged("CardID");
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
	public string CreateStaffCode
	{
		get
		{
			return _CreateStaffCode;
		}
		set
		{
			ReportPropertyChanging("CreateStaffCode");
			_CreateStaffCode = StructuralObject.SetValidValue(value, isNullable: true);
			ReportPropertyChanged("CreateStaffCode");
		}
	}

	[EdmScalarProperty(EntityKeyProperty = false, IsNullable = true)]
	[DataMember]
	public DateTime? CreateDate
	{
		get
		{
			return _CreateDate;
		}
		set
		{
			ReportPropertyChanging("CreateDate");
			_CreateDate = StructuralObject.SetValidValue(value);
			ReportPropertyChanged("CreateDate");
		}
	}

	public static AdminCharge CreateAdminCharge(int id)
	{
		AdminCharge adminCharge = new AdminCharge();
		adminCharge.ID = id;
		return adminCharge;
	}
}
