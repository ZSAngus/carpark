using System.Runtime.Serialization;

namespace Master.SystemCommunication.Lib;

/// <summary>
/// 参数基类
/// </summary>
public class Carpark2018Args
{
	/// <summary>
	/// 閘口ID
	/// </summary>
	[DataMember]
	public int GateID { get; set; }

	/// <summary>
	/// 票或卡號碼
	/// </summary>
	[DataMember]
	public string CardNumber { get; set; }

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
}
