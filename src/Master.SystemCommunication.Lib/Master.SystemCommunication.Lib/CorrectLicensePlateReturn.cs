using System.Runtime.Serialization;

namespace Master.SystemCommunication.Lib;

/// <summary>
/// 糾正車牌後返回結果
/// </summary>
public class CorrectLicensePlateReturn : ProgramBase
{
	[DataMember]
	public bool ISOK { get; set; }

	[DataMember]
	public string ErrCode { get; set; }

	public CorrectLicensePlateReturn()
	{
	}

	public CorrectLicensePlateReturn(string onlyID)
		: base(onlyID)
	{
	}
}
