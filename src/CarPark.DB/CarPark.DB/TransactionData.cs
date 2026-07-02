using System;
using System.Data.Objects.DataClasses;
using System.Runtime.Serialization;
using CarPark.DB.AdditionalDataSource;
using SkyInno.Lang;
using SkyInno.UI.BindingText;

namespace CarPark.DB;

[Serializable]
[EdmEntityType(NamespaceName = "CarPark.DB", Name = "TransactionData")]
[DataContract(IsReference = true)]
public class TransactionData : EntityObject
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

	private bool _IsDelete;

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

	[DataMember]
	[EdmScalarProperty(EntityKeyProperty = false, IsNullable = false)]
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

	[EdmScalarProperty(EntityKeyProperty = false, IsNullable = false)]
	[DataMember]
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

	[DataMember]
	[EdmScalarProperty(EntityKeyProperty = false, IsNullable = false)]
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

	[EdmScalarProperty(EntityKeyProperty = false, IsNullable = true)]
	[DataMember]
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

	[BindingControlEditStyle(EnumEditStyle.EnumComboBox, typeof(EnumParkTypeSource))]
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

	[DataMember]
	[EdmScalarProperty(EntityKeyProperty = false, IsNullable = true)]
	[BindingControlEditStyle(EnumEditStyle.DbComboBox, typeof(DBRentalTypeSource))]
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

	[BindingControlEditStyle(EnumEditStyle.EnumComboBox, typeof(EnumCardTypeSource))]
	[DataMember]
	[EdmScalarProperty(EntityKeyProperty = false, IsNullable = false)]
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

	[EdmScalarProperty(EntityKeyProperty = false, IsNullable = false)]
	[DataMember]
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

	[DataMember]
	[EdmScalarProperty(EntityKeyProperty = false, IsNullable = true)]
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

	public static TransactionData CreateTransactionData(int transactionID, string inCardCode, DateTime inTime, int inGateID, int transactionStatus, int parkTypeID, int transactionBillType, int parkMin, bool isDelete)
	{
		TransactionData transactionData = new TransactionData();
		transactionData.TransactionID = transactionID;
		transactionData.InCardCode = inCardCode;
		transactionData.InTime = inTime;
		transactionData.InGateID = inGateID;
		transactionData.TransactionStatus = transactionStatus;
		transactionData.ParkTypeID = parkTypeID;
		transactionData.TransactionBillType = transactionBillType;
		transactionData.ParkMin = parkMin;
		transactionData.IsDelete = isDelete;
		return transactionData;
	}
}
