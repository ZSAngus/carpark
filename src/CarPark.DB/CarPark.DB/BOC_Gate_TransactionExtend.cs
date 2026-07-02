using System;
using System.Data.Objects.DataClasses;
using System.Runtime.Serialization;

namespace CarPark.DB;

[Serializable]
[DataContract(IsReference = true)]
[EdmEntityType(NamespaceName = "CarPark.DB", Name = "BOC_Gate_TransactionExtend")]
public class BOC_Gate_TransactionExtend : EntityObject
{
	private int _TransactionID;

	private decimal _CardBillAmount;

	private decimal _CardRemain;

	private string _CardNumber;

	private string _BillArea;

	private string _TxnNo;

	private string _BillDate;

	private string _BillTime;

	private string _ServerCode;

	private string _LogicNo;

	private string _DeviceCode;

	private string _ReceiverCode;

	private string _IC_Data;

	private string _AlternateData;

	private string _MD5;

	private string _ReplyCode;

	private string _ErrorCode;

	private int? _REQUEST_CARD_State;

	private int? _PURCHASE_CARD_State;

	private string _CardPhyType;

	private string _CardAppType;

	private string _BillAreaB;

	private decimal? _OffLineRemain_MOP;

	private decimal? _OffLineRemain_RMB;

	private string _Purchase_FullData;

	private string _EncryptedCardNumber;

	private string _Description;

	private string _CardReaderNumber;

	private string _StaffInfo;

	private bool? _Valid;

	private int? _IsBlack;

	private int _FromGateID;

	private int _SysTransacionID;

	private DateTime _TransactionTime;

	private bool _ISUploaded;

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

	[DataMember]
	[EdmScalarProperty(EntityKeyProperty = false, IsNullable = false)]
	public decimal CardBillAmount
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
	[EdmScalarProperty(EntityKeyProperty = false, IsNullable = false)]
	public decimal CardRemain
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
	public string BillArea
	{
		get
		{
			return _BillArea;
		}
		set
		{
			ReportPropertyChanging("BillArea");
			_BillArea = StructuralObject.SetValidValue(value, isNullable: true);
			ReportPropertyChanged("BillArea");
		}
	}

	[DataMember]
	[EdmScalarProperty(EntityKeyProperty = false, IsNullable = true)]
	public string TxnNo
	{
		get
		{
			return _TxnNo;
		}
		set
		{
			ReportPropertyChanging("TxnNo");
			_TxnNo = StructuralObject.SetValidValue(value, isNullable: true);
			ReportPropertyChanged("TxnNo");
		}
	}

	[EdmScalarProperty(EntityKeyProperty = false, IsNullable = true)]
	[DataMember]
	public string BillDate
	{
		get
		{
			return _BillDate;
		}
		set
		{
			ReportPropertyChanging("BillDate");
			_BillDate = StructuralObject.SetValidValue(value, isNullable: true);
			ReportPropertyChanged("BillDate");
		}
	}

	[DataMember]
	[EdmScalarProperty(EntityKeyProperty = false, IsNullable = true)]
	public string BillTime
	{
		get
		{
			return _BillTime;
		}
		set
		{
			ReportPropertyChanging("BillTime");
			_BillTime = StructuralObject.SetValidValue(value, isNullable: true);
			ReportPropertyChanged("BillTime");
		}
	}

	[EdmScalarProperty(EntityKeyProperty = false, IsNullable = true)]
	[DataMember]
	public string ServerCode
	{
		get
		{
			return _ServerCode;
		}
		set
		{
			ReportPropertyChanging("ServerCode");
			_ServerCode = StructuralObject.SetValidValue(value, isNullable: true);
			ReportPropertyChanged("ServerCode");
		}
	}

	[DataMember]
	[EdmScalarProperty(EntityKeyProperty = false, IsNullable = true)]
	public string LogicNo
	{
		get
		{
			return _LogicNo;
		}
		set
		{
			ReportPropertyChanging("LogicNo");
			_LogicNo = StructuralObject.SetValidValue(value, isNullable: true);
			ReportPropertyChanged("LogicNo");
		}
	}

	[EdmScalarProperty(EntityKeyProperty = false, IsNullable = true)]
	[DataMember]
	public string DeviceCode
	{
		get
		{
			return _DeviceCode;
		}
		set
		{
			ReportPropertyChanging("DeviceCode");
			_DeviceCode = StructuralObject.SetValidValue(value, isNullable: true);
			ReportPropertyChanged("DeviceCode");
		}
	}

	[DataMember]
	[EdmScalarProperty(EntityKeyProperty = false, IsNullable = true)]
	public string ReceiverCode
	{
		get
		{
			return _ReceiverCode;
		}
		set
		{
			ReportPropertyChanging("ReceiverCode");
			_ReceiverCode = StructuralObject.SetValidValue(value, isNullable: true);
			ReportPropertyChanged("ReceiverCode");
		}
	}

	[EdmScalarProperty(EntityKeyProperty = false, IsNullable = true)]
	[DataMember]
	public string IC_Data
	{
		get
		{
			return _IC_Data;
		}
		set
		{
			ReportPropertyChanging("IC_Data");
			_IC_Data = StructuralObject.SetValidValue(value, isNullable: true);
			ReportPropertyChanged("IC_Data");
		}
	}

	[DataMember]
	[EdmScalarProperty(EntityKeyProperty = false, IsNullable = true)]
	public string AlternateData
	{
		get
		{
			return _AlternateData;
		}
		set
		{
			ReportPropertyChanging("AlternateData");
			_AlternateData = StructuralObject.SetValidValue(value, isNullable: true);
			ReportPropertyChanged("AlternateData");
		}
	}

	[DataMember]
	[EdmScalarProperty(EntityKeyProperty = false, IsNullable = true)]
	public string MD5
	{
		get
		{
			return _MD5;
		}
		set
		{
			ReportPropertyChanging("MD5");
			_MD5 = StructuralObject.SetValidValue(value, isNullable: true);
			ReportPropertyChanged("MD5");
		}
	}

	[EdmScalarProperty(EntityKeyProperty = false, IsNullable = true)]
	[DataMember]
	public string ReplyCode
	{
		get
		{
			return _ReplyCode;
		}
		set
		{
			ReportPropertyChanging("ReplyCode");
			_ReplyCode = StructuralObject.SetValidValue(value, isNullable: true);
			ReportPropertyChanged("ReplyCode");
		}
	}

	[EdmScalarProperty(EntityKeyProperty = false, IsNullable = true)]
	[DataMember]
	public string ErrorCode
	{
		get
		{
			return _ErrorCode;
		}
		set
		{
			ReportPropertyChanging("ErrorCode");
			_ErrorCode = StructuralObject.SetValidValue(value, isNullable: true);
			ReportPropertyChanged("ErrorCode");
		}
	}

	[EdmScalarProperty(EntityKeyProperty = false, IsNullable = true)]
	[DataMember]
	public int? REQUEST_CARD_State
	{
		get
		{
			return _REQUEST_CARD_State;
		}
		set
		{
			ReportPropertyChanging("REQUEST_CARD_State");
			_REQUEST_CARD_State = StructuralObject.SetValidValue(value);
			ReportPropertyChanged("REQUEST_CARD_State");
		}
	}

	[EdmScalarProperty(EntityKeyProperty = false, IsNullable = true)]
	[DataMember]
	public int? PURCHASE_CARD_State
	{
		get
		{
			return _PURCHASE_CARD_State;
		}
		set
		{
			ReportPropertyChanging("PURCHASE_CARD_State");
			_PURCHASE_CARD_State = StructuralObject.SetValidValue(value);
			ReportPropertyChanged("PURCHASE_CARD_State");
		}
	}

	[DataMember]
	[EdmScalarProperty(EntityKeyProperty = false, IsNullable = true)]
	public string CardPhyType
	{
		get
		{
			return _CardPhyType;
		}
		set
		{
			ReportPropertyChanging("CardPhyType");
			_CardPhyType = StructuralObject.SetValidValue(value, isNullable: true);
			ReportPropertyChanged("CardPhyType");
		}
	}

	[DataMember]
	[EdmScalarProperty(EntityKeyProperty = false, IsNullable = true)]
	public string CardAppType
	{
		get
		{
			return _CardAppType;
		}
		set
		{
			ReportPropertyChanging("CardAppType");
			_CardAppType = StructuralObject.SetValidValue(value, isNullable: true);
			ReportPropertyChanged("CardAppType");
		}
	}

	[EdmScalarProperty(EntityKeyProperty = false, IsNullable = true)]
	[DataMember]
	public string BillAreaB
	{
		get
		{
			return _BillAreaB;
		}
		set
		{
			ReportPropertyChanging("BillAreaB");
			_BillAreaB = StructuralObject.SetValidValue(value, isNullable: true);
			ReportPropertyChanged("BillAreaB");
		}
	}

	[DataMember]
	[EdmScalarProperty(EntityKeyProperty = false, IsNullable = true)]
	public decimal? OffLineRemain_MOP
	{
		get
		{
			return _OffLineRemain_MOP;
		}
		set
		{
			ReportPropertyChanging("OffLineRemain_MOP");
			_OffLineRemain_MOP = StructuralObject.SetValidValue(value);
			ReportPropertyChanged("OffLineRemain_MOP");
		}
	}

	[DataMember]
	[EdmScalarProperty(EntityKeyProperty = false, IsNullable = true)]
	public decimal? OffLineRemain_RMB
	{
		get
		{
			return _OffLineRemain_RMB;
		}
		set
		{
			ReportPropertyChanging("OffLineRemain_RMB");
			_OffLineRemain_RMB = StructuralObject.SetValidValue(value);
			ReportPropertyChanged("OffLineRemain_RMB");
		}
	}

	[EdmScalarProperty(EntityKeyProperty = false, IsNullable = true)]
	[DataMember]
	public string Purchase_FullData
	{
		get
		{
			return _Purchase_FullData;
		}
		set
		{
			ReportPropertyChanging("Purchase_FullData");
			_Purchase_FullData = StructuralObject.SetValidValue(value, isNullable: true);
			ReportPropertyChanged("Purchase_FullData");
		}
	}

	[DataMember]
	[EdmScalarProperty(EntityKeyProperty = false, IsNullable = true)]
	public string EncryptedCardNumber
	{
		get
		{
			return _EncryptedCardNumber;
		}
		set
		{
			ReportPropertyChanging("EncryptedCardNumber");
			_EncryptedCardNumber = StructuralObject.SetValidValue(value, isNullable: true);
			ReportPropertyChanged("EncryptedCardNumber");
		}
	}

	[EdmScalarProperty(EntityKeyProperty = false, IsNullable = true)]
	[DataMember]
	public string Description
	{
		get
		{
			return _Description;
		}
		set
		{
			ReportPropertyChanging("Description");
			_Description = StructuralObject.SetValidValue(value, isNullable: true);
			ReportPropertyChanged("Description");
		}
	}

	[DataMember]
	[EdmScalarProperty(EntityKeyProperty = false, IsNullable = true)]
	public string CardReaderNumber
	{
		get
		{
			return _CardReaderNumber;
		}
		set
		{
			ReportPropertyChanging("CardReaderNumber");
			_CardReaderNumber = StructuralObject.SetValidValue(value, isNullable: true);
			ReportPropertyChanged("CardReaderNumber");
		}
	}

	[DataMember]
	[EdmScalarProperty(EntityKeyProperty = false, IsNullable = true)]
	public string StaffInfo
	{
		get
		{
			return _StaffInfo;
		}
		set
		{
			ReportPropertyChanging("StaffInfo");
			_StaffInfo = StructuralObject.SetValidValue(value, isNullable: true);
			ReportPropertyChanged("StaffInfo");
		}
	}

	[DataMember]
	[EdmScalarProperty(EntityKeyProperty = false, IsNullable = true)]
	public bool? Valid
	{
		get
		{
			return _Valid;
		}
		set
		{
			ReportPropertyChanging("Valid");
			_Valid = StructuralObject.SetValidValue(value);
			ReportPropertyChanged("Valid");
		}
	}

	[DataMember]
	[EdmScalarProperty(EntityKeyProperty = false, IsNullable = true)]
	public int? IsBlack
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

	[EdmScalarProperty(EntityKeyProperty = false, IsNullable = true)]
	[DataMember]
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

	public static BOC_Gate_TransactionExtend CreateBOC_Gate_TransactionExtend(int transactionID, decimal cardBillAmount, decimal cardRemain, int fromGateID, int sysTransacionID, DateTime transactionTime, bool iSUploaded)
	{
		BOC_Gate_TransactionExtend bOC_Gate_TransactionExtend = new BOC_Gate_TransactionExtend();
		bOC_Gate_TransactionExtend.TransactionID = transactionID;
		bOC_Gate_TransactionExtend.CardBillAmount = cardBillAmount;
		bOC_Gate_TransactionExtend.CardRemain = cardRemain;
		bOC_Gate_TransactionExtend.FromGateID = fromGateID;
		bOC_Gate_TransactionExtend.SysTransacionID = sysTransacionID;
		bOC_Gate_TransactionExtend.TransactionTime = transactionTime;
		bOC_Gate_TransactionExtend.ISUploaded = iSUploaded;
		return bOC_Gate_TransactionExtend;
	}
}
