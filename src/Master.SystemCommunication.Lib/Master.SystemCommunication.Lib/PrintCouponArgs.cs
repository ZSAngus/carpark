using System;
using System.Runtime.Serialization;

namespace Master.SystemCommunication.Lib;

/// <summary>
/// 打印優惠券參數
/// </summary>
[DataContract]
public class PrintCouponArgs
{
	[DataMember]
	public string QRCode { get; set; }

	[DataMember]
	public DateTime StartDate { get; set; }

	[DataMember]
	public DateTime ExpireDate { get; set; }

	[DataMember]
	public string UserName { get; set; }

	[DataMember]
	public string LPNumber { get; set; }

	[DataMember]
	public string FreeName { get; set; }
}
