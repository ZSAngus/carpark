using System;
using System.Data.Objects.DataClasses;
using System.Runtime.Serialization;

namespace CarPark.DB;

[Serializable]
[EdmEntityType(NamespaceName = "CarPark.DB", Name = "ChargeItemDetail")]
[DataContract(IsReference = true)]
public class ChargeItemDetail : EntityObject
{
	private int _ChargeItemDetailID;

	private int _ChargeItemID;

	private int _ChargeRecordID;

	private decimal _TotalCharge;

	private string _Remark;

	private bool _IsDelete;

	[DataMember]
	[EdmScalarProperty(EntityKeyProperty = true, IsNullable = false)]
	public int ChargeItemDetailID
	{
		get
		{
			return _ChargeItemDetailID;
		}
		set
		{
			if (_ChargeItemDetailID != value)
			{
				ReportPropertyChanging("ChargeItemDetailID");
				_ChargeItemDetailID = StructuralObject.SetValidValue(value);
				ReportPropertyChanged("ChargeItemDetailID");
			}
		}
	}

	[EdmScalarProperty(EntityKeyProperty = false, IsNullable = false)]
	[DataMember]
	public int ChargeItemID
	{
		get
		{
			return _ChargeItemID;
		}
		set
		{
			ReportPropertyChanging("ChargeItemID");
			_ChargeItemID = StructuralObject.SetValidValue(value);
			ReportPropertyChanged("ChargeItemID");
		}
	}

	[DataMember]
	[EdmScalarProperty(EntityKeyProperty = false, IsNullable = false)]
	public int ChargeRecordID
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
	[EdmScalarProperty(EntityKeyProperty = false, IsNullable = false)]
	public decimal TotalCharge
	{
		get
		{
			return _TotalCharge;
		}
		set
		{
			ReportPropertyChanging("TotalCharge");
			_TotalCharge = StructuralObject.SetValidValue(value);
			ReportPropertyChanged("TotalCharge");
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

	[EdmScalarProperty(EntityKeyProperty = false, IsNullable = false)]
	[DataMember]
	public bool IsDelete
	{
		get
		{
			return _IsDelete;
		}
		set
		{
			ReportPropertyChanging("IsDelete");
			_IsDelete = StructuralObject.SetValidValue(value);
			ReportPropertyChanged("IsDelete");
		}
	}

	public static ChargeItemDetail CreateChargeItemDetail(int chargeItemDetailID, int chargeItemID, int chargeRecordID, decimal totalCharge, bool isDelete)
	{
		ChargeItemDetail chargeItemDetail = new ChargeItemDetail();
		chargeItemDetail.ChargeItemDetailID = chargeItemDetailID;
		chargeItemDetail.ChargeItemID = chargeItemID;
		chargeItemDetail.ChargeRecordID = chargeRecordID;
		chargeItemDetail.TotalCharge = totalCharge;
		chargeItemDetail.IsDelete = isDelete;
		return chargeItemDetail;
	}
}
