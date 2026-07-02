using System;
using System.Data.Objects.DataClasses;
using System.Runtime.Serialization;

namespace CarPark.DB;

[Serializable]
[EdmEntityType(NamespaceName = "CarPark.DB", Name = "view_transactiondata")]
[DataContract(IsReference = true)]
public class view_transactiondata : EntityObject
{
	private int _TransactionID;

	private string _InCardCode;

	private DateTime _InTime;

	private int _InGateID;

	private string _OutCardCode;

	private DateTime? _OutTime;

	private int? _OutGateID;

	private int _TransactionStatus;

	private int _ParkTypeID;

	private int? _RentalType;

	private int _TransactionBillType;

	private int _ParkMin;

	private string _Remark;

	private sbyte _IsDelete;

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
	[EdmScalarProperty(EntityKeyProperty = true, IsNullable = false)]
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

	[DataMember]
	[EdmScalarProperty(EntityKeyProperty = true, IsNullable = false)]
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
	[EdmScalarProperty(EntityKeyProperty = true, IsNullable = false)]
	public sbyte IsDelete
	{
		get
		{
			return _IsDelete;
		}
		set
		{
			if (_IsDelete != value)
			{
				ReportPropertyChanging("IsDelete");
				_IsDelete = StructuralObject.SetValidValue(value);
				ReportPropertyChanged("IsDelete");
			}
		}
	}

	public static view_transactiondata Createview_transactiondata(int transactionID, string inCardCode, DateTime inTime, int inGateID, int transactionStatus, int parkTypeID, int transactionBillType, int parkMin, sbyte isDelete)
	{
		view_transactiondata view_transactiondata2 = new view_transactiondata();
		view_transactiondata2.TransactionID = transactionID;
		view_transactiondata2.InCardCode = inCardCode;
		view_transactiondata2.InTime = inTime;
		view_transactiondata2.InGateID = inGateID;
		view_transactiondata2.TransactionStatus = transactionStatus;
		view_transactiondata2.ParkTypeID = parkTypeID;
		view_transactiondata2.TransactionBillType = transactionBillType;
		view_transactiondata2.ParkMin = parkMin;
		view_transactiondata2.IsDelete = isDelete;
		return view_transactiondata2;
	}
}
