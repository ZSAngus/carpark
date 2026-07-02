using System.Runtime.Serialization;

namespace Master.SystemCommunication.Lib;

/// <summary>
/// 手動添加車牌返回結果
/// </summary>
public class ManualEntryLicensePlateReturn : ProgramBase
{
	[DataMember]
	public bool ISOK { get; set; }

	[DataMember]
	public string ErrCode { get; set; }

	public ManualEntryLicensePlateReturn()
	{
	}

	public ManualEntryLicensePlateReturn(string onlyID)
		: base(onlyID)
	{
	}
}
