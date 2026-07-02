using System;
using System.Data.Objects.DataClasses;
using System.Runtime.Serialization;

namespace CarPark.DB;

[Serializable]
[DataContract(IsReference = true)]
[EdmEntityType(NamespaceName = "CarPark.DB", Name = "APS_ShiftRecordDetailed")]
public class APS_ShiftRecordDetailed : EntityObject
{
	private int _ShiftRecord_ID;

	private int _CurrencyID;

	private int _APSID;

	private int _CashBoxTypeID;

	private int _ShiftID;

	private int _CurrencyValue;

	private int _Quantity;

	private decimal _Amount;

	[DataMember]
	[EdmScalarProperty(EntityKeyProperty = true, IsNullable = false)]
	public int ShiftRecord_ID
	{
		get
		{
			return _ShiftRecord_ID;
		}
		set
		{
			if (_ShiftRecord_ID != value)
			{
				ReportPropertyChanging("ShiftRecord_ID");
				_ShiftRecord_ID = StructuralObject.SetValidValue(value);
				ReportPropertyChanged("ShiftRecord_ID");
			}
		}
	}

	[EdmScalarProperty(EntityKeyProperty = false, IsNullable = false)]
	[DataMember]
	public int CurrencyID
	{
		get
		{
			return _CurrencyID;
		}
		set
		{
			ReportPropertyChanging("CurrencyID");
			_CurrencyID = StructuralObject.SetValidValue(value);
			ReportPropertyChanged("CurrencyID");
		}
	}

	[DataMember]
	[EdmScalarProperty(EntityKeyProperty = false, IsNullable = false)]
	public int APSID
	{
		get
		{
			return _APSID;
		}
		set
		{
			ReportPropertyChanging("APSID");
			_APSID = StructuralObject.SetValidValue(value);
			ReportPropertyChanged("APSID");
		}
	}

	[DataMember]
	[EdmScalarProperty(EntityKeyProperty = false, IsNullable = false)]
	public int CashBoxTypeID
	{
		get
		{
			return _CashBoxTypeID;
		}
		set
		{
			ReportPropertyChanging("CashBoxTypeID");
			_CashBoxTypeID = StructuralObject.SetValidValue(value);
			ReportPropertyChanged("CashBoxTypeID");
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
	public int CurrencyValue
	{
		get
		{
			return _CurrencyValue;
		}
		set
		{
			ReportPropertyChanging("CurrencyValue");
			_CurrencyValue = StructuralObject.SetValidValue(value);
			ReportPropertyChanged("CurrencyValue");
		}
	}

	[EdmScalarProperty(EntityKeyProperty = false, IsNullable = false)]
	[DataMember]
	public int Quantity
	{
		get
		{
			return _Quantity;
		}
		set
		{
			ReportPropertyChanging("Quantity");
			_Quantity = StructuralObject.SetValidValue(value);
			ReportPropertyChanged("Quantity");
		}
	}

	[DataMember]
	[EdmScalarProperty(EntityKeyProperty = false, IsNullable = false)]
	public decimal Amount
	{
		get
		{
			return _Amount;
		}
		set
		{
			ReportPropertyChanging("Amount");
			_Amount = StructuralObject.SetValidValue(value);
			ReportPropertyChanged("Amount");
		}
	}

	public static APS_ShiftRecordDetailed CreateAPS_ShiftRecordDetailed(int shiftRecord_ID, int currencyID, int aPSID, int cashBoxTypeID, int shiftID, int currencyValue, int quantity, decimal amount)
	{
		APS_ShiftRecordDetailed aPS_ShiftRecordDetailed = new APS_ShiftRecordDetailed();
		aPS_ShiftRecordDetailed.ShiftRecord_ID = shiftRecord_ID;
		aPS_ShiftRecordDetailed.CurrencyID = currencyID;
		aPS_ShiftRecordDetailed.APSID = aPSID;
		aPS_ShiftRecordDetailed.CashBoxTypeID = cashBoxTypeID;
		aPS_ShiftRecordDetailed.ShiftID = shiftID;
		aPS_ShiftRecordDetailed.CurrencyValue = currencyValue;
		aPS_ShiftRecordDetailed.Quantity = quantity;
		aPS_ShiftRecordDetailed.Amount = amount;
		return aPS_ShiftRecordDetailed;
	}
}
