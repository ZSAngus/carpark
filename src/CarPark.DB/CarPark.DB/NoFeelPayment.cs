using System;
using System.Data.Objects.DataClasses;
using System.Runtime.Serialization;

namespace CarPark.DB;

[Serializable]
[EdmEntityType(NamespaceName = "CarPark.DB", Name = "NoFeelPayment")]
[DataContract(IsReference = true)]
public class NoFeelPayment : EntityObject
{
	private int _ID;

	private int? _TrancationID;

	private string _CustomerNo;

	private string _OrderNumber;

	private string _AddCode;

	private string _BindingTagFlag;

	private string _RFIDTID;

	private string _RFIDNO;

	private bool? _isUploadTrans;

	private bool? _isUploadCharge;

	private string _LicensePlate;

	private string _AuthCode;

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
	public int? TrancationID
	{
		get
		{
			return _TrancationID;
		}
		set
		{
			ReportPropertyChanging("TrancationID");
			_TrancationID = StructuralObject.SetValidValue(value);
			ReportPropertyChanged("TrancationID");
		}
	}

	[EdmScalarProperty(EntityKeyProperty = false, IsNullable = true)]
	[DataMember]
	public string CustomerNo
	{
		get
		{
			return _CustomerNo;
		}
		set
		{
			ReportPropertyChanging("CustomerNo");
			_CustomerNo = StructuralObject.SetValidValue(value, isNullable: true);
			ReportPropertyChanged("CustomerNo");
		}
	}

	[DataMember]
	[EdmScalarProperty(EntityKeyProperty = false, IsNullable = true)]
	public string OrderNumber
	{
		get
		{
			return _OrderNumber;
		}
		set
		{
			ReportPropertyChanging("OrderNumber");
			_OrderNumber = StructuralObject.SetValidValue(value, isNullable: true);
			ReportPropertyChanged("OrderNumber");
		}
	}

	[DataMember]
	[EdmScalarProperty(EntityKeyProperty = false, IsNullable = true)]
	public string AddCode
	{
		get
		{
			return _AddCode;
		}
		set
		{
			ReportPropertyChanging("AddCode");
			_AddCode = StructuralObject.SetValidValue(value, isNullable: true);
			ReportPropertyChanged("AddCode");
		}
	}

	[EdmScalarProperty(EntityKeyProperty = false, IsNullable = true)]
	[DataMember]
	public string BindingTagFlag
	{
		get
		{
			return _BindingTagFlag;
		}
		set
		{
			ReportPropertyChanging("BindingTagFlag");
			_BindingTagFlag = StructuralObject.SetValidValue(value, isNullable: true);
			ReportPropertyChanged("BindingTagFlag");
		}
	}

	[EdmScalarProperty(EntityKeyProperty = false, IsNullable = true)]
	[DataMember]
	public string RFIDTID
	{
		get
		{
			return _RFIDTID;
		}
		set
		{
			ReportPropertyChanging("RFIDTID");
			_RFIDTID = StructuralObject.SetValidValue(value, isNullable: true);
			ReportPropertyChanged("RFIDTID");
		}
	}

	[EdmScalarProperty(EntityKeyProperty = false, IsNullable = true)]
	[DataMember]
	public string RFIDNO
	{
		get
		{
			return _RFIDNO;
		}
		set
		{
			ReportPropertyChanging("RFIDNO");
			_RFIDNO = StructuralObject.SetValidValue(value, isNullable: true);
			ReportPropertyChanged("RFIDNO");
		}
	}

	[DataMember]
	[EdmScalarProperty(EntityKeyProperty = false, IsNullable = true)]
	public bool? isUploadTrans
	{
		get
		{
			return _isUploadTrans;
		}
		set
		{
			ReportPropertyChanging("isUploadTrans");
			_isUploadTrans = StructuralObject.SetValidValue(value);
			ReportPropertyChanged("isUploadTrans");
		}
	}

	[DataMember]
	[EdmScalarProperty(EntityKeyProperty = false, IsNullable = true)]
	public bool? isUploadCharge
	{
		get
		{
			return _isUploadCharge;
		}
		set
		{
			ReportPropertyChanging("isUploadCharge");
			_isUploadCharge = StructuralObject.SetValidValue(value);
			ReportPropertyChanged("isUploadCharge");
		}
	}

	[EdmScalarProperty(EntityKeyProperty = false, IsNullable = true)]
	[DataMember]
	public string LicensePlate
	{
		get
		{
			return _LicensePlate;
		}
		set
		{
			ReportPropertyChanging("LicensePlate");
			_LicensePlate = StructuralObject.SetValidValue(value, isNullable: true);
			ReportPropertyChanged("LicensePlate");
		}
	}

	[EdmScalarProperty(EntityKeyProperty = false, IsNullable = true)]
	[DataMember]
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

	public static NoFeelPayment CreateNoFeelPayment(int id)
	{
		NoFeelPayment noFeelPayment = new NoFeelPayment();
		noFeelPayment.ID = id;
		return noFeelPayment;
	}
}
