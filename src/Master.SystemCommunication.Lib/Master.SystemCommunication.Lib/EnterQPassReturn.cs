using System.Runtime.Serialization;

namespace Master.SystemCommunication.Lib;

/// <summary>
/// 入口閃付返回信息
/// </summary>
public class EnterQPassReturn
{
	/// <summary>
	/// 是否通過
	/// </summary>
	[DataMember]
	public bool ISPass { get; set; }

	/// <summary>
	/// 不通過原因
	/// </summary>
	[DataMember]
	public string ErrCode { get; set; }
}
