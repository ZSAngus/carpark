using System;
using System.Data.Objects.DataClasses;
using System.Runtime.Serialization;
using CarPark.DB.AdditionalDataSource;
using SkyInno.UI.BindingText;

namespace CarPark.DB;

[Serializable]
[EdmEntityType(NamespaceName = "CarPark.DB", Name = "view_RentalChargeRecord")]
[DataContract(IsReference = true)]
public class view_RentalChargeRecord : EntityObject
{
	private int _TimeChargeID;

	private string _CardCode;

	private string _LicensePlate;

	private int _RentalTypeID;

	private DateTime _StartDate;

	private DateTime? _ExpireDate;

	private DateTime _ChargeTime;

	private int _ParkTypeID;

	private decimal _TotalCharge;

	private decimal? _Deposit;

	private decimal _Remain;

	private string _StaffCode;

	private int _BillType;

	[DataMember]
	[EdmScalarProperty(EntityKeyProperty = true, IsNullable = false)]
	public int TimeChargeID
	{
		get
		{
			return _TimeChargeID;
		}
		set
		{
			if (_TimeChargeID != value)
			{
				ReportPropertyChanging("TimeChargeID");
				_TimeChargeID = StructuralObject.SetValidValue(value);
				ReportPropertyChanged("TimeChargeID");
			}
		}
	}

	[EdmScalarProperty(EntityKeyProperty = true, IsNullable = false)]
	[DataMember]
	public string CardCode
	{
		get
		{
			return _CardCode;
		}
		set
		{
			if (_CardCode != value)
			{
				ReportPropertyChanging("CardCode");
				_CardCode = StructuralObject.SetValidValue(value, isNullable: false);
				ReportPropertyChanged("CardCode");
			}
		}
	}

	[DataMember]
	[EdmScalarProperty(EntityKeyProperty = false, IsNullable = true)]
	public string LicensePlate
	{
		get
		{
			return _LicensePlate;
		}
		set
		{
			ReportPropertyChanging("LicensePlate");
			_LicensePlate = StructuralObject.SetValidValue(value, isNullable: true);
			ReportPropertyChanged("LicensePlate");
		}
	}

	[EdmScalarProperty(EntityKeyProperty = true, IsNullable = false)]
	[BindingControlEditStyle(EnumEditStyle.DbComboBox, typeof(DBRentalTypeSource))]
	[DataMember]
	public int RentalTypeID
	{
		get
		{
			return _RentalTypeID;
		}
		set
		{
			if (_RentalTypeID != value)
			{
				ReportPropertyChanging("RentalTypeID");
				_RentalTypeID = StructuralObject.SetValidValue(value);
				ReportPropertyChanged("RentalTypeID");
			}
		}
	}

	[DataMember]
	[EdmScalarProperty(EntityKeyProperty = true, IsNullable = false)]
	public DateTime StartDate
	{
		get
		{
			return _StartDate;
		}
		set
		{
			if (_StartDate != value)
			{
				ReportPropertyChanging("StartDate");
				_StartDate = StructuralObject.SetValidValue(value);
				ReportPropertyChanged("StartDate");
			}
		}
	}

	[EdmScalarProperty(EntityKeyProperty = false, IsNullable = true)]
	[DataMember]
	public DateTime? ExpireDate
	{
		get
		{
			return _ExpireDate;
		}
		set
		{
			ReportPropertyChanging("ExpireDate");
			_ExpireDate = StructuralObject.SetValidValue(value);
			ReportPropertyChanged("ExpireDate");
		}
	}

	[EdmScalarProperty(EntityKeyProperty = true, IsNullable = false)]
	[DataMember]
	public DateTime ChargeTime
	{
		get
		{
			return _ChargeTime;
		}
		set
		{
			if (_ChargeTime != value)
			{
				ReportPropertyChanging("ChargeTime");
				_ChargeTime = StructuralObject.SetValidValue(value);
				ReportPropertyChanged("ChargeTime");
			}
		}
	}

	[EdmScalarProperty(EntityKeyProperty = true, IsNullable = false)]
	[BindingControlEditStyle(EnumEditStyle.EnumComboBox, typeof(EnumParkTypeSource))]
	[DataMember]
	public int ParkTypeID
	{
		get
		{
			return _ParkTypeID;
		}
		set
		{
			if (_ParkTypeID != value)
			{
				ReportPropertyChanging("ParkTypeID");
				_ParkTypeID = StructuralObject.SetValidValue(value);
				ReportPropertyChanged("ParkTypeID");
			}
		}
	}

	[EdmScalarProperty(EntityKeyProperty = true, IsNullable = false)]
	[DataMember]
	public decimal TotalCharge
	{
		get
		{
			return _TotalCharge;
		}
		set
		{
			if (_TotalCharge != value)
			{
				ReportPropertyChanging("TotalCharge");
				_TotalCharge = StructuralObject.SetValidValue(value);
				ReportPropertyChanged("TotalCharge");
			}
		}
	}

	[DataMember]
	[EdmScalarProperty(EntityKeyProperty = false, IsNullable = true)]
	public decimal? Deposit
	{
		get
		{
			return _Deposit;
		}
		set
		{
			ReportPropertyChanging("Deposit");
			_Deposit = StructuralObject.SetValidValue(value);
			ReportPropertyChanged("Deposit");
		}
	}

	[EdmScalarProperty(EntityKeyProperty = true, IsNullable = false)]
	[DataMember]
	public decimal Remain
	{
		get
		{
			return _Remain;
		}
		set
		{
			if (_Remain != value)
			{
				ReportPropertyChanging("Remain");
				_Remain = StructuralObject.SetValidValue(value);
				ReportPropertyChanged("Remain");
			}
		}
	}

	[DataMember]
	[EdmScalarProperty(EntityKeyProperty = true, IsNullable = false)]
	public string StaffCode
	{
		get
		{
			return _StaffCode;
		}
		set
		{
			if (_StaffCode != value)
			{
				ReportPropertyChanging("StaffCode");
				_StaffCode = StructuralObject.SetValidValue(value, isNullable: false);
				ReportPropertyChanged("StaffCode");
			}
		}
	}

	[EdmScalarProperty(EntityKeyProperty = true, IsNullable = false)]
	[DataMember]
	public int BillType
	{
		get
		{
			return _BillType;
		}
		set
		{
			if (_BillType != value)
			{
				ReportPropertyChanging("BillType");
				_BillType = StructuralObject.SetValidValue(value);
				ReportPropertyChanged("BillType");
			}
		}
	}

	public static view_RentalChargeRecord Createview_RentalChargeRecord(int timeChargeID, string cardCode, int rentalTypeID, DateTime startDate, DateTime chargeTime, int parkTypeID, decimal totalCharge, decimal remain, string staffCode, int billType)
	{
		view_RentalChargeRecord view_RentalChargeRecord2 = new view_RentalChargeRecord();
		view_RentalChargeRecord2.TimeChargeID = timeChargeID;
		view_RentalChargeRecord2.CardCode = cardCode;
		view_RentalChargeRecord2.RentalTypeID = rentalTypeID;
		view_RentalChargeRecord2.StartDate = startDate;
		view_RentalChargeRecord2.ChargeTime = chargeTime;
		view_RentalChargeRecord2.ParkTypeID = parkTypeID;
		view_RentalChargeRecord2.TotalCharge = totalCharge;
		view_RentalChargeRecord2.Remain = remain;
		view_RentalChargeRecord2.StaffCode = staffCode;
		view_RentalChargeRecord2.BillType = billType;
		return view_RentalChargeRecord2;
	}
}
