using System;
using System.Data.Objects.DataClasses;
using System.Runtime.Serialization;
using SkyInno.Lang;

namespace CarPark.DB;

[Serializable]
[DataContract(IsReference = true)]
[EdmEntityType(NamespaceName = "CarPark.DB", Name = "view_Transaction")]
public class view_Transaction : EntityObject
{
	private int _TransactionID;

	private int _ParkTypeID;

	private string _InCardCode;

	private DateTime _InTime;

	private int _InGateID;

	private string _OutCardCode;

	private DateTime? _OutTime;

	private int? _OutGateID;

	private int _ParkMin;

	private int _TransactionBillType;

	private int? _RentalType;

	private int? _AreaID;

	private string _AnalysisResult;

	private int? _CardTypeID;

	private string _LicensePlate;

	private int? _RentalTypeID;

	private int _TransactionStatus;

	public string HourlyOrRental
	{
		get
		{
			if (!RentalType.HasValue)
			{
				return LangManager.GetLangString("Report.Hourly");
			}
			return LangManager.GetLangString("Report.Rental");
		}
	}

	[DataMember]
	[EdmScalarProperty(EntityKeyProperty = true, IsNullable = false)]
	public int TransactionID
	{
		get
		{
			return _TransactionID;
		}
		set
		{
			if (_TransactionID != value)
			{
				ReportPropertyChanging("TransactionID");
				_TransactionID = StructuralObject.SetValidValue(value);
				ReportPropertyChanged("TransactionID");
			}
		}
	}

	[EdmScalarProperty(EntityKeyProperty = true, IsNullable = false)]
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

	[DataMember]
	[EdmScalarProperty(EntityKeyProperty = true, IsNullable = false)]
	public string InCardCode
	{
		get
		{
			return _InCardCode;
		}
		set
		{
			if (_InCardCode != value)
			{
				ReportPropertyChanging("InCardCode");
				_InCardCode = StructuralObject.SetValidValue(value, isNullable: false);
				ReportPropertyChanged("InCardCode");
			}
		}
	}

	[EdmScalarProperty(EntityKeyProperty = true, IsNullable = false)]
	[DataMember]
	public DateTime InTime
	{
		get
		{
			return _InTime;
		}
		set
		{
			if (_InTime != value)
			{
				ReportPropertyChanging("InTime");
				_InTime = StructuralObject.SetValidValue(value);
				ReportPropertyChanged("InTime");
			}
		}
	}

	[DataMember]
	[EdmScalarProperty(EntityKeyProperty = true, IsNullable = false)]
	public int InGateID
	{
		get
		{
			return _InGateID;
		}
		set
		{
			if (_InGateID != value)
			{
				ReportPropertyChanging("InGateID");
				_InGateID = StructuralObject.SetValidValue(value);
				ReportPropertyChanged("InGateID");
			}
		}
	}

	[EdmScalarProperty(EntityKeyProperty = false, IsNullable = true)]
	[DataMember]
	public string OutCardCode
	{
		get
		{
			return _OutCardCode;
		}
		set
		{
			ReportPropertyChanging("OutCardCode");
			_OutCardCode = StructuralObject.SetValidValue(value, isNullable: true);
			ReportPropertyChanged("OutCardCode");
		}
	}

	[EdmScalarProperty(EntityKeyProperty = false, IsNullable = true)]
	[DataMember]
	public DateTime? OutTime
	{
		get
		{
			return _OutTime;
		}
		set
		{
			ReportPropertyChanging("OutTime");
			_OutTime = StructuralObject.SetValidValue(value);
			ReportPropertyChanged("OutTime");
		}
	}

	[DataMember]
	[EdmScalarProperty(EntityKeyProperty = false, IsNullable = true)]
	public int? OutGateID
	{
		get
		{
			return _OutGateID;
		}
		set
		{
			ReportPropertyChanging("OutGateID");
			_OutGateID = StructuralObject.SetValidValue(value);
			ReportPropertyChanged("OutGateID");
		}
	}

	[DataMember]
	[EdmScalarProperty(EntityKeyProperty = true, IsNullable = false)]
	public int ParkMin
	{
		get
		{
			return _ParkMin;
		}
		set
		{
			if (_ParkMin != value)
			{
				ReportPropertyChanging("ParkMin");
				_ParkMin = StructuralObject.SetValidValue(value);
				ReportPropertyChanged("ParkMin");
			}
		}
	}

	[DataMember]
	[EdmScalarProperty(EntityKeyProperty = true, IsNullable = false)]
	public int TransactionBillType
	{
		get
		{
			return _TransactionBillType;
		}
		set
		{
			if (_TransactionBillType != value)
			{
				ReportPropertyChanging("TransactionBillType");
				_TransactionBillType = StructuralObject.SetValidValue(value);
				ReportPropertyChanged("TransactionBillType");
			}
		}
	}

	[DataMember]
	[EdmScalarProperty(EntityKeyProperty = false, IsNullable = true)]
	public int? RentalType
	{
		get
		{
			return _RentalType;
		}
		set
		{
			ReportPropertyChanging("RentalType");
			_RentalType = StructuralObject.SetValidValue(value);
			ReportPropertyChanged("RentalType");
		}
	}

	[DataMember]
	[EdmScalarProperty(EntityKeyProperty = false, IsNullable = true)]
	public int? AreaID
	{
		get
		{
			return _AreaID;
		}
		set
		{
			ReportPropertyChanging("AreaID");
			_AreaID = StructuralObject.SetValidValue(value);
			ReportPropertyChanged("AreaID");
		}
	}

	[DataMember]
	[EdmScalarProperty(EntityKeyProperty = false, IsNullable = true)]
	public string AnalysisResult
	{
		get
		{
			return _AnalysisResult;
		}
		set
		{
			ReportPropertyChanging("AnalysisResult");
			_AnalysisResult = StructuralObject.SetValidValue(value, isNullable: true);
			ReportPropertyChanged("AnalysisResult");
		}
	}

	[EdmScalarProperty(EntityKeyProperty = false, IsNullable = true)]
	[DataMember]
	public int? CardTypeID
	{
		get
		{
			return _CardTypeID;
		}
		set
		{
			ReportPropertyChanging("CardTypeID");
			_CardTypeID = StructuralObject.SetValidValue(value);
			ReportPropertyChanged("CardTypeID");
		}
	}

	[EdmScalarProperty(EntityKeyProperty = false, IsNullable = true)]
	[DataMember]
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

	[EdmScalarProperty(EntityKeyProperty = false, IsNullable = true)]
	[DataMember]
	public int? RentalTypeID
	{
		get
		{
			return _RentalTypeID;
		}
		set
		{
			ReportPropertyChanging("RentalTypeID");
			_RentalTypeID = StructuralObject.SetValidValue(value);
			ReportPropertyChanged("RentalTypeID");
		}
	}

	[EdmScalarProperty(EntityKeyProperty = true, IsNullable = false)]
	[DataMember]
	public int TransactionStatus
	{
		get
		{
			return _TransactionStatus;
		}
		set
		{
			if (_TransactionStatus != value)
			{
				ReportPropertyChanging("TransactionStatus");
				_TransactionStatus = StructuralObject.SetValidValue(value);
				ReportPropertyChanged("TransactionStatus");
			}
		}
	}

	public static view_Transaction Createview_Transaction(int transactionID, int parkTypeID, string inCardCode, DateTime inTime, int inGateID, int parkMin, int transactionBillType, int transactionStatus)
	{
		view_Transaction view_Transaction2 = new view_Transaction();
		view_Transaction2.TransactionID = transactionID;
		view_Transaction2.ParkTypeID = parkTypeID;
		view_Transaction2.InCardCode = inCardCode;
		view_Transaction2.InTime = inTime;
		view_Transaction2.InGateID = inGateID;
		view_Transaction2.ParkMin = parkMin;
		view_Transaction2.TransactionBillType = transactionBillType;
		view_Transaction2.TransactionStatus = transactionStatus;
		return view_Transaction2;
	}
}
