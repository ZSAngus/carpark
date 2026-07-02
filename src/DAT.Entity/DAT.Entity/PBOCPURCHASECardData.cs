namespace DAT.Entity;

public class PBOCPURCHASECardData
{
	public string DATA_NO_DLE_N_REPLY { get; set; }

	public string CardNumber { get; set; }

	public decimal CardBillAmount { get; set; }

	public string BillArea { get; set; }

	public string TxnNo { get; set; }

	public string BillDate { get; set; }

	public string BillTime { get; set; }

	public string ServerCode { get; set; }

	public string LogicNo { get; set; }

	public string DeviceCode { get; set; }

	public string ReceiverCode { get; set; }

	public string IC_Data { get; set; }

	public string AlternateData { get; set; }

	public string MD5 { get; set; }

	public string ReplyCode { get; set; }

	public string ErrorCode { get; set; }

	public EnumPURCHASE_CARD_State State { get; set; }

	public bool Valid { get; set; }
}
