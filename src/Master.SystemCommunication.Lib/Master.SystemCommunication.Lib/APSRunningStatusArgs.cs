using System;
using System.Runtime.Serialization;

namespace Master.SystemCommunication.Lib;

/// <summary>
/// aps運行狀態
/// </summary>
public class APSRunningStatusArgs
{
	[DataMember]
	public int APSID { get; set; }

	[DataMember]
	public int APSCurrStatus { get; set; }

	[DataMember]
	public DateTime LastUpdateTime { get; set; }

	[DataMember]
	public DateTime LastWamingTime { get; set; }

	[DataMember]
	public int LastWamingEvent { get; set; }

	[DataMember]
	public DateTime LastFeeTime { get; set; }

	[DataMember]
	public string TicketCode { get; set; }

	[DataMember]
	public string CouponCode { get; set; }

	[DataMember]
	public string Certificate { get; set; }

	[DataMember]
	public DateTime LastLoginTime { get; set; }

	[DataMember]
	public string LastLoginStaffCode { get; set; }

	[DataMember]
	public int LastOperating { get; set; }

	[DataMember]
	public int LastAPSMXtraSendCmd { get; set; }

	[DataMember]
	public bool IsCarriedOut { get; set; }

	[DataMember]
	public bool IsAlarm { get; set; }

	[DataMember]
	public bool IsShowMsg { get; set; }
}
