using System.Runtime.Serialization;

namespace Master.SystemCommunication.Lib;

/// <summary>
/// 入口澳門通返回信息
/// </summary>
public class EnterMPassReturn
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
	/// 預計可停泊小時數
	/// </summary>
	[DataMember]
	public int ExpectedAnchorTime { get; set; }
}
