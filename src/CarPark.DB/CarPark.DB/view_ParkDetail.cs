using System;
using System.Data.Objects.DataClasses;
using System.Runtime.Serialization;
using CarPark.DB.AdditionalDataSource;
using SkyInno.Lang;
using SkyInno.UI.BindingText;

namespace CarPark.DB;

[Serializable]
[EdmEntityType(NamespaceName = "CarPark.DB", Name = "view_ParkDetail")]
[DataContract(IsReference = true)]
public class view_ParkDetail : EntityObject
{
	private int _TransactionID;

	private string _InCardCode;

	private int _ParkTypeID;

	private DateTime _InTime;

	private int _InGateID;

	private int _TransactionBillType;

	private int? _RentalType;

	private string _ImagePath;

	private string _AnalysisResult;

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

	[DataMember]
	[BindingControlEditStyle(EnumEditStyle.EnumComboBox, typeof(EnumParkTypeSource))]
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

	[EdmScalarProperty(EntityKeyProperty = true, IsNullable = false)]
	[DataMember]
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

	[EdmScalarProperty(EntityKeyProperty = true, IsNullable = false)]
	[BindingControlEditStyle(EnumEditStyle.EnumComboBox, typeof(EnumCardTypeSource))]
	[DataMember]
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

	[EdmScalarProperty(EntityKeyProperty = false, IsNullable = true)]
	[DataMember]
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

	[EdmScalarProperty(EntityKeyProperty = false, IsNullable = true)]
	[DataMember]
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

	public static view_ParkDetail Createview_ParkDetail(int transactionID, string inCardCode, int parkTypeID, DateTime inTime, int inGateID, int transactionBillType)
	{
		view_ParkDetail view_ParkDetail2 = new view_ParkDetail();
		view_ParkDetail2.TransactionID = transactionID;
		view_ParkDetail2.InCardCode = inCardCode;
		view_ParkDetail2.ParkTypeID = parkTypeID;
		view_ParkDetail2.InTime = inTime;
		view_ParkDetail2.InGateID = inGateID;
		view_ParkDetail2.TransactionBillType = transactionBillType;
		return view_ParkDetail2;
	}
}
