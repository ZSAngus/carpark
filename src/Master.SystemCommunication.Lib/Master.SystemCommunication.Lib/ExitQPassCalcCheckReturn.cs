using System.Runtime.Serialization;

namespace Master.SystemCommunication.Lib;

/// <summary>
/// 闪付返回参数
/// </summary>
public class ExitQPassCalcCheckReturn
{
	/// <summary>
	///             出口是否驗證通過
	///             驗證通過可以按計算金額進行扣值
	///             驗證不通過顯示原因
	/// </summary>
	[DataMember]
	public bool ISVerifyPass { get; set; }

	/// <summary>
	/// 是否需要扣值
	/// </summary>
	[DataMember]
	public bool NeedToBuckle { get; set; }

	/// <summary>
	/// 不通過原因
	/// </summary>
	[DataMember]
	public string ErrCode { get; set; }

	/// <summary>
	/// 入場車牌
	/// </summary>
	[DataMember]
	public string InLicensePlateStr { get; set; }

	/// <summary>
	/// 入場車牌圖片路徑
	/// </summary>
	[DataMember]
	public string ImagePath { get; set; }

	[DataMember]
	public int PassTraceID { get; set; }

	[DataMember]
	public int TransactionDataID { get; set; }

	[DataMember]
	public int LicensePlate_PassTraceID { get; set; }
}
