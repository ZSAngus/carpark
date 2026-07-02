using System;
using System.Data.Objects.DataClasses;
using System.Runtime.Serialization;

namespace CarPark.DB;

[Serializable]
[DataContract(IsReference = true)]
[EdmEntityType(NamespaceName = "CarPark.DB", Name = "APS_CashBoxDispenser")]
public class APS_CashBoxDispenser : EntityObject
{
	private int _CashBoxDispenserID;

	private int _StationID;

	private int _CashBoxID;

	private int _CashBoxTypeID;

	private int _CurrencyID;

	private int _CurrencyValue;

	private int _RemainQty;

	private int _LowlevelQty;

	private int _ResetQty;

	private int _ForceChange;

	private bool _IsDeleted;

	[DataMember]
	[EdmScalarProperty(EntityKeyProperty = true, IsNullable = false)]
	public int CashBoxDispenserID
	{
		get
		{
			return _CashBoxDispenserID;
		}
		set
		{
			if (_CashBoxDispenserID != value)
			{
				ReportPropertyChanging("CashBoxDispenserID");
				_CashBoxDispenserID = StructuralObject.SetValidValue(value);
				ReportPropertyChanged("CashBoxDispenserID");
			}
		}
	}

	[DataMember]
	[EdmScalarProperty(EntityKeyProperty = false, IsNullable = false)]
	public int StationID
	{
		get
		{
			return _StationID;
		}
		set
		{
			ReportPropertyChanging("StationID");
			_StationID = StructuralObject.SetValidValue(value);
			ReportPropertyChanged("StationID");
		}
	}

	[EdmScalarProperty(EntityKeyProperty = false, IsNullable = false)]
	[DataMember]
	public int CashBoxID
	{
		get
		{
			return _CashBoxID;
		}
		set
		{
			ReportPropertyChanging("CashBoxID");
			_CashBoxID = StructuralObject.SetValidValue(value);
			ReportPropertyChanged("CashBoxID");
		}
	}

	[EdmScalarProperty(EntityKeyProperty = false, IsNullable = false)]
	[DataMember]
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

	[DataMember]
	[EdmScalarProperty(EntityKeyProperty = false, IsNullable = false)]
	public int RemainQty
	{
		get
		{
			return _RemainQty;
		}
		set
		{
			ReportPropertyChanging("RemainQty");
			_RemainQty = StructuralObject.SetValidValue(value);
			ReportPropertyChanged("RemainQty");
		}
	}

	[EdmScalarProperty(EntityKeyProperty = false, IsNullable = false)]
	[DataMember]
	public int LowlevelQty
	{
		get
		{
			return _LowlevelQty;
		}
		set
		{
			ReportPropertyChanging("LowlevelQty");
			_LowlevelQty = StructuralObject.SetValidValue(value);
			ReportPropertyChanged("LowlevelQty");
		}
	}

	[EdmScalarProperty(EntityKeyProperty = false, IsNullable = false)]
	[DataMember]
	public int ResetQty
	{
		get
		{
			return _ResetQty;
		}
		set
		{
			ReportPropertyChanging("ResetQty");
			_ResetQty = StructuralObject.SetValidValue(value);
			ReportPropertyChanged("ResetQty");
		}
	}

	[EdmScalarProperty(EntityKeyProperty = false, IsNullable = false)]
	[DataMember]
	public int ForceChange
	{
		get
		{
			return _ForceChange;
		}
		set
		{
			ReportPropertyChanging("ForceChange");
			_ForceChange = StructuralObject.SetValidValue(value);
			ReportPropertyChanged("ForceChange");
		}
	}

	[DataMember]
	[EdmScalarProperty(EntityKeyProperty = false, IsNullable = false)]
	public bool IsDeleted
	{
		get
		{
			return _IsDeleted;
		}
		set
		{
			ReportPropertyChanging("IsDeleted");
			_IsDeleted = StructuralObject.SetValidValue(value);
			ReportPropertyChanged("IsDeleted");
		}
	}

	public static APS_CashBoxDispenser CreateAPS_CashBoxDispenser(int cashBoxDispenserID, int stationID, int cashBoxID, int cashBoxTypeID, int currencyID, int currencyValue, int remainQty, int lowlevelQty, int resetQty, int forceChange, bool isDeleted)
	{
		APS_CashBoxDispenser aPS_CashBoxDispenser = new APS_CashBoxDispenser();
		aPS_CashBoxDispenser.CashBoxDispenserID = cashBoxDispenserID;
		aPS_CashBoxDispenser.StationID = stationID;
		aPS_CashBoxDispenser.CashBoxID = cashBoxID;
		aPS_CashBoxDispenser.CashBoxTypeID = cashBoxTypeID;
		aPS_CashBoxDispenser.CurrencyID = currencyID;
		aPS_CashBoxDispenser.CurrencyValue = currencyValue;
		aPS_CashBoxDispenser.RemainQty = remainQty;
		aPS_CashBoxDispenser.LowlevelQty = lowlevelQty;
		aPS_CashBoxDispenser.ResetQty = resetQty;
		aPS_CashBoxDispenser.ForceChange = forceChange;
		aPS_CashBoxDispenser.IsDeleted = isDeleted;
		return aPS_CashBoxDispenser;
	}
}
