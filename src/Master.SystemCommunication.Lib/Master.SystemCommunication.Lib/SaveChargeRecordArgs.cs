using System;
using System.Runtime.Serialization;

namespace Master.SystemCommunication.Lib;

/// <summary>
/// 保存收費傳入參數
/// </summary>
public class SaveChargeRecordArgs
{
	[DataMember]
	public int CustomFreeID { get; set; }

	[DataMember]
	public int CustomFreeTenatID { get; set; }

	[DataMember]
	public string FreeImagePath { get; set; }

	[DataMember]
	public string TicketNumber { get; set; }

	[DataMember]
	public DateTime InTime { get; set; }

	/// <summary>
	/// 免費記錄的Remark,有免費信息並有備註時有
	/// </summary>
	[DataMember]
	public string CustomFreeRecordRemark { get; set; }

	/// <summary>
	/// 做失票需要傳入場號，記錄在TransactionData備註信息
	/// </summary>
	[DataMember]
	public string TransactionDataRemark { get; set; }

	/// <summary>
	/// 真正超時為true
	/// </summary>
	[DataMember]
	public bool ISTimeOut { get; set; }

	/// <summary>
	/// 優惠券號
	/// </summary>
	[DataMember]
	public string BarCode { get; set; }

	/// <summary>
	/// 業務記錄ID
	/// 轉紙票或失票用來刪除入場記錄
	/// </summary>
	[DataMember]
	public int TransactionID { get; set; }
}
