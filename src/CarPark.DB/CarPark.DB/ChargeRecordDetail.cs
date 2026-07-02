using System;
using System.Data.Objects.DataClasses;
using System.Runtime.Serialization;

namespace CarPark.DB;

[Serializable]
[DataContract(IsReference = true)]
[EdmEntityType(NamespaceName = "CarPark.DB", Name = "ChargeRecordDetail")]
public class ChargeRecordDetail : EntityObject
{
	private int _ChargeRecordDetailID;

	private decimal _C1H;

	private decimal _C2H;

	private decimal _C3H;

	private decimal _C4H;

	private decimal _C5H;

	private decimal _C6H;

	private decimal _C7H;

	private decimal _C8H;

	private decimal _C9H;

	private decimal _C10H;

	private decimal _C11H;

	private decimal _C12H;

	private decimal _C13H;

	private decimal _C14H;

	private decimal _C15H;

	private decimal _C16H;

	private decimal _C17H;

	private decimal _C18H;

	private decimal _C19H;

	private decimal _C20H;

	private decimal _C21H;

	private decimal _C22H;

	private decimal _C23H;

	private decimal _C0H;

	private int _ChargeRecordID;

	[EdmScalarProperty(EntityKeyProperty = true, IsNullable = false)]
	[DataMember]
	public int ChargeRecordDetailID
	{
		get
		{
			return _ChargeRecordDetailID;
		}
		set
		{
			if (_ChargeRecordDetailID != value)
			{
				ReportPropertyChanging("ChargeRecordDetailID");
				_ChargeRecordDetailID = StructuralObject.SetValidValue(value);
				ReportPropertyChanged("ChargeRecordDetailID");
			}
		}
	}

	[DataMember]
	[EdmScalarProperty(EntityKeyProperty = true, IsNullable = false)]
	public decimal C1H
	{
		get
		{
			return _C1H;
		}
		set
		{
			if (_C1H != value)
			{
				ReportPropertyChanging("C1H");
				_C1H = StructuralObject.SetValidValue(value);
				ReportPropertyChanged("C1H");
			}
		}
	}

	[EdmScalarProperty(EntityKeyProperty = true, IsNullable = false)]
	[DataMember]
	public decimal C2H
	{
		get
		{
			return _C2H;
		}
		set
		{
			if (_C2H != value)
			{
				ReportPropertyChanging("C2H");
				_C2H = StructuralObject.SetValidValue(value);
				ReportPropertyChanged("C2H");
			}
		}
	}

	[DataMember]
	[EdmScalarProperty(EntityKeyProperty = true, IsNullable = false)]
	public decimal C3H
	{
		get
		{
			return _C3H;
		}
		set
		{
			if (_C3H != value)
			{
				ReportPropertyChanging("C3H");
				_C3H = StructuralObject.SetValidValue(value);
				ReportPropertyChanged("C3H");
			}
		}
	}

	[DataMember]
	[EdmScalarProperty(EntityKeyProperty = true, IsNullable = false)]
	public decimal C4H
	{
		get
		{
			return _C4H;
		}
		set
		{
			if (_C4H != value)
			{
				ReportPropertyChanging("C4H");
				_C4H = StructuralObject.SetValidValue(value);
				ReportPropertyChanged("C4H");
			}
		}
	}

	[DataMember]
	[EdmScalarProperty(EntityKeyProperty = true, IsNullable = false)]
	public decimal C5H
	{
		get
		{
			return _C5H;
		}
		set
		{
			if (_C5H != value)
			{
				ReportPropertyChanging("C5H");
				_C5H = StructuralObject.SetValidValue(value);
				ReportPropertyChanged("C5H");
			}
		}
	}

	[EdmScalarProperty(EntityKeyProperty = true, IsNullable = false)]
	[DataMember]
	public decimal C6H
	{
		get
		{
			return _C6H;
		}
		set
		{
			if (_C6H != value)
			{
				ReportPropertyChanging("C6H");
				_C6H = StructuralObject.SetValidValue(value);
				ReportPropertyChanged("C6H");
			}
		}
	}

	[DataMember]
	[EdmScalarProperty(EntityKeyProperty = true, IsNullable = false)]
	public decimal C7H
	{
		get
		{
			return _C7H;
		}
		set
		{
			if (_C7H != value)
			{
				ReportPropertyChanging("C7H");
				_C7H = StructuralObject.SetValidValue(value);
				ReportPropertyChanged("C7H");
			}
		}
	}

	[EdmScalarProperty(EntityKeyProperty = true, IsNullable = false)]
	[DataMember]
	public decimal C8H
	{
		get
		{
			return _C8H;
		}
		set
		{
			if (_C8H != value)
			{
				ReportPropertyChanging("C8H");
				_C8H = StructuralObject.SetValidValue(value);
				ReportPropertyChanged("C8H");
			}
		}
	}

	[DataMember]
	[EdmScalarProperty(EntityKeyProperty = true, IsNullable = false)]
	public decimal C9H
	{
		get
		{
			return _C9H;
		}
		set
		{
			if (_C9H != value)
			{
				ReportPropertyChanging("C9H");
				_C9H = StructuralObject.SetValidValue(value);
				ReportPropertyChanged("C9H");
			}
		}
	}

	[EdmScalarProperty(EntityKeyProperty = true, IsNullable = false)]
	[DataMember]
	public decimal C10H
	{
		get
		{
			return _C10H;
		}
		set
		{
			if (_C10H != value)
			{
				ReportPropertyChanging("C10H");
				_C10H = StructuralObject.SetValidValue(value);
				ReportPropertyChanged("C10H");
			}
		}
	}

	[EdmScalarProperty(EntityKeyProperty = true, IsNullable = false)]
	[DataMember]
	public decimal C11H
	{
		get
		{
			return _C11H;
		}
		set
		{
			if (_C11H != value)
			{
				ReportPropertyChanging("C11H");
				_C11H = StructuralObject.SetValidValue(value);
				ReportPropertyChanged("C11H");
			}
		}
	}

	[DataMember]
	[EdmScalarProperty(EntityKeyProperty = true, IsNullable = false)]
	public decimal C12H
	{
		get
		{
			return _C12H;
		}
		set
		{
			if (_C12H != value)
			{
				ReportPropertyChanging("C12H");
				_C12H = StructuralObject.SetValidValue(value);
				ReportPropertyChanged("C12H");
			}
		}
	}

	[DataMember]
	[EdmScalarProperty(EntityKeyProperty = true, IsNullable = false)]
	public decimal C13H
	{
		get
		{
			return _C13H;
		}
		set
		{
			if (_C13H != value)
			{
				ReportPropertyChanging("C13H");
				_C13H = StructuralObject.SetValidValue(value);
				ReportPropertyChanged("C13H");
			}
		}
	}

	[EdmScalarProperty(EntityKeyProperty = true, IsNullable = false)]
	[DataMember]
	public decimal C14H
	{
		get
		{
			return _C14H;
		}
		set
		{
			if (_C14H != value)
			{
				ReportPropertyChanging("C14H");
				_C14H = StructuralObject.SetValidValue(value);
				ReportPropertyChanged("C14H");
			}
		}
	}

	[DataMember]
	[EdmScalarProperty(EntityKeyProperty = true, IsNullable = false)]
	public decimal C15H
	{
		get
		{
			return _C15H;
		}
		set
		{
			if (_C15H != value)
			{
				ReportPropertyChanging("C15H");
				_C15H = StructuralObject.SetValidValue(value);
				ReportPropertyChanged("C15H");
			}
		}
	}

	[EdmScalarProperty(EntityKeyProperty = true, IsNullable = false)]
	[DataMember]
	public decimal C16H
	{
		get
		{
			return _C16H;
		}
		set
		{
			if (_C16H != value)
			{
				ReportPropertyChanging("C16H");
				_C16H = StructuralObject.SetValidValue(value);
				ReportPropertyChanged("C16H");
			}
		}
	}

	[DataMember]
	[EdmScalarProperty(EntityKeyProperty = true, IsNullable = false)]
	public decimal C17H
	{
		get
		{
			return _C17H;
		}
		set
		{
			if (_C17H != value)
			{
				ReportPropertyChanging("C17H");
				_C17H = StructuralObject.SetValidValue(value);
				ReportPropertyChanged("C17H");
			}
		}
	}

	[EdmScalarProperty(EntityKeyProperty = true, IsNullable = false)]
	[DataMember]
	public decimal C18H
	{
		get
		{
			return _C18H;
		}
		set
		{
			if (_C18H != value)
			{
				ReportPropertyChanging("C18H");
				_C18H = StructuralObject.SetValidValue(value);
				ReportPropertyChanged("C18H");
			}
		}
	}

	[EdmScalarProperty(EntityKeyProperty = true, IsNullable = false)]
	[DataMember]
	public decimal C19H
	{
		get
		{
			return _C19H;
		}
		set
		{
			if (_C19H != value)
			{
				ReportPropertyChanging("C19H");
				_C19H = StructuralObject.SetValidValue(value);
				ReportPropertyChanged("C19H");
			}
		}
	}

	[EdmScalarProperty(EntityKeyProperty = true, IsNullable = false)]
	[DataMember]
	public decimal C20H
	{
		get
		{
			return _C20H;
		}
		set
		{
			if (_C20H != value)
			{
				ReportPropertyChanging("C20H");
				_C20H = StructuralObject.SetValidValue(value);
				ReportPropertyChanged("C20H");
			}
		}
	}

	[DataMember]
	[EdmScalarProperty(EntityKeyProperty = true, IsNullable = false)]
	public decimal C21H
	{
		get
		{
			return _C21H;
		}
		set
		{
			if (_C21H != value)
			{
				ReportPropertyChanging("C21H");
				_C21H = StructuralObject.SetValidValue(value);
				ReportPropertyChanged("C21H");
			}
		}
	}

	[DataMember]
	[EdmScalarProperty(EntityKeyProperty = true, IsNullable = false)]
	public decimal C22H
	{
		get
		{
			return _C22H;
		}
		set
		{
			if (_C22H != value)
			{
				ReportPropertyChanging("C22H");
				_C22H = StructuralObject.SetValidValue(value);
				ReportPropertyChanged("C22H");
			}
		}
	}

	[EdmScalarProperty(EntityKeyProperty = true, IsNullable = false)]
	[DataMember]
	public decimal C23H
	{
		get
		{
			return _C23H;
		}
		set
		{
			if (_C23H != value)
			{
				ReportPropertyChanging("C23H");
				_C23H = StructuralObject.SetValidValue(value);
				ReportPropertyChanged("C23H");
			}
		}
	}

	[DataMember]
	[EdmScalarProperty(EntityKeyProperty = true, IsNullable = false)]
	public decimal C0H
	{
		get
		{
			return _C0H;
		}
		set
		{
			if (_C0H != value)
			{
				ReportPropertyChanging("C0H");
				_C0H = StructuralObject.SetValidValue(value);
				ReportPropertyChanged("C0H");
			}
		}
	}

	[EdmScalarProperty(EntityKeyProperty = true, IsNullable = false)]
	[DataMember]
	public int ChargeRecordID
	{
		get
		{
			return _ChargeRecordID;
		}
		set
		{
			if (_ChargeRecordID != value)
			{
				ReportPropertyChanging("ChargeRecordID");
				_ChargeRecordID = StructuralObject.SetValidValue(value);
				ReportPropertyChanged("ChargeRecordID");
			}
		}
	}

	public static ChargeRecordDetail CreateChargeRecordDetail(int chargeRecordDetailID, decimal c1H, decimal c2H, decimal c3H, decimal c4H, decimal c5H, decimal c6H, decimal c7H, decimal c8H, decimal c9H, decimal c10H, decimal c11H, decimal c12H, decimal c13H, decimal c14H, decimal c15H, decimal c16H, decimal c17H, decimal c18H, decimal c19H, decimal c20H, decimal c21H, decimal c22H, decimal c23H, decimal c0H, int chargeRecordID)
	{
		ChargeRecordDetail chargeRecordDetail = new ChargeRecordDetail();
		chargeRecordDetail.ChargeRecordDetailID = chargeRecordDetailID;
		chargeRecordDetail.C1H = c1H;
		chargeRecordDetail.C2H = c2H;
		chargeRecordDetail.C3H = c3H;
		chargeRecordDetail.C4H = c4H;
		chargeRecordDetail.C5H = c5H;
		chargeRecordDetail.C6H = c6H;
		chargeRecordDetail.C7H = c7H;
		chargeRecordDetail.C8H = c8H;
		chargeRecordDetail.C9H = c9H;
		chargeRecordDetail.C10H = c10H;
		chargeRecordDetail.C11H = c11H;
		chargeRecordDetail.C12H = c12H;
		chargeRecordDetail.C13H = c13H;
		chargeRecordDetail.C14H = c14H;
		chargeRecordDetail.C15H = c15H;
		chargeRecordDetail.C16H = c16H;
		chargeRecordDetail.C17H = c17H;
		chargeRecordDetail.C18H = c18H;
		chargeRecordDetail.C19H = c19H;
		chargeRecordDetail.C20H = c20H;
		chargeRecordDetail.C21H = c21H;
		chargeRecordDetail.C22H = c22H;
		chargeRecordDetail.C23H = c23H;
		chargeRecordDetail.C0H = c0H;
		chargeRecordDetail.ChargeRecordID = chargeRecordID;
		return chargeRecordDetail;
	}
}
