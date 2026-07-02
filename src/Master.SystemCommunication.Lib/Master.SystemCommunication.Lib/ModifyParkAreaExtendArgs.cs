using System.Runtime.Serialization;

namespace Master.SystemCommunication.Lib;

/// <summary>
/// 修改車位輸入參數
/// </summary>
public class ModifyParkAreaExtendArgs
{
	/// <summary>
	/// 操作PC名
	/// </summary>
	[DataMember]
	public string PayStationName { get; set; }

	[DataMember]
	public string StaffCode { get; set; }
}
