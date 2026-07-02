namespace DAT.Entity;

public class GET_READERHISTORYdata
{
	public Reader_record_info ReaderRecord { get; set; }

	public string ReplyCode { get; set; }

	public string ErrorCode { get; set; }

	public GET_READERHISTORY_State State { get; set; }

	public bool Valid { get; set; }
}
