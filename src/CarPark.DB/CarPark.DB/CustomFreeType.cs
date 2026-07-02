using System;
using System.Data.Objects.DataClasses;
using System.Runtime.Serialization;
using SkyInno.Lang;

namespace CarPark.DB;

[Serializable]
[DataContract(IsReference = true)]
[EdmEntityType(NamespaceName = "CarPark.DB", Name = "CustomFreeType")]
public class CustomFreeType : EntityObject
{
	private bool? _IsDiscountByPercentage;

	private decimal? _DiscountPercentage;

	private bool? _IsDiscountByAmount;

	private decimal? _DiscountAmount;

	private int? _DiscountMinutes;

	private int _CustomFreeTypeID;

	private string _CustomFreeNameCn;

	private string _CustomFreeNamePt;

	private bool _IsAllFree;

	private bool _IsFreeByTime;

	private int _FreeMinutes;

	private decimal _FreeAmount;

	private bool _IsEmployeesPrice;

	private decimal _EmployeesAmount;

	private bool _IsDelete;

	[EdmScalarProperty(EntityKeyProperty = false, IsNullable = true)]
	[DataMember]
	public bool? IsDiscountByPercentage
	{
		get
		{
			return _IsDiscountByPercentage;
		}
		set
		{
			ReportPropertyChanging("IsDiscountByPercentage");
			_IsDiscountByPercentage = StructuralObject.SetValidValue(value);
			ReportPropertyChanged("IsDiscountByPercentage");
		}
	}

	[DataMember]
	[EdmScalarProperty(EntityKeyProperty = false, IsNullable = true)]
	public decimal? DiscountPercentage
	{
		get
		{
			return _DiscountPercentage;
		}
		set
		{
			ReportPropertyChanging("DiscountPercentage");
			_DiscountPercentage = StructuralObject.SetValidValue(value);
			ReportPropertyChanged("DiscountPercentage");
		}
	}

	[DataMember]
	[EdmScalarProperty(EntityKeyProperty = false, IsNullable = true)]
	public bool? IsDiscountByAmount
	{
		get
		{
			return _IsDiscountByAmount;
		}
		set
		{
			ReportPropertyChanging("IsDiscountByAmount");
			_IsDiscountByAmount = StructuralObject.SetValidValue(value);
			ReportPropertyChanged("IsDiscountByAmount");
		}
	}

	[EdmScalarProperty(EntityKeyProperty = false, IsNullable = true)]
	[DataMember]
	public decimal? DiscountAmount
	{
		get
		{
			return _DiscountAmount;
		}
		set
		{
			ReportPropertyChanging("DiscountAmount");
			_DiscountAmount = StructuralObject.SetValidValue(value);
			ReportPropertyChanged("DiscountAmount");
		}
	}

	[EdmScalarProperty(EntityKeyProperty = false, IsNullable = true)]
	[DataMember]
	public int? DiscountMinutes
	{
		get
		{
			return _DiscountMinutes;
		}
		set
		{
			ReportPropertyChanging("DiscountMinutes");
			_DiscountMinutes = StructuralObject.SetValidValue(value);
			ReportPropertyChanged("DiscountMinutes");
		}
	}

	public string CustomFreeName
	{
		get
		{
			string customFreeNameCn = CustomFreeNameCn;
			switch (LangManager.CurLanguage)
			{
			case SysLanguage.CHS:
			case SysLanguage.CHT:
				return customFreeNameCn;
			case SysLanguage.ENG:
			case SysLanguage.PT:
				return CustomFreeNamePt;
			default:
				return customFreeNameCn;
			}
		}
	}

	[EdmScalarProperty(EntityKeyProperty = true, IsNullable = false)]
	[DataMember]
	public int CustomFreeTypeID
	{
		get
		{
			return _CustomFreeTypeID;
		}
		set
		{
			if (_CustomFreeTypeID != value)
			{
				ReportPropertyChanging("CustomFreeTypeID");
				_CustomFreeTypeID = StructuralObject.SetValidValue(value);
				ReportPropertyChanged("CustomFreeTypeID");
			}
		}
	}

	[DataMember]
	[EdmScalarProperty(EntityKeyProperty = false, IsNullable = false)]
	public string CustomFreeNameCn
	{
		get
		{
			return _CustomFreeNameCn;
		}
		set
		{
			ReportPropertyChanging("CustomFreeNameCn");
			_CustomFreeNameCn = StructuralObject.SetValidValue(value, isNullable: false);
			ReportPropertyChanged("CustomFreeNameCn");
		}
	}

	[EdmScalarProperty(EntityKeyProperty = false, IsNullable = false)]
	[DataMember]
	public string CustomFreeNamePt
	{
		get
		{
			return _CustomFreeNamePt;
		}
		set
		{
			ReportPropertyChanging("CustomFreeNamePt");
			_CustomFreeNamePt = StructuralObject.SetValidValue(value, isNullable: false);
			ReportPropertyChanged("CustomFreeNamePt");
		}
	}

	[EdmScalarProperty(EntityKeyProperty = false, IsNullable = false)]
	[DataMember]
	public bool IsAllFree
	{
		get
		{
			return _IsAllFree;
		}
		set
		{
			ReportPropertyChanging("IsAllFree");
			_IsAllFree = StructuralObject.SetValidValue(value);
			ReportPropertyChanged("IsAllFree");
		}
	}

	[EdmScalarProperty(EntityKeyProperty = false, IsNullable = false)]
	[DataMember]
	public bool IsFreeByTime
	{
		get
		{
			return _IsFreeByTime;
		}
		set
		{
			ReportPropertyChanging("IsFreeByTime");
			_IsFreeByTime = StructuralObject.SetValidValue(value);
			ReportPropertyChanged("IsFreeByTime");
		}
	}

	[EdmScalarProperty(EntityKeyProperty = false, IsNullable = false)]
	[DataMember]
	public int FreeMinutes
	{
		get
		{
			return _FreeMinutes;
		}
		set
		{
			ReportPropertyChanging("FreeMinutes");
			_FreeMinutes = StructuralObject.SetValidValue(value);
			ReportPropertyChanged("FreeMinutes");
		}
	}

	[DataMember]
	[EdmScalarProperty(EntityKeyProperty = false, IsNullable = false)]
	public decimal FreeAmount
	{
		get
		{
			return _FreeAmount;
		}
		set
		{
			ReportPropertyChanging("FreeAmount");
			_FreeAmount = StructuralObject.SetValidValue(value);
			ReportPropertyChanged("FreeAmount");
		}
	}

	[DataMember]
	[EdmScalarProperty(EntityKeyProperty = false, IsNullable = false)]
	public bool IsEmployeesPrice
	{
		get
		{
			return _IsEmployeesPrice;
		}
		set
		{
			ReportPropertyChanging("IsEmployeesPrice");
			_IsEmployeesPrice = StructuralObject.SetValidValue(value);
			ReportPropertyChanged("IsEmployeesPrice");
		}
	}

	[DataMember]
	[EdmScalarProperty(EntityKeyProperty = false, IsNullable = false)]
	public decimal EmployeesAmount
	{
		get
		{
			return _EmployeesAmount;
		}
		set
		{
			ReportPropertyChanging("EmployeesAmount");
			_EmployeesAmount = StructuralObject.SetValidValue(value);
			ReportPropertyChanged("EmployeesAmount");
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

	public static CustomFreeType CreateCustomFreeType(int customFreeTypeID, string customFreeNameCn, string customFreeNamePt, bool isAllFree, bool isFreeByTime, int freeMinutes, decimal freeAmount, bool isEmployeesPrice, decimal employeesAmount, bool isDelete)
	{
		CustomFreeType customFreeType = new CustomFreeType();
		customFreeType.CustomFreeTypeID = customFreeTypeID;
		customFreeType.CustomFreeNameCn = customFreeNameCn;
		customFreeType.CustomFreeNamePt = customFreeNamePt;
		customFreeType.IsAllFree = isAllFree;
		customFreeType.IsFreeByTime = isFreeByTime;
		customFreeType.FreeMinutes = freeMinutes;
		customFreeType.FreeAmount = freeAmount;
		customFreeType.IsEmployeesPrice = isEmployeesPrice;
		customFreeType.EmployeesAmount = employeesAmount;
		customFreeType.IsDelete = isDelete;
		return customFreeType;
	}
}
