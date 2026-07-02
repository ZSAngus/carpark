using System;
using System.Data.Objects.DataClasses;
using System.Runtime.Serialization;

namespace CarPark.DB;

[Serializable]
[DataContract(IsReference = true)]
[EdmEntityType(NamespaceName = "CarPark.DB", Name = "BOC_Smart_Payment_DetailEX")]
public class BOC_Smart_Payment_DetailEX : EntityObject
{
	private int _SmartPaymentID;

	private int _ChargeRecordID;

	private int _TransactionID;

	private string _MerchantId;

	private string _TrmNo;

	private string _PayOrderNo;

	private decimal? _Amount;

	private string _AuthCode;

	private string _Subject;

	private string _OrdDt;

	private string _OrdTm;

	private string _ValNum;

	private string _NotifyUrl;

	private string _LogNo;

	private string _Result;

	private string _ResultMessage;

	private string _ValTime;

	private string _PayType;

	private string _ThdLogNo;

	private decimal? _CashFee;

	private string _CashFeeType;

	private string _UsrId;

	private string _MercMarketFlg;

	private string _MarketCnNm;

	private string _MarketEnNm;

	private decimal? _ActPayAmt;

	private string _ParkingLotNo;

	[DataMember]
	[EdmScalarProperty(EntityKeyProperty = true, IsNullable = false)]
	public int SmartPaymentID
	{
		get
		{
			return _SmartPaymentID;
		}
		set
		{
			if (_SmartPaymentID != value)
			{
				ReportPropertyChanging("SmartPaymentID");
				_SmartPaymentID = StructuralObject.SetValidValue(value);
				ReportPropertyChanged("SmartPaymentID");
			}
		}
	}

	[DataMember]
	[EdmScalarProperty(EntityKeyProperty = false, IsNullable = false)]
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

	[EdmScalarProperty(EntityKeyProperty = false, IsNullable = true)]
	[DataMember]
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
	public string OrdDt
	{
		get
		{
			return _OrdDt;
		}
		set
		{
			ReportPropertyChanging("OrdDt");
			_OrdDt = StructuralObject.SetValidValue(value, isNullable: true);
			ReportPropertyChanged("OrdDt");
		}
	}

	[EdmScalarProperty(EntityKeyProperty = false, IsNullable = true)]
	[DataMember]
	public string OrdTm
	{
		get
		{
			return _OrdTm;
		}
		set
		{
			ReportPropertyChanging("OrdTm");
			_OrdTm = StructuralObject.SetValidValue(value, isNullable: true);
			ReportPropertyChanged("OrdTm");
		}
	}

	[DataMember]
	[EdmScalarProperty(EntityKeyProperty = false, IsNullable = true)]
	public string ValNum
	{
		get
		{
			return _ValNum;
		}
		set
		{
			ReportPropertyChanging("ValNum");
			_ValNum = StructuralObject.SetValidValue(value, isNullable: true);
			ReportPropertyChanged("ValNum");
		}
	}

	[EdmScalarProperty(EntityKeyProperty = false, IsNullable = true)]
	[DataMember]
	public string NotifyUrl
	{
		get
		{
			return _NotifyUrl;
		}
		set
		{
			ReportPropertyChanging("NotifyUrl");
			_NotifyUrl = StructuralObject.SetValidValue(value, isNullable: true);
			ReportPropertyChanged("NotifyUrl");
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

	[DataMember]
	[EdmScalarProperty(EntityKeyProperty = false, IsNullable = true)]
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

	[DataMember]
	[EdmScalarProperty(EntityKeyProperty = false, IsNullable = true)]
	public string ValTime
	{
		get
		{
			return _ValTime;
		}
		set
		{
			ReportPropertyChanging("ValTime");
			_ValTime = StructuralObject.SetValidValue(value, isNullable: true);
			ReportPropertyChanged("ValTime");
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

	[EdmScalarProperty(EntityKeyProperty = false, IsNullable = true)]
	[DataMember]
	public string ThdLogNo
	{
		get
		{
			return _ThdLogNo;
		}
		set
		{
			ReportPropertyChanging("ThdLogNo");
			_ThdLogNo = StructuralObject.SetValidValue(value, isNullable: true);
			ReportPropertyChanged("ThdLogNo");
		}
	}

	[EdmScalarProperty(EntityKeyProperty = false, IsNullable = true)]
	[DataMember]
	public decimal? CashFee
	{
		get
		{
			return _CashFee;
		}
		set
		{
			ReportPropertyChanging("CashFee");
			_CashFee = StructuralObject.SetValidValue(value);
			ReportPropertyChanged("CashFee");
		}
	}

	[DataMember]
	[EdmScalarProperty(EntityKeyProperty = false, IsNullable = true)]
	public string CashFeeType
	{
		get
		{
			return _CashFeeType;
		}
		set
		{
			ReportPropertyChanging("CashFeeType");
			_CashFeeType = StructuralObject.SetValidValue(value, isNullable: true);
			ReportPropertyChanged("CashFeeType");
		}
	}

	[EdmScalarProperty(EntityKeyProperty = false, IsNullable = true)]
	[DataMember]
	public string UsrId
	{
		get
		{
			return _UsrId;
		}
		set
		{
			ReportPropertyChanging("UsrId");
			_UsrId = StructuralObject.SetValidValue(value, isNullable: true);
			ReportPropertyChanged("UsrId");
		}
	}

	[DataMember]
	[EdmScalarProperty(EntityKeyProperty = false, IsNullable = true)]
	public string MercMarketFlg
	{
		get
		{
			return _MercMarketFlg;
		}
		set
		{
			ReportPropertyChanging("MercMarketFlg");
			_MercMarketFlg = StructuralObject.SetValidValue(value, isNullable: true);
			ReportPropertyChanged("MercMarketFlg");
		}
	}

	[EdmScalarProperty(EntityKeyProperty = false, IsNullable = true)]
	[DataMember]
	public string MarketCnNm
	{
		get
		{
			return _MarketCnNm;
		}
		set
		{
			ReportPropertyChanging("MarketCnNm");
			_MarketCnNm = StructuralObject.SetValidValue(value, isNullable: true);
			ReportPropertyChanged("MarketCnNm");
		}
	}

	[DataMember]
	[EdmScalarProperty(EntityKeyProperty = false, IsNullable = true)]
	public string MarketEnNm
	{
		get
		{
			return _MarketEnNm;
		}
		set
		{
			ReportPropertyChanging("MarketEnNm");
			_MarketEnNm = StructuralObject.SetValidValue(value, isNullable: true);
			ReportPropertyChanged("MarketEnNm");
		}
	}

	[EdmScalarProperty(EntityKeyProperty = false, IsNullable = true)]
	[DataMember]
	public decimal? ActPayAmt
	{
		get
		{
			return _ActPayAmt;
		}
		set
		{
			ReportPropertyChanging("ActPayAmt");
			_ActPayAmt = StructuralObject.SetValidValue(value);
			ReportPropertyChanged("ActPayAmt");
		}
	}

	[DataMember]
	[EdmScalarProperty(EntityKeyProperty = false, IsNullable = true)]
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

	public static BOC_Smart_Payment_DetailEX CreateBOC_Smart_Payment_DetailEX(int smartPaymentID, int chargeRecordID, int transactionID)
	{
		BOC_Smart_Payment_DetailEX bOC_Smart_Payment_DetailEX = new BOC_Smart_Payment_DetailEX();
		bOC_Smart_Payment_DetailEX.SmartPaymentID = smartPaymentID;
		bOC_Smart_Payment_DetailEX.ChargeRecordID = chargeRecordID;
		bOC_Smart_Payment_DetailEX.TransactionID = transactionID;
		return bOC_Smart_Payment_DetailEX;
	}
}
