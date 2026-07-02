using System.Runtime.Serialization;

namespace Master.SystemCommunication.Lib;

/// <summary>
/// 掃描RFID消息
/// 發送到RFID控制，如果讀到卡號則填充CardCode並返回
/// </summary>
public class ScanRFIDArgs : ProgramBase
{
	[DataMember]
	public int GateID { get; set; }

	[DataMember]
	public string CardCode { get; set; }

	[DataMember]
	public int Antenna { get; set; }

	[DataMember]
	public bool LoopStatus { get; set; }

	public ScanRFIDArgs()
	{
	}

	public ScanRFIDArgs(string onlyID)
		: base(onlyID)
	{
	}
}
