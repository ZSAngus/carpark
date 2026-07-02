using System;
using System.Runtime.Serialization;

namespace Master.SystemCommunication.Lib;

public class CalcTicketFeeReturn
{
	/// <summary>
	/// 是否有效
	/// </summary>
	[DataMember]
	public bool ISValid { get; set; }

	/// <summary>
	/// 無效原因，商戶不存在，免費類型不存在，優惠券條碼無效
	/// </summary>
	[DataMember]
	public string ErrCode { get; set; }

	/// <summary>
	/// 真正超時為true
	/// </summary>
	[DataMember]
	public bool ISTimeOut { get; set; }

	/// <summary>
	/// 是否存在上次收費
	/// 如果HasLastTimeCharge為true,會轉到超時收費窗體，在顯示窗體前如果ISTimeOut為false，提示用戶該票沒超時
	/// </summary>
	[DataMember]
	public bool HasLastTimeCharge { get; set; }

	/// <summary>
	/// 上次繳費時間
	/// </summary>
	[DataMember]
	public DateTime LastTimeCharge { get; set; }
}
