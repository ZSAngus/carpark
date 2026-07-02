using System;
using System.Data.Objects.DataClasses;
using System.Runtime.Serialization;

namespace CarPark.DB;

[Serializable]
[EdmEntityType(NamespaceName = "CarPark.DB", Name = "APS_CashBoxAcceptor")]
[DataContract(IsReference = true)]
public class APS_CashBoxAcceptor : EntityObject
{
	private int _CashBoxAcceptorID;

	private int _StationID;

	private int _CashBoxID;

	private int _CashBoxTypeID;

	private int _CurrencyID;

	private int _CurrencyValue;

	private int _ExistQty;

	private bool _IsDeleted;

	[DataMember]
	[EdmScalarProperty(EntityKeyProperty = true, IsNullable = false)]
	public int CashBoxAcceptorID
	{
		get
		{
			return _CashBoxAcceptorID;
		}
		set
		{
			if (_CashBoxAcceptorID != value)
			{
				ReportPropertyChanging("CashBoxAcceptorID");
				_CashBoxAcceptorID = StructuralObject.SetValidValue(value);
				ReportPropertyChanged("CashBoxAcceptorID");
			}
		}
	}

	[EdmScalarProperty(EntityKeyProperty = false, IsNullable = false)]
	[DataMember]
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
	public int ExistQty
	{
		get
		{
			return _ExistQty;
		}
		set
		{
			ReportPropertyChanging("ExistQty");
			_ExistQty = StructuralObject.SetValidValue(value);
			ReportPropertyChanged("ExistQty");
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

	public static APS_CashBoxAcceptor CreateAPS_CashBoxAcceptor(int cashBoxAcceptorID, int stationID, int cashBoxID, int cashBoxTypeID, int currencyID, int currencyValue, int existQty, bool isDeleted)
	{
		APS_CashBoxAcceptor aPS_CashBoxAcceptor = new APS_CashBoxAcceptor();
		aPS_CashBoxAcceptor.CashBoxAcceptorID = cashBoxAcceptorID;
		aPS_CashBoxAcceptor.StationID = stationID;
		aPS_CashBoxAcceptor.CashBoxID = cashBoxID;
		aPS_CashBoxAcceptor.CashBoxTypeID = cashBoxTypeID;
		aPS_CashBoxAcceptor.CurrencyID = currencyID;
		aPS_CashBoxAcceptor.CurrencyValue = currencyValue;
		aPS_CashBoxAcceptor.ExistQty = existQty;
		aPS_CashBoxAcceptor.IsDeleted = isDeleted;
		return aPS_CashBoxAcceptor;
	}
}
