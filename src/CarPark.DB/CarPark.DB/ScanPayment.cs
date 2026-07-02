using System;
using System.Data.Objects.DataClasses;
using System.Runtime.Serialization;

namespace CarPark.DB;

[Serializable]
[EdmEntityType(NamespaceName = "CarPark.DB", Name = "ScanPayment")]
[DataContract(IsReference = true)]
public class ScanPayment : EntityObject
{
	private int _ID;

	private int _ChargeRecordID;

	private int _TransactionID;

	private string _MerchantId;

	private string _TrmNo;

	private string _PayOrderNo;

	private string _LogNo;

	private string _ChargeTime;

	private string _Subject;

	private decimal? _Amount;

	private string _AuthCode;

	private string _PayType;

	private string _UserData;

	private string _ParkingLotNo;

	private string _Result;

	private string _ResultMessage;

	private string _LastUpdateTime;

	[DataMember]
	[EdmScalarProperty(EntityKeyProperty = true, IsNullable = false)]
	public int ID
	{
		get
		{
			return _ID;
		}
		set
		{
			if (_ID != value)
			{
				ReportPropertyChanging("ID");
				_ID = StructuralObject.SetValidValue(value);
				ReportPropertyChanged("ID");
			}
		}
	}

	[EdmScalarProperty(EntityKeyProperty = false, IsNullable = false)]
	[DataMember]
	public int ChargeRecordID
	{
		get
		{
			return _ChargeRecordID;
		}
		set
		{
			ReportPropertyChanging("ChargeRecordID");
			_ChargeRecordID = StructuralObject.SetValidValue(value);
			ReportPropertyChanged("ChargeRecordID");
		}
	}

	[EdmScalarProperty(EntityKeyProperty = false, IsNullable = false)]
	[DataMember]
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

	[DataMember]
	[EdmScalarProperty(EntityKeyProperty = false, IsNullable = true)]
	public string MerchantId
	{
		get
		{
			return _MerchantId;
		}
		set
		{
			ReportPropertyChanging("MerchantId");
			_MerchantId = StructuralObject.SetValidValue(value, isNullable: true);
			ReportPropertyChanged("MerchantId");
		}
	}

	[DataMember]
	[EdmScalarProperty(EntityKeyProperty = false, IsNullable = true)]
	public string TrmNo
	{
		get
		{
			return _TrmNo;
		}
		set
		{
			ReportPropertyChanging("TrmNo");
			_TrmNo = StructuralObject.SetValidValue(value, isNullable: true);
			ReportPropertyChanged("TrmNo");
		}
	}

	[EdmScalarProperty(EntityKeyProperty = false, IsNullable = true)]
	[DataMember]
	public string PayOrderNo
	{
		get
		{
			return _PayOrderNo;
		}
		set
		{
			ReportPropertyChanging("PayOrderNo");
			_PayOrderNo = StructuralObject.SetValidValue(value, isNullable: true);
			ReportPropertyChanged("PayOrderNo");
		}
	}

	[DataMember]
	[EdmScalarProperty(EntityKeyProperty = false, IsNullable = true)]
	public string LogNo
	{
		get
		{
			return _LogNo;
		}
		set
		{
			ReportPropertyChanging("LogNo");
			_LogNo = StructuralObject.SetValidValue(value, isNullable: true);
			ReportPropertyChanged("LogNo");
		}
	}

	[EdmScalarProperty(EntityKeyProperty = false, IsNullable = true)]
	[DataMember]
	public string ChargeTime
	{
		get
		{
			return _ChargeTime;
		}
		set
		{
			ReportPropertyChanging("ChargeTime");
			_ChargeTime = StructuralObject.SetValidValue(value, isNullable: true);
			ReportPropertyChanged("ChargeTime");
		}
	}

	[DataMember]
	[EdmScalarProperty(EntityKeyProperty = false, IsNullable = true)]
	public string Subject
	{
		get
		{
			return _Subject;
		}
		set
		{
			ReportPropertyChanging("Subject");
			_Subject = StructuralObject.SetValidValue(value, isNullable: true);
			ReportPropertyChanged("Subject");
		}
	}

	[EdmScalarProperty(EntityKeyProperty = false, IsNullable = true)]
	[DataMember]
	public decimal? Amount
	{
		get
		{
			return _Amount;
		}
		set
		{
			ReportPropertyChanging("Amount");
			_Amount = StructuralObject.SetValidValue(value);
			ReportPropertyChanged("Amount");
		}
	}

	[DataMember]
	[EdmScalarProperty(EntityKeyProperty = false, IsNullable = true)]
	public string AuthCode
	{
		get
		{
			return _AuthCode;
		}
		set
		{
			ReportPropertyChanging("AuthCode");
			_AuthCode = StructuralObject.SetValidValue(value, isNullable: true);
			ReportPropertyChanged("AuthCode");
		}
	}

	[EdmScalarProperty(EntityKeyProperty = false, IsNullable = true)]
	[DataMember]
	public string PayType
	{
		get
		{
			return _PayType;
		}
		set
		{
			ReportPropertyChanging("PayType");
			_PayType = StructuralObject.SetValidValue(value, isNullable: true);
			ReportPropertyChanged("PayType");
		}
	}

	[DataMember]
	[EdmScalarProperty(EntityKeyProperty = false, IsNullable = true)]
	public string UserData
	{
		get
		{
			return _UserData;
		}
		set
		{
			ReportPropertyChanging("UserData");
			_UserData = StructuralObject.SetValidValue(value, isNullable: true);
			ReportPropertyChanged("UserData");
		}
	}

	[EdmScalarProperty(EntityKeyProperty = false, IsNullable = true)]
	[DataMember]
	public string ParkingLotNo
	{
		get
		{
			return _ParkingLotNo;
		}
		set
		{
			ReportPropertyChanging("ParkingLotNo");
			_ParkingLotNo = StructuralObject.SetValidValue(value, isNullable: true);
			ReportPropertyChanged("ParkingLotNo");
		}
	}

	[DataMember]
	[EdmScalarProperty(EntityKeyProperty = false, IsNullable = true)]
	public string Result
	{
		get
		{
			return _Result;
		}
		set
		{
			ReportPropertyChanging("Result");
			_Result = StructuralObject.SetValidValue(value, isNullable: true);
			ReportPropertyChanged("Result");
		}
	}

	[EdmScalarProperty(EntityKeyProperty = false, IsNullable = true)]
	[DataMember]
	public string ResultMessage
	{
		get
		{
			return _ResultMessage;
		}
		set
		{
			ReportPropertyChanging("ResultMessage");
			_ResultMessage = StructuralObject.SetValidValue(value, isNullable: true);
			ReportPropertyChanged("ResultMessage");
		}
	}

	[EdmScalarProperty(EntityKeyProperty = false, IsNullable = true)]
	[DataMember]
	public string LastUpdateTime
	{
		get
		{
			return _LastUpdateTime;
		}
		set
		{
			ReportPropertyChanging("LastUpdateTime");
			_LastUpdateTime = StructuralObject.SetValidValue(value, isNullable: true);
			ReportPropertyChanged("LastUpdateTime");
		}
	}

	public static ScanPayment CreateScanPayment(int id, int chargeRecordID, int transactionID)
	{
		ScanPayment scanPayment = new ScanPayment();
		scanPayment.ID = id;
		scanPayment.ChargeRecordID = chargeRecordID;
		scanPayment.TransactionID = transactionID;
		return scanPayment;
	}
}
