using System;
using System.Data.Objects.DataClasses;
using System.Runtime.Serialization;

namespace CarPark.DB;

[Serializable]
[DataContract(IsReference = true)]
[EdmEntityType(NamespaceName = "CarPark.DB", Name = "view_transactionandlp")]
public class view_transactionandlp : EntityObject
{
	private int _TransactionID;

	private string _InCardCode;

	private DateTime _InTime;

	private int _InGateID;

	private int _ParkTypeID;

	private int? _RentalType;

	private int _TransactionBillType;

	private string _AnalysisResult;

	private string _ImagePath;

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

	[EdmScalarProperty(EntityKeyProperty = true, IsNullable = false)]
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

	public static view_transactionandlp Createview_transactionandlp(int transactionID, string inCardCode, DateTime inTime, int inGateID, int parkTypeID, int transactionBillType)
	{
		view_transactionandlp view_transactionandlp2 = new view_transactionandlp();
		view_transactionandlp2.TransactionID = transactionID;
		view_transactionandlp2.InCardCode = inCardCode;
		view_transactionandlp2.InTime = inTime;
		view_transactionandlp2.InGateID = inGateID;
		view_transactionandlp2.ParkTypeID = parkTypeID;
		view_transactionandlp2.TransactionBillType = transactionBillType;
		return view_transactionandlp2;
	}
}
