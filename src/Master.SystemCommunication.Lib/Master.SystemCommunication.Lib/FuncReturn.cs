using System.Runtime.Serialization;

namespace Master.SystemCommunication.Lib;

/// <summary>
/// 返回结果基类
/// </summary>
public class FuncReturn
{
	/// <summary>
	/// 是否成功保存數據
	/// </summary>
	[DataMember]
	public bool ISSaveSuccess { get; set; }

	/// <summary>
	/// 是否需要人工比對
	/// </summary>
	[DataMember]
	public bool ISNeedContrast { get; set; }

	/// <summary>
	/// 錯誤信息
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
