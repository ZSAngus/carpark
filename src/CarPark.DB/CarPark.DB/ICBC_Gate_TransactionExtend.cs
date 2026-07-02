using System;
using System.Data.Objects.DataClasses;
using System.Runtime.Serialization;

namespace CarPark.DB;

[Serializable]
[DataContract(IsReference = true)]
[EdmEntityType(NamespaceName = "CarPark.DB", Name = "ICBC_Gate_TransactionExtend")]
public class ICBC_Gate_TransactionExtend : EntityObject
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

	private string _DeviceCode;

	private string _ReceiverCode;

	private string _CardReaderNumber;

	private string _PosNo;

	private string _CardVaildDate;

	private string _IC_Data;

	private string _AlternateData;

	private string _ReplyCode;

	private string _ErrorCode;

	private int? _REQUEST_CARD_State;

	private int? _PURCHASE_CARD_State;

	private string _EncryptedCardNumber;

	private string _StaffInfo;

	private bool? _Valid;

	private int? _IsBlack;

	private string _Description;

	private int _FromGateID;

	private int _SysTransacionID;

	private bool _ISUploaded;

	private DateTime _TransactionTime;

	private string _Purchase_FullData;

	private string _CardPhyType;

	private string _CardAppType;

	private string _BillAreaB;

	private decimal? _OffLineRemain_MOP;

	private decimal? _OffLineRemain_RMB;

	private int? _ChargeTransactionID;

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

	[EdmScalarProperty(EntityKeyProperty = false, IsNullable = false)]
	[DataMember]
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

	[DataMember]
	[EdmScalarProperty(EntityKeyProperty = false, IsNullable = true)]
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

	[DataMember]
	[EdmScalarProperty(EntityKeyProperty = false, IsNullable = true)]
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
	public string PosNo
	{
		get
		{
			return _PosNo;
		}
		set
		{
			ReportPropertyChanging("PosNo");
			_PosNo = StructuralObject.SetValidValue(value, isNullable: true);
			ReportPropertyChanged("PosNo");
		}
	}

	[EdmScalarProperty(EntityKeyProperty = false, IsNullable = true)]
	[DataMember]
	public string CardVaildDate
	{
		get
		{
			return _CardVaildDate;
		}
		set
		{
			ReportPropertyChanging("CardVaildDate");
			_CardVaildDate = StructuralObject.SetValidValue(value, isNullable: true);
			ReportPropertyChanged("CardVaildDate");
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

	[DataMember]
	[EdmScalarProperty(EntityKeyProperty = false, IsNullable = true)]
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

	[DataMember]
	[EdmScalarProperty(EntityKeyProperty = false, IsNullable = true)]
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

	[EdmScalarProperty(EntityKeyProperty = false, IsNullable = true)]
	[DataMember]
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

	[EdmScalarProperty(EntityKeyProperty = false, IsNullable = true)]
	[DataMember]
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

	[EdmScalarProperty(EntityKeyProperty = false, IsNullable = true)]
	[DataMember]
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

	[DataMember]
	[EdmScalarProperty(EntityKeyProperty = false, IsNullable = true)]
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

	[EdmScalarProperty(EntityKeyProperty = false, IsNullable = false)]
	[DataMember]
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

	[EdmScalarProperty(EntityKeyProperty = false, IsNullable = true)]
	[DataMember]
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

	public static ICBC_Gate_TransactionExtend CreateICBC_Gate_TransactionExtend(int transactionID, decimal cardBillAmount, decimal cardRemain, int fromGateID, int sysTransacionID, bool iSUploaded, DateTime transactionTime)
	{
		ICBC_Gate_TransactionExtend iCBC_Gate_TransactionExtend = new ICBC_Gate_TransactionExtend();
		iCBC_Gate_TransactionExtend.TransactionID = transactionID;
		iCBC_Gate_TransactionExtend.CardBillAmount = cardBillAmount;
		iCBC_Gate_TransactionExtend.CardRemain = cardRemain;
		iCBC_Gate_TransactionExtend.FromGateID = fromGateID;
		iCBC_Gate_TransactionExtend.SysTransacionID = sysTransacionID;
		iCBC_Gate_TransactionExtend.ISUploaded = iSUploaded;
		iCBC_Gate_TransactionExtend.TransactionTime = transactionTime;
		return iCBC_Gate_TransactionExtend;
	}
}
