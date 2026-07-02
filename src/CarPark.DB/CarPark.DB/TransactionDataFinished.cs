using System;
using System.Data.Objects.DataClasses;
using System.Runtime.Serialization;

namespace CarPark.DB;

[Serializable]
[EdmEntityType(NamespaceName = "CarPark.DB", Name = "TransactionDataFinished")]
[DataContract(IsReference = true)]
public class TransactionDataFinished : EntityObject
{
	private int _TransactionFinishedID;

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

	private bool _IsDelete;

	[EdmScalarProperty(EntityKeyProperty = true, IsNullable = false)]
	[DataMember]
	public int TransactionFinishedID
	{
		get
		{
			return _TransactionFinishedID;
		}
		set
		{
			if (_TransactionFinishedID != value)
			{
				ReportPropertyChanging("TransactionFinishedID");
				_TransactionFinishedID = StructuralObject.SetValidValue(value);
				ReportPropertyChanged("TransactionFinishedID");
			}
		}
	}

	[DataMember]
	[EdmScalarProperty(EntityKeyProperty = false, IsNullable = false)]
	public int TransactionID
	{
		get
		{
			return _TransactionID;
		}
		set
		{
			ReportPropertyChanging("TransactionID");
			_TransactionID = StructuralObject.SetValidValue(value);
			ReportPropertyChanged("TransactionID");
		}
	}

	[EdmScalarProperty(EntityKeyProperty = false, IsNullable = false)]
	[DataMember]
	public string InCardCode
	{
		get
		{
			return _InCardCode;
		}
		set
		{
			ReportPropertyChanging("InCardCode");
			_InCardCode = StructuralObject.SetValidValue(value, isNullable: false);
			ReportPropertyChanged("InCardCode");
		}
	}

	[DataMember]
	[EdmScalarProperty(EntityKeyProperty = false, IsNullable = false)]
	public DateTime InTime
	{
		get
		{
			return _InTime;
		}
		set
		{
			ReportPropertyChanging("InTime");
			_InTime = StructuralObject.SetValidValue(value);
			ReportPropertyChanged("InTime");
		}
	}

	[EdmScalarProperty(EntityKeyProperty = false, IsNullable = false)]
	[DataMember]
	public int InGateID
	{
		get
		{
			return _InGateID;
		}
		set
		{
			ReportPropertyChanging("InGateID");
			_InGateID = StructuralObject.SetValidValue(value);
			ReportPropertyChanged("InGateID");
		}
	}

	[DataMember]
	[EdmScalarProperty(EntityKeyProperty = false, IsNullable = true)]
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
	[EdmScalarProperty(EntityKeyProperty = false, IsNullable = false)]
	public int TransactionStatus
	{
		get
		{
			return _TransactionStatus;
		}
		set
		{
			ReportPropertyChanging("TransactionStatus");
			_TransactionStatus = StructuralObject.SetValidValue(value);
			ReportPropertyChanged("TransactionStatus");
		}
	}

	[EdmScalarProperty(EntityKeyProperty = false, IsNullable = false)]
	[DataMember]
	public int ParkTypeID
	{
		get
		{
			return _ParkTypeID;
		}
		set
		{
			ReportPropertyChanging("ParkTypeID");
			_ParkTypeID = StructuralObject.SetValidValue(value);
			ReportPropertyChanged("ParkTypeID");
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

	[EdmScalarProperty(EntityKeyProperty = false, IsNullable = false)]
	[DataMember]
	public int TransactionBillType
	{
		get
		{
			return _TransactionBillType;
		}
		set
		{
			ReportPropertyChanging("TransactionBillType");
			_TransactionBillType = StructuralObject.SetValidValue(value);
			ReportPropertyChanged("TransactionBillType");
		}
	}

	[DataMember]
	[EdmScalarProperty(EntityKeyProperty = false, IsNullable = false)]
	public int ParkMin
	{
		get
		{
			return _ParkMin;
		}
		set
		{
			ReportPropertyChanging("ParkMin");
			_ParkMin = StructuralObject.SetValidValue(value);
			ReportPropertyChanged("ParkMin");
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

	[EdmScalarProperty(EntityKeyProperty = false, IsNullable = false)]
	[DataMember]
	public bool IsDelete
	{
		get
		{
			return _IsDelete;
		}
		set
		{
			ReportPropertyChanging("IsDelete");
			_IsDelete = StructuralObject.SetValidValue(value);
			ReportPropertyChanged("IsDelete");
		}
	}

	public static TransactionDataFinished CreateTransactionDataFinished(int transactionFinishedID, int transactionID, string inCardCode, DateTime inTime, int inGateID, int transactionStatus, int parkTypeID, int transactionBillType, int parkMin, bool isDelete)
	{
		TransactionDataFinished transactionDataFinished = new TransactionDataFinished();
		transactionDataFinished.TransactionFinishedID = transactionFinishedID;
		transactionDataFinished.TransactionID = transactionID;
		transactionDataFinished.InCardCode = inCardCode;
		transactionDataFinished.InTime = inTime;
		transactionDataFinished.InGateID = inGateID;
		transactionDataFinished.TransactionStatus = transactionStatus;
		transactionDataFinished.ParkTypeID = parkTypeID;
		transactionDataFinished.TransactionBillType = transactionBillType;
		transactionDataFinished.ParkMin = parkMin;
		transactionDataFinished.IsDelete = isDelete;
		return transactionDataFinished;
	}
}
