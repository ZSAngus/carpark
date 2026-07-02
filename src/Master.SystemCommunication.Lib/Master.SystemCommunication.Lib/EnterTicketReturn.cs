using System.Runtime.Serialization;

namespace Master.SystemCommunication.Lib;

/// <summary>
/// 入口時租返回結果
/// </summary>
public class EnterTicketReturn
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
}
