using System.Runtime.Serialization;

namespace Master.SystemCommunication.Lib;

/// <summary>
/// 離線卡結構
/// </summary>
public class OfflineCard
{
	/// <summary>
	/// 卡邏輯號
	/// </summary>
	[DataMember]
	public string CardCode { get; set; }

	/// <summary>
	/// 車型
	/// </summary>
	[DataMember]
	public int ParkTypeID { get; set; }
}
