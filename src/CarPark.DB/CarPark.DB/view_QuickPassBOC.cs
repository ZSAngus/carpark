using System;
using System.Data.Objects.DataClasses;
using System.Runtime.Serialization;

namespace CarPark.DB;

[Serializable]
[DataContract(IsReference = true)]
[EdmEntityType(NamespaceName = "CarPark.DB", Name = "view_QuickPassBOC")]
public class view_QuickPassBOC : EntityObject
{
	private int _TransactionID;

	private string _EncryptedCardNumber;

	private string _BillDate;

	private string _BillTime;

	private decimal _CardBillAmount;

	private string _BillArea;

	private string _DeviceCode;

	private string _TxnNo;

	private int _ShiftID;

	private string _CardNumber;

	private bool _ISUploaded;

	private DateTime _TransactionTime;

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

	[DataMember]
	[EdmScalarProperty(EntityKeyProperty = true, IsNullable = false)]
	public decimal CardBillAmount
	{
		get
		{
			return _CardBillAmount;
		}
		set
		{
			if (_CardBillAmount != value)
			{
				ReportPropertyChanging("CardBillAmount");
				_CardBillAmount = StructuralObject.SetValidValue(value);
				ReportPropertyChanged("CardBillAmount");
			}
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

	[EdmScalarProperty(EntityKeyProperty = true, IsNullable = false)]
	[DataMember]
	public int ShiftID
	{
		get
		{
			return _ShiftID;
		}
		set
		{
			if (_ShiftID != value)
			{
				ReportPropertyChanging("ShiftID");
				_ShiftID = StructuralObject.SetValidValue(value);
				ReportPropertyChanged("ShiftID");
			}
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

	[EdmScalarProperty(EntityKeyProperty = true, IsNullable = false)]
	[DataMember]
	public bool ISUploaded
	{
		get
		{
			return _ISUploaded;
		}
		set
		{
			if (_ISUploaded != value)
			{
				ReportPropertyChanging("ISUploaded");
				_ISUploaded = StructuralObject.SetValidValue(value);
				ReportPropertyChanged("ISUploaded");
			}
		}
	}

	[EdmScalarProperty(EntityKeyProperty = true, IsNullable = false)]
	[DataMember]
	public DateTime TransactionTime
	{
		get
		{
			return _TransactionTime;
		}
		set
		{
			if (_TransactionTime != value)
			{
				ReportPropertyChanging("TransactionTime");
				_TransactionTime = StructuralObject.SetValidValue(value);
				ReportPropertyChanged("TransactionTime");
			}
		}
	}

	public static view_QuickPassBOC Createview_QuickPassBOC(int transactionID, decimal cardBillAmount, int shiftID, bool iSUploaded, DateTime transactionTime)
	{
		view_QuickPassBOC view_QuickPassBOC2 = new view_QuickPassBOC();
		view_QuickPassBOC2.TransactionID = transactionID;
		view_QuickPassBOC2.CardBillAmount = cardBillAmount;
		view_QuickPassBOC2.ShiftID = shiftID;
		view_QuickPassBOC2.ISUploaded = iSUploaded;
		view_QuickPassBOC2.TransactionTime = transactionTime;
		return view_QuickPassBOC2;
	}
}
