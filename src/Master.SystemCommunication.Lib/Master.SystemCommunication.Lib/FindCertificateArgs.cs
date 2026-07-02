using System.Runtime.Serialization;

namespace Master.SystemCommunication.Lib;

public class FindCertificateArgs
{
	[DataMember]
	public string Barcode { get; set; }
}
