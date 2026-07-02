using System;
using System.Data.Objects.DataClasses;
using System.Runtime.Serialization;

namespace CarPark.DB;

[Serializable]
[EdmEntityType(NamespaceName = "CarPark.DB", Name = "APS_CashBoxType")]
[DataContract(IsReference = true)]
public class APS_CashBoxType : EntityObject
{
	private int _CashBoxTypeID;

	private string _CashBoxNameCN;

	private string _CashBoxNamePT;

	private bool _IsAcceprot;

	private bool _IsBanknote;

	private int _AcceptorQty;

	private int _AcceptWarningQty;

	private bool _DetectStatus;

	private bool _IsDeleted;

	[EdmScalarProperty(EntityKeyProperty = true, IsNullable = false)]
	[DataMember]
	public int CashBoxTypeID
	{
		get
		{
			return _CashBoxTypeID;
		}
		set
		{
			if (_CashBoxTypeID != value)
			{
				ReportPropertyChanging("CashBoxTypeID");
				_CashBoxTypeID = StructuralObject.SetValidValue(value);
				ReportPropertyChanged("CashBoxTypeID");
			}
		}
	}

	[DataMember]
	[EdmScalarProperty(EntityKeyProperty = false, IsNullable = false)]
	public string CashBoxNameCN
	{
		get
		{
			return _CashBoxNameCN;
		}
		set
		{
			ReportPropertyChanging("CashBoxNameCN");
			_CashBoxNameCN = StructuralObject.SetValidValue(value, isNullable: false);
			ReportPropertyChanged("CashBoxNameCN");
		}
	}

	[DataMember]
	[EdmScalarProperty(EntityKeyProperty = false, IsNullable = false)]
	public string CashBoxNamePT
	{
		get
		{
			return _CashBoxNamePT;
		}
		set
		{
			ReportPropertyChanging("CashBoxNamePT");
			_CashBoxNamePT = StructuralObject.SetValidValue(value, isNullable: false);
			ReportPropertyChanged("CashBoxNamePT");
		}
	}

	[EdmScalarProperty(EntityKeyProperty = false, IsNullable = false)]
	[DataMember]
	public bool IsAcceprot
	{
		get
		{
			return _IsAcceprot;
		}
		set
		{
			ReportPropertyChanging("IsAcceprot");
			_IsAcceprot = StructuralObject.SetValidValue(value);
			ReportPropertyChanged("IsAcceprot");
		}
	}

	[DataMember]
	[EdmScalarProperty(EntityKeyProperty = false, IsNullable = false)]
	public bool IsBanknote
	{
		get
		{
			return _IsBanknote;
		}
		set
		{
			ReportPropertyChanging("IsBanknote");
			_IsBanknote = StructuralObject.SetValidValue(value);
			ReportPropertyChanged("IsBanknote");
		}
	}

	[EdmScalarProperty(EntityKeyProperty = false, IsNullable = false)]
	[DataMember]
	public int AcceptorQty
	{
		get
		{
			return _AcceptorQty;
		}
		set
		{
			ReportPropertyChanging("AcceptorQty");
			_AcceptorQty = StructuralObject.SetValidValue(value);
			ReportPropertyChanged("AcceptorQty");
		}
	}

	[DataMember]
	[EdmScalarProperty(EntityKeyProperty = false, IsNullable = false)]
	public int AcceptWarningQty
	{
		get
		{
			return _AcceptWarningQty;
		}
		set
		{
			ReportPropertyChanging("AcceptWarningQty");
			_AcceptWarningQty = StructuralObject.SetValidValue(value);
			ReportPropertyChanged("AcceptWarningQty");
		}
	}

	[EdmScalarProperty(EntityKeyProperty = false, IsNullable = false)]
	[DataMember]
	public bool DetectStatus
	{
		get
		{
			return _DetectStatus;
		}
		set
		{
			ReportPropertyChanging("DetectStatus");
			_DetectStatus = StructuralObject.SetValidValue(value);
			ReportPropertyChanged("DetectStatus");
		}
	}

	[DataMember]
	[EdmScalarProperty(EntityKeyProperty = false, IsNullable = false)]
	public bool IsDeleted
	{
		get
		{
			return _IsDeleted;
		}
		set
		{
			ReportPropertyChanging("IsDeleted");
			_IsDeleted = StructuralObject.SetValidValue(value);
			ReportPropertyChanged("IsDeleted");
		}
	}

	public static APS_CashBoxType CreateAPS_CashBoxType(int cashBoxTypeID, string cashBoxNameCN, string cashBoxNamePT, bool isAcceprot, bool isBanknote, int acceptorQty, int acceptWarningQty, bool detectStatus, bool isDeleted)
	{
		APS_CashBoxType aPS_CashBoxType = new APS_CashBoxType();
		aPS_CashBoxType.CashBoxTypeID = cashBoxTypeID;
		aPS_CashBoxType.CashBoxNameCN = cashBoxNameCN;
		aPS_CashBoxType.CashBoxNamePT = cashBoxNamePT;
		aPS_CashBoxType.IsAcceprot = isAcceprot;
		aPS_CashBoxType.IsBanknote = isBanknote;
		aPS_CashBoxType.AcceptorQty = acceptorQty;
		aPS_CashBoxType.AcceptWarningQty = acceptWarningQty;
		aPS_CashBoxType.DetectStatus = detectStatus;
		aPS_CashBoxType.IsDeleted = isDeleted;
		return aPS_CashBoxType;
	}
}
