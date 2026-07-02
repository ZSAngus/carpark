using System;
using System.Data.Objects.DataClasses;
using System.Runtime.Serialization;

namespace CarPark.DB;

[Serializable]
[EdmEntityType(NamespaceName = "CarPark.DB", Name = "barsys_Discount")]
[DataContract(IsReference = true)]
public class barsys_Discount : EntityObject
{
	private int? _DiscountType;

	private string _CustomerName;

	private string _Licenseplate;

	private int _DiscountID;

	private int _Discount_BatchID;

	private int _CustomFreeTypeID;

	private int _TenatID;

	private string _DiscountBanks;

	private DateTime _StartTime;

	private DateTime _EndTime;

	private int _ReductionTime;

	private DateTime? _UseTime;

	private bool? _State;

	private bool? _PrintFlag;

	private int? _PrintNumber;

	private string _PrintStaff;

	private DateTime? _PrintTime;

	private DateTime? _CancellationTime;

	private string _CancellationStaff;

	private string _Remark;

	private string _carparkRemark;

	private bool? _PrintState;

	private string _TicketNo;

	private decimal? _TotalTicketAmt;

	private int? _CompHr;

	[EdmScalarProperty(EntityKeyProperty = false, IsNullable = true)]
	[DataMember]
	public int? DiscountType
	{
		get
		{
			return _DiscountType;
		}
		set
		{
			ReportPropertyChanging("DiscountType");
			_DiscountType = StructuralObject.SetValidValue(value);
			ReportPropertyChanged("DiscountType");
		}
	}

	[EdmScalarProperty(EntityKeyProperty = false, IsNullable = true)]
	[DataMember]
	public string CustomerName
	{
		get
		{
			return _CustomerName;
		}
		set
		{
			ReportPropertyChanging("CustomerName");
			_CustomerName = StructuralObject.SetValidValue(value, isNullable: true);
			ReportPropertyChanged("CustomerName");
		}
	}

	[DataMember]
	[EdmScalarProperty(EntityKeyProperty = false, IsNullable = true)]
	public string Licenseplate
	{
		get
		{
			return _Licenseplate;
		}
		set
		{
			ReportPropertyChanging("Licenseplate");
			_Licenseplate = StructuralObject.SetValidValue(value, isNullable: true);
			ReportPropertyChanged("Licenseplate");
		}
	}

	[EdmScalarProperty(EntityKeyProperty = true, IsNullable = false)]
	[DataMember]
	public int DiscountID
	{
		get
		{
			return _DiscountID;
		}
		set
		{
			if (_DiscountID != value)
			{
				ReportPropertyChanging("DiscountID");
				_DiscountID = StructuralObject.SetValidValue(value);
				ReportPropertyChanged("DiscountID");
			}
		}
	}

	[DataMember]
	[EdmScalarProperty(EntityKeyProperty = false, IsNullable = false)]
	public int Discount_BatchID
	{
		get
		{
			return _Discount_BatchID;
		}
		set
		{
			ReportPropertyChanging("Discount_BatchID");
			_Discount_BatchID = StructuralObject.SetValidValue(value);
			ReportPropertyChanged("Discount_BatchID");
		}
	}

	[EdmScalarProperty(EntityKeyProperty = false, IsNullable = false)]
	[DataMember]
	public int CustomFreeTypeID
	{
		get
		{
			return _CustomFreeTypeID;
		}
		set
		{
			ReportPropertyChanging("CustomFreeTypeID");
			_CustomFreeTypeID = StructuralObject.SetValidValue(value);
			ReportPropertyChanged("CustomFreeTypeID");
		}
	}

	[DataMember]
	[EdmScalarProperty(EntityKeyProperty = false, IsNullable = false)]
	public int TenatID
	{
		get
		{
			return _TenatID;
		}
		set
		{
			ReportPropertyChanging("TenatID");
			_TenatID = StructuralObject.SetValidValue(value);
			ReportPropertyChanged("TenatID");
		}
	}

	[EdmScalarProperty(EntityKeyProperty = false, IsNullable = false)]
	[DataMember]
	public string DiscountBanks
	{
		get
		{
			return _DiscountBanks;
		}
		set
		{
			ReportPropertyChanging("DiscountBanks");
			_DiscountBanks = StructuralObject.SetValidValue(value, isNullable: false);
			ReportPropertyChanged("DiscountBanks");
		}
	}

	[DataMember]
	[EdmScalarProperty(EntityKeyProperty = false, IsNullable = false)]
	public DateTime StartTime
	{
		get
		{
			return _StartTime;
		}
		set
		{
			ReportPropertyChanging("StartTime");
			_StartTime = StructuralObject.SetValidValue(value);
			ReportPropertyChanged("StartTime");
		}
	}

	[EdmScalarProperty(EntityKeyProperty = false, IsNullable = false)]
	[DataMember]
	public DateTime EndTime
	{
		get
		{
			return _EndTime;
		}
		set
		{
			ReportPropertyChanging("EndTime");
			_EndTime = StructuralObject.SetValidValue(value);
			ReportPropertyChanged("EndTime");
		}
	}

	[DataMember]
	[EdmScalarProperty(EntityKeyProperty = false, IsNullable = false)]
	public int ReductionTime
	{
		get
		{
			return _ReductionTime;
		}
		set
		{
			ReportPropertyChanging("ReductionTime");
			_ReductionTime = StructuralObject.SetValidValue(value);
			ReportPropertyChanged("ReductionTime");
		}
	}

	[EdmScalarProperty(EntityKeyProperty = false, IsNullable = true)]
	[DataMember]
	public DateTime? UseTime
	{
		get
		{
			return _UseTime;
		}
		set
		{
			ReportPropertyChanging("UseTime");
			_UseTime = StructuralObject.SetValidValue(value);
			ReportPropertyChanged("UseTime");
		}
	}

	[EdmScalarProperty(EntityKeyProperty = false, IsNullable = true)]
	[DataMember]
	public bool? State
	{
		get
		{
			return _State;
		}
		set
		{
			ReportPropertyChanging("State");
			_State = StructuralObject.SetValidValue(value);
			ReportPropertyChanged("State");
		}
	}

	[DataMember]
	[EdmScalarProperty(EntityKeyProperty = false, IsNullable = true)]
	public bool? PrintFlag
	{
		get
		{
			return _PrintFlag;
		}
		set
		{
			ReportPropertyChanging("PrintFlag");
			_PrintFlag = StructuralObject.SetValidValue(value);
			ReportPropertyChanged("PrintFlag");
		}
	}

	[EdmScalarProperty(EntityKeyProperty = false, IsNullable = true)]
	[DataMember]
	public int? PrintNumber
	{
		get
		{
			return _PrintNumber;
		}
		set
		{
			ReportPropertyChanging("PrintNumber");
			_PrintNumber = StructuralObject.SetValidValue(value);
			ReportPropertyChanged("PrintNumber");
		}
	}

	[DataMember]
	[EdmScalarProperty(EntityKeyProperty = false, IsNullable = true)]
	public string PrintStaff
	{
		get
		{
			return _PrintStaff;
		}
		set
		{
			ReportPropertyChanging("PrintStaff");
			_PrintStaff = StructuralObject.SetValidValue(value, isNullable: true);
			ReportPropertyChanged("PrintStaff");
		}
	}

	[EdmScalarProperty(EntityKeyProperty = false, IsNullable = true)]
	[DataMember]
	public DateTime? PrintTime
	{
		get
		{
			return _PrintTime;
		}
		set
		{
			ReportPropertyChanging("PrintTime");
			_PrintTime = StructuralObject.SetValidValue(value);
			ReportPropertyChanged("PrintTime");
		}
	}

	[DataMember]
	[EdmScalarProperty(EntityKeyProperty = false, IsNullable = true)]
	public DateTime? CancellationTime
	{
		get
		{
			return _CancellationTime;
		}
		set
		{
			ReportPropertyChanging("CancellationTime");
			_CancellationTime = StructuralObject.SetValidValue(value);
			ReportPropertyChanged("CancellationTime");
		}
	}

	[EdmScalarProperty(EntityKeyProperty = false, IsNullable = true)]
	[DataMember]
	public string CancellationStaff
	{
		get
		{
			return _CancellationStaff;
		}
		set
		{
			ReportPropertyChanging("CancellationStaff");
			_CancellationStaff = StructuralObject.SetValidValue(value, isNullable: true);
			ReportPropertyChanged("CancellationStaff");
		}
	}

	[DataMember]
	[EdmScalarProperty(EntityKeyProperty = false, IsNullable = true)]
	public string Remark
	{
		get
		{
			return _Remark;
		}
		set
		{
			ReportPropertyChanging("Remark");
			_Remark = StructuralObject.SetValidValue(value, isNullable: true);
			ReportPropertyChanged("Remark");
		}
	}

	[EdmScalarProperty(EntityKeyProperty = false, IsNullable = true)]
	[DataMember]
	public string carparkRemark
	{
		get
		{
			return _carparkRemark;
		}
		set
		{
			ReportPropertyChanging("carparkRemark");
			_carparkRemark = StructuralObject.SetValidValue(value, isNullable: true);
			ReportPropertyChanged("carparkRemark");
		}
	}

	[EdmScalarProperty(EntityKeyProperty = false, IsNullable = true)]
	[DataMember]
	public bool? PrintState
	{
		get
		{
			return _PrintState;
		}
		set
		{
			ReportPropertyChanging("PrintState");
			_PrintState = StructuralObject.SetValidValue(value);
			ReportPropertyChanged("PrintState");
		}
	}

	[EdmScalarProperty(EntityKeyProperty = false, IsNullable = true)]
	[DataMember]
	public string TicketNo
	{
		get
		{
			return _TicketNo;
		}
		set
		{
			ReportPropertyChanging("TicketNo");
			_TicketNo = StructuralObject.SetValidValue(value, isNullable: true);
			ReportPropertyChanged("TicketNo");
		}
	}

	[DataMember]
	[EdmScalarProperty(EntityKeyProperty = false, IsNullable = true)]
	public decimal? TotalTicketAmt
	{
		get
		{
			return _TotalTicketAmt;
		}
		set
		{
			ReportPropertyChanging("TotalTicketAmt");
			_TotalTicketAmt = StructuralObject.SetValidValue(value);
			ReportPropertyChanged("TotalTicketAmt");
		}
	}

	[DataMember]
	[EdmScalarProperty(EntityKeyProperty = false, IsNullable = true)]
	public int? CompHr
	{
		get
		{
			return _CompHr;
		}
		set
		{
			ReportPropertyChanging("CompHr");
			_CompHr = StructuralObject.SetValidValue(value);
			ReportPropertyChanged("CompHr");
		}
	}

	public static barsys_Discount Createbarsys_Discount(int discountID, int discount_BatchID, int customFreeTypeID, int tenatID, string discountBanks, DateTime startTime, DateTime endTime, int reductionTime)
	{
		barsys_Discount barsys_Discount2 = new barsys_Discount();
		barsys_Discount2.DiscountID = discountID;
		barsys_Discount2.Discount_BatchID = discount_BatchID;
		barsys_Discount2.CustomFreeTypeID = customFreeTypeID;
		barsys_Discount2.TenatID = tenatID;
		barsys_Discount2.DiscountBanks = discountBanks;
		barsys_Discount2.StartTime = startTime;
		barsys_Discount2.EndTime = endTime;
		barsys_Discount2.ReductionTime = reductionTime;
		return barsys_Discount2;
	}
}
