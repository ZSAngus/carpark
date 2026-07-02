using System;
using System.Runtime.Serialization;

namespace Master.SystemCommunication.Lib;

/// <summary>
/// 出口时租参数
/// </summary>
public class ExitTicketArgs : Carpark2018Args
{
	/// <summary>
	/// 入场时间
	/// </summary>
	[DataMember]
	public DateTime OutTime { get; set; }

	/// <summary>
	/// Is485=true,取InTime，否则为232信息，取服务本地时间
	/// </summary>
	[DataMember]
	public bool Is485 { get; set; }

	/// <summary>
	/// 存在用戶號表示人手通過，可不比對車牌
	/// </summary>
	[DataMember]
	public string StaffCode { get; set; }

	[DataMember]
	public int CardType { get; set; }
}
