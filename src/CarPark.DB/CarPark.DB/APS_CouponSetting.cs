using System;
using System.Data.Objects.DataClasses;
using System.Runtime.Serialization;

namespace CarPark.DB;

[Serializable]
[EdmEntityType(NamespaceName = "CarPark.DB", Name = "APS_CouponSetting")]
[DataContract(IsReference = true)]
public class APS_CouponSetting : EntityObject
{
	private int _CouponSettingID;

	private int _StationID;

	private int _TrayQty;

	private int _ExistQty;

	private int _WarningQty;

	[DataMember]
	[EdmScalarProperty(EntityKeyProperty = true, IsNullable = false)]
	public int CouponSettingID
	{
		get
		{
			return _CouponSettingID;
		}
		set
		{
			if (_CouponSettingID != value)
			{
				ReportPropertyChanging("CouponSettingID");
				_CouponSettingID = StructuralObject.SetValidValue(value);
				ReportPropertyChanged("CouponSettingID");
			}
		}
	}

	[DataMember]
	[EdmScalarProperty(EntityKeyProperty = false, IsNullable = false)]
	public int StationID
	{
		get
		{
			return _StationID;
		}
		set
		{
			ReportPropertyChanging("StationID");
			_StationID = StructuralObject.SetValidValue(value);
			ReportPropertyChanged("StationID");
		}
	}

	[DataMember]
	[EdmScalarProperty(EntityKeyProperty = false, IsNullable = false)]
	public int TrayQty
	{
		get
		{
			return _TrayQty;
		}
		set
		{
			ReportPropertyChanging("TrayQty");
			_TrayQty = StructuralObject.SetValidValue(value);
			ReportPropertyChanged("TrayQty");
		}
	}

	[EdmScalarProperty(EntityKeyProperty = false, IsNullable = false)]
	[DataMember]
	public int ExistQty
	{
		get
		{
			return _ExistQty;
		}
		set
		{
			ReportPropertyChanging("ExistQty");
			_ExistQty = StructuralObject.SetValidValue(value);
			ReportPropertyChanged("ExistQty");
		}
	}

	[DataMember]
	[EdmScalarProperty(EntityKeyProperty = false, IsNullable = false)]
	public int WarningQty
	{
		get
		{
			return _WarningQty;
		}
		set
		{
			ReportPropertyChanging("WarningQty");
			_WarningQty = StructuralObject.SetValidValue(value);
			ReportPropertyChanged("WarningQty");
		}
	}

	public static APS_CouponSetting CreateAPS_CouponSetting(int couponSettingID, int stationID, int trayQty, int existQty, int warningQty)
	{
		APS_CouponSetting aPS_CouponSetting = new APS_CouponSetting();
		aPS_CouponSetting.CouponSettingID = couponSettingID;
		aPS_CouponSetting.StationID = stationID;
		aPS_CouponSetting.TrayQty = trayQty;
		aPS_CouponSetting.ExistQty = existQty;
		aPS_CouponSetting.WarningQty = warningQty;
		return aPS_CouponSetting;
	}
}
