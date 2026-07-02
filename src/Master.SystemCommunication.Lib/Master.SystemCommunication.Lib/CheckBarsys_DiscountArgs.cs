using System.Runtime.Serialization;

namespace Master.SystemCommunication.Lib;

public class CheckBarsys_DiscountArgs
{
	[DataMember]
	public string barcode { get; set; }
}
