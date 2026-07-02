using System.Runtime.Serialization;

namespace Master.SystemCommunication.Lib;

public class UpdateTimeChargeArgs
{
	/// <summary>
	/// 操作PC名
	/// </summary>
	[DataMember]
	public string PayStationName { get; set; }

	[DataMember]
	public string StaffCode { get; set; }
}
