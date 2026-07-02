using System;
using System.Data.Objects.DataClasses;
using System.Runtime.Serialization;

namespace CarPark.DB;

[Serializable]
[DataContract(IsReference = true)]
[EdmEntityType(NamespaceName = "CarPark.DB", Name = "BOC_N910_POS_Card_Payment_DetailEX")]
public class BOC_N910_POS_Card_Payment_DetailEX : EntityObject
{
	private int _ID;

	private string _VERSION;

	private string _CMD;

	private string _STATUS;

	private string _TXNDATE;

	private string _TXNTIME;

	private string _RCODE;

	private string _MESSAGE;

	private decimal? _AMOUNT;

	private string _AUTHCODE;

	private string _PAN;

	private string _EXPIRYDATE;

	private string _ENTERMODE;

	private string _TRACEID;

	private string _TERMINALID;

	private string _MERCHANTID;

	private string _CARDTYPE;

	private string _CARDHOLDERNAME;

	private string _BATCHNO;

	private string _REFERENCENO;

	private string _ISSIGNATURE;

	private string _REQUESTID;

	private int? _ChargeRecordID;

	private int? _TransactionID;

	[EdmScalarProperty(EntityKeyProperty = true, IsNullable = false)]
	[DataMember]
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

	[DataMember]
	[EdmScalarProperty(EntityKeyProperty = false, IsNullable = true)]
	public string VERSION
	{
		get
		{
			return _VERSION;
		}
		set
		{
			ReportPropertyChanging("VERSION");
			_VERSION = StructuralObject.SetValidValue(value, isNullable: true);
			ReportPropertyChanged("VERSION");
		}
	}

	[DataMember]
	[EdmScalarProperty(EntityKeyProperty = false, IsNullable = true)]
	public string CMD
	{
		get
		{
			return _CMD;
		}
		set
		{
			ReportPropertyChanging("CMD");
			_CMD = StructuralObject.SetValidValue(value, isNullable: true);
			ReportPropertyChanged("CMD");
		}
	}

	[DataMember]
	[EdmScalarProperty(EntityKeyProperty = false, IsNullable = true)]
	public string STATUS
	{
		get
		{
			return _STATUS;
		}
		set
		{
			ReportPropertyChanging("STATUS");
			_STATUS = StructuralObject.SetValidValue(value, isNullable: true);
			ReportPropertyChanged("STATUS");
		}
	}

	[DataMember]
	[EdmScalarProperty(EntityKeyProperty = false, IsNullable = true)]
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

	[DataMember]
	[EdmScalarProperty(EntityKeyProperty = false, IsNullable = true)]
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

	[EdmScalarProperty(EntityKeyProperty = false, IsNullable = true)]
	[DataMember]
	public string RCODE
	{
		get
		{
			return _RCODE;
		}
		set
		{
			ReportPropertyChanging("RCODE");
			_RCODE = StructuralObject.SetValidValue(value, isNullable: true);
			ReportPropertyChanged("RCODE");
		}
	}

	[EdmScalarProperty(EntityKeyProperty = false, IsNullable = true)]
	[DataMember]
	public string MESSAGE
	{
		get
		{
			return _MESSAGE;
		}
		set
		{
			ReportPropertyChanging("MESSAGE");
			_MESSAGE = StructuralObject.SetValidValue(value, isNullable: true);
			ReportPropertyChanged("MESSAGE");
		}
	}

	[EdmScalarProperty(EntityKeyProperty = false, IsNullable = true)]
	[DataMember]
	public decimal? AMOUNT
	{
		get
		{
			return _AMOUNT;
		}
		set
		{
			ReportPropertyChanging("AMOUNT");
			_AMOUNT = StructuralObject.SetValidValue(value);
			ReportPropertyChanged("AMOUNT");
		}
	}

	[EdmScalarProperty(EntityKeyProperty = false, IsNullable = true)]
	[DataMember]
	public string AUTHCODE
	{
		get
		{
			return _AUTHCODE;
		}
		set
		{
			ReportPropertyChanging("AUTHCODE");
			_AUTHCODE = StructuralObject.SetValidValue(value, isNullable: true);
			ReportPropertyChanged("AUTHCODE");
		}
	}

	[EdmScalarProperty(EntityKeyProperty = false, IsNullable = true)]
	[DataMember]
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

	[EdmScalarProperty(EntityKeyProperty = false, IsNullable = true)]
	[DataMember]
	public string EXPIRYDATE
	{
		get
		{
			return _EXPIRYDATE;
		}
		set
		{
			ReportPropertyChanging("EXPIRYDATE");
			_EXPIRYDATE = StructuralObject.SetValidValue(value, isNullable: true);
			ReportPropertyChanged("EXPIRYDATE");
		}
	}

	[DataMember]
	[EdmScalarProperty(EntityKeyProperty = false, IsNullable = true)]
	public string ENTERMODE
	{
		get
		{
			return _ENTERMODE;
		}
		set
		{
			ReportPropertyChanging("ENTERMODE");
			_ENTERMODE = StructuralObject.SetValidValue(value, isNullable: true);
			ReportPropertyChanged("ENTERMODE");
		}
	}

	[DataMember]
	[EdmScalarProperty(EntityKeyProperty = false, IsNullable = true)]
	public string TRACEID
	{
		get
		{
			return _TRACEID;
		}
		set
		{
			ReportPropertyChanging("TRACEID");
			_TRACEID = StructuralObject.SetValidValue(value, isNullable: true);
			ReportPropertyChanged("TRACEID");
		}
	}

	[DataMember]
	[EdmScalarProperty(EntityKeyProperty = false, IsNullable = true)]
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
	public string CARDHOLDERNAME
	{
		get
		{
			return _CARDHOLDERNAME;
		}
		set
		{
			ReportPropertyChanging("CARDHOLDERNAME");
			_CARDHOLDERNAME = StructuralObject.SetValidValue(value, isNullable: true);
			ReportPropertyChanged("CARDHOLDERNAME");
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
	public string REFERENCENO
	{
		get
		{
			return _REFERENCENO;
		}
		set
		{
			ReportPropertyChanging("REFERENCENO");
			_REFERENCENO = StructuralObject.SetValidValue(value, isNullable: true);
			ReportPropertyChanged("REFERENCENO");
		}
	}

	[EdmScalarProperty(EntityKeyProperty = false, IsNullable = true)]
	[DataMember]
	public string ISSIGNATURE
	{
		get
		{
			return _ISSIGNATURE;
		}
		set
		{
			ReportPropertyChanging("ISSIGNATURE");
			_ISSIGNATURE = StructuralObject.SetValidValue(value, isNullable: true);
			ReportPropertyChanged("ISSIGNATURE");
		}
	}

	[DataMember]
	[EdmScalarProperty(EntityKeyProperty = false, IsNullable = true)]
	public string REQUESTID
	{
		get
		{
			return _REQUESTID;
		}
		set
		{
			ReportPropertyChanging("REQUESTID");
			_REQUESTID = StructuralObject.SetValidValue(value, isNullable: true);
			ReportPropertyChanged("REQUESTID");
		}
	}

	[EdmScalarProperty(EntityKeyProperty = false, IsNullable = true)]
	[DataMember]
	public int? ChargeRecordID
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
	[EdmScalarProperty(EntityKeyProperty = false, IsNullable = true)]
	public int? TransactionID
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

	public static BOC_N910_POS_Card_Payment_DetailEX CreateBOC_N910_POS_Card_Payment_DetailEX(int id)
	{
		BOC_N910_POS_Card_Payment_DetailEX bOC_N910_POS_Card_Payment_DetailEX = new BOC_N910_POS_Card_Payment_DetailEX();
		bOC_N910_POS_Card_Payment_DetailEX.ID = id;
		return bOC_N910_POS_Card_Payment_DetailEX;
	}
}
