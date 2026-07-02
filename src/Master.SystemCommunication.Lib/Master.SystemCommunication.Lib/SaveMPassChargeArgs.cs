using System;
using System.Runtime.Serialization;

namespace Master.SystemCommunication.Lib;

/// <summary>
/// 保存澳门通增值传入参数
/// </summary>
public class SaveMPassChargeArgs
{
	[DataMember]
	public DateTime StartDate { get; set; }

	[DataMember]
	public DateTime ExpireDate { get; set; }

	/// <summary>
	/// 卡邏輯號
	/// </summary>
	[DataMember]
	public string CardCode { get; set; }
}
