using System;
using System.Runtime.Serialization;

namespace Master.SystemCommunication.Lib;

/// <summary>
/// 時租收費計算輸入參數
/// </summary>
public class CalcTicketFeeArgs
{
	[DataMember]
	public string TicketNumber { get; set; }

	[DataMember]
	public DateTime InTime { get; set; }

	[DataMember]
	public string SerialNumber { get; set; }

	[DataMember]
	public int CustomFreeID { get; set; }

	[DataMember]
	public int CustomFreeTenatID { get; set; }

	[DataMember]
	public string BarCode { get; set; }

	/// <summary>
	/// 收費時間
	/// </summary>
	[DataMember]
	public DateTime ChargeTime { get; set; }

	[DataMember]
	public string StaffCode { get; set; }

	/// <summary>
	/// 操作PC名
	/// </summary>
	[DataMember]
	public string PayStationName { get; set; }

	/// <summary>
	/// 場號，失票傳入場號計算收費
	/// </summary>
	[DataMember]
	public int AreaID { get; set; }

	[DataMember]
	public bool ISFine { get; set; }
}
