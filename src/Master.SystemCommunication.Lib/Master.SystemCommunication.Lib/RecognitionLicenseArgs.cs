using System.Runtime.Serialization;

namespace Master.SystemCommunication.Lib;

/// <summary>
/// 分析車牌參數
/// </summary>
public class RecognitionLicenseArgs : ProgramBase
{
	[DataMember]
	public int size { get; set; }

	[DataMember]
	public byte[] imagestream { get; set; }

	[DataMember]
	public string velue { get; set; }

	[DataMember]
	public double prob { get; set; }

	/// <summary>
	/// 擴展
	/// </summary>
	[DataMember]
	public string Extend { get; set; }

	public RecognitionLicenseArgs()
	{
	}

	public RecognitionLicenseArgs(string onlyID)
		: base(onlyID)
	{
	}
}
