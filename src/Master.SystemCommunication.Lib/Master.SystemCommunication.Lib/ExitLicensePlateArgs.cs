using System.Runtime.Serialization;

namespace Master.SystemCommunication.Lib;

/// <summary>
/// 出口檢測到車牌
/// </summary>
public class ExitLicensePlateArgs
{
	/// <summary>
	/// 閘口ID
	/// </summary>
	[DataMember]
	public int GateID { get; set; }

	/// <summary>
	/// 車牌
	/// </summary>
	[DataMember]
	public string LicensePlateStr { get; set; }

	/// <summary>
	/// 車牌圖片路徑
	/// </summary>
	[DataMember]
	public string ImagePath { get; set; }

	/// <summary>
	/// 車牌可信度
	/// </summary>
	[DataMember]
	public double Similarity { get; set; }

	[DataMember]
	public string PayStationName { get; set; }
}
