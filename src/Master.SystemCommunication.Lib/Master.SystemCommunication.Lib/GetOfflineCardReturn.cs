using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Master.SystemCommunication.Lib;

/// <summary>
/// 離線卡信息返回
/// </summary>
public class GetOfflineCardReturn
{
	/// <summary>
	/// 離線卡集合
	/// </summary>
	[DataMember]
	public List<OfflineCard> OfflineCardList { get; set; }
}
