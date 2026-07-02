using System.Runtime.Serialization;

namespace Master.SystemCommunication.Lib;

/// <summary>
/// 入口車牌返回
/// </summary>
public class EnterLicensePlateReturn : ProgramBase
{
	/// <summary>
	/// 能否起杆
	/// </summary>
	[DataMember]
	public bool CanOpenGate { get; set; }

	/// <summary>
	/// 錯誤信息
	/// </summary>
	[DataMember]
	public string ErrCode { get; set; }
}
