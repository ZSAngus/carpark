using System;
using System.Data.Objects.DataClasses;
using System.Runtime.Serialization;
using SkyInno.Lang;

namespace CarPark.DB;

[Serializable]
[EdmEntityType(NamespaceName = "CarPark.DB", Name = "ChargeItem")]
[DataContract(IsReference = true)]
public class ChargeItem : EntityObject
{
	private int _ChargeItemID;

	private string _ItemNameCn;

	private string _ItemNamePt;

	private sbyte _Status;

	private sbyte _IsDefaultMOP;

	private int _precedence;

	private int _ChargeItemType;

	private bool _IsDelete;

	public string ChargeItemName
	{
		get
		{
			string itemNameCn = _ItemNameCn;
			switch (LangManager.CurLanguage)
			{
			case SysLanguage.CHS:
			case SysLanguage.CHT:
				return _ItemNameCn;
			case SysLanguage.ENG:
			case SysLanguage.PT:
				return ItemNamePt;
			default:
				return itemNameCn;
			}
		}
	}

	[DataMember]
	[EdmScalarProperty(EntityKeyProperty = true, IsNullable = false)]
	public int ChargeItemID
	{
		get
		{
			return _ChargeItemID;
		}
		set
		{
			if (_ChargeItemID != value)
			{
				ReportPropertyChanging("ChargeItemID");
				_ChargeItemID = StructuralObject.SetValidValue(value);
				ReportPropertyChanged("ChargeItemID");
			}
		}
	}

	[EdmScalarProperty(EntityKeyProperty = false, IsNullable = false)]
	[DataMember]
	public string ItemNameCn
	{
		get
		{
			return _ItemNameCn;
		}
		set
		{
			ReportPropertyChanging("ItemNameCn");
			_ItemNameCn = StructuralObject.SetValidValue(value, isNullable: false);
			ReportPropertyChanged("ItemNameCn");
		}
	}

	[DataMember]
	[EdmScalarProperty(EntityKeyProperty = false, IsNullable = false)]
	public string ItemNamePt
	{
		get
		{
			return _ItemNamePt;
		}
		set
		{
			ReportPropertyChanging("ItemNamePt");
			_ItemNamePt = StructuralObject.SetValidValue(value, isNullable: false);
			ReportPropertyChanged("ItemNamePt");
		}
	}

	[EdmScalarProperty(EntityKeyProperty = false, IsNullable = false)]
	[DataMember]
	public sbyte Status
	{
		get
		{
			return _Status;
		}
		set
		{
			ReportPropertyChanging("Status");
			_Status = StructuralObject.SetValidValue(value);
			ReportPropertyChanged("Status");
		}
	}

	[DataMember]
	[EdmScalarProperty(EntityKeyProperty = false, IsNullable = false)]
	public sbyte IsDefaultMOP
	{
		get
		{
			return _IsDefaultMOP;
		}
		set
		{
			ReportPropertyChanging("IsDefaultMOP");
			_IsDefaultMOP = StructuralObject.SetValidValue(value);
			ReportPropertyChanged("IsDefaultMOP");
		}
	}

	[DataMember]
	[EdmScalarProperty(EntityKeyProperty = false, IsNullable = false)]
	public int precedence
	{
		get
		{
			return _precedence;
		}
		set
		{
			ReportPropertyChanging("precedence");
			_precedence = StructuralObject.SetValidValue(value);
			ReportPropertyChanged("precedence");
		}
	}

	[EdmScalarProperty(EntityKeyProperty = false, IsNullable = false)]
	[DataMember]
	public int ChargeItemType
	{
		get
		{
			return _ChargeItemType;
		}
		set
		{
			ReportPropertyChanging("ChargeItemType");
			_ChargeItemType = StructuralObject.SetValidValue(value);
			ReportPropertyChanged("ChargeItemType");
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

	public static ChargeItem CreateChargeItem(int chargeItemID, string itemNameCn, string itemNamePt, sbyte status, sbyte isDefaultMOP, int precedence, int chargeItemType, bool isDelete)
	{
		ChargeItem chargeItem = new ChargeItem();
		chargeItem.ChargeItemID = chargeItemID;
		chargeItem.ItemNameCn = itemNameCn;
		chargeItem.ItemNamePt = itemNamePt;
		chargeItem.Status = status;
		chargeItem.IsDefaultMOP = isDefaultMOP;
		chargeItem.precedence = precedence;
		chargeItem.ChargeItemType = chargeItemType;
		chargeItem.IsDelete = isDelete;
		return chargeItem;
	}
}
