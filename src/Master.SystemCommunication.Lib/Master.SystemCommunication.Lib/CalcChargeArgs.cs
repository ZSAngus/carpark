using System;
using System.Runtime.Serialization;
using CarPark.Core;

namespace Master.SystemCommunication.Lib;

/// <summary>
/// 收費參數
/// </summary>
public class CalcChargeArgs
{
	[DataMember]
	public string TicketNumber { get; set; }

	[DataMember]
	public EnumParkType parkType { get; set; }

	[DataMember]
	public DateTime passTime { get; set; }

	[DataMember]
	public string SerialNo { get; set; }

	/// <summary>
	/// 服務設定收費時間
	/// </summary>
	[DataMember]
	public DateTime FeeTime { get; set; }
}
