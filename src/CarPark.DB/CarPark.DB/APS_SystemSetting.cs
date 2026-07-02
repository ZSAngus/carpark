using System;
using System.Data.Objects.DataClasses;
using System.Runtime.Serialization;

namespace CarPark.DB;

[Serializable]
[EdmEntityType(NamespaceName = "CarPark.DB", Name = "APS_SystemSetting")]
[DataContract(IsReference = true)]
public class APS_SystemSetting : EntityObject
{
	private int _SettingID;

	private int _MsgShowTime;

	private int _HourlyWaitTime;

	private int _FinishWaitTime;

	private int _LoginWaitTime;

	private int _FinanceWaitTime;

	private int _FinanceWarnTime;

	private int _RepairWaitTime;

	private int _RepairWarnTime;

	private int _UploadRecordTime;

	private int _UpdateStatusTime;

	private int _CheckInterval;

	private int _BankNotesAcceptorQty;

	private int _BankNotesAcceptorWamingQty;

	private int _CoinsAcceptorQty;

	private int _CoinsAcceptorWamingQty;

	private int _BankNotesDispenserLowlevelQty;

	private int _BankNotesDispenserResetQty;

	private int _CoinsDispenserLowlevelQty;

	private int _CoinsDispenserResetQty;

	private int _TrayQty;

	private int _WamingQty;

	[DataMember]
	[EdmScalarProperty(EntityKeyProperty = true, IsNullable = false)]
	public int SettingID
	{
		get
		{
			return _SettingID;
		}
		set
		{
			if (_SettingID != value)
			{
				ReportPropertyChanging("SettingID");
				_SettingID = StructuralObject.SetValidValue(value);
				ReportPropertyChanged("SettingID");
			}
		}
	}

	[EdmScalarProperty(EntityKeyProperty = false, IsNullable = false)]
	[DataMember]
	public int MsgShowTime
	{
		get
		{
			return _MsgShowTime;
		}
		set
		{
			ReportPropertyChanging("MsgShowTime");
			_MsgShowTime = StructuralObject.SetValidValue(value);
			ReportPropertyChanged("MsgShowTime");
		}
	}

	[DataMember]
	[EdmScalarProperty(EntityKeyProperty = false, IsNullable = false)]
	public int HourlyWaitTime
	{
		get
		{
			return _HourlyWaitTime;
		}
		set
		{
			ReportPropertyChanging("HourlyWaitTime");
			_HourlyWaitTime = StructuralObject.SetValidValue(value);
			ReportPropertyChanged("HourlyWaitTime");
		}
	}

	[DataMember]
	[EdmScalarProperty(EntityKeyProperty = false, IsNullable = false)]
	public int FinishWaitTime
	{
		get
		{
			return _FinishWaitTime;
		}
		set
		{
			ReportPropertyChanging("FinishWaitTime");
			_FinishWaitTime = StructuralObject.SetValidValue(value);
			ReportPropertyChanged("FinishWaitTime");
		}
	}

	[EdmScalarProperty(EntityKeyProperty = false, IsNullable = false)]
	[DataMember]
	public int LoginWaitTime
	{
		get
		{
			return _LoginWaitTime;
		}
		set
		{
			ReportPropertyChanging("LoginWaitTime");
			_LoginWaitTime = StructuralObject.SetValidValue(value);
			ReportPropertyChanged("LoginWaitTime");
		}
	}

	[EdmScalarProperty(EntityKeyProperty = false, IsNullable = false)]
	[DataMember]
	public int FinanceWaitTime
	{
		get
		{
			return _FinanceWaitTime;
		}
		set
		{
			ReportPropertyChanging("FinanceWaitTime");
			_FinanceWaitTime = StructuralObject.SetValidValue(value);
			ReportPropertyChanged("FinanceWaitTime");
		}
	}

	[EdmScalarProperty(EntityKeyProperty = false, IsNullable = false)]
	[DataMember]
	public int FinanceWarnTime
	{
		get
		{
			return _FinanceWarnTime;
		}
		set
		{
			ReportPropertyChanging("FinanceWarnTime");
			_FinanceWarnTime = StructuralObject.SetValidValue(value);
			ReportPropertyChanged("FinanceWarnTime");
		}
	}

	[EdmScalarProperty(EntityKeyProperty = false, IsNullable = false)]
	[DataMember]
	public int RepairWaitTime
	{
		get
		{
			return _RepairWaitTime;
		}
		set
		{
			ReportPropertyChanging("RepairWaitTime");
			_RepairWaitTime = StructuralObject.SetValidValue(value);
			ReportPropertyChanged("RepairWaitTime");
		}
	}

	[DataMember]
	[EdmScalarProperty(EntityKeyProperty = false, IsNullable = false)]
	public int RepairWarnTime
	{
		get
		{
			return _RepairWarnTime;
		}
		set
		{
			ReportPropertyChanging("RepairWarnTime");
			_RepairWarnTime = StructuralObject.SetValidValue(value);
			ReportPropertyChanged("RepairWarnTime");
		}
	}

	[EdmScalarProperty(EntityKeyProperty = false, IsNullable = false)]
	[DataMember]
	public int UploadRecordTime
	{
		get
		{
			return _UploadRecordTime;
		}
		set
		{
			ReportPropertyChanging("UploadRecordTime");
			_UploadRecordTime = StructuralObject.SetValidValue(value);
			ReportPropertyChanged("UploadRecordTime");
		}
	}

	[EdmScalarProperty(EntityKeyProperty = false, IsNullable = false)]
	[DataMember]
	public int UpdateStatusTime
	{
		get
		{
			return _UpdateStatusTime;
		}
		set
		{
			ReportPropertyChanging("UpdateStatusTime");
			_UpdateStatusTime = StructuralObject.SetValidValue(value);
			ReportPropertyChanged("UpdateStatusTime");
		}
	}

	[EdmScalarProperty(EntityKeyProperty = false, IsNullable = false)]
	[DataMember]
	public int CheckInterval
	{
		get
		{
			return _CheckInterval;
		}
		set
		{
			ReportPropertyChanging("CheckInterval");
			_CheckInterval = StructuralObject.SetValidValue(value);
			ReportPropertyChanged("CheckInterval");
		}
	}

	[EdmScalarProperty(EntityKeyProperty = false, IsNullable = false)]
	[DataMember]
	public int BankNotesAcceptorQty
	{
		get
		{
			return _BankNotesAcceptorQty;
		}
		set
		{
			ReportPropertyChanging("BankNotesAcceptorQty");
			_BankNotesAcceptorQty = StructuralObject.SetValidValue(value);
			ReportPropertyChanged("BankNotesAcceptorQty");
		}
	}

	[DataMember]
	[EdmScalarProperty(EntityKeyProperty = false, IsNullable = false)]
	public int BankNotesAcceptorWamingQty
	{
		get
		{
			return _BankNotesAcceptorWamingQty;
		}
		set
		{
			ReportPropertyChanging("BankNotesAcceptorWamingQty");
			_BankNotesAcceptorWamingQty = StructuralObject.SetValidValue(value);
			ReportPropertyChanged("BankNotesAcceptorWamingQty");
		}
	}

	[EdmScalarProperty(EntityKeyProperty = false, IsNullable = false)]
	[DataMember]
	public int CoinsAcceptorQty
	{
		get
		{
			return _CoinsAcceptorQty;
		}
		set
		{
			ReportPropertyChanging("CoinsAcceptorQty");
			_CoinsAcceptorQty = StructuralObject.SetValidValue(value);
			ReportPropertyChanged("CoinsAcceptorQty");
		}
	}

	[DataMember]
	[EdmScalarProperty(EntityKeyProperty = false, IsNullable = false)]
	public int CoinsAcceptorWamingQty
	{
		get
		{
			return _CoinsAcceptorWamingQty;
		}
		set
		{
			ReportPropertyChanging("CoinsAcceptorWamingQty");
			_CoinsAcceptorWamingQty = StructuralObject.SetValidValue(value);
			ReportPropertyChanged("CoinsAcceptorWamingQty");
		}
	}

	[EdmScalarProperty(EntityKeyProperty = false, IsNullable = false)]
	[DataMember]
	public int BankNotesDispenserLowlevelQty
	{
		get
		{
			return _BankNotesDispenserLowlevelQty;
		}
		set
		{
			ReportPropertyChanging("BankNotesDispenserLowlevelQty");
			_BankNotesDispenserLowlevelQty = StructuralObject.SetValidValue(value);
			ReportPropertyChanged("BankNotesDispenserLowlevelQty");
		}
	}

	[DataMember]
	[EdmScalarProperty(EntityKeyProperty = false, IsNullable = false)]
	public int BankNotesDispenserResetQty
	{
		get
		{
			return _BankNotesDispenserResetQty;
		}
		set
		{
			ReportPropertyChanging("BankNotesDispenserResetQty");
			_BankNotesDispenserResetQty = StructuralObject.SetValidValue(value);
			ReportPropertyChanged("BankNotesDispenserResetQty");
		}
	}

	[EdmScalarProperty(EntityKeyProperty = false, IsNullable = false)]
	[DataMember]
	public int CoinsDispenserLowlevelQty
	{
		get
		{
			return _CoinsDispenserLowlevelQty;
		}
		set
		{
			ReportPropertyChanging("CoinsDispenserLowlevelQty");
			_CoinsDispenserLowlevelQty = StructuralObject.SetValidValue(value);
			ReportPropertyChanged("CoinsDispenserLowlevelQty");
		}
	}

	[DataMember]
	[EdmScalarProperty(EntityKeyProperty = false, IsNullable = false)]
	public int CoinsDispenserResetQty
	{
		get
		{
			return _CoinsDispenserResetQty;
		}
		set
		{
			ReportPropertyChanging("CoinsDispenserResetQty");
			_CoinsDispenserResetQty = StructuralObject.SetValidValue(value);
			ReportPropertyChanged("CoinsDispenserResetQty");
		}
	}

	[EdmScalarProperty(EntityKeyProperty = false, IsNullable = false)]
	[DataMember]
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
	public int WamingQty
	{
		get
		{
			return _WamingQty;
		}
		set
		{
			ReportPropertyChanging("WamingQty");
			_WamingQty = StructuralObject.SetValidValue(value);
			ReportPropertyChanged("WamingQty");
		}
	}

	public static APS_SystemSetting CreateAPS_SystemSetting(int settingID, int msgShowTime, int hourlyWaitTime, int finishWaitTime, int loginWaitTime, int financeWaitTime, int financeWarnTime, int repairWaitTime, int repairWarnTime, int uploadRecordTime, int updateStatusTime, int checkInterval, int bankNotesAcceptorQty, int bankNotesAcceptorWamingQty, int coinsAcceptorQty, int coinsAcceptorWamingQty, int bankNotesDispenserLowlevelQty, int bankNotesDispenserResetQty, int coinsDispenserLowlevelQty, int coinsDispenserResetQty, int trayQty, int wamingQty)
	{
		APS_SystemSetting aPS_SystemSetting = new APS_SystemSetting();
		aPS_SystemSetting.SettingID = settingID;
		aPS_SystemSetting.MsgShowTime = msgShowTime;
		aPS_SystemSetting.HourlyWaitTime = hourlyWaitTime;
		aPS_SystemSetting.FinishWaitTime = finishWaitTime;
		aPS_SystemSetting.LoginWaitTime = loginWaitTime;
		aPS_SystemSetting.FinanceWaitTime = financeWaitTime;
		aPS_SystemSetting.FinanceWarnTime = financeWarnTime;
		aPS_SystemSetting.RepairWaitTime = repairWaitTime;
		aPS_SystemSetting.RepairWarnTime = repairWarnTime;
		aPS_SystemSetting.UploadRecordTime = uploadRecordTime;
		aPS_SystemSetting.UpdateStatusTime = updateStatusTime;
		aPS_SystemSetting.CheckInterval = checkInterval;
		aPS_SystemSetting.BankNotesAcceptorQty = bankNotesAcceptorQty;
		aPS_SystemSetting.BankNotesAcceptorWamingQty = bankNotesAcceptorWamingQty;
		aPS_SystemSetting.CoinsAcceptorQty = coinsAcceptorQty;
		aPS_SystemSetting.CoinsAcceptorWamingQty = coinsAcceptorWamingQty;
		aPS_SystemSetting.BankNotesDispenserLowlevelQty = bankNotesDispenserLowlevelQty;
		aPS_SystemSetting.BankNotesDispenserResetQty = bankNotesDispenserResetQty;
		aPS_SystemSetting.CoinsDispenserLowlevelQty = coinsDispenserLowlevelQty;
		aPS_SystemSetting.CoinsDispenserResetQty = coinsDispenserResetQty;
		aPS_SystemSetting.TrayQty = trayQty;
		aPS_SystemSetting.WamingQty = wamingQty;
		return aPS_SystemSetting;
	}
}
