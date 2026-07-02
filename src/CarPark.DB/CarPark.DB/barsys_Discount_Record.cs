using System;
using System.Data.Objects.DataClasses;
using System.Runtime.Serialization;

namespace CarPark.DB;

[Serializable]
[EdmEntityType(NamespaceName = "CarPark.DB", Name = "barsys_Discount_Record")]
[DataContract(IsReference = true)]
public class barsys_Discount_Record : EntityObject
{
	private int _DiscountUsedID;

	private int? _DiscountID;

	private string _TicketNo;

	private int? _TimeChargeID;

	private DateTime? _UseTime;

	[DataMember]
	[EdmScalarProperty(EntityKeyProperty = true, IsNullable = false)]
	public int DiscountUsedID
	{
		get
		{
			return _DiscountUsedID;
		}
		set
		{
			if (_DiscountUsedID != value)
			{
				ReportPropertyChanging("DiscountUsedID");
				_DiscountUsedID = StructuralObject.SetValidValue(value);
				ReportPropertyChanged("DiscountUsedID");
			}
		}
	}

	[EdmScalarProperty(EntityKeyProperty = false, IsNullable = true)]
	[DataMember]
	public int? DiscountID
	{
		get
		{
			return _DiscountID;
		}
		set
		{
			ReportPropertyChanging("DiscountID");
			_DiscountID = StructuralObject.SetValidValue(value);
			ReportPropertyChanged("DiscountID");
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
	public int? TimeChargeID
	{
		get
		{
			return _TimeChargeID;
		}
		set
		{
			ReportPropertyChanging("TimeChargeID");
			_TimeChargeID = StructuralObject.SetValidValue(value);
			ReportPropertyChanged("TimeChargeID");
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

	public static barsys_Discount_Record Createbarsys_Discount_Record(int discountUsedID)
	{
		barsys_Discount_Record barsys_Discount_Record2 = new barsys_Discount_Record();
		barsys_Discount_Record2.DiscountUsedID = discountUsedID;
		return barsys_Discount_Record2;
	}
}
