using System.Collections.Generic;

namespace DAT.Entity;

public class GET_CARDHISTORYdata
{
	public string ReplyCode { get; set; }

	public string ErrorCode { get; set; }

	public GET_CARDHISTORY_State State { get; set; }

	public bool Valid { get; set; }

	public List<Card_record_info> CardRecord { get; set; }
}
