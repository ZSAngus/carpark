using System.Runtime.Serialization;

namespace Master.SystemCommunication.Lib;

public class GetInitInfoArgs
{
	/// <summary>
	/// 操作PC名
	/// </summary>
	[DataMember]
	public string PayStationName { get; set; }
}
