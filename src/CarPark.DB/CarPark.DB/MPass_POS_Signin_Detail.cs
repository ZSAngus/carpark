using System;
using System.Data.Objects.DataClasses;
using System.Runtime.Serialization;

namespace CarPark.DB;

[Serializable]
[DataContract(IsReference = true)]
[EdmEntityType(NamespaceName = "CarPark.DB", Name = "MPass_POS_Signin_Detail")]
public class MPass_POS_Signin_Detail : EntityObject
{
	private int _SignInDetailID;

	private int _SiginInID;

	private decimal _ERRORNO;

	private string _INVOICENO;

	private string _TXNTYPE;

	private string _TXNDATE;

	private string _TXNTIME;

	private string _PAN;

	private string _AUTH;

	private decimal? _TOTALAMT;

	[EdmScalarProperty(EntityKeyProperty = true, IsNullable = false)]
	[DataMember]
	public int SignInDetailID
	{
		get
		{
			return _SignInDetailID;
		}
		set
		{
			if (_SignInDetailID != value)
			{
				ReportPropertyChanging("SignInDetailID");
				_SignInDetailID = StructuralObject.SetValidValue(value);
				ReportPropertyChanged("SignInDetailID");
			}
		}
	}

	[DataMember]
	[EdmScalarProperty(EntityKeyProperty = false, IsNullable = false)]
	public int SiginInID
	{
		get
		{
			return _SiginInID;
		}
		set
		{
			ReportPropertyChanging("SiginInID");
			_SiginInID = StructuralObject.SetValidValue(value);
			ReportPropertyChanged("SiginInID");
		}
	}

	[EdmScalarProperty(EntityKeyProperty = false, IsNullable = false)]
	[DataMember]
	public decimal ERRORNO
	{
		get
		{
			return _ERRORNO;
		}
		set
		{
			ReportPropertyChanging("ERRORNO");
			_ERRORNO = StructuralObject.SetValidValue(value);
			ReportPropertyChanged("ERRORNO");
		}
	}

	[DataMember]
	[EdmScalarProperty(EntityKeyProperty = false, IsNullable = true)]
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
	public decimal? TOTALAMT
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

	public static MPass_POS_Signin_Detail CreateMPass_POS_Signin_Detail(int signInDetailID, int siginInID, decimal eRRORNO)
	{
		MPass_POS_Signin_Detail mPass_POS_Signin_Detail = new MPass_POS_Signin_Detail();
		mPass_POS_Signin_Detail.SignInDetailID = signInDetailID;
		mPass_POS_Signin_Detail.SiginInID = siginInID;
		mPass_POS_Signin_Detail.ERRORNO = eRRORNO;
		return mPass_POS_Signin_Detail;
	}
}
