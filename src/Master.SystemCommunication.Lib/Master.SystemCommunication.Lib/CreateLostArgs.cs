using System.Runtime.Serialization;

namespace Master.SystemCommunication.Lib;

public class CreateLostArgs
{
	[DataMember]
	public string StaffCode { get; set; }

	/// <summary>
	/// 操作PC名
	/// </summary>
	[DataMember]
	public string PayStationName { get; set; }
}
