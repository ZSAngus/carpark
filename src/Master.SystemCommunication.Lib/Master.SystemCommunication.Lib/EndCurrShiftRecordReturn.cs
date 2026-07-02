using System.Runtime.Serialization;

namespace Master.SystemCommunication.Lib;

public class EndCurrShiftRecordReturn
{
	[DataMember]
	public bool ISOK { get; set; }

	[DataMember]
	public string ErrCode { get; set; }

	/// <summary>
	/// 新的更次號
	/// </summary>
	[DataMember]
	public int NewShiftID { get; set; }
}
