using System.Runtime.Serialization;

namespace Master.SystemCommunication.Lib;

/// <summary>
/// 入口月租返回信息
/// </summary>
public class EnterMonthReturn
{
	/// <summary>
	/// 是否保存成功
	/// </summary>
	[DataMember]
	public bool ISPass { get; set; }

	/// <summary>
	/// 不通過原因
	/// </summary>
	[DataMember]
	public string ErrCode { get; set; }

	/// <summary>
	/// 是否需要人工比對
	/// </summary>
	[DataMember]
	public bool ISNeedContrast { get; set; }

	/// <summary>
	/// 登記車牌
	/// </summary>
	[DataMember]
	public string RegistrationLicensePlate { get; set; }

	/// <summary>
	/// 是否離線
	/// </summary>
	[DataMember]
	public bool ISOffline { get; set; }
}
