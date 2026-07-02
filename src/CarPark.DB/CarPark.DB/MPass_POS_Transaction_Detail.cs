using System;
using System.Data.Objects.DataClasses;
using System.Runtime.Serialization;

namespace CarPark.DB;

[Serializable]
[EdmEntityType(NamespaceName = "CarPark.DB", Name = "MPass_POS_Transaction_Detail")]
[DataContract(IsReference = true)]
public class MPass_POS_Transaction_Detail : EntityObject
{
	private string _PAY_MODE;

	private string _PAY_ACCOUNT;

	private string _PAY_PURSETYPE;

	private decimal? _PAY_TOTALAMT;

	private string _PAY_INVOICENO;

	private decimal? _DISCOUNTAMT;

	private decimal? _ORIGTXNAMT;

	private string _DISCOUNTTYPE;

	private string _COUPONTYPE;

	private string _COUPONCODE;

	private decimal? _COUPONDEDAMT;

	private decimal? _ACTUALAMT;

	private string _MERCHANTORDERNO;

	private int _TransactionID;

	private int _ChargeTransactionID;

	private DateTime _TransactionTime;

	private string _STATUS;

	private string _TERMINALID;

	private string _MERCHANTID;

	private string _INVOICENO;

	private string _TXNDATE;

	private string _TXNTIME;

	private string _TXNTYPE;

	private string _CARDTYPE;

	private string _MPTXNTYPE;

	private string _PAN;

	private decimal _TOTALAMT;

	private decimal _BALANCE;

	private string _LOGNO;

	private string _M1CARDTYPE;

	private decimal _ORIGBALANCE;

	private decimal _DEPOSIT;

	private decimal _CHARGE;

	private string _TXNTAC;

	private string _TXNNO;

	private string _PURSETYPE;

	private string _EXPDATE;

	private decimal _EPA;

	private decimal _EPAAMT;

	private string _BATCHNO;

	private string _VOCNO;

	private string _REFNO;

	private string _AUTH;

	private string _AID;

	private string _TC;

	private string _TVR;

	private string _TSI;

	private string _ATC;

	private string _APPLAB;

	private int _TransactionType;

	private int _CommandResult;

	private string _ErrDescription;

	private string _RETCODE;

	private string _TransactionSEQ;

	private string _CashType;

	[EdmScalarProperty(EntityKeyProperty = false, IsNullable = true)]
	[DataMember]
	public string PAY_MODE
	{
		get
		{
			return _PAY_MODE;
		}
		set
		{
			ReportPropertyChanging("PAY_MODE");
			_PAY_MODE = StructuralObject.SetValidValue(value, isNullable: true);
			ReportPropertyChanged("PAY_MODE");
		}
	}

	[EdmScalarProperty(EntityKeyProperty = false, IsNullable = true)]
	[DataMember]
	public string PAY_ACCOUNT
	{
		get
		{
			return _PAY_ACCOUNT;
		}
		set
		{
			ReportPropertyChanging("PAY_ACCOUNT");
			_PAY_ACCOUNT = StructuralObject.SetValidValue(value, isNullable: true);
			ReportPropertyChanged("PAY_ACCOUNT");
		}
	}

	[EdmScalarProperty(EntityKeyProperty = false, IsNullable = true)]
	[DataMember]
	public string PAY_PURSETYPE
	{
		get
		{
			return _PAY_PURSETYPE;
		}
		set
		{
			ReportPropertyChanging("PAY_PURSETYPE");
			_PAY_PURSETYPE = StructuralObject.SetValidValue(value, isNullable: true);
			ReportPropertyChanged("PAY_PURSETYPE");
		}
	}

	[EdmScalarProperty(EntityKeyProperty = false, IsNullable = true)]
	[DataMember]
	public decimal? PAY_TOTALAMT
	{
		get
		{
			return _PAY_TOTALAMT;
		}
		set
		{
			ReportPropertyChanging("PAY_TOTALAMT");
			_PAY_TOTALAMT = StructuralObject.SetValidValue(value);
			ReportPropertyChanged("PAY_TOTALAMT");
		}
	}

	[EdmScalarProperty(EntityKeyProperty = false, IsNullable = true)]
	[DataMember]
	public string PAY_INVOICENO
	{
		get
		{
			return _PAY_INVOICENO;
		}
		set
		{
			ReportPropertyChanging("PAY_INVOICENO");
			_PAY_INVOICENO = StructuralObject.SetValidValue(value, isNullable: true);
			ReportPropertyChanged("PAY_INVOICENO");
		}
	}

	[EdmScalarProperty(EntityKeyProperty = false, IsNullable = true)]
	[DataMember]
	public decimal? DISCOUNTAMT
	{
		get
		{
			return _DISCOUNTAMT;
		}
		set
		{
			ReportPropertyChanging("DISCOUNTAMT");
			_DISCOUNTAMT = StructuralObject.SetValidValue(value);
			ReportPropertyChanged("DISCOUNTAMT");
		}
	}

	[EdmScalarProperty(EntityKeyProperty = false, IsNullable = true)]
	[DataMember]
	public decimal? ORIGTXNAMT
	{
		get
		{
			return _ORIGTXNAMT;
		}
		set
		{
			ReportPropertyChanging("ORIGTXNAMT");
			_ORIGTXNAMT = StructuralObject.SetValidValue(value);
			ReportPropertyChanged("ORIGTXNAMT");
		}
	}

	[DataMember]
	[EdmScalarProperty(EntityKeyProperty = false, IsNullable = true)]
	public string DISCOUNTTYPE
	{
		get
		{
			return _DISCOUNTTYPE;
		}
		set
		{
			ReportPropertyChanging("DISCOUNTTYPE");
			_DISCOUNTTYPE = StructuralObject.SetValidValue(value, isNullable: true);
			ReportPropertyChanged("DISCOUNTTYPE");
		}
	}

	[DataMember]
	[EdmScalarProperty(EntityKeyProperty = false, IsNullable = true)]
	public string COUPONTYPE
	{
		get
		{
			return _COUPONTYPE;
		}
		set
		{
			ReportPropertyChanging("COUPONTYPE");
			_COUPONTYPE = StructuralObject.SetValidValue(value, isNullable: true);
			ReportPropertyChanged("COUPONTYPE");
		}
	}

	[EdmScalarProperty(EntityKeyProperty = false, IsNullable = true)]
	[DataMember]
	public string COUPONCODE
	{
		get
		{
			return _COUPONCODE;
		}
		set
		{
			ReportPropertyChanging("COUPONCODE");
			_COUPONCODE = StructuralObject.SetValidValue(value, isNullable: true);
			ReportPropertyChanged("COUPONCODE");
		}
	}

	[DataMember]
	[EdmScalarProperty(EntityKeyProperty = false, IsNullable = true)]
	public decimal? COUPONDEDAMT
	{
		get
		{
			return _COUPONDEDAMT;
		}
		set
		{
			ReportPropertyChanging("COUPONDEDAMT");
			_COUPONDEDAMT = StructuralObject.SetValidValue(value);
			ReportPropertyChanged("COUPONDEDAMT");
		}
	}

	[EdmScalarProperty(EntityKeyProperty = false, IsNullable = true)]
	[DataMember]
	public decimal? ACTUALAMT
	{
		get
		{
			return _ACTUALAMT;
		}
		set
		{
			ReportPropertyChanging("ACTUALAMT");
			_ACTUALAMT = StructuralObject.SetValidValue(value);
			ReportPropertyChanged("ACTUALAMT");
		}
	}

	[EdmScalarProperty(EntityKeyProperty = false, IsNullable = true)]
	[DataMember]
	public string MERCHANTORDERNO
	{
		get
		{
			return _MERCHANTORDERNO;
		}
		set
		{
			ReportPropertyChanging("MERCHANTORDERNO");
			_MERCHANTORDERNO = StructuralObject.SetValidValue(value, isNullable: true);
			ReportPropertyChanged("MERCHANTORDERNO");
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

	[EdmScalarProperty(EntityKeyProperty = false, IsNullable = false)]
	[DataMember]
	public int ChargeTransactionID
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
	[EdmScalarProperty(EntityKeyProperty = false, IsNullable = false)]
	public string STATUS
	{
		get
		{
			return _STATUS;
		}
		set
		{
			ReportPropertyChanging("STATUS");
			_STATUS = StructuralObject.SetValidValue(value, isNullable: false);
			ReportPropertyChanged("STATUS");
		}
	}

	[EdmScalarProperty(EntityKeyProperty = false, IsNullable = true)]
	[DataMember]
	public string TERMINALID
	{
		get
		{
			return _TERMINALID;
		}
		set
		{
			ReportPropertyChanging("TERMINALID");
			_TERMINALID = StructuralObject.SetValidValue(value, isNullable: true);
			ReportPropertyChanged("TERMINALID");
		}
	}

	[DataMember]
	[EdmScalarProperty(EntityKeyProperty = false, IsNullable = true)]
	public string MERCHANTID
	{
		get
		{
			return _MERCHANTID;
		}
		set
		{
			ReportPropertyChanging("MERCHANTID");
			_MERCHANTID = StructuralObject.SetValidValue(value, isNullable: true);
			ReportPropertyChanged("MERCHANTID");
		}
	}

	[EdmScalarProperty(EntityKeyProperty = false, IsNullable = true)]
	[DataMember]
	public string INVOICENO
	{
		get
		{
			return _INVOICENO;
		}
		set
		{
			ReportPropertyChanging("INVOICENO");
			_INVOICENO = StructuralObject.SetValidValue(value, isNullable: true);
			ReportPropertyChanged("INVOICENO");
		}
	}

	[EdmScalarProperty(EntityKeyProperty = false, IsNullable = true)]
	[DataMember]
	public string TXNDATE
	{
		get
		{
			return _TXNDATE;
		}
		set
		{
			ReportPropertyChanging("TXNDATE");
			_TXNDATE = StructuralObject.SetValidValue(value, isNullable: true);
			ReportPropertyChanged("TXNDATE");
		}
	}

	[EdmScalarProperty(EntityKeyProperty = false, IsNullable = true)]
	[DataMember]
	public string TXNTIME
	{
		get
		{
			return _TXNTIME;
		}
		set
		{
			ReportPropertyChanging("TXNTIME");
			_TXNTIME = StructuralObject.SetValidValue(value, isNullable: true);
			ReportPropertyChanged("TXNTIME");
		}
	}

	[DataMember]
	[EdmScalarProperty(EntityKeyProperty = false, IsNullable = true)]
	public string TXNTYPE
	{
		get
		{
			return _TXNTYPE;
		}
		set
		{
			ReportPropertyChanging("TXNTYPE");
			_TXNTYPE = StructuralObject.SetValidValue(value, isNullable: true);
			ReportPropertyChanged("TXNTYPE");
		}
	}

	[DataMember]
	[EdmScalarProperty(EntityKeyProperty = false, IsNullable = true)]
	public string CARDTYPE
	{
		get
		{
			return _CARDTYPE;
		}
		set
		{
			ReportPropertyChanging("CARDTYPE");
			_CARDTYPE = StructuralObject.SetValidValue(value, isNullable: true);
			ReportPropertyChanged("CARDTYPE");
		}
	}

	[EdmScalarProperty(EntityKeyProperty = false, IsNullable = true)]
	[DataMember]
	public string MPTXNTYPE
	{
		get
		{
			return _MPTXNTYPE;
		}
		set
		{
			ReportPropertyChanging("MPTXNTYPE");
			_MPTXNTYPE = StructuralObject.SetValidValue(value, isNullable: true);
			ReportPropertyChanged("MPTXNTYPE");
		}
	}

	[DataMember]
	[EdmScalarProperty(EntityKeyProperty = false, IsNullable = true)]
	public string PAN
	{
		get
		{
			return _PAN;
		}
		set
		{
			ReportPropertyChanging("PAN");
			_PAN = StructuralObject.SetValidValue(value, isNullable: true);
			ReportPropertyChanged("PAN");
		}
	}

	[EdmScalarProperty(EntityKeyProperty = false, IsNullable = false)]
	[DataMember]
	public decimal TOTALAMT
	{
		get
		{
			return _TOTALAMT;
		}
		set
		{
			ReportPropertyChanging("TOTALAMT");
			_TOTALAMT = StructuralObject.SetValidValue(value);
			ReportPropertyChanged("TOTALAMT");
		}
	}

	[DataMember]
	[EdmScalarProperty(EntityKeyProperty = false, IsNullable = false)]
	public decimal BALANCE
	{
		get
		{
			return _BALANCE;
		}
		set
		{
			ReportPropertyChanging("BALANCE");
			_BALANCE = StructuralObject.SetValidValue(value);
			ReportPropertyChanged("BALANCE");
		}
	}

	[DataMember]
	[EdmScalarProperty(EntityKeyProperty = false, IsNullable = true)]
	public string LOGNO
	{
		get
		{
			return _LOGNO;
		}
		set
		{
			ReportPropertyChanging("LOGNO");
			_LOGNO = StructuralObject.SetValidValue(value, isNullable: true);
			ReportPropertyChanged("LOGNO");
		}
	}

	[EdmScalarProperty(EntityKeyProperty = false, IsNullable = true)]
	[DataMember]
	public string M1CARDTYPE
	{
		get
		{
			return _M1CARDTYPE;
		}
		set
		{
			ReportPropertyChanging("M1CARDTYPE");
			_M1CARDTYPE = StructuralObject.SetValidValue(value, isNullable: true);
			ReportPropertyChanged("M1CARDTYPE");
		}
	}

	[DataMember]
	[EdmScalarProperty(EntityKeyProperty = false, IsNullable = false)]
	public decimal ORIGBALANCE
	{
		get
		{
			return _ORIGBALANCE;
		}
		set
		{
			ReportPropertyChanging("ORIGBALANCE");
			_ORIGBALANCE = StructuralObject.SetValidValue(value);
			ReportPropertyChanged("ORIGBALANCE");
		}
	}

	[DataMember]
	[EdmScalarProperty(EntityKeyProperty = false, IsNullable = false)]
	public decimal DEPOSIT
	{
		get
		{
			return _DEPOSIT;
		}
		set
		{
			ReportPropertyChanging("DEPOSIT");
			_DEPOSIT = StructuralObject.SetValidValue(value);
			ReportPropertyChanged("DEPOSIT");
		}
	}

	[DataMember]
	[EdmScalarProperty(EntityKeyProperty = false, IsNullable = false)]
	public decimal CHARGE
	{
		get
		{
			return _CHARGE;
		}
		set
		{
			ReportPropertyChanging("CHARGE");
			_CHARGE = StructuralObject.SetValidValue(value);
			ReportPropertyChanged("CHARGE");
		}
	}

	[DataMember]
	[EdmScalarProperty(EntityKeyProperty = false, IsNullable = true)]
	public string TXNTAC
	{
		get
		{
			return _TXNTAC;
		}
		set
		{
			ReportPropertyChanging("TXNTAC");
			_TXNTAC = StructuralObject.SetValidValue(value, isNullable: true);
			ReportPropertyChanged("TXNTAC");
		}
	}

	[EdmScalarProperty(EntityKeyProperty = false, IsNullable = true)]
	[DataMember]
	public string TXNNO
	{
		get
		{
			return _TXNNO;
		}
		set
		{
			ReportPropertyChanging("TXNNO");
			_TXNNO = StructuralObject.SetValidValue(value, isNullable: true);
			ReportPropertyChanged("TXNNO");
		}
	}

	[DataMember]
	[EdmScalarProperty(EntityKeyProperty = false, IsNullable = true)]
	public string PURSETYPE
	{
		get
		{
			return _PURSETYPE;
		}
		set
		{
			ReportPropertyChanging("PURSETYPE");
			_PURSETYPE = StructuralObject.SetValidValue(value, isNullable: true);
			ReportPropertyChanged("PURSETYPE");
		}
	}

	[EdmScalarProperty(EntityKeyProperty = false, IsNullable = true)]
	[DataMember]
	public string EXPDATE
	{
		get
		{
			return _EXPDATE;
		}
		set
		{
			ReportPropertyChanging("EXPDATE");
			_EXPDATE = StructuralObject.SetValidValue(value, isNullable: true);
			ReportPropertyChanged("EXPDATE");
		}
	}

	[DataMember]
	[EdmScalarProperty(EntityKeyProperty = false, IsNullable = false)]
	public decimal EPA
	{
		get
		{
			return _EPA;
		}
		set
		{
			ReportPropertyChanging("EPA");
			_EPA = StructuralObject.SetValidValue(value);
			ReportPropertyChanged("EPA");
		}
	}

	[DataMember]
	[EdmScalarProperty(EntityKeyProperty = false, IsNullable = false)]
	public decimal EPAAMT
	{
		get
		{
			return _EPAAMT;
		}
		set
		{
			ReportPropertyChanging("EPAAMT");
			_EPAAMT = StructuralObject.SetValidValue(value);
			ReportPropertyChanged("EPAAMT");
		}
	}

	[EdmScalarProperty(EntityKeyProperty = false, IsNullable = true)]
	[DataMember]
	public string BATCHNO
	{
		get
		{
			return _BATCHNO;
		}
		set
		{
			ReportPropertyChanging("BATCHNO");
			_BATCHNO = StructuralObject.SetValidValue(value, isNullable: true);
			ReportPropertyChanged("BATCHNO");
		}
	}

	[DataMember]
	[EdmScalarProperty(EntityKeyProperty = false, IsNullable = true)]
	public string VOCNO
	{
		get
		{
			return _VOCNO;
		}
		set
		{
			ReportPropertyChanging("VOCNO");
			_VOCNO = StructuralObject.SetValidValue(value, isNullable: true);
			ReportPropertyChanged("VOCNO");
		}
	}

	[DataMember]
	[EdmScalarProperty(EntityKeyProperty = false, IsNullable = true)]
	public string REFNO
	{
		get
		{
			return _REFNO;
		}
		set
		{
			ReportPropertyChanging("REFNO");
			_REFNO = StructuralObject.SetValidValue(value, isNullable: true);
			ReportPropertyChanged("REFNO");
		}
	}

	[EdmScalarProperty(EntityKeyProperty = false, IsNullable = true)]
	[DataMember]
	public string AUTH
	{
		get
		{
			return _AUTH;
		}
		set
		{
			ReportPropertyChanging("AUTH");
			_AUTH = StructuralObject.SetValidValue(value, isNullable: true);
			ReportPropertyChanged("AUTH");
		}
	}

	[DataMember]
	[EdmScalarProperty(EntityKeyProperty = false, IsNullable = true)]
	public string AID
	{
		get
		{
			return _AID;
		}
		set
		{
			ReportPropertyChanging("AID");
			_AID = StructuralObject.SetValidValue(value, isNullable: true);
			ReportPropertyChanged("AID");
		}
	}

	[EdmScalarProperty(EntityKeyProperty = false, IsNullable = true)]
	[DataMember]
	public string TC
	{
		get
		{
			return _TC;
		}
		set
		{
			ReportPropertyChanging("TC");
			_TC = StructuralObject.SetValidValue(value, isNullable: true);
			ReportPropertyChanged("TC");
		}
	}

	[DataMember]
	[EdmScalarProperty(EntityKeyProperty = false, IsNullable = true)]
	public string TVR
	{
		get
		{
			return _TVR;
		}
		set
		{
			ReportPropertyChanging("TVR");
			_TVR = StructuralObject.SetValidValue(value, isNullable: true);
			ReportPropertyChanged("TVR");
		}
	}

	[DataMember]
	[EdmScalarProperty(EntityKeyProperty = false, IsNullable = true)]
	public string TSI
	{
		get
		{
			return _TSI;
		}
		set
		{
			ReportPropertyChanging("TSI");
			_TSI = StructuralObject.SetValidValue(value, isNullable: true);
			ReportPropertyChanged("TSI");
		}
	}

	[DataMember]
	[EdmScalarProperty(EntityKeyProperty = false, IsNullable = true)]
	public string ATC
	{
		get
		{
			return _ATC;
		}
		set
		{
			ReportPropertyChanging("ATC");
			_ATC = StructuralObject.SetValidValue(value, isNullable: true);
			ReportPropertyChanged("ATC");
		}
	}

	[DataMember]
	[EdmScalarProperty(EntityKeyProperty = false, IsNullable = true)]
	public string APPLAB
	{
		get
		{
			return _APPLAB;
		}
		set
		{
			ReportPropertyChanging("APPLAB");
			_APPLAB = StructuralObject.SetValidValue(value, isNullable: true);
			ReportPropertyChanged("APPLAB");
		}
	}

	[DataMember]
	[EdmScalarProperty(EntityKeyProperty = false, IsNullable = false)]
	public int TransactionType
	{
		get
		{
			return _TransactionType;
		}
		set
		{
			ReportPropertyChanging("TransactionType");
			_TransactionType = StructuralObject.SetValidValue(value);
			ReportPropertyChanged("TransactionType");
		}
	}

	[DataMember]
	[EdmScalarProperty(EntityKeyProperty = false, IsNullable = false)]
	public int CommandResult
	{
		get
		{
			return _CommandResult;
		}
		set
		{
			ReportPropertyChanging("CommandResult");
			_CommandResult = StructuralObject.SetValidValue(value);
			ReportPropertyChanged("CommandResult");
		}
	}

	[DataMember]
	[EdmScalarProperty(EntityKeyProperty = false, IsNullable = true)]
	public string ErrDescription
	{
		get
		{
			return _ErrDescription;
		}
		set
		{
			ReportPropertyChanging("ErrDescription");
			_ErrDescription = StructuralObject.SetValidValue(value, isNullable: true);
			ReportPropertyChanged("ErrDescription");
		}
	}

	[EdmScalarProperty(EntityKeyProperty = false, IsNullable = true)]
	[DataMember]
	public string RETCODE
	{
		get
		{
			return _RETCODE;
		}
		set
		{
			ReportPropertyChanging("RETCODE");
			_RETCODE = StructuralObject.SetValidValue(value, isNullable: true);
			ReportPropertyChanged("RETCODE");
		}
	}

	[EdmScalarProperty(EntityKeyProperty = false, IsNullable = true)]
	[DataMember]
	public string TransactionSEQ
	{
		get
		{
			return _TransactionSEQ;
		}
		set
		{
			ReportPropertyChanging("TransactionSEQ");
			_TransactionSEQ = StructuralObject.SetValidValue(value, isNullable: true);
			ReportPropertyChanged("TransactionSEQ");
		}
	}

	[DataMember]
	[EdmScalarProperty(EntityKeyProperty = false, IsNullable = false)]
	public string CashType
	{
		get
		{
			return _CashType;
		}
		set
		{
			ReportPropertyChanging("CashType");
			_CashType = StructuralObject.SetValidValue(value, isNullable: false);
			ReportPropertyChanged("CashType");
		}
	}

	public static MPass_POS_Transaction_Detail CreateMPass_POS_Transaction_Detail(int transactionID, int chargeTransactionID, DateTime transactionTime, string sTATUS, decimal tOTALAMT, decimal bALANCE, decimal oRIGBALANCE, decimal dEPOSIT, decimal cHARGE, decimal ePA, decimal ePAAMT, int transactionType, int commandResult, string cashType)
	{
		MPass_POS_Transaction_Detail mPass_POS_Transaction_Detail = new MPass_POS_Transaction_Detail();
		mPass_POS_Transaction_Detail.TransactionID = transactionID;
		mPass_POS_Transaction_Detail.ChargeTransactionID = chargeTransactionID;
		mPass_POS_Transaction_Detail.TransactionTime = transactionTime;
		mPass_POS_Transaction_Detail.STATUS = sTATUS;
		mPass_POS_Transaction_Detail.TOTALAMT = tOTALAMT;
		mPass_POS_Transaction_Detail.BALANCE = bALANCE;
		mPass_POS_Transaction_Detail.ORIGBALANCE = oRIGBALANCE;
		mPass_POS_Transaction_Detail.DEPOSIT = dEPOSIT;
		mPass_POS_Transaction_Detail.CHARGE = cHARGE;
		mPass_POS_Transaction_Detail.EPA = ePA;
		mPass_POS_Transaction_Detail.EPAAMT = ePAAMT;
		mPass_POS_Transaction_Detail.TransactionType = transactionType;
		mPass_POS_Transaction_Detail.CommandResult = commandResult;
		mPass_POS_Transaction_Detail.CashType = cashType;
		return mPass_POS_Transaction_Detail;
	}
}
