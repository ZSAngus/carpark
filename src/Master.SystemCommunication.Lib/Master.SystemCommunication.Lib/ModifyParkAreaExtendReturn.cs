using System.Runtime.Serialization;

namespace Master.SystemCommunication.Lib;

/// <summary>
/// 修改車位輸出參數
/// </summary>
public class ModifyParkAreaExtendReturn
{
	[DataMember]
	public bool ISOK { get; set; }

	[DataMember]
	public string ErrCode { get; set; }
}
