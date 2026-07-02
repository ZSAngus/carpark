using System;
using System.Runtime.Serialization;

namespace Master.SystemCommunication.Lib;

/// <summary>
/// 記錄比對協議
/// </summary>
public class RecordContrastArgs : ProgramBase
{
	[DataMember]
	public string GateName { get; set; }

	[DataMember]
	public int GateID { get; set; }

	/// <summary>
	/// 接收者ID
	/// </summary>
	[DataMember]
	public string ReceiveID { get; set; }

	/// <summary>
	/// 登記車牌
	/// </summary>
	[DataMember]
	public string Registration { get; set; }

	/// <summary>
	/// 當前圖片路徑
	/// </summary>
	[DataMember]
	public string ImagePath { get; set; }

	/// <summary>
	/// 當前結果
	/// </summary>
	[DataMember]
	public string currResult { get; set; }

	[DataMember]
	public DateTime CallTimestamp { get; set; }

	[DataMember]
	public int ShowTime { get; set; }

	/// <summary>
	/// 人手處理完結果
	/// </summary>
	[DataMember]
	public bool IsPass { get; set; }

	/// <summary>
	/// 操作人員
	/// </summary>
	[DataMember]
	public string ShiffCode { get; set; }

	public RecordContrastArgs()
	{
	}

	public RecordContrastArgs(string onlyID)
		: base(onlyID)
	{
	}
}
