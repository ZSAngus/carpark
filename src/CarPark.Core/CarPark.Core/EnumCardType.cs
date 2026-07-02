using System;
using System.ComponentModel;

namespace CarPark.Core;

[Flags]
public enum EnumCardType
{
	[Description("時租票")]
	Ticket = 0,
	[Description("智能卡")]
	SmartCard = 1,
	[Description("信用卡")]
	PrePaidCard = 2,
	[Description("澳门通")]
	MacauPass = 3,
	[Description("條碼卡")]
	Tape = 4,
	[Description("銀聯閃付卡")]
	PBOC = 5,
	[Description("中国人民银行")]
	BOCPBOC = 6,
	[Description("工商銀行")]
	ICBC = 7,
	[Description("臨時月租卡")]
	TempMonthCard = 8,
	[Description("保安卡")]
	SecurityCard = 9,
	[Description("RFID時租")]
	RFID_Ticket = 0xA,
	[Description("RFID月租")]
	RFID_SmartCard = 0xB,
	[Description("時租車牌")]
	LicensePlate = 0xC
}
