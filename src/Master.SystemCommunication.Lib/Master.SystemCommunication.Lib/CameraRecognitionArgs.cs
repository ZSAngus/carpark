using System.Runtime.Serialization;

namespace Master.SystemCommunication.Lib;

/// <summary>
/// 捉拍並分析參數
/// </summary>
public class CameraRecognitionArgs : ProgramBase
{
	[DataMember]
	public int GateID { get; set; }

	[DataMember]
	public string PhotoPath { get; set; }

	[DataMember]
	public string velue { get; set; }

	[DataMember]
	public double prob { get; set; }

	[DataMember]
	public string Extend { get; set; }

	/// <summary>
	/// 手工填入車牌
	/// 用於閘機系統向車牌請求時附帶參數
	/// </summary>
	[DataMember]
	public string InputLicensePlate { get; set; }

	public CameraRecognitionArgs()
	{
	}

	public CameraRecognitionArgs(string onlyID)
		: base(onlyID)
	{
	}
}
