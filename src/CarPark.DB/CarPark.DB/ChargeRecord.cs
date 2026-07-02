using System;
using System.Collections.Generic;
using System.Data.Objects.DataClasses;
using System.Runtime.Serialization;
using CarPark.DB.AdditionalDataSource;
using SkyInno.UI.BindingText;

namespace CarPark.DB;

[Serializable]
[DataContract(IsReference = true)]
[EdmEntityType(NamespaceName = "CarPark.DB", Name = "ChargeRecord")]
public class ChargeRecord : EntityObject
{
	private DateTime? _StartDate;

	private DateTime? _EndDate;

	private string _Remark2;

	private int? _PayType;

	private int _TimeChargeID;

	private DateTime _ChargeTime;

	private string _StaffCode;

	private decimal _TotalCharge;

	private int _FreeMin;

	private decimal _FreeCharge;

	private int? _TransactionID;

	private int _BillType;

	private int _ShiftID;

	private int _ChargeMin;

	private int _ParkMin;

	private string _CardCode;

	private int _ParkTypeID;

	private string _Remark;

	private string _FromStation;

	private string _PeriodofTime;

	private int? _BufferTime;

	private string _Currency;

	private int? _FirstTime;

	private decimal? _Fine;

	private bool _IsDelete;

	private int? _subPayType;

	private string _AuthCode;

	[EdmScalarProperty(EntityKeyProperty = false, IsNullable = true)]
	[DataMember]
	public DateTime? StartDate
	{
		get
		{
			return _StartDate;
		}
		set
		{
			ReportPropertyChanging("StartDate");
			_StartDate = StructuralObject.SetValidValue(value);
			ReportPropertyChanged("StartDate");
		}
	}

	[EdmScalarProperty(EntityKeyProperty = false, IsNullable = true)]
	[DataMember]
	public DateTime? EndDate
	{
		get
		{
			return _EndDate;
		}
		set
		{
			ReportPropertyChanging("EndDate");
			_EndDate = StructuralObject.SetValidValue(value);
			ReportPropertyChanged("EndDate");
		}
	}

	[DataMember]
	[EdmScalarProperty(EntityKeyProperty = false, IsNullable = true)]
	public string Remark2
	{
		get
		{
			return _Remark2;
		}
		set
		{
			ReportPropertyChanging("Remark2");
			_Remark2 = StructuralObject.SetValidValue(value, isNullable: true);
			ReportPropertyChanged("Remark2");
		}
	}

	[BindingControlEditStyle(EnumEditStyle.EnumComboBox, typeof(EnumPayTypeSource))]
	[EdmScalarProperty(EntityKeyProperty = false, IsNullable = true)]
	[DataMember]
	public int? PayType
	{
		get
		{
			return _PayType;
		}
		set
		{
			ReportPropertyChanging("PayType");
			_PayType = StructuralObject.SetValidValue(value);
			ReportPropertyChanged("PayType");
		}
	}

	public List<PeriodofTimeInfo> PeriodofTimeInfoList { get; set; }

	public string GetPeriodofTime
	{
		get
		{
			string text = string.Empty;
			if (PeriodofTimeInfoList != null)
			{
				foreach (PeriodofTimeInfo periodofTimeInfo in PeriodofTimeInfoList)
				{
					text += $"{periodofTimeInfo.PeriodofTimeName}@{periodofTimeInfo._Minute}#{periodofTimeInfo._Money};";
				}
			}
			return text;
		}
	}

	[EdmScalarProperty(EntityKeyProperty = true, IsNullable = false)]
	[DataMember]
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

	[EdmScalarProperty(EntityKeyProperty = false, IsNullable = false)]
	[DataMember]
	public DateTime ChargeTime
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
	public decimal TotalCharge
	{
		get
		{
			return _TotalCharge;
		}
		set
		{
			ReportPropertyChanging("TotalCharge");
			_TotalCharge = StructuralObject.SetValidValue(value);
			ReportPropertyChanged("TotalCharge");
		}
	}

	[DataMember]
	[EdmScalarProperty(EntityKeyProperty = false, IsNullable = false)]
	public int FreeMin
	{
		get
		{
			return _FreeMin;
		}
		set
		{
			ReportPropertyChanging("FreeMin");
			_FreeMin = StructuralObject.SetValidValue(value);
			ReportPropertyChanged("FreeMin");
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

	[EdmScalarProperty(EntityKeyProperty = false, IsNullable = true)]
	[DataMember]
	public int? TransactionID
	{
		get
		{
			return _TransactionID;
		}
		set
		{
			ReportPropertyChanging("TransactionID");
			_TransactionID = StructuralObject.SetValidValue(value);
			ReportPropertyChanged("TransactionID");
		}
	}

	[DataMember]
	[EdmScalarProperty(EntityKeyProperty = false, IsNullable = false)]
	[BindingControlEditStyle(EnumEditStyle.EnumComboBox, typeof(EnumBillTypeSource))]
	public int BillType
	{
		get
		{
			return _BillType;
		}
		set
		{
			ReportPropertyChanging("BillType");
			_BillType = StructuralObject.SetValidValue(value);
			ReportPropertyChanged("BillType");
		}
	}

	[DataMember]
	[EdmScalarProperty(EntityKeyProperty = false, IsNullable = false)]
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
	public int ChargeMin
	{
		get
		{
			return _ChargeMin;
		}
		set
		{
			ReportPropertyChanging("ChargeMin");
			_ChargeMin = StructuralObject.SetValidValue(value);
			ReportPropertyChanged("ChargeMin");
		}
	}

	[EdmScalarProperty(EntityKeyProperty = false, IsNullable = false)]
	[DataMember]
	public int ParkMin
	{
		get
		{
			return _ParkMin;
		}
		set
		{
			ReportPropertyChanging("ParkMin");
			_ParkMin = StructuralObject.SetValidValue(value);
			ReportPropertyChanged("ParkMin");
		}
	}

	[DataMember]
	[EdmScalarProperty(EntityKeyProperty = false, IsNullable = false)]
	public string CardCode
	{
		get
		{
			return _CardCode;
		}
		set
		{
			ReportPropertyChanging("CardCode");
			_CardCode = StructuralObject.SetValidValue(value, isNullable: false);
			ReportPropertyChanged("CardCode");
		}
	}

	[EdmScalarProperty(EntityKeyProperty = false, IsNullable = false)]
	[DataMember]
	[BindingControlEditStyle(EnumEditStyle.EnumComboBox, typeof(EnumParkTypeAllSource))]
	public int ParkTypeID
	{
		get
		{
			return _ParkTypeID;
		}
		set
		{
			ReportPropertyChanging("ParkTypeID");
			_ParkTypeID = StructuralObject.SetValidValue(value);
			ReportPropertyChanged("ParkTypeID");
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

	[DataMember]
	[EdmScalarProperty(EntityKeyProperty = false, IsNullable = true)]
	public string FromStation
	{
		get
		{
			return _FromStation;
		}
		set
		{
			ReportPropertyChanging("FromStation");
			_FromStation = StructuralObject.SetValidValue(value, isNullable: true);
			ReportPropertyChanged("FromStation");
		}
	}

	[EdmScalarProperty(EntityKeyProperty = false, IsNullable = true)]
	[DataMember]
	public string PeriodofTime
	{
		get
		{
			return _PeriodofTime;
		}
		set
		{
			ReportPropertyChanging("PeriodofTime");
			_PeriodofTime = StructuralObject.SetValidValue(value, isNullable: true);
			ReportPropertyChanged("PeriodofTime");
		}
	}

	[DataMember]
	[EdmScalarProperty(EntityKeyProperty = false, IsNullable = true)]
	public int? BufferTime
	{
		get
		{
			return _BufferTime;
		}
		set
		{
			ReportPropertyChanging("BufferTime");
			_BufferTime = StructuralObject.SetValidValue(value);
			ReportPropertyChanged("BufferTime");
		}
	}

	[EdmScalarProperty(EntityKeyProperty = false, IsNullable = true)]
	[BindingControlEditStyle(EnumEditStyle.EnumComboBox, typeof(DBEnumCurrencySource))]
	[DataMember]
	public string Currency
	{
		get
		{
			return _Currency;
		}
		set
		{
			ReportPropertyChanging("Currency");
			_Currency = StructuralObject.SetValidValue(value, isNullable: true);
			ReportPropertyChanged("Currency");
		}
	}

	[EdmScalarProperty(EntityKeyProperty = false, IsNullable = true)]
	[DataMember]
	public int? FirstTime
	{
		get
		{
			return _FirstTime;
		}
		set
		{
			ReportPropertyChanging("FirstTime");
			_FirstTime = StructuralObject.SetValidValue(value);
			ReportPropertyChanged("FirstTime");
		}
	}

	[DataMember]
	[EdmScalarProperty(EntityKeyProperty = false, IsNullable = true)]
	public decimal? Fine
	{
		get
		{
			return _Fine;
		}
		set
		{
			ReportPropertyChanging("Fine");
			_Fine = StructuralObject.SetValidValue(value);
			ReportPropertyChanged("Fine");
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

	[DataMember]
	[EdmScalarProperty(EntityKeyProperty = false, IsNullable = true)]
	public int? subPayType
	{
		get
		{
			return _subPayType;
		}
		set
		{
			ReportPropertyChanging("subPayType");
			_subPayType = StructuralObject.SetValidValue(value);
			ReportPropertyChanged("subPayType");
		}
	}

	[DataMember]
	[EdmScalarProperty(EntityKeyProperty = false, IsNullable = true)]
	public string AuthCode
	{
		get
		{
			return _AuthCode;
		}
		set
		{
			ReportPropertyChanging("AuthCode");
			_AuthCode = StructuralObject.SetValidValue(value, isNullable: true);
			ReportPropertyChanged("AuthCode");
		}
	}

	public TimeSpan ParkTimeSpan { get; set; }

	public bool IsOverTime => ChargeTime.AddMinutes(DataBuffer.CompanyInfo.ExitTimeOutMin) < DateTime.Now;

	public static ChargeRecord CreateChargeRecord(int timeChargeID, DateTime chargeTime, string staffCode, decimal totalCharge, int freeMin, decimal freeCharge, int billType, int shiftID, int chargeMin, int parkMin, string cardCode, int parkTypeID, string fromStation, bool isDelete)
	{
		ChargeRecord chargeRecord = new ChargeRecord();
		chargeRecord.TimeChargeID = timeChargeID;
		chargeRecord.ChargeTime = chargeTime;
		chargeRecord.StaffCode = staffCode;
		chargeRecord.TotalCharge = totalCharge;
		chargeRecord.FreeMin = freeMin;
		chargeRecord.FreeCharge = freeCharge;
		chargeRecord.BillType = billType;
		chargeRecord.ShiftID = shiftID;
		chargeRecord.ChargeMin = chargeMin;
		chargeRecord.ParkMin = parkMin;
		chargeRecord.CardCode = cardCode;
		chargeRecord.ParkTypeID = parkTypeID;
		chargeRecord.FromStation = fromStation;
		chargeRecord.IsDelete = isDelete;
		return chargeRecord;
	}
}
