using System;
using System.Data.Objects.DataClasses;
using System.Runtime.Serialization;

namespace CarPark.DB;

[Serializable]
[DataContract(IsReference = true)]
[EdmEntityType(NamespaceName = "CarPark.DB", Name = "APS_FinishApsDispenseLog")]
public class APS_FinishApsDispenseLog : EntityObject
{
	private int _FinishApsDispenseLogID;

	private int _StationID;

	private string _TicketCode;

	private DateTime _DispenseTime;

	private int _CashBoxID;

	private bool _IsBanknote;

	private int _CurrencyID;

	private string _CurrencySymbol;

	private int _CurrencyValue;

	private int _OrderCount;

	private int _OutCount;

	private DateTime _LastUpdateTime;

	private int? _ReturnCount;

	[EdmScalarProperty(EntityKeyProperty = true, IsNullable = false)]
	[DataMember]
	public int FinishApsDispenseLogID
	{
		get
		{
			return _FinishApsDispenseLogID;
		}
		set
		{
			if (_FinishApsDispenseLogID != value)
			{
				ReportPropertyChanging("FinishApsDispenseLogID");
				_FinishApsDispenseLogID = StructuralObject.SetValidValue(value);
				ReportPropertyChanged("FinishApsDispenseLogID");
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
	public string TicketCode
	{
		get
		{
			return _TicketCode;
		}
		set
		{
			ReportPropertyChanging("TicketCode");
			_TicketCode = StructuralObject.SetValidValue(value, isNullable: false);
			ReportPropertyChanged("TicketCode");
		}
	}

	[DataMember]
	[EdmScalarProperty(EntityKeyProperty = false, IsNullable = false)]
	public DateTime DispenseTime
	{
		get
		{
			return _DispenseTime;
		}
		set
		{
			ReportPropertyChanging("DispenseTime");
			_DispenseTime = StructuralObject.SetValidValue(value);
			ReportPropertyChanged("DispenseTime");
		}
	}

	[DataMember]
	[EdmScalarProperty(EntityKeyProperty = false, IsNullable = false)]
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
	public bool IsBanknote
	{
		get
		{
			return _IsBanknote;
		}
		set
		{
			ReportPropertyChanging("IsBanknote");
			_IsBanknote = StructuralObject.SetValidValue(value);
			ReportPropertyChanged("IsBanknote");
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
	public string CurrencySymbol
	{
		get
		{
			return _CurrencySymbol;
		}
		set
		{
			ReportPropertyChanging("CurrencySymbol");
			_CurrencySymbol = StructuralObject.SetValidValue(value, isNullable: false);
			ReportPropertyChanged("CurrencySymbol");
		}
	}

	[EdmScalarProperty(EntityKeyProperty = false, IsNullable = false)]
	[DataMember]
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
	public int OrderCount
	{
		get
		{
			return _OrderCount;
		}
		set
		{
			ReportPropertyChanging("OrderCount");
			_OrderCount = StructuralObject.SetValidValue(value);
			ReportPropertyChanged("OrderCount");
		}
	}

	[EdmScalarProperty(EntityKeyProperty = false, IsNullable = false)]
	[DataMember]
	public int OutCount
	{
		get
		{
			return _OutCount;
		}
		set
		{
			ReportPropertyChanging("OutCount");
			_OutCount = StructuralObject.SetValidValue(value);
			ReportPropertyChanged("OutCount");
		}
	}

	[DataMember]
	[EdmScalarProperty(EntityKeyProperty = false, IsNullable = false)]
	public DateTime LastUpdateTime
	{
		get
		{
			return _LastUpdateTime;
		}
		set
		{
			ReportPropertyChanging("LastUpdateTime");
			_LastUpdateTime = StructuralObject.SetValidValue(value);
			ReportPropertyChanged("LastUpdateTime");
		}
	}

	[EdmScalarProperty(EntityKeyProperty = false, IsNullable = true)]
	[DataMember]
	public int? ReturnCount
	{
		get
		{
			return _ReturnCount;
		}
		set
		{
			ReportPropertyChanging("ReturnCount");
			_ReturnCount = StructuralObject.SetValidValue(value);
			ReportPropertyChanged("ReturnCount");
		}
	}

	public static APS_FinishApsDispenseLog CreateAPS_FinishApsDispenseLog(int finishApsDispenseLogID, int stationID, string ticketCode, DateTime dispenseTime, int cashBoxID, bool isBanknote, int currencyID, string currencySymbol, int currencyValue, int orderCount, int outCount, DateTime lastUpdateTime)
	{
		APS_FinishApsDispenseLog aPS_FinishApsDispenseLog = new APS_FinishApsDispenseLog();
		aPS_FinishApsDispenseLog.FinishApsDispenseLogID = finishApsDispenseLogID;
		aPS_FinishApsDispenseLog.StationID = stationID;
		aPS_FinishApsDispenseLog.TicketCode = ticketCode;
		aPS_FinishApsDispenseLog.DispenseTime = dispenseTime;
		aPS_FinishApsDispenseLog.CashBoxID = cashBoxID;
		aPS_FinishApsDispenseLog.IsBanknote = isBanknote;
		aPS_FinishApsDispenseLog.CurrencyID = currencyID;
		aPS_FinishApsDispenseLog.CurrencySymbol = currencySymbol;
		aPS_FinishApsDispenseLog.CurrencyValue = currencyValue;
		aPS_FinishApsDispenseLog.OrderCount = orderCount;
		aPS_FinishApsDispenseLog.OutCount = outCount;
		aPS_FinishApsDispenseLog.LastUpdateTime = lastUpdateTime;
		return aPS_FinishApsDispenseLog;
	}
}
