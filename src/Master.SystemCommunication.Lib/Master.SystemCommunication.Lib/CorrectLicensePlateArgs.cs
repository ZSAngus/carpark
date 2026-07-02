using System.Runtime.Serialization;

namespace Master.SystemCommunication.Lib;

/// <summary>
/// 糾正車牌參數
/// </summary>
public class CorrectLicensePlateArgs : ProgramBase
{
	/// <summary>
	/// 業務記錄ＩＤ
	/// </summary>
	[DataMember]
	public int TransactionDataID { get; set; }

	/// <summary>
	/// 糾正後的新車牌
	/// </summary>
	[DataMember]
	public string NewLicensePlate { get; set; }

	/// <summary>
	/// 操作PC名
	/// </summary>
	[DataMember]
	public string PayStationName { get; set; }

	[DataMember]
	public string StaffCode { get; set; }

	public CorrectLicensePlateArgs()
	{
	}

	public CorrectLicensePlateArgs(string onlyID)
		: base(onlyID)
	{
	}
}
