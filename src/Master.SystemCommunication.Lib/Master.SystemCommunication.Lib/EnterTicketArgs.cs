using System;
using System.Runtime.Serialization;

namespace Master.SystemCommunication.Lib;

/// <summary>
///             時租入口參數
/// </summary>
public class EnterTicketArgs : Carpark2018Args
{
	/// <summary>
	/// 入场时间
	/// </summary>
	[DataMember]
	public DateTime InTime { get; set; }

	/// <summary>
	/// Is485=true,取InTime，否则为232信息，取服务本地时间
	/// </summary>
	[DataMember]
	public bool Is485 { get; set; }

	[DataMember]
	public int CardType { get; set; }
}
