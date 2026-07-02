using System;
using System.Data.Objects.DataClasses;
using System.Runtime.Serialization;

namespace CarPark.DB;

[Serializable]
[DataContract(IsReference = true)]
[EdmEntityType(NamespaceName = "CarPark.DB", Name = "MPass_POS_Signin")]
public class MPass_POS_Signin : EntityObject
{
	private int _SigiInID;

	private DateTime _SignInTime;

	private int _ShiftID;

	private string _TERMINALID;

	private string _MERCHANTID;

	private string _BATCHNO;

	private string _TXNDATE;

	private string _TXNTIME;

	private decimal _CSNUM;

	private decimal _CSAMT;

	private decimal _CRNUM;

	private decimal _CRNUM_HKD;

	private decimal _CRNUM_RMB;

	private decimal _CRAMT;

	private decimal _CRAMT_HKD;

	private decimal _CRAMT_RMB;

	private decimal _CANUM;

	private decimal _CAAMT;

	private decimal _CDEPOSIT;

	private decimal _CSCHAR;

	private decimal _MSNUM;

	private decimal _MSAMT;

	private decimal _MRNUM;

	private decimal _MRAMT;

	private decimal _MANUM;

	private decimal _MAAMT;

	private decimal _MDEPOSIT;

	private decimal _MSCHAR;

	private decimal _MCSNUM;

	private decimal _MCSAMT;

	private decimal _MCRNUM;

	private decimal _MCRAMT;

	private decimal _ERRORNUM;

	private string _TXNNO;

	private int _CommandResult;

	private string _ErrDescription;

	private string _RETCODE;

	private string _STATUS;

	[DataMember]
	[EdmScalarProperty(EntityKeyProperty = true, IsNullable = false)]
	public int SigiInID
	{
		get
		{
			return _SigiInID;
		}
		set
		{
			if (_SigiInID != value)
			{
				ReportPropertyChanging("SigiInID");
				_SigiInID = StructuralObject.SetValidValue(value);
				ReportPropertyChanged("SigiInID");
			}
		}
	}

	[DataMember]
	[EdmScalarProperty(EntityKeyProperty = false, IsNullable = false)]
	public DateTime SignInTime
	{
		get
		{
			return _SignInTime;
		}
		set
		{
			ReportPropertyChanging("SignInTime");
			_SignInTime = StructuralObject.SetValidValue(value);
			ReportPropertyChanged("SignInTime");
		}
	}

	[DataMember]
	[EdmScalarProperty(EntityKeyProperty = false, IsNullable = false)]
	public int ShiftID
	{
		get
		{
			return _ShiftID;
		}
		set
		{
			ReportPropertyChanging("ShiftID");
			_ShiftID = StructuralObject.SetValidValue(value);
			ReportPropertyChanged("ShiftID");
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

	[DataMember]
	[EdmScalarProperty(EntityKeyProperty = false, IsNullable = true)]
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
	[EdmScalarProperty(EntityKeyProperty = false, IsNullable = false)]
	public decimal CSNUM
	{
		get
		{
			return _CSNUM;
		}
		set
		{
			ReportPropertyChanging("CSNUM");
			_CSNUM = StructuralObject.SetValidValue(value);
			ReportPropertyChanged("CSNUM");
		}
	}

	[DataMember]
	[EdmScalarProperty(EntityKeyProperty = false, IsNullable = false)]
	public decimal CSAMT
	{
		get
		{
			return _CSAMT;
		}
		set
		{
			ReportPropertyChanging("CSAMT");
			_CSAMT = StructuralObject.SetValidValue(value);
			ReportPropertyChanged("CSAMT");
		}
	}

	[EdmScalarProperty(EntityKeyProperty = false, IsNullable = false)]
	[DataMember]
	public decimal CRNUM
	{
		get
		{
			return _CRNUM;
		}
		set
		{
			ReportPropertyChanging("CRNUM");
			_CRNUM = StructuralObject.SetValidValue(value);
			ReportPropertyChanged("CRNUM");
		}
	}

	[DataMember]
	[EdmScalarProperty(EntityKeyProperty = false, IsNullable = false)]
	public decimal CRNUM_HKD
	{
		get
		{
			return _CRNUM_HKD;
		}
		set
		{
			ReportPropertyChanging("CRNUM_HKD");
			_CRNUM_HKD = StructuralObject.SetValidValue(value);
			ReportPropertyChanged("CRNUM_HKD");
		}
	}

	[DataMember]
	[EdmScalarProperty(EntityKeyProperty = false, IsNullable = false)]
	public decimal CRNUM_RMB
	{
		get
		{
			return _CRNUM_RMB;
		}
		set
		{
			ReportPropertyChanging("CRNUM_RMB");
			_CRNUM_RMB = StructuralObject.SetValidValue(value);
			ReportPropertyChanged("CRNUM_RMB");
		}
	}

	[DataMember]
	[EdmScalarProperty(EntityKeyProperty = false, IsNullable = false)]
	public decimal CRAMT
	{
		get
		{
			return _CRAMT;
		}
		set
		{
			ReportPropertyChanging("CRAMT");
			_CRAMT = StructuralObject.SetValidValue(value);
			ReportPropertyChanged("CRAMT");
		}
	}

	[DataMember]
	[EdmScalarProperty(EntityKeyProperty = false, IsNullable = false)]
	public decimal CRAMT_HKD
	{
		get
		{
			return _CRAMT_HKD;
		}
		set
		{
			ReportPropertyChanging("CRAMT_HKD");
			_CRAMT_HKD = StructuralObject.SetValidValue(value);
			ReportPropertyChanged("CRAMT_HKD");
		}
	}

	[EdmScalarProperty(EntityKeyProperty = false, IsNullable = false)]
	[DataMember]
	public decimal CRAMT_RMB
	{
		get
		{
			return _CRAMT_RMB;
		}
		set
		{
			ReportPropertyChanging("CRAMT_RMB");
			_CRAMT_RMB = StructuralObject.SetValidValue(value);
			ReportPropertyChanged("CRAMT_RMB");
		}
	}

	[DataMember]
	[EdmScalarProperty(EntityKeyProperty = false, IsNullable = false)]
	public decimal CANUM
	{
		get
		{
			return _CANUM;
		}
		set
		{
			ReportPropertyChanging("CANUM");
			_CANUM = StructuralObject.SetValidValue(value);
			ReportPropertyChanged("CANUM");
		}
	}

	[EdmScalarProperty(EntityKeyProperty = false, IsNullable = false)]
	[DataMember]
	public decimal CAAMT
	{
		get
		{
			return _CAAMT;
		}
		set
		{
			ReportPropertyChanging("CAAMT");
			_CAAMT = StructuralObject.SetValidValue(value);
			ReportPropertyChanged("CAAMT");
		}
	}

	[EdmScalarProperty(EntityKeyProperty = false, IsNullable = false)]
	[DataMember]
	public decimal CDEPOSIT
	{
		get
		{
			return _CDEPOSIT;
		}
		set
		{
			ReportPropertyChanging("CDEPOSIT");
			_CDEPOSIT = StructuralObject.SetValidValue(value);
			ReportPropertyChanged("CDEPOSIT");
		}
	}

	[DataMember]
	[EdmScalarProperty(EntityKeyProperty = false, IsNullable = false)]
	public decimal CSCHAR
	{
		get
		{
			return _CSCHAR;
		}
		set
		{
			ReportPropertyChanging("CSCHAR");
			_CSCHAR = StructuralObject.SetValidValue(value);
			ReportPropertyChanged("CSCHAR");
		}
	}

	[EdmScalarProperty(EntityKeyProperty = false, IsNullable = false)]
	[DataMember]
	public decimal MSNUM
	{
		get
		{
			return _MSNUM;
		}
		set
		{
			ReportPropertyChanging("MSNUM");
			_MSNUM = StructuralObject.SetValidValue(value);
			ReportPropertyChanged("MSNUM");
		}
	}

	[EdmScalarProperty(EntityKeyProperty = false, IsNullable = false)]
	[DataMember]
	public decimal MSAMT
	{
		get
		{
			return _MSAMT;
		}
		set
		{
			ReportPropertyChanging("MSAMT");
			_MSAMT = StructuralObject.SetValidValue(value);
			ReportPropertyChanged("MSAMT");
		}
	}

	[EdmScalarProperty(EntityKeyProperty = false, IsNullable = false)]
	[DataMember]
	public decimal MRNUM
	{
		get
		{
			return _MRNUM;
		}
		set
		{
			ReportPropertyChanging("MRNUM");
			_MRNUM = StructuralObject.SetValidValue(value);
			ReportPropertyChanged("MRNUM");
		}
	}

	[DataMember]
	[EdmScalarProperty(EntityKeyProperty = false, IsNullable = false)]
	public decimal MRAMT
	{
		get
		{
			return _MRAMT;
		}
		set
		{
			ReportPropertyChanging("MRAMT");
			_MRAMT = StructuralObject.SetValidValue(value);
			ReportPropertyChanged("MRAMT");
		}
	}

	[DataMember]
	[EdmScalarProperty(EntityKeyProperty = false, IsNullable = false)]
	public decimal MANUM
	{
		get
		{
			return _MANUM;
		}
		set
		{
			ReportPropertyChanging("MANUM");
			_MANUM = StructuralObject.SetValidValue(value);
			ReportPropertyChanged("MANUM");
		}
	}

	[EdmScalarProperty(EntityKeyProperty = false, IsNullable = false)]
	[DataMember]
	public decimal MAAMT
	{
		get
		{
			return _MAAMT;
		}
		set
		{
			ReportPropertyChanging("MAAMT");
			_MAAMT = StructuralObject.SetValidValue(value);
			ReportPropertyChanged("MAAMT");
		}
	}

	[DataMember]
	[EdmScalarProperty(EntityKeyProperty = false, IsNullable = false)]
	public decimal MDEPOSIT
	{
		get
		{
			return _MDEPOSIT;
		}
		set
		{
			ReportPropertyChanging("MDEPOSIT");
			_MDEPOSIT = StructuralObject.SetValidValue(value);
			ReportPropertyChanged("MDEPOSIT");
		}
	}

	[DataMember]
	[EdmScalarProperty(EntityKeyProperty = false, IsNullable = false)]
	public decimal MSCHAR
	{
		get
		{
			return _MSCHAR;
		}
		set
		{
			ReportPropertyChanging("MSCHAR");
			_MSCHAR = StructuralObject.SetValidValue(value);
			ReportPropertyChanged("MSCHAR");
		}
	}

	[EdmScalarProperty(EntityKeyProperty = false, IsNullable = false)]
	[DataMember]
	public decimal MCSNUM
	{
		get
		{
			return _MCSNUM;
		}
		set
		{
			ReportPropertyChanging("MCSNUM");
			_MCSNUM = StructuralObject.SetValidValue(value);
			ReportPropertyChanged("MCSNUM");
		}
	}

	[EdmScalarProperty(EntityKeyProperty = false, IsNullable = false)]
	[DataMember]
	public decimal MCSAMT
	{
		get
		{
			return _MCSAMT;
		}
		set
		{
			ReportPropertyChanging("MCSAMT");
			_MCSAMT = StructuralObject.SetValidValue(value);
			ReportPropertyChanged("MCSAMT");
		}
	}

	[EdmScalarProperty(EntityKeyProperty = false, IsNullable = false)]
	[DataMember]
	public decimal MCRNUM
	{
		get
		{
			return _MCRNUM;
		}
		set
		{
			ReportPropertyChanging("MCRNUM");
			_MCRNUM = StructuralObject.SetValidValue(value);
			ReportPropertyChanged("MCRNUM");
		}
	}

	[EdmScalarProperty(EntityKeyProperty = false, IsNullable = false)]
	[DataMember]
	public decimal MCRAMT
	{
		get
		{
			return _MCRAMT;
		}
		set
		{
			ReportPropertyChanging("MCRAMT");
			_MCRAMT = StructuralObject.SetValidValue(value);
			ReportPropertyChanged("MCRAMT");
		}
	}

	[EdmScalarProperty(EntityKeyProperty = false, IsNullable = false)]
	[DataMember]
	public decimal ERRORNUM
	{
		get
		{
			return _ERRORNUM;
		}
		set
		{
			ReportPropertyChanging("ERRORNUM");
			_ERRORNUM = StructuralObject.SetValidValue(value);
			ReportPropertyChanged("ERRORNUM");
		}
	}

	[DataMember]
	[EdmScalarProperty(EntityKeyProperty = false, IsNullable = true)]
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

	[EdmScalarProperty(EntityKeyProperty = false, IsNullable = false)]
	[DataMember]
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

	[EdmScalarProperty(EntityKeyProperty = false, IsNullable = true)]
	[DataMember]
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

	[DataMember]
	[EdmScalarProperty(EntityKeyProperty = false, IsNullable = true)]
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

	public static MPass_POS_Signin CreateMPass_POS_Signin(int sigiInID, DateTime signInTime, int shiftID, decimal cSNUM, decimal cSAMT, decimal cRNUM, decimal cRNUM_HKD, decimal cRNUM_RMB, decimal cRAMT, decimal cRAMT_HKD, decimal cRAMT_RMB, decimal cANUM, decimal cAAMT, decimal cDEPOSIT, decimal cSCHAR, decimal mSNUM, decimal mSAMT, decimal mRNUM, decimal mRAMT, decimal mANUM, decimal mAAMT, decimal mDEPOSIT, decimal mSCHAR, decimal mCSNUM, decimal mCSAMT, decimal mCRNUM, decimal mCRAMT, decimal eRRORNUM, int commandResult)
	{
		MPass_POS_Signin mPass_POS_Signin = new MPass_POS_Signin();
		mPass_POS_Signin.SigiInID = sigiInID;
		mPass_POS_Signin.SignInTime = signInTime;
		mPass_POS_Signin.ShiftID = shiftID;
		mPass_POS_Signin.CSNUM = cSNUM;
		mPass_POS_Signin.CSAMT = cSAMT;
		mPass_POS_Signin.CRNUM = cRNUM;
		mPass_POS_Signin.CRNUM_HKD = cRNUM_HKD;
		mPass_POS_Signin.CRNUM_RMB = cRNUM_RMB;
		mPass_POS_Signin.CRAMT = cRAMT;
		mPass_POS_Signin.CRAMT_HKD = cRAMT_HKD;
		mPass_POS_Signin.CRAMT_RMB = cRAMT_RMB;
		mPass_POS_Signin.CANUM = cANUM;
		mPass_POS_Signin.CAAMT = cAAMT;
		mPass_POS_Signin.CDEPOSIT = cDEPOSIT;
		mPass_POS_Signin.CSCHAR = cSCHAR;
		mPass_POS_Signin.MSNUM = mSNUM;
		mPass_POS_Signin.MSAMT = mSAMT;
		mPass_POS_Signin.MRNUM = mRNUM;
		mPass_POS_Signin.MRAMT = mRAMT;
		mPass_POS_Signin.MANUM = mANUM;
		mPass_POS_Signin.MAAMT = mAAMT;
		mPass_POS_Signin.MDEPOSIT = mDEPOSIT;
		mPass_POS_Signin.MSCHAR = mSCHAR;
		mPass_POS_Signin.MCSNUM = mCSNUM;
		mPass_POS_Signin.MCSAMT = mCSAMT;
		mPass_POS_Signin.MCRNUM = mCRNUM;
		mPass_POS_Signin.MCRAMT = mCRAMT;
		mPass_POS_Signin.ERRORNUM = eRRORNUM;
		mPass_POS_Signin.CommandResult = commandResult;
		return mPass_POS_Signin;
	}
}
