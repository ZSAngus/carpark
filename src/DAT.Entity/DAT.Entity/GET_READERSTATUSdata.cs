namespace DAT.Entity;

public class GET_READERSTATUSdata
{
	public string ReaderStatus { get; set; }

	public string ReplyCode { get; set; }

	public string ErrorCode { get; set; }

	public GET_READERSTATUS_State State { get; set; }

	public bool Valid { get; set; }
}
