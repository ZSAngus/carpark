using System;
using System.Runtime.Serialization;

namespace Master.SystemCommunication.Lib;

public class GetView_TransactionAndLPArgs : ProgramBase
{
	/// <summary>
	/// 入場車牌號
	/// </summary>
	[DataMember]
	public string LicensePlate { get; set; }

	/// <summary>
	/// 入場開始時間
	/// </summary>
	[DataMember]
	public DateTime InStartTime { get; set; }

	/// <summary>
	/// 入場結束時間
	/// </summary>
	[DataMember]
	public DateTime InEndTime { get; set; }

	/// <summary>
	/// 轉紙票用，用戶輸入澳門通號碼
	/// </summary>
	[DataMember]
	public string MPassNumber { get; set; }

	/// <summary>
	/// 閃付轉紙票用，用戶輸入尾4位數
	/// </summary>
	public string QPassNumber { get; set; }

	public GetView_TransactionAndLPArgs()
	{
	}

	public GetView_TransactionAndLPArgs(string onlyID)
		: base(onlyID)
	{
	}
}
