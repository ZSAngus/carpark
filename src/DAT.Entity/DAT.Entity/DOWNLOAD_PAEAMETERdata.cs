namespace DAT.Entity;

internal class DOWNLOAD_PAEAMETERdata
{
	public byte ReplyCode { get; set; }

	public byte ErrorCode { get; set; }

	public DOWNLOAD_PAEAMETER_State State { get; set; }

	public bool Valid { get; set; }
}
