using System;
using System.Data.Objects.DataClasses;
using System.Runtime.Serialization;
using CarPark.DB.AdditionalDataSource;
using SkyInno.Lang;
using SkyInno.UI.BindingText;

namespace CarPark.DB;

[Serializable]
[EdmEntityType(NamespaceName = "CarPark.DB", Name = "view_ComeAndGo")]
[DataContract(IsReference = true)]
public class view_ComeAndGo : EntityObject
{
	private int _TransactionID;

	private string _InCardCode;

	private int _ParkTypeID;

	private DateTime _InTime;

	private int _InGateID;

	private DateTime? _OutTime;

	private int? _OutGateID;

	private int _ParkMin;

	private int _TransactionBillType;

	private int? _RentalType;

	private string _ImagePath;

	private string _LicensePlate;

	private int? _RentalTypeID;

	private int _TransactionStatus;

	private int? _AreaID;

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

	[EdmScalarProperty(EntityKeyProperty = true, IsNullable = false)]
	[DataMember]
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

	[BindingControlEditStyle(EnumEditStyle.EnumComboBox, typeof(EnumParkTypeSource))]
	[DataMember]
	[EdmScalarProperty(EntityKeyProperty = true, IsNullable = false)]
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

	[DataMember]
	[EdmScalarProperty(EntityKeyProperty = false, IsNullable = true)]
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

	[EdmScalarProperty(EntityKeyProperty = true, IsNullable = false)]
	[DataMember]
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

	[EdmScalarProperty(EntityKeyProperty = true, IsNullable = false)]
	[DataMember]
	[BindingControlEditStyle(EnumEditStyle.EnumComboBox, typeof(EnumCardTypeSource))]
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

	[DataMember]
	[EdmScalarProperty(EntityKeyProperty = false, IsNullable = true)]
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

	[EdmScalarProperty(EntityKeyProperty = false, IsNullable = true)]
	[DataMember]
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

	public static view_ComeAndGo Createview_ComeAndGo(int transactionID, string inCardCode, int parkTypeID, DateTime inTime, int inGateID, int parkMin, int transactionBillType, int transactionStatus)
	{
		view_ComeAndGo view_ComeAndGo2 = new view_ComeAndGo();
		view_ComeAndGo2.TransactionID = transactionID;
		view_ComeAndGo2.InCardCode = inCardCode;
		view_ComeAndGo2.ParkTypeID = parkTypeID;
		view_ComeAndGo2.InTime = inTime;
		view_ComeAndGo2.InGateID = inGateID;
		view_ComeAndGo2.ParkMin = parkMin;
		view_ComeAndGo2.TransactionBillType = transactionBillType;
		view_ComeAndGo2.TransactionStatus = transactionStatus;
		return view_ComeAndGo2;
	}
}
