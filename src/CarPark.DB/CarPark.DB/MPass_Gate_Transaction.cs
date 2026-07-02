using System;
using System.Data.Objects.DataClasses;
using System.Runtime.Serialization;

namespace CarPark.DB;

[Serializable]
[DataContract(IsReference = true)]
[EdmEntityType(NamespaceName = "CarPark.DB", Name = "MPass_Gate_Transaction")]
public class MPass_Gate_Transaction : EntityObject
{
	private int _TransactionID;

	private string _LogicalCardNumber;

	private string _CardNumber;

	private decimal? _CardBillAmount;

	private decimal? _CardRemain;

	private bool _CardIsValid;

	private int _FromGateID;

	private int _SysTransacionID;

	private bool _ISUploaded;

	private DateTime? _UploadTime;

	private bool _IsBlack;

	private string _CardReaderNo;

	private DateTime _TransactionTime;

	private int? _ChargeTransactionID;

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

	[EdmScalarProperty(EntityKeyProperty = false, IsNullable = true)]
	[DataMember]
	public string LogicalCardNumber
	{
		get
		{
			return _LogicalCardNumber;
		}
		set
		{
			ReportPropertyChanging("LogicalCardNumber");
			_LogicalCardNumber = StructuralObject.SetValidValue(value, isNullable: true);
			ReportPropertyChanged("LogicalCardNumber");
		}
	}

	[EdmScalarProperty(EntityKeyProperty = false, IsNullable = true)]
	[DataMember]
	public string CardNumber
	{
		get
		{
			return _CardNumber;
		}
		set
		{
			ReportPropertyChanging("CardNumber");
			_CardNumber = StructuralObject.SetValidValue(value, isNullable: true);
			ReportPropertyChanged("CardNumber");
		}
	}

	[EdmScalarProperty(EntityKeyProperty = false, IsNullable = true)]
	[DataMember]
	public decimal? CardBillAmount
	{
		get
		{
			return _CardBillAmount;
		}
		set
		{
			ReportPropertyChanging("CardBillAmount");
			_CardBillAmount = StructuralObject.SetValidValue(value);
			ReportPropertyChanged("CardBillAmount");
		}
	}

	[DataMember]
	[EdmScalarProperty(EntityKeyProperty = false, IsNullable = true)]
	public decimal? CardRemain
	{
		get
		{
			return _CardRemain;
		}
		set
		{
			ReportPropertyChanging("CardRemain");
			_CardRemain = StructuralObject.SetValidValue(value);
			ReportPropertyChanged("CardRemain");
		}
	}

	[EdmScalarProperty(EntityKeyProperty = false, IsNullable = false)]
	[DataMember]
	public bool CardIsValid
	{
		get
		{
			return _CardIsValid;
		}
		set
		{
			ReportPropertyChanging("CardIsValid");
			_CardIsValid = StructuralObject.SetValidValue(value);
			ReportPropertyChanged("CardIsValid");
		}
	}

	[DataMember]
	[EdmScalarProperty(EntityKeyProperty = false, IsNullable = false)]
	public int FromGateID
	{
		get
		{
			return _FromGateID;
		}
		set
		{
			ReportPropertyChanging("FromGateID");
			_FromGateID = StructuralObject.SetValidValue(value);
			ReportPropertyChanged("FromGateID");
		}
	}

	[DataMember]
	[EdmScalarProperty(EntityKeyProperty = false, IsNullable = false)]
	public int SysTransacionID
	{
		get
		{
			return _SysTransacionID;
		}
		set
		{
			ReportPropertyChanging("SysTransacionID");
			_SysTransacionID = StructuralObject.SetValidValue(value);
			ReportPropertyChanged("SysTransacionID");
		}
	}

	[DataMember]
	[EdmScalarProperty(EntityKeyProperty = false, IsNullable = false)]
	public bool ISUploaded
	{
		get
		{
			return _ISUploaded;
		}
		set
		{
			ReportPropertyChanging("ISUploaded");
			_ISUploaded = StructuralObject.SetValidValue(value);
			ReportPropertyChanged("ISUploaded");
		}
	}

	[DataMember]
	[EdmScalarProperty(EntityKeyProperty = false, IsNullable = true)]
	public DateTime? UploadTime
	{
		get
		{
			return _UploadTime;
		}
		set
		{
			ReportPropertyChanging("UploadTime");
			_UploadTime = StructuralObject.SetValidValue(value);
			ReportPropertyChanged("UploadTime");
		}
	}

	[DataMember]
	[EdmScalarProperty(EntityKeyProperty = false, IsNullable = false)]
	public bool IsBlack
	{
		get
		{
			return _IsBlack;
		}
		set
		{
			ReportPropertyChanging("IsBlack");
			_IsBlack = StructuralObject.SetValidValue(value);
			ReportPropertyChanged("IsBlack");
		}
	}

	[EdmScalarProperty(EntityKeyProperty = false, IsNullable = false)]
	[DataMember]
	public string CardReaderNo
	{
		get
		{
			return _CardReaderNo;
		}
		set
		{
			ReportPropertyChanging("CardReaderNo");
			_CardReaderNo = StructuralObject.SetValidValue(value, isNullable: false);
			ReportPropertyChanged("CardReaderNo");
		}
	}

	[EdmScalarProperty(EntityKeyProperty = false, IsNullable = false)]
	[DataMember]
	public DateTime TransactionTime
	{
		get
		{
			return _TransactionTime;
		}
		set
		{
			ReportPropertyChanging("TransactionTime");
			_TransactionTime = StructuralObject.SetValidValue(value);
			ReportPropertyChanged("TransactionTime");
		}
	}

	[DataMember]
	[EdmScalarProperty(EntityKeyProperty = false, IsNullable = true)]
	public int? ChargeTransactionID
	{
		get
		{
			return _ChargeTransactionID;
		}
		set
		{
			ReportPropertyChanging("ChargeTransactionID");
			_ChargeTransactionID = StructuralObject.SetValidValue(value);
			ReportPropertyChanged("ChargeTransactionID");
		}
	}

	public static MPass_Gate_Transaction CreateMPass_Gate_Transaction(int transactionID, bool cardIsValid, int fromGateID, int sysTransacionID, bool iSUploaded, bool isBlack, string cardReaderNo, DateTime transactionTime)
	{
		MPass_Gate_Transaction mPass_Gate_Transaction = new MPass_Gate_Transaction();
		mPass_Gate_Transaction.TransactionID = transactionID;
		mPass_Gate_Transaction.CardIsValid = cardIsValid;
		mPass_Gate_Transaction.FromGateID = fromGateID;
		mPass_Gate_Transaction.SysTransacionID = sysTransacionID;
		mPass_Gate_Transaction.ISUploaded = iSUploaded;
		mPass_Gate_Transaction.IsBlack = isBlack;
		mPass_Gate_Transaction.CardReaderNo = cardReaderNo;
		mPass_Gate_Transaction.TransactionTime = transactionTime;
		return mPass_Gate_Transaction;
	}
}
