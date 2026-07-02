using System;

namespace CarPark2018;

public class BOC_Gate_TransactionExtendEX
{
	public string AlternateData { get; set; }

	public string BillArea { get; set; }

	public string BillAreaB { get; set; }

	public string BillDate { get; set; }

	public string BillTime { get; set; }

	public string CardAppType { get; set; }

	public decimal CardBillAmount { get; set; }

	public string CardNumber { get; set; }

	public string CardPhyType { get; set; }

	public string CardReaderNumber { get; set; }

	public decimal CardRemain { get; set; }

	public int? ChargeTransactionID { get; set; }

	public string Description { get; set; }

	public string DeviceCode { get; set; }

	public string EncryptedCardNumber { get; set; }

	public string ErrorCode { get; set; }

	public int FromGateID { get; set; }

	public string IC_Data { get; set; }

	public int? IsBlack { get; set; }

	public bool ISUploaded { get; set; }

	public string LogicNo { get; set; }

	public string MD5 { get; set; }

	public decimal? OffLineRemain_MOP { get; set; }

	public decimal? OffLineRemain_RMB { get; set; }

	public int? PURCHASE_CARD_State { get; set; }

	public string Purchase_FullData { get; set; }

	public string ReceiverCode { get; set; }

	public string ReplyCode { get; set; }

	public int? REQUEST_CARD_State { get; set; }

	public string ServerCode { get; set; }

	public string StaffInfo { get; set; }

	public int SysTransacionID { get; set; }

	public int TransactionID { get; set; }

	public DateTime TransactionTime { get; set; }

	public string TxnNo { get; set; }

	public bool? Valid { get; set; }
}
