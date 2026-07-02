using System;
using System.Data.Objects.DataClasses;
using System.Runtime.Serialization;

namespace CarPark.DB;

[Serializable]
[EdmEntityType(NamespaceName = "CarPark.DB", Name = "APS_FinishApsAcceptLog")]
[DataContract(IsReference = true)]
public class APS_FinishApsAcceptLog : EntityObject
{
	private int _FinishApsAcceptLogID;

	private int _StationID;

	private string _TicketCode;

	private DateTime _AcceptTime;

	private int _CashBoxID;

	private bool _IsBanknote;

	private int _CurrencyID;

	private string _CurrencySymbol;

	private int _CurrencyValue;

	private bool _Malfunction;

	[DataMember]
	[EdmScalarProperty(EntityKeyProperty = true, IsNullable = false)]
	public int FinishApsAcceptLogID
	{
		get
		{
			return _FinishApsAcceptLogID;
		}
		set
		{
			if (_FinishApsAcceptLogID != value)
			{
				ReportPropertyChanging("FinishApsAcceptLogID");
				_FinishApsAcceptLogID = StructuralObject.SetValidValue(value);
				ReportPropertyChanged("FinishApsAcceptLogID");
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

	[DataMember]
	[EdmScalarProperty(EntityKeyProperty = false, IsNullable = false)]
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
	public DateTime AcceptTime
	{
		get
		{
			return _AcceptTime;
		}
		set
		{
			ReportPropertyChanging("AcceptTime");
			_AcceptTime = StructuralObject.SetValidValue(value);
			ReportPropertyChanged("AcceptTime");
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

	[DataMember]
	[EdmScalarProperty(EntityKeyProperty = false, IsNullable = false)]
	public bool Malfunction
	{
		get
		{
			return _Malfunction;
		}
		set
		{
			ReportPropertyChanging("Malfunction");
			_Malfunction = StructuralObject.SetValidValue(value);
			ReportPropertyChanged("Malfunction");
		}
	}

	public static APS_FinishApsAcceptLog CreateAPS_FinishApsAcceptLog(int finishApsAcceptLogID, int stationID, string ticketCode, DateTime acceptTime, int cashBoxID, bool isBanknote, int currencyID, string currencySymbol, int currencyValue, bool malfunction)
	{
		APS_FinishApsAcceptLog aPS_FinishApsAcceptLog = new APS_FinishApsAcceptLog();
		aPS_FinishApsAcceptLog.FinishApsAcceptLogID = finishApsAcceptLogID;
		aPS_FinishApsAcceptLog.StationID = stationID;
		aPS_FinishApsAcceptLog.TicketCode = ticketCode;
		aPS_FinishApsAcceptLog.AcceptTime = acceptTime;
		aPS_FinishApsAcceptLog.CashBoxID = cashBoxID;
		aPS_FinishApsAcceptLog.IsBanknote = isBanknote;
		aPS_FinishApsAcceptLog.CurrencyID = currencyID;
		aPS_FinishApsAcceptLog.CurrencySymbol = currencySymbol;
		aPS_FinishApsAcceptLog.CurrencyValue = currencyValue;
		aPS_FinishApsAcceptLog.Malfunction = malfunction;
		return aPS_FinishApsAcceptLog;
	}
}
