using System.Runtime.Serialization;

namespace Master.SystemCommunication.Lib;

/// <summary>
/// 獲取離線卡信息
/// </summary>
public class GetOfflineCardArgs
{
	[DataMember]
	public int GateID { get; set; }
}
