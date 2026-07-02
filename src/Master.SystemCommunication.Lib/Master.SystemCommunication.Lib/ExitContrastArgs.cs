using System;
using System.Runtime.Serialization;

namespace Master.SystemCommunication.Lib;

/// <summary>
/// 記錄比對協議
/// </summary>
public class ExitContrastArgs : ProgramBase
{
	/// <summary>
	/// 接收者ID
	/// </summary>
	[DataMember]
	public string ReceiveID { get; set; }

	[DataMember]
	public string EnterValue { get; set; }

	[DataMember]
	public string EnterImagePath { get; set; }

	[DataMember]
	public string ExitImagePath { get; set; }

	[DataMember]
	public string ExitValue { get; set; }

	[DataMember]
	public DateTime CallTimestamp { get; set; }

	[DataMember]
	public int ShowTime { get; set; }

	[DataMember]
	public string GateName { get; set; }

	[DataMember]
	public int GateID { get; set; }

	/// <summary>
	/// 人手處理完結果
	/// </summary>
	public bool IsPass { get; set; }

	public string ShiffCode { get; set; }

	public ExitContrastArgs()
	{
	}

	public ExitContrastArgs(string onlyID)
		: base(onlyID)
	{
	}
}
