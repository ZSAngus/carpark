using System;
using System.Runtime.Serialization;

namespace Master.SystemCommunication.Lib;

public class CalcTicketFeeReturnV2 : CalcTicketFeeReturn
{
	/// <summary>
	/// 車輛入場時間
	/// </summary>
	[DataMember]
	public DateTime InTime { get; set; }

	/// <summary>
	/// 車牌圖片路徑
	/// </summary>
	[DataMember]
	public string InLicensePlatePath { get; set; }

	/// <summary>
	/// 車牌
	/// </summary>
	[DataMember]
	public string LicensePlate { get; set; }
}
