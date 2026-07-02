using System.Runtime.Serialization;

namespace Master.SystemCommunication.Lib;

/// <summary>
/// 閘門機信息
/// </summary>
public class GateObj
{
	[DataMember]
	public DeviceStatus ACETicket { get; set; }

	[DataMember]
	public DeviceStatus Monthly { get; set; }

	[DataMember]
	public DeviceStatus MacauPass { get; set; }

	[DataMember]
	public DeviceStatus QuickPass { get; set; }
}
