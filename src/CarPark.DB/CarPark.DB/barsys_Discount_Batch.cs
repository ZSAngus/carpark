using System;
using System.Data.Objects.DataClasses;
using System.Runtime.Serialization;

namespace CarPark.DB;

[Serializable]
[EdmEntityType(NamespaceName = "CarPark.DB", Name = "barsys_Discount_Batch")]
[DataContract(IsReference = true)]
public class barsys_Discount_Batch : EntityObject
{
	private int _DiscountID;

	private string _TenatID;

	private DateTime _StartTime;

	private DateTime _EndTime;

	private int _PrintAmount;

	private string _StartDiscount;

	private string _EndDiscount;

	private int _FreeTime;

	private string _IssStaff;

	private DateTime _IssTime;

	private string _Remark;

	private int? _DiscountType;

	[DataMember]
	[EdmScalarProperty(EntityKeyProperty = true, IsNullable = false)]
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
	public string TenatID
	{
		get
		{
			return _TenatID;
		}
		set
		{
			ReportPropertyChanging("TenatID");
			_TenatID = StructuralObject.SetValidValue(value, isNullable: false);
			ReportPropertyChanged("TenatID");
		}
	}

	[EdmScalarProperty(EntityKeyProperty = false, IsNullable = false)]
	[DataMember]
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

	[DataMember]
	[EdmScalarProperty(EntityKeyProperty = false, IsNullable = false)]
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
	public int PrintAmount
	{
		get
		{
			return _PrintAmount;
		}
		set
		{
			ReportPropertyChanging("PrintAmount");
			_PrintAmount = StructuralObject.SetValidValue(value);
			ReportPropertyChanged("PrintAmount");
		}
	}

	[EdmScalarProperty(EntityKeyProperty = false, IsNullable = false)]
	[DataMember]
	public string StartDiscount
	{
		get
		{
			return _StartDiscount;
		}
		set
		{
			ReportPropertyChanging("StartDiscount");
			_StartDiscount = StructuralObject.SetValidValue(value, isNullable: false);
			ReportPropertyChanged("StartDiscount");
		}
	}

	[DataMember]
	[EdmScalarProperty(EntityKeyProperty = false, IsNullable = false)]
	public string EndDiscount
	{
		get
		{
			return _EndDiscount;
		}
		set
		{
			ReportPropertyChanging("EndDiscount");
			_EndDiscount = StructuralObject.SetValidValue(value, isNullable: false);
			ReportPropertyChanged("EndDiscount");
		}
	}

	[DataMember]
	[EdmScalarProperty(EntityKeyProperty = false, IsNullable = false)]
	public int FreeTime
	{
		get
		{
			return _FreeTime;
		}
		set
		{
			ReportPropertyChanging("FreeTime");
			_FreeTime = StructuralObject.SetValidValue(value);
			ReportPropertyChanged("FreeTime");
		}
	}

	[DataMember]
	[EdmScalarProperty(EntityKeyProperty = false, IsNullable = false)]
	public string IssStaff
	{
		get
		{
			return _IssStaff;
		}
		set
		{
			ReportPropertyChanging("IssStaff");
			_IssStaff = StructuralObject.SetValidValue(value, isNullable: false);
			ReportPropertyChanged("IssStaff");
		}
	}

	[EdmScalarProperty(EntityKeyProperty = false, IsNullable = false)]
	[DataMember]
	public DateTime IssTime
	{
		get
		{
			return _IssTime;
		}
		set
		{
			ReportPropertyChanging("IssTime");
			_IssTime = StructuralObject.SetValidValue(value);
			ReportPropertyChanged("IssTime");
		}
	}

	[EdmScalarProperty(EntityKeyProperty = false, IsNullable = true)]
	[DataMember]
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

	[DataMember]
	[EdmScalarProperty(EntityKeyProperty = false, IsNullable = true)]
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

	public static barsys_Discount_Batch Createbarsys_Discount_Batch(int discountID, string tenatID, DateTime startTime, DateTime endTime, int printAmount, string startDiscount, string endDiscount, int freeTime, string issStaff, DateTime issTime)
	{
		barsys_Discount_Batch barsys_Discount_Batch2 = new barsys_Discount_Batch();
		barsys_Discount_Batch2.DiscountID = discountID;
		barsys_Discount_Batch2.TenatID = tenatID;
		barsys_Discount_Batch2.StartTime = startTime;
		barsys_Discount_Batch2.EndTime = endTime;
		barsys_Discount_Batch2.PrintAmount = printAmount;
		barsys_Discount_Batch2.StartDiscount = startDiscount;
		barsys_Discount_Batch2.EndDiscount = endDiscount;
		barsys_Discount_Batch2.FreeTime = freeTime;
		barsys_Discount_Batch2.IssStaff = issStaff;
		barsys_Discount_Batch2.IssTime = issTime;
		return barsys_Discount_Batch2;
	}
}
