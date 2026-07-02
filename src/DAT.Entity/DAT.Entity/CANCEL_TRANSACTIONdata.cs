namespace DAT.Entity;

public class CANCEL_TRANSACTIONdata
{
	public string ReplyCode { get; set; }

	public string ErrorCode { get; set; }

	public CANCEL_TRANSACTION_State State { get; set; }

	public bool Valid { get; set; }
}
