using System.Runtime.Serialization;

namespace Master.SystemCommunication.Lib;

/// <summary>
///  保存澳门通增值返回结果
/// </summary>
public class SaveMPassChargeReturn
{
	/// <summary>
	/// 是否有效
	/// </summary>
	[DataMember]
	public bool ISValid { get; set; }

	/// <summary>
	/// 無效信息
	/// </summary>
	[DataMember]
	public string ErrCode { get; set; }
}
