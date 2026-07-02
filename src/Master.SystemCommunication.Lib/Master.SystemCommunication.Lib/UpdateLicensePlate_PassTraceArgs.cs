using System.Runtime.Serialization;

namespace Master.SystemCommunication.Lib;

/// <summary>
/// 更新車牌識別參數
/// </summary>
public class UpdateLicensePlate_PassTraceArgs
{
	/// <summary>
	/// 操作用戶
	/// </summary>
	[DataMember]
	public string StaffCode { get; set; }

	/// <summary>
	/// 車牌
	/// </summary>
	[DataMember]
	public string LicensePlateStr { get; set; }

	/// <summary>
	/// 圖片路徑
	/// </summary>
	[DataMember]
	public string ImagePath { get; set; }

	/// <summary>
	/// 車牌表ID
	/// </summary>
	[DataMember]
	public int LicensePlateID { get; set; }
}
